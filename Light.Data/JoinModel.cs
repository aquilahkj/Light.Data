
namespace Light.Data
{
	class JoinModel
	{
		readonly JoinConnect _connect;

		public JoinConnect Connect {
			get {
				return _connect;
			}
		}

		readonly DataEntityMapping _mapping = null;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		readonly QueryExpression _query = null;

		public QueryExpression Query {
			get {
				return _query;
			}
		}

		readonly OrderExpression _order = null;

		public OrderExpression Order {
			get {
				return _order;
			}
		}

		public JoinModel (DataEntityMapping mapping, JoinConnect connect, QueryExpression query, OrderExpression order)
		{
			this._mapping = mapping;
			this._connect = connect;
			this._query = query;
			this._order = order;
		}


	}
}

