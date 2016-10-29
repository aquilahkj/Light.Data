using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate order expression.
	/// </summary>
	public class AggregateOrderExpression : OrderExpression
	{
		AggregateData _function;

		OrderType _orderType = OrderType.ASC;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Light.Data.AggregateOrderExpression"/> class.
		/// </summary>
		/// <param name="function">Function.</param>
		/// <param name="orderType">Order type.</param>
		public AggregateOrderExpression (AggregateData function, OrderType orderType)
			: base (function.TableMapping)
		{
			_function = function;
			_orderType = orderType;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string functionSql = _function.CreateSqlString (factory, isFullName, out dataParameters);
		//	return factory.CreateOrderBySql (functionSql, _orderType);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string functionSql = _function.CreateSqlString (factory, isFullName, state);
			return factory.CreateOrderBySql (functionSql, _orderType);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		//{
		//	string alise = handler (_function);
		//	if (string.IsNullOrEmpty (alise)) {
		//		return CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	else {
		//		dataParameters = null;
		//		string name;
		//		if (string.IsNullOrEmpty (this._aliasTableName)) {
		//			name = factory.CreateDataFieldSql (alise);
		//		}
		//		else {
		//			name = factory.CreateFullDataFieldSql (this._aliasTableName, alise);
		//		}
		//		return factory.CreateOrderBySql (name, _orderType);
		//	}
		//}

		//		internal override string CreateSqlString (CommandFactory factory, string aliasTableName, out DataParameter[] dataParameters)
		//		{
		//			string functionSql = _function.CreateSqlString (factory, false, out dataParameters);
		//			return factory.CreateOrderBySql (functionSql, _orderType);
		//		}

		//internal override OrderExpression CreateAliasTableNameOrder (string aliasTableName)
		//{
		//	AggregateOrderExpression expression = new AggregateOrderExpression (this._function, this._orderType);
		//	expression._aliasTableName = aliasTableName;
		//	return expression;
		//}

		//public override bool Equals (OrderExpression target)
		//{
		//	if (Object.Equals (target, null)) {
		//		return false;
		//	}
		//	if (Object.ReferenceEquals (this, target)) {
		//		return true;
		//	}
		//	if (this.GetType () == target.GetType ()) {
		//		AggregateOrderExpression exp = target as AggregateOrderExpression;
		//		return this._function.Equals (exp._function) && this._orderType == exp._orderType && this._aliasTableName == exp._aliasTableName;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
