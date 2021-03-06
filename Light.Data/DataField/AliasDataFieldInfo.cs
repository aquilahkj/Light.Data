﻿using System;

namespace Light.Data
{
	/// <summary>
	/// Alias data field info.
	/// </summary>
	class AliasDataFieldInfo:DataFieldInfo
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

		/// <summary>
		/// Creates the data field sql.
		/// </summary>
		/// <returns>The data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> is full name.</param>
		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			return _baseFieldInfo.CreateDataFieldSql (factory, isFullName);
		}

		/// <summary>
		/// Creates the alias data field sql.
		/// </summary>
		/// <returns>The alias data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> is full name.</param>
		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName)
		{
//			if (isFullName) {
//				string tableName = this._aliasTableName ?? TableMapping.TableName;
//				string field = _baseFieldInfo.CreateDataFieldSql( (factory);factory.CreateFullDataFieldSql (tableName, FieldName);
//				return factory.CreateAliasSql (field, this._alias);
//			}
//			else {
//				string field = _baseFieldInfo.CreateDataFieldSql (factory);
//				return factory.CreateAliasSql (field, this._alias);
//			}
			string field = _baseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateAliasSql (field, this._alias);
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			AliasDataFieldInfo target = info as AliasDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._baseFieldInfo.Equals (target._baseFieldInfo) && this.Alias == target.Alias;
			}
			else {
				return false;
			}
		}

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

