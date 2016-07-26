
namespace Light.Data
{
	class MaxFunction : AggregateFunction
	{
		DataFieldInfo _fieldinfo;

		internal MaxFunction (DataEntityMapping mapping, DataFieldInfo fieldInfo)
			: base (mapping)
		{
			_fieldinfo = fieldInfo;
		}


		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateMaxSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName));
		//}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			return factory.CreateMaxSql (_fieldinfo.CreateDataFieldSql (factory, fullFieldName, out dataParameters));
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	if (base.EqualsDetail (function)) {
		//		MaxFunction target = function as MaxFunction;
		//		return this._fieldinfo.Equals (target._fieldinfo);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
