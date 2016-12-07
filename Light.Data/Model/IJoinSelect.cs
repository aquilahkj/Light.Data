using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Join select.
	/// </summary>
	public interface IJoinSelect<K> : IEnumerable<K> where K : class
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
