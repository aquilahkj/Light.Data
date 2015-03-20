using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 枚举查询器
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LEnumerable<T> : IEnumerable where T : class, new()
	{
		DataEntityMapping _mapping = null;

		QueryExpression _query = null;

		OrderExpression _order = null;

		Region _region = null;

		DataContext _context = null;

		SafeLevel _level = SafeLevel.None;

		internal LEnumerable (DataContext dataContext)
		{
			_context = dataContext;
			_mapping = DataMapping.GetEntityMapping (typeof(T));
		}

		#region IEnumerable 成员

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _context.QueryDataEnumerable (_mapping, _query, _order, _region, _level).GetEnumerator ();
		}

		#endregion

		#region LEnumerable<T> 成员

		/// <summary>
		/// 重置条件语句
		/// </summary>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Reset ()
		{
			_query = null;
			_order = null;
			_region = null;
			_level = SafeLevel.Default;
			return this;
		}

		/// <summary>
		/// 添加查询表达式
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Where (QueryExpression expression)
		{
			Where (expression, CatchOperatorsType.AND);
			return this;
		}

		/// <summary>
		/// 添加查询表达式
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <param name="catchType">连接操作符类型</param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Where (QueryExpression expression, CatchOperatorsType catchType)
		{
			if (catchType == CatchOperatorsType.AND) {
				_query = QueryExpression.And (_query, expression);
			}
			else {
				_query = QueryExpression.Or (_query, expression);
			}
			return this;
		}

		/// <summary>
		/// 添加排序表达式
		/// </summary>
		/// <param name="expression"></param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> OrderBy (OrderExpression expression)
		{
			_order &= expression;
			return this;
		}

		/// <summary>
		/// 获取的数据行数
		/// </summary>
		/// <param name="count"></param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Take (int count)
		{
			if (_region == null) {
				_region = new Region (0, count);
			}
			else {
				_region.Size = count;
			}
			return this;
		}

		/// <summary>
		///  需要跳过的数据行数
		/// </summary>
		/// <param name="index"></param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Skip (int index)
		{
			if (_region == null) {
				_region = new Region (index, int.MaxValue);
			}
			else {
				_region.Start = index;
			}
			return this;

		}

		/// <summary>
		/// 取数据的范围
		/// </summary>
		/// <param name="from">开始序号</param>
		/// <param name="to">结束序号</param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			if (_region == null) {
				_region = new Region (start, size);
			}
			else {
				_region.Start = start;
				_region.Size = size;
			}
			return this;
		}

		/// <summary>
		/// 分页取数据范围
		/// </summary>
		/// <param name="page">页数</param>
		/// <param name="size">每页数量</param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> PageSize (int page, int size)
		{
			int start = page * size;
			if (_region == null) {
				_region = new Region (start, size);
			}
			else {
				_region.Start = start;
				_region.Size = size;
			}
			return this;
		}

		/// <summary>
		/// 安全模式
		/// </summary>
		/// <param name="level">数据锁类型</param>
		/// <returns> 枚举查询器</returns>
		public LEnumerable<T> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}


		/// <summary>
		/// 数据集数量
		/// </summary>
		public int Count {
			get {
				return Convert.ToInt32 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		/// <summary>
		/// 长整形数据集数量
		/// </summary>
		public long LongCount {
			get {
				return Convert.ToInt64 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		private object Aggregate (BasicFieldInfo field, AggregateType aggregateType, bool isDistinct)
		{
			if (!_mapping.Equals (field.TableMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.Aggregate (field.DataField, aggregateType, _query, isDistinct, _level);
		}

		/// <summary>
		/// 聚合统计改字段的数量
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object CountField (BasicFieldInfo field)
		{
			return CountField (field, false);
		}

		/// <summary>
		/// 聚合统计改字段的数量
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns></returns>
		public object CountField (BasicFieldInfo field, bool isDistinct)
		{
			return Aggregate (field, AggregateType.COUNT, isDistinct);
		}

		/// <summary>
		/// 聚合统计该字段的最大值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Max (BasicFieldInfo field)
		{
			return Max (field, false);
		}

		/// <summary>
		/// 聚合统计该字段的最大值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>结果</returns>
		public object Max (BasicFieldInfo field, bool isDistinct)
		{
			return Aggregate (field, AggregateType.MAX, isDistinct);
		}

		/// <summary>
		/// 聚合统计该字段的最小值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Min (BasicFieldInfo field)
		{
			return Min (field, false);
		}

		/// <summary>
		/// 聚合统计该字段的最小值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>结果</returns>
		public object Min (BasicFieldInfo field, bool isDistinct)
		{
			return Aggregate (field, AggregateType.MIN, isDistinct);
		}

		/// <summary>
		/// 聚合统计该字段的平均值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Avg (BasicFieldInfo field)
		{
			return Avg (field, false);
		}

		/// <summary>
		/// 聚合统计该字段的平均值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>结果</returns>
		public object Avg (BasicFieldInfo field, bool isDistinct)
		{
			return Aggregate (field, AggregateType.AVG, isDistinct);
		}

		/// <summary>
		/// 聚合统计该字段的总和
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Sum (BasicFieldInfo field)
		{
			return Sum (field, false);
		}

		/// <summary>
		/// 聚合统计该字段的总和
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>结果</returns>
		public object Sum (BasicFieldInfo field, bool isDistinct)
		{
			return Aggregate (field, AggregateType.SUM, isDistinct);
		}

		/// <summary>
		/// 获取首条记录
		/// </summary>
		/// <returns>数据对象</returns>
		public T Single ()
		{
			return _context.SelectSingle (_mapping, _query, _order, 0, _level) as T;
		}

		/// <summary>
		/// 获取索引的某条记录
		/// </summary>
		/// <param name="index">索引值</param>
		/// <returns></returns>
		public T ElementAt (int index)
		{
			return _context.SelectSingle (_mapping, _query, _order, index, _level) as T;
		}

		/// <summary>
		/// 批量删除
		/// </summary>
		/// <returns>受影响数据</returns>
		public int DeleteMass ()
		{
			DataTableEntityMapping dtMapping = _mapping as DataTableEntityMapping;
			if (dtMapping == null) {
				throw new LightDataException (RE.TheDataMappingNotAllowDeleteMass);
			}
			return _context.DeleteMass (dtMapping, _query);
		}

		/// <summary>
		/// 批量更新
		/// </summary>
		/// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
		/// <returns>受影响数据</returns>
		public int UpdateMass (UpdateSetValue[] updates)
		{
			DataTableEntityMapping dtMapping = _mapping as DataTableEntityMapping;
			if (dtMapping == null) {
				throw new LightDataException (RE.TheDataMappingNotAllowDeleteMass);
			}
			return _context.UpdateMass (dtMapping, updates, _query);
		}


		/// <summary>
		/// 是否存在
		/// </summary>
		public bool Exists {
			get {
				return _context.Exists (_mapping, _query, _level);
			}
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <param name="fieldInfo">字段</param>
		/// <returns>数据枚举</returns>
		public IEnumerable QuerySingleField (DataFieldInfo fieldInfo)
		{
			return QuerySingleField (fieldInfo, false);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>数据枚举</returns>
		public IEnumerable QuerySingleField (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeEnumerable (fieldInfo, _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <param name="fieldInfo">字段</param>
		/// <returns>数据枚举</returns>
		public IEnumerable QuerySingleFieldList (DataFieldInfo fieldInfo)
		{
			return QuerySingleFieldList (fieldInfo, false);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <typeparam name="K">输出字段类型,必须为原始数据类型</typeparam>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isNullable">是否可空</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns></returns>
		public IEnumerable QuerySingleField<K> (DataFieldInfo fieldInfo, bool isNullable, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeEnumerable (fieldInfo, typeof(K), isNullable, _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>数据集合</returns>
		public IList QuerySingleFieldList (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeList (fieldInfo, _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <typeparam name="K">输出字段类型,必须为原始数据类型</typeparam>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isNullable">是否可空</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>数据集合</returns>
		public IList QuerySingleFieldList<K> (DataFieldInfo fieldInfo, bool isNullable, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeList (fieldInfo, typeof(K), isNullable, _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// 转换为集合
		/// </summary>
		/// <returns>泛型集合</returns>
		public List<T> ToList ()
		{
			return _context.QueryDataList (_mapping, _query, _order, _region, _level) as List<T>;
		}

		/// <summary>
		/// 转换为数组
		/// </summary>
		/// <returns>泛型数组</returns>
		public T[] ToArray ()
		{
			return ToList ().ToArray ();
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return GetDbCommand ().CommandText;
		}

		/// <summary>
		/// 生成命令
		/// </summary>
		/// <returns>命令接口</returns>
		public IDbCommand GetDbCommand ()
		{
			return _context.DataBase.Factory.CreateSelectCommand (_mapping, _query, _order, _context.IsInnerPager ? _region : null);
		}
	}
}
