using System;
namespace Light.Data
{
	/// <summary>
	/// Lambda match expression.
	/// </summary>
	class LambdaMatchQuerryExpression : QueryExpression, ISupportNotDefine
	{
		LambdaStringMatchDataFieldInfo _fieldInfo;

		public LambdaMatchQuerryExpression (LambdaStringMatchDataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			this._fieldInfo = fieldInfo;
		}

		public void SetNot ()
		{
			this._fieldInfo.SetNot ();
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _fieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		LambdaMatchExpression target = expression as LambdaMatchExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}

