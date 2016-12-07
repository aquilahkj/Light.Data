using System;
namespace Light.Data
{
	class LambdaExistsDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		bool _isExists;

		QueryExpression _expression;

		public LambdaExistsDataFieldInfo (DataEntityMapping mapping, QueryExpression expression, bool isExists)
			: base (mapping)
		{
			_expression = expression;
			_isExists = isExists;
		}

		public void SetNot ()
		{
			_isExists = !_isExists;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}
			string query = _expression.CreateSqlString (factory, true, state);

			sql = factory.CreateExistsQuerySql (TableMapping.TableName, query, !_isExists);

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaExistsQueryExpression (this);
		}
	}
}
