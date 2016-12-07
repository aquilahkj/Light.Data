//using System;
//using System.Linq.Expressions;

//namespace Light.Data
//{
//	/// <summary>
//	/// Join table.
//	/// </summary>
//	public interface IJoinTable<T, T1, T2>
//		where T : class
//		where T1 : class
//		where T2 : class
//	{
//		/// <summary>
//		/// Inner Join table with specified queryExpression and onExpression.
//		/// </summary>
//		/// <param name="queryExpression">Query expression.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> Join<T3> (Expression<Func<T3, bool>> queryExpression, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Inner Join table with specified specified onExpression.
//		/// </summary>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> Join<T3> (Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Left Join table with specified queryExpression and onExpression.
//		/// </summary>
//		/// <param name="queryExpression">Query expression.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> LeftJoin<T3> (Expression<Func<T3, bool>> queryExpression, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Left Join table with specified onExpression.
//		/// </summary>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> LeftJoin<T3> (Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Right Join table with specified queryExpression and onExpression.
//		/// </summary>
//		/// <param name="queryExpression">Query expression.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> RightJoin<T3> (Expression<Func<T3, bool>> queryExpression, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Right Join table with specified onExpression.
//		/// </summary>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> RightJoin<T3> (Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Inner Join query data with onExpression.
//		/// </summary>
//		/// <param name="query">Query.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> Join<T3> (IQuery<T3> query, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Left Join query data with onExpression.
//		/// </summary>
//		/// <param name="query">Query.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> LeftJoin<T3> (IQuery<T3> query, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Right Join query data with onExpression.
//		/// </summary>
//		/// <param name="query">Query.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> RightJoin<T3> (IQuery<T3> query, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;


//		/// <summary>
//		/// Inner Join aggregate data with onExpression.
//		/// </summary>
//		/// <param name="aggregate">Aggregate.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> Join<T3> (IAggregate<T3> aggregate, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Left Join aggregate data with onExpression.
//		/// </summary>
//		/// <param name="aggregate">Aggregate.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> LeftJoin<T3> (IAggregate<T3> aggregate, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;

//		/// <summary>
//		/// Right Join aggregate data with onExpression.
//		/// </summary>
//		/// <param name="aggregate">Aggregate.</param>
//		/// <param name="onExpression">On expression.</param>
//		/// <typeparam name="T3">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3> RightJoin<T3> (IAggregate<T3> aggregate, Expression<Func<T, T1, T2, T3, bool>> onExpression) where T3 : class;


//		/// <summary>
//		/// Reset the specified where expression
//		/// </summary>
//		IJoinTable<T, T1, T2> WhereReset ();

//		/// <summary>
//		/// Set the specified where expression.
//		/// </summary>T1,
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2> Where (Expression<Func<T, T1, T2, bool>> expression);

//		/// <summary>
//		/// Catch the specified where expression with and.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2> WhereWithAnd (Expression<Func<T, T1, T2, bool>> expression);

//		/// <summary>
//		/// Catch the specified where expression with or.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2> WhereWithOr (Expression<Func<T, T1, T2, bool>> expression);

//		/// <summary>
//		/// Catch the specified asc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2> OrderByCatch<TKey> (Expression<Func<T, T1, T2, TKey>> expression);

//		/// <summary>
//		/// Catch the specified desc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, TKey>> expression);

//		/// <summary>
//		/// Set the specified asc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2> OrderBy<TKey> (Expression<Func<T, T1, T2, TKey>> expression);

//		/// <summary>
//		/// Set the specified desc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2> OrderByDescending<TKey> (Expression<Func<T, T1, T2, TKey>> expression);

//		/// <summary>
//		/// Reset the specified order by expression.
//		/// </summary>
//		IJoinTable<T, T1, T2> OrderByReset ();

//		/// <summary>
//		/// Set take datas count.
//		/// </summary>
//		/// <param name="count">Count.</param>
//		IJoinTable<T, T1, T2> Take (int count);

//		/// <summary>
//		/// Set from datas index.
//		/// </summary>
//		/// <returns>JoinTable.</returns>
//		/// <param name="index">Index.</param>
//		IJoinTable<T, T1, T2> Skip (int index);

//		/// <summary>
//		/// Set take datas range.
//		/// </summary>
//		/// <param name="from">From.</param>
//		/// <param name="to">To.</param>
//		IJoinTable<T, T1, T2> Range (int from, int to);

//		/// <summary>
//		/// Reset take datas range.
//		/// </summary>
//		IJoinTable<T, T1, T2> RangeReset ();

//		/// <summary>
//		/// Sets page size.
//		/// </summary>
//		/// <param name="page">Page.</param>
//		/// <param name="size">Size.</param>
//		IJoinTable<T, T1, T2> PageSize (int page, int size);

//		/// <summary>
//		/// Set the SafeLevel.
//		/// </summary>
//		IJoinTable<T, T1, T2> SafeMode (SafeLevel level);

//		/// <summary>
//		/// Sets the distinct.
//		/// </summary>
//		IJoinTable<T, T1, T2> SetDistinct (bool distinct);

//		/// <summary>
//		/// Create Selector.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
//		IJoinSelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, TResult>> expression) where TResult : class;

//		/// <summary>
//		/// Select fileds data insert to the special table K.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="K">The 1st type parameter.</typeparam>
//		int SelectInsert<K> (Expression<Func<T, T1, T2, K>> expression) where K : class, new();
//	}
//}

