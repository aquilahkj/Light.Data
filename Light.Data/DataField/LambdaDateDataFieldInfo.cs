using System;
namespace Light.Data
{
	class LambdaDateDataFieldInfo : LambdaDataFieldInfo
	{
		public LambdaDateDataFieldInfo (DataFieldInfo info)
			: base (info)
		{
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			return factory.CreateDateSql (field);
		}
	}
}

