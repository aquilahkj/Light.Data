﻿
namespace Light.Data
{
	class ConditionSumFunction : AggregateFunction
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal ConditionSumFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
			return factory.CreateConditionSumSql (sql, _fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
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
