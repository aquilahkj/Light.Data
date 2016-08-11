using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// LSelectable.
	/// </summary>
	public class LSelectable<K> : IEnumerable<K> where K : class
	{
		readonly Type _type;

		readonly QueryExpression _query;

		readonly OrderExpression _order;

		readonly Region _region;

		readonly DataContext _context;

		readonly SafeLevel _level;

		readonly Delegate _dele;

		readonly ISelector _selector;

		internal LSelectable (DataContext context, Delegate dele, ISelector selector, Type type, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		{
			_context = context;
			_dele = dele;
			_selector = selector;
			_type = type;
			_query = query;
			_order = order;
			_region = region;
			_level = level;
		}


		public IEnumerator<K> GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		public List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QueryDataMappingEnumerable (_type, _selector, _query, _order, _region, _level)) {
				object obj = _dele.DynamicInvoke (item);
				list.Add (obj as K);
			}
			return list;
		}
	}
}

