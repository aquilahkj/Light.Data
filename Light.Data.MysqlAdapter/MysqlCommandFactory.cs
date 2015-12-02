using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data.MysqlAdapter
{
	class MysqlCommandFactory : CommandFactory
	{
		public MysqlCommandFactory (Database database)
			: base (database)
		{
			_canInnerPage = true;
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("`{0}`", fieldName);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("`{0}`", tableName);
			;
		}

		protected override IDbCommand CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			IDbCommand command = base.CreateSelectBaseCommand (mapping, customSelect, query, order, region);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {1},{2}", command.CommandText, region.Start, region.Size);
				}
			}
			return command;
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
			List<FieldMapping> fields = new List<FieldMapping> ();
			int totalCount = entitys.Length;
			fields.AddRange (mapping.GetFieldMappings ());
			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			StringBuilder values = new StringBuilder ();

			List<DataParameter> paramList = GetDataParameters (fields, tmpEntity);
			string[] insertList = new string[paramList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in paramList) {
				insertList [index] = CreateDataFieldSql (dataParameter.ParameterName);
				index++;
			}
			string insert = string.Join (",", insertList);
			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;

			IDbCommand command = _database.CreateCommand ();
			int paramCount = 0;
			List<IDbCommand> commands = new List<IDbCommand> ();
			foreach (object entity in entitys) {
				paramList = GetDataParameters (fields, entity);
				string[] valueList = new string[paramList.Count];
				int vindex = 0;
				foreach (DataParameter dataParameter in paramList) {
					IDataParameter param = _database.CreateParameter ("P" + paramCount, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
					command.Parameters.Add (param);
					valueList [vindex] = param.ParameterName;
					paramCount++;
					vindex++;
				}
				string value = string.Join (",", valueList);
				values.AppendFormat ("({0})", value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					values.Append (";");
					command.CommandText = string.Format ("{0}values{1}", insertsql, values);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					command = _database.CreateCommand ();
					createCount = 0;
					paramCount = 0;
					values = new StringBuilder ();
				}
				else {
					values.Append (",");
				}
			}
			return commands.ToArray ();
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, bool fullFieldName)
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

		public override string CreateMatchSql (string field, bool left, bool right)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("concat(");
			if (left) {
				sb.AppendFormat ("'{0}',", _wildcards);
			}
			sb.Append (field);
			if (right) {
				sb.AppendFormat (",'{0}'", _wildcards);
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public override string CreateDateSql (string field, string format)
		{
			if (string.IsNullOrEmpty (format)) {
				return string.Format ("date({0})", field);
			}
			else {
				string format1 = format.ToUpper ();
				string sqlformat = null;
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

		public override string CreateYearSql (string field)
		{
			return string.Format ("year({0})", field);
		}

		public override string CreateMonthSql (string field)
		{
			return string.Format ("month({0})", field);
		}

		public override string CreateDaySql (string field)
		{
			return string.Format ("day({0})", field);
		}

		public override string CreateHourSql (string field)
		{
			return string.Format ("hour({0})", field);
		}

		public override string CreateMinuteSql (string field)
		{
			return string.Format ("minute({0})", field);
		}

		public override string CreateSecondSql (string field)
		{
			return string.Format ("second({0})", field);
		}

		public override string CreateWeekSql (string field)
		{
			return string.Format ("week({0})", field);
		}

		public override string CreateWeekDaySql (string field)
		{
			return string.Format ("weekday({0})", field);
		}

		public override string CreateLengthSql (string field)
		{
			return string.Format ("length({0})", field);
		}

		public override string CreateSubStringSql (string field, int start, int size)
		{
			if (size == 0) {
				return string.Format ("substring({0},{1})", field, start);
			}
			else {
				return string.Format ("substring({0},{1},{2})", field, start, size);
			}
		}

		public override string CreatePowerSql (string field, object value)
		{
			return string.Format ("power({0},{1})", field, value);
		}
	}
}

