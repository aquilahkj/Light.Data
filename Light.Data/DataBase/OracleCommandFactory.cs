using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class OracleCommandFactory : CommandFactory
	{
		byte _roundScale = 8;

		//Regex _roundCaptureRegex;

		//Regex _roundReplaceRegex;

		bool _identityAuto;

		bool _strictMode;

		public void SetIdentityAuto (bool identityAuto)
		{
			_identityAuto = identityAuto;
		}

		public void SetStrictMode (bool strictMode)
		{
			_strictMode = strictMode;
		}

		DateTimeFormater dateTimeFormater = new DateTimeFormater ();

		readonly string defaultDateTime = "YYYY-MM-DD HH:MI:SS";

		public OracleCommandFactory ()
		{
			_identityAuto = true;
			_canInnerPage = true;
			_strictMode = true;

			dateTimeFormater.YearFormat = "YYYY";
			dateTimeFormater.MonthFormat = "MM";
			dateTimeFormater.DayFormat = "DD";
			dateTimeFormater.HourFormat = "HH";
			dateTimeFormater.MinuteFormat = "MI";
			dateTimeFormater.SecondFormat = "SS";
		}

		public void SetRoundScale (byte scale)
		{
			_roundScale = scale;
			//LoadRoundRegex ();
		}

		//private void LoadRoundRegex ()
		//{
		//	_roundCaptureRegex = new Regex (string.Format (@"(?<=round\().*(?=,{0}\))", _roundScale), RegexOptions.Compiled);
		//	_roundReplaceRegex = new Regex (string.Format (@"round\(.*,{0}\)", _roundScale), RegexOptions.Compiled);
		//}

		public override string CreateDataFieldSql (string fieldName)
		{
			if (_strictMode) {
				return string.Format ("\"{0}\"", fieldName);
			}
			else {
				return fieldName;
			}
		}

		public override string CreateDataTableSql (string tableName)
		{
			if (_strictMode) {
				return string.Format ("\"{0}\"", tableName);
			}
			else {
				return tableName;
			}
		}


		public override CommandData CreateTruncateTableCommand (DataTableEntityMapping mapping)
		{
			const string TRUNCATE_SQL = "declare\ntmp NUMBER;\ncmd_str VARCHAR(256);\nbegin\ncmd_str := 'truncate table {0} ';\nexecute immediate cmd_str;\ncmd_str := 'alter sequence {1} minvalue 0';\nexecute immediate cmd_str;\ncmd_str := 'select {1}.nextval from dual';\nexecute immediate cmd_str into tmp;\ntmp:=(-1*tmp);\ncmd_str := 'alter sequence {1} increment by '|| tmp;\nexecute immediate cmd_str;\ncmd_str := 'select {1}.nextval from dual';\nexecute immediate cmd_str into tmp;\ncmd_str := 'alter sequence {1} increment by 1';\nexecute immediate cmd_str;\nend;\n";

			string sql;
			if (mapping.IdentityField != null) {
				string seq = GetIndentitySeq (mapping);
				sql = string.Format (TRUNCATE_SQL, CreateDataTableSql (mapping), CreateDataTableSql (seq));
			}
			else {
				sql = string.Format ("truncate table {0}", CreateDataTableSql (mapping));
			}

			CommandData command = new CommandData (sql);
			return command;
			//			string sql = string.Format ("truncate table {0}", CreateDataTableSql (mapping));
			//			CommandData command = new CommandData (sql);
			//			return command;
		}

		//public override string CreateBooleanSql (bool value)
		//{
		//	return value ? "1" : "0";
		//}

		public override CommandData CreateInsertCommand (DataTableEntityMapping mapping, object entity)
		{
			bool identityAuto = CheckIndentityAuto (mapping);

			List<DataParameter> paramList = CreateColumnParameter (mapping.NoIdentityFields, entity);

			string [] insertList = new string [paramList.Count];
			string [] valuesList = new string [paramList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in paramList) {
				string paramName = CreateParamName ("P" + index);
				insertList [index] = CreateDataFieldSql (dataParameter.ParameterName);
				valuesList [index] = paramName;
				dataParameter.ParameterName = paramName;
				index++;
			}
			string insert = string.Join (",", insertList);
			string values = string.Join (",", valuesList);
			StringBuilder sql = new StringBuilder ();
			if (!identityAuto && mapping.IdentityField != null) {
				sql.AppendFormat ("insert into {0}({3},{1})values({4}.nextval,{2})", CreateDataTableSql (mapping.TableName), insert, values, CreateDataFieldSql (mapping.IdentityField.Name), GetIndentitySeq (mapping));
			}
			else {
				sql.AppendFormat ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			}
			CommandData command = new CommandData (sql.ToString (), paramList);
			return command;
		}

		static Dictionary<DataTableEntityMapping, bool> IdentityDict = new Dictionary<DataTableEntityMapping, bool> ();

		private bool CheckIndentityAuto (DataTableEntityMapping mapping)
		{
			bool identityAuto = false;

			if (mapping.IdentityField != null) {
				bool ret;
				if (IdentityDict.TryGetValue (mapping, out ret)) {
					identityAuto = ret;
				}
				else {
					string oracleIdentityAuto = mapping.ExtentParams ["OracleIdentityAuto"];
					if (oracleIdentityAuto != null && bool.TryParse (oracleIdentityAuto, out ret)) {
						identityAuto = ret;
					}
					else {
						identityAuto = _identityAuto;
					}
					IdentityDict [mapping] = identityAuto;
				}
			}
			return identityAuto;
		}

		public override CommandData [] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			bool identityAuto = CheckIndentityAuto (mapping);
			int totalCount = entitys.Length;
			List<string> insertList = new List<string> ();
			foreach (DataFieldMapping field in mapping.NoIdentityFields) {
				insertList.Add (CreateDataFieldSql (field.Name));
			}
			string insert = string.Join (",", insertList);
			string insertsql;
			if (!identityAuto && mapping.IdentityField != null) {
				insertsql = string.Format ("insert into {0}({2},{1})", CreateDataTableSql (mapping.TableName), insert, CreateDataFieldSql (mapping.IdentityField.Name));
			}
			else {
				insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);
			}

			int createCount = 0;
			int totalCreateCount = 0;
			string identityString = null;
			if (identityAuto) {
				identityString = GetIndentitySeq (mapping);
			}
			StringBuilder totalSql = new StringBuilder ();
			int paramIndex = 0;
			List<DataParameter> dataParams = new List<DataParameter> ();
			List<CommandData> commands = new List<CommandData> ();

			foreach (object entity in entitys) {
				List<DataParameter> entityParams = CreateColumnParameter (mapping.NoIdentityFields, entity);
				string [] valueList = new string [entityParams.Count];
				int index = 0;
				foreach (DataParameter dataParameter in entityParams) {
					string paramName = CreateParamName ("P" + paramIndex);
					valueList [index] = paramName;
					dataParameter.ParameterName = paramName;
					dataParams.Add (dataParameter);
					index++;
					paramIndex++;
				}
				string value = string.Join (",", valueList);
				if (!identityAuto && mapping.IdentityField != null) {
					totalSql.AppendFormat ("{0}values({2}.nextval,{1});", insertsql, value, identityString);
				}
				else {
					totalSql.AppendFormat ("{0}values({1});", insertsql, value);
				}
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					CommandData command = new CommandData (string.Format ("begin {0} end;", totalSql), dataParams);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					dataParams = new List<DataParameter> ();
					createCount = 0;
					paramIndex = 0;
					totalSql = new StringBuilder ();
				}
			}
			return commands.ToArray ();
		}

		protected override string CreateIdentitySql (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField != null) {
				string seq = GetIndentitySeq (mapping);
				return string.Format ("select {0}.currval from dual", CreateDataTableSql (seq));
			}
			else {
				return string.Empty;
			}
		}

		private static string GetIndentitySeq (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField == null) {
				throw new LightDataException (RE.DataTableNotIdentityField);
			}
			string seq;
			string oracleIdentity = mapping.ExtentParams ["OracleIdentitySeq"];
			if (!string.IsNullOrEmpty (oracleIdentity)) {
				seq = oracleIdentity;
			}
			else {
				seq = string.Format ("{0}_seq", mapping.TableName);
			}
			return seq;
		}

		public override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)//, bool distinct)
		{
			if (region == null) {
				return base.CreateSelectBaseCommand (mapping, customSelect, query, order, null, state);
			}

			StringBuilder sql = new StringBuilder ();
			//DataParameter [] queryparameters;
			//DataParameter [] orderparameters;
			//string queryString = GetQueryString (query, out queryparameters);
			//string orderString = GetOrderString (order, out orderparameters);

			if (region.Start == 0 && order == null) {
				sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
				if (query != null) {
					sql.Append (GetQueryString (query, false, state));
					sql.AppendFormat (" and ROWNUM<={0}", region.Size);
				}
				else {
					sql.AppendFormat (" where ROWNUM<={0}", region.Size);
				}
			}
			else {
				/*
                SELECT * FROM 
                (
                SELECT A.*, ROWNUM RN 
                FROM (SELECT * FROM TABLE_NAME) A 
                WHERE ROWNUM <= 40
                )
                WHERE RN > 20
                */
				StringBuilder innerSQL = new StringBuilder ();
				innerSQL.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
				if (query != null) {
					sql.Append (GetQueryString (query, false, state));
				}
				if (order != null) {
					sql.Append (GetOrderString (order, false, state));
				}
				string tempRowNumber = CreateCustomFiledName ();
				sql.AppendFormat ("select {4} from (select a.*,ROWNUM {3} from ({0})a where ROWNUM<={2})b where {3}>{1}",
					innerSQL, region.Start, region.Start + region.Size, tempRowNumber, customSelect);
			}
			//DataParameter [] parameters = DataParameter.ConcatDataParameters (dataParameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString ());
			command.TransParamName = true;
			return command;

		}

		//

		public override CommandData CreateSelectJoinTableBaseCommand (string customSelect, List<JoinModel> modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			StringBuilder tables = new StringBuilder ();
			OrderExpression totalOrder = null;
			QueryExpression totalQuery = null;
			foreach (JoinModel model in modelList) {
				if (model.Connect != null) {
					tables.AppendFormat (" {0} ", _joinCollectionPredicateDict [model.Connect.Type]);
				}

				if (model.Query != null) {
					string mqueryString = GetQueryString (model.Query, false, state);
					tables.AppendFormat ("(select * from {0}", CreateDataTableSql (model.Mapping.TableName));
					tables.Append (GetQueryString (model.Query, false, state));
					string aliseName = model.AliasTableName;
					if (aliseName != null) {
						tables.AppendFormat (") {0}", CreateDataTableSql (aliseName));
					}
					else {
						tables.AppendFormat (") {0}", CreateDataTableSql (model.Mapping.TableName));
					}
				}
				else {
					string aliseName = model.AliasTableName;
					if (aliseName != null) {
						tables.AppendFormat ("{0} {1}", CreateDataTableSql (model.Mapping.TableName), CreateDataTableSql (aliseName));
					}
					else {
						tables.Append (CreateDataTableSql (model.Mapping.TableName));
					}
				}
				if (model.Order != null) {
					totalOrder &= model.Order.CreateAliasTableNameOrder (model.AliasTableName);
				}
				if (model.Connect != null && model.Connect.On != null) {
					tables.Append (GetOnString (model.Connect.On, state));
				}
			}
			totalQuery &= query;
			totalOrder &= order;
			StringBuilder sql = new StringBuilder ();


			sql.AppendFormat ("select {0} from {1}", customSelect, tables);
			if (totalQuery != null) {
				sql.AppendFormat (GetQueryString (totalQuery, true, state));
			}
			if (totalOrder != null) {
				sql.AppendFormat (GetOrderString (totalOrder, true, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}


		public override string CreateAvgSql (object fieldName, bool isDistinct)
		{
			string sql = base.CreateAvgSql (fieldName, isDistinct);
			return AddRound (sql);
		}

		public override string CreateConditionAvgSql (string expressionSql, object fieldName, bool isDistinct)
		{
			string sql = base.CreateConditionAvgSql (expressionSql, fieldName, isDistinct);
			return AddRound (sql);
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
		{
			return "dbms_random.value";
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
				return string.Format ("trunc({0})", field);
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
				return string.Format ("to_char({0},'{1}')", field, sqlformat);
			}
		}

		public override string CreateDateSql (object field)
		{
			return string.Format ("trunc({0})", field);
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
			return string.Format ("to_char({0},'{1}')", field, sqlformat);
		}

		public override string CreateTruncateSql (object field)
		{
			return string.Format ("trunc({0})", field);
		}

		public override string CreateCeilingSql (object field)
		{
			return string.Format ("ceil({0})", field);
		}

		public override string CreateLogSql (object field)
		{
			return string.Format ("ln({0})", field);
		}

		public override string CreateLogSql (object field, object value)
		{
			return string.Format ("log({0},{1})", field, value);
		}

		public override string CreateLog10Sql (object field)
		{
			return string.Format ("log({0},10)", field);
		}

		public override string CreateYearSql (object field)
		{
			return string.Format ("extract(year from {0})", field);
		}

		public override string CreateMonthSql (object field)
		{
			return string.Format ("extract(month from {0})", field);
		}

		public override string CreateDaySql (object field)
		{
			return string.Format ("extract(day from {0})", field);
		}

		public override string CreateHourSql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'hh24'))", field);
		}

		public override string CreateMinuteSql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'mi'))", field);
		}

		public override string CreateSecondSql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'ss'))", field);
		}

		public override string CreateWeekSql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'ww'))", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'd'))-1", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("to_number(to_char({0}, 'ddd'))", field);
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
				return string.Format ("instr({0},{1},{2})", field, value, startIndex);
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

		//private string ClearRound (string field)
		//{
		//	Match match = _roundCaptureRegex.Match (field);
		//	while (match.Success) {
		//		field = _roundReplaceRegex.Replace (field, match.Value);
		//		match = _roundCaptureRegex.Match (field);
		//	}
		//	return field;
		//}

		private string AddRound (string field)
		{
			return string.Format ("round({0},{1})", field, _roundScale);
		}

		//public override string CreateDividedSql (string field, object value, bool forward)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateDividedSql (field, value, forward);
		//	return AddRound (field);
		//}

		public override string CreateModSql (object field, object value, bool forward)
		{
			//field = ClearRound (field);
			//if (forward) {
			//	field = string.Format ("mod({0},{1})", field, value);
			//}
			//else {
			//	field = string.Format ("mod({0},{1})", value, field);
			//}
			//return AddRound (field);
			if (forward) {
				return string.Format ("mod({0},{1})", field, value);
			}
			else {
				return string.Format ("mod({0},{1})", value, field);
			}
		}


		public override string CreatePowerSql (object field, object value, bool forward)
		{
			//field = ClearRound (field);
			//if (forward) {
			//	field = string.Format ("power({0},{1})", field, value);
			//}
			//else {
			//	field = string.Format ("power({0},{1})", value, field);
			//}
			//return AddRound (field);
			if (forward) {
				return string.Format ("power({0},{1})", field, value);
			}
			else {
				return string.Format ("power({0},{1})", value, field);
			}
		}


		public override string CreateModSql (object left, object right)
		{
			return string.Format ("mod({0},{1})", left, right);
		}

		public override string CreatePowerSql (object left, object right)
		{
			return string.Format ("power({0},{1})", left, right);
		}

		//public override string CreateAbsSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateAbsSql (field);
		//	return AddRound (field);
		//}

		//public override string CreateLogSql (object field)
		//{
		//	//field = ClearRound (field);
		//	//field = string.Format ("ln({0})", field);
		//	//return AddRound (field);
		//	return string.Format ("ln({0})", field);
		//}

		//public override string CreateExpSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateExpSql (field);
		//	return AddRound (field);
		//}

		//public override string CreateSinSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateSinSql (field);
		//	return AddRound (field);
		//}

		//public override string CreateCosSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateCosSql (field);
		//	return AddRound (field);
		//}

		//public override string CreateTanSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateTanSql (field);
		//	return AddRound (field);
		//}

		//public override string CreateAtanSql (object field)
		//{
		//	field = ClearRound (field);
		//	field = base.CreateAtanSql (field);
		//	return AddRound (field);
		//}

		public override string CreateDataBaseTimeSql ()
		{
			return "sysdate";
		}

		public override string CreateParamName (string name)
		{
			if (name == null)
				throw new ArgumentNullException (nameof (name));
			if (!name.StartsWith (":", StringComparison.Ordinal)) {
				return ":" + name;
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
	}
}
