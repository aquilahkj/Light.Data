using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class FieldOrderExpression : OrderExpression
	{
		DataFieldInfo _fieldInfo = null;

		OrderType _orderType = OrderType.ASC;

		public FieldOrderExpression (DataFieldInfo fieldInfo, OrderType orderType)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_orderType = orderType;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateOrderBySql (_fieldInfo.CreateDataFieldSql (factory), _orderType);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_fieldInfo);
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
				FieldOrderExpression exp = target as FieldOrderExpression;
				return this._fieldInfo.Equals (exp._fieldInfo) && this._orderType == exp._orderType;
			}
			else {
				return false;
			}
		}
	}
}
