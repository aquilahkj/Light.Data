using System;
namespace Light.Data
{
	class SelectDataFieldInfo : LambdaDataFieldInfo, IAliasDataFieldInfo
	{
		DataFieldInfo _fieldInfo;

		string _aliasName;


		public SelectDataFieldInfo (DataFieldInfo fieldInfo, string name)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_aliasName = name;
		}

		public DataFieldInfo FieldInfo {
			get {
				return _fieldInfo;
			}
		}

		public string AliasName {
			get {
				return _aliasName;
			}

			set {
				_aliasName = value;
			}
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, state);
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string fieldSql = _fieldInfo.CreateSqlString (factory, isFullName, state);
			string sql = factory.CreateAliasFieldSql (fieldSql, _aliasName);
			return sql;
		}
	}
}
