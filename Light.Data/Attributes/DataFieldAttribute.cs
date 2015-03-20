using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

namespace Light.Data
{
	/// <summary>
	/// 数据字段属性
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true)]
	public class DataFieldAttribute : Attribute, IDataFieldConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">字段名称</param>
		public DataFieldAttribute (string name)
		{
			Name = name;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public DataFieldAttribute ()
		{

		}

		/// <summary>
		/// 数据库列名
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
		/// 是否主键
		/// </summary>
		public bool IsPrimaryKey {
			get;
			set;
		}

		/// <summary>
		/// 是否自增ID
		/// </summary>
		public bool IsIdentity {
			get;
			set;
		}

		/// <summary>
		/// 数据类型
		/// </summary>
		public string DBType {
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


		/// <summary>
		/// 数据顺序
		/// </summary>
		public int DataOrder {
			get;
			set;
		}
	}
}
