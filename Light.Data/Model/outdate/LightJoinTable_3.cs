//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;

//namespace Light.Data
//{
//	class LightJoinTable<T, T1, T2, T3> : IJoinTable<T, T1, T2, T3>
//	   where T : class
//	   where T1 : class
//	   where T2 : class
//	   where T3 : class
//	{
//		QueryExpression _query;

//		internal QueryExpression Query {
//			get {
//				return _query;
//			}
//		}

//		OrderExpression _order;

//		internal OrderExpression Order {
//			get {
//				return _order;
//			}
//		}

//		Region _region;

//		internal Region Region {
//			get {
//				return _region;
//			}
//		}

//		readonly DataContext _context;

//		internal DataContext Context {
//			get {
//				return _context;
//			}
//		}

//		SafeLevel _level = SafeLevel.None;

//		internal SafeLevel Level {
//			get {
//				return _level;
//			}
//		}

//		bool _distinct;

//		internal bool Distinct {
//			get {
//				return _distinct;
//			}
//		}

//		readonly List<IJoinModel> _modelList = new List<IJoinModel> ();

//		internal List<IJoinModel> ModelList {
//			get {
//				return _modelList;
//			}
//		}

//		readonly List<IMap> _maps = new List<IMap> ();

//		internal List<IMap> Maps {
//			get {
//				return _maps;
//			}
//		}

//		//internal LightJoinTable (LightJoinTable<T, T1, T2> query1, JoinType joinType, Expression<Func<T3, bool>> queryExpression, Expression<Func<T, T1, T2, T3, bool>> onExpression)
//		//{
//		//	_query = query1.Query;
//		//	_order = query1.Order;
//		//	_region = query1.Region;
//		//	_context = query1.Context;
//		//	_level = query1.Level;
//		//	_distinct = query1.Distinct;
//		//	_modelList.AddRange (query1.ModelList);
//		//	_maps.AddRange (query1.Maps);
//		//	DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (typeof (T3));
//		//	_maps.Add (entityMapping.GetRelationMap ());
//		//	QueryExpression subQuery;
//		//	DataFieldExpression on;
//		//	if (queryExpression != null) {
//		//		subQuery = LambdaExpressionExtend.ResolveLambdaQueryExpression (queryExpression);
//		//	}
//		//	else {
//		//		subQuery = null;
//		//	}
//		//	if (onExpression != null) {
//		//		on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
//		//	}
//		//	else {
//		//		throw new LightDataException (RE.OnExpressionNotExists);
//		//	}

//		//	JoinConnect connect = new JoinConnect (joinType, on);
//		//	EntityJoinModel model = new EntityJoinModel (entityMapping, "T3", connect, subQuery, null);
//		//	_modelList.Add (model);
//		//}

//		internal LightJoinTable (LightJoinTable<T, T1, T2> left, JoinType joinType, QueryBase<T3> right, Expression<Func<T, T1, T2, T3, bool>> onExpression)
//		{
//			_query = left.Query;
//			_order = left.Order;
//			_context = left.Context;
//			_modelList.AddRange (left.ModelList);
//			_maps.AddRange (left.Maps);
//			DataEntityMapping entityMapping = right.Mapping;
//			_maps.Add (entityMapping.GetRelationMap ());
//			DataFieldExpression on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
//			JoinConnect connect = new JoinConnect (joinType, on);
//			EntityJoinModel model = new EntityJoinModel (entityMapping, "T3", connect, right.QueryExpression, right.OrderExpression);
//			model.Distinct = right.Distinct;
//			_modelList.Add (model);
//		}

