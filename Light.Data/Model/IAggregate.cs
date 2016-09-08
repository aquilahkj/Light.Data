using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	public interface IAggregate<K>
		where K : class
	{
		/// <summary>
		/// Set the specified having expression
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		IAggregate<K> Having (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Catch the specified having expression with and.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Catch the specified having expression with or.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression);

		/// <summary>
		/// Reset the specified having expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <returns>The reset.</returns>
		IAggregate<K> HavingReset ();

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IAggregate<K> OrderByReset ();

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IAggregate<K> OrderByRandom ();

		/// <summary>
		/// Tos the list.
		/// </summary>
		/// <returns>The list.</returns>
		List<K> ToList ();

		K First {
			get;
		}

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		IAggregate<K> Take (int count);

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		IAggregate<K> Skip (int index);

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IAggregate<K> Range (int from, int to);

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IAggregate<K> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IAggregate<K> PageSize (int page, int size);

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		IAggregate<K> SafeMode (SafeLevel level);

		AggregateGroupData GetGroupData ();

		IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

		IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;//, new()

	}
}

