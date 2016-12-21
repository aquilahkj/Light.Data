using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace Light.Data.PostgreAdapter
{
	class PostgreCommandFactory : CommandFactory
	{

		DateTimeFormater dateTimeFormater = new DateTimeFormater ();

		readonly string defaultDateTime = "YYYY-MM-DD HH:MI:SS";


		public PostgreCommandFactory ()
		{
			//_canInnerPage = true;
			_strictMode = true;
			dateTimeFormater.YearFormat = "YYYY";
			dateTimeFormater.MonthFormat = "MM";
			dateTimeFormater.DayFormat = "DD";
			dateTimeFormater.HourFormat = "HH";
			dateTimeFormater.MinuteFormat = "MI";
			dateTimeFormater.SecondFormat = "SS";
		}

		bool _strictMode;

		public void SetStrictMode (bool strictMode)
		{
			_strictMode = strictMode;
		}

		public override CommandData CreateTruncateTableCommand (DataTableEntityMapping mapping)
		{
			CommandData data = base.CreateTruncateTableCommand (mapping);
			if (mapping.IdentityField != null) {
				string restartSeq = string.Format ("alter sequence \"{0}\" restart;", GetIndentitySeq (mapping));
				data.CommandText += restartSeq;
			}
			return data;
		}

		public override string CreateBooleanQuerySql (object fieldName, bool isTrue)
		{
			return string.Format ("{0}={1}", fieldName, isTrue ? "true" : "false");
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

		public override string CreateDividedSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}::float/{1})", field, value);
			}
			else {
				return string.Format ("({0}/{1}::float)", value, field);
			}
		}


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

		public override Tuple<CommandData, CreateSqlState> [] CreateBatchInsertCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			int totalCount = entitys.Count;
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

		public override string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, IEnumerable<object> list)
		{
			if (predicate == QueryCollectionPredicate.In || predicate == QueryCollectionPredicate.NotIn) {
				return base.CreateCollectionParamsQuerySql (fieldName, predicate, list);
			}
			string op = GetQueryCollectionPredicate (predicate);

			int i = 0;
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {1} (", fieldName, op);
			foreach (object item in list) {
				if (i > 0)
					sb.Append (" union all ");
				sb.AppendFormat ("select {0}", item);
				i++;
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
		{
			return "random()";
		}

		protected override string CreateIdentitySql (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField != null) {
				return string.Format ("select currval('\"{0}\"');", GetIndentitySeq (mapping));
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
			string postgreIdentity = mapping.ExtentParams ["PostgreIdentitySeq"];
			if (!string.IsNullOrEmpty (postgreIdentity)) {
				seq = postgreIdentity;
			}
			else {
				seq = string.Format ("{0}_{1}_seq", mapping.TableName, mapping.IdentityField.Name);
			}
			return seq;
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

		public override string CreateBooleanQuerySql (object field, bool isTrue, bool isEqual, bool isReverse)
		{
			if (!isReverse) {
				return string.Format ("{0}{2}{1}", field, isTrue ? "true" : "false", isEqual ? "=" : "!=");
			}
			else {
				return string.Format ("{1}{2}{0}", field, isTrue ? "true" : "false", isEqual ? "=" : "!=");
			}
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
					sqlformat = "YYYYMMDD";
					break;
				case "YM":
					sqlformat = "YYYYMM";
					break;
				case "Y-M-D":
					sqlformat = "YYYY-MM-DD";
					break;
				case "Y-M":
					sqlformat = "YYYY-MM";
					break;
				case "M-D-Y":
					sqlformat = "MM-DD-YYYY";
					break;
				case "D-M-Y":
					sqlformat = "DD-MM-YYYY";
					break;
				case "Y/M/D":
					sqlformat = "YYYY/MM/DD";
					break;
				case "Y/M":
					sqlformat = "YYYY/MM";
					break;
				case "M/D/Y":
					sqlformat = "MM/DD/YYYY";
					break;
				case "D/M/Y":
					sqlformat = "DD/MM/YYYY";
					break;
				default:
					throw new LightDataException (string.Format (RE.UnsupportDateFormat, format));
				}
				return string.Format ("to_char({0},'{1}')", field, sqlformat);
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
			return string.Format ("to_char({0},'{1}')", field, sqlformat);
		}

		public override string CreateTruncateSql (object field)
		{
			return string.Format ("trunc({0})", field);
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
			return string.Format ("extract(year from {0})::int4", field);
		}

		public override string CreateMonthSql (object field)
		{
			return string.Format ("extract(month from {0})::int4", field);
		}

		public override string CreateDaySql (object field)
		{
			return string.Format ("extract(day from {0})::int4", field);
		}

		public override string CreateHourSql (object field)
		{
			return string.Format ("extract(hour from {0})::int4", field);
		}

		public override string CreateMinuteSql (object field)
		{
			return string.Format ("extract(minute from {0})::int4", field);
		}

		public override string CreateSecondSql (object field)
		{
			return string.Format ("extract(second from {0})::int4", field);
		}

		public override string CreateWeekSql (object field)
		{
			return string.Format ("extract(week from {0})::int4", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("extract(dow from {0})::int4", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("extract(doy from {0})::int4", field);
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
				return string.Format ("strpos({0},{1})", field, value);
			}
			else {
				throw new NotSupportedException ();
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
			return "current_time";
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

