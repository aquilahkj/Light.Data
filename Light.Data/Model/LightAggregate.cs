using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightAggregate<T, K> : AggregateBase<K>
	   where T : class
	   where K : class
	{
		//AggregateGroup _model;

		//QueryExpression _query;

		//QueryExpression _having;

		//OrderExpression _order;

		//DataContext _context;

		//SafeLevel _level = SafeLevel.None;

		//Region _region;

		public LightAggregate (LightQuery<T> query, Expression<Func<T, K>> expression)
			: base (query.Context, LambdaExpressionExtend.CreateAggregateGroup (expression))
		{
			//_context = query.Context;
			_query = query.QueryExpression;
			_level = query.Level;
			//_model = LambdaExpressionExtend.CreateAggregateGroup (expression);
		}

		public override IAggregate<K> Having (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = queryExpression;
			return this;
		}

		public override IAggregate<K> HavingReset ()
		{
			_having = null;
			return this;
		}

		public override IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = QueryExpression.And (_having, queryExpression);
			return this;
		}

		public override IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = QueryExpression.Or (_having, queryExpression);
			return this;
		}

		public override IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaAggregateOrderByExpression (expression, OrderType.ASC, _model);
			_order = orderExpression;
			return this;
		}

		public override IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaAggregateOrderByExpression (expression, OrderType.ASC, _model);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public override IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaAggregateOrderByExpression (expression, OrderType.DESC, _model);
			_order = orderExpression;
			return this;
		}

		public override IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaAggregateOrderByExpression (expression, OrderType.DESC, _model);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public override IAggregate<K> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		public override IAggregate<K> OrderByReset ()
		{
			_order = null;
			return this;
		}

		public override List<K> ToList ()
		{
			List<K> list = new List<K> ();
			IEnumerable ie = _context.QueryDynamicAggregate (_model, _query, _having, _order, _region, _level);
			foreach (K item in ie) {
				list.Add (item);
			}
			return list;
		}

		public override K First ()
		{
			return _context.SelectDynamicAggregateSingle (_model, _query, _having, _order, 0, _level) as K;
		}

		public override IAggregate<K> Take (int count)
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

		public override IAggregate<K> Skip (int index)
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

		public override IAggregate<K> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		public override IAggregate<K> RangeReset ()
		{
			_region = null;
			return this;
		}

		public override IAggregate<K> PageSize (int page, int size)
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

		public override IAggregate<K> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		//public override AggregateGroupData GetGroupData ()
		//{
		//	AggregateGroupData data = new AggregateGroupData (_context, _model, _query, _having, _order, _region, _level);
		//	return data;
		//}

		public override IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) //where T1 : class
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, aggregateBase, onExpression);
		}
	}
}

