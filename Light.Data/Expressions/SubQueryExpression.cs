using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class SubQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		QueryCollectionPredicate _predicate;

		QueryExpression _queryExpression = null;

		DataFieldInfo _queryFieldInfo = null;

		public SubQueryExpression (DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_queryFieldInfo = queryFieldInfo;
			_queryExpression = queryExpression;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			string queryString = null;
			if (_queryExpression == null) {
				dataParameters = new DataParameter[0];
			}
			else {
				queryString = _queryExpression.CreateSqlString (factory, fullFieldName, out dataParameters);
			}
			return factory.CreateSubQuerySql (_fieldInfo.CreateDataFieldSql (factory), _predicate, _queryFieldInfo.CreateDataFieldSql (factory), factory.CreateDataTableSql (_queryFieldInfo.TableMapping), queryString);
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
