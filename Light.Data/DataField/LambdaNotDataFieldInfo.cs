using System;
namespace Light.Data
{
	class LambdaNotDataFieldInfo : LambdaDataFieldInfo
	{
		public LambdaNotDataFieldInfo (DataFieldInfo info)
			: base (info)
		{
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = base.BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			sql = factory.CreateLambdaNotSql (sql);
			return sql;
		}
	}
}

