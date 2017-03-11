namespace Light.Data
{
	class LambdaAggregateCountDataFieldInfo : LambdaAggregateDataFieldInfo
	{
		readonly QueryExpression _expression;

		public LambdaAggregateCountDataFieldInfo ()
			: base (DataEntityMapping.Default)
		{
		}

		public LambdaAggregateCountDataFieldInfo (QueryExpression expression)
			: base (DataEntityMapping.Default)
		{
			_expression = expression;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}
			if (_expression == null) {
				sql = factory.CreateCountAllSql ();
			}
			else {
				string expressionSql = _expression.CreateSqlString (factory, isFullName, state);
				sql = factory.CreateCountAllSql (expressionSql);
			}

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

