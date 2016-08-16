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

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _fieldinfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
		}
	}
}

