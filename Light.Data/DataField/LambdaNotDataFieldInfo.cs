using System;
namespace Light.Data
{
	class LambdaNotDataFieldInfo : LambdaDataFieldInfo
	{
		DataFieldInfo _baseFieldInfo;

		public LambdaNotDataFieldInfo (DataFieldInfo info)
			: base (info.TableMapping)
		{
			_baseFieldInfo = info;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = _baseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			sql = factory.CreateLambdaNotSql (sql);
			return sql;
		}
	}
}

