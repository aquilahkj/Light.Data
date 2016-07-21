using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Light.Data
{
	/// <summary>
	/// Light data config.
	/// </summary>
	class LightDataConfig : IConfig
	{
		readonly Dictionary<Type, DataTableConfig> _dataTableConfigs = new Dictionary<Type, DataTableConfig> ();

		readonly Dictionary<Type, AggregateTableConfig> _aggregateTableConfigs = new Dictionary<Type, AggregateTableConfig> ();

		/// <summary>
		/// Loads the config.
		/// </summary>
		/// <param name="configNode">Config node.</param>
		public void LoadConfig (XmlNode configNode)
		{
			if (configNode == null) {
				throw new ArgumentNullException (nameof (configNode));
			}
			foreach (XmlNode typeNode in configNode.ChildNodes) {
				if (typeNode.Name == "dataType") {
					DataTableConfig config = LoadDataTableConfig (typeNode);
					this._dataTableConfigs.Add (config.DataType, config);
				}
				else if (typeNode.Name == "aggregateType") {
					AggregateTableConfig config = LoadAggregateTableConfig (typeNode);
					this._aggregateTableConfigs.Add (config.DataType, config);
				}
			}
		}

		static DataTableConfig LoadDataTableConfig (XmlNode typeNode)
		{
			if (typeNode == null) {
				throw new ArgumentNullException (nameof (typeNode));
			}
			string typeName = GetAttributeValue (typeNode, "type");
			if (typeName == null) {
				throw new LightDataException (RE.ConfigDataTypeValueIsEmpty);
			}
			Type dataType = Type.GetType (typeName, true);
			DataTableConfig config = new DataTableConfig (dataType);
			string tableName = GetAttributeValue (typeNode, "tableName");
			if (tableName != null) {
				config.TableName = tableName;
			}
//			string extendParams = GetAttributeValue (typeNode, "extendParams");
//			if (extendParams != null) {
//				config.ExtendParams = extendParams;
//			}

			string isEntityTable = GetAttributeValue (typeNode, "isEntityTable");
			if (isEntityTable != null) {
				bool value;
				if (bool.TryParse (isEntityTable, out value)) {
					config.IsEntityTable = value;
				}
				else {
					throw new LightDataException (string.Format (RE.ConfigDataLoadError, "isEntityTable"));
				}
			}

			foreach (XmlNode fieldNode in typeNode.ChildNodes) {
//				IConfiguratorFieldConfig fieldConfig = null;
				if (fieldNode.Name == "dataField") {
					IConfiguratorFieldConfig fieldConfig = LoadDataFieldConfig (fieldNode, dataType);
					if (fieldConfig != null) {
						config.SetField (fieldConfig);
					}
				}
				else if (fieldNode.Name == "relationField") {
					IConfiguratorFieldConfig fieldConfig = LoadRelationFieldConfig (fieldNode, dataType);
					if (fieldConfig != null) {
						config.SetField (fieldConfig);
					}
				}
				else if (fieldNode.Name == "extendParams") {
					if (config.ExtendParams == null) {
						ExtendParamCollection extendParams = ExtendParamCollection.CreateExtendParamsCollection (fieldNode);
						config.ExtendParams = extendParams;
					}
				}
			}
			if (config.ExtendParams == null) {
				config.ExtendParams = new ExtendParamCollection ();
			}
			return config;
		}

		static AggregateTableConfig LoadAggregateTableConfig (XmlNode typeNode)
		{
			if (typeNode == null) {
				throw new ArgumentNullException (nameof (typeNode));
			}
			string typeName = GetAttributeValue (typeNode, "type");
			if (typeName == null) {
				throw new LightDataException (RE.ConfigDataTypeValueIsEmpty);
			}
			Type dataType = Type.GetType (typeName, true);
			AggregateTableConfig config = new AggregateTableConfig (dataType);
//			string extendParams = GetAttributeValue (typeNode, "extendParams");
//			if (extendParams != null) {
//				config.ExtendParams = extendParams;
//			}
			foreach (XmlNode fieldNode in typeNode.ChildNodes) {
//				IConfiguratorFieldConfig fieldConfig = null;
				if (fieldNode.Name == "aggregateField") {
					IConfiguratorFieldConfig fieldConfig = LoadAggregateFieldConfig (fieldNode, dataType);
					if (fieldConfig != null) {
						config.SetField (fieldConfig);
					}
				}
				else if (fieldNode.Name == "extendParams") {
					if (config.ExtendParams == null) {
						ExtendParamCollection extendParams = ExtendParamCollection.CreateExtendParamsCollection (fieldNode);
						config.ExtendParams = extendParams;
					}
				}
			}
			if (config.ExtendParams == null) {
				config.ExtendParams = new ExtendParamCollection ();
			}
			return config;
		}

		static DataFieldConfig LoadDataFieldConfig (XmlNode fieldNode, Type dataType)
		{
			if (fieldNode == null) {
				throw new ArgumentNullException (nameof (fieldNode));
			}
			if (dataType == null) {
				throw new ArgumentNullException (nameof (dataType));
			}
			if (fieldNode.Name != "dataField") {
				throw new LightDataException (string.Format (RE.ConfigDataLoadError, "dataField"));
			}
			DataFieldConfig config;
			string fieldName = GetAttributeValue (fieldNode, "fieldName");
			if (fieldName == null) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldNameIsEmpty, dataType.Name));
			}
			PropertyInfo property = dataType.GetProperty (fieldName);
			if (property == null) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldIsNotExists, dataType.Name, fieldName));
			}

			config = new DataFieldConfig (fieldName);
			string name = GetAttributeValue (fieldNode, "name");
			if (name != null) {
				config.Name = name;
			}
			string dataOrder = GetAttributeValue (fieldNode, "dataOrder");
			if (dataOrder != null) {
				int value;
				if (int.TryParse (dataOrder, out value)) {
					config.DataOrder = value;
				}
				else {
					throw new LightDataException (string.Format (RE.ConfigDataLoadError, "dataOrder"));
				}
			}
			string isNullable = GetAttributeValue (fieldNode, "isNullable");
			if (isNullable != null) {
				bool value;
				if (bool.TryParse (isNullable, out value)) {
					config.IsNullable = value;
				}
				else {
					throw new LightDataException (string.Format (RE.ConfigDataLoadError, "isNullable"));
				}
			}
			string isPrimaryKey = GetAttributeValue (fieldNode, "isPrimaryKey");
			if (isPrimaryKey != null) {
				bool value;
				if (bool.TryParse (isPrimaryKey, out value)) {
					config.IsPrimaryKey = value;
				}
				else {
					throw new LightDataException (string.Format (RE.ConfigDataLoadError, "isPrimaryKey"));
				}
			}
			string isIdentity = GetAttributeValue (fieldNode, "isIdentity");
			if (isIdentity != null) {
				bool value;
				if (bool.TryParse (isIdentity, out value)) {
					config.IsIdentity = value;
				}
				else {
					throw new LightDataException (string.Format (RE.ConfigDataLoadError, "isIdentity"));
				}
			}
			string dbType = GetAttributeValue (fieldNode, "dbType");
			if (dbType != null) {
				config.DBType = dbType;
			}
			string defaultValue = GetAttributeValue (fieldNode, "defaultValue");
			if (defaultValue != null) {
				Type type = property.PropertyType;
				if (type.IsGenericType) {
					Type frameType = type.GetGenericTypeDefinition ();
					if (frameType.FullName == "System.Nullable`1") {
						Type[] arguments = type.GetGenericArguments ();
						type = arguments [0];
					}
				}
				object value;
				if (type == typeof(string)) {
					value = defaultValue;
				}
				else if (type.IsEnum) {
					try {
						value = Enum.Parse (type, defaultValue, true);
					} catch {
						throw new LightDataException (string.Format (RE.ConfigDataLoadError, "defaultValue"));
					}
				}
				else {
					if (type == typeof(DateTime)) {
						DateTime dt;
						if (DateTime.TryParse (defaultValue, out dt)) {
							value = dt;
						}
						else {
							try {
								value = Enum.Parse (typeof(DefaultTime), defaultValue, true);
							} catch {
								throw new LightDataException (string.Format (RE.ConfigDataLoadError, "defaultValue"));
							}
						}
					}
					else {
						try {
							value = Convert.ChangeType (defaultValue, type);
						} catch {
							throw new LightDataException (string.Format (RE.ConfigDataLoadError, "defaultValue"));
						}
					}
				}
				config.DefaultValue = value;
			}
			return config;
		}

		static RelationFieldConfig LoadRelationFieldConfig (XmlNode fieldNode, Type dataType)
		{
			if (fieldNode == null) {
				throw new ArgumentNullException (nameof (fieldNode));
			}
			if (dataType == null) {
				throw new ArgumentNullException (nameof (dataType));
			}
			if (fieldNode.Name != "relationField") {
				throw new LightDataException (string.Format (RE.ConfigDataLoadError, "relationField"));
			}
			RelationFieldConfig config;
			string fieldName = null;
			if (fieldNode.Attributes ["fieldName"] != null) {
				fieldName = fieldNode.Attributes ["fieldName"].Value;
			}
			if (string.IsNullOrEmpty (fieldName)) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldNameIsEmpty, dataType.Name));
			}
			PropertyInfo property = dataType.GetProperty (fieldName);
			if (property == null) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldNameIsEmpty, dataType.Name, fieldName));
			}
			config = new RelationFieldConfig (fieldName);
			if (fieldNode.Attributes ["property"] != null) {
				RelationMode mode;
				if (Enum.TryParse<RelationMode> (fieldNode.Attributes ["property"].Value, out mode)) {
					config.RelationMode = mode;
				}
			}
			foreach (XmlNode keyNode in fieldNode.ChildNodes) {
				if (keyNode.Name == "relationKey") {
					string masterKey;
					string relateKey;
					if (keyNode.Attributes ["masterKey"] != null) {
						masterKey = keyNode.Attributes ["masterKey"].Value;
					}
					else {
						throw new LightDataException (string.Format (RE.ConfigDataLoadError, "masterKey"));
					}
					if (keyNode.Attributes ["relateKey"] != null) {
						relateKey = keyNode.Attributes ["relateKey"].Value;
					}
					else {
						throw new LightDataException (string.Format (RE.ConfigDataLoadError, "relateKey"));
					}
					config.AddRelationKeys (masterKey, relateKey);
				}
			}
			return config;
		}

		static AggregateFieldConfig LoadAggregateFieldConfig (XmlNode fieldNode, Type aggregateType)
		{
			if (fieldNode == null) {
				throw new ArgumentNullException (nameof (fieldNode));
			}
			if (aggregateType == null) {
				throw new ArgumentNullException (nameof (aggregateType));
			}
			AggregateFieldConfig config;
			string fieldName = GetAttributeValue (fieldNode, "fieldName");
			if (fieldName == null) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldNameIsEmpty, aggregateType.Name));
			}
			PropertyInfo property = aggregateType.GetProperty (fieldName);
			if (property == null) {
				throw new LightDataException (string.Format (RE.ConfigDataFieldIsNotExists, aggregateType.Name, fieldName));
			}

			config = new AggregateFieldConfig (fieldName);

			string name = GetAttributeValue (fieldNode, "name");
			if (name != null) {
				config.Name = name;
			}
			return config;
		}

		static string GetAttributeValue (XmlNode node, string name)
		{
			string value = null;
			if (node.Attributes [name] != null) {
				value = node.Attributes [name].Value;
				if (value != null) {
					value = value.Trim ();
					if (value == string.Empty) {
						value = null;
					}
				}
			}
			return value;
		}

		/// <summary>
		/// Combines the config.
		/// </summary>
		/// <param name="config">Config.</param>
		public void CombineConfig (LightDataConfig config)
		{
			foreach (KeyValuePair<Type, DataTableConfig> kvs in config._dataTableConfigs) {
				this._dataTableConfigs [kvs.Key] = kvs.Value;
			}
			foreach (KeyValuePair<Type, AggregateTableConfig> kvs in config._aggregateTableConfigs) {
				this._aggregateTableConfigs [kvs.Key] = kvs.Value;
			}
		}

		/// <summary>
		/// Gets the data table config.
		/// </summary>
		/// <returns>The data table config.</returns>
		/// <param name="type">Type.</param>
		public DataTableConfig GetDataTableConfig (Type type)
		{
			DataTableConfig config;
			_dataTableConfigs.TryGetValue (type, out config);
			return config;
		}

		/// <summary>
		/// Gets the aggregate table config.
		/// </summary>
		/// <returns>The aggregate table config.</returns>
		/// <param name="type">Type.</param>
		public AggregateTableConfig GetAggregateTableConfig (Type type)
		{
			AggregateTableConfig config;
			_aggregateTableConfigs.TryGetValue (type, out config);
			return config;
		}

		/// <summary>
		/// Searchs for assembly.
		/// </summary>
		/// <returns>The for assembly.</returns>
		/// <param name="assembly">Assembly.</param>
		public LightDataConfig SearchForAssembly (Assembly assembly)
		{
			LightDataConfig config = new LightDataConfig ();
			foreach (KeyValuePair<Type, DataTableConfig> kvs in _dataTableConfigs) {
				if (kvs.Key.Assembly == assembly) {
					config._dataTableConfigs [kvs.Key] = kvs.Value;
				}
			}
			foreach (KeyValuePair<Type, AggregateTableConfig> kvs in _aggregateTableConfigs) {
				if (kvs.Key.Assembly == assembly) {
					config._aggregateTableConfigs [kvs.Key] = kvs.Value;
				}
			}
			return config;
		}
	}
}
