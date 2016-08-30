
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

		readonly DataEntityMapping _mapping = null;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		readonly IJoinTableMapping _joinMapping = null;

		public IJoinTableMapping JoinMapping {
			get {
				return _joinMapping;
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

		string _aliasTableName;

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
		{
			this._mapping = mapping;
			this._connect = connect;
			this._query = query;
			this._order = order;
			this._aliasTableName = aliasTableName;
			this._joinMapping = mapping;
		}

		public string CreateSqlString (CommandFactory factory, CreateSqlState state)
		{
			StringBuilder sb = new StringBuilder ();
			//if (_connect != null) {
			//	sb.AppendFormat (" {0} ", factory.GetJoinPredicate (_connect.Type));
			//}
			if (_query != null) {
				CommandData command = factory.CreateSelectBaseCommand (_mapping, "*", _query, null, null, state);
				string sql = string.Concat ("(", command.CommandText, ")");
				string aliasName = _aliasTableName ?? _mapping.TableName;
				sb.Append (factory.CreateAliasTableSql (sql, aliasName));
			}
			else {
				if (_aliasTableName != null) {
					sb.Append (factory.CreateAliasTableSql (factory.CreateDataTableSql (_mapping.TableName), _aliasTableName));
				}
				else {
					sb.Append (factory.CreateDataTableSql (_mapping.TableName));
				}
			}

			//if (_connect != null && _connect.On != null) {
			//	sb.Append (factory.GetOnString (_connect.On, state));
			//}
			return sb.ToString ();
		}
	}
}

