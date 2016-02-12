using System;
using System.Reflection;
using System.Collections.Generic;

namespace Light.Data
{
	static class ConfigManager
	{
		readonly static LightDataConfig InnerLightDataConfig;

		readonly static Dictionary<Assembly,LightDataConfig> LoadAssemblys = new Dictionary<Assembly,LightDataConfig> ();

		//		static Dictionary<Assembly,

		
		static ConfigManager ()
		{
			LightDataConfig dataConfig = new LightDataConfig ();

			Configurator systemConfiguator = Configurator.LoadConfiguratorFromSystemConfig ();
			if (systemConfiguator != null) {
				LightDataConfig config = systemConfiguator.CreateConfig<LightDataConfig> ();
				dataConfig.CombineConfig (config);
			}

			Configurator defaultFileConfiguator = Configurator.LoadConfiguratorFromFile ("lightdata.config");
			if (defaultFileConfiguator != null) {
				LightDataConfig config = defaultFileConfiguator.CreateConfig<LightDataConfig> ();
				dataConfig.CombineConfig (config);
			}

			Configurator[] defaultDirConfiguators = Configurator.LoadConfiguratorFromConfigFileDir ("lightdata");
			if (defaultDirConfiguators != null && defaultDirConfiguators.Length > 0) {
				foreach (Configurator configuator in defaultDirConfiguators) {
					LightDataConfig config = configuator.CreateConfig<LightDataConfig> ();
					dataConfig.CombineConfig (config);
				}
			}

//			Configurator[] assemblyConfiguators = Configurator.LoadConfiguratorFromAssembly ();
//			if (assemblyConfiguators != null && assemblyConfiguators.Length > 0) {
//				foreach (Configurator configuator in assemblyConfiguators) {
//					LightDataConfig config = configuator.CreateConfig<LightDataConfig> ();
//					dataConfig.CombineConfig (config);
//				}
//			}

			Configurator[] appSettingConfiguators = Configurator.LoadConfiguratorFromAppSetting ();
			if (appSettingConfiguators != null && appSettingConfiguators.Length > 0) {
				foreach (Configurator configuator in appSettingConfiguators) {
					LightDataConfig config = configuator.CreateConfig<LightDataConfig> ();
					dataConfig.CombineConfig (config);
				}
			}
			InnerLightDataConfig = dataConfig;
		}

		static LightDataConfig GetDataConfigFromAssembly (Assembly assembly)
		{
			LightDataConfig config = null;
			Configurator[] assemblyConfiguators = Configurator.LoadConfiguratorFromAssembly (assembly);
			if (assemblyConfiguators != null && assemblyConfiguators.Length > 0) {
				foreach (Configurator configuator in assemblyConfiguators) {
					config = configuator.CreateConfig<LightDataConfig> ();
					config = config.SearchForAssembly (assembly);
				}
			}
			return config;
		}

		static LightDataConfig GetConfig ()
		{
			return InnerLightDataConfig;
		}

		static DataTableConfig InnerLoadDataTableConfig (Type type)
		{
			DataTableConfig config = InnerLightDataConfig.GetDataTableConfig (type);
			if (config == null) {
				LightDataConfig dataConfig;
				if (!LoadAssemblys.TryGetValue (type.Assembly, out dataConfig)) {
					lock (LoadAssemblys) {
						if (!LoadAssemblys.TryGetValue (type.Assembly, out dataConfig)) {
							dataConfig = GetDataConfigFromAssembly (type.Assembly);
							LoadAssemblys.Add (type.Assembly, dataConfig);
						}
					}
				}
				if (dataConfig != null) {
					config = dataConfig.GetDataTableConfig (type);
				}
			}
			return config;
		}

		static AggregateTableConfig InnerLoadAggregateTableConfig (Type type)
		{
			AggregateTableConfig config = InnerLightDataConfig.GetAggregateTableConfig (type);
			if (config == null) {
				LightDataConfig dataConfig;
				if (!LoadAssemblys.TryGetValue (type.Assembly, out dataConfig)) {
					lock (LoadAssemblys) {
						if (!LoadAssemblys.TryGetValue (type.Assembly, out dataConfig)) {
							dataConfig = GetDataConfigFromAssembly (type.Assembly);
							LoadAssemblys.Add (type.Assembly, dataConfig);
						}
					}
				}
				if (dataConfig != null) {
					config = dataConfig.GetAggregateTableConfig (type);
				}
			}
			return config;
		}

