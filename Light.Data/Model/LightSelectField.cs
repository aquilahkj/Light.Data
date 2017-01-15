using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightSelectField<K> : SelectFieldBase<K>
	{
		readonly QueryExpression _query;

		readonly OrderExpression _order;

		readonly bool _distinct;

		readonly Region _region;

		readonly SafeLevel _level;

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

		public LightSelectField (DataContext context, LambdaExpression expression, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			: base (context, expression)
		{
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
		}

		#region IEnumerable implementation

		public override IEnumerator<K> GetEnumerator ()
		{
			IEnumerable ie = _context.QuerySingleField (_fieldInfo, typeof (K), _query, _order, _distinct, _region, _level);
			foreach (K item in ie) {
				yield return item;
			}
		}

		#endregion

		public override List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QuerySingleField (_fieldInfo, typeof (K), _query, _order, _distinct, _region, _level)) {
				if (item != null) {
					list.Add ((K)item);
				}
				else {
					list.Add (default (K));
				}
			}
			return list;
		}

		public override K First ()
		{
			object item = _context.QuerySingleFieldFirst (_fieldInfo, typeof (K), _query, _order, _distinct, 0, _level);
			if (item != null) {
				return (K)item;
			}
			else {
				return default (K);
			}
		}
	}
}
