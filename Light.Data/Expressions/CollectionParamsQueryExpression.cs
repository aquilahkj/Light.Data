using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class CollectionParamsQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		QueryCollectionPredicate _predicate;

		IEnumerable _values = null;

		public CollectionParamsQueryExpression (DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, IEnumerable values)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_values = values;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			List<DataParameter> list = new List<DataParameter> ();
			foreach (object value in _values) {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, _fieldInfo.DataField.ToColumn (value), _fieldInfo.DBType));
			}
			dataParameters = list.ToArray ();
			return factory.CreateCollectionParamsQuerySql (_fieldInfo.CreateDataFieldSql (factory), _predicate, list);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				CollectionParamsQueryExpression target = expression as CollectionParamsQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._predicate == target._predicate
				&& Utility.EnumableObjectEquals (this._values, target._values);
			}
			else {
				return false;
			}
		}
	}
}
