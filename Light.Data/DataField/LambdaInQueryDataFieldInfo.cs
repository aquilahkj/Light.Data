using System;
namespace Light.Data
{
	class LambdaInQueryDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		bool _isTrue;

		QueryExpression _expression;

		DataFieldInfo _selectField;

		DataFieldInfo _field;

		public LambdaInQueryDataFieldInfo (DataEntityMapping mapping, DataFieldInfo field, DataFieldInfo selectField, QueryExpression expression, bool isTrue)
			: base (mapping)
		{
			_field = field;
			_selectField = selectField;
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

			string tableName = factory.CreateDataTableSql (TableMapping);
			string selectField = _selectField.CreateSqlString (factory, true, state);

			string field = _field.CreateSqlString (factory, isFullName, state);

			string query = null;
			if (_expression != null) {
				query = _expression.CreateSqlString (factory, true, state);
			}
			QueryCollectionPredicate op = _isTrue ? QueryCollectionPredicate.In : QueryCollectionPredicate.NotIn;
			sql = factory.CreateSubQuerySql (field, op, selectField, tableName, query);

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaInQueryExpression (this);
		}
	}
}
