using System;
namespace Light.Data
{
	/// <summary>
	/// Aggregate group data.
	/// </summary>
	public class AggregateGroupData
	{
		AggregateModel _model;

		internal AggregateModel Model {
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


		Region _region;

		internal Region Region {
			get {
				return _region;
			}
		}

		SafeLevel _level;

		internal SafeLevel Level {
			get {
				return _level;
			}
		}

		DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		internal AggregateGroupData (DataContext context, AggregateModel model, QueryExpression query, QueryExpression having, OrderExpression order, Region region, SafeLevel level)
		{
			_model = model;
			_query = query;
			_having = having;
			_order = order;
			_region = region;
			_level = level;
			_context = context;
		}
	}
}

