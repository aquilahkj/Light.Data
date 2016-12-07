using System;
namespace Light.Data
{
	class LambdaNotQueryExpression : QueryExpression
	{
		QueryExpression _queryExpression;

		public LambdaNotQueryExpression (QueryExpression expression)
			: base (expression.TableMapping)
		{
			_queryExpression = expression;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string queryString = _queryExpression.CreateSqlString (factory, isFullName, state);
			return factory.CreateNotQuerySql (queryString);
		}
	}
}

