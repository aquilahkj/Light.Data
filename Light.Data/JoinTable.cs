using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// Join table.
	/// </summary>
	public class JoinTable
	{
		internal static JoinTable CreateJoinTable<T,K> (DataContext dataContext, JoinType joinType, LEnumerable<T> left, LEnumerable<K> right, DataFieldExpression on)
			where T : class, new() 
			where K : class, new()
		{
			JoinTable table = new JoinTable (dataContext);
			JoinConnect connect = new JoinConnect (joinType, on);

			JoinModel model1;
			if (left != null) {
				model1 = new JoinModel (left.Mapping, null, left.Query, left.Order);
			}
			else {
				model1 = new JoinModel (DataMapping.GetEntityMapping (typeof(T)), null, null, null);
			}
			JoinModel model2;
			if (right != null) {
				model2 = new JoinModel (right.Mapping, null, right.Query, right.Order);
			}
			else {
				model2 = new JoinModel (DataMapping.GetEntityMapping (typeof(K)), connect, null, null);
			}

			if (model1.Mapping.Equals (model2.Mapping)) {
				throw new LightDataException (RE.CanNotJoinTheSameTable);
			}

//			model2 = new JoinModel (right.Mapping, connect, right.Query, right.Order);
			table._modelList.Add (model1);
			table._modelList.Add (model2);
			return table;
		}

		internal JoinTable (DataContext dataContext)
		{
			_context = dataContext;
		}

		JoinSelector _selector = new JoinSelector ();

		QueryExpression _query = null;

		OrderExpression _order = null;

		Region _region = null;

		DataContext _context = null;

		SafeLevel _level = SafeLevel.None;

		List<JoinModel> _modelList = new List<JoinModel> ();

		//		/// <summary>
		//		/// 重置条件语句
		//		/// </summary>
		//		/// <returns> 枚举查询器</returns>
		//		public JoinTable Reset ()
		//		{
		//			_selector = new JoinSelector ();
		//			_query = null;
		//			_order = null;
		//			_region = null;
		//			_level = SafeLevel.Default;
		//			return this;
		//		}

		/// <summary>
		/// Join the specified le and on.
		/// </summary>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.InnerJoin, le, on);
		}

		/// <summary>
		/// Join the specified on.
		/// </summary>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.InnerJoin, null, on);
		}


		/// <summary>
		/// Join the specified le.
		/// </summary>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.InnerJoin, le, null);
		}

		/// <summary>
		/// Join this instance.
		/// </summary>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.InnerJoin, null, null);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		/// <param name = "on"></param>
		public JoinTable LeftJoin<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.LeftJoin, le, on);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.LeftJoin, null, on);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.LeftJoin, le, null);
		}

		/// <summary>
		/// Lefts the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.LeftJoin, null, null);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.RightJoin, le, on);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.RightJoin, null, on);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.RightJoin, le, null);
		}

		/// <summary>
		/// Rights the join.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.RightJoin, null, null);
		}

		private JoinTable InternalJoin<K> (JoinType joinType, LEnumerable<K> right, DataFieldExpression on)where K : class, new()
		{
			JoinConnect connect = new JoinConnect (joinType, on);
			JoinModel model2;
			if (right != null) {
				model2 = new JoinModel (right.Mapping, null, right.Query, right.Order);
			}
			else {
				model2 = new JoinModel (DataMapping.GetEntityMapping (typeof(K)), connect, null, null);
			}
			foreach (JoinModel model in this._modelList) {
				if (model.Mapping.Equals (model2.Mapping)) {
					throw new LightDataException (RE.CanNotJoinTheSameTable);
				}
			}

			this._modelList.Add (model2);
			return this;
		}

		/// <summary>
		/// Raises the On.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public JoinTable On (DataFieldExpression expression)
		{
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = DataFieldExpression.And (connect.On, expression);
			return this;
		}

		/// <summary>
		/// Raises the On.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public JoinTable OnWithOr (DataFieldExpression expression)
		{
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = DataFieldExpression.Or (connect.On, expression);
			return this;
		}

		/// <summary>
		/// Select the specified fields.
		/// </summary>
		/// <param name="fields">Fields.</param>
		public JoinTable Select (params DataFieldInfo[] fields)
		{
			foreach (DataFieldInfo field in fields) {
				JoinModel m = null;
				foreach (JoinModel model in _modelList) {
					if (model.Mapping.Equals (field.TableMapping)) {
						m = model;
						break;
					}
				}
				if (m != null) {
					_selector.SetDataField (field);
				}
				else {
					throw new LightDataException (RE.SelectFiledsNotInJoinTables);
				}
			}
			return this;
		}

		/// <summary>
		/// Selects the alias.
		/// </summary>
		/// <returns>The alias.</returns>
		/// <param name="field">Field.</param>
		/// <param name="alias">Alias.</param>
		public JoinTable SelectAlias (DataFieldInfo field, string alias)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException ("field");
			if (string.IsNullOrEmpty (alias))
				throw new ArgumentNullException ("alias");
			JoinModel m = null;
			foreach (JoinModel model in _modelList) {
				if (model.Mapping.Equals (field.TableMapping)) {
					m = model;
					break;
				}
			}
			if (m != null) {
				AliasDataFieldInfo aliasField = new AliasDataFieldInfo (field, alias);
				_selector.SetAliasDataField (aliasField);
			}
			else {
				throw new LightDataException (RE.SelectFiledsNotInJoinTables);
			}

			return this;
		}

		/// <summary>
		/// Selects all.
		/// </summary>
		/// <returns>The all.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable SelectAll<K> ()
		{
			DataEntityMapping mapping = DataMapping.GetEntityMapping (typeof(K));
			JoinModel m = null;
			foreach (JoinModel model in _modelList) {
				if (model.Mapping.Equals (mapping)) {
					m = model;
					break;
				}
			}
			if (m != null) {
				_selector.SetDataEntity (mapping);
			}
			else {
				throw new LightDataException (RE.SelectFiledsNotInJoinTables);
			}
			return this;
		}


		/// <summary>
		/// 添加查询表达式
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <returns> 枚举查询器</returns>
		public JoinTable Where (QueryExpression expression)
		{
			_query &= expression;
			return this;
		}

		/// <summary>
		/// 添加查询表达式
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <returns> 枚举查询器</returns>
		public JoinTable WhereWithOr (QueryExpression expression)
		{
			_query |= expression;
			return this;
		}

		//		/// <summary>
		//		/// 添加查询表达式
		//		/// </summary>
		//		/// <param name="expression">查询表达式</param>
		//		/// <param name="catchType">连接操作符类型</param>
		//		/// <returns> 枚举查询器</returns>
		//		public JoinTable Where (QueryExpression expression, CatchOperatorsType catchType)
		//		{
		//			if (catchType == CatchOperatorsType.AND) {
		//				_query = QueryExpression.And (_query, expression);
		//			}
		//			else {
		//				_query = QueryExpression.Or (_query, expression);
		//			}
		//			return this;
		//		}



		/// <summary>
		/// 添加排序表达式
		/// </summary>
		/// <param name="expression"></param>
		/// <returns> 枚举查询器</returns>
		public JoinTable OrderBy (OrderExpression expression)
		{
			_order &= expression;
			return this;
		}

		/// <summary>
		/// Orders the by random.
		/// </summary>
		/// <returns>The by random.</returns>
		public JoinTable OrderByRandom ()
		{
			_order = new RandomOrderExpression (this._modelList [0].Mapping);
			return this;
		}

		/// <summary>
		/// 获取的数据行数
		/// </summary>
		/// <param name="count"></param>
		/// <returns> 枚举查询器</returns>
		public JoinTable Take (int count)
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
		public JoinTable Skip (int index)
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
		public JoinTable Range (int from, int to)
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
		public JoinTable PageSize (int page, int size)
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
		public JoinTable SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}


		/// <summary>
		/// 数据集数量
		/// </summary>
		public int Count {
			get {
				return Convert.ToInt32 (_context.AggregateJoinTableCount (_modelList, _query, _level));
			}
		}

		/// <summary>
		/// 长整形数据集数量
		/// </summary>
		public long LongCount {
			get {
				return Convert.ToInt64 (_context.AggregateJoinTableCount (_modelList, _query, _level));
			}
		}

		/// <summary>
		/// 生成统计表的类型集合
		/// </summary>
		/// <typeparam name="K">生成类型</typeparam>
		/// <returns>类型集合</returns>
		public List<K> ToList<K> () where K : class, new()
		{
			DataMapping mapping = DataMapping.GetMapping (typeof(K));
			List<K> list = _context.QueryJoinDataList (mapping, _selector, _modelList, _query, _order, _region, _level) as List<K>;
			return list;
		}

		/// <summary>
		/// 转换为数组
		/// </summary>
		/// <returns>泛型数组</returns>
		public K[] ToArray<K> () where K : class, new()
		{
			return ToList<K> ().ToArray ();
		}
	}
}

