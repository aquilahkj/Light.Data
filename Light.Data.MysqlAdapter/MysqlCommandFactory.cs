using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data.MysqlAdapter
{
	class MysqlCommandFactory : CommandFactory
	{

		DateTimeFormater dateTimeFormater = new DateTimeFormater ();

		readonly string defaultDateTime = "%Y-%m-%d %H:%i:%S";

		public MysqlCommandFactory ()
		{
			//_canInnerPage = true;
			dateTimeFormater.YearFormat = "%Y";
			dateTimeFormater.MonthFormat = "%m";
			dateTimeFormater.DayFormat = "%d";
			dateTimeFormater.HourFormat = "%H";
			dateTimeFormater.MinuteFormat = "%i";
			dateTimeFormater.SecondFormat = "%S";

			_havingAlias = true;
			_orderbyAlias = true;
			//_bulkInsertCompose = true;
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("`{0}`", fieldName);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("`{0}`", tableName);
		}

		public override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)//, bool distinct)
		{
			CommandData command = base.CreateSelectBaseCommand (mapping, customSelect, query, order, region, state);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {1},{2}", command.CommandText, region.Start, region.Size);
				}
				command.InnerPage = true;
			}
			return command;
		}

		public override CommandData CreateSelectJoinTableBaseCommand (string customSelect, IJoinModel[] modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			CommandData command = base.CreateSelectJoinTableBaseCommand (customSelect, modelList, query, order, region, state);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {1},{2}", command.CommandText, region.Start, region.Size);
				}
				command.InnerPage = true;
			}
			return command;
		}

		public override CommandData CreateAggregateTableCommand (DataEntityMapping mapping, AggregateDataFieldInfo [] fieldInfos, QueryExpression query, QueryExpression having, OrderExpression order, Region region, CreateSqlState state)
		{
			CommandData command = base.CreateAggregateTableCommand (mapping, fieldInfos, query, having, order, region, state);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {1},{2}", command.CommandText, region.Start, region.Size);
				}
				command.InnerPage = true;
			}
			return command;
		}

		//public override CommandData [] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		//{
		//	if (entitys == null || entitys.Length == 0) {
		//		throw new ArgumentNullException (nameof (entitys));
		//	}
		//	if (batchCount <= 0) {
		//		batchCount = 10;
		//	}

		//	int totalCount = entitys.Length;
		//	List<string> insertList = new List<string> ();
		//	foreach (DataFieldMapping field in mapping.NoIdentityFields) {
		//		insertList.Add (CreateDataFieldSql (field.Name));
		//	}

		//	string insert = string.Join (",", insertList);
		//	string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

		//	int createCount = 0;
		//	int totalCreateCount = 0;
		//	StringBuilder values = new StringBuilder ();
		//	int paramIndex = 0;
		//	List<DataParameter> dataParams = new List<DataParameter> ();
		//	List<CommandData> commands = new List<CommandData> ();
		//	foreach (object entity in entitys) {
		//		List<DataParameter> entityParams = CreateColumnParameter (mapping.NoIdentityFields, entity);
		//		string [] valueList = new string [entityParams.Count];
		//		int index = 0;
		//		foreach (DataParameter dataParameter in entityParams) {
		//			string paramName = CreateParamName ("P" + paramIndex);
		//			valueList [index] = paramName;
		//			dataParameter.ParameterName = paramName;
		//			dataParams.Add (dataParameter);
		//			index++;
		//			paramIndex++;
		//		}
		//		string value = string.Join (",", valueList);
		//		values.AppendFormat ("({0})", value);
		//		createCount++;
		//		totalCreateCount++;
		//		if (createCount == batchCount || totalCreateCount == totalCount || totalCreateCount == totalCount - 1) {
		//			CommandData command = new CommandData (string.Format ("{0}values{1};", insertsql, values), dataParams);
		//			commands.Add (command);
		//			if (totalCreateCount == totalCount) {
		//				break;
		//			}
		//			dataParams = new List<DataParameter> ();
		//			createCount = 0;
		//			paramIndex = 0;
		//			values = new StringBuilder ();
		//		}
		//		else {
		//			values.Append (",");
		//		}
		//	}
		//	return commands.ToArray ();
		//}


		public override Tuple<CommandData, CreateSqlState> [] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			int totalCount = entitys.Length;
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException ("");
			}
			string [] insertList = new string [insertLen];
			for (int i = 0; i < insertLen; i++) {
				DataFieldMapping field = fields [i];
				insertList [i] = CreateDataFieldSql (field.Name);
			}
			string insert = string.Join (",", insertList);
			string insertSql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();

			totalSql.AppendFormat ("{0}values", insertSql);

			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				string [] valuesList = new string [insertLen];
				for (int i = 0; i < insertLen; i++) {
					DataFieldMapping field = fields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					valuesList [i] = state.AddDataParameter (value, field.DBType, ParameterDirection.Input);
				}
				string values = string.Join (",", valuesList);

				totalSql.AppendFormat ("({0})", values);

				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount || totalCreateCount == totalCount - 1) {
					totalSql.Append (";");
					CommandData command = new CommandData (totalSql.ToString ());
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
					totalSql.AppendFormat ("{0}values", insertSql);
				}
				else {
					totalSql.Append (",");
				}
			}
			return list.ToArray ();
		}


		//public override CommandData CreateDynamicAggregateCommand (DataEntityMapping mapping, List<DataFieldInfo> fields, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		//{
		//	if (fields == null || fields.Count == 0) {
		//		throw new LightDataException (RE.DynamicAggregateFieldIsNotExists);
		//	}
		//	StringBuilder sql = new StringBuilder ();

		//	string [] selectList = new string [fields.Count + functions.Count];
		//	string [] groupbyList = new string [fields.Count];
		//	int index = 0;
		//	List<DataParameter> innerParameters = new List<DataParameter> ();
		//	foreach (DataFieldInfo fieldInfo in fields) {
		//		if (!mapping.Equals (fieldInfo.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] dataParameters = null;
		//		string groupbyField = fieldInfo.CreateDataFieldSql (this, false, out dataParameters);
		//		groupbyList [index] = groupbyField;
		//		AliasDataFieldInfo aliasInfo = fieldInfo as AliasDataFieldInfo;
		//		if (!Object.Equals (aliasInfo, null)) {
		//			selectList [index] = aliasInfo.CreateAliasDataFieldSql (this, false, out dataParameters);
		//		}
		//		else {
		//			selectList [index] = groupbyField;
		//		}
		//		index++;
		//	}
		//	List<DataParameter> functionParameters = new List<DataParameter> ();
		//	foreach (AggregateDataInfo functionInfo in functions) {
		//		AggregateData function = functionInfo.Data;
		//		if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] aggparameters;
		//		string aggField = function.CreateSqlString (this, false, out aggparameters);
		//		string selectField = CreateAliasSql (aggField, functionInfo.Name);
		//		selectList [index] = selectField;
		//		if (aggparameters != null && aggparameters.Length > 0) {
		//			functionParameters.AddRange (aggparameters);
		//		}
		//		index++;
		//	}
		//	string select = string.Join (",", selectList);
		//	string groupby = string.Join (",", groupbyList);
		//	sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

		//	DataParameter [] queryparameters;
		//	string queryString = GetQueryString (query, out queryparameters);
		//	DataParameter [] havingparameters;
		//	string havingString = GetHavingString (having, out havingparameters, functions);
		//	DataParameter [] orderparameters;
		//	string orderString = GetOrderString (order, out orderparameters, fields, functions);

		//	if (!string.IsNullOrEmpty (queryString)) {
		//		sql.AppendFormat (" {0}", queryString);
		//	}

		//	sql.AppendFormat (" group by {0}", groupby);

		//	if (!string.IsNullOrEmpty (havingString)) {
		//		sql.AppendFormat (" {0}", havingString);
		//	}

		//	if (!string.IsNullOrEmpty (orderString)) {
		//		sql.AppendFormat (" {0}", orderString);
		//	}
		//	DataParameter [] parameters = DataParameter.ConcatDataParameters (innerParameters, functionParameters, queryparameters, havingparameters, orderparameters);
		//	CommandData command = new CommandData (sql.ToString (), parameters);
		//	command.TransParamName = true;
		//	return command;
		//}

		//public override CommandData CreateAggregateTableCommand (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		//{
		//	StringBuilder sql = new StringBuilder ();

		//	string [] selectList = new string [groupbys.Count + functions.Count];
		//	string [] groupbyList = new string [groupbys.Count];
		//	int index = 0;
		//	List<DataParameter> innerParameters = new List<DataParameter> ();
		//	foreach (AggregateDataInfo groupbyInfo in groupbys) {
		//		AggregateData data = groupbyInfo.Data;
		//		if (!mapping.Equals (data.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] dataParameters = null;
		//		string groupbyField = data.CreateSqlString (this, false, out dataParameters);
		//		groupbyList [index] = groupbyField;
		//		string selectField = CreateAliasSql (groupbyField, groupbyInfo.Name);
		//		selectList [index] = selectField;
		//		if (dataParameters != null && dataParameters.Length > 0) {
		//			innerParameters.AddRange (dataParameters);
		//		}
		//		index++;
		//	}
		//	List<DataParameter> functionParameters = new List<DataParameter> ();
		//	foreach (AggregateDataInfo functionInfo in functions) {
		//		AggregateData function = functionInfo.Data;
		//		if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] aggparameters;
		//		string aggField = function.CreateSqlString (this, false, out aggparameters);
		//		string selectField = CreateAliasSql (aggField, functionInfo.Name);
		//		selectList [index] = selectField;
		//		if (aggparameters != null && aggparameters.Length > 0) {
		//			functionParameters.AddRange (aggparameters);
		//		}
		//		index++;
		//	}
		//	string select = string.Join (",", selectList);
		//	string groupby = string.Join (",", groupbyList);
		//	sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

		//	DataParameter [] queryparameters;
		//	string queryString = GetQueryString (query, out queryparameters);
		//	DataParameter [] havingparameters;
		//	//string havingString = GetHavingString (having, out havingparameters, functions);
		//	string havingString = GetHavingString (having, out havingparameters);
		//	DataParameter [] orderparameters;
		//	//string orderString = GetOrderString (order, out orderparameters, groupbys, functions);
		//	string orderString = GetOrderString (order, out orderparameters);
		//	if (!string.IsNullOrEmpty (queryString)) {
		//		sql.AppendFormat (" {0}", queryString);
		//	}

		//	sql.AppendFormat (" group by {0}", groupby);

		//	if (!string.IsNullOrEmpty (havingString)) {
		//		sql.AppendFormat (" {0}", havingString);
		//	}

		//	if (!string.IsNullOrEmpty (orderString)) {
		//		sql.AppendFormat (" {0}", orderString);
		//	}
		//	DataParameter [] parameters = DataParameter.ConcatDataParameters (innerParameters, functionParameters, queryparameters, havingparameters, orderparameters);
		//	CommandData command = new CommandData (sql.ToString (), parameters);
		//	command.TransParamName = true;
		//	return command;
		//}


		public override string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, IEnumerable<object> list)
		{
			if (predicate == QueryCollectionPredicate.In || predicate == QueryCollectionPredicate.NotIn) {
				return base.CreateCollectionParamsQuerySql (fieldName, predicate, list);
			}
			string op = GetQueryCollectionPredicate (predicate);

			int i = 0;
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {1} (", fieldName, op);
			foreach (object item in list) {
				if (i > 0)
					sb.Append (" union all ");
				sb.AppendFormat ("select {0}", item);
				i++;
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
		{
			return "rand()";
		}

		protected override string CreateIdentitySql (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField != null) {
				return "select last_insert_id();";
			}
			else {
				return string.Empty;
			}
		}

		public override string CreateMatchSql (object field, bool starts, bool ends)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("concat(");
			if (starts) {
				sb.AppendFormat ("'{0}',", _wildcards);
			}
			sb.Append (field);
			if (ends) {
				sb.AppendFormat (",'{0}'", _wildcards);
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public override string CreateConcatSql (params object [] values)
		{
			string value1 = string.Join (",", values);
			string sql = string.Format ("concat({0})", value1);
			return sql;
		}

		public override string CreateDualConcatSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("concat({0},{1})", field, value);
			}
			else {
				return string.Format ("concat({0},{1})", value, field);
			}
		}

		public override string CreateDateSql (object field, string format)
		{
			if (string.IsNullOrEmpty (format)) {
				return string.Format ("date({0})", field);
			}
			else {
				string format1 = format.ToUpper ();
				string sqlformat;
				switch (format1) {
				case "YMD":
					sqlformat = "%Y%m%d";
					break;
				case "YM":
					sqlformat = "%Y%m";
					break;
				case "Y-M-D":
					sqlformat = "%Y-%m-%d";
					break;
				case "Y-M":
					sqlformat = "%Y-%m";
					break;
				case "M-D-Y":
					sqlformat = "%m-%d-%Y";
					break;
				case "D-M-Y":
					sqlformat = "%d-%m-%Y";
					break;
				case "Y/M/D":
					sqlformat = "%Y/%m/%d";
					break;
				case "Y/M":
					sqlformat = "%Y/%m";
					break;
				case "M/D/Y":
					sqlformat = "%m/%d/%Y";
					break;
				case "D/M/Y":
					sqlformat = "%d/%m/%Y";
					break;
				default:
					throw new LightDataException (string.Format (RE.UnsupportDateFormat, format));
				}
				return string.Format ("date_format({0},'{1}')", field, sqlformat);
			}
		}

		public override string CreateDateSql (object field)
		{
			return string.Format ("date({0})", field);
		}

		public override string CreateDateTimeFormatSql (string field, string format)
		{
			string sqlformat;
			if (string.IsNullOrEmpty (format)) {
				sqlformat = defaultDateTime;
			}
			else {
				sqlformat = dateTimeFormater.FormatData (format);
			}
			return string.Format ("date_format({0},'{1}')", field, sqlformat);
		}

		public override string CreateYearSql (object field)
		{
			return string.Format ("year({0})", field);
		}

		public override string CreateMonthSql (object field)
		{
			return string.Format ("month({0})", field);
		}

		public override string CreateDaySql (object field)
		{
			return string.Format ("day({0})", field);
		}

		public override string CreateHourSql (object field)
		{
			return string.Format ("hour({0})", field);
		}

		public override string CreateMinuteSql (object field)
		{
			return string.Format ("minute({0})", field);
		}

		public override string CreateSecondSql (object field)
		{
			return string.Format ("second({0})", field);
		}

		public override string CreateWeekSql (object field)
		{
			return string.Format ("week({0},7)", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("dayofweek({0})-1", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("dayofyear({0})", field);
		}

		public override string CreateLengthSql (object field)
		{
			return string.Format ("length({0})", field);
		}

		public override string CreateSubStringSql (object field, object start, object size)
		{
			if (object.Equals (size, null)) {
				return string.Format ("substring({0},{1})", field, start);
			}
			else {
				return string.Format ("substring({0},{1},{2})", field, start, size);
			}
		}

		public override string CreateIndexOfSql (object field, object value, object startIndex)
		{
			if (object.Equals (startIndex, null)) {
				return string.Format ("locate({0},{1})", field, value);
			}
			else {
				return string.Format ("locate({0},{1},{2})", field, value, startIndex);
			}
		}

		public override string CreateReplaceSql (object field, object oldValue, object newValue)
		{
			return string.Format ("replace({0},{1},{2})", field, oldValue, newValue);
		}

		public override string CreateToLowerSql (object field)
		{
			return string.Format ("lower({0})", field);
		}

		public override string CreateToUpperSql (object field)
		{
			return string.Format ("upper({0})", field);
		}

		public override string CreateTrimSql (object field)
		{
			return string.Format ("trim({0})", field);
		}

		public override string CreatePowerSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("power({0},{1})", field, value);
			}
			else {
				return string.Format ("power({0},{1})", value, field);
			}
		}

		public override string CreatePowerSql (object left, object right)
		{
			return string.Format ("power({0},{1})", left, right);
		}

		public override string CreateDataBaseTimeSql ()
		{
			return "now()";
		}

		public override string CreateParamName (string name)
		{
			if (!name.StartsWith ("?", StringComparison.Ordinal)) {
				return "?" + name;
			}
			else {
				return name;
			}
		}
	}
}

