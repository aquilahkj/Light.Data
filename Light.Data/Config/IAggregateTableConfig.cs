using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	interface IAggregateTableConfig
	{
		/// <summary>
		/// 关联表类型
		/// </summary>
		Type RelateType {
			get;
		}

		/// <summary>
		/// 扩展参数
		/// </summary>
		string ExtendParams {
			get;
			set;
		}
	}
}
