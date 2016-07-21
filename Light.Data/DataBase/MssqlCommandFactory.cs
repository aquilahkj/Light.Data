using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
	class MssqlCommandFactory : CommandFactory
	{
		public MssqlCommandFactory ()
		{
			_canInnerPage = true;
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("[{0}]", fieldName);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("[{0}]", tableName);
		}

		public override string GetHavingString (AggregateHavingExpression having, out DataParameter [] parameters, List<AggregateFunctionInfo> functions)
		{
			string havingString = null;
			parameters = null;
			if (having != null) {
				havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate (object obj) {
					return null;
				})));
			}
			return havingString;
		}

		protected override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, DataParameter [] dataParameters, QueryExpression query, OrderExpression order, Region region)
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
			bool distinct = false;
			if (customSelect.StartsWith ("distinct ", StringComparison.OrdinalIgnoreCase)) {
				distinct = true;
				customSelect = customSelect.Substring (9);
			}
			if (region.Start == 0) {
				sql.AppendFormat ("select {3}top {2} {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName), region.Size, distinct ? "distinct " : string.Empty);
				if (!string.IsNullOrEmpty (queryString)) {
					sql.AppendFormat (" {0}", queryString);
					//if (queryparameters != null) {
					//	parameters.AddRange (queryparameters);
					//}
				}
				if (!string.IsNullOrEmpty (orderString)) {
					sql.AppendFormat (" {0}", orderString);
					//if (orderparameters != null) {
					//	parameters.AddRange (orderparameters);
					//}
				}
			}
			else {
				StringBuilder innerSQL = new StringBuilder ();
				string tempCount = CreateCustomFiledName ();
				string tempRowNumber = CreateCustomFiledName ();
				innerSQL.AppendFormat ("select {4}top {2} {0},0 {3} from {1}", customSelect, CreateDataTableSql (mapping.TableName), region.Start + region.Size, tempCount, distinct ? "distinct " : string.Empty);
				if (!string.IsNullOrEmpty (queryString)) {
					innerSQL.AppendFormat (" {0}", queryString);
				}
				if (!string.IsNullOrEmpty (orderString)) {
					innerSQL.AppendFormat (" {0}", orderString);
				}
				sql.AppendFormat ("select {1} from (select a.*,row_number()over(order by {3}) {4} from ({0})a )b where {4}>{2}",
					innerSQL, customSelect, region.Start, tempCount, tempRowNumber);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (dataParameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
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

		public override string CreateAvgSql (object fieldName, bool isDistinct)
		{
			return string.Format ("avg({1}convert(float,{0}))", fieldName, isDistinct ? "distinct " : "");
		}

		public override string CreateConditionAvgSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("avg({2}case when {0} then convert(float,{1}) else null end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
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
				return string.Format ("cast({0} as date)", field);
			}
			else {
				string format1 = format.ToUpper ();
				string sqlformat;
				switch (format1) {
				case "YMD":
					sqlformat = "convert(char(8),{0},112)";
					break;
				case "YM":
					sqlformat = "convert(char(6),{0},112)";
					break;
				case "Y-M-D":
					sqlformat = "convert(char(10),{0},23)";
					break;
				case "Y-M":
					sqlformat = "convert(char(7),{0},23)";
					break;
				case "M-D-Y":
					sqlformat = "convert(char(10),{0},110)";
					break;
				case "D-M-Y":
					sqlformat = "convert(char(10),{0},105)";
					break;
				case "Y/M/D":
					sqlformat = "convert(char(10),{0},111)";
					break;
				case "Y/M":
					sqlformat = "convert(char(7),{0},111)";
					break;
				case "M/D/Y":
					sqlformat = "convert(char(10),{0},101)";
					break;
				case "D/M/Y":
					sqlformat = "convert(char(10),{0},103)";
					break;
				default:
					throw new LightDataException (string.Format (RE.UnsupportDateFormat, format));
				}
				return string.Format (sqlformat, field);
			}
		}

		public override string CreateYearSql (object field)
		{
			return string.Format ("datepart(year,{0})", field);
		}

		public override string CreateMonthSql (object field)
		{
			return string.Format ("datepart(month,{0})", field);
		}

		public override string CreateDaySql (object field)
		{
			return string.Format ("datepart(day,{0})", field);
		}

		public override string CreateHourSql (object field)
		{
			return string.Format ("datepart(hour,{0})", field);
		}

		public override string CreateMinuteSql (object field)
		{
			return string.Format ("datepart(minute,{0})", field);
		}

		public override string CreateSecondSql (object field)
		{
			return string.Format ("datepart(second,{0})", field);
		}

		public override string CreateWeekSql (object field)
		{
			return string.Format ("datepart(week,{0})", field);
		}

		public override string CreateWeekDaySql (object field)
		{
			return string.Format ("datepart(weekday,{0})-1", field);
		}

		public override string CreateYearDaySql (object field)
		{
			return string.Format ("datepart(dayofyear,{0})", field);
		}

		public override string CreateLengthSql (object field)
		{
			return string.Format ("len({0})", field);
		}

		public override string CreateLogSql (object field)
		{
			return string.Format ("log({0})", field);
		}

		public override string CreateSubStringSql (object field, object start, object size)
		{
			//start++;
			//if (size == 0) {
			//	return string.Format ("substring({0},{1},len({0})-{1}+1)", field, start);
			//}
			//else {
			//	return string.Format ("substring({0},{1},{2})", field, start, size);
			//}

			if (object.Equals (size, null)) {
				return string.Format ("substring({0},{1}+1,len({0}))", field, start);
			}
			else {
				return string.Format ("substring({0},{1}+1,{2})", field, start, size);
			}
		}

		public override string CreateDividedSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("convert(float,{0})/{1}", field, value);
			}
			else {
				return string.Format ("convert(float,{0})/{1}", value, field);
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

		public override string CreateDataBaseTimeSql ()
		{
			return "getdate()";
		}

		public override string CreateParamName (string name)
		{
			if (name == null)
				throw new ArgumentNullException (nameof (name));
			if (!name.StartsWith ("@", StringComparison.Ordinal)) {
				return "@" + name;
			}
			else {
				return name;
			}
		}
	}
}