using System;
using System.Linq.Expressions;

namespace Light.Data
{
	public interface IJoinTable<T, T1, T2>
		where T : class, new()
		where T1 : class, new()
		where T2 : class, new()
	{
		//IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		//IJoinTable<T, T1, T2> Join<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		//IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		//IJoinTable<T, T1, T2> LeftJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		//IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T2, bool>> queryExpression, Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		//IJoinTable<T, T1, T2> RightJoin<T2> (Expression<Func<T, T1, T2, bool>> onExpression) where T2 : class, new();

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2> WhereReset ();

		/// <summary>
		/// Where the specified expression.
		/// </summary>T1,
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2> Where (Expression<Func<T, T1, T2, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2> WhereWithAnd (Expression<Func<T, T1, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2> WhereWithOr (Expression<Func<T, T1, bool>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2> OrderByCatch<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IJoinTable<T, T1, T2> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> OrderBy<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IJoinTable<T, T1, T2> OrderByDescending<TKey> (Expression<Func<T, T1, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2> OrderByReset ();

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2> OrderByRandom ();

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		IJoinTable<T, T1, T2> Take (int count);

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		IJoinTable<T, T1, T2> Skip (int index);

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IJoinTable<T, T1, T2> Range (int from, int to);

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IJoinTable<T, T1, T2> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IJoinTable<T, T1, T2> PageSize (int page, int size);

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		IJoinTable<T, T1, T2> SafeMode (SafeLevel level);
	}
}

