using System;
using System.Text;
using System.Collections.Generic;

namespace Light.Data.SQLiteAdapter
{
	class SQLiteCommandFactory:CommandFactory
	{
		public SQLiteCommandFactory ()
		{
			_canInnerPage = true;
		}

		public override CommandData CreateTruncateCommand (DataTableEntityMapping mapping)
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


		protected override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			CommandData command = base.CreateSelectBaseCommand (mapping, customSelect, query, order, region);
			if (region != null) {
				if (region.Start == 0) {
					command.CommandText = string.Format ("{0} limit {1}", command.CommandText, region.Size);
				}
				else {
					command.CommandText = string.Format ("{0} limit {2} offset {1}", command.CommandText, region.Start, region.Size);
				}
			}
			return command;
		}

		public override string CreateDividedSql (string field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("(cast({0} as real)/{1})", field, value);
			}
			else {
				return string.Format ("({0}/cast({1} as real))", value, field);
			}
		}


		public override string CreateCollectionParamsQuerySql (string fieldName, QueryCollectionPredicate predicate, List<DataParameter> dataParameters)
		{
			if (predicate == QueryCollectionPredicate.In || predicate == QueryCollectionPredicate.NotIn) {
				return base.CreateCollectionParamsQuerySql (fieldName, predicate, dataParameters);
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

		public override string CreateYearSql (string field)
		{
			return string.Format ("strftime('%Y',{0})", field);
		}

		public override string CreateMonthSql (string field)
		{
			return string.Format ("strftime('%m',{0})", field);
		}

		public override string CreateDaySql (string field)
		{
			return string.Format ("strftime('%d',{0})", field);
		}

		public override string CreateHourSql (string field)
		{
			return string.Format ("strftime('%H',{0})", field);
		}

		public override string CreateMinuteSql (string field)
		{
			return string.Format ("strftime('%M',{0})", field);
		}

		public override string CreateSecondSql (string field)
		{
			return string.Format ("strftime('%S',{0})", field);
		}

		public override string CreateWeekSql (string field)
		{
			return string.Format ("strftime('%W',{0})", field);
		}

		public override string CreateWeekDaySql (string field)
		{
			return string.Format ("strftime('%w',{0})", field);
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

		public override string CreateDataBaseTimeSql ()
		{
			return "now";
		}

		public override string CreateParamName (string name)
		{
			if (!name.StartsWith ("@")) {
				return "@" + name;
			}
			else {
				return name;
			}
		}

		public override string GetHavingString (AggregateHavingExpression having, out DataParameter[] parameters, List<AggregateFunctionInfo> functions)
		{
			string havingString = null;
			parameters = null;
			if (having != null) {
				havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate(object obj) {
					return null;
				})));
			}
			return havingString;
		}

		public override string CreatePowerSql (string field, object value, bool forward)
		{
			throw new NotSupportedException ();
		}

		public override string CreateLogSql (string field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateExpSql (string field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateSinSql (string field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateCosSql (string field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateTanSql (string field)
		{
			throw new NotSupportedException ();
		}

		public override string CreateAtanSql (string field)
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

