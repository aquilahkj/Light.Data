using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate order expression.
	/// </summary>
	class AggregateOrderExpression : OrderExpression
	{
		AggregateFunction _function;
		OrderType _orderType = OrderType.ASC;

		public AggregateOrderExpression (AggregateFunction function, OrderType orderType)
			: base (function.TableMapping)
		{
			_function = function;
			_orderType = orderType;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string functionSql = _function.CreateSqlString (factory, fullFieldName, out dataParameters);
			return factory.CreateOrderBySql (functionSql, _orderType);
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, fullFieldName, out dataParameters);
			}
			dataParameters = null; 
			string name = factory.CreateDataFieldSql (alise);
			return factory.CreateOrderBySql (name, _orderType);
		}

		public override bool Equals (OrderExpression target)
		{
			if (Object.Equals (target, null)) {
				return false;
			}
			if (Object.ReferenceEquals (this, target)) {
				return true;
			}
			if (this.GetType () == target.GetType ()) {
				AggregateOrderExpression exp = target as AggregateOrderExpression;
				return this._function.Equals (exp._function) && this._orderType == exp._orderType;
			}
			else {
				return false;
			}
		}
	}
}
