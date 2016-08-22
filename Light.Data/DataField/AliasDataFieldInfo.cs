using System;

namespace Light.Data
{
	/// <summary>
	/// Alias data field info.
	/// </summary>
	class AliasDataFieldInfo : DataFieldInfo, IAliasDataFieldInfo
	{
		DataFieldInfo _baseFieldInfo;

		string _alias;

		/// <summary>
		/// Gets the alias.
		/// </summary>
		/// <value>The alias.</value>
		public string Alias {
			get {
				return _alias;
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
			: base (info.DataField)
		{
			_baseFieldInfo = info;
			_alias = alias;
		}

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	return _baseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//}

		/// <summary>
		/// Creates the data field sql.
		/// </summary>
		/// <returns>The data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> is full name.</param>
		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	return _baseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _baseFieldInfo.CreateSqlString (factory, isFullName, state);
		}

		/// <summary>
		/// Creates the alias data field sql.
		/// </summary>
		/// <returns>The alias data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> is full name.</param>
		//internal string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = _baseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	return factory.CreateAliasSql (field, this._alias);
		//}

		/// <summary>
		/// Creates the alias data field sql.
		/// </summary>
		/// <returns>The alias data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">Is full name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		//public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string field = _baseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		//	return factory.CreateAliasSql (field, this._alias);
		//}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, state);
			return factory.CreateAliasSql (field, this._alias);
		}

		///// <summary>
		///// 匹配细节内容是否相等
		///// </summary>
		///// <param name="info">匹配对象</param>
		///// <returns></returns>
		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	AliasDataFieldInfo target = info as AliasDataFieldInfo;
		//	if (!Object.Equals (target, null)) {
		//		return this._baseFieldInfo.Equals (target._baseFieldInfo) && this.Alias == target.Alias;
		//	}
		//	else {
		//		return false;
		//	}
		//}

		internal override string AliasTableName {
			get {
				return this._baseFieldInfo.AliasTableName;
			}
			set {
				this._baseFieldInfo.AliasTableName = value;
			}
		}
	}
}

