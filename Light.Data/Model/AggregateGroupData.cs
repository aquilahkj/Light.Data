using System;
namespace Light.Data
{
	public class AggregateGroupData
	{
		AggregateGroup _model;

		internal AggregateGroup Model {
			get {
				return _model;
			}
		}

		QueryExpression _query;

		internal QueryExpression Query {
			get {
				return _query;
			}
		}

		QueryExpression _having;

		internal QueryExpression Having {
			get {
				return _having;
			}
		}

		OrderExpression _order;

		internal OrderExpression Order {
			get {
				return _order;
			}
		}



		internal AggregateGroupData (AggregateGroup model, QueryExpression query, QueryExpression having, OrderExpression order)
		{
			_model = model;
			_query = query;
			_having = having;
			_order = order;
		}
	}
}

