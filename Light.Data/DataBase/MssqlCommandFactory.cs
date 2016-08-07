using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
	class MssqlCommandFactory : CommandFactory
	{
		Dictionary<string, string> dateTimeFormatDict = new Dictionary<string, string> ();

		readonly string defaultDateTime = "CONVERT(char(19), {0}, 120)";

		public MssqlCommandFactory ()
		{
			_canInnerPage = true;
			dateTimeFormatDict.Add ("yyyy-MM-dd hh:mm:ss", "CONVERT(char(19), {0}, 120)");
			dateTimeFormatDict.Add ("yyyy-MM-dd", "CONVERT(char(10), {0}, 23)");
			dateTimeFormatDict.Add ("MM/dd/yyyy", "CONVERT(char(10), {0}, 101)");
			dateTimeFormatDict.Add ("yyyy.MM.dd", "CONVERT(char(10), {0}, 102)");
			dateTimeFormatDict.Add ("dd/MM/yyyy", "CONVERT(char(10), {0}, 103)");
			dateTimeFormatDict.Add ("dd.MM.yyyy", "CONVERT(char(10), {0}, 104)");
			dateTimeFormatDict.Add ("dd-MM-yyyy", "CONVERT(char(10), {0}, 105)");
			dateTimeFormatDict.Add ("dd MM yyyy", "CONVERT(char(10), {0}, 106)");
			dateTimeFormatDict.Add ("MM dd, yyyy", "CONVERT(char(11), {0}, 107)");
			dateTimeFormatDict.Add ("hh:mm:ss", "CONVERT(char(8), {0}, 108)");
			dateTimeFormatDict.Add ("MM-dd-yyyy", "CONVERT(char(10), {0}, 110)");
			dateTimeFormatDict.Add ("yyyy/MM/dd", "CONVERT(char(10), {0}, 111)");
			dateTimeFormatDict.Add ("yyyyMMdd", "CONVERT(char(8), {0}, 112)");
			dateTimeFormatDict.Add ("yyyyMM", "CONVERT(char(6), {0}, 112)");
			dateTimeFormatDict.Add ("yyyy", "CONVERT(char(4), {0}, 112)");
			dateTimeFormatDict.Add ("MM", "CONVERT(char(2), {0}, 101)");
			dateTimeFormatDict.Add ("dd", "CONVERT(char(2), {0}, 103)");
			dateTimeFormatDict.Add ("hh:mm", "CONVERT(char(5), {0}, 108)");

			dateTimeFormatDict.Add ("yyyy-MM","CONVERT(char(7), {0}, 23)");
			dateTimeFormatDict.Add ("dd-MM", "CONVERT(char(5), {0}, 105)");
			dateTimeFormatDict.Add ("MM-dd", "CONVERT(char(5), {0}, 110)");

			dateTimeFormatDict.Add ("yyyy/MM", "CONVERT(char(7), {0}, 111)");
			dateTimeFormatDict.Add ("dd/MM", "CONVERT(char(5), {0}, 103)");
			dateTimeFormatDict.Add ("MM/dd", "CONVERT(char(5), {0}, 101)");

			dateTimeFormatDict.Add ("yyyy.MM", "CONVERT(char(7), {0}, 102)");
			dateTimeFormatDict.Add ("dd.MM", "CONVERT(char(5), {0}, 104)");

			dateTimeFormatDict.Add ("dd MM", "CONVERT(char(5), {0}, 106)");
			dateTimeFormatDict.Add ("MM dd", "CONVERT(char(5), {0}, 107)");
		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("[{0}]", fieldName);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("[{0}]", tableName);
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

		public override CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, DataParameter [] dataParameters, QueryExpression query, OrderExpression order, Region region)
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
				}
				if (!string.IsNullOrEmpty (orderString)) {
					sql.AppendFormat (" {0}", orderString);
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

		public override string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, List<DataParameter> dataParameters)
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

		public override string CreateDateTimeFormatSql (string field, string format)
		{
			string sqlformat;
			if (string.IsNullOrEmpty (format)) {
				sqlformat = defaultDateTime;
			}
			else if (!dateTimeFormatDict.TryGetValue (format, out sqlformat)) {
				throw new NotSupportedException ();
			}
			return string.Format (sqlformat, field);
		}


		public override string CreateTruncateSql (object field)
		{
			return string.Format ("cast({0} as int)", field);
		}

		public override string CreateAtan2Sql (object field, object value)
		{
			return string.Format ("atn2({0},{1})", field, value);
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
			if (object.Equals (size, null)) {
				return string.Format ("substring({0},{1},len({0}))", field, start);
			}
			else {
				return string.Format ("substring({0},{1},{2})", field, start, size);
			}
		}

		public override string CreateIndexOfSql (object field, object value, object startIndex)
		{
			if (object.Equals (startIndex, null)) {
				return string.Format ("charindex({0},{1})", value, field);
			}
			else {
				return string.Format ("charindex({0},{1},{2})", value, field, startIndex);
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
			return string.Format ("rtrim(ltrim({0}))", field);
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

		public override string CreatePowerSql (object left, object right)
		{
			return string.Format ("power({0},{1})", left, right);
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