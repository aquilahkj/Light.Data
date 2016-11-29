using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Select.
	/// </summary>
	public interface ISelect<K> : IEnumerable<K> where K : class
	{
		/// <summary>
		/// Get data list.
		/// </summary>
		/// <returns>The list.</returns>
		List<K> ToList ();

		/// <summary>
		/// Get first instance.
		/// </summary>
		K First ();

		IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;


		/// <summary>
		/// Inner Join query data with onExpression.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="onExpression">On expression.</param>
		/// <typeparam name="T1">The 1st type parameter.</typeparam>
		IJoinTable<K, T1> Join<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		///// <summary>
		///// Left Join query data with onExpression.
		///// </summary>
		///// <param name="query">Query.</param>
		///// <param name="onExpression">On expression.</param>
		///// <typeparam name="T1">The 1st type parameter.</typeparam>
		//IJoinTable<K, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		///// <summary>
		///// Right Join query data with onExpression.
		///// </summary>
		///// <param name="query">Query.</param>
		///// <param name="onExpression">On expression.</param>
		///// <typeparam name="T1">The 1st type parameter.</typeparam>
		//IJoinTable<K, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

	}
}

