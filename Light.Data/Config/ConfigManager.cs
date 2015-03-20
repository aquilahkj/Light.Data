using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Light.Data.Handler;

namespace Light.Data.Config
{
    static class ConfigManager
    {
        static readonly string SECTION_NAME = "LightDataConfig";

        static LightDataConfig _lightDataConfig = null;

        static Dictionary<Assembly, LightDataConfig> _assemblyConfig = new Dictionary<Assembly, LightDataConfig>();

        static ConfigManager()
        {
            Configurator configuator = null;
            //if (DataContextConfiguration.CallingAssembly != null)
            //{
            //    configuator = Configurator.LoadConfiguratorFromAssembly(DataContextConfiguration.CallingAssembly, SECTION_NAME);
            //}
            if (configuator == null)
            {
                configuator = Configurator.LoadConfiguratorFromFile("LightData.config", SECTION_NAME);
            }
            if (configuator == null)
            {
                configuator = Configurator.LoadConfiguratorFromSystemConfig(SECTION_NAME);
            }
            if (configuator != null)
            {
                _lightDataConfig = configuator.CreateConfig<LightDataConfig>();
            }
        }

        static LightDataConfig GetAssemblyConfig()
        {
            Assembly callingAssembly = DataContext.CallingAssembly;
            if (callingAssembly == null)
            {
                return null;
            }
            if (!_assemblyConfig.ContainsKey(callingAssembly))
            {
                Configurator configuator = Configurator.LoadConfiguratorFromAssembly(callingAssembly, SECTION_NAME);
                LightDataConfig lightDataConfig = null;
                if (configuator != null)
                {
                     lightDataConfig = configuator.CreateConfig<LightDataConfig>();
                }
                _assemblyConfig.Add(callingAssembly, lightDataConfig);
            }
            return _assemblyConfig[callingAssembly];
        }

        static LightDataConfig GetConfig()
        {
            LightDataConfig config = GetAssemblyConfig();
            if (config == null)
            {
                config = _lightDataConfig;
            }
            return config;
        }

        internal static IDataTableConfig LoadDataTableConfig(Type type)
        {
            IDataTableConfig config = null;
            if (config == null)
            {
                LightDataConfig lightDataConfig = GetConfig();
                if (lightDataConfig != null && lightDataConfig.ContainDataTableConfig(type))
                {
                    config = lightDataConfig.GetDataTableConfig(type);
                }
            }
            if (config == null)
            {
                DataTableAttribute[] attributes = AttributeCore.GetTypeAttributes<DataTableAttribute>(type, true);
                if (attributes.Length > 0)
                {
                    config = attributes[0];
                }
            }
            return config;
        }

        internal static IDataFieldConfig LoadDataFieldConfig(PropertyInfo pi)
        {
            IDataFieldConfig config = null;
            if (config == null)
            {
                LightDataConfig lightDataConfig = GetConfig();
                if (lightDataConfig != null && lightDataConfig.ContainDataTableConfig(pi.ReflectedType))
                {
                    DataTableConfig dtconfig = lightDataConfig.GetDataTableConfig(pi.ReflectedType);
                    IConfiguratorFieldConfig fieldConfig = dtconfig[pi.Name];
                    if (fieldConfig != null)
                    {
                        if (fieldConfig is IgnoraFieldConfig)
                        {
                            return null;
                        }
                        config = dtconfig[pi.Name] as DataFieldConfig;
                    }
                }
            }
            if (config == null)
            {
                DataFieldAttribute[] attributes = AttributeCore.GetPropertyAttributes<DataFieldAttribute>(pi, true);
                if (attributes.Length > 0)
                {
                    config = attributes[0];
                }
            }
            return config;
        }

