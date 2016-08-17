
namespace Light.Data
{
	class NullQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo;

		bool _isNull;

		public NullQueryExpression (DataFieldInfo fieldInfo, bool isNull)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_isNull = isNull;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = new DataParameter[0];
		//	return factory.CreateNullQuerySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _isNull);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return factory.CreateNullQuerySql (_fieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters), _isNull);
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		NullQueryExpression target = expression as NullQueryExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo)
		//		&& this._isNull == target._isNull;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
