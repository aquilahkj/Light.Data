
namespace Light.Data
{
	class SubQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo;

		QueryCollectionPredicate _predicate;

		QueryExpression _queryExpression;

		DataFieldInfo _queryFieldInfo;

		public SubQueryExpression (DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_queryFieldInfo = queryFieldInfo;
			_queryExpression = queryExpression;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		//{
		//	string queryString = null;
		//	if (_queryExpression == null) {
		//		dataParameters = new DataParameter [0];
		//	}
		//	else {
		//		queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	return factory.CreateSubQuerySql (_fieldInfo.CreateDataFieldSql (factory, false), _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			string queryString = null;
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			DataParameter [] dataParameters3 = null;
			if (_queryExpression == null) {
				dataParameters1 = new DataParameter [0];
			}
			else {
				queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters1);
			}
			string sql = factory.CreateSubQuerySql (_fieldInfo.CreateDataFieldSql (factory, false, out dataParameters2), _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false, out dataParameters3), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2, dataParameters3);
			return sql;
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				SubQueryExpression target = expression as SubQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._predicate == target._predicate
				&& this._queryExpression.Equals (target._queryExpression)
				&& this._queryFieldInfo.Equals (target._queryFieldInfo);
			}
			else {
				return false;
			}
		}
	}
}
