using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    interface IDataFieldConfig 
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        string Name
        {
            get;
            set;
        }

		/// <summary>
		/// 字段排序
		/// </summary>
        int DataOrder
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可空
        /// </summary>
        bool IsNullable
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        bool IsPrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自增ID
        /// </summary>
        bool IsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        string DBType
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        object DefaultValue
        {
            get;
            set;
        }
    }
}
