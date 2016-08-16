using System;
namespace Light.Data
{
	class LambdaDateDataFieldInfo : LambdaDataFieldInfo
	{
		DataFieldInfo _baseFieldInfo;

		public LambdaDateDataFieldInfo (DataFieldInfo info)
			: base (info.TableMapping)
		{
			_baseFieldInfo = info;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = _baseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			return factory.CreateDateSql (field);
		}
	}
}

