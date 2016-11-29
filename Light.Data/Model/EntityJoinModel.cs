
using System.Text;

namespace Light.Data
{
	class EntityJoinModel : IJoinModel
	{
		readonly JoinConnect _connect;

		public JoinConnect Connect {
			get {
				return _connect;
			}
		}

		readonly DataEntityMapping _mapping;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		readonly IJoinTableMapping _joinMapping;

		public IJoinTableMapping JoinMapping {
			get {
				return _joinMapping;
			}
		}

		QueryExpression _query;

		public QueryExpression Query {
			get {
				return _query;
			}
			set {
				_query = value;
			}
		}

		OrderExpression _order;

		public OrderExpression Order {
			get {
				return _order;
			}
			set {
				_order = value;
			}
		}

		bool _distinct;

		public bool Distinct {
			get {
				return _distinct;
			}
			set {
				_distinct = value;
			}
		}

		//readonly ISelector _selector;

		//public ISelector Selector {
		//	get {
		//		return _selector;
		//	}
		//}

		readonly string _aliasTableName;

		public string AliasTableName {
			get {
				return _aliasTableName;
			}
			//set {
			//	if (!string.IsNullOrEmpty (value)) {
			//		aliasTableName = value;
			//	}
			//}
		}

		public EntityJoinModel (DataEntityMapping mapping, string aliasTableName, JoinConnect connect, QueryExpression query, OrderExpression order)
			//: this (mapping, AllSelector.Value, aliasTableName, connect, query, order)
		{
			//this._mapping = mapping;
			//this.Selector
			//this._connect = connect;
			//this._query = query;
			//this._order = order;
			//this._aliasTableName = aliasTableName;
			//this._joinMapping = mapping;

			this._mapping = mapping;
			//this._selector = AllSelector.Value;
			this._connect = connect;
			this._query = query;
			this._order = order;
			this._aliasTableName = aliasTableName;
			this._joinMapping = mapping;
		}

		//public EntityJoinModel (DataEntityMapping mapping, ISelector selector, string aliasTableName, JoinConnect connect, QueryExpression query, OrderExpression order)
		//{
		//	this._mapping = mapping;
		//	this._selector = selector;
		//	this._connect = connect;
		//	this._query = query;
		//	this._order = order;
		//	this._aliasTableName = aliasTableName;
		//	this._joinMapping = mapping;
		//}

		public string CreateSqlString (CommandFactory factory, CreateSqlState state)
		{
			StringBuilder sb = new StringBuilder ();
			if (_query != null || _order != null || _distinct) {
				CommandData command = factory.CreateSelectCommand (_mapping, AllSelector.Value, _query, _order, _distinct, null, state);
				string aliasName = _aliasTableName ?? _mapping.TableName;
				sb.Append (factory.CreateAliasQuerySql (command.CommandText, aliasName));
			}
			else {
				if (_aliasTableName != null) {
					sb.Append (factory.CreateAliasTableSql (factory.CreateDataTableSql (_mapping.TableName), _aliasTableName));
				}
				else {
					sb.Append (factory.CreateDataTableSql (_mapping.TableName));
				}
			}
			return sb.ToString ();
		}
	}
}

