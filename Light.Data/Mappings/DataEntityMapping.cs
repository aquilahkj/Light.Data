using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Light.Data
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
			RelationFieldMapping mapping;
			if (_relationMappingDictionary.TryGetValue (keyName, out mapping)) {
				return _relationMappingDictionary [keyName];
			}
			else {
				throw new LightDataException (string.Format (RE.RelationMappingIsNotExists, keyName));
			}
//			if (_relationMappingDictionary.ContainsKey (keyName)) {
//				return _relationMappingDictionary [keyName];
//			}
//			else {
//				throw new LightDataException (string.Format (RE.RelationMappingIsNotExists, keyName));
//			}
		}

		bool _isDataEntity;

		public bool IsDataEntity {
			get {
				return _isDataEntity;
			}
		}

		string _tableName;

		public string TableName {
			get {
				if (_aliasName != null) {
					return DataEntityMapping._aliasName;
				}
				else {
					return _tableName;
				}
			}
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
			int index = 0;
			List<FieldInfo> list = new List<FieldInfo> ();
			foreach (PropertyInfo pi in propertys) {
				//字段属性
				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
				if (config != null) {
					index++;
					FieldInfo info = new FieldInfo (pi, config, index);
					list.Add (info);
				}
			}
			if (list.Count == 0) {
				throw new LightDataException (RE.DataFieldsIsNotExists);
			}
			list.Sort ((x, y) => {
				if (x.DataOrder.HasValue && y.DataOrder.HasValue) {
					if (x.DataOrder > y.DataOrder) {
						return  1;
					}
					else if (x.DataOrder < y.DataOrder) {
						return -1;
					}
					else {
						return x.FieldOrder > y.FieldOrder ? 1 : -1;
					}
				}
				else if (x.DataOrder.HasValue && !y.DataOrder.HasValue) {
					return  -1;
				}
				else if (!x.DataOrder.HasValue && y.DataOrder.HasValue) {
					return  1;
				}
				else {
					return x.FieldOrder > y.FieldOrder ? 1 : -1;
				}
			});

			for (int i = 0; i < list.Count; i++) {
				FieldInfo info = list [i];
				DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (info.Property, info.Config, i+1, this);
				_fieldMappingDictionary.Add (mapping.IndexName, mapping);
				if (mapping.Name != mapping.IndexName) {
					_fieldMappingDictionary.Add (mapping.Name, mapping);
				}
				_fieldList.Add (mapping);
			}
		}

//		protected void InitialDataFieldMapping ()
//		{
//			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
//			bool useOrder = false;
//			int index = 0;
//			List<DataFieldMapping> list = new List<DataFieldMapping> ();
//			foreach (PropertyInfo pi in propertys) {
//				//字段属性
//				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
//				if (config != null) {
//					index++;
//					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (pi, config, index, this);
//
//					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
//					if (mapping.Name != mapping.IndexName) {
//						_fieldMappingDictionary.Add (mapping.Name, mapping);
//					}
//					list.Add (mapping);
//					if (mapping.DataOrder != null) {
//						useOrder = true;
//					}
//				}
//			}
//			if (list.Count == 0) {
//				throw new LightDataException (RE.DataFieldsIsNotExists);
//			}
//			if (useOrder) {
//				list.Sort ((x, y) => {
//					if (x.DataOrder.HasValue && y.DataOrder.HasValue) {
//						if (x.DataOrder > y.DataOrder) {
//							return  1;
//						}
//						else if (x.DataOrder < y.DataOrder) {
//							return -1;
//						}
//						else {
//							return x.PositionOrder > y.PositionOrder ? 1 : -1;
//						}
//					}
//					else if (x.DataOrder.HasValue && !y.DataOrder.HasValue) {
//						return  -1;
//					}
//					else if (!x.DataOrder.HasValue && y.DataOrder.HasValue) {
//						return  1;
//					}
//					else {
//						return x.PositionOrder > y.PositionOrder ? 1 : -1;
//					}
//				});
//			}
//
//			for (int i = 0; i < list.Count; i++) {
//				list [i].FieldOrder = i + 1;
//			}
//			_fieldList.AddRange (list);
//		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object item = Activator.CreateInstance (ObjectType);
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datareader);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
//					object obj = field.DataOrder.HasValue ? datareader [field.DataOrder.Value] : datareader [field.Name];
					object obj = datareader [field.Name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
				}
			}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object item = Activator.CreateInstance (ObjectType);
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datarow);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
//					object obj = field.DataOrder.HasValue ? datarow [field.DataOrder.Value] : datarow [field.Name];
					object obj = datarow [field.Name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
				}
			}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
//			InitalDataField (item, this);
			return item;
		}

		private static void InitalDataField (object source, IFieldCollection collection)
		{
			foreach (DataFieldMapping field in collection.FieldMappings) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.InitialData ();
					field.Handler.Set (source, obj);
				}
				else {
					object obj = field.ToProperty (null);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (source, obj);
					}
				}
			}
		}

		#region alise

		[ThreadStatic]
		static string _aliasName;

		public void SetAliasName (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}
			DataEntityMapping._aliasName = name;
		}

		public void ClearAliasName ()
		{
			DataEntityMapping._aliasName = null;
		}

		#endregion

		class FieldInfo
		{
			public FieldInfo (PropertyInfo property, IDataFieldConfig config, int fieldOrder)
			{
				this.property = property;
				this.config = config;
				this.fieldOrder = fieldOrder;
				this.dataOrder = config.DataOrder;
			}

			PropertyInfo property;

			public PropertyInfo Property {
				get {
					return property;
				}
			}

			IDataFieldConfig config;

			public IDataFieldConfig Config {
				get {
					return config;
				}
			}

			int? dataOrder;

			public int? DataOrder {
				get {
					return dataOrder;
				}
			}

			int fieldOrder;

			public int FieldOrder {
				get {
					return fieldOrder;
				}
			}
		}
	}
}
