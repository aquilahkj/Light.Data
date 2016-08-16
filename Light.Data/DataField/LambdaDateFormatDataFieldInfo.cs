using System;
namespace Light.Data
{
	class LambdaDateFormatDataFieldInfo : LambdaDataFieldInfo
	{
		string _format;

		DataFieldInfo _baseFieldInfo;

		internal LambdaDateFormatDataFieldInfo (DataFieldInfo info, string format)
			: base (info.TableMapping)
		{
			_baseFieldInfo = info;
			_format = format;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = _baseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			return factory.CreateDateTimeFormatSql (field, _format);
		}
	}
}

