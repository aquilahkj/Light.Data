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

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			return _fieldInfo.CreateDataFieldSql (factory, fullFieldName, out dataParameters);
		}
	}
}

