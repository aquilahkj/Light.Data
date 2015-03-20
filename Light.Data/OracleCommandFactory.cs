using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Light.Data.Mappings;

namespace Light.Data
{
	class OracleCommandFactory : CommandFactory
	{
		byte _roundScale = 8;

		Regex _roundCaptureRegex = null;

		Regex _roundReplaceRegex = null;

		public OracleCommandFactory (Database database)
			: base (database)
		{
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

		public override string CreateDataFieldSql (string fieldName)
		{
			return fieldName;
		}

		public override string CreateDataTableSql (string tableName)
		{
			return tableName;
		}

		public override IDbCommand CreateInsertCommand (object entity)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (entity.GetType ());
			bool identityIntegrated = CheckIndentityIntegrated (mapping);

			List<FieldMapping> fields = new List<FieldMapping> ();
			fields.AddRange (mapping.GetFieldMappings ());
			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			List<DataParameter> paramList = GetDataParameters (fields, entity);
			StringBuilder insert = new StringBuilder ();
			StringBuilder values = new StringBuilder ();

			IDataParameter[] dataParameters = new IDataParameter[paramList.Count];
			int count = 0;
			foreach (DataParameter dataParameter in paramList) {
				IDataParameter param = _database.CreateParameter ("P" + count, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				insert.AppendFormat ("{0},", CreateDataFieldSql (dataParameter.ParameterName));
				values.AppendFormat ("{0},", param.ParameterName);
				dataParameters [count] = param;
				count++;
			}
			insert.Remove (insert.Length - 1, 1);
			values.Remove (values.Length - 1, 1);
			StringBuilder sql = new StringBuilder ();
			if (identityIntegrated) {
				sql.AppendFormat ("insert into {0}({3},{1})values({4}.nextval,{2})", CreateDataTableSql (mapping.TableName), insert, values, CreateDataFieldSql (mapping.IdentityField.Name), GetIndentitySeq (mapping));
			}
			else {
				sql.AppendFormat ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			}
			IDbCommand command = _database.CreateCommand (sql.ToString ());
			command.CommandType = CommandType.Text;
			foreach (IDataParameter param in dataParameters) {
				command.Parameters.Add (param);
			}
			return command;
		}

		private static bool CheckIndentityIntegrated (DataTableEntityMapping mapping)
		{
			bool identityIntegrated = false;

			if (mapping.IdentityField != null) {
				string oracleIdentityAuto = mapping.ExtentParams ["OracleIdentityAuto"];
				if (oracleIdentityAuto != null && oracleIdentityAuto.ToLower () == "true") {
					identityIntegrated = false;
				}
				else {
					identityIntegrated = true;
				}
			}
			return identityIntegrated;
		}

		public override IDbCommand[] CreateBulkInsertCommand (Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException ("entitys");
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			object tmpEntity = entitys.GetValue (0);
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (tmpEntity.GetType ());
			bool identityIntegrated = CheckIndentityIntegrated (mapping);
			List<FieldMapping> fields = new List<FieldMapping> ();
			int totalCount = entitys.Length;
			fields.AddRange (mapping.GetFieldMappings ());
			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			StringBuilder totalSql = new StringBuilder ();
			StringBuilder insert = new StringBuilder ();

			List<DataParameter> paramList = GetDataParameters (fields, tmpEntity);
			foreach (DataParameter dataParameter in paramList) {
				insert.AppendFormat ("{0},", CreateDataFieldSql (dataParameter.ParameterName));
			}
			insert.Remove (insert.Length - 1, 1);
			string insertsql;
			if (identityIntegrated) {
				insertsql = string.Format ("insert into {0}({2},{1})", CreateDataTableSql (mapping.TableName), insert, CreateDataFieldSql (mapping.IdentityField.Name));
			}
			else {
				insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);
			}

			int createCount = 0;
			int totalCreateCount = 0;
			string identityString = null;
			if (identityIntegrated) {
				identityString = GetIndentitySeq (mapping);
			}
			IDbCommand command = _database.CreateCommand ();
			int paramCount = 0;
			List<IDbCommand> commands = new List<IDbCommand> ();
			foreach (object entity in entitys) {
				paramList = GetDataParameters (fields, entity);
				StringBuilder value = new StringBuilder ();
				foreach (DataParameter dataParameter in paramList) {
					IDataParameter param = _database.CreateParameter ("P" + paramCount, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
					value.AppendFormat ("{0},", param.ParameterName);
					command.Parameters.Add (param);
					paramCount++;
				}
				value.Remove (value.Length - 1, 1);
				if (identityIntegrated) {
					totalSql.AppendFormat ("{0}values({2}.nextval,{1});", insertsql, value, identityString);
				}
				else {
					totalSql.AppendFormat ("{0}values({1});", insertsql, value);
				}
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					command.CommandText = string.Format ("begin {0} end;", totalSql);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					command = _database.CreateCommand ();
					createCount = 0;
					paramCount = 0;
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

		private string GetIndentitySeq (DataTableEntityMapping mapping)
		{
			string seq = null;
			string oracleIdentity = mapping.ExtentParams ["OracleIdentitySeq"];
			if (!string.IsNullOrEmpty (oracleIdentity)) {
				seq = oracleIdentity;
			}
			else {
				seq = string.Format ("{0}_{1}", mapping.TableName, "Sequence");//mapping.TableName + "_Sequence";
			}
			return seq;
		}

		protected override IDbCommand CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			if (region == null) {
				return base.CreateSelectBaseCommand (mapping, customSelect, query, order, null);
			}

			StringBuilder sql = new StringBuilder ();
			DataParameter[] parameters;
			string queryString = GetQueryString (mapping, query, out parameters);
			string orderString = GetOrderString (mapping, order);

			if (region.Start == 0 && orderString == null) {
				sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
				if (!string.IsNullOrEmpty (queryString)) {
					sql.AppendFormat (" {0}", queryString);
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
				}
				if (!string.IsNullOrEmpty (orderString)) {
					innerSQL.AppendFormat (" {0}", orderString);
				}
				string tempRowNumber = CreateCustomFiledName ();
				sql.AppendFormat ("select {4} from (select a.*,ROWNUM {3} from ({0})a where ROWNUM<={2})b where {3}>{1}",
					innerSQL, region.Start, region.Start + region.Size, tempRowNumber, customSelect);
			}
			IDbCommand command = BuildCommand (sql.ToString (), parameters);
			return command;
		}

		public override string CreateAvgSql (string fieldName, bool isDistinct)
		{
			string sql = base.CreateAvgSql (fieldName, isDistinct);
			return AddRound (sql);
		}

		public override string CreateConditionAvgSql (string expressionSql, string fieldName, bool isDistinct)
		{
			string sql = base.CreateConditionAvgSql (expressionSql, fieldName, isDistinct);
			return AddRound (sql);
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping)
		{
			return "dbms_random.value";
		}

		public override string CreateMatchSql (string field, bool left, bool right)
		{
			StringBuilder sb = new StringBuilder ();
			if (left) {
				sb.AppendFormat ("'{0}'||", _wildcards);
			}
			sb.Append (field);
			if (right) {
				sb.AppendFormat ("||'{0}'", _wildcards);
			}
			return sb.ToString ();
		}

		public override string CreateDateSql (string field, string format)
		{
			if (string.IsNullOrEmpty (format)) {
				return string.Format ("trunc({0})", field);
			}
			else {
				string format1 = format.ToUpper ();
				string sqlformat = null;
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

		public override string CreateYearSql (string field)
		{
			return string.Format ("extract(year from {0})", field);
		}

		public override string CreateMonthSql (string field)
		{
			return string.Format ("extract(month from {0})", field);
		}

		public override string CreateDaySql (string field)
		{
			return string.Format ("extract(day from {0})", field);
		}

		public override string CreateHourSql (string field)
		{
			return string.Format ("to_number(to_char({0}, 'hh24'))", field);
		}

		public override string CreateMinuteSql (string field)
		{
			return string.Format ("to_number(to_char({0}, 'mi'))", field);
		}

		public override string CreateSecondSql (string field)
		{
			return string.Format ("to_number(to_char({0}, 'ss'))", field);
		}

		public override string CreateWeekSql (string field)
		{
			return string.Format ("to_number(to_char({0}, 'ww'))", field);
		}

		public override string CreateWeekDaySql (string field)
		{
			return string.Format ("to_number(to_char({0}, 'd'))", field);
		}

		public override string CreateLengthSql (string field)
		{
			return string.Format ("length({0})", field);
		}

		public override string CreateSubStringSql (string field, int start, int size)
		{
			if (size == 0) {
				return string.Format ("substr({0},{1})", field, start);
			}
			else {
				return string.Format ("substr({0},{1},{2})", field, start, size);
			}
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

		public override string CreateDividedSql (string field, object value)
		{
			field = ClearRound (field);
			field = base.CreateDividedSql (field, value);
			return AddRound (field);
		}

		public override string CreateModSql (string field, object value)
		{
			field = ClearRound (field);
			field = string.Format ("mod({0},{1})", field, value);
			return AddRound (field);
		}

		public override string CreatePowerSql (string field, object value)
		{
			field = ClearRound (field);
			field = string.Format ("power({0},{1})", field, value);
			return AddRound (field);
		}


		public override string CreateAbsSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateAbsSql (field);
			return AddRound (field);
		}

		public override string CreateLogSql (string field)
		{
			field = ClearRound (field);
			field = string.Format ("ln({0})", field);
			return AddRound (field);
		}

		public override string CreateExpSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateExpSql (field);
			return AddRound (field);
		}

		public override string CreateSinSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateSinSql (field);
			return AddRound (field);
		}

		public override string CreateCosSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateCosSql (field);
			return AddRound (field);
		}

		public override string CreateTanSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateTanSql (field);
			return AddRound (field);
		}

		public override string CreateAtanSql (string field)
		{
			field = ClearRound (field);
			field = base.CreateAtanSql (field);
			return AddRound (field);
		}
	}
}
