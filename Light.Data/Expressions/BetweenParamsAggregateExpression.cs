using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class BetweenParamsAggregateExpression : AggregateHavingExpression
	{
		AggregateFunction _function = null;

		bool _isNot = false;

		object _fromValue = null;

		object _toValue = null;

		public BetweenParamsAggregateExpression (AggregateFunction function, bool isNot, object fromValue, object toValue)
			: base (function.TableMapping)
		{
			_function = function;
			_isNot = isNot;
			_fromValue = fromValue;
			_toValue = toValue;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			string pn1 = factory.CreateTempParamName ();

			DataParameter fromParam = new DataParameter (pn, _fromValue, null);
			DataParameter toParam = new DataParameter (pn1, _toValue, null);
			List<DataParameter> list = new List<DataParameter> ();
			DataParameter[] ps = null;
			string functionSql = _function.CreateSqlString (factory, out ps);
			list.AddRange (ps);
			list.Add (fromParam);
			list.Add (toParam);
			dataParameters = list.ToArray ();
			return factory.CreateBetweenParamsQuerySql (functionSql, _isNot, fromParam, toParam);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, out dataParameters);
			}
			string name = factory.CreateDataFieldSql (alise);

			string pn = factory.CreateTempParamName ();
			string pn1 = factory.CreateTempParamName ();

			DataParameter fromParam = new DataParameter (pn, _fromValue, null);
			DataParameter toParam = new DataParameter (pn1, _toValue, null);
			dataParameters = new DataParameter[] { fromParam, toParam };

			return factory.CreateBetweenParamsQuerySql (name, _isNot, fromParam, toParam);

		}

		protected override bool EqualsDetail (AggregateHavingExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				BetweenParamsAggregateExpression target = expression as BetweenParamsAggregateExpression;
				return this._function.Equals (target._function)
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
