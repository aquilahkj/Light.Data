using System;
namespace Light.Data
{
	class LambdaConditionQueryExpression : QueryExpression, ISupportNotDefine
	{
		LambdaConditionDataFieldInfo _fieldInfo;

		public LambdaConditionQueryExpression (LambdaConditionDataFieldInfo fieldInfo)
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

