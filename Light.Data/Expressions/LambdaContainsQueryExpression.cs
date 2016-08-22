using System;
namespace Light.Data
{
	class LambdaContainsQueryExpression : QueryExpression, ISupportNotDefine
	{
		LambdaContainsDataFieldInfo _fieldInfo;

		public LambdaContainsQueryExpression (LambdaContainsDataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			this._fieldInfo = fieldInfo;
		}

		public void SetNot ()
		{
			this._fieldInfo.SetNot ();
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	return _fieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, state);
		}
	}
}

