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
	}
}

