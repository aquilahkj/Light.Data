using System;
using System.Text;

namespace Light.Data
{
	class SelectJoinModel : IJoinModel
	{
		readonly SelectModel _model = null;

		public SelectModel Model {
			get {
				return _model;
			}
		}

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

		readonly ISelector _selector;

		public ISelector Selector {
			get {
				return _selector;
			}
		}

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

		//public SelectJoinModel (DataEntityMapping mapping, string aliasTableName, JoinConnect connect, QueryExpression query, OrderExpression order)
		//	: this (mapping, AllSelector.Value, aliasTableName, connect, query, order)
		//{
		//	//this._mapping = mapping;
		//	//this.Selector
		//	//this._connect = connect;
		//	//this._query = query;
		//	//this._order = order;
		//	//this._aliasTableName = aliasTableName;
		//	//this._joinMapping = mapping;
		//}

		//public SelectJoinModel (DataEntityMapping mapping, ISelector selector, IJoinTableMapping joinMapping, string aliasTableName, JoinConnect connect, QueryExpression query, OrderExpression order)
		//{
		//	this._mapping = mapping;
		//	this._selector = selector;
		//	this._connect = connect;
		//	this._query = query;
		//	this._order = order;
		//	this._aliasTableName = aliasTableName;
		//	this._joinMapping = joinMapping;
		//}

		public SelectJoinModel (SelectModel model, string aliasTableName, JoinConnect connect, QueryExpression query, OrderExpression order)
		{
			this._model = model;
			this._connect = connect;
			this._query = query;
			this._order = order;
			this._aliasTableName = aliasTableName;
			this._joinMapping = model.OutputMapping;
		}


		public string CreateSqlString (CommandFactory factory, CreateSqlState state)
		{
			//StringBuilder sb = new StringBuilder ();
			//CommandData command = factory.CreateSelectCommand (_mapping, _selector, _query, _order, _distinct, null, state);
			//string aliasName = _aliasTableName ?? _mapping.TableName;
			//sb.Append (factory.CreateAliasQuerySql (command.CommandText, aliasName));
			//return sb.ToString ();


			StringBuilder sb = new StringBuilder ();
			CommandData command = factory.CreateSelectCommand (_model.EntityMapping, _model.CreateSelector (), _query, _order, _distinct, null, state);
			string aliasName = _aliasTableName ?? _model.EntityMapping.TableName;
			sb.Append (factory.CreateAliasQuerySql (command.CommandText, aliasName));
			return sb.ToString ();
		}
	}
}
