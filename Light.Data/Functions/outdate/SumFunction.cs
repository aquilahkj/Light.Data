
namespace Light.Data
{
	class SumFunction : AggregateData
	{
		DataFieldInfo _fieldinfo;

		bool _isDistinct;

		internal SumFunction (DataFieldInfo fieldInfo, bool isDistinct)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
			_isDistinct = isDistinct;
		}

		//internal SumFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
		//	: base (mapping)
		//{
		//	_fieldinfo = fieldInfo;
		//	_isDistinct = isDistinct;
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateSumSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName), _isDistinct);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return factory.CreateSumSql (_fieldinfo.CreateSqlString (factory, isFullName, out dataParameters), _isDistinct);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return factory.CreateSumSql (_fieldinfo.CreateSqlString (factory, isFullName, state), _isDistinct);
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		SumFunction target = function as SumFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo) && this._isDistinct == target._isDistinct;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
