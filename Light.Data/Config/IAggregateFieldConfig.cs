using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	interface IAggregateFieldConfig
	{
		/// <summary>
		/// 查询别名
		/// </summary>
		string Name {
			get;
			set;
		}

		/// <summary>
		/// 是否可空
		/// </summary>
		bool IsNullable
		{
			get;
			set;
		}

		/// <summary>
		/// 默认值
		/// </summary>
		object DefaultValue
		{
			get;
			set;
		}
	}
}
