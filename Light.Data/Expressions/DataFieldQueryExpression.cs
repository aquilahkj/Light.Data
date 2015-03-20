using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class DataFieldQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		QueryPredicate _predicate;

		DataFieldInfo _relateFieldInfo = null;

		bool _isReverse = false;

		bool _isSameTable = false;

		public DataFieldQueryExpression (DataFieldInfo fieldInfo, QueryPredicate predicate, DataFieldInfo relateFieldInfo, bool isReverse)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_relateFieldInfo = relateFieldInfo;
			_isReverse = isReverse;
			_isSameTable = Object.Equals (fieldInfo.TableMapping, relateFieldInfo.TableMapping);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRelationTableSql (_fieldInfo.CreateDataFieldSql (factory), _predicate, _isReverse, _relateFieldInfo.CreateDataFieldSql (factory, !_isSameTable));
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				DataFieldQueryExpression target = expression as DataFieldQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._relateFieldInfo.Equals (target._relateFieldInfo)
				&& this._predicate == target._predicate
				&& this._isReverse == target._isReverse;
			}
			else {
				return false;
			}
		}
	}
}
