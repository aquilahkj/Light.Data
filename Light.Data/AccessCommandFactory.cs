using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Light.Data.Expressions;
using Light.Data.Mappings;

namespace Light.Data
{
	class AccessCommandFactory : CommandFactory
	{
		public AccessCommandFactory (Database database)
			: base (database)
		{

		}

		public override string CreateDataFieldSql (string fieldName)
		{
			return string.Format ("[{0}]", fieldName);
		}

		public void UseAccessWildcards ()
		{
			_wildcards = "*";
		}

		public override string GetHavingString (DataEntityMapping mapping, AggregateHavingExpression having, out DataParameter[] parameters, Dictionary<string, AggregateFunction> aggregateFunctionDictionary)
		{
			string havingString = null;
			parameters = null;
			if (having != null) {
				if (!having.IgnoreConsistency && !mapping.Equals (having.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregationExpression);
				}
				havingString = string.Format ("having {0}", having.CreateSqlString (this, out parameters, new GetAliasHandler (delegate(object obj) {
					return null;
				})));
			}
			return havingString;
		}

		public override string GetOrderString (DataEntityMapping mapping, OrderExpression order, out DataParameter[] parameters, Dictionary<string, DataFieldInfo> dataFieldInfoDictionary, Dictionary<string, AggregateFunction> aggregateFunctionDictionary)
		{
			string orderString = null;
			parameters = null;
			if (order != null) {
				if (order.IgnoreConsistency) {
					RandomOrderExpression random = order as RandomOrderExpression;
					if (random != null) {
						random.SetTableMapping (mapping);
					}
				}
				if (!order.IgnoreConsistency && !mapping.Equals (order.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchOrderExpression);
				}
				orderString = string.Format ("order by {0}", order.CreateSqlString (this, out parameters, new GetAliasHandler (delegate(object obj) {
					return null;
				})));
			}
			return orderString;
		}

		public override string CreateConditionCountSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("count({2}iif({0},{1},null))", expressionSql, !string.IsNullOrEmpty (fieldName) ? CreateDataFieldSql (fieldName) : "1", isDistinct ? "distinct " : "");
		}

		public override string CreateConditionSumSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("sum({2}iif({0},{1},0))", expressionSql, CreateDataFieldSql (fieldName), isDistinct ? "distinct " : "");
		}

		public override string CreateConditionAvgSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("avg({2}iif({0},cdbl({1}),0,)", expressionSql, CreateDataFieldSql (fieldName), isDistinct ? "distinct " : "");
		}

		/// <summary>
		/// 创建内容Exists查询命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="query">查询表达式</param>
		/// <returns></returns>
		public override IDbCommand CreateExistsCommand (DataEntityMapping mapping, QueryExpression query)
		{
			return this.CreateSelectBaseCommand (mapping, "top 1", query, null, null);
		}

		public override string CreateDataTableSql (string tableName)
		{
			return string.Format ("[{0}]", tableName);
		}

		public override string CreateRandomOrderBySql (DataEntityMapping mapping)
		{
			Random rnd = new Random (unchecked((int)DateTime.Now.Ticks));
			int intRandomNumber = rnd.Next () * -1;

			string field = "";
			foreach (DataFieldMapping df in mapping.GetFieldMappings()) {
				if (df.ObjectType == typeof(int)) {
					field = CreateDataFieldSql (df.Name);
					break;
				}
			}
			if (string.IsNullOrEmpty (field)) {
				foreach (DataFieldMapping df in mapping.GetFieldMappings()) {
					if (df.ObjectType == typeof(string)) {
						field = string.Format ("len({0})", CreateDataFieldSql (df.Name));
						break;
					}
				}
			}
			if (string.IsNullOrEmpty (field)) {
				throw new LightDataException (RE.TypfOfDataFieldIsNotString);
			}
			return string.Format ("rnd({0}*{1})", intRandomNumber, field);
		}

		public override string CreateMatchSql (string value, bool left, bool right)
		{
			StringBuilder sb = new StringBuilder ();
			if (left) {
				sb.AppendFormat ("'{0}'+", _wildcards);
			}
			sb.Append (value);
			if (right) {
				sb.AppendFormat ("+'{0}'", _wildcards);
			}
			return sb.ToString ();
		}

		public override string CreateDateSql (string value, string format)
		{
			if (string.IsNullOrEmpty (format)) {
				return string.Format ("cdate(format({0}),'yyyy-mm-dd')", value);
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
				return string.Format ("format({0},'{1}')", value, sqlformat);
			}
		}

		public override string CreateYearSql (string value)
		{
			return string.Format ("year({0})", value);
		}

		public override string CreateMonthSql (string value)
		{
			return string.Format ("month({0})", value);
		}

		public override string CreateDaySql (string value)
		{
			return string.Format ("day({0})", value);
		}

		public override string CreateHourSql (string value)
		{
			return string.Format ("hour({0})", value);
		}

		public override string CreateMinuteSql (string value)
		{
			return string.Format ("minute({0})", value);
		}

		public override string CreateSecondSql (string value)
		{
			return string.Format ("second({0})", value);
		}

		public override string CreateWeekSql (string value)
		{
			return string.Format ("val(format({0},'ww'))", value);
		}

		public override string CreateWeekDaySql (string value)
		{
			return string.Format ("weekday({0})", value);
		}

		public override string CreateLengthSql (string value)
		{
			return string.Format ("len({0})", value);
		}

		public override string CreateSubStringSql (string value, int start, int size)
		{
			if (size == 0) {
				return string.Format ("mid({0},{1})", value, start);
			}
			else {
				return string.Format ("mid({0},{1},{2})", value, start, size);
			}
		}

		public override string CreateModSql (string field, object value)
		{
			return string.Format ("{0} mod {1}", field, value);
		}

		public override string CreateAtanSql (string field)
		{
			return string.Format ("atn({0})", field);
		}
	}
}
