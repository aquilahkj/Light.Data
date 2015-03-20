using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class AggregateOrderExpression : OrderExpression
	{
		AggregateFunction _function = null;
		OrderType _orderType = OrderType.ASC;

		public AggregateOrderExpression (AggregateFunction function, OrderType orderType)
			: base (function.TableMapping)
		{
			_function = function;
			_orderType = orderType;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string functionSql = _function.CreateSqlString (factory, out dataParameters);
			return factory.CreateOrderBySql (functionSql, _orderType);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, out dataParameters);
			}
			dataParameters = new DataParameter[0]; 
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