        internal static IAggregateTableConfig LoadAggregateTableConfig(Type type)
        {
            IAggregateTableConfig config = null;
            if (config == null)
            {
                LightDataConfig lightDataConfig = GetConfig();
                if (lightDataConfig != null && lightDataConfig.ContainAggregateTableConfig(type))
                {
                    config = lightDataConfig.GetAggregateTableConfig(type);
                }
            }
            if (config == null)
            {
                AggregateTableAttribute[] attributes = AttributeCore.GetTypeAttributes<AggregateTableAttribute>(type, true);
                if (attributes.Length > 0)
                {
                    config = attributes[0];
                }
            }

            return config;
        }

        internal static IAggregateFieldConfig LoadAggregateFieldConfig(PropertyInfo pi)
        {
            IAggregateFieldConfig config = null;
            if (config == null)
            {
                LightDataConfig lightDataConfig = GetConfig();
                if (lightDataConfig != null && lightDataConfig.ContainAggregateTableConfig(pi.ReflectedType))
                {
                    AggregateTableConfig atconfig = lightDataConfig.GetAggregateTableConfig(pi.ReflectedType);
                    IConfiguratorFieldConfig fieldConfig = atconfig[pi.Name];
                    if (fieldConfig != null)
                    {
                        if (fieldConfig is IgnoraFieldConfig)
                        {
                            return null;
                        }
                        config = atconfig[pi.Name] as AggregateFieldConfig;
                    }
                }
            }
            if (config == null)
            {
                AggregateFieldAttribute[] attributes = AttributeCore.GetPropertyAttributes<AggregateFieldAttribute>(pi, true);
                if (attributes.Length > 0)
                {
                    config = attributes[0];
                }
            }
            return config;
        }

        //internal static IRelationConfig[] LoadRelationConfigs(PropertyInfo pi)
        //{
        //    IRelationConfig[] configs = null;
        //    RelationAttribute[] attributes = AttributeCore.GetPropertyAttributes<RelationAttribute>(pi, true);
        //    if (attributes.Length > 0)
        //    {
        //        configs = attributes;
        //    }
        //    return configs;
        //}

        //internal static IRelationPropertyConfig LoadRelationPropertyConfig(PropertyInfo pi)
        //{
        //    IRelationPropertyConfig config = null;
        //    RelationPropertyAttribute[] attributes = AttributeCore.GetPropertyAttributes<RelationPropertyAttribute>(pi, true);
        //    if (attributes.Length > 0)
        //    {
        //        config = attributes[0];
        //    }
        //    return config;
        //}

        internal static IRelationFieldConfig LoadRelationFieldConfig(PropertyInfo pi)
        {
            IRelationFieldConfig config = null;
            if (config == null)
            {
                LightDataConfig lightDataConfig = GetConfig();
                if (lightDataConfig != null && lightDataConfig.ContainDataTableConfig(pi.ReflectedType))
                {
                    DataTableConfig dtconfig = lightDataConfig.GetDataTableConfig(pi.ReflectedType);
                    IConfiguratorFieldConfig fieldConfig = dtconfig[pi.Name];
                    if (fieldConfig != null)
                    {
                        if (fieldConfig is IgnoraFieldConfig)
                        {
                            return null;
                        }
                        config = dtconfig[pi.Name] as RelationFieldConfig;
                    }
                }
            }
            if (config == null)
            {
                RelationAttribute[] relationAttributes = AttributeCore.GetPropertyAttributes<RelationAttribute>(pi, true);
                if (relationAttributes.Length > 0)
                {
                    RelationFieldConfig rfConfig = new RelationFieldConfig(pi.Name);
                    foreach (RelationAttribute ra in relationAttributes)
                    {
                        rfConfig.AddRelationKeys(ra.MasterKey, ra.RelateKey);
                    }
                    RelationPropertyAttribute[] relationPropertyAttributes = AttributeCore.GetPropertyAttributes<RelationPropertyAttribute>(pi, true);
                    if (relationPropertyAttributes.Length > 0)
                    {
                        rfConfig.PropertyName = relationPropertyAttributes[0].PropertyName;
                    }
                    config = rfConfig;
                }
            }
            return config;
        }
    }
}
