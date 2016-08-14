using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	public class LightJoinTable<T, T1, T2, T3, T4> : IJoinTable<T, T1, T2, T3, T4>
		where T : class, new()
		where T1 : class, new()
		where T2 : class, new()
		where T3 : class, new()
		where T4 : class, new()
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

		List<JoinModel> _modelList = new List<JoinModel> ();

		internal List<JoinModel> ModelList {
			get {
				return _modelList;
			}
		}

		internal LightJoinTable (LightJoinTable<T, T1, T2, T3> query1, JoinType joinType, Expression<Func<T4, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, bool>> onExpression)
		{
			_query = query1.Query;
			_order = query1.Order;
			_region = query1.Region;
			_context = query1.Context;
			_level = query1.Level;
			_modelList.AddRange (query1.ModelList);
			QueryExpression subQuery;
			DataFieldExpression on;
			if (queryExpression != null) {
				subQuery = LambdaExpressionExtend.ResolveLambdaQueryExpression (queryExpression);
			}
			else {
				subQuery = null;
			}
			if (onExpression != null) {
				on = LambdaExpressionExtend.ResolvelambdaOnExpression (onExpression);
			}
			else {
				throw new LightDataException ("");
			}

			JoinConnect connect = new JoinConnect (joinType, on);
			JoinModel model = new JoinModel (DataEntityMapping.GetEntityMapping (typeof (T4)), "T4", connect, subQuery, null);
			_modelList.Add (model);
		}

		public IJoinTable<T, T1, T2, T3, T4, T5> Join<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2, T3, T4, T5> Join<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.InnerJoin, null, onExpression);
		}

		public IJoinTable<T, T1, T2, T3, T4, T5> LeftJoin<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1, T2, T3, T4, T5> LeftJoin<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.LeftJoin, null, onExpression);
		}


		public IJoinTable<T, T1, T2, T3, T4, T5> RightJoin<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.RightJoin, queryExpression, onExpression);
		}


		public IJoinTable<T, T1, T2, T3, T4, T5> RightJoin<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class, new()
		{
			return new LightJoinTable<T, T1, T2, T3, T4, T5> (this, JoinType.RightJoin, null, onExpression);
		}

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IJoinTable<T, T1, T2, T3, T4> WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// Where the specified expression.
		/// </summary>T1,
		/// <param name="expression">Expression.</param>
		public IJoinTable<T, T1, T2, T3, T4> Where (Expression<Func<T, T1, T2, T3, T4, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = queryExpression;
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IJoinTable<T, T1, T2, T3, T4> WhereWithAnd (Expression<Func<T, T1, T2, T3, T4, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.And (_query, queryExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		public IJoinTable<T, T1, T2, T3, T4> WhereWithOr (Expression<Func<T, T1, T2, T3, T4, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.Or (_query, queryExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IJoinTable<T, T1, T2, T3, T4> OrderByCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IJoinTable<T, T1, T2, T3, T4> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		public IJoinTable<T, T1, T2, T3, T4> OrderBy<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = orderExpression;
			return this;
		}

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		public IJoinTable<T, T1, T2, T3, T4> OrderByDescending<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = orderExpression;
			return this;
		}

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IJoinTable<T, T1, T2, T3, T4> OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IJoinTable<T, T1, T2, T3, T4> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		public IJoinTable<T, T1, T2, T3, T4> Take (int count)
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
			//if (_region == null) {
			//	_region = new Region (0, count);
			//}
			//else {
			//	_region.Size = count;
			//}
			return this;
		}

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		public IJoinTable<T, T1, T2, T3, T4> Skip (int index)
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
			//if (_region == null) {
			//	_region = new Region (index, int.MaxValue);
			//}
			//else {
			//	_region.Start = index;
			//}
			return this;
		}

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public IJoinTable<T, T1, T2, T3, T4> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			//if (_region == null) {
			//	_region = new Region (start, size);
			//}
			//else {
			//	_region.Start = start;
			//	_region.Size = size;
			//}
			return this;
		}

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IJoinTable<T, T1, T2, T3, T4> RangeReset ()
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
		public IJoinTable<T, T1, T2, T3, T4> PageSize (int page, int size)
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
			//if (_region == null) {
			//	_region = new Region (start, size);
			//}
			//else {
			//	_region.Start = start;
			//	_region.Size = size;
			//}
			return this;
		}

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		public IJoinTable<T, T1, T2, T3, T4> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		public ISelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, T3, T4, TResult>> expression) where TResult : class
		{
			JoinSelector selector = LambdaExpressionExtend.CreateSelector (expression) as JoinSelector;
			if (selector == null) {
				throw new LightDataException ("");
			}
			Delegate dele = expression.Compile ();
			LightJoinSelect<TResult> selectable = new LightJoinSelect<TResult> (_context, dele, selector, _modelList, _query, _order, _region, _level);
			return selectable;
		}

	}
}

