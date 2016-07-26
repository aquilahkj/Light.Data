using System.Collections.Generic;

namespace Light.Data
{
	class SubAggregateExpression : AggregateHavingExpression
	{
		AggregateFunction _function;

		QueryCollectionPredicate _predicate;

		QueryExpression _queryExpression;

		DataFieldInfo _queryFieldInfo;

		public SubAggregateExpression (AggregateFunction function, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
			: base (function.TableMapping)
		{
			_function = function;
			_predicate = predicate;
			_queryFieldInfo = queryFieldInfo;
			_queryExpression = queryExpression;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	string queryString;
		//	List<DataParameter> list = new List<DataParameter> ();
		//	DataParameter[] ps;
		//	string functionSql = _function.CreateSqlString (factory, fullFieldName, out ps);
		//	if (ps != null && ps.Length > 0) {
		//		list.AddRange (ps);
		//	}

		//	DataParameter[] ps2;
		//	queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out ps2);
		//	if (ps2 != null && ps2.Length > 0) {
		//		list.AddRange (ps2);
		//	}
		//	dataParameters = list.ToArray ();
		//	return factory.CreateSubQuerySql (functionSql, _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			string queryString;
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			DataParameter [] dataParameters3 = null;
			string functionSql = _function.CreateSqlString (factory, fullFieldName, out dataParameters1);
			queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters2);

			string sql = factory.CreateSubQuerySql (functionSql, _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false, out dataParameters3), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2, dataParameters3);
			return sql;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters, GetAliasHandler handler)
		//{
		//	string alise = handler (_function);
		//	if (string.IsNullOrEmpty (alise)) {
		//		return CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	string name = factory.CreateDataFieldSql (alise);
		//	string queryString;
		//	queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters);
		//	return factory.CreateSubQuerySql (name, _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, fullFieldName, out dataParameters);
			}
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string name = factory.CreateDataFieldSql (alise);
			string queryString;
			queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters1);
			string sql = factory.CreateSubQuerySql (name, _predicate, _queryFieldInfo.CreateDataFieldSql (factory, false, out dataParameters2), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		//protected override bool EqualsDetail (AggregateHavingExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		SubAggregateExpression target = expression as SubAggregateExpression;
		//		return this._function.Equals (target._function)
		//		&& this._predicate == target._predicate
		//		&& this._queryExpression.Equals (target._queryExpression)
		//		&& this._queryFieldInfo.Equals (target._queryFieldInfo);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
