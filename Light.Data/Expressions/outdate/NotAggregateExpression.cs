using System;
namespace Light.Data
{
	class NotAggregateExpression : AggregateHavingExpression
	{
		AggregateHavingExpression _aggregateExpression;

		public NotAggregateExpression (AggregateHavingExpression expression)
			: base (expression.TableMapping)
		{
			_aggregateExpression = expression;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string queryString = _aggregateExpression.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateNotQuerySql (queryString);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string queryString = _aggregateExpression.CreateSqlString (factory, isFullName, state);
			return factory.CreateNotQuerySql (queryString);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters, GetAliasHandler handler)
		//{
		//	string queryString = _aggregateExpression.CreateSqlString (factory, fullFieldName, out dataParameters, handler);
		//	return factory.CreateNotQuerySql (queryString);
		//}

		//protected override bool EqualsDetail (AggregateHavingExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		NotAggregateExpression target = expression as NotAggregateExpression;
		//		return this._aggregateExpression.Equals (target._aggregateExpression);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}

