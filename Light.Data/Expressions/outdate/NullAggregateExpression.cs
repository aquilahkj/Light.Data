
namespace Light.Data
{
	class NullAggregateExpression : AggregateHavingExpression
	{
		AggregateData _function;

		bool _isNull;

		public NullAggregateExpression (AggregateData function, bool isNull)
			: base (function.TableMapping)
		{
			_function = function;
			_isNull = isNull;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter[] dataParameters)
		{
			string functionSql = _function.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateNullQuerySql (functionSql, _isNull);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string functionSql = _function.CreateSqlString (factory, isFullName, state);
			return factory.CreateNullQuerySql (functionSql, _isNull);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		//{
		//	string alise = handler (_function);
		//	if (string.IsNullOrEmpty (alise)) {
		//		return CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	string name = factory.CreateDataFieldSql (alise);
		//	dataParameters = new DataParameter[0];
		//	return factory.CreateNullQuerySql (name, _isNull);
		//}

		//protected override bool EqualsDetail (AggregateHavingExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		NullAggregateExpression target = expression as NullAggregateExpression;
		//		return this._function.Equals (target._function)
		//		&& this._isNull == target._isNull;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
