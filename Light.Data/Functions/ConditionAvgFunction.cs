using System;

namespace Light.Data
{
	class ConditionAvgFunction : AggregateData
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal ConditionAvgFunction (DataFieldInfo fieldinfo, QueryExpression expression, bool isDistinct)
			: base (fieldinfo.TableMapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
			_isDistinct = isDistinct;
		}

		//internal ConditionAvgFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo, bool isDistinct)
		//	: base (mapping)
		//{
		//	_expression = expression;
		//	_fieldinfo = fieldinfo;
		//	_isDistinct = isDistinct;
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
		//	return factory.CreateConditionAvgSql (sql, _fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string expressionString = _expression.CreateSqlString (factory, isFullName, out dataParameters1);
			string sql = factory.CreateConditionAvgSql (expressionString, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters2) : null, _isDistinct);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

//		internal override AggregateFunction CreateAliasTableFunction (string aliasTableName)
//		{
//			DataFieldInfo info = this._fieldinfo.Clone () as DataFieldInfo;
//			info.AliasTableName = aliasTableName;
//			AvgFunction function = new AvgFunction (this.TableMapping, info, this._isDistinct);
//			return function;
//		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		ConditionAvgFunction target = function as ConditionAvgFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct && this._expression.Equals (target._expression);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
