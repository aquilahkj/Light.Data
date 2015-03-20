using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	interface IDataTableConfig
	{
		/// <summary>
		/// 数据表名
		/// </summary>
		string TableName {
			get;
			set;
		}

		/// <summary>
		/// 扩展参数
		/// </summary>
		string ExtendParams {
			get;
			set;
		}

		/// <summary>
		/// 是否有实体表对应关系
		/// </summary>
		bool IsEntityTable {
			get;
			set;
		}
	}
}
