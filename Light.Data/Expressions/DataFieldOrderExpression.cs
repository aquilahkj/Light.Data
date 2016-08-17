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

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = new DataParameter[0];
		//	return factory.CreateOrderBySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _orderType);
		//}


		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string fieldSql = _fieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			return factory.CreateOrderBySql (fieldSql, _orderType);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters, GetAliasHandler handler)
		//{
		//	string alise = handler (_fieldInfo);
		//	if (string.IsNullOrEmpty (alise)) {
		//		return CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	dataParameters = new DataParameter [0];
		//	string name = factory.CreateDataFieldSql (alise);
		//	return factory.CreateOrderBySql (name, _orderType);
		//}

		//		internal override string CreateSqlString (CommandFactory factory, string aliasTableName, out DataParameter[] dataParameters)
		//		{
		//			dataParameters = new DataParameter[0];
		//			return factory.CreateOrderBySql (_fieldInfo.CreateDataFieldSql (factory, aliasTableName), _orderType);
		//		}

		internal override OrderExpression CreateAliasTableNameOrder (string aliasTableName)
		{
			DataFieldInfo info = this._fieldInfo.Clone () as DataFieldInfo;
			info.AliasTableName = aliasTableName;
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
