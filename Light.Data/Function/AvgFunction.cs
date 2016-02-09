using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class AvgFunction : AggregateFunction
	{
		DataFieldInfo _fieldinfo = null;

		bool _isDistinct = false;

		internal AvgFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
			: base (mapping)
		{
			_fieldinfo = fieldInfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = null;
			return factory.CreateAvgSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if (base.EqualsDetail (function)) {
				AvgFunction target = function as AvgFunction;
				return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct;
			}
			else {
				return false;
			}
		}
	}
}
