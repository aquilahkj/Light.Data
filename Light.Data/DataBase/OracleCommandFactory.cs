﻿using System;
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

		public override CommandData CreateTruncateCommand (DataTableEntityMapping mapping)
		{
			CommandData data = base.CreateTruncateCommand (mapping);
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

			string[] insertList = new string[paramList.Count];
			string[] valuesList = new string[paramList.Count];
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

		static Dictionary<DataTableEntityMapping,bool> IdentityDict = new Dictionary<DataTableEntityMapping, bool> ();

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

		public override CommandData[] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException ("entitys");
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
				string[] valueList = new string[entityParams.Count];
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

		protected override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			if (region == null) {
				return base.CreateSelectBaseCommand (mapping, customSelect, query, order, null);
			}

			StringBuilder sql = new StringBuilder ();
			List<DataParameter> parameters = new List<DataParameter> ();
			DataParameter[] queryparameters;
			DataParameter[] orderparameters;
			string queryString = GetQueryString (query, out queryparameters);
			string orderString = GetOrderString (order, out orderparameters);

			if (region.Start == 0 && orderString == null) {
				sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
				if (!string.IsNullOrEmpty (queryString)) {
					sql.AppendFormat (" {0}", queryString);
					if (queryparameters != null) {
						parameters.AddRange (queryparameters);
					}
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
					if (queryparameters != null) {
						parameters.AddRange (queryparameters);
					}
				}
				if (!string.IsNullOrEmpty (orderString)) {
					innerSQL.AppendFormat (" {0}", orderString);
					if (orderparameters != null) {
						parameters.AddRange (orderparameters);
					}
				}
				string tempRowNumber = CreateCustomFiledName ();
				sql.AppendFormat ("select {4} from (select a.*,ROWNUM {3} from ({0})a where ROWNUM<={2})b where {3}>{1}",
					innerSQL, region.Start, region.Start + region.Size, tempRowNumber, customSelect);
			}
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
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

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
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
			start++;
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

		public override string CreateDividedSql (string field, object value, bool forward)
		{
			field = ClearRound (field);
			field = base.CreateDividedSql (field, value, forward);
			return AddRound (field);
		}

		public override string CreateModSql (string field, object value, bool forward)
		{
			field = ClearRound (field);
			if (forward) {
				field = string.Format ("mod({0},{1})", field, value);
			}
			else {
				field = string.Format ("mod({0},{1})", value, field);
			}
			return AddRound (field);
		}

		public override string CreatePowerSql (string field, object value, bool forward)
		{
			field = ClearRound (field);
			if (forward) {
				field = string.Format ("power({0},{1})", field, value);
			}
			else {
				field = string.Format ("power({0},{1})", value, field);
			}
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

		public override string CreateDataBaseTimeSql ()
		{
			return "sysdate";
		}

		public override string CreateParamName (string name)
		{
			if (name == null)
				throw new ArgumentNullException ("name");
			if (!name.StartsWith (":")) {
				return ":" + name;
			}
			else {
				return name;
			}
		}
	}
}
