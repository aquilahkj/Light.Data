using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class SingleParamQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		QueryPredicate _predicate;

		object _value = null;

		bool _isReverse = false;

		public SingleParamQueryExpression (DataFieldInfo fieldInfo, QueryPredicate predicate, object value, bool isReverse)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_value = value;
			_isReverse = isReverse;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _fieldInfo.DataField.ToColumn (_value), _fieldInfo.DBType);
			dataParameters = new DataParameter[] { dataParameter };
			return factory.CreateSingleParamSql (_fieldInfo.CreateDataFieldSql (factory), _predicate, _isReverse, dataParameter);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				SingleParamQueryExpression target = expression as SingleParamQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& Object.Equals (this._value, target._value)
				&& this._predicate == target._predicate
				&& this._isReverse == target._isReverse;
			}
			else {
				return false;
			}
		}
	}
}
