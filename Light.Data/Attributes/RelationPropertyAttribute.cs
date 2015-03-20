using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

namespace Light.Data
{
	/// <summary>
	/// 相关联对象属性,设定后该对象会被赋到关联结果指定的属性中,指定的属性类型必须与本数据类型一致
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true)]
	public class RelationPropertyAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="propertyName">关联的对象属性名称</param>
		public RelationPropertyAttribute (string propertyName)
		{
			if (string.IsNullOrEmpty (propertyName)) {
				throw new ArgumentNullException ("PropertyName");
			}
			PropertyName = propertyName;
		}

		/// <summary>
		/// 关联的对象属性名称
		/// </summary>
		public string PropertyName {
			get;
			private set;
		}
	}
}
