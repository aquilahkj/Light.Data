﻿
namespace Light.Data
{
	class ConditionAvgFunction : AggregateFunction
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		bool _isDistinct;

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

//		internal override AggregateFunction CreateAliasTableFunction (string aliasTableName)
//		{
//			DataFieldInfo info = this._fieldinfo.Clone () as DataFieldInfo;
//			info.AliasTableName = aliasTableName;
//			AvgFunction function = new AvgFunction (this.TableMapping, info, this._isDistinct);
//			return function;
//		}

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
