using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Aggregate.
	/// </summary>
	public interface IAggregate<K> : IEnumerable<K> where K : class
	{
		/// <summary>
		/// Set the specified having expression
		/// </summary>
		/// <param name="expression">Expression.</param>
		IAggregate<K> Having (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Catch the specified having expression with and.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Catch the specified having expression with or.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Reset the specified having expression.
		/// </summary>
		IAggregate<K> HavingReset ();

		/// <summary>
		/// Catch the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Catch the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Set the specified asc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Set the specified desc order by expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		IAggregate<K> OrderByReset ();

		/// <summary>
		/// Set order by random.
		/// </summary>
		IAggregate<K> OrderByRandom ();

		/// <summary>
		/// Get data list.
		/// </summary>
		/// <returns>The list.</returns>
		List<K> ToList ();

		/// <summary>
		/// Get first instance.
		/// </summary>
		K First ();

		/// <summary>
		/// Set take datas count.
		/// </summary>
		/// <param name="count">Count.</param>
		IAggregate<K> Take (int count);

		/// <summary>
		/// Set from datas index.
		/// </summary>
		/// <returns>JoinTable.</returns>
		/// <param name="index">Index.</param>
		IAggregate<K> Skip (int index);

		/// <summary>
		/// Set take datas range.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IAggregate<K> Range (int from, int to);

		/// <summary>
		/// Reset take datas range.
		/// </summary>
		IAggregate<K> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IAggregate<K> PageSize (int page, int size);

		/// <summary>
		/// Set the SafeLevel.
		/// </summary>
		IAggregate<K> SafeMode (SafeLevel level);

		/// <summary>
		/// Inner Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Inner Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Inner Join table with query and onExpression.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Left Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Left Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Left Join table with query and onExpression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Right Join table with specified queryExpression and onExpression.
		/// </summary>
		/// <param name="queryExpression">Query expression.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Right Join table with specified onExpression.
		/// </summary>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Right Join table with query and onExpression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Inner Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Left Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Right Join aggregate data with onExpression.
		/// </summary>
		/// <param name="aggregate">Aggregate.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Inner Join select data with onExpression.
		/// </summary>
		/// <param name="select">Select.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Left Join select data with onExpression.
		/// </summary>
		/// <param name="select">Select.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> LeftJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		/// <summary>
		/// Right Join select data with onExpression.
		/// </summary>
		/// <param name="select">Select.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> RightJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

	}
}

