using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	public class LightJoinSelect<K> : ISelect<K> where K : class
	{
		readonly QueryExpression _query;

		readonly OrderExpression _order;

		readonly Region _region;

		readonly DataContext _context;

		readonly SafeLevel _level;

		readonly Delegate _dele;

		readonly JoinSelector _selector;

		readonly List<JoinModel> _models;

		readonly DynamicMultiDataMapping _mappping;

		//readonly DynamicMultiDataMapping _mapping;

		internal LightJoinSelect (DataContext context, Delegate dele, JoinSelector selector, List<JoinModel> models, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		{
			_models = models;
			_context = context;
			_dele = dele;
			_selector = selector;
			_query = query;
			_order = order;
			_region = region;
			_level = level;
			_mappping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), models);
		}

		public IEnumerator<K> GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj as K;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj;
			}
		}

		public List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object[]);
				list.Add (obj as K);
			}
			return list;
		}
	}
}

