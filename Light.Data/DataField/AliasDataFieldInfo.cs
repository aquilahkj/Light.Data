namespace Light.Data
{
	/// <summary>
	/// Alias data field info.
	/// </summary>
	class AliasDataFieldInfo : DataFieldInfo, IAliasDataFieldInfo
	{
		readonly DataFieldInfo _baseFieldInfo;

		readonly string _aliasName;

		/// <summary>
		/// Gets the alias.
		/// </summary>
		/// <value>The alias.</value>
		public string AliasName {
			get {
				return _aliasName;
			}
		}

		/// <summary>
		/// Gets the base field info.
		/// </summary>
		/// <value>The base field info.</value>
		public DataFieldInfo BaseFieldInfo {
			get {
				return _baseFieldInfo;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AliasDataFieldInfo"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="alias">Alias.</param>
		internal AliasDataFieldInfo (DataFieldInfo info, string alias)
			: base (info.TableMapping, info.DataField)
		{
			_baseFieldInfo = info;
			_aliasName = alias;
		}

		internal AliasDataFieldInfo (DataFieldInfo info, string alias, string aliasTableName)
			: base (info.TableMapping, info.DataField, aliasTableName)
		{
			_baseFieldInfo = info;
			_aliasName = alias;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			if (isFullName) {
				string tableName = this._aliasTableName ?? TableMapping.TableName;
				return factory.CreateFullDataFieldSql (tableName, FieldName);
			}
			else {
				return factory.CreateDataFieldSql (FieldName);
			}
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field;
			if (isFullName) {
				string tableName = this._aliasTableName ?? TableMapping.TableName;
				field = factory.CreateFullDataFieldSql (tableName, FieldName);
			}
			else {
				field = factory.CreateDataFieldSql (FieldName);
			}

			return factory.CreateAliasFieldSql (field, this._aliasName);
		}
	}
}

