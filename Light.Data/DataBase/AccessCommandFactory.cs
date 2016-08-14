using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class AccessCommandFactory : CommandFactory
	{

		DateTimeFormater dateTimeFormater = new DateTimeFormater ();

		readonly string defaultDateTime = "yyyy-mm-dd Hh:Nn:Ss";

		public AccessCommandFactory ()
		{
			dateTimeFormater.YearFormat = "yyyy";
			dateTimeFormater.MonthFormat = "mm";
			dateTimeFormater.DayFormat = "dd";
			dateTimeFormater.HourFormat = "Hh";
			dateTimeFormater.MinuteFormat = "Nn";
			dateTimeFormater.SecondFormat = "Ss";
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("[{0}]", fieldName);
		}

		public void UseAccessWildcards ()
		{
			_wildcards = "*";
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
		//	string havingString = GetHavingString (having, out havingparameters);
		//	DataParameter [] orderparameters;
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

		public override CommandData CreateDynamicAggregateCommand (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		{
			StringBuilder sql = new StringBuilder ();

			string [] selectList = new string [groupbys.Count + functions.Count];
			string [] groupbyList = new string [groupbys.Count];
			int index = 0;
			List<DataParameter> innerParameters = new List<DataParameter> ();
			foreach (AggregateDataInfo groupbyInfo in groupbys) {
				AggregateData data = groupbyInfo.Data;
				if (!mapping.Equals (data.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				DataParameter [] dataParameters = null;
				string groupbyField = data.CreateSqlString (this, false, out dataParameters);
				groupbyList [index] = groupbyField;
				string selectField = CreateAliasSql (groupbyField, groupbyInfo.Name);
				selectList [index] = selectField;
				if (dataParameters != null && dataParameters.Length > 0) {
					innerParameters.AddRange (dataParameters);
				}
				index++;
			}
			List<DataParameter> functionParameters = new List<DataParameter> ();
			foreach (AggregateDataInfo functionInfo in functions) {
				AggregateData function = functionInfo.Data;
				if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				DataParameter [] aggparameters;
				string aggField = function.CreateSqlString (this, false, out aggparameters);
				string selectField = CreateAliasSql (aggField, functionInfo.Name);
				selectList [index] = selectField;
				if (aggparameters != null && aggparameters.Length > 0) {
					functionParameters.AddRange (aggparameters);
				}
				index++;
			}
			string select = string.Join (",", selectList);
			string groupby = string.Join (",", groupbyList);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

			DataParameter [] queryparameters;
			string queryString = GetQueryString (query, out queryparameters);
			DataParameter [] havingparameters;
			string havingString = GetHavingString (having, out havingparameters);
			DataParameter [] orderparameters;
			string orderString = GetOrderString (order, out orderparameters);

			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}

			sql.AppendFormat (" group by {0}", groupby);

			if (!string.IsNullOrEmpty (havingString)) {
				sql.AppendFormat (" {0}", havingString);
			}

			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (innerParameters, functionParameters, queryparameters, havingparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}



		//public override string GetHavingString (AggregateHavingExpression having, out DataParameter [] parameters, List<AggregateFunctionInfo> functions)
		//{
		//	string havingString = null;
		//	parameters = null;
		//	if (having != null) {
		//		havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate {
		//			return null;
		//		})));
		//	}
		//	return havingString;
		//}

		//public override string GetOrderString (OrderExpression order, out DataParameter [] parameters, List<DataFieldInfo> fields, List<AggregateDataInfo> functions)
		//{
		//	string orderString = null;
		//	parameters = null;
		//	if (order != null) {
		//		orderString = string.Format ("order by {0}", order.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate {
		//			return null;
		//		})));
		//	}
		//	return orderString;
		//}

		public override string CreateConditionCountSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("count({2}iif({0},{1},null))", expressionSql, !Object.Equals (fieldName, null) ? fieldName : "1", isDistinct ? "distinct " : "");
		}

		public override string CreateConditionSumSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("sum({2}iif({0},{1},0))", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public override string CreateConditionAvgSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("avg({2}iif({0},cdbl({1}),0,)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		/// <summary>
		/// 创建内容Exists查询命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="query">查询表达式</param>
		/// <returns></returns>
		public override CommandData CreateExistsCommand (DataEntityMapping mapping, QueryExpression query)
		{
			return this.CreateSelectBaseCommand (mapping, "top 1 1", null, query, null, null);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("[{0}]", tableName);
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
		{
			Random rnd = new Random (unchecked((int)DateTime.Now.Ticks));
			int intRandomNumber = rnd.Next () * -1;

			DataFieldMapping keyfield = null;
			string fieldNames = null;
			string tableName = aliasName ?? mapping.TableName;
			DataTableEntityMapping tableMapping = mapping as DataTableEntityMapping;
			if (tableMapping != null) {
				if (tableMapping.HasIdentity) {
					fieldNames = CreateRandomField (tableMapping.IdentityField, tableName, fullFieldName);
				}
				else if (tableMapping.HasPrimaryKey) {
					List<string> list = new List<string> ();
					foreach (DataFieldMapping item in tableMapping.PrimaryKeyFields) {
						string name = CreateRandomField (item, tableName, fullFieldName);
						list.Add (name);
					}
					fieldNames = string.Join ("*", list);
				}
			}
			if (keyfield == null) {
				DataFieldMapping stringField = null;
				DataFieldMapping numberField = null;
				foreach (DataFieldMapping field in mapping.DataEntityFields) {
					if (field.TypeCode == TypeCode.Boolean || field.TypeCode == TypeCode.DBNull || field.TypeCode == TypeCode.Empty || field.TypeCode == TypeCode.Object) {
						continue;
					}
					else if (field.TypeCode == TypeCode.String) {
						if (stringField != null) {
							stringField = field;
						}
					}
					else {
						numberField = field;
						break;
					}
				}
				if (numberField != null) {
					fieldNames = CreateRandomField (numberField, tableName, fullFieldName);
				}
				else if (stringField != null) {
					fieldNames = CreateRandomField (stringField, tableName, fullFieldName);
				}
			}
			if (fieldNames != null) {
				return string.Format ("rnd({0}*{1})", intRandomNumber, fieldNames);
			}
			else {
				throw new LightDataException (RE.DataFieldIsNotStringType);
			}
		}

		private string CreateRandomField (FieldMapping keyfield, string tableName, bool fullFieldName)
		{
			string field;
			if (fullFieldName) {
				field = CreateFullDataFieldSql (tableName, keyfield.Name);
			}
			else {
				field = CreateDataFieldSql (keyfield.Name);
			}
			if (keyfield.TypeCode == TypeCode.String) {
				field = string.Format ("len({0})", field);
			}
			return field;
		}

		public override string CreateMatchSql (object field, bool starts, bool ends)
		{
			StringBuilder sb = new StringBuilder ();
			if (starts) {
				sb.AppendFormat ("'{0}'+", _wildcards);
			}
			sb.Append (field);
			if (ends) {
				sb.AppendFormat ("+'{0}'", _wildcards);
			}
			return sb.ToString ();
		}

		public override string CreateDateSql (object field, string format)
		{
			if (string.IsNullOrEmpty (format)) {
				return string.Format ("cdate(format({0}),'yyyy-mm-dd')", field);
			}
			else {
				string format1 = format.ToUpper ();
				string sqlformat;
				switch (format1) {
				case "YMD":
					sqlformat = "yyyymmdd";
					break;
				case "YM":
					sqlformat = "yyyymm";
					break;
				case "Y-M-D":
					sqlformat = "yyyy-mm-dd";
					break;
				case "Y-M":
					sqlformat = "yyyy-mm";
					break;
				case "M-D-Y":
					sqlformat = "mm-dd-yyyy";
					break;
				case "D-M-Y":
					sqlformat = "dd-mm-yyyy";
					break;
				case "Y/M/D":
					sqlformat = "yyyy/mm/dd";
					break;
				case "Y/M":
					sqlformat = "yyyy/mm";
					break;
				case "M/D/Y":
					sqlformat = "mm/dd/yyyy";
					break;
				case "D/M/Y":
					sqlformat = "dd/mm/yyyy";
					break;
				default:
					throw new LightDataException (string.Format (RE.UnsupportDateFormat, format));
				}
				return string.Format ("format({0},'{1}')", field, sqlformat);
			}
		}

		public override string CreateDateSql (object field)
		{
			return string.Format ("cdate(format({0}),'yyyy-mm-dd')", field);
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
			return string.Format ("format({0},'{1}')", field, sqlformat);
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
			return string.Format ("val(format({0},'ww'))", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("weekday({0})", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("val(format({0},'y'))", field);
		}

		public override string CreateLengthSql (object field)
		{
			return string.Format ("len({0})", field);
		}

		public override string CreateDataBaseTimeSql ()
		{
			return "now()";
		}

		public override string CreateSubStringSql (object field, object start, object size)
		{
			if (object.Equals (size, null)) {
				return string.Format ("mid({0},{1})", field, start);
			}
			else {
				return string.Format ("mid({0},{1},{2})", field, start, size);
			}
		}

		public override string CreateIndexOfSql (object field, object value, object startIndex)
		{
			if (object.Equals (startIndex, null)) {
				return string.Format ("InStr({0},{1})", field, value);
			}
			else {
				return string.Format ("InStr({0},{1},{2})", startIndex, field, value);
			}
		}

		public override string CreateReplaceSql (object field, object oldValue, object newValue)
		{
			return string.Format ("replace({0},{1},{2})", field, oldValue, newValue);
		}

		public override string CreateToLowerSql (object field)
		{
			return string.Format ("lcase({0})", field);
		}

		public override string CreateToUpperSql (object field)
		{
			return string.Format ("ucase({0})", field);
		}

		public override string CreateTrimSql (object field)
		{
			return string.Format ("trim({0})", field);
		}

		public override string CreateModSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0} mod {1})", field, value);
			}
			else {
				return string.Format ("({0} mod {1})", value, field);
			}
		}

		public override string CreateAtanSql (object field)
		{
			return string.Format ("atn({0})", field);
		}
	}
}
