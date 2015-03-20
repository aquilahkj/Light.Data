using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 统计类型
	/// </summary>
	public enum AggregateType
	{
		/// <summary>
		/// 总数
		/// </summary>
		COUNT,
		/// <summary>
		/// 平均值
		/// </summary>
		AVG,
		/// <summary>
		/// 最大值
		/// </summary>
		MAX,
		/// <summary>
		/// 最小值
		/// </summary>
		MIN,
		/// <summary>
		/// 总和
		/// </summary>
		SUM
	}
}
