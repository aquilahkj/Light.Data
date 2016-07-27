using System;
namespace Light.Data
{
	class LambdaDateFormatDataFieldInfo : LambdaDataFieldInfo
	{
		string _format;

		internal LambdaDateFormatDataFieldInfo (DataFieldInfo info, string format)
			: base (info)
		{
			_format = format;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			return factory.CreateDateTimeFormatSql (field, _format);
		}
	}
}

