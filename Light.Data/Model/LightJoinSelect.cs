using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

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

		readonly DynamicMultiDataMapping _mapping;

		protected LightJoinSelect (DataContext context, LambdaExpression expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
		{
			_selector = LambdaExpressionExtend.CreateMutliSelector (expression, maps);
			_dele = expression.Compile ();
			_models = models.ToArray ();
			_context = context;
			_mapping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), _models);
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		#region IEnumerable implementation

		public IEnumerator<K> GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mapping, _selector, _models, _query, _order, _distinct, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj as K;
			}
		}

		#endregion

		//protected QueryExpression _query;

		//public override QueryExpression QueryExpression {
		//	get {
		//		return _query;
		//	}
		//}

		//protected OrderExpression _order;

		//public override OrderExpression OrderExpression {
		//	get {
		//		return _order;
		//	}
		//}

		//protected bool _distinct;

		//public override bool Distinct {
		//	get {
		//		return _distinct;
		//	}
		//}

		//protected Region _region;

		//public override Region Region {
		//	get {
		//		return _region;
		//	}
		//}

		//protected SafeLevel _level;

		//public override SafeLevel Level {
		//	get {
		//		return _level;
		//	}
		//}

		//public LightJoinSelect (DataContext context, LambdaExpression expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
		//	: base (context, expression, models, maps)
		//{
		//	_query = query;
		//	_order = order;
		//	_distinct = distinct;
		//	_region = region;
		//	_level = level;
		//} 

		public List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QueryJoinData (_mapping, _selector, _models, _query, _order, _distinct, _region, _level)) {
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
			object item = _context.SelectJoinDataSingle (_mapping, _selector, _models, _query, _order, 0, _level);
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

