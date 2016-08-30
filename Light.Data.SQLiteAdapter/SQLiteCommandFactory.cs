using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Light.Data.SQLiteAdapter
{
	class SQLiteCommandFactory : CommandFactory
	{
		DateTimeFormater dateTimeFormater = new DateTimeFormater ();

		readonly string defaultDateTime = "%Y-%m-%d %H:%M:%S";

		public SQLiteCommandFactory ()
		{
			//_canInnerPage = true;
			dateTimeFormater.YearFormat = "%Y";
			dateTimeFormater.MonthFormat = "%m";
			dateTimeFormater.DayFormat = "%d";
			dateTimeFormater.HourFormat = "%H";
			dateTimeFormater.MinuteFormat = "%M";
			dateTimeFormater.SecondFormat = "%S";
		}

		public override CommandData CreateTruncateTableCommand (DataTableEntityMapping mapping)
		{
			string sql = string.Format ("delete from {0};update sqlite_sequence set seq=0 where name='{1}';", CreateDataTableSql (mapping), mapping.TableName);
			CommandData command = new CommandData (sql);
			return command;
		}

		//		public string CreateDateTimeDataFieldSql (string fieldName)
		//		{
		//			return string.Format ("datetime(\"{0}\")", fieldName);
		//		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("\"{0}\"", fieldName);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("\"{0}\"", tableName);
		}

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
				if (createCount == batchCount || totalCreateCount == totalCount) {
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


		public override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)//, bool distinct)
		{
			CommandData command = base.CreateSelectBaseCommand (mapping, customSelect, query, order, region, state);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {2} offset {1}", command.CommandText, region.Start, region.Size);
				}
				command.InnerPage = true;
			}
			return command;
		}

		public override CommandData CreateSelectJoinTableBaseCommand (string customSelect, IJoinModel [] modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			CommandData command = base.CreateSelectJoinTableBaseCommand (customSelect, modelList, query, order, region, state);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {2} offset {1}", command.CommandText, region.Start, region.Size);
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
					command.CommandText = string.Format ("{0} limit {2} offset {1}", command.CommandText, region.Start, region.Size);
				}
				command.InnerPage = true;
			}
			return command;
		}

		public override string CreateDividedSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("(cast({0} as real)/{1})", field, value);
			}
			else {
				return string.Format ("({0}/cast({1} as real))", value, field);
			}
		}


		public override string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, IEnumerable<object> list)
		{
			if (predicate == QueryCollectionPredicate.In || predicate == QueryCollectionPredicate.NotIn) {
				return base.CreateCollectionParamsQuerySql (fieldName, predicate, list);
			}
			else {
				throw new NotSupportedException ();
			}
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
		{
			return "random()";
		}

		protected override string CreateIdentitySql (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField != null) {
				return "select last_insert_rowid();";
			}
			else {
				return string.Empty;
			}
		}

		public override string CreateMatchSql (object field, bool starts, bool ends)
		{
			StringBuilder sb = new StringBuilder ();
			if (starts) {
				sb.AppendFormat ("'{0}'||", _wildcards);
			}
			sb.Append (field);
			if (ends) {
				sb.AppendFormat ("||'{0}'", _wildcards);
			}
			return sb.ToString ();
		}

		public override string CreateConcatSql (params object [] values)
		{
			string value1 = string.Join ("||", values);
			string sql = string.Format ("({0})", value1);
			return sql;
		}

		public override string CreateDualConcatSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}||{1})", field, value);
			}
			else {
				return string.Format ("({0}||{1})", value, field);
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
				return string.Format ("strftime('{1}',{0})", field, sqlformat);
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
			return string.Format ("strftime('{1}',{0})", field, sqlformat);
		}

		public override string CreateYearSql (object field)
		{
			return string.Format ("strftime('%Y',{0})", field);
		}

		public override string CreateMonthSql (object field)
		{
			return string.Format ("strftime('%m',{0})", field);
		}

		public override string CreateDaySql (object field)
		{
			return string.Format ("strftime('%d',{0})", field);
		}

		public override string CreateHourSql (object field)
		{
			return string.Format ("strftime('%H',{0})", field);
		}

		public override string CreateMinuteSql (object field)
		{
			return string.Format ("strftime('%M',{0})", field);
		}

		public override string CreateSecondSql (object field)
		{
			return string.Format ("strftime('%S',{0})", field);
		}

		public override string CreateWeekSql (object field)
		{
			return string.Format ("strftime('%W',{0})", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("strftime('%w',{0})", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("strftime('%j',{0})", field);
		}

		public override string CreateLengthSql (object field)
		{
			return string.Format ("length({0})", field);
		}

		public override string CreateSubStringSql (object field, object start, object size)
		{
			if (object.Equals (size, null)) {
				return string.Format ("substr({0},{1})", field, start);
			}
			else {
				return string.Format ("substr({0},{1},{2})", field, start, size);
			}
		}

		public override string CreateIndexOfSql (object field, object value, object startIndex)
		{
			if (object.Equals (startIndex, null)) {
				return string.Format ("instr({0},{1})", field, value);
			}
			else {
				throw new NotSupportedException ();
				//return string.Format ("instr({0},{1},{2})", field, value, startIndex);
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

		public override string CreateDataBaseTimeSql ()
		{
			return "now";
		}

		public override string CreateParamName (string name)
		{
			if (name == null)
				throw new ArgumentNullException (nameof (name));
			if (!name.StartsWith ("@", StringComparison.Ordinal)) {
				return "@" + name;
			}
			else {
				return name;
			}
		}

		//public override string GetHavingString (AggregateHavingExpression having, out DataParameter [] parameters, List<AggregateFunctionInfo> functions)
		//{
		//	string havingString = null;
		//	parameters = null;
		//	if (having != null) {
		//		havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate (object obj) {
		//			return null;
		//		})));
		//	}
		//	return havingString;
		//}

		public override string CreatePowerSql (object field, object value, bool forward)
		{
			throw new NotSupportedException ();
		}

		public override string CreateLogSql (object field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateExpSql (object field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateSinSql (object field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateCosSql (object field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateTanSql (object field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateAtanSql (object field)
		{
			throw new NotSupportedException ();
		}

		//		public override CommandData CreateSelectCommand (DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region)
		//		{
		//			if (region != null && !_canInnerPage) {
		//				throw new LightDataException (RE.DataBaseNotSupportInnerPage);
		//			}
		//			CommandData data;
		//			if (mapping.HasJoinRelateModel) {
		//				JoinCapsule capsule = mapping.LoadJoinCapsule (query, order);
		//				data = CreateSelectRelateTableCommand (capsule.Slector, capsule.Models);
		//				RelationContent rc = new RelationContent ();
		//				rc.SetRelationMap (capsule.RelationMap);
		//				data.State = rc;
		//				return data;
		//			}
		//
		//			string[] fieldNames = new string[mapping.FieldCount];
		//			int i = 0;
		//			foreach (DataFieldMapping field in mapping.DataEntityFields) {
		//				if (field.ObjectType == typeof(DateTime)) {
		//					fieldNames [i] = CreateDateTimeDataFieldSql (field.Name);
		//				}
		//				else {
		//					fieldNames [i] = CreateDataFieldSql (field.Name);
		//				}
		//				i++;
		//			}
		//			string selectString = string.Join (",", fieldNames);
		//			data = this.CreateSelectBaseCommand (mapping, selectString, query, order, region);
		//			if (mapping.HasMultiRelateModel) {
		//				data.State = new RelationContent ();
		//			}
		//			return data;
		//		}
	}
}

