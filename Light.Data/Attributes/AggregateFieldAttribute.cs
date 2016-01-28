using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 统计字段属性
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true)]
	public class AggregateFieldAttribute : Attribute, IAggregateFieldConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public AggregateFieldAttribute ()
		{
		}


		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">字段名称</param>
		public AggregateFieldAttribute (string name)
		{
			Name = name;
		}

		/// <summary>
		/// 查询别名
		/// </summary>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// 是否可空
		/// </summary>
		public bool IsNullable {
			get;
			set;
		}

		/// <summary>
		/// 默认值
		/// </summary>
		public object DefaultValue {
			get;
			set;
		}
	}
}
