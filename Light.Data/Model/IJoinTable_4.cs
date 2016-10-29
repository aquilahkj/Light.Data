using System;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Join table.
	/// </summary>
	public interface IJoinTable<T, T1, T2, T3, T4>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
	{
		/// <summary>
		/// Inner Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> Join<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Inner Join table with specified specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> Join<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Left Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> LeftJoin<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Left Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> LeftJoin<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Right Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> RightJoin<T5> (Expression<Func<T5, bool>> queryExpression, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Right Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> RightJoin<T5> (Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Inner Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> Join<T5> (IAggregate<T5> aggregate, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Left Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> LeftJoin<T5> (IAggregate<T5> aggregate, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;

		/// <summary>
		/// Right Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T5">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> RightJoin<T5> (IAggregate<T5> aggregate, Expression<Func<T, T1, T2, T3, T4, T5, bool>> onExpression) where T5 : class;


		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		IJoinTable<T, T1, T2, T3, T4> WhereReset ();

		/// <summary>
		/// Set the specified where expression.
		/// </summary>T1,
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4> Where (Expression<Func<T, T1, T2, T3, T4, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4> WhereWithAnd (Expression<Func<T, T1, T2, T3, T4, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4> WhereWithOr (Expression<Func<T, T1, T2, T3, T4, bool>> expression);

		/// <summary>
		/// Catch the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4> OrderByCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression);

		/// <summary>
		/// Catch the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression);

		/// <summary>
		/// Set the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4> OrderBy<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression);

		/// <summary>
		/// Set the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4> OrderByDescending<TKey> (Expression<Func<T, T1, T2, T3, T4, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		IJoinTable<T, T1, T2, T3, T4> OrderByReset ();

		/// <summary>
		/// Set take datas count.
		/// </summary>
		/// <param name="count">Count.</param>
		IJoinTable<T, T1, T2, T3, T4> Take (int count);

		/// <summary>
		/// Set from datas index.
		/// </summary>
		/// <returns>JoinTable.</returns>
		/// <param name="index">Index.</param>
		IJoinTable<T, T1, T2, T3, T4> Skip (int index);

		/// <summary>
		/// Set take datas range.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IJoinTable<T, T1, T2, T3, T4> Range (int from, int to);

		/// <summary>
		/// Reset take datas range.
		/// </summary>
		IJoinTable<T, T1, T2, T3, T4> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IJoinTable<T, T1, T2, T3, T4> PageSize (int page, int size);

		/// <summary>
		/// Set the SafeLevel.
		/// </summary>
		IJoinTable<T, T1, T2, T3, T4> SafeMode (SafeLevel level);

		/// <summary>
		/// Create Selector.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
		ISelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, T3, T4, TResult>> expression) where TResult : class;
	}
}

