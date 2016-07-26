using System;

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

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
		//	return factory.CreateConditionSumSql (sql, _fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string expressionString = _expression.CreateSqlString (factory, fullFieldName, out dataParameters1);
			string sql = factory.CreateConditionSumSql (expressionString, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateDataFieldSql (factory, fullFieldName, out dataParameters2) : null, _isDistinct);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}


		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		ConditionSumFunction target = function as ConditionSumFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct && this._expression.Equals (target._expression);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
