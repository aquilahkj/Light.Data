using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
	class ConditionCountFunction : AggregateFunction
	{
		QueryExpression _expression = null;

		DataFieldInfo _fieldinfo = null;

		bool _isDistinct = false;

		internal ConditionCountFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string sql = _expression.CreateSqlString (factory, out dataParameters);
			return factory.CreateConditionCountSql (sql, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.FieldName : null, _isDistinct);
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
