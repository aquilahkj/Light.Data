using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    class DataTableConfig : TableConfig,IDataTableConfig
    {
        //Dictionary<string, IConfiguratorFieldConfig> _fieldConfigDictionary = new Dictionary<string, IConfiguratorFieldConfig>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableConfig(Type dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentException("DataType");
            }
            DataType = dataType;
            IsEntityTable = true;
        }

        /// <summary>
        /// 数据表名
        /// </summary>
        public Type DataType
        {
            get;
            private set;
        }

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtendParams
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有实体表对应关系
        /// </summary>
        public bool IsEntityTable
        {
            get;
            set;
        }
        /*
        public void SetField(IConfiguratorFieldConfig config)
        {
            this.SetField(config.FieldName, config);
        }

        public void SetField(string fieldName, IConfiguratorFieldConfig config)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("FieldName");
            }
            if (config == null)
            {
                throw new ArgumentNullException("Config");
            }
            _fieldConfigDictionary.Add(fieldName, config);
        }

        public IConfiguratorFieldConfig GetField(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("FieldName");
            }
            if (_fieldConfigDictionary.ContainsKey(fieldName))
            {
                return _fieldConfigDictionary[fieldName];
            }
            else
            {
                return null;
            }
        }

        public IConfiguratorFieldConfig this[string fieldName]
        {
            get
            {
                return GetField(fieldName);
            }
            set
            {
                SetField(fieldName, value);
            }
        }


        public IEnumerator<IConfiguratorFieldConfig> GetEnumerator()
        {
            foreach (KeyValuePair<string, IConfiguratorFieldConfig> kv in _fieldConfigDictionary)
            {
                yield return kv.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (KeyValuePair<string, IConfiguratorFieldConfig> kv in _fieldConfigDictionary)
            {
                yield return kv.Value;
            }
        }
         */ 
    }
}
