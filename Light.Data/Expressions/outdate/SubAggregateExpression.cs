using System.Collections.Generic;

namespace Light.Data
{
	class SubAggregateExpression : AggregateHavingExpression
	{
		AggregateData _function;

		QueryCollectionPredicate _predicate;

		QueryExpression _queryExpression;

		DataFieldInfo _queryFieldInfo;

		public SubAggregateExpression (AggregateData function, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
			: base (function.TableMapping)
		{
			_function = function;
			_predicate = predicate;
			_queryFieldInfo = queryFieldInfo;
			_queryExpression = queryExpression;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string queryString;
		//	DataParameter [] dataParameters1 = null;
		//	DataParameter [] dataParameters2 = null;
		//	DataParameter [] dataParameters3 = null;
		//	string functionSql = _function.CreateSqlString (factory, isFullName, out dataParameters1);
		//	queryString = _queryExpression.CreateSqlString (factory, isFullName, out dataParameters2);

		//	string sql = factory.CreateSubQuerySql (functionSql, _predicate, _queryFieldInfo.CreateSqlString (factory, false, out dataParameters3), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2, dataParameters3);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string queryString;

			string functionSql = _function.CreateSqlString (factory, isFullName, state);
			queryString = _queryExpression.CreateSqlString (factory, isFullName, state);

			string sql = factory.CreateSubQuerySql (functionSql, _predicate, _queryFieldInfo.CreateSqlString (factory, false, state), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);

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
