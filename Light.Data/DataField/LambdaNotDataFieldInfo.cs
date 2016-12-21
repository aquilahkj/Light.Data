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

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			sql = _baseFieldInfo.CreateSqlString (factory, isFullName, state);
			sql = factory.CreateNotSql (sql);

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

