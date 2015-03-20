using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	class AggregateTableConfig : TableConfig, IAggregateTableConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dataType">数据类型</param>
		/// <param name="relateType">关联类型</param>
		public AggregateTableConfig (Type dataType, Type relateType)
		{
			if (dataType == null) {
				throw new ArgumentNullException ("DataType");
			}
			//if (relateType == null)
			//{
			//    throw new ArgumentNullException("RelateType");
			//}
			DataType = dataType;
			RelateType = relateType;
		}

		/// <summary>
		/// 数据表名
		/// </summary>
		public Type DataType {
			get;
			private set;
		}

		/// <summary>
		/// 关联表类型
		/// </summary>
		public Type RelateType {
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
