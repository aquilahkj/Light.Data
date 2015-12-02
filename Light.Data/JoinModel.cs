using System;
using System.Collections.Generic;

namespace Light.Data
{
	class JoinModel
	{
		//		bool isSingle;

		JoinType _type = JoinType.Default;

		public JoinType Type {
			get {
				return _type;
			}
		}

		bool selectAllField;

		public bool SelectAllField {
			get {
				return selectAllField;
			}
			set {
				selectAllField = value;
			}
		}

		List<DataFieldInfo> fields = new List<DataFieldInfo> ();

		public DataFieldInfo[] GetFields ()
		{
			return fields.ToArray ();
		}

		public void SetField (DataFieldInfo field)
		{
			this.fields.Add (field);
		}

		DataEntityMapping _mapping = null;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		QueryExpression _query = null;

		public QueryExpression Query {
			get {
				return _query;
			}
		}

		OrderExpression _order = null;

		public OrderExpression Order {
			get {
				return _order;
			}
		}

		//		List<JoinModel> models = null;
		//
		//		DataFieldExpression _on = null;

		public JoinModel (DataEntityMapping mapping, JoinType type, QueryExpression query, OrderExpression order)
		{
			this._mapping = mapping;
			this._type = type;
			this._query = query;
			this._order = order;
		}


	}
}

