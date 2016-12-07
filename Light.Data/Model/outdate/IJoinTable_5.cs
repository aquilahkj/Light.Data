//using System;
//using System.Linq.Expressions;

//namespace Light.Data
//{
//	/// <summary>
//	/// Join table.
//	/// </summary>
//	public interface IJoinTable<T, T1, T2, T3, T4, T5>
//		where T : class
//		where T1 : class
//		where T2 : class
//		where T3 : class
//		where T4 : class
//		where T5 : class
//	{

//		/// <summary>
//		/// Reset the specified where expression
//		/// </summary>
//		IJoinTable<T, T1, T2, T3, T4, T5> WhereReset ();

//		/// <summary>
//		/// Set the specified where expression.
//		/// </summary>T1,
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> Where (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

//		/// <summary>
//		/// Catch the specified where expression with and.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> WhereWithAnd (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

//		/// <summary>
//		/// Catch the specified where expression with or.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> WhereWithOr (Expression<Func<T, T1, T2, T3, T4, T5, bool>> expression);

//		/// <summary>
//		/// Catch the specified asc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> OrderByCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

//		/// <summary>
//		/// Catch the specified desc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

//		/// <summary>
//		/// Set the specified asc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3, T4, T5> OrderBy<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

//		/// <summary>
//		/// Set the specified desc order by expression.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
//		IJoinTable<T, T1, T2, T3, T4, T5> OrderByDescending<TKey> (Expression<Func<T, T1, T2, T3, T4, T5, TKey>> expression);

//		/// <summary>
//		/// Reset the specified order by expression.
//		/// </summary>
//		IJoinTable<T, T1, T2, T3, T4, T5> OrderByReset ();

//		/// <summary>
//		/// Set take datas count.
//		/// </summary>
//		/// <param name="count">Count.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> Take (int count);

//		/// <summary>
//		/// Set from datas index.
//		/// </summary>
//		/// <returns>JoinTable.</returns>
//		/// <param name="index">Index.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> Skip (int index);

//		/// <summary>
//		/// Set take datas range.
//		/// </summary>
//		/// <param name="from">From.</param>
//		/// <param name="to">To.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> Range (int from, int to);

//		/// <summary>
//		/// Reset take datas range.
//		/// </summary>
//		IJoinTable<T, T1, T2, T3, T4, T5> RangeReset ();

//		/// <summary>
//		/// Sets page size.
//		/// </summary>
//		/// <param name="page">Page.</param>
//		/// <param name="size">Size.</param>
//		IJoinTable<T, T1, T2, T3, T4, T5> PageSize (int page, int size);

//		/// <summary>
//		/// Set the SafeLevel.
//		/// </summary>
//		IJoinTable<T, T1, T2, T3, T4, T5> SafeMode (SafeLevel level);

//		/// <summary>
//		/// Sets the distinct.
//		/// </summary>
//		IJoinTable<T, T1, T2, T3, T4, T5> SetDistinct (bool distinct);

//		/// <summary>
//		/// Create Selector.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
//		IJoinSelect<TResult> Select<TResult> (Expression<Func<T, T1, T2, T3, T4, T5, TResult>> expression) where TResult : class;

//		/// <summary>
//		/// Select fileds data insert to the special table K.
//		/// </summary>
//		/// <param name="expression">Expression.</param>
//		/// <typeparam name="K">The 1st type parameter.</typeparam>
//		int SelectInsert<K> (Expression<Func<T, T1, T2, T3, T4, T5, K>> expression) where K : class, new();
//	}
//}

