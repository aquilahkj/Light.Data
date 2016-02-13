﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	public class LCollection<T>:ICollection<T> where T:class, new()
	{
		List<T> list = null;

		QueryExpression query;

		DataContext context;

		internal LCollection (DataContext context, QueryExpression query)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			if (query == null)
				throw new ArgumentNullException ("query");
			this.context = context;
			this.query = query;
		}

		#region ICollection implementation

		void InitialList ()
		{
			if (list == null) {
				list = context.LQuery<T> ().Where (query).ToList ();
			}
		}

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (T item)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear ()
		{
			list = null;
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Contains (T item)
		{
			if (item == null)
				throw new ArgumentNullException ("item");
			InitialList ();
			return list.Contains (item);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
		public void CopyTo (T[] array, int arrayIndex)
		{
			InitialList ();
			list.CopyTo (array, arrayIndex);
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove (T item)
		{
			throw new NotImplementedException ();
		}

		public int Count {
			get {
				InitialList ();
				return list.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		#endregion

		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			InitialList ();
			return list.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			InitialList ();
			return list.GetEnumerator ();
		}

		#endregion
	}
}
