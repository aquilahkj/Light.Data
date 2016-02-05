using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

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
//			int fieldCount = 0;
			foreach (PropertyInfo pi in propertys) {
				//字段属性
				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
				if (config != null) {
//					Type type = pi.PropertyType;
//					string name = string.IsNullOrEmpty (config.Name) ? pi.Name : config.Name;
//					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (type, pi, name, pi.Name, config, this);
//					mapping.Handler = new PropertyHandler (pi);
					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (pi, config, this);

					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					if (mapping.Name != mapping.IndexName) {
						_fieldMappingDictionary.Add (mapping.Name, mapping);
					}
					_fieldList.Add (mapping);
					_fieldNames.Add (mapping.Name);
//					if (mapping.Name != mapping.IndexName) {
//						_fieldMappingAlterNameDictionary.Add (mapping.Name, mapping);
//					}
//					ComplexFieldMapping complex = mapping as ComplexFieldMapping;
//					if (complex != null) {
//						fieldCount += complex.SubDataFieldCount;
//					}
//					else {
//						fieldCount++;
//					}
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.DataFieldsIsNotExists);
			}
//			this._fieldCount = fieldCount;
		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object item = Activator.CreateInstance (ObjectType);
//			LoadDataField (item, this._fieldList, context, datareader);
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
					object obj = field.DataOrder.HasValue ? datareader [field.DataOrder.Value] : datareader [field.Name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
//					bool flag;
//					if (!Object.Equals (value, null)) {
//						field.Handler.Set (item, value);
//						flag = true;
//					}
//					else {
//						flag = false;
//					}
//					if (field.SpecifiedHandler != null) {
//						field.SpecifiedHandler.Set (item, flag);
//					}
				}
			}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		//		void LoadDataField (object source, IEnumerable<FieldMapping> fields, DataContext context, IDataReader datareader)
		//		{
		//			foreach (DataFieldMapping field in fields) {
		//				if (field == null)
		//					continue;
		//
		//				IFieldCollection fieldCollection = field as IFieldCollection;
		//				if (fieldCollection != null) {
		//					IFieldCollection ifc = fieldCollection;
		//					object obj = ifc.LoadData (context, datareader);
		//					field.Handler.Set (source, obj);
		//				}
		//				else {
		//					object obj = field.DataOrder.HasValue ? datareader [field.DataOrder.Value] : datareader [field.Name];
		//					object value = field.ToProperty (obj);
		//					bool flag;
		//					if (!Object.Equals (value, null)) {
		//						field.Handler.Set (source, value);
		//						flag = true;
		//					}
		//					else {
		//						flag = false;
		//					}
		//					if (field.SpecifiedHandler != null) {
		//						field.SpecifiedHandler.Set (source, flag);
		//					}
		////					bool isnull = Object.Equals (obj, DBNull.Value) || Object.Equals (obj, null);
		////					if (field.SpecifiedHandler != null) {
		////						if (isnull && field.DefaultValue == null) {
		////							field.SpecifiedHandler.Set (source, false);
		////						}
		////						else {
		////							field.SpecifiedHandler.Set (source, true);
		////						}
		////					}
		////					if (!isnull) {
		////						field.Handler.Set (source, field.ToProperty (obj));
		////					}
		////					else {
		////						if (field.DefaultValue != null) {
		////							field.Handler.Set (source, field.ToProperty (field.DefaultValue));
		////						}
		////						else if (!field.IsNullable && field.IsString) {
		////							field.Handler.Set (source, string.Empty);
		////						}
		////					}
		//				}
		//			}
		//		}

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
					object obj = field.DataOrder.HasValue ? datarow [field.DataOrder.Value] : datarow [field.Name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
//					bool flag;
//					if (!Object.Equals (value, null)) {
//						field.Handler.Set (item, value);
//						flag = true;
//					}
//					else {
//						flag = false;
//					}
//					if (field.SpecifiedHandler != null) {
//						field.SpecifiedHandler.Set (item, flag);
//					}
				}
			}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		//		void LoadDataField (object source, IFieldCollection collection, DataContext context, DataRow datarow)
		//		{
		//			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
		//				if (field == null)
		//					continue;
		//
		//				if (field is IFieldCollection) {
		//					IFieldCollection ifc = field as IFieldCollection;
		//					object obj = ifc.LoadData (context, datarow);
		//					field.Handler.Set (source, obj);
		//				}
		//				else {
		//					object obj = field.DataOrder.HasValue ? datarow [field.DataOrder.Value] : datarow [field.Name];
		//					bool isnull = Object.Equals (obj, DBNull.Value);
		//					if (field.SpecifiedHandler != null) {
		//						if (isnull && field.DefaultValue == null) {
		//							field.SpecifiedHandler.Set (source, false);
		//						}
		//						else {
		//							field.SpecifiedHandler.Set (source, true);
		//						}
		//					}
		//					if (!isnull) {
		//						field.Handler.Set (source, field.ToProperty (obj));
		//					}
		//					else {
		//						if (field.DefaultValue != null) {
		//							field.Handler.Set (source, field.ToProperty (field.DefaultValue));
		//						}
		//					}
		//				}
		//			}
		//		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			InitalDataField (item, this);
			return item;
		}

		private static void InitalDataField (object source, IFieldCollection collection)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
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
		static string _aliasName = null;

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
	}
}
