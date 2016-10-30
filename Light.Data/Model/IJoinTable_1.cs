using System;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Join table.
	/// </summary>
	public interface IJoinTable<T, T1>
		where T : class
		where T1 : class
	{
		/// <summary>
		/// Inner Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Inner Join table with specified specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Left Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Left Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Right Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Right Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Inner Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> Join<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Left Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> LeftJoin<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Right Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T2">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> RightJoin<T2> (IAggregate<T2> aggregate, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class;

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		IJoinTable<T, T1> WhereReset ();

		/// <summary>
		/// Set the specified where expression.
		/// </summary>T1,
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1> Where (Expression<Func<T, T1, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1> WhereWithAnd (Expression<Func<T, T1, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1> WhereWithOr (Expression<Func<T, T1, bool>> expression);

		/// <summary>
		/// Catch the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1> OrderByCatch<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Catch the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Set the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1> OrderBy<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Set the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1> OrderByDescending<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		IJoinTable<T, T1> OrderByReset ();

		/// <summary>
		/// Set take datas count.
		/// </summary>
		/// <param name="count">Count.</param>
		IJoinTable<T, T1> Take (int count);

		/// <summary>
		/// Set from datas index.
		/// </summary>
		/// <returns>JoinTable.</returns>
		/// <param name="index">Index.</param>
		IJoinTable<T, T1> Skip (int index);

		/// <summary>
		/// Set take datas range.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IJoinTable<T, T1> Range (int from, int to);

		/// <summary>
		/// Reset take datas range.
		/// </summary>
		IJoinTable<T, T1> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IJoinTable<T, T1> PageSize (int page, int size);

		/// <summary>
		/// Set the SafeLevel.
		/// </summary>
		IJoinTable<T, T1> SafeMode (SafeLevel level);

		/// <summary>
		/// Create Selector.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
		ISelect<TResult> Select<TResult> (Expression<Func<T, T1, TResult>> expression) where TResult : class;

		/// <summary>
		/// Select fileds data insert to the special table K.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		int SelectInsert<K> (Expression<Func<T, T1, K>> expression) where K : class, new();
	}
}

