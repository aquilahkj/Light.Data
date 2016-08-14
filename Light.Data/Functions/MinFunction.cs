
namespace Light.Data
{
	class MinFunction : AggregateData
	{
		DataFieldInfo _fieldinfo;

		internal MinFunction (DataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
		}

		//internal MinFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo)
		//	: base (mapping)
		//{
		//	_fieldinfo = fieldInfo;
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateMinSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName));
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return factory.CreateMinSql (_fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters));
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		MinFunction target = function as MinFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
