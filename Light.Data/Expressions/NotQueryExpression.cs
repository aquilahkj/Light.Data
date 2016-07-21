using System;
namespace Light.Data
{
	class NotQueryExpression : QueryExpression
	{
		QueryExpression _queryExpression;


		public NotQueryExpression (QueryExpression expression)
			: base (expression.TableMapping)
		{
			_queryExpression = expression;
		}


		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			string queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters);
			return factory.CreateNotQuerySql (queryString);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				NotQueryExpression target = expression as NotQueryExpression;
				return this._queryExpression.Equals (target._queryExpression);
			}
			else {
				return false;
			}
		}
	}
}

