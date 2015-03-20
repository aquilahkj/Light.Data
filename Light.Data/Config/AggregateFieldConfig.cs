using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    /// <summary>
    /// 统计字段属性
    /// </summary>
    class AggregateFieldConfig : IAggregateFieldConfig, IConfiguratorFieldConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public AggregateFieldConfig(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("FieldName");
            }
            FieldName = fieldName;
        }

        /// <summary>
        /// 查询别名
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }

		/// <summary>
		/// 配置字段名
		/// </summary>
		/// <value>The name of the field.</value>
        public string FieldName
        {
            get;
            private set;
        }
    }
}
