using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	public class LightAggregate<T, K> : IAggregate<K>
		where T : class, new()
		where K : class
	{
		AggregateGroup _model;

		QueryExpression _query;

		//internal QueryExpression Query {
		//	get {
		//		return _query;
		//	}
		//}

		QueryExpression _having;

		//internal QueryExpression Having {
		//	get {
		//		return _having;
		//	}
		//}

		OrderExpression _order;

		//internal OrderExpression Order {
		//	get {
		//		return _order;
		//	}
		//}

		DataContext _context;

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

		DataEntityMapping _mapping;

		public LightAggregate (LightQuery<T> query, Expression<Func<T, K>> expression)
		{
			_context = query.Context;
			_query = query.Query;
			_level = query.Level;
			_model = LambdaExpressionExtend.CreateAggregateGroup (expression);
			_mapping = _model.EntityMapping;
		}

		public IAggregate<K> Having (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = queryExpression;
			return this;
		}

		public IAggregate<K> HavingReset ()
		{
			_having = null;
			return this;
		}

		public IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = QueryExpression.And (_having, queryExpression);
			return this;
		}

		public IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaHavingExpression (expression, _model);
			_having = QueryExpression.Or (_having, queryExpression);
			return this;
		}

		public IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC, _model);
			_order = orderExpression;
			return this;
		}

		public IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC, _model);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC, _model);
			_order = orderExpression;
			return this;
		}

		public IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC, _model);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public IAggregate<K> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		public IAggregate<K> OrderByReset ()
		{
			_order = null;
			return this;
		}

		public List<K> ToList ()
		{
			List<K> list = new List<K> ();
			IEnumerable ie = _context.QueryDynamicAggregateEnumerable (_model, _query, _having, _order, _level);
			foreach (K item in ie) {
				list.Add (item);
			}
			return list;
		}


	}
}

