using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	class LightJoinSelect<K> : IJoinSelect<K> where K : class
	{
		readonly QueryExpression _query;

		readonly OrderExpression _order;

		readonly bool _distinct;

		readonly Region _region;

		readonly DataContext _context;

		readonly SafeLevel _level;

		readonly Delegate _dele;

		readonly ISelector _selector;

		readonly IJoinModel [] _models;

		readonly DynamicMultiDataMapping _mappping;

		internal LightJoinSelect (DataContext context, Delegate dele, ISelector selector, IJoinModel [] models, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
		{
			_models = models;
			_context = context;
			_dele = dele;
			_selector = selector;
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
			_mappping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), models);
		}

		public IEnumerator<K> GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _distinct, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj as K;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _distinct, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj;
			}
		}

		public List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QueryJoinData (_mappping, _selector, _models, _query, _order, _distinct, _region, _level)) {
				if (item != null) {
					object obj = _dele.DynamicInvoke (item as object []);
					list.Add (obj as K);
				}
				else {
					list.Add (null);
				}
			}
			return list;
		}

		public K First ()
		{
			object item = _context.SelectJoinDataSingle (_mappping, _selector, _models, _query, _order, 0, _level);
			if (item != null) {
				object obj = _dele.DynamicInvoke (item);
				return obj as K;
			}
			else {
				return null;
			}
		}
	}
}

