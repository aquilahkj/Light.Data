using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Expressions;
using Light.Data.Function;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 聚合函数
	/// </summary>
	public abstract class AggregateFunction
	{
		internal AggregateFunction (DataEntityMapping tableMapping)
		{
			TableMapping = tableMapping;
		}

		internal DataEntityMapping TableMapping {
			get;
			set;
		}

		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression Eq (object value)
		{
			return SingleParam (QueryPredicate.Eq, value);
		}

		/// <summary>
		/// 少于等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression LtEq (object value)
		{
			return SingleParam (QueryPredicate.LtEq, value);
		}

		/// <summary>
		/// 少于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression Lt (object value)
		{
			return SingleParam (QueryPredicate.Lt, value);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression Gt (object value)
		{
			return SingleParam (QueryPredicate.Gt, value);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression GtEq (object value)
		{
			return SingleParam (QueryPredicate.GtEq, value);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="value">数值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression NotEq (object value)
		{
			return SingleParam (QueryPredicate.NotEq, value);
		}

		/// <summary>
		/// In查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression In (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.In, values);
		}

		/// <summary>
		/// In查询
		/// </summary>
		/// <param name="values">数据枚举集</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression NotIn (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, values);
		}

		/// <summary>
		/// In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression In (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.In, field, expression);
		}

		/// <summary>
		/// In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression In (DataFieldInfo field)
		{
			return In (field, null);
		}


		/// <summary>
		/// Not In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression NotIn (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, field, expression);
		}


		/// <summary>
		/// Not In子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression NotIn (DataFieldInfo field)
		{
			return NotIn (field, null);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression GtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAll, field, expression);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression GtAll (DataFieldInfo field)
		{
			return GtAll (field, null);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression LtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAll, field, expression);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression LtAll (DataFieldInfo field)
		{
			return LtAll (field, null);
		}

		/// <summary>
		/// 大于Any子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression GtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAny, field, expression);
		}

		/// <summary>
		/// 大于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression GtAny (DataFieldInfo field)
		{
			return GtAny (field, null);
		}

		/// <summary>
		/// 小于Any子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <param name="expression">子查询表达式</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression LtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAny, field, expression);
		}

		/// <summary>
		/// 小于All子查询
		/// </summary>
		/// <param name="field">子查询字段</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression LtAny (DataFieldInfo field)
		{
			return LtAny (field, null);
		}

		/// <summary>
		/// Between查询
		/// </summary>
		/// <param name="fromvalue">开始值</param>
		/// <param name="tovalue">结束值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression Between (object fromvalue, object tovalue)
		{
			return BetweenParams (true, fromvalue, tovalue);
		}

		/// <summary>
		/// Not Between查询
		/// </summary>
		/// <param name="fromvalue">开始值</param>
		/// <param name="tovalue">结束值</param>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression NotBetween (object fromvalue, object tovalue)
		{
			return BetweenParams (false, fromvalue, tovalue);
			;
		}

		/// <summary>
		/// 查询字段是否空值
		/// </summary>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression IsNull ()
		{
			return Null (true);
		}

		/// <summary>
		/// 查询字段是否非空值
		/// </summary>
		/// <returns>查询表达式</returns>
		public AggregateHavingExpression IsNotNull ()
		{
			return Null (false);
		}

		private AggregateHavingExpression SingleParam (QueryPredicate predicate, object value)
		{
			return SingleParam (predicate, value, false);
		}

		private AggregateHavingExpression SingleParam (QueryPredicate predicate, object value, bool isReverse)
		{
			if (Object.Equals (value, null)) {
				throw new ArgumentNullException ("value");
			}
			AggregateHavingExpression exp = new SingleParamAggregateExpression (this, predicate, value, isReverse);
			return exp;
		}

		private AggregateHavingExpression CollectionParams (QueryCollectionPredicate predicate, System.Collections.IEnumerable values)
		{
			if (Object.Equals (values, null)) {
				throw new ArgumentNullException ("values");
			}
			AggregateHavingExpression exp = new CollectionParamsAggregateExpression (this, predicate, values);
			return exp;
		}

		private AggregateHavingExpression CollectionParams (QueryCollectionPredicate predicate, DataFieldInfo field, QueryExpression expression)
		{
			if (Object.Equals (field, null)) {
				throw new ArgumentNullException ("field");
			}
			if (expression == null) {
				throw new ArgumentNullException ("expression");
			}
			if (!expression.IgnoreConsistency && !field.TableMapping.Equals (expression.TableMapping)) {
				throw new LightDataException (RE.DataMappingIsNotMatchQueryExpression);
			}
			AggregateHavingExpression exp = new SubAggregateExpression (this, predicate, field, expression);
			return exp;
		}



		private AggregateHavingExpression BetweenParams (bool isNot, object fromValue, object toValue)
		{
			if (Object.Equals (fromValue, null)) {
				throw new ArgumentNullException ("fromValue");
			}
			if (Object.Equals (toValue, null)) {
				throw new ArgumentNullException ("toValue");
			}
			AggregateHavingExpression exp = new BetweenParamsAggregateExpression (this, isNot, fromValue, toValue);
			return exp;
		}

		private AggregateHavingExpression Null (bool isNull)
		{
			AggregateHavingExpression exp = new NullAggregateExpression (this, isNull);
			return exp;
		}

		/// <summary>
		/// 等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator == (AggregateFunction field, object value)
		{
			if (value == null)
				return field.IsNull ();
			if (value is System.Collections.IEnumerable && value.GetType () != typeof(string))
				return field.In ((System.Collections.IEnumerable)value);
			return field.Eq (value);
		}

		/// <summary>
		/// 不等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator != (AggregateFunction field, object value)
		{
			if (value == null)
				return field.IsNotNull ();
			if (value is System.Collections.IEnumerable && value.GetType () != typeof(string))
				return field.NotIn ((System.Collections.IEnumerable)value);
			return field.NotEq (value);
		}

		/// <summary>
		/// 大于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator > (AggregateFunction field, object value)
		{
			return field.Gt (value);
		}

		/// <summary>
		/// 大于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator >= (AggregateFunction field, object value)
		{
			return field.GtEq (value);
		}

		/// <summary>
		/// 小于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator < (AggregateFunction field, object value)
		{
			return field.Lt (value);
		}

		/// <summary>
		/// 小于等于
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static AggregateHavingExpression operator <= (AggregateFunction field, object value)
		{
			return field.LtEq (value);
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
			OrderExpression exp = new AggregateOrderExpression (this, type);
			return exp;
		}

		internal abstract string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters);

		/// <summary>
		/// 生成计数函数,Count所有数据
		/// </summary>
		/// <returns>函数对象</returns>
		public static AggregateFunction Count ()
		{
			return new CountAllFunction ();
		}

		/// <summary>
		/// 条件计数语句
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Count (QueryExpression expression)
		{
			if (expression == null) {
				throw new ArgumentNullException ("expression");
			}
			return new ConditionCountFunction (!expression.IgnoreConsistency ? expression.TableMapping : null, expression, null, false);
		}

		/// <summary>
		/// 条件计数语句
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Count (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException ("expression");
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			if (!expression.IgnoreConsistency && !fieldInfo.TableMapping.Equals (expression.TableMapping)) {
				throw new LightDataException (RE.DataMappingIsNotMatchQueryExpression);
			}
			return new ConditionCountFunction (fieldInfo.TableMapping, expression, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 对字段计算统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Count (DataFieldInfo fieldInfo)
		{
			return Count (fieldInfo, false);
		}

		/// <summary>
		///  对字段计算统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Count (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			return new CountFunction (fieldInfo.TableMapping, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 对字段总和统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Sum (DataFieldInfo fieldInfo)
		{
			return Sum (fieldInfo, false);
		}

		/// <summary>
		/// 对字段总和统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Sum (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			return new SumFunction (fieldInfo.TableMapping, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 对字段总和进行条件统计
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Sum (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			return Sum (expression, fieldInfo, false);
		}

		/// <summary>
		/// 对字段总和进行条件统计
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Sum (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException ("expression");
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			if (!expression.IgnoreConsistency && !fieldInfo.TableMapping.Equals (expression.TableMapping)) {
				throw new LightDataException (RE.DataMappingIsNotMatchQueryExpression);
			}
			return new ConditionSumFunction (fieldInfo.TableMapping, expression, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 对字段进行平均值统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Avg (DataFieldInfo fieldInfo)
		{
			return Avg (fieldInfo, false);
		}

		/// <summary>
		/// 对字段进行平均值统计
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Avg (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			return new AvgFunction (fieldInfo.TableMapping, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 对字段进行平均值条件统计
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Avg (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			return Avg (expression, fieldInfo, false);
		}

		/// <summary>
		/// 对字段进行平均值条件统计
		/// </summary>
		/// <param name="expression">条件表达式</param>
		/// <param name="fieldInfo">统计字段</param>
		/// <param name="isDistinct">是否去重复</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Avg (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException ("expression");
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			if (!expression.IgnoreConsistency && !fieldInfo.TableMapping.Equals (expression.TableMapping)) {
				throw new LightDataException (RE.DataMappingIsNotMatchQueryExpression);
			}
			return new ConditionAvgFunction (fieldInfo.TableMapping, expression, fieldInfo, isDistinct);
		}

		/// <summary>
		/// 统计最大值
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Max (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			return new MaxFunction (fieldInfo.TableMapping, fieldInfo);
		}

		/// <summary>
		/// 统计最小值
		/// </summary>
		/// <param name="fieldInfo">统计字段</param>
		/// <returns>函数对象</returns>
		public static AggregateFunction Min (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			return new MinFunction (fieldInfo.TableMapping, fieldInfo);
		}

		/// <summary>
		/// 匹配对象是否相等
		/// </summary>
		/// <param name="target">匹配对象</param>
		/// <returns></returns>
		public virtual bool Equals (AggregateFunction target)
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
		/// 匹配对象细节是否相等
		/// </summary>
		/// <param name="function">匹配对象</param>
		/// <returns></returns>
		protected virtual bool EqualsDetail (AggregateFunction function)
		{
			return Object.Equals (this.TableMapping, function.TableMapping);
		}

		/// <summary>
		/// 匹配对象是否相等
		/// </summary>
		/// <param name="obj">匹配对象</param>
		/// <returns></returns>
		public override bool Equals (object obj)
		{
			return base.Equals (obj);
		}

		/// <summary>
		/// 获取哈希码
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}
