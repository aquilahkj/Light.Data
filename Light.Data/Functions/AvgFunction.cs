
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

		//internal AvgFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
		//	: base (mapping)
		//{
		//	_fieldinfo = fieldInfo;
		//	_isDistinct = isDistinct;
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateAvgSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return factory.CreateAvgSql (_fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters), _isDistinct);
		}

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
