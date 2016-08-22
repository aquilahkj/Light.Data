
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

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	return factory.CreateMinSql (_fieldinfo.CreateSqlString (factory, isFullName, out dataParameters));
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return factory.CreateMinSql (_fieldinfo.CreateSqlString (factory, isFullName, state));
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
