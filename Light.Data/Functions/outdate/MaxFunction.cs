
namespace Light.Data
{
	class MaxFunction : AggregateData
	{
		DataFieldInfo _fieldinfo;

		internal MaxFunction (DataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	return factory.CreateMaxSql (_fieldinfo.CreateSqlString (factory, isFullName, out dataParameters));
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return factory.CreateMaxSql (_fieldinfo.CreateSqlString (factory, isFullName, state));
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
