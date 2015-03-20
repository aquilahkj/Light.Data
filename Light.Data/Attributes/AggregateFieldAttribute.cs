using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

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
	}
}
