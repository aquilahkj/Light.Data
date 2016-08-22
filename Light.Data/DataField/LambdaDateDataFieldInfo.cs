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

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string field = _baseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//	return factory.CreateDateSql (field);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, state);
			sql = factory.CreateDateSql (field);
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

