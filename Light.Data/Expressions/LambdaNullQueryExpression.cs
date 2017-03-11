namespace Light.Data
{
	class LambdaNullQueryExpression : QueryExpression, ISupportNotDefine
	{
		readonly LambdaNullDataFieldInfo _fieldInfo;

		public LambdaNullQueryExpression (LambdaNullDataFieldInfo fieldInfo)
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
