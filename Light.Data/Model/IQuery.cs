using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	public interface IQuery<T> : IEnumerable<T> where T : class, new()
	{
		#region LQuery<T> 成员

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IQuery<T> WhereReset ();

		/// <summary>
		/// Where the specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		IQuery<T> Where (Expression<Func<T, bool>> expression);


		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IQuery<T> WhereWithAnd (Expression<Func<T, bool>> expression);

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		IQuery<T> WhereWithOr (Expression<Func<T, bool>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression);

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression);

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression);

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IQuery<T> OrderByReset ();

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IQuery<T> OrderByRandom ();

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		IQuery<T> Take (int count);

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		IQuery<T> Skip (int index);

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		IQuery<T> Range (int from, int to);

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		IQuery<T> RangeReset ();

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		IQuery<T> PageSize (int page, int size);

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		IQuery<T> SafeMode (SafeLevel level);

		/// <summary>
		/// Gets the datas count.
		/// </summary>
		/// <value>The count.</value>
		int Count {
			get;
		}

		/// <summary>
		/// Get single instance.
		/// </summary>
		/// <returns>instance.</returns>
		T Single ();

		/// <summary>
		/// Elements at index.
		/// </summary>
		/// <returns>instance.</returns>
		/// <param name="index">Index.</param>
		T ElementAt (int index);

		/// <summary>
		/// Gets the data is exists with query expression.
		/// </summary>
		/// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
		bool Exists {
			get;
		}

		/// <summary>
		/// To the list.
		/// </summary>
		/// <returns>The list.</returns>
		List<T> ToList ();

		/// <summary>
		/// To the array.
		/// </summary>
		/// <returns>The array.</returns>
		T [] ToArray ();

		#endregion

		/// <summary>
		/// Insert this instance.
		/// </summary>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		int Insert<K> ();

		/// <summary>
		/// Update the values on specified query expression..
		/// </summary>
		/// <param name="updates">Updates.</param>
		int Update (params UpdateSetValue [] updates);

		/// <summary>
		/// Delete datas on specified query expression.
		/// </summary>
		int Delete ();

		ISelect<TResult> Select<TResult> (Expression<Func<T, TResult>> expression) where TResult : class;

		IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new();

		IAggregate<K> GroupBy<K> (Expression<Func<T, K>> expression) where K : class;
	}
}

