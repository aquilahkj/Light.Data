using System;
using System.Linq.Expressions;

namespace Light.Data
{
	public interface IJoinTable<T, T1, T2, T3, T4, T5>
		where T : class, new()
		where T1 : class, new()
		where T2 : class, new()
		where T3 : class, new()
		where T4 : class, new()
		where T5 : class, new()
	{

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2, T3, T4, T5> WhereReset ();

		/// <summary>
		/// Where the specified expression.
		/// </summary>T1,
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> Where (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> WhereWithAnd (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> WhereWithOr (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderByCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderBy<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderByDescending<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderByReset ();

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2, T3, T4, T5> OrderByRandom ();

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> Take (int count);

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> Skip (int index);

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> Range (int from, int to);

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2, T3, T4, T5> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> PageSize (int page, int size);

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		IJoinTable<T, T1, T2, T3, T4, T5> SafeMode (SafeLevel level);

		/// <summary>
		/// Select the specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
		ISelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, T3, T4, T5, TResult>> expression) where TResult : class;
	}
}

