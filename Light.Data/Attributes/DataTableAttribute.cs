using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

namespace Light.Data
{
	/// <summary>
	/// 数据表属性
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true)]
	public class DataTableAttribute : Attribute, IDataTableConfig
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="tableName">表名称</param>
		public DataTableAttribute (string tableName)
			: this ()
		{
			TableName = tableName;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public DataTableAttribute ()
		{
			IsEntityTable = true;
		}

		/// <summary>
		/// 数据表名
		/// </summary>
		public string TableName {
			get;
			set;
		}

		/// <summary>
		/// 扩展参数
		/// </summary>
		public string ExtendParams {
			get;
			set;
		}

		/// <summary>
		/// 是否有实体表对应关系
		/// </summary>
		public bool IsEntityTable {
			get;
			set;
		}

	}
}
