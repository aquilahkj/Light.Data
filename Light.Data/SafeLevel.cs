using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 安全级别
	/// </summary>
	public enum SafeLevel
	{
		/// <summary>
		/// 默认值
		/// </summary>
		Default,
		/// <summary>
		/// 无安全事务
		/// </summary>
		None,
		/// <summary>
		/// 低级别
		/// </summary>
		Low,
		/// <summary>
		/// 标准级别
		/// </summary>
		Normal,
		/// <summary>
		/// 高级别
		/// </summary>
		High
	}
}
