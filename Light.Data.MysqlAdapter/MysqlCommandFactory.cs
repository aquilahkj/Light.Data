using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data.MysqlAdapter
{
	class MysqlCommandFactory : CommandFactory
	{
		public MysqlCommandFactory ()
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
		}

		protected override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			CommandData command = base.CreateSelectBaseCommand (mapping, customSelect, query, order, region);
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


		public override CommandData[] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException ("entitys");
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}

			int totalCount = entitys.Length;
			List<string> insertList = new List<string> ();
			foreach (DataFieldMapping field in mapping.NoIdentityFields) {
				insertList.Add (CreateDataFieldSql (field.Name));
			}

			string insert = string.Join (",", insertList);
			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;
			StringBuilder values = new StringBuilder ();
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
				values.AppendFormat ("({0})", value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount || totalCreateCount == totalCount - 1) {
					CommandData command = new CommandData (string.Format ("{0}values{1};", insertsql, values), dataParams);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					dataParams = new List<DataParameter> ();
					createCount = 0;
					paramIndex = 0;
					values = new StringBuilder ();
				}
				else {
					values.Append (",");
				}
			}
			return commands.ToArray ();
		}

		public override string CreateCollectionParamsQuerySql (string fieldName, QueryCollectionPredicate predicate, List<DataParameter> dataParameters)
		{
			if (predicate == QueryCollectionPredicate.In || predicate == QueryCollectionPredicate.NotIn) {
				return base.CreateCollectionParamsQuerySql (fieldName, predicate, dataParameters);
			}
			string op = GetQueryCollectionPredicate (predicate);
			if (dataParameters.Count == 0) {
				throw new LightDataException (RE.EnumerableLengthNotAllowIsZero);
			}
			int i = 0;
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {1} (", fieldName, op);
			foreach (DataParameter dataParameter in dataParameters) {
				if (i > 0)
					sb.Append (" union all ");
				sb.AppendFormat ("select {0}", dataParameter.ParameterName);
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
			return string.Format ("week({0},7)", field);
		}

		public override string CreateWeekDaySql (string field)
		{
			return string.Format ("dayofweek({0})-1", field);
		}

		public override string CreateLengthSql (string field)
		{
			return string.Format ("length({0})", field);
		}

		public override string CreateSubStringSql (string field, int start, int size)
		{
			start++;
			if (size == 0) {
				return string.Format ("substring({0},{1})", field, start);
			}
			else {
				return string.Format ("substring({0},{1},{2})", field, start, size);
			}
		}

		public override string CreatePowerSql (string field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("power({0},{1})", field, value);
			}
			else {
				return string.Format ("power({0},{1})", value, field);
			}
		}

		public override string CreateDataBaseTimeSql ()
		{
			return "now()";
		}
	}
}

