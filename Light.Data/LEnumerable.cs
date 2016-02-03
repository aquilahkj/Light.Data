using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace Light.Data
{
	/// <summary>
	/// 枚举查询器
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LEnumerable<T> : IEnumerable where T : class, new()
	{
		DataEntityMapping _mapping = null;

		internal DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		QueryExpression _query = null;

		internal QueryExpression Query {
			get {
				return _query;
			}
		}

		OrderExpression _order = null;

		internal OrderExpression Order {
			get {
				return _order;
			}
		}

		Region _region = null;

		DataContext _context = null;

		SafeLevel _level = SafeLevel.None;

		internal LEnumerable (DataContext dataContext)
		{
			_context = dataContext;
			_mapping = DataMapping.GetEntityMapping (typeof(T));
		}

		#region IEnumerable 成员

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _context.QueryDataEnumerable (_mapping, _query, _order, _region, _level).GetEnumerator ();
		}

		#endregion

		#region LEnumerable<T> 成员

		//		/// <summary>
		//		/// 重置条件语句
		//		/// </summary>
		//		/// <returns> 枚举查询器</returns>
		//		public LEnumerable<T> Reset ()
		//		{
		//			_query = null;
		//			_order = null;
		//			_region = null;
		//			_level = SafeLevel.Default;
		//			return this;
		//		}

		/// <summary>
		/// reset where expression.
		/// </summary>
		/// <returns>The reset.</returns>
		public LEnumerable<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// replace where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public LEnumerable<T> Where (QueryExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_query = expression;
			return this;
		}

		/// <summary>
		/// and catch where expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public LEnumerable<T> WhereWithAnd (QueryExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_query = QueryExpression.And (_query, expression);
			return this;
		}

		/// <summary>
		/// or catch where expression.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		public LEnumerable<T> WhereWithOr (QueryExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_query = QueryExpression.Or (_query, expression);
			return this;
		}

		/// <summary>
		/// catch order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public LEnumerable<T> OrderByCatch (OrderExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_order = OrderExpression.Catch (_order, expression);
			return this;
		}

		/// <summary>
		/// replace order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public LEnumerable<T> OrderBy (OrderExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_order = expression;
			return this;
		}

		/// <summary>
		/// reset order by expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public LEnumerable<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public LEnumerable<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataMapping.GetEntityMapping (typeof(T)));
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
		/// reset the range
		/// </summary>
		/// <returns>The reset.</returns>
		public LEnumerable<T> RangeReset ()
		{
			_region = null;
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
			if (page < 1) {
				throw new ArgumentOutOfRangeException ("page");
			}
			if (size < 1) {
				throw new ArgumentOutOfRangeException ("size");
			}
			page--;
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

		//		/// <summary>
		//		/// 长整形数据集数量
		//		/// </summary>
		//		public long LongCount {
		//			get {
		//				return Convert.ToInt64 (_context.AggregateCount (_mapping, _query, _level));
		//			}
		//		}
		//
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
		public int CountField (BasicFieldInfo field)
		{
			return CountField (field, false);
		}

		/// <summary>
		/// 聚合统计改字段的数量
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns></returns>
		public int CountField (BasicFieldInfo field, bool isDistinct)
		{
			return Convert.ToInt32 (Aggregate (field, AggregateType.COUNT, isDistinct));
		}

		/// <summary>
		/// 聚合统计该字段的最大值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Max (BasicFieldInfo field)
		{
//			return Max (field, false);
			return Aggregate (field, AggregateType.MAX, false);
		}

		//		/// <summary>
		//		/// 聚合统计该字段的最大值
		//		/// </summary>
		//		/// <param name="field">统计字段</param>
		//		/// <param name="isDistinct">是否去重</param>
		//		/// <returns>结果</returns>
		//		public object Max (BasicFieldInfo field, bool isDistinct)
		//		{
		//			return Aggregate (field, AggregateType.MAX, isDistinct);
		//		}

		/// <summary>
		/// 聚合统计该字段的最小值
		/// </summary>
		/// <param name="field">统计字段</param>
		/// <returns>结果</returns>
		public object Min (BasicFieldInfo field)
		{
//			return Min (field, false);
			return Aggregate (field, AggregateType.MIN, false);
		}

		//		/// <summary>
		//		/// 聚合统计该字段的最小值
		//		/// </summary>
		//		/// <param name="field">统计字段</param>
		//		/// <param name="isDistinct">是否去重</param>
		//		/// <returns>结果</returns>
		//		public object Min (BasicFieldInfo field, bool isDistinct)
		//		{
		//			return Aggregate (field, AggregateType.MIN, isDistinct);
		//		}

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

//		/// <summary>
//		/// 批量删除
//		/// </summary>
//		/// <returns>受影响数据</returns>
//		public int DeleteMass ()
//		{
//			DataTableEntityMapping dtMapping = _mapping as DataTableEntityMapping;
//			if (dtMapping == null) {
//				throw new LightDataException (RE.TheDataMappingNotAllowDeleteMass);
//			}
//			return _context.DeleteMass (dtMapping, _query);
//		}
//
//		/// <summary>
//		/// 批量更新
//		/// </summary>
//		/// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
//		/// <returns>受影响数据</returns>
//		public int UpdateMass (UpdateSetValue[] updates)
//		{
//			DataTableEntityMapping dtMapping = _mapping as DataTableEntityMapping;
//			if (dtMapping == null) {
//				throw new LightDataException (RE.TheDataMappingNotAllowDeleteMass);
//			}
//			return _context.UpdateMass (dtMapping, updates, _query);
//		}
//

		/// <summary>
		/// 是否存在
		/// </summary>
		public bool Exists {
			get {
				return _context.Exists (_mapping, _query, _level);
			}
		}

		//		/// <summary>
		//		/// 查询单列字段的数据
		//		/// </summary>
		//		/// <param name="fieldInfo">字段</param>
		//		/// <returns>数据枚举</returns>
		//		public IEnumerable QuerySingleField (DataFieldInfo fieldInfo)
		//		{
		//			return QuerySingleField (fieldInfo, false);
		//		}
		//
		//		/// <summary>
		//		/// 查询单列字段的数据
		//		/// </summary>
		//		/// <param name="fieldInfo">字段</param>
		//		/// <param name="isDistinct">是否去重</param>
		//		/// <returns>数据枚举</returns>
		//		public IEnumerable QuerySingleField (DataFieldInfo fieldInfo, bool isDistinct)
		//		{
		//			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
		//				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
		//			}
		//			return _context.QueryColumeEnumerable (fieldInfo, _query, _order, _region, isDistinct, _level);
		//		}
		//


		//		/// <summary>
		//		/// 查询单列字段的数据
		//		/// </summary>
		//		/// <param name="fieldInfo">字段</param>
		//		/// <returns>数据枚举</returns>
		//		public IEnumerable QuerySingleFieldList (DataFieldInfo fieldInfo)
		//		{
		//			return QuerySingleFieldList (fieldInfo, false);
		//		}

		/// <summary>
		/// Queries the single field.
		/// </summary>
		/// <returns>The single field.</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public IEnumerable QuerySingleField<K> (DataFieldInfo fieldInfo)
		{
			return QuerySingleField<K> (fieldInfo, false);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <typeparam name="K">输出字段类型,必须为原始数据类型</typeparam>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns></returns>
		public IEnumerable QuerySingleField<K> (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeEnumerable (fieldInfo, typeof(K), _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <typeparam name="K">输出字段类型,必须为原始数据类型</typeparam>
		/// <param name="fieldInfo">字段</param>
		/// <returns>数据集合</returns>
		public List<K> QuerySingleFieldList<K> (DataFieldInfo fieldInfo)
		{
			return QuerySingleFieldList<K> (fieldInfo, false);
		}

		/// <summary>
		/// 查询单列字段的数据
		/// </summary>
		/// <typeparam name="K">输出字段类型,必须为原始数据类型</typeparam>
		/// <param name="fieldInfo">字段</param>
		/// <param name="isDistinct">是否去重</param>
		/// <returns>数据集合</returns>
		public List<K> QuerySingleFieldList<K> (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.QueryColumeList<K> (fieldInfo, _query, _order, _region, isDistinct, _level);
		}

		/// <summary>
		/// Queries the single field array.
		/// </summary>
		/// <returns>The single field array.</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public K[] QuerySingleFieldArray<K> (DataFieldInfo fieldInfo)
		{
			return QuerySingleFieldList<K> (fieldInfo, true).ToArray ();
		}

		/// <summary>
		/// Queries the single field array.
		/// </summary>
		/// <returns>The single field array.</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public K[] QuerySingleFieldArray<K> (DataFieldInfo fieldInfo, bool isDistinct)
		{
			return QuerySingleFieldList<K> (fieldInfo, isDistinct).ToArray ();
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
			CommandData commandData = _context.DataBase.Factory.CreateSelectCommand (_mapping, _query, _order, _context.IsInnerPager ? _region : null);
			return commandData.CreateCommand (_context.DataBase);
		}

		/// <summary>
		/// Join the specified le and on.
		/// </summary>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, le, on);
		}

		/// <summary>
		/// Join the specified query and on.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, le, on);
		}

		/// <summary>
		/// Join the specified query.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (QueryExpression query) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, le, null);
		}

		/// <summary>
		/// Join the specified le.
		/// </summary>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, le, null);
		}

		/// <summary>
		/// Join the specified on.
		/// </summary>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (DataFieldExpression on) where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, null, on);
		}

		/// <summary>
		/// Join this instance.
		/// </summary>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> () where K : class, new()
		{
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.InnerJoin, this, null, null);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, le, on);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, le, on);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (QueryExpression query) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, le, null);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (LEnumerable<K> le) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, le, null);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (DataFieldExpression on) where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, null, on);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> () where K : class, new()
		{
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.LeftJoin, this, null, null);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, le, on);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, le, on);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le) where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, le, null);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (QueryExpression query) where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, le, null);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (DataFieldExpression on) where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, null, on);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> () where K : class, new()
		{
			return JoinTable.CreateJoinTable<T,K> (this._context, JoinType.RightJoin, this, null, null);
		}

		/// <summary>
		/// Creates the insertor.
		/// </summary>
		/// <returns>The insertor.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public SelectInsertor CreateInsertor<K> ()
		{
			return new SelectInsertor (this._context, typeof(K), typeof(T), this._query, this._order);
		}

		/// <summary>
		/// Insert the specified selectInfos.
		/// </summary>
		/// <param name="selectInfos">Select infos.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public int Insert<K> (params SelectFieldInfo[] selectInfos)
		{
			SelectInsertor insertor = new SelectInsertor (this._context, typeof(K), typeof(T), this._query, this._order);
			if (selectInfos != null && selectInfos.Length > 0) {
				insertor.SetSelectField (selectInfos);
			}
			return insertor.Execute ();
		}

		/// <summary>
		/// Insert this instance.
		/// </summary>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public int Insert<K> ()
		{
			return Insert<K> (null);
		}

		/// <summary>
		/// Update the specified updates.
		/// </summary>
		/// <param name="updates">Updates.</param>
		public int Update (UpdateSetValue[] updates)
		{
			return _context.UpdateMass<T> (updates, this._query);
		}

		/// <summary>
		/// Delete this instance.
		/// </summary>
		public int Delete()
		{
			return _context.DeleteMass<T> (this._query);
		}
	}
}
