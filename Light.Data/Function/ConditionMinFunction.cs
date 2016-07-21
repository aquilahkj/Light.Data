using System;

namespace Light.Data
{
	class ConditionMinFunction: AggregateFunction
	{
		QueryExpression _expression;

		DataFieldInfo _fieldinfo;

		internal ConditionMinFunction (DataEntityMapping mapping, QueryExpression expression, DataFieldInfo fieldinfo)
			: base (mapping)
		{
			_expression = expression;
			_fieldinfo = fieldinfo;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	string sql = _expression.CreateSqlString (factory, fullFieldName, out dataParameters);
		//	return factory.CreateConditionMinSql (sql, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateDataFieldSql (factory, fullFieldName) : null);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string expressionString = _expression.CreateSqlString (factory, fullFieldName, out dataParameters1);
			string sql = factory.CreateConditionMinSql (expressionString, !Object.Equals (this._fieldinfo, null) ? _fieldinfo.CreateDataFieldSql (factory, fullFieldName, out dataParameters2) : null);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if ((this.TableMapping == null && function.TableMapping == null) || base.EqualsDetail (function)) {
				ConditionMinFunction target = function as ConditionMinFunction;
				bool f1 = Object.Equals (this._fieldinfo, null);
				bool f2 = Object.Equals (target._fieldinfo, null);
				if ((f1 && f2) || (!f1 && !f2)) {
					return this._expression.Equals (target._expression);
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

