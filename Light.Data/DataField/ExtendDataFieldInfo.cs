﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 扩展字段信息基类,使用数据库自带函数扩展
	/// </summary>
	public abstract class ExtendDataFieldInfo : DataFieldInfo
	{
		DataFieldInfo _baseFieldInfo = null;

		/// <summary>
		/// 原始字段信息
		/// </summary>
		public DataFieldInfo BaseFieldInfo {
			get {
				return _baseFieldInfo;
			}
		}

		internal ExtendDataFieldInfo (DataFieldInfo info)
			: base (info.DataField)
		{
			_baseFieldInfo = info;
		}

		internal override string DBType {
			get {
//				return _baseFieldInfo.DBType;
				return string.Empty;
			}
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			ExtendDataFieldInfo target = info as ExtendDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._baseFieldInfo.Equals (target._baseFieldInfo);
			}
			else {
				return false;
			}
		}
	}
}
