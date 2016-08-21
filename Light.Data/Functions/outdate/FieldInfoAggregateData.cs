using System;
namespace Light.Data
{
	public class FieldInfoAggregateData:AggregateData
	{
		DataFieldInfo _fieldinfo;

		internal FieldInfoAggregateData (DataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldinfo.CreateSqlString (factory, isFullName, state);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _fieldinfo.CreateSqlString (factory, isFullName, out dataParameters);
		}
	}
}

