using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class OracleCommandFactory : CommandFactory
	{
		byte _roundScale = 8;

		Regex _roundCaptureRegex;

		Regex _roundReplaceRegex;

		bool _identityAuto;

		public void SetIdentityAuto (bool identityAuto)
		{
			_identityAuto = identityAuto;
		}

		public OracleCommandFactory ()
		{
			_identityAuto = true;
			_canInnerPage = true;
			LoadRoundRegex ();
		}

		public void SetRoundScale (byte scale)
		{
			_roundScale = scale;
			LoadRoundRegex ();
		}

		private void LoadRoundRegex ()
		{
			_roundCaptureRegex = new Regex (string.Format (@"(?<=round\().*(?=,{0}\))", _roundScale), RegexOptions.Compiled);
			_roundReplaceRegex = new Regex (string.Format (@"round\(.*,{0}\)", _roundScale), RegexOptions.Compiled);
		}

		public override CommandData CreateTruncateTableCommand (DataTableEntityMapping mapping)
		{
			CommandData data = base.CreateTruncateTableCommand (mapping);
			if (mapping.IdentityField != null) {
				string restartSeq = string.Format ("alter sequence \"{0}\" increment by 1;", GetIndentitySeq (mapping));
				data.CommandText += restartSeq;
			}
			return data;
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return fieldName;
		}

		public override string CreateDataTableSql (string tableName)
		{
			return tableName;
		}

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
			if (!identityAuto) {
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
					IdentityDict [mapping] = ret;
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
			if (!identityAuto) {
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
				if (identityAuto) {
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
				return string.Format ("select {0}.currval from dual", seq);
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
				seq = string.Format ("{0}_seq", mapping.TableName);//mapping.TableName + "_Sequence";
			}
			return seq;
		}

		protected override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, DataParameter [] dataParameters, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			if (region == null) {
				return base.CreateSelectBaseCommand (mapping, customSelect, dataParameters, query, order, null);
			}

			StringBuilder sql = new StringBuilder ();
			//List<DataParameter> parameters = new List<DataParameter> ();
			DataParameter [] queryparameters;
			DataParameter [] orderparameters;
			string queryString = GetQueryString (query, out queryparameters);
			string orderString = GetOrderString (order, out orderparameters);

			if (region.Start == 0 && orderString == null) {
				sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
				if (!string.IsNullOrEmpty (queryString)) {
					sql.AppendFormat (" {0}", queryString);
					//if (queryparameters != null) {
					//	parameters.AddRange (queryparameters);
					//}
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
				if (!string.IsNullOrEmpty (queryString)) {
					innerSQL.AppendFormat (" {0}", queryString);
					//if (queryparameters != null) {
					//	parameters.AddRange (queryparameters);
					//}
				}
				if (!string.IsNullOrEmpty (orderString)) {
					innerSQL.AppendFormat (" {0}", orderString);
					//if (orderparameters != null) {
					//	parameters.AddRange (orderparameters);
					//}
				}
				string tempRowNumber = CreateCustomFiledName ();
				sql.AppendFormat ("select {4} from (select a.*,ROWNUM {3} from ({0})a where ROWNUM<={2})b where {3}>{1}",
					innerSQL, region.Start, region.Start + region.Size, tempRowNumber, customSelect);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (dataParameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
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

		public override string CreateLambdaConcatSql (params object [] values)
		{
			string value1 = string.Join ("||", values);
			string sql = string.Format ("({0})", value1);
			return sql;
		}

		public override string CreateConcatSql (object field, object value, bool forward)
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
			return string.Format ("to_number(to_char({0}, 'd'))", field);
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

		private string ClearRound (string field)
		{
			Match match = _roundCaptureRegex.Match (field);
			while (match.Success) {
				field = _roundReplaceRegex.Replace (field, match.Value);
				match = _roundCaptureRegex.Match (field);
			}
			return field;
		}

		private string AddRound (string field)
		{
			return string.Format ("round({0},{1})", field, _roundScale);
		}

		//public override string CreateDividedSql (object field, object value, bool forward)
		//{
		//	field = ClearRound (field.ToString ());
		//	field = base.CreateDividedSql (field, value, forward);
		//	return AddRound (field.ToString ());
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

		public override string CreateLogSql (object field)
		{
			//field = ClearRound (field);
			//field = string.Format ("ln({0})", field);
			//return AddRound (field);
			return string.Format ("ln({0})", field);
		}

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
	}
}
