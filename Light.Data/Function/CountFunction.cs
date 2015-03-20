using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
	class CountFunction : AggregateFunction
	{
		DataFieldInfo _fieldinfo = null;
		bool _isDistinct = false;

		internal CountFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
			: base (mapping)
		{
			_fieldinfo = fieldInfo;
			_isDistinct = isDistinct;
		}


		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateCountSql (_fieldinfo.FieldName, _isDistinct);
		}

		protected override bool EqualsDetail (AggregateFunction function)
		{
			if (base.EqualsDetail (function)) {
				CountFunction target = function as CountFunction;
				return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct;
			}
			else {
				return false;
			}
		}
	}
}
