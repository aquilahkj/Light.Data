﻿using System;

namespace Light.Data
{
	class ConditionCountFunction : AggregateFunction
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal ConditionCountFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
			return factory.CreateConditionCountSql (sql, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateDataFieldSql (factory, fullFieldName) : null, _isDistinct);
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if ((this.TableMapping == null && function.TableMapping == null) || base.EqualsDetail (function)) {
				ConditionCountFunction target = function as ConditionCountFunction;
				bool f1 = Object.Equals (this._fieldinfo, null);
				bool f2 = Object.Equals (target._fieldinfo, null);
				if ((f1 && f2) || (!f1 && !f2)) {
					return this._isDistinct == target._isDistinct && this._expression.Equals (target._expression);
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}
	}
}
