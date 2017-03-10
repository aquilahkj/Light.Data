using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Select field.
	/// </summary>
	public interface ISelectField<K> : IEnumerable<K>
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

		/// <summary>
		/// Elements at index.
		/// </summary>
		/// <returns>The <see cref="!:K"/>.</returns>
		/// <param name="index">Index.</param>
		K ElementAt (int index);
	}
}
