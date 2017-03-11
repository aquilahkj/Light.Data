namespace Light.Data
{
	class LambdaInQueryExpression : QueryExpression, ISupportNotDefine
	{
		readonly LambdaInQueryDataFieldInfo _fieldInfo;

		public LambdaInQueryExpression (LambdaInQueryDataFieldInfo fieldInfo)
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
