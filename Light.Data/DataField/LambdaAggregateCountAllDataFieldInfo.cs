using System;
namespace Light.Data
{
	class LambdaAggregateCountAllDataFieldInfo : LambdaDataFieldInfo
	{
		QueryExpression _query;

		public LambdaAggregateCountAllDataFieldInfo (QueryExpression query)
			: base (DataEntityMapping.Default)
		{
			_query = query;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}
			string expression = null;
			if (_query != null) {
				expression = _query.CreateSqlString (factory, isFullName, state);
				sql = factory.CreateCountAllSql (expression);
			}
			else {
				sql = factory.CreateCountAllSql ();
			}

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

