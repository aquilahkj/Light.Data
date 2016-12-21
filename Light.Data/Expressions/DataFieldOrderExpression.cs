using System;

namespace Light.Data
{
	class DataFieldOrderExpression : OrderExpression
	{
		DataFieldInfo _fieldInfo;

		OrderType _orderType = OrderType.ASC;

		public DataFieldOrderExpression (DataFieldInfo fieldInfo, OrderType orderType)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_orderType = orderType;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string fieldSql = _fieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//	return factory.CreateOrderBySql (fieldSql, _orderType);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string fieldSql = _fieldInfo.CreateSqlString (factory, isFullName, state);
			return factory.CreateOrderBySql (fieldSql, _orderType);
		}

		internal override OrderExpression CreateAliasTableNameOrder (string aliasTableName)
		{
			//DataFieldInfo info = this._fieldInfo.Clone () as DataFieldInfo;
			//info.AliasTableName = aliasTableName;

			DataFieldInfo info = this._fieldInfo.CreateAliasTableInfo (aliasTableName);
			return new DataFieldOrderExpression (info, this._orderType);
		}

		//public override bool Equals (OrderExpression target)
		//{
		//	if (Object.Equals (target, null)) {
		//		return false;
		//	}
		//	if (Object.ReferenceEquals (this, target)) {
		//		return true;
		//	}
		//	if (this.GetType () == target.GetType ()) {
		//		FieldOrderExpression exp = target as FieldOrderExpression;
		//		return this._fieldInfo.Equals (exp._fieldInfo) && this._orderType == exp._orderType;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
