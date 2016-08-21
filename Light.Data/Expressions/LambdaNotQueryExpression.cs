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


		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string queryString = _queryExpression.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateNotQuerySql (queryString);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string queryString = _queryExpression.CreateSqlString (factory, isFullName, state);
			return factory.CreateNotQuerySql (queryString);
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		NotQueryExpression target = expression as NotQueryExpression;
		//		return this._queryExpression.Equals (target._queryExpression);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}

