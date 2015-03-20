using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Light.Data.Config
{
    class LightDataConfig : IConfig
    {
        Dictionary<Type, DataTableConfig> _dataTableConfigs = null;

        Dictionary<Type, AggregateTableConfig> _aggregateTableConfigs = null;

        public void LoadConfig(XmlNode configNode)
        {
            if (configNode == null)
            {
                throw new ArgumentNullException("ConfigNode");
            }
            _dataTableConfigs = new Dictionary<Type, DataTableConfig>();
            _aggregateTableConfigs = new Dictionary<Type, AggregateTableConfig>();
            foreach (XmlNode typeNode in configNode.ChildNodes)
            {
                if (typeNode.Name == "DataType")
                {
                    DataTableConfig config = LoadDataTableConfig(typeNode);
                    _dataTableConfigs.Add(config.DataType, config);
                }
                else if (typeNode.Name == "AggregateType")
                {
                    AggregateTableConfig config = LoadAggregateTableConfig(typeNode);
                    _aggregateTableConfigs.Add(config.DataType, config);
                }
            }
        }

        DataTableConfig LoadDataTableConfig(XmlNode typeNode)
        {
            if (typeNode == null)
            {
                throw new ArgumentException("TypeNode");
            }

            if (typeNode.Name != "DataType")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "DataType"));
            }
            DataTableConfig config = null;
            Type dataType = null;
            string typeName = null;
            if (typeNode.Attributes["Type"] != null)
            {
                typeName = typeNode.Attributes["Type"].Value;
            }
            if (string.IsNullOrEmpty(typeName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataTypeValueIsEmpty, "Type"));
            }
            dataType = System.Type.GetType(typeName, true);
            config = new DataTableConfig(dataType);

            if (typeNode.Attributes["TableName"] != null)
            {
                config.TableName = typeNode.Attributes["TableName"].Value;
            }
            if (typeNode.Attributes["ExtendParams"] != null)
            {
                config.ExtendParams = typeNode.Attributes["ExtendParams"].Value;
            }
            if (typeNode.Attributes["IsEntityTable"] != null)
            {
                config.IsEntityTable = Convert.ToBoolean(typeNode.Attributes["IsEntityTable"].Value);
            }
            foreach (XmlNode fieldNode in typeNode.ChildNodes)
            {
                IConfiguratorFieldConfig fieldConfig = null;
                if (fieldNode.Name == "DataField")
                {
                    fieldConfig = LoadDataFieldConfig(fieldNode, dataType);
                }
                else if (fieldNode.Name == "IgnoraField")
                {
                    fieldConfig = LoadIgnoraFieldConfig(fieldNode, dataType);
                }
                else if (fieldNode.Name == "RelationField")
                {
                    fieldConfig = LoadRelationFieldConfig(fieldNode, dataType);
                }
                if (fieldConfig != null)
                {
                    config.SetField(fieldConfig);
                }
            }
            return config;
        }

        AggregateTableConfig LoadAggregateTableConfig(XmlNode typeNode)
        {
            if (typeNode == null)
            {
                throw new ArgumentException("TypeNode");
            }

            if (typeNode.Name != "AggregateType")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "AggregateType"));
            }
            AggregateTableConfig config = null;
            Type dataType = null;
            Type relateType = null;
            string dataTypeName = null;
            string relateTypeName = null;
            if (typeNode.Attributes["Type"] != null)
            {
                dataTypeName = typeNode.Attributes["Type"].Value;
            }
            if (string.IsNullOrEmpty(dataTypeName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataTypeValueIsEmpty, "Type"));
            }
            if (typeNode.Attributes["RelateType"] != null)
            {
                relateTypeName = typeNode.Attributes["RelateType"].Value;
            }
            //if (string.IsNullOrEmpty(relateTypeName))
            //{
            //    throw new LightDataException(string.Format(RE.ConfigDataTypeValueIsEmpty, "RelateType"));
            //}

            dataType = System.Type.GetType(dataTypeName, true);
            if (!string.IsNullOrEmpty(relateTypeName))
            {
                relateType = System.Type.GetType(relateTypeName, true);
            }
            config = new AggregateTableConfig(dataType, relateType);

            if (typeNode.Attributes["ExtendParams"] != null)
            {
                config.ExtendParams = typeNode.Attributes["ExtendParams"].Value;
            }

            foreach (XmlNode fieldNode in typeNode.ChildNodes)
            {
                IConfiguratorFieldConfig fieldConfig = null;
                if (fieldNode.Name == "AggregateField")
                {
                    fieldConfig = LoadAggregateFieldConfig(fieldNode, dataType);
                }
                if (fieldNode.Name == "IgnoraField")
                {
                    fieldConfig = LoadIgnoraFieldConfig(fieldNode, dataType);
                }
                if (fieldConfig != null)
                {
                    config.SetField(fieldConfig);
                }
            }
            return config;
        }

        DataFieldConfig LoadDataFieldConfig(XmlNode fieldNode, Type dataType)
        {
            if (fieldNode == null)
            {
                throw new ArgumentException("FieldNode");
            }
            if (dataType == null)
            {
                throw new ArgumentException("DataType");
            }
            if (fieldNode.Name != "DataField")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "DataField"));
            }
            DataFieldConfig config = null;
            string fieldName = null;
            if (fieldNode.Attributes["FieldName"] != null)
            {
                fieldName = fieldNode.Attributes["FieldName"].Value;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name));
            }
            PropertyInfo property = dataType.GetProperty(fieldName);
            if (property == null)
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name, fieldName));
            }

            config = new DataFieldConfig(fieldName);

            if (fieldNode.Attributes["Name"] != null)
            {
                config.Name = fieldNode.Attributes["Name"].Value;
            }
            if (fieldNode.Attributes["DataOrder"] != null)
            {
                string order = fieldNode.Attributes["Name"].Value;
                if (!string.IsNullOrEmpty(order))
                {
                    config.DataOrder = Convert.ToInt32(order);
                }
            }
            if (fieldNode.Attributes["IsNullable"] != null)
            {
                config.IsNullable = Convert.ToBoolean(fieldNode.Attributes["IsNullable"].Value);
            }
            if (fieldNode.Attributes["IsPrimaryKey"] != null)
            {
                config.IsPrimaryKey = Convert.ToBoolean(fieldNode.Attributes["IsPrimaryKey"].Value);
            }
            if (fieldNode.Attributes["IsIdentity"] != null)
            {
                config.IsIdentity = Convert.ToBoolean(fieldNode.Attributes["IsIdentity"].Value);
            }
            if (fieldNode.Attributes["DBType"] != null)
            {
                config.DBType = fieldNode.Attributes["DBType"].Value;
            }
            if (fieldNode.Attributes["DefaultValue"] != null)
            {
                Type type = property.PropertyType;
                if (type.IsGenericType)
                {
                    Type frameType = type.GetGenericTypeDefinition();
                    if (frameType.FullName == "System.Nullable`1")
                    {
                        Type[] arguments = type.GetGenericArguments();
                        type = arguments[0];
                    }
                }
                if (type.IsEnum)
                {
                    config.DefaultValue = Enum.Parse(type, fieldNode.Attributes["DefaultValue"].Value, true);
                }
                else
                {
                    config.DefaultValue = Convert.ChangeType(fieldNode.Attributes["DefaultValue"].Value, type);
                }
            }
            return config;
        }

        IgnoraFieldConfig LoadIgnoraFieldConfig(XmlNode fieldNode, Type dataType)
        {
            if (fieldNode == null)
            {
                throw new ArgumentException("FieldNode");
            }
            if (dataType == null)
            {
                throw new ArgumentException("IgnoraField");
            }
            if (fieldNode.Name != "IgnoraField")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "IgnoraField"));
            }
            IgnoraFieldConfig config = null;
            string fieldName = null;
            if (fieldNode.Attributes["FieldName"] != null)
            {
                fieldName = fieldNode.Attributes["FieldName"].Value;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name));
            }
            PropertyInfo property = dataType.GetProperty(fieldName);
            if (property == null)
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name, fieldName));
            }
            config = new IgnoraFieldConfig(fieldName);
            return config;
        }

        RelationFieldConfig LoadRelationFieldConfig(XmlNode fieldNode, Type dataType)
        {
            if (fieldNode == null)
            {
                throw new ArgumentException("FieldNode");
            }
            if (dataType == null)
            {
                throw new ArgumentException("IgnoraField");
            }
            if (fieldNode.Name != "RelationField")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "RelationField"));
            }
            RelationFieldConfig config = null;
            string fieldName = null;
            if (fieldNode.Attributes["FieldName"] != null)
            {
                fieldName = fieldNode.Attributes["FieldName"].Value;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name));
            }
            PropertyInfo property = dataType.GetProperty(fieldName);
            if (property == null)
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, dataType.Name, fieldName));
            }
            config = new RelationFieldConfig(fieldName);
            if (fieldNode.Attributes["Property"] != null)
            {
                config.PropertyName = fieldNode.Attributes["Property"].Value;
            }
            foreach (XmlNode keyNode in fieldNode.ChildNodes)
            {
                if (keyNode.Name == "RelationKey")
                {
                    string masterKey = null;
                    string relateKey = null;
                    if (keyNode.Attributes["MasterKey"] != null)
                    {
                        masterKey = keyNode.Attributes["MasterKey"].Value;
                    }
                    else
                    {
                        throw new LightDataException(string.Format(RE.ConfigDataLoadError, "MasterKey"));
                    }
                    if (keyNode.Attributes["RelateKey"] != null)
                    {
                        relateKey = keyNode.Attributes["RelateKey"].Value;
                    }
                    else
                    {
                        throw new LightDataException(string.Format(RE.ConfigDataLoadError, "RelateKey"));
                    }
                    config.AddRelationKeys(masterKey, relateKey);
                }
            }
            return config;
        }

        AggregateFieldConfig LoadAggregateFieldConfig(XmlNode fieldNode, Type aggregateType)
        {
            if (fieldNode == null)
            {
                throw new ArgumentException("FieldNode");
            }
            if (aggregateType == null)
            {
                throw new ArgumentException("AggregateType");
            }
            if (fieldNode.Name != "AggregateField")
            {
                throw new LightDataException(string.Format(RE.ConfigDataLoadError, "AggregateField"));
            }
            AggregateFieldConfig config = null;
            string fieldName = null;
            if (fieldNode.Attributes["FieldName"] != null)
            {
                fieldName = fieldNode.Attributes["FieldName"].Value;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, aggregateType.Name));
            }
            PropertyInfo property = aggregateType.GetProperty(fieldName);
            if (property == null)
            {
                throw new LightDataException(string.Format(RE.ConfigDataFieldNameIsEmpty, aggregateType.Name, fieldName));
            }

            config = new AggregateFieldConfig(fieldName);

            if (fieldNode.Attributes["Name"] != null)
            {
                config.Name = fieldNode.Attributes["Name"].Value;
            }
            return config;
        }

        public bool ContainDataTableConfig(Type type)
        {
            return _dataTableConfigs.ContainsKey(type);
        }

        public DataTableConfig GetDataTableConfig(Type type)
        {
            return _dataTableConfigs[type];
        }

        public bool ContainAggregateTableConfig(Type type)
        {
            return _aggregateTableConfigs.ContainsKey(type);
        }

        public AggregateTableConfig GetAggregateTableConfig(Type type)
        {
            return _aggregateTableConfigs[type];
        }
    }
}
