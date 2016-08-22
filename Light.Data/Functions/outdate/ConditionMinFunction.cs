using System;

namespace Light.Data
{
	class ConditionMinFunction: AggregateData
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		internal ConditionMinFunction (DataFieldInfo fieldinfo, QueryExpression expression)
			: base (fieldinfo.TableMapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	DataParameter [] dataParameters1 = null;
		//	DataParameter [] dataParameters2 = null;
		//	string expressionString = _expression.CreateSqlString (factory, isFullName, out dataParameters1);
		//	string sql = factory.CreateConditionMinSql (expressionString, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateSqlString (factory, isFullName, out dataParameters2) : null);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string expressionString = _expression.CreateSqlString (factory, isFullName, state);
			string sql = factory.CreateConditionMinSql (expressionString, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateSqlString (factory, isFullName, state) : null);
			return sql;
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if ((this.TableMapping == null && function.TableMapping == null) || base.EqualsDetail (function)) {
		//		ConditionMinFunction target = function as ConditionMinFunction;
		//		bool f1 = Object.Equals (this._fieldinfo, null);
		//		bool f2 = Object.Equals (target._fieldinfo, null);
		//		if ((f1 && f2) || (!f1 && !f2)) {
		//			return this._expression.Equals (target._expression);
		//		}
		//		else {
		//			return false;
		//		}
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}

