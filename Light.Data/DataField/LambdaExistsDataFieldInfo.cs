using System;
namespace Light.Data
{
	class LambdaExistsDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		bool _isTrue;

		QueryExpression _expression;

		public LambdaExistsDataFieldInfo (DataEntityMapping mapping, QueryExpression expression, bool isTrue)
			: base (mapping)
		{
			_expression = expression;
			_isTrue = isTrue;
		}

		public void SetNot ()
		{
			_isTrue = !_isTrue;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}
			string query = _expression.CreateSqlString (factory, true, state);
			string tableName = factory.CreateDataTableSql (TableMapping);
			sql = factory.CreateExistsQuerySql (tableName, query, !_isTrue);

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaExistsQueryExpression (this);
		}
	}
}
