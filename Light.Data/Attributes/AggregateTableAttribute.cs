using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

namespace Light.Data
{
	/// <summary>
	/// 统计表属性
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true)]
	public class AggregateTableAttribute : Attribute, IAggregateTableConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public AggregateTableAttribute ()
		{

		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="relateType">关联表类型</param>
		public AggregateTableAttribute (Type relateType)
		{
			RelateType = relateType;
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
