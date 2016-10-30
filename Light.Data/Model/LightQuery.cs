using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightQuery<T> : IQuery<T> where T : class
	{
		#region IEnumerable implementation

		public IEnumerator<T> GetEnumerator ()
		{
			foreach (T item in _context.QueryEntityData (_mapping, null, _query, _order, _region, _level)) {
				yield return item;
			}
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _context.QueryEntityData (_mapping, null, _query, _order, _region, _level).GetEnumerator ();
		}

		#endregion

		DataEntityMapping _mapping;

		internal DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		QueryExpression _query;

		internal QueryExpression Query {
			get {
				return _query;
			}
		}

		OrderExpression _order;

		internal OrderExpression Order {
			get {
				return _order;
			}
		}

		Region _region;

		internal Region Region {
			get {
				return _region;
			}
		}

		readonly DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		SafeLevel _level = SafeLevel.None;

		internal SafeLevel Level {
			get {
				return _level;
			}
		}

		internal LightQuery (DataContext dataContext)
		{
			_context = dataContext;
			_mapping = DataEntityMapping.GetEntityMapping (typeof (T));
		}

		#region LQuery<T> 成员

		public IQuery<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		public IQuery<T> Where (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = queryExpression;
			return this;
		}

		public IQuery<T> WhereWithAnd (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.And (_query, queryExpression);
			return this;
		}

		public IQuery<T> WhereWithOr (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.Or (_query, queryExpression);
			return this;
		}

		public IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = orderExpression;
			return this;
		}

		public IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = orderExpression;
			return this;
		}

		public IQuery<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		public IQuery<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		public IQuery<T> Take (int count)
		{
			int start;
			int size = count;
			if (_region == null) {
				start = 0;
			}
			else {
				start = _region.Start;
			}
			_region = new Region (start, size);
			return this;
		}

		public IQuery<T> Skip (int index)
		{
			int start = index;
			int size;
			if (_region == null) {
				size = int.MaxValue;
			}
			else {
				size = _region.Size;
			}
			_region = new Region (start, size);
			return this;
		}

		public IQuery<T> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		public IQuery<T> RangeReset ()
		{
			_region = null;
			return this;
		}

		public IQuery<T> PageSize (int page, int size)
		{
			if (page < 1) {
				throw new ArgumentOutOfRangeException (nameof (page));
			}
			if (size < 1) {
				throw new ArgumentOutOfRangeException (nameof (size));
			}
			page--;
			int start = page * size;
			_region = new Region (start, size);
			return this;
		}

		public IQuery<T> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		#region aggregate function

		public int Count {
			get {
				return Convert.ToInt32 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		#endregion

		public T First ()
		{
			return _context.SelectEntityDataSingle (_mapping, _query, _order, 0, _level) as T;
		}

		public T ElementAt (int index)
		{
			return _context.SelectEntityDataSingle (_mapping, _query, _order, index, _level) as T;
		}

		public bool Exists {
			get {
				return _context.Exists (_mapping, _query, _level);
			}
		}

		public List<T> ToList ()
		{
			List<T> list = new List<T> ();
			IEnumerable ie = _context.QueryEntityData (_mapping, null, _query, _order, _region, _level);
			foreach (T item in ie) {
				list.Add (item);
			}
			return list;
		}

		public T [] ToArray ()
		{
			return ToList ().ToArray ();
		}


		#endregion

		public int Insert<K> () where K : class, new()
		{
			DataTableEntityMapping insertMapping = DataEntityMapping.GetTableMapping (typeof (K));
			return this._context.SelectInsert (insertMapping, _mapping, _query, _order, _level);
		}

		public int SelectInsert<K> (Expression<Func<T, K>> expression) where K : class, new()
		{
			InsertSelector selector = LambdaExpressionExtend.CreateInsertSelector (expression);
			return this._context.SelectInsert (selector, _mapping, _query, _order, _level);
		}

		public int Update (Expression<Func<T, T>> expression)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
			MassUpdator updator = LambdaExpressionExtend.CreateMassUpdator (expression);
			return this._context.Update (mapping, updator, _query, _level);
		}

		public int Delete ()
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
			return _context.Delete (mapping, this._query, _level);
		}

		public ISelect<TResult> Select<TResult> (Expression<Func<T, TResult>> expression) where TResult : class
		{
			ISelector selector = LambdaExpressionExtend.CreateSelector (expression);
			Delegate dele = expression.Compile ();
			LightSelect<TResult> selectable = new LightSelect<TResult> (_context, dele, selector, typeof (T), _query, _order, _region, _level);
			return selectable;
		}

		public IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			Expression<Func<T1, bool>> queryExpression = null;
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			Expression<Func<T1, bool>> queryExpression = null;
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			Expression<Func<T1, bool>> queryExpression = null;
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, queryExpression, onExpression);
		}

		public IAggregate<K> GroupBy<K> (Expression<Func<T, K>> expression) where K : class
		{
			return new LightAggregate<T, K> (this, expression);
		}

		public IJoinTable<T, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new();
		{
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, aggregate, onExpression);
		}

		public IJoinTable<T, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, aggregate, onExpression);
		}

		public IJoinTable<T, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class//, new();
		{
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, aggregate, onExpression);
		}

		#region single field
		public IEnumerable<K> QuerySingleField<K> (Expression<Func<T, K>> expression, bool isDistinct = false)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleColume (fieldInfo, typeof (K), _query, _order, _region, isDistinct, _level);
			foreach (K item in ie) {
				yield return item;
			}
		}

		public List<K> QuerySingleFieldList<K> (Expression<Func<T, K>> expression, bool isDistinct = false)
		{
			List<K> list = new List<K> ();
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleColume (fieldInfo, typeof (K), _query, _order, _region, isDistinct, _level);
			foreach (K item in ie) {
				list.Add (item);
			}
			return list;
		}

		#endregion
	}
}

