using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	class DataFieldConfig : IDataFieldConfig, IConfiguratorFieldConfig
	{
		public DataFieldConfig (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException ("FieldName");
			}
			FieldName = fieldName;
		}

		/// <summary>
		/// 字段名称
		/// </summary>
		public string FieldName {
			get;
			private set;
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
