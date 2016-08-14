using System;
namespace Light.Data
{
	public class AggregateDataFieldInfo:AggregateData
	{
		DataFieldInfo _fieldinfo;

		internal AggregateDataFieldInfo (DataFieldInfo fieldInfo)
			: base (fieldInfo.TableMapping)
		{
			_fieldinfo = fieldInfo;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
		}
	}
}

