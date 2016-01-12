using System;

namespace Light.Data
{
	class AliasDataFieldInfo:DataFieldInfo
	{
		DataFieldInfo _baseFieldInfo;

		string _alias;

		public string Alias {
			get {
				return _alias;
			}
		}

		/// <summary>
		/// 原始字段信息
		/// </summary>
		public DataFieldInfo BaseFieldInfo {
			get {
				return _baseFieldInfo;
			}
		}

		internal AliasDataFieldInfo (DataFieldInfo info, string alias)
			: base (info.DataField)
		{
			_baseFieldInfo = info;
			_alias = alias;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
//			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
//			return factory.CreateAliasSql (field, this._alias);
			return BaseFieldInfo.CreateDataFieldSql (factory, isFullName);;
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
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
	}
}