//		internal LightJoinTable (LightJoinTable<T, T1, T2> left, JoinType joinType, AggregateBase<T3> right, Expression<Func<T, T1, T2, T3, bool>> onExpression)
//		{
//			_query = left.Query;
//			_order = left.Order;
//			_context = left.Context;
//			_modelList.AddRange (left.ModelList);
//			_maps.AddRange (left.Maps);
//			_maps.Add (new AggregateMap (right.Model));
//			DataFieldExpression on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
//			JoinConnect connect = new JoinConnect (joinType, on);
//			AggregateJoinModel model = new AggregateJoinModel (right.Model, "T3", connect, right.QueryExpression, right.HavingExpression, right.OrderExpression);
//			_modelList.Add (model);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> Join<T4> (Expression<Func<T4, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			if (queryExpression != null) {
//				lightQuery.Where (queryExpression);
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.InnerJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> Join<T4> (Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.InnerJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> LeftJoin<T4> (Expression<Func<T4, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			if (queryExpression != null) {
//				lightQuery.Where (queryExpression);
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.LeftJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> LeftJoin<T4> (Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.LeftJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> RightJoin<T4> (Expression<Func<T4, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			if (queryExpression != null) {
//				lightQuery.Where (queryExpression);
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.RightJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> RightJoin<T4> (Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			LightQuery<T4> lightQuery = new LightQuery<T4> (_context);
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.RightJoin, lightQuery, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> Join<T4> (IQuery<T4> query, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			QueryBase<T4> queryBase = query as QueryBase<T4>;
//			if (queryBase == null) {
//				throw new ArgumentException (nameof (query));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.InnerJoin, queryBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> LeftJoin<T4> (IQuery<T4> query, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			QueryBase<T4> queryBase = query as QueryBase<T4>;
//			if (queryBase == null) {
//				throw new ArgumentException (nameof (query));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.LeftJoin, queryBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> RightJoin<T4> (IQuery<T4> query, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			QueryBase<T4> queryBase = query as QueryBase<T4>;
//			if (queryBase == null) {
//				throw new ArgumentException (nameof (query));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.RightJoin, queryBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> Join<T4> (IAggregate<T4> aggregate, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			AggregateBase<T4> aggregateBase = aggregate as AggregateBase<T4>;
//			if (aggregateBase == null) {
//				throw new ArgumentException (nameof (aggregate));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.InnerJoin, aggregateBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> LeftJoin<T4> (IAggregate<T4> aggregate, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			AggregateBase<T4> aggregateBase = aggregate as AggregateBase<T4>;
//			if (aggregateBase == null) {
//				throw new ArgumentException (nameof (aggregate));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.LeftJoin, aggregateBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3, T4> RightJoin<T4> (IAggregate<T4> aggregate, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression) where T4 : class
//		{
//			AggregateBase<T4> aggregateBase = aggregate as AggregateBase<T4>;
//			if (aggregateBase == null) {
//				throw new ArgumentException (nameof (aggregate));
//			}
//			return new LightJoinTable<T, T1, T2, T3, T4> (this, JoinType.RightJoin, aggregateBase, onExpression);
//		}

//		public IJoinTable<T, T1, T2, T3> WhereReset ()
//		{
//			_query = null;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> Where (Expression<Func<T, T1, T2, T3, bool>> expression)
//		{
//			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
//			_query = queryExpression;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> WhereWithAnd (Expression<Func<T, T1, T2, T3, bool>> expression)
//		{
//			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
//			_query = QueryExpression.And (_query, queryExpression);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> WhereWithOr (Expression<Func<T, T1, T2, T3, bool>> expression)
//		{
//			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
//			_query = QueryExpression.Or (_query, queryExpression);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> OrderByCatch<TKey> (Expression<Func<T, T1, T2, T3, TKey>> expression)
//		{
//			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.ASC, _maps);
//			_order = OrderExpression.Catch (_order, orderExpression);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, T3, TKey>> expression)
//		{
//			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.DESC, _maps);
//			_order = OrderExpression.Catch (_order, orderExpression);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> OrderBy<TKey> (Expression<Func<T, T1, T2, T3, TKey>> expression)
//		{
//			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.ASC, _maps);
//			_order = orderExpression;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> OrderByDescending<TKey> (Expression<Func<T, T1, T2, T3, TKey>> expression)
//		{
//			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.DESC, _maps);
//			_order = orderExpression;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> OrderByReset ()
//		{
//			_order = null;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> Take (int count)
//		{
//			int start;
//			int size = count;
//			if (_region == null) {
//				start = 0;
//			}
//			else {
//				start = _region.Start;
//			}
//			_region = new Region (start, size);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> Skip (int index)
//		{
//			int start = index;
//			int size;
//			if (_region == null) {
//				size = int.MaxValue;
//			}
//			else {
//				size = _region.Size;
//			}
//			_region = new Region (start, size);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> Range (int from, int to)
//		{
//			int start = from;
//			int size = to - from;
//			_region = new Region (start, size);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> RangeReset ()
//		{
//			_region = null;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> PageSize (int page, int size)
//		{
//			if (page < 1) {
//				throw new ArgumentOutOfRangeException (nameof (page));
//			}
//			if (size < 1) {
//				throw new ArgumentOutOfRangeException (nameof (size));
//			}
//			page--;
//			int start = page * size;
//			_region = new Region (start, size);
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> SafeMode (SafeLevel level)
//		{
//			_level = level;
//			return this;
//		}

//		public IJoinTable<T, T1, T2, T3> SetDistinct (bool distinct)
//		{
//			_distinct = distinct;
//			return this;
//		}

//		public IJoinSelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, T3, TResult>> expression) where TResult : class
//		{
//			LightJoinSelect<TResult> selectable = new LightJoinSelect<TResult> (_context, expression, _modelList, _maps, _query, _order, _distinct, _region, _level);
//			return selectable;
//		}

//		public int SelectInsert<K> (Expression<Func<T, T1, T2, T3, K>> expression) where K : class, new()
//		{
//			InsertSelector selector = LambdaExpressionExtend.CreateMutliInsertSelector (expression, _maps);
//			return this._context.SelectInsertWithJoinTable (selector, _modelList.ToArray (), _query, _order, _distinct, _level);
//		}
//	}
//}

