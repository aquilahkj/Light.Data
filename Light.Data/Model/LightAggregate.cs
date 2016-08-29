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

		Region _region;

		internal Region Region {
			get {
				return _region;
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
			IEnumerable ie = _context.QueryDynamicAggregate (_model, _query, _having, _order, _region, _level);
			foreach (K item in ie) {
				list.Add (item);
			}
			return list;
		}

		public K First ()
		{
			return _context.SelectDynamicAggregateSingle (_model, _query, _having, _order, 0, _level) as K;
		}

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		public IAggregate<K> Take (int count)
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

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		public IAggregate<K> Skip (int index)
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

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public IAggregate<K> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IAggregate<K> RangeReset ()
		{
			_region = null;
			return this;
		}

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		public IAggregate<K> PageSize (int page, int size)
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

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		public IAggregate<K> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}
	}
}

