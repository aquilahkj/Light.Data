using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Light.Data.Config;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	class DataEntityMapping : DataMapping
	{
		protected Dictionary<string, RelationFieldMapping> _relationMappingDictionary = new Dictionary<string, RelationFieldMapping> ();

		internal DataEntityMapping (Type type, string tableName, bool isDataEntity)
			: base (type)
		{
			if (string.IsNullOrEmpty (tableName)) {
				_tableName = type.Name;
			}
			else {
				_tableName = tableName;
			}
			_isDataEntity = isDataEntity;
			InitialDataFieldMapping ();
			InitialRelationField ();
		}

		private void InitialRelationField ()
		{
			if (!ObjectType.IsSubclassOf (typeof(DataEntity))) {
				return;
			}
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo pi in propertys) {
				//关联属性
				//RelationAttribute[] relationAttributes = AttributeCore.GetPropertyAttributes<RelationAttribute>(pi, true);
				//IRelationConfig[] configs = ConfigManager.LoadRelationConfigs(pi);
				IRelationFieldConfig config = ConfigManager.LoadRelationFieldConfig (pi);
				if (config != null && config.RelationKeyCount > 0) {
					RelationFieldMapping mapping = new RelationFieldMapping (this, pi.PropertyType, pi.Name);
					foreach (RelationKey key in config.GetRelationKeys()) {
						mapping.AddRelationKeys (key.MasterKey, key.RelateKey);
					}
					if (!string.IsNullOrEmpty (config.PropertyName)) {
						//设定关联属性名称,使用时才生成对应关系
						mapping.SetRelationProperty (config.PropertyName);
					}
					_relationMappingDictionary.Add (mapping.RelationName, mapping);
				}
			}
		}

		public RelationFieldMapping FindRelateionMapping (string keyName)
		{
			if (_relationMappingDictionary.ContainsKey (keyName)) {
				return _relationMappingDictionary [keyName];
			}
			else {
				throw new LightDataException (string.Format (RE.RelationMappingIsNotExists, keyName));
			}
		}

		bool _isDataEntity = false;

		public bool IsDataEntity {
			get {
				return _isDataEntity;
			}

		}

		string _tableName = null;

		public string TableName {
			get {
				if (_aliasName != null) {
					return _aliasName;
				}
				else {
					return _tableName;
				}
			}

		}

		[ThreadStatic]
		string _aliasName = null;

		public void SetAliasName (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("AliasName");
			}
			_aliasName = name;
		}

		public void ClearAliasName ()
		{
			_aliasName = null;
		}

		public bool Equals (DataEntityMapping mapping)
		{
			if (mapping == null)
				return false;
			return this.ObjectType.Equals (mapping.ObjectType);
		}

		protected void InitialDataFieldMapping ()
		{
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo pi in propertys) {
				//字段属性
				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
				if (config != null) {
					Type type = pi.PropertyType;
					string name = string.IsNullOrEmpty (config.Name) ? pi.Name : config.Name;
					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (type, pi, name, pi.Name, config, this, ObjectType);
					mapping.Handler = new PropertyHandler (pi);
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					if (mapping.Name != mapping.IndexName) {
						_fieldMappingAlterNameDictionary.Add (mapping.Name, mapping);
					}
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.DataFieldsIsNotExists);
			}

		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, context, datareader);
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		void LoadDataField (object source, IFieldCollection collection, DataContext context, IDataReader datareader)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
				if (field == null)
					continue;

				if (field is IFieldCollection) {
					IFieldCollection ifc = field as IFieldCollection;
					object obj = ifc.LoadData (context, datareader);
					field.Handler.Set (source, obj);
				}
				else {
					object obj = field.DataOrder.HasValue ? datareader [field.DataOrder.Value] : datareader [field.Name];
					bool isnull = Object.Equals (obj, DBNull.Value);
					if (field.SpecifiedHandler != null) {
						if (isnull && field.DefaultValue == null) {
							field.SpecifiedHandler.Set (source, false);
						}
						else {
							field.SpecifiedHandler.Set (source, true);
						}
					}
					if (!isnull) {
						field.Handler.Set (source, field.ToProperty (obj));
					}
					else {
						if (field.DefaultValue != null) {
							field.Handler.Set (source, field.ToProperty (field.DefaultValue));
						}
					}
				}
			}
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, context, datarow);
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		void LoadDataField (object source, IFieldCollection collection, DataContext context, DataRow datarow)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
				if (field == null)
					continue;

				if (field is IFieldCollection) {
					IFieldCollection ifc = field as IFieldCollection;
					object obj = ifc.LoadData (context, datarow);
					field.Handler.Set (source, obj);
				}
				else {
					object obj = field.DataOrder.HasValue ? datarow [field.DataOrder.Value] : datarow [field.Name];
					bool isnull = Object.Equals (obj, DBNull.Value);
					if (field.SpecifiedHandler != null) {
						if (isnull && field.DefaultValue == null) {
							field.SpecifiedHandler.Set (source, false);
						}
						else {
							field.SpecifiedHandler.Set (source, true);
						}
					}
					if (!isnull) {
						field.Handler.Set (source, field.ToProperty (obj));
					}
					else {
						if (field.DefaultValue != null) {
							field.Handler.Set (source, field.ToProperty (field.DefaultValue));
						}
					}
				}
			}
		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			InitalDataField (item, this);
			return item;
		}

		private void InitalDataField (object source, IFieldCollection collection)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
				if (field == null)
					continue;

				if (field is IFieldCollection) {
					IFieldCollection ifc = field as IFieldCollection;
					object obj = ifc.InitialData ();
					field.Handler.Set (source, obj);
				}
				else {
					if (field.DefaultValue != null) {
						if (field.SpecifiedHandler != null) {
							field.SpecifiedHandler.Set (source, true);
						}
						field.Handler.Set (source, field.ToProperty (field.DefaultValue));
					}
				}
			}
		}
	}
}
