using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data.OracleAdapter
{
	class OracleCommandFactory : CommandFactory
	{
		byte _roundScale = 8;

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

		public override string CreateAliasTableSql (string field, string alias)
		{
			return string.Format ("{0} {1}", field, CreateDataFieldSql (alias));
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
		}

		public override CommandData CreateInsertCommand (DataTableEntityMapping mapping, object entity, CreateSqlState state)
		{
			bool identityAuto = CheckIndentityAuto (mapping);
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException ("");
			}
			string [] insertList = new string [insertLen];
			string [] valuesList = new string [insertLen];
			for (int i = 0; i < insertLen; i++) {
				DataFieldMapping field = fields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				insertList [i] = CreateDataFieldSql (field.Name);
				valuesList [i] = state.AddDataParameter (value, field.DBType, ParameterDirection.Input);
			}
			string insert = string.Join (",", insertList);
			string values = string.Join (",", valuesList);
			string sql;
			if (!identityAuto && mapping.IdentityField != null) {
				sql = string.Format ("insert into {0}({3},{1})values({4}.nextval,{2})", CreateDataTableSql (mapping.TableName), insert, values, CreateDataFieldSql (mapping.IdentityField.Name), GetIndentitySeq (mapping));
			}
			else {
				sql = string.Format ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			}
			CommandData command = new CommandData (sql);
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

		public override Tuple<CommandData, CreateSqlState> [] CreateBatchInsertCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			bool identityAuto = CheckIndentityAuto (mapping);
			string identityString = null;
			if (identityAuto) {
				identityString = GetIndentitySeq (mapping);
			}
			int totalCount = entitys.Count;
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException (RE.NoFieldInsert);
			}
			string [] insertList = new string [insertLen];
			for (int i = 0; i < insertLen; i++) {
				DataFieldMapping field = fields [i];
				insertList [i] = CreateDataFieldSql (field.Name);
			}
			string insert = string.Join (",", insertList);

			string insertSql;
			if (!identityAuto && mapping.IdentityField != null) {
				insertSql = string.Format ("insert into {0}({2},{1})", CreateDataTableSql (mapping.TableName), insert, CreateDataFieldSql (mapping.IdentityField.Name));
			}
			else {
				insertSql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);
			}

			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			totalSql.Append ("begin ");
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

				if (!identityAuto && mapping.IdentityField != null) {
					totalSql.AppendFormat ("{0}values({2}.nextval,{1});", insertSql, values, identityString);
				}
				else {
					totalSql.AppendFormat ("{0}values({1});", insertSql, values);
				}
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					totalSql.Append (" end;");
					CommandData command = new CommandData (totalSql.ToString ());
					command.ReturnRowCount = false;
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
					totalSql.Append ("begin ");
				}
			}
			return list.ToArray ();
		}
     
		public override Tuple<CommandData, CreateSqlState> [] CreateBatchUpdateCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			if (mapping.NoPrimaryKeyFields.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;

			int totalCount = entitys.Count;
			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			totalSql.Append ("begin ");
			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				IList<DataFieldMapping> columnFields;
				DataTableEntity tableEntity = entity as DataTableEntity;
				if (tableEntity != null) {
					string [] updatefieldNames = tableEntity.GetUpdateFields ();
					if (updatefieldNames != null && updatefieldNames.Length > 0) {
						List<DataFieldMapping> updateFields = new List<DataFieldMapping> ();
						foreach (string name in updatefieldNames) {
							DataFieldMapping fm = mapping.FindDataEntityField (name);
							if (fm == null) {
								continue;
							}
							PrimitiveFieldMapping pfm = fm as PrimitiveFieldMapping;
							if (pfm != null && pfm.IsPrimaryKey) {
								continue;
							}
							if (!updateFields.Contains (fm)) {
								updateFields.Add (fm);
							}
						}
						columnFields = updateFields;
					}
					else {
						columnFields = mapping.NoPrimaryKeyFields;
					}
				}
				else {
					columnFields = mapping.NoPrimaryKeyFields;
				}

				int updateLen = columnFields.Count;
				string [] updateList = new string [updateLen];
				string [] whereList = new string [keyLen];
				for (int i = 0; i < updateLen; i++) {
					DataFieldMapping field = columnFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					updateList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				for (int i = 0; i < keyLen; i++) {
					DataFieldMapping field = keyFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				string update = string.Join (",", updateList);
				string where = string.Join (" and ", whereList);
				totalSql.AppendFormat ("update {0} set {1} where {2};", CreateDataTableSql (mapping.TableName), update, where);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					totalSql.Append (" end;");
					CommandData command = new CommandData (totalSql.ToString ());
					command.ReturnRowCount = false;
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
					totalSql.Append ("begin ");
				}
			}
			return list.ToArray ();
		}

		public override Tuple<CommandData, CreateSqlState> [] CreateBatchDeleteCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			if (mapping.NoPrimaryKeyFields.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;

			int totalCount = entitys.Count;
			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			totalSql.Append ("begin ");
			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				string [] whereList = new string [keyLen];
				for (int i = 0; i < keyLen; i++) {
					DataFieldMapping field = keyFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				string where = string.Join (" and ", whereList);
				totalSql.AppendFormat ("delete from {0} where {1};", CreateDataTableSql (mapping.TableName), where);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					totalSql.Append (" end;");
					CommandData command = new CommandData (totalSql.ToString ());
					command.ReturnRowCount = false;
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
					totalSql.Append ("begin ");
				}
			}
			return list.ToArray ();
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
				seq = string.Format ("{0}_seq", mapping.TableName);//mapping.TableName + "_Sequence";
			}
			return seq;
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
				return string.Format ("substr({0},{1}+1)", field, start);
			}
			else {
				return string.Format ("substr({0},{1}+1,{2})", field, start, size);
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

		private string AddRound (string field)
		{
			return string.Format ("round({0},{1})", field, _roundScale);
		}

		public override string CreateModSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("mod({0},{1})", field, value);
			}
			else {
				return string.Format ("mod({0},{1})", value, field);
			}
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


		public override string CreateModSql (object left, object right)
		{
			return string.Format ("mod({0},{1})", left, right);
		}

		public override string CreatePowerSql (object left, object right)
		{
			return string.Format ("power({0},{1})", left, right);
		}

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
