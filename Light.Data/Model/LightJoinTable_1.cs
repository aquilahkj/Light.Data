using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightJoinTable<T, T1> : IJoinTable<T, T1>
	   where T : class
	   where T1 : class
	{

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

		readonly List<IJoinModel> _modelList = new List<IJoinModel> ();

		internal List<IJoinModel> ModelList {
			get {
				return _modelList;
			}
		}

		readonly List<IMap> _maps = new List<IMap> ();

		internal List<IMap> Maps {
			get {
				return _maps;
			}
		}

		internal LightJoinTable (LightQuery<T> query1, JoinType joinType, Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression)
		{
			_query = null;
			_order = null;
			_region = query1.Region;
			_context = query1.Context;
			_level = query1.Level;
			DataEntityMapping entityMapping1 = DataEntityMapping.GetEntityMapping (typeof (T));
			DataEntityMapping entityMapping2 = DataEntityMapping.GetEntityMapping (typeof (T1));
			_maps.Add (entityMapping1.GetRelationMap ());
			_maps.Add (entityMapping2.GetRelationMap ());
			QueryExpression subQuery;
			DataFieldExpression on;
			if (queryExpression != null) {
				subQuery = LambdaExpressionExtend.ResolveLambdaQueryExpression (queryExpression);
			}
			else {
				subQuery = null;
			}
			if (onExpression != null) {
				on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
			}
			else {
				throw new LightDataException (RE.OnExpressionNotExists);
			}

			JoinConnect connect = new JoinConnect (joinType, on);
			EntityJoinModel model1 = new EntityJoinModel (entityMapping1, "T0", null, query1.Query, query1.Order);
			EntityJoinModel model2 = new EntityJoinModel (entityMapping2, "T1", connect, subQuery, null);
			_modelList.Add (model1);
			_modelList.Add (model2);
		}

		internal LightJoinTable (LightQuery<T> query1, JoinType joinType, IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression)
		{
			_query = null;
			_order = null;
			_region = query1.Region;
			_context = query1.Context;
			_level = query1.Level;
			AggregateGroupData data = aggregate.GetGroupData ();
			DataEntityMapping entityMapping1 = DataEntityMapping.GetEntityMapping (typeof (T));
			_maps.Add (entityMapping1.GetRelationMap ());
			_maps.Add (new AggregateMap (data.Model));
			DataFieldExpression on;

			if (onExpression != null) {
				on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
			}
			else {
				throw new LightDataException (RE.OnExpressionNotExists);
			}

			JoinConnect connect = new JoinConnect (joinType, on);
			EntityJoinModel model1 = new EntityJoinModel (entityMapping1, "T0", null, query1.Query, query1.Order);
			AggregateJoinModel model2 = new AggregateJoinModel (data.Model, "T1", connect, data.Query, data.Having, data.Order);
			_modelList.Add (model1);
			_modelList.Add (model2);
		}

		internal LightJoinTable (IAggregate<T> aggregate, JoinType joinType, Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression)
		{
			AggregateGroupData data = aggregate.GetGroupData ();
			_query = null;
			_order = null;
			_region = data.Region;
			_context = data.Context;
			_level = data.Level;

			DataEntityMapping entityMapping1 = DataEntityMapping.GetEntityMapping (typeof (T1));
			_maps.Add (new AggregateMap (data.Model));
			_maps.Add (entityMapping1.GetRelationMap ());

			QueryExpression subQuery;
			DataFieldExpression on;
			if (queryExpression != null) {
				subQuery = LambdaExpressionExtend.ResolveLambdaQueryExpression (queryExpression);
			}
			else {
				subQuery = null;
			}
			if (onExpression != null) {
				on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
			}
			else {
				throw new LightDataException (RE.OnExpressionNotExists);
			}

			JoinConnect connect = new JoinConnect (joinType, on);
			AggregateJoinModel model = new AggregateJoinModel (data.Model, "T0", null, data.Query, data.Having, data.Order);
			EntityJoinModel model1 = new EntityJoinModel (entityMapping1, "T1", connect, subQuery, null);
			_modelList.Add (model);
			_modelList.Add (model1);
		}

		internal LightJoinTable (IAggregate<T> aggregate, JoinType joinType, IAggregate<T1> aggregate1, Expression<Func<T, T1, bool>> onExpression)
		{
			AggregateGroupData data = aggregate.GetGroupData ();
			AggregateGroupData data1 = aggregate1.GetGroupData ();
			_query = null;
			_order = null;
			_region = data.Region;
			_context = data.Context;
			_level = data.Level;

			_maps.Add (new AggregateMap (data.Model));
			_maps.Add (new AggregateMap (data1.Model));

			DataFieldExpression on;

			if (onExpression != null) {
				on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression, _maps);
			}
			else {
				throw new LightDataException (RE.OnExpressionNotExists);
			}

			JoinConnect connect = new JoinConnect (joinType, on);
			AggregateJoinModel model = new AggregateJoinModel (data.Model, "T0", null, data.Query, data.Having, data.Order);
			AggregateJoinModel model1 = new AggregateJoinModel (data1.Model, "T1", connect, data1.Query, data1.Having, data1.Order);
			_modelList.Add (model);
			_modelList.Add (model1);
		}


		public IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			Expression<Func<T2, bool>> queryExpression = null;
			return new LightJoinTable<T, T1, T2> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			Expression<Func<T2, bool>> queryExpression = null;
			return new LightJoinTable<T, T1, T2> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.RightJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class//, new()
		{
			Expression<Func<T2, bool>> queryExpression = null;
			return new LightJoinTable<T, T1, T2> (this, JoinType.RightJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2> Join<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.InnerJoin, aggregate, onExpression);
		}

		public IJoinTable<T, T1, T2> LeftJoin<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.LeftJoin, aggregate, onExpression);
		}

		public IJoinTable<T, T1, T2> RightJoin<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class
		{
			return new LightJoinTable<T, T1, T2> (this, JoinType.RightJoin, aggregate, onExpression);
		}

		public IJoinTable<T, T1> WhereReset ()
		{
			_query = null;
			return this;
		}

		public IJoinTable<T, T1> Where (Expression<Func<T, T1, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
			_query = queryExpression;
			return this;
		}

		public IJoinTable<T, T1> WhereWithAnd (Expression<Func<T, T1, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
			_query = QueryExpression.And (_query, queryExpression);
			return this;
		}

		public IJoinTable<T, T1> WhereWithOr (Expression<Func<T, T1, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaMutliQueryExpression (expression, _maps);
			_query = QueryExpression.Or (_query, queryExpression);
			return this;
		}

		public IJoinTable<T, T1> OrderByCatch<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.ASC, _maps);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IJoinTable<T, T1> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.DESC, _maps);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IJoinTable<T, T1> OrderBy<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.ASC, _maps);
			_order = orderExpression;
			return this;
		}

		public IJoinTable<T, T1> OrderByDescending<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaMutliOrderByExpression (expression, OrderType.DESC, _maps);
			_order = orderExpression;
			return this;
		}

		public IJoinTable<T, T1> OrderByReset ()
		{
			_order = null;
			return this;
		}

		public IJoinTable<T, T1> Take (int count)
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

		public IJoinTable<T, T1> Skip (int index)
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

		public IJoinTable<T, T1> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		public IJoinTable<T, T1> RangeReset ()
		{
			_region = null;
			return this;
		}

		public IJoinTable<T, T1> PageSize (int page, int size)
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

		public IJoinTable<T, T1> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		public ISelect<TResult> Select<TResult> (Expression<Func<T, T1, TResult>> expression) where TResult : class
		{
			JoinSelector selector = LambdaExpressionExtend.CreateMutliSelector (expression, _maps) as JoinSelector;
			Delegate dele = expression.Compile ();
			LightJoinSelect<TResult> selectable = new LightJoinSelect<TResult> (_context, dele, selector, _modelList.ToArray (), _query, _order, _region, _level);
			return selectable;
		}

		public int SelectInsert<K> (Expression<Func<T, T1, K>> expression) where K : class, new()
		{
			InsertSelector selector = LambdaExpressionExtend.CreateMutliInsertSelector (expression, _maps);
			return this._context.SelectInsertWithJoinTable (selector, _modelList.ToArray (), _query, _order, _level);
		}
	}

}

