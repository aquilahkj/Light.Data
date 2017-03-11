namespace Light.Data
{
	class LambdaExistsQueryExpression : QueryExpression, ISupportNotDefine
	{
		readonly LambdaExistsDataFieldInfo _fieldInfo;

		public LambdaExistsQueryExpression (LambdaExistsDataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			this._fieldInfo = fieldInfo;
		}

		public void SetNot ()
		{
			this._fieldInfo.SetNot ();
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, state);
		}
	}
}
