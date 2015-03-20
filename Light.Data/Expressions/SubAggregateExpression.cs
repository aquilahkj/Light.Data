using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Expressions
{
	class SubAggregateExpression : AggregateHavingExpression
	{
		AggregateFunction _function = null;

		QueryCollectionPredicate _predicate;

		QueryExpression _queryExpression = null;

		DataFieldInfo _queryFieldInfo = null;

		public SubAggregateExpression (AggregateFunction function, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
			: base (function.TableMapping)
		{
			_function = function;
			_predicate = predicate;
			_queryFieldInfo = queryFieldInfo;
			_queryExpression = queryExpression;
		}


		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string queryString = null;
			List<DataParameter> list = new List<DataParameter> ();
			DataParameter[] ps = null;
			string functionSql = _function.CreateSqlString (factory, out ps);
			list.AddRange (ps);

			DataParameter[] ps2 = null;
			queryString = _queryExpression.CreateSqlString (factory, out ps2);
			list.AddRange (ps2);

			dataParameters = list.ToArray ();
			return factory.CreateSubQuerySql (functionSql, _predicate, _queryFieldInfo.FieldName, _queryFieldInfo.TableMapping.TableName, queryString);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, out dataParameters);
			}
			string name = factory.CreateDataFieldSql (alise);
			string queryString = null;
			queryString = _queryExpression.CreateSqlString (factory, out dataParameters);
			return factory.CreateSubQuerySql (name, _predicate, _queryFieldInfo.CreateDataFieldSql (factory), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
		}

		protected override bool EqualsDetail (AggregateHavingExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				SubAggregateExpression target = expression as SubAggregateExpression;
				return this._function.Equals (target._function)
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
