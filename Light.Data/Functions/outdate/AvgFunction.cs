
using System;

namespace Light.Data
{
	class AvgFunction : AggregateData
	{
		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal AvgFunction (DataFieldInfo fieldInfo, bool isDistinct)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
			_isDistinct = isDistinct;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return factory.CreateAvgSql (_fieldinfo.CreateSqlString (factory, isFullName, state), _isDistinct);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	return factory.CreateAvgSql (_fieldinfo.CreateSqlString (factory, isFullName, out dataParameters), _isDistinct);
		//}

		//		internal override AggregateFunction CreateAliasTableFunction (string aliasTableName)
		//		{
		//			DataFieldInfo info = this._fieldinfo.Clone () as DataFieldInfo;
		//			info.AliasTableName = aliasTableName;
		//			AvgFunction function = new AvgFunction (this.TableMapping, info, this._isDistinct);
		//			return function;
		//		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		AvgFunction target = function as AvgFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
