namespace Light.Data
{
	class LambdaSubQueryExpression : QueryExpression
	{
		readonly LambdaSubQueryDataFieldInfo _fieldInfo;

		public LambdaSubQueryExpression (LambdaSubQueryDataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			this._fieldInfo = fieldInfo;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, state);
		}
	}
}
