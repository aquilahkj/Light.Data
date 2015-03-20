using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class NullQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		bool _isNull = false;

		public NullQueryExpression (DataFieldInfo fieldInfo, bool isNull)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_isNull = isNull;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateNullQuerySql (_fieldInfo.CreateDataFieldSql (factory), _isNull);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				NullQueryExpression target = expression as NullQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._isNull == target._isNull;
			}
			else {
				return false;
			}
		}
	}
}
