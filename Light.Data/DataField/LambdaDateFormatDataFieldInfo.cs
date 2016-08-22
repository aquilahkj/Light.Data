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

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string field = _baseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//	return factory.CreateDateTimeFormatSql (field, _format);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, state);
			sql = factory.CreateDateTimeFormatSql (field, _format);
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

