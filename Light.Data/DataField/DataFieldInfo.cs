using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 字段信息,用户生成查询或排序表达式
	/// </summary>
	/// <typeparam name="T">字段类型</typeparam>
	public class DataFieldInfo<T> : DataFieldInfo where T : class, new()
	{
		/// <summary>
		/// 创建字段信息
		/// </summary>
		/// <param name="name">字段名称</param>
		/// <returns>字段信息</returns>
		public static DataFieldInfo<T> Create (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}
			return new DataFieldInfo<T> (name);
		}

		private DataFieldInfo (string name)
			: base (typeof(T), name)
		{

		}
	}

	/// <summary>
	/// 字段信息,用户生成查询或排序表达式
	/// </summary>
	public class DataFieldInfo : BasicFieldInfo//, ICloneable
	{
		internal DataFieldInfo (Type type, string name) :
			this (DataMapping.GetEntityMapping (type), name)
		{

		}

		internal DataFieldInfo (DataEntityMapping mapping, string name)
		{
			TableMapping = mapping;
			DataField = TableMapping.FindFieldMapping (name) as DataFieldMapping;
			if (DataField == null) {
				DataField = new CustomFieldMapping (name, mapping);
			}
		}

		internal DataFieldInfo (DataFieldMapping fieldMapping)
		{
			TableMapping = fieldMapping.EntityMapping;
			DataField = fieldMapping;
		}

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>The position.</value>
		public int Position {
			get {
				if (DataField != null) {
					return DataField.PositionOrder;
				}
				else {
					return -1;
				}
			}
		}

		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Eq (object value)
		{
			if (Object.Equals (value, null)) {
				return IsNull ();
			}
			else if (value is System.Collections.IEnumerable && !(value is string)) {
				return In ((System.Collections.IEnumerable)value);
			}
			else if (value is Boolean) {
				bool b = (bool)value;
				return b ? IsTrue () : IsFalse ();
			}
			else {
				return SingleParam (QueryPredicate.Eq, value);
			}
		}

		/// <summary>
		/// 少于等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtEq (object value)
		{
			return SingleParam (QueryPredicate.LtEq, value);
		}

		/// <summary>
		/// 少于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Lt (object value)
		{
			return SingleParam (QueryPredicate.Lt, value);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Gt (object value)
		{
			return SingleParam (QueryPredicate.Gt, value);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtEq (object value)
		{
			return SingleParam (QueryPredicate.GtEq, value);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotEq (object value)
		{
			if (Object.Equals (value, null)) {
				return IsNotNull ();
			}
			else if (value is System.Collections.IEnumerable && !(value is string)) {
				return NotIn ((System.Collections.IEnumerable)value);
			}
			else if (value is Boolean) {
				bool b = (bool)value;
				return !b ? IsTrue () : IsFalse ();
			}
			else {
				return SingleParam (QueryPredicate.NotEq, value);
			}
		}

		/// <summary>
		/// In查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression In (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.In, values);
		}

		/// <summary>
		/// In查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotIn (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, values);
		}

		/// <summary>
		/// In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression In (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.In, field, expression);
		}

		/// <summary>
		/// In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression In (DataFieldInfo field)
		{
			return In (field, null);
		}


		/// <summary>
		/// Not In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotIn (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, field, expression);
		}


		/// <summary>
		/// Not In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotIn (DataFieldInfo field)
		{
			return NotIn (field, null);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAll, field, expression);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAll (DataFieldInfo field)
		{
			return GtAll (field, null);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAll (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.GtAll, values);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAll, field, expression);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAll (DataFieldInfo field)
		{
			return LtAll (field, null);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAll (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.LtAll, values);
		}

		/// <summary>
		/// 大于Any子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAny, field, expression);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAny (DataFieldInfo field)
		{
			return GtAny (field, null);
		}

		/// <summary>
		/// 大于Any子查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression GtAny (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.GtAny, values);
		}

		/// <summary>
		/// 小于Any子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAny, field, expression);
		}

		/// <summary>
		/// 小于Any子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAny (DataFieldInfo field)
		{
			return LtAny (field, null);
		}

		/// <summary>
		/// 小于Any子查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public QueryExpression LtAny (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.LtAny, values);
		}


		/// <summary>
		/// Between查询
		/// </summary>
		/// <param name="fromvalue">开始值</param>
		/// <param name="tovalue">结束值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Between (object fromvalue, object tovalue)
		{
			return BetweenParams (true, fromvalue, tovalue);
		}

		/// <summary>
		/// Not Between查询
		/// </summary>
		/// <param name="fromvalue">开始值</param>
		/// <param name="tovalue">结束值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotBetween (object fromvalue, object tovalue)
		{
			return BetweenParams (false, fromvalue, tovalue);
		}

		/// <summary>
		/// like匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Like (string value)
		{
			return MatchValue (value, false, false, false);
		}

		/// <summary>
		/// like匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Like (params string[] values)
		{
			return MatchValue (values, false, false, false);
		}

		/// <summary>
		/// like匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Like (IEnumerable<string> values)
		{
			return MatchValue (values, false, false, false);
		}

		/// <summary>
		/// not like匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotLike (string value)
		{
			return MatchValue (value, false, false, true);
		}

		/// <summary>
		/// not like匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotLike (params string[] values)
		{
			return MatchValue (values, false, false, true);
		}

		/// <summary>
		/// not like匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotLike (IEnumerable<string> values)
		{
			return MatchValue (values, false, false, true);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Contains (string value)
		{
			return MatchValue (value, true, true, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Contains (params string[] values)
		{
			return MatchValue (values, true, true, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression Contains (IEnumerable<string> values)
		{
			return MatchValue (values, true, true, false);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotContains (string value)
		{
			return MatchValue (value, true, true, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotContains (params string[] values)
		{
			return MatchValue (values, true, true, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotContains (IEnumerable<string> values)
		{
			return MatchValue (values, true, true, true);
		}


		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression EndsWith (string value)
		{
			return MatchValue (value, true, false, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression EndsWith (params string[] values)
		{
			return MatchValue (values, true, false, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression EndsWith (IEnumerable<string> values)
		{
			return MatchValue (values, true, false, false);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotEndsWith (string value)
		{
			return MatchValue (value, true, false, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotEndsWith (params string[] values)
		{
			return MatchValue (values, true, false, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotEndsWith (IEnumerable<string> values)
		{
			return MatchValue (values, true, false, true);
		}


		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression StartsWith (string value)
		{
			return MatchValue (value, false, true, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression StartsWith (params string[] values)
		{
			return MatchValue (values, false, true, false);
		}

		/// <summary>
		/// 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression StartsWith (IEnumerable<string> values)
		{
			return MatchValue (values, false, true, false);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotStartsWith (string value)
		{
			return MatchValue (value, false, true, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotStartsWith (params string[] values)
		{
			return MatchValue (values, false, true, true);
		}

		/// <summary>
		/// not like 模糊匹配
		/// </summary>
		/// <param name="values">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression NotStartsWith (IEnumerable<string> values)
		{
			return MatchValue (values, false, true, true);
		}

		/// <summary>
		/// 查询字段是否空值
		/// </summary>
		/// <returns>查询表达式</returns>
		public QueryExpression IsNull ()
		{
			return Null (true);
		}

		/// <summary>
		/// 查询字段是否非空值
		/// </summary>
		/// <returns>查询表达式</returns>
		public QueryExpression IsNotNull ()
		{
			return Null (false);
		}

		/// <summary>
		/// 查询字段是否True
		/// </summary>
		/// <returns>查询表达式</returns>
		public QueryExpression IsTrue ()
		{
			return Boolean (true);
		}


		/// <summary>
		/// 查询字段是否False
		/// </summary>
		/// <returns>查询表达式</returns>
		public QueryExpression IsFalse ()
		{
			return Boolean (false);
		}

		private QueryExpression SingleParam (QueryPredicate predicate, object value)
		{
			return SingleParam (predicate, value, false);
		}

		private QueryExpression SingleParam (QueryPredicate predicate, object value, bool isReverse)
		{
			if (Object.Equals (value, null)) {
				throw new ArgumentNullException ("value");
			}
			QueryExpression exp;
			DataFieldInfo dataFieldInfo = value as DataFieldInfo;
			if (!Object.Equals (dataFieldInfo, null)) {
				exp = new DataFieldQueryExpression (this, predicate, dataFieldInfo, isReverse);
			}
			else {
				exp = new SingleParamQueryExpression (this, predicate, value, isReverse);
			}
			return exp;
		}

		private QueryExpression CollectionParams (QueryCollectionPredicate predicate, System.Collections.IEnumerable values)
		{
			if (Object.Equals (values, null)) {
				throw new ArgumentNullException ("values");
			}
			QueryExpression exp = new CollectionParamsQueryExpression (this, predicate, values);
			return exp;
		}

		private QueryExpression CollectionParams (QueryCollectionPredicate predicate, DataFieldInfo field, QueryExpression expression)
		{
			if (Object.Equals (field, null)) {
				throw new ArgumentNullException ("field");
			}
//			if (expression == null) {
//				throw new ArgumentNullException ("expression");
//			}
			QueryExpression exp = new SubQueryExpression (this, predicate, field, expression);
			return exp;
		}

		private QueryExpression BetweenParams (bool isNot, object fromValue, object toValue)
		{
			if (Object.Equals (fromValue, null)) {
				throw new ArgumentNullException ("fromValue");
			}
			if (Object.Equals (toValue, null)) {
				throw new ArgumentNullException ("toValue");
			}
			QueryExpression exp = new BetweenParamsQueryExpression (this, isNot, fromValue, toValue);
			return exp;
		}

		private QueryExpression MatchValue (string value, bool starts, bool ends, bool isNot)
		{
			if (value == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, value, false, starts, ends, isNot);
			return exp;
		}

		private QueryExpression MatchValue (IEnumerable<string> values, bool starts, bool ends, bool isNot)
		{
			if (values == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, values, false, starts, ends, isNot);
			return exp;
		}

		private QueryExpression Null (bool isNull)
		{
			QueryExpression exp = new NullQueryExpression (this, isNull);
			return exp;
		}

		private QueryExpression Boolean (bool isTrue)
		{
			QueryExpression exp = new BooleanQueryExpression (this, isTrue);
			return exp;
		}

		/// <summary>
		/// 字段顺序排序
		/// </summary>
		/// <returns>排序表达式</returns>
		public OrderExpression OrderByAsc ()
		{
			return OrderBy (OrderType.ASC);
		}

		/// <summary>
		/// 字段倒序排序
		/// </summary>
		/// <returns>排序表达式</returns>
		public OrderExpression OrderByDesc ()
		{
			return OrderBy (OrderType.DESC);
		}

		private OrderExpression OrderBy (OrderType type)
		{
			OrderExpression exp = new FieldOrderExpression (this, type);
			return exp;
		}

		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator == (DataFieldInfo field, object value)
		{
			return field.Eq (value);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator != (DataFieldInfo field, object value)
		{
			return field.NotEq (value);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator > (DataFieldInfo field, object value)
		{
			return field.Gt (value);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator >= (DataFieldInfo field, object value)
		{
			return field.GtEq (value);
		}

		/// <summary>
		/// 小于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator < (DataFieldInfo field, object value)
		{
			return field.Lt (value);
		}

		/// <summary>
		/// 小于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static QueryExpression operator <= (DataFieldInfo field, object value)
		{
			return field.LtEq (value);
		}

		internal virtual string DBType {
			get {
				return DataField.DBType;
			}
		}

		internal virtual string CreateDataFieldSql (CommandFactory factory)
		{
			return CreateDataFieldSql (factory, false);
		}

		internal virtual string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			if (isFullName) {
				return factory.CreateFullDataFieldSql (TableMapping.TableName, FieldName);
			}
			else {
				return factory.CreateDataFieldSql (FieldName);
			}
		}

		//		/// <summary>
		//		/// 可在字段的左右两边添加匹配字符
		//		/// </summary>
		//		/// <param name="left">左匹配</param>
		//		/// <param name="right">右匹配</param>
		//		/// <returns></returns>
		//		public MatchDataFieldInfo TransformMatch (bool left, bool right)
		//		{
		//			return new MatchDataFieldInfo (this, left, right);
		//		}

		/// <summary>
		/// Transforms the starts with match.
		/// </summary>
		/// <returns>The starts with match.</returns>
		public MatchDataFieldInfo TransformEndsWithMatch ()
		{
			return new MatchDataFieldInfo (this, true, false);
		}

		/// <summary>
		/// Transforms the endss with match.
		/// </summary>
		/// <returns>The endss with match.</returns>
		public MatchDataFieldInfo TransformStartsWithMatch ()
		{
			return new MatchDataFieldInfo (this, false, true);
		}

		/// <summary>
		/// Transforms the contains match.
		/// </summary>
		/// <returns>The contains match.</returns>
		public MatchDataFieldInfo TransformContainsMatch ()
		{
			return new MatchDataFieldInfo (this, true, true);
		}

		/// <summary>
		/// 获取时间类型字段的日期,返回时间类型数据
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformDate ()
		{
			return new DateDataFieldInfo (this, null);
			//return new DateDataFieldInfo(DataField, null);
		}

		/// <summary>
		/// 获取时间类型字段的日期,按Y-M-D格式返回字符串类型的日期
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformDateString ()
		{
			return new DateDataFieldInfo (this, "Y-M-D");
			//return new DateDataFieldInfo(DataField, "Y-M-D");
		}

		/// <summary>
		/// 获取时间类型字段的日期,按format格式返回字符串类型的日期
		/// </summary>
		/// <param name="format">日期格式 Y年M月D日 目前支持格式 YMD|YM|Y-M-D|Y-M|D-M-Y|M-D-Y|Y/M/D|Y/M|D/M/Y|M/D/Y</param>
		/// <returns></returns>
		public DataFieldInfo TransformDateString (string format)
		{
			if (string.IsNullOrEmpty (format)) {
				throw new ArgumentNullException (format);
			}
			return new DateDataFieldInfo (this, format);
			//return new DateDataFieldInfo(DataField, format);
		}

		/// <summary>
		/// 获取时间类型字段的年份
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformYear ()
		{
			//return new YearDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Year);
		}

		/// <summary>
		/// 获取时间类型字段的月份
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformMonth ()
		{
			//return new MonthDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Month);
		}

		/// <summary>
		/// 获取时间类型字段的当月日数
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformDay ()
		{
			//return new DayDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Day);
		}

		/// <summary>
		/// 获取时间类型字段的小时数
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformHour ()
		{
			//return new HourDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Hour);
		}

		/// <summary>
		/// 获取时间类型字段的分钟数
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformMinute ()
		{
			//return new MinuteDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Minute);
		}

		/// <summary>
		/// 获取时间类型字段的秒数
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformSecond ()
		{
			//return new SecondDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Second);
		}

		/// <summary>
		/// 获取时间类型字段的当年周数
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformWeek ()
		{
			//return new WeekDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Week);
		}

		/// <summary>
		/// 获取时间类型字段的星期索引,不同数据库有不同的定义
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformWeekDay ()
		{
			//return new WeekDayDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.WeekDay);
		}

		/// <summary>
		/// 获取字符串类型字段的长度
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformLength ()
		{
			return new LengthDataFieldInfo (this);
		}

		/// <summary>
		/// 获取字符串类型字段的指定位置数据
		/// </summary>
		/// <param name="start">开始索引</param>
		/// <param name="size">长度</param>
		/// <returns></returns>
		public DataFieldInfo TransformSubString (int start, int size)
		{
			if (size <= 0) {
				throw new ArgumentOutOfRangeException ("size");
			}
			return new SubStringDataFieldInfo (this, start, size);
		}

		/// <summary>
		/// 获取字符串类型字段的指定位置数据
		/// </summary>
		/// <param name="start">开始索引</param>
		/// <returns></returns>
		public DataFieldInfo TransformSubString (int start)
		{
			return new SubStringDataFieldInfo (this, start, 0);
		}

		#region math operate int

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate long

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}



		#endregion

		#region math operate short

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate uint

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate ulong

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}



		#endregion

		#region math operate ushort

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate float

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate double

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate decimal

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}

		/// <summary>
		/// 加
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator + (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}

		/// <summary>
		/// 减
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator - (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}

		/// <summary>
		/// 乘
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator * (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}

		/// <summary>
		/// 除
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator / (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}

		/// <summary>
		/// 余
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator % (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}

		/// <summary>
		/// 幂
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DataFieldInfo operator ^ (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		private DataFieldInfo TransformMathCalculate (MathOperator opera, object value, bool forward)
		{
			return new MathCalculateDataFieldInfo (this, opera, value, forward);
		}

		/// <summary>
		/// 获取字段的绝对值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformAbs ()
		{
			return TransformMathFunction (MathFunction.Abs);
		}

		/// <summary>
		/// 获取字段的e为底的对数值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformLog ()
		{
			return TransformMathFunction (MathFunction.Log);
		}

		/// <summary>
		/// 获取字段的e 的给定次幂
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformExp ()
		{
			return TransformMathFunction (MathFunction.Exp);
		}

		/// <summary>
		/// 获取字段的正弦值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformSin ()
		{
			return TransformMathFunction (MathFunction.Sin);
		}

		/// <summary>
		/// 获取字段的余弦值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformCos ()
		{
			return TransformMathFunction (MathFunction.Cos);
		}

		/// <summary>
		/// 获取字段的正切值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformTan ()
		{
			return TransformMathFunction (MathFunction.Tan);
		}

		/// <summary>
		/// 获取字段的反正切值
		/// </summary>
		/// <returns></returns>
		public DataFieldInfo TransformAtan ()
		{
			return TransformMathFunction (MathFunction.Atan);
		}


		private DataFieldInfo TransformMathFunction (MathFunction function)
		{
			return new MathFunctionDataFieldInfo (this, function);
		}

		/// <summary>
		/// 匹配对象是否相等
		/// </summary>
		/// <param name="target">匹配对象</param>
		/// <returns></returns>
		public virtual bool Equals (DataFieldInfo target)
		{
			if (Object.Equals (target, null)) {
				return false;
			}
			if (Object.ReferenceEquals (this, target)) {
				return true;
			}
			else {
				if (this.GetType () == target.GetType ()) {
					return EqualsDetail (target);
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected virtual bool EqualsDetail (DataFieldInfo info)
		{
			bool result = Object.Equals (this.DataField, info.DataField);
			if (!result) {
				if (this.DataField is CustomFieldMapping && info.DataField is CustomFieldMapping) {
					return this.DataField.Name == info.DataField.Name && Object.Equals (this.DataField.EntityMapping, info.DataField.EntityMapping);
				}
			}
			return result;
		}

		/// <summary>
		/// 匹配对象是否相等
		/// </summary>
		/// <param name="obj">匹配对象</param>
		/// <returns></returns>
		public override bool Equals (object obj)
		{
			return object.ReferenceEquals (this, obj);
		}

		/// <summary>
		/// 获取哈希码
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}


		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression Eq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Eq, field);
		}

		/// <summary>
		/// 少于等于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression LtEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.LtEq, field);
		}

		/// <summary>
		/// 少于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression Lt (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Lt, field);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression Gt (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Gt, field);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression GtEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.GtEq, field);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="field">数值</param>
		/// <returns>查询表达式</returns>
		public DataFieldExpression NotEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.NotEq, field);
		}


		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator == (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Eq (mfield);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator != (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.NotEq (mfield);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator > (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Gt (mfield);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator >= (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.GtEq (mfield);
		}

		/// <summary>
		/// 小于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator < (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Lt (mfield);
		}

		/// <summary>
		/// 小于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="mfield"></param>
		/// <returns></returns>
		public static DataFieldExpression operator <= (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.LtEq (mfield);
		}


		private DataFieldExpression OnDataFieldMatch (QueryPredicate predicate, DataFieldInfo field)
		{
			if (Object.Equals (field, null) && predicate != QueryPredicate.Eq && predicate != QueryPredicate.NotEq) {
				throw new ArgumentNullException ("field");
			}
			DataFieldMatchExpression exp = new DataFieldMatchExpression (this, field, predicate);
			return exp;
		}

		internal virtual object ToParameter (object value)
		{
			return base.DataField.ToParameter (value);
		}

	}
}
