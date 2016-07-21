using System;
namespace Light.Data
{
	/// <summary>
	/// Lambda match expression.
	/// </summary>
	class LambdaMatchExpression : QueryExpression, ISupportNotDefine
	{
		LambdaMatchDataFieldInfo _fieldInfo;

		public LambdaMatchExpression (LambdaMatchDataFieldInfo fieldInfo)
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

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				LambdaMatchExpression target = expression as LambdaMatchExpression;
				return this._fieldInfo.Equals (target._fieldInfo);
			}
			else {
				return false;
			}
		}
	}
}

