using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightSelectJoin<K> : SelectJoinBase<K> where K : class
	{
		readonly QueryExpression _query;

		readonly OrderExpression _order;

		readonly bool _distinct;

		readonly Region _region;

		//readonly DataContext _context;

		readonly SafeLevel _level;

		//readonly Delegate _dele;

		//readonly ISelector _selector;

		//readonly IJoinModel [] _models;

		//readonly DynamicMultiDataMapping _mapping;

		public override QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		public override OrderExpression OrderExpression {
			get {
				return _order;
			}
		}

		public override bool Distinct {
			get {
				return _distinct;
			}
		}

		public override Region Region {
			get {
				return _region;
			}
		}

		public override SafeLevel Level {
			get {
				return _level;
			}
		}

		//protected LightSelectJoin (DataContext context, LambdaExpression expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
		//{
		//	_selector = LambdaExpressionExtend.CreateMutliSelector (expression, maps);
		//	_dele = expression.Compile ();
		//	_models = models.ToArray ();
		//	_context = context;
		//	_mapping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), _models);
		//	_query = query;
		//	_order = order;
		//	_distinct = distinct;
		//	_region = region;
		//	_level = level;
		//}

		protected LightSelectJoin (DataContext context, LambdaExpression expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			: base (context, expression, models, maps)
		{
			//_selector = LambdaExpressionExtend.CreateMutliSelector (expression, maps);
			//_dele = expression.Compile ();
			//_models = models.ToArray ();
			//_context = context;
			//_mapping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), _models);
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
		}

		#region IEnumerable implementation

		public override IEnumerator<K> GetEnumerator ()
		{
			foreach (object item in _context.QueryJoinData (_mapping, _selector, _models, _query, _order, _distinct, _region, _level)) {
				object obj = _dele.DynamicInvoke (item as object []);
				yield return obj as K;
			}
		}

		#endregion

		public override List<K> ToList ()
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

		public override K First ()
		{
			object item = _context.SelectJoinDataFirst (_mapping, _selector, _models, _query, _order, 0, _level);
			if (item != null) {
				object obj = _dele.DynamicInvoke (item as object []);
				return obj as K;
			}
			else {
				return null;
			}
		}

		//public override IEnumerator<K> GetEnumerator ()
		//{
		//	throw new NotImplementedException ();
		//}

		//public override List<K> ToList ()
		//{
		//	throw new NotImplementedException ();
		//}

		//public override K First ()
		//{
		//	throw new NotImplementedException ();
		//}
	}
}

