namespace Light.Data
{
	class LambdaNullQueryExpression : QueryExpression, ISupportNotDefine
	{
		readonly LambdaNullDataFieldInfo _fieldInfo;

		//public LambdaNullQueryExpression (DataFieldInfo info, bool isNull)
		//	: base (info.TableMapping)
		//{
		//	this._fieldInfo = new LambdaNullDataFieldInfo (info, isNull);
		//}

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
