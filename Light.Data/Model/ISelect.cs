using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	public interface ISelect<K> : IEnumerable<K> where K : class
	{
		List<K> ToList ();

		K First {
			get;
		}
	}
}

