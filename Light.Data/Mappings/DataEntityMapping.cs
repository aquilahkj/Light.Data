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
			bool useOrder = false;
			int index = 0;
			foreach (PropertyInfo pi in propertys) {
				//字段属性
				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
				if (config != null) {
					index++;
					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (pi, config, index, this);

					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					if (mapping.Name != mapping.IndexName) {
						_fieldMappingDictionary.Add (mapping.Name, mapping);
					}
					_fieldList.Add (mapping);
					if (mapping.DataOrder != null) {
						useOrder = true;
					}
				}
			}
			if (_fieldList.Count == 0) {
				throw new LightDataException (RE.DataFieldsIsNotExists);
			}
			if (useOrder) {
				_fieldList.Sort ((x, y) => {
					DataFieldMapping m1 = x as DataFieldMapping;
					DataFieldMapping m2 = y as DataFieldMapping;
					if (m1.DataOrder.HasValue && m2.DataOrder.HasValue) {
						if (m1.DataOrder > m2.DataOrder) {
							return  1;
						}
						else if (m1.DataOrder < m2.DataOrder) {
							return -1;
						}
						else {
							return m1.PositionOrder > m2.PositionOrder ? 1 : -1;
						}
					}
					else if (m1.DataOrder.HasValue && !m2.DataOrder.HasValue) {
						return  -1;
					}
					else if (!m1.DataOrder.HasValue && m2.DataOrder.HasValue) {
						return  1;
					}
					else {
						return m1.PositionOrder > m2.PositionOrder ? 1 : -1;
					}
				});
				
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
