
namespace Light.Data
{
	class CountFunction : AggregateData
	{
		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal CountFunction (DataFieldInfo fieldInfo, bool isDistinct)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
			_isDistinct = isDistinct;
		}

		//internal CountFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
		//	: base (mapping)
		//{
		//	_fieldinfo = fieldInfo;
		//	_isDistinct = isDistinct;
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateCountSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return factory.CreateCountSql (_fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters), _isDistinct);
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		CountFunction target = function as CountFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
