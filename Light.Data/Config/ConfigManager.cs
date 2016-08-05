using System;
using System.Collections.Generic;
using System.Reflection;

namespace Light.Data
{
	/// <summary>
	/// Config manager.
	/// </summary>
	static class ConfigManager
	{
		readonly static LightDataConfig InnerLightDataConfig;

		readonly static Dictionary<Assembly,LightDataConfig> LoadAssemblys = new Dictionary<Assembly,LightDataConfig> ();

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

		/// <summary>
		/// Loads the data table config.
		/// </summary>
		/// <returns>The data table config.</returns>
		/// <param name="type">Type.</param>
		public static IDataTableConfig LoadDataTableConfig (Type type)
		{
			IDataTableConfig config = InnerLoadDataTableConfig (type);
			if (config != null) {
				return config;
			}
			DataTableAttribute[] attributes = AttributeCore.GetTypeAttributes<DataTableAttribute> (type, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
		}

		/// <summary>
		/// Loads the data field config.
		/// </summary>
		/// <returns>The data field config.</returns>
		/// <param name="pi">Pi.</param>
		public static IDataFieldConfig LoadDataFieldConfig (PropertyInfo pi)
		{
			DataTableConfig config = InnerLoadDataTableConfig (pi.ReflectedType);
			if (config != null) {
				return config [pi.Name] as DataFieldConfig;
			}

			DataFieldAttribute[] attributes = AttributeCore.GetPropertyAttributes<DataFieldAttribute> (pi, true);
			if (attributes.Length > 0) {
				return attributes [0];
			}
			return null;
		}

		/// <summary>
		/// Loads the aggregate table config.
		/// </summary>
		/// <returns>The aggregate table config.</returns>
		/// <param name="type">Type.</param>
		public static IAggregateTableConfig LoadAggregateTableConfig (Type type)
		{
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

		/// <summary>
		/// Loads the aggregate field config.
		/// </summary>
		/// <returns>The aggregate field config.</returns>
		/// <param name="pi">Pi.</param>
		public static IAggregateFieldConfig LoadAggregateFieldConfig (PropertyInfo pi)
		{
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

		public static ExtendParamCollection LoadAggregateExtendParamsConfig (Type type)
		{
			AggregateTableConfig config = InnerLoadAggregateTableConfig (type);
			if (config != null) {
				return config.ExtendParams;
			}
			ExtendParamCollection extendParams = ExtendParamCollection.CreateExtendParamsCollection<AggregateExtendParamAttribute> (type);
			return extendParams;
		}

		public static ExtendParamCollection LoadDataTableExtendParamsConfig (Type type)
		{
			DataTableConfig config = InnerLoadDataTableConfig (type);
			if (config != null) {
				return config.ExtendParams;
			}
			ExtendParamCollection extendParams = ExtendParamCollection.CreateExtendParamsCollection<DataTableExtendParamAttribute> (type);
			return extendParams;
		}

		/// <summary>
		/// Loads the relation field config.
		/// </summary>
		/// <returns>The relation field config.</returns>
		/// <param name="pi">Pi.</param>
		public static IRelationFieldConfig LoadRelationFieldConfig (PropertyInfo pi)
		{
			IRelationFieldConfig config = null;
			if (config == null) {
				RelationFieldAttribute[] relationAttributes = AttributeCore.GetPropertyAttributes<RelationFieldAttribute> (pi, true);
				if (relationAttributes.Length > 0) {
					RelationFieldConfig rfConfig = new RelationFieldConfig (pi.Name);
					foreach (RelationFieldAttribute ra in relationAttributes) {
						rfConfig.AddRelationKeys (ra.MasterKey, ra.RelateKey);
					}
					RelationPropertyAttribute[] relationPropertyAttributes = AttributeCore.GetPropertyAttributes<RelationPropertyAttribute> (pi, true);
					//if (relationPropertyAttributes.Length > 0) {
					//	rfConfig.RelationMode = relationPropertyAttributes [0].RelationMode;
					//}
					config = rfConfig;
				}
			}
			return config;
		}
	
		
	
	}
}