		internal static IDataTableConfig LoadDataTableConfig (Type type)
		{
//			LightDataConfig lightDataConfig = GetConfig ();
//			if (lightDataConfig != null && lightDataConfig.ContainsDataTableConfig (type)) {
//				return lightDataConfig.GetDataTableConfig (type);
//			}
			IDataTableConfig config = InnerLoadDataTableConfig (type);
			if (config != null) {
				return config;
			}
			DataTableAttribute[] attributes = AttributeCore.GetTypeAttributes<DataTableAttribute> (type, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
//			return DataTableConfig (type);
		}

		internal static IDataFieldConfig LoadDataFieldConfig (PropertyInfo pi)
		{
//			LightDataConfig lightDataConfig = GetConfig ();
//			if (lightDataConfig != null && lightDataConfig.ContainsDataTableConfig (pi.ReflectedType)) {
//				DataTableConfig dtconfig = lightDataConfig.GetDataTableConfig (pi.ReflectedType);
//				return dtconfig [pi.Name] as DataFieldConfig;
//			}
			DataTableConfig config = InnerLoadDataTableConfig (pi.ReflectedType);
			if (config != null) {
				return config [pi.Name] as DataFieldConfig;
			}

			DataFieldAttribute[] attributes = AttributeCore.GetPropertyAttributes<DataFieldAttribute> (pi, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
//			DataFieldConfig fieldconfig = new DataFieldConfig (pi.Name);
//			return fieldconfig;
		}

		internal static IAggregateTableConfig LoadAggregateTableConfig (Type type)
		{
//			LightDataConfig lightDataConfig = GetConfig ();
//			if (lightDataConfig != null && lightDataConfig.ContainsAggregateTableConfig (type)) {
//				return lightDataConfig.GetAggregateTableConfig (type);
//			}
			IAggregateTableConfig config = InnerLoadAggregateTableConfig (type);
			if (config != null) {
				return config;
			}
			AggregateTableAttribute[] attributes = AttributeCore.GetTypeAttributes<AggregateTableAttribute> (type, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
		}

		internal static IAggregateFieldConfig LoadAggregateFieldConfig (PropertyInfo pi)
		{
//			LightDataConfig lightDataConfig = GetConfig ();
//			if (lightDataConfig != null && lightDataConfig.ContainsAggregateTableConfig (pi.ReflectedType)) {
//				AggregateTableConfig atconfig = lightDataConfig.GetAggregateTableConfig (pi.ReflectedType);
//				return atconfig [pi.Name] as AggregateFieldConfig;
//			}
			AggregateTableConfig config = InnerLoadAggregateTableConfig (pi.ReflectedType);
			if (config != null) {
				return config [pi.Name] as AggregateFieldConfig;
			}

			AggregateFieldAttribute[] attributes = AttributeCore.GetPropertyAttributes<AggregateFieldAttribute> (pi, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
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

		internal static IRelationFieldConfig LoadRelationFieldConfig (PropertyInfo pi)
		{
//			IRelationFieldConfig config = null;
//			if (config == null) {
//				LightDataConfig lightDataConfig = GetConfig ();
//				if (lightDataConfig != null && lightDataConfig.ContainDataTableConfig (pi.ReflectedType)) {
//					DataTableConfig dtconfig = lightDataConfig.GetDataTableConfig (pi.ReflectedType);
//					IConfiguratorFieldConfig fieldConfig = dtconfig [pi.Name];
//					if (fieldConfig != null) {
//						if (fieldConfig is IgnoraFieldConfig) {
//							return null;
//						}
//						config = dtconfig [pi.Name] as RelationFieldConfig;
//					}
//				}
//			}
//			if (config == null) {
//				RelationAttribute[] relationAttributes = AttributeCore.GetPropertyAttributes<RelationAttribute> (pi, true);
//				if (relationAttributes.Length > 0) {
//					RelationFieldConfig rfConfig = new RelationFieldConfig (pi.Name);
//					foreach (RelationAttribute ra in relationAttributes) {
//						rfConfig.AddRelationKeys (ra.MasterKey, ra.RelateKey);
//					}
//					RelationPropertyAttribute[] relationPropertyAttributes = AttributeCore.GetPropertyAttributes<RelationPropertyAttribute> (pi, true);
//					if (relationPropertyAttributes.Length > 0) {
//						rfConfig.PropertyName = relationPropertyAttributes [0].PropertyName;
//					}
//					config = rfConfig;
//				}
//			}
//			return config;
			return null;
		}
	}
}
