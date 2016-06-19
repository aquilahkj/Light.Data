
namespace Light.Data
{
	class ExistsQueryExpression : QueryExpression
	{
		QueryExpression _queryExpression;

		bool _isNot;

		public ExistsQueryExpression (QueryExpression expression, bool isNot)
			: base (expression.TableMapping)
		{
			_queryExpression = expression;
			_isNot = isNot;
		}


		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters);
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
