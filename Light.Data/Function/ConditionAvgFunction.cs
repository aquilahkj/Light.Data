﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class ConditionAvgFunction : AggregateFunction
	{
		QueryExpression _expression = null;

		DataFieldInfo _fieldinfo = null;

		bool _isDistinct = false;

		internal ConditionAvgFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
			return factory.CreateConditionAvgSql (sql, _fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if (base.EqualsDetail (function)) {
				ConditionAvgFunction target = function as ConditionAvgFunction;
				return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct && this._expression.Equals (target._expression);
			}
			else {
				return false;
			}
		}
	}
}
