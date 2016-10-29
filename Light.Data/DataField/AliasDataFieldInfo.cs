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
			: base (info.TableMapping, info.DataField)
		{
			_baseFieldInfo = info;
			_alias = alias;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _baseFieldInfo.CreateSqlString (factory, isFullName, state);
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, state);
			return factory.CreateAliasFieldSql (field, this._alias);
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

