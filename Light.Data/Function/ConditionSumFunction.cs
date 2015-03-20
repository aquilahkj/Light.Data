using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
	class ConditionSumFunction : AggregateFunction
	{
		QueryExpression _expression = null;

		DataFieldInfo _fieldinfo = null;

		bool _isDistinct = false;

		internal ConditionSumFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string sql = _expression.CreateSqlString (factory, out dataParameters);
			return factory.CreateConditionSumSql (sql, _fieldinfo.FieldName, _isDistinct);
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if (base.EqualsDetail (function)) {
				ConditionSumFunction target = function as ConditionSumFunction;
				return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct && this._expression.Equals (target._expression);
			}
			else {
				return false;
			}
		}
	}
}
