using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class AggregateTableConfig : TableConfig, IAggregateTableConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dataType">数据类型</param>
		public AggregateTableConfig (Type dataType)
		{
			if (dataType == null) {
				throw new ArgumentNullException ("dataType");
			}
			DataType = dataType;
		}

		/// <summary>
		/// 数据表名
		/// </summary>
		public Type DataType {
			get;
			private set;
		}

		/// <summary>
		/// 扩展参数
		/// </summary>
		public string ExtendParams {
			get;
			set;
		}
	}
}
