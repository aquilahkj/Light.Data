using System;
using System.Collections;
using System.Collections.Generic;

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
	}
}

