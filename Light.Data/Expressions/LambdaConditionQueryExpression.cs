namespace Light.Data
{
	class LambdaConditionQueryExpression : QueryExpression, ISupportNotDefine
	{
		readonly LambdaConditionDataFieldInfo _fieldInfo;

		public LambdaConditionQueryExpression (LambdaConditionDataFieldInfo fieldInfo)
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

