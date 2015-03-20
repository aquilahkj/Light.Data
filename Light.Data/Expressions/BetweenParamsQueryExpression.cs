using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class BetweenParamsQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		bool _isNot = false;

		object _fromValue = null;

		object _toValue = null;

		public BetweenParamsQueryExpression (DataFieldInfo fieldInfo, bool isNot, object fromValue, object toValue)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_isNot = isNot;
			_fromValue = fromValue;
			_toValue = toValue;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			string pn1 = factory.CreateTempParamName ();

			DataParameter fromParam = new DataParameter (pn, _fieldInfo.DataField.ToColumn (_fromValue), _fieldInfo.DBType);
			DataParameter toParam = new DataParameter (pn1, _fieldInfo.DataField.ToColumn (_toValue), _fieldInfo.DBType);
			dataParameters = new DataParameter[] { fromParam, toParam };
			return factory.CreateBetweenParamsQuerySql (_fieldInfo.CreateDataFieldSql (factory), _isNot, fromParam, toParam);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				BetweenParamsQueryExpression target = expression as BetweenParamsQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& Object.Equals (this._fromValue, target._fromValue)
				&& Object.Equals (this._toValue, target._toValue)
				&& this._isNot == target._isNot;
			}
			else {
				return false;
			}
		}
	}
}
