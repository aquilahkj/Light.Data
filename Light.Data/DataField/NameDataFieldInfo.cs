using System;
namespace Light.Data
{
	class NameDataFieldInfo : LambdaDataFieldInfo
	{
		readonly string name;

		public NameDataFieldInfo (DataFieldInfo info, string name)
			: base (info.TableMapping)
		{
			this.name = name;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			if (isFullName) {
				string tableName = AliasTableName ?? TableMapping.TableName;
				return factory.CreateFullDataFieldSql (tableName, FieldName);
			}
			else {
				return factory.CreateDataFieldSql (name);
			}
		}

		public override string FieldName {
			get {
				return name;
			}
		}
	}
}

