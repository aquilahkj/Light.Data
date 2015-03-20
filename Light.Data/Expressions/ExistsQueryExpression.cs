using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class ExistsQueryExpression : QueryExpression
	{
		QueryExpression _queryExpression = null;

		bool _isNot = false;

		public ExistsQueryExpression (QueryExpression expression, bool isNot)
			: base (expression.TableMapping)
		{
			_queryExpression = expression;
			_isNot = isNot;
			IgnoreConsistency = true;
		}


		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string queryString = _queryExpression.CreateSqlString (factory, out dataParameters);
			return factory.CreateExistsQuerySql (factory.CreateDataTableSql (_queryExpression.TableMapping), queryString, _isNot);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				ExistsQueryExpression target = expression as ExistsQueryExpression;
				return this._queryExpression.Equals (target._queryExpression)
				&& this._isNot == target._isNot;
			}
			else {
				return false;
			}
		}
	}
}
