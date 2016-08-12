using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Light.Data
{
	/// <summary>
	/// Data entity mapping.
	/// </summary>
	class DataEntityMapping : DataMapping
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, DataEntityMapping> _defaultMapping = new Dictionary<Type, DataEntityMapping> ();


		/// <summary>
		/// Gets the table mapping.
		/// </summary>
		/// <returns>The table mapping.</returns>
		/// <param name="type">Type.</param>
		public static DataTableEntityMapping GetTableMapping (Type type)
		{
			DataTableEntityMapping dataMapping = GetEntityMapping (type) as DataTableEntityMapping;
			if (dataMapping == null) {
				throw new LightDataException (RE.TheDataMappingIsNotDataTableMapping);
			}
			else {
				return dataMapping;
			}
		}

		/// <summary>
		/// Gets the entity mapping.
		/// </summary>
		/// <returns>The entity mapping.</returns>
		/// <param name="type">Type.</param>
		public static DataEntityMapping GetEntityMapping (Type type)
		{
			Dictionary<Type, DataEntityMapping> mappings = _defaultMapping;
			DataEntityMapping mapping;
			mappings.TryGetValue (type, out mapping);
			if (mapping == null) {
				lock (_synobj) {
					mappings.TryGetValue (type, out mapping);
					if (mapping == null) {
						try {
							mapping = CreateMapping (type);
						}
						catch (Exception ex) {
							mapping = new ErrorDataMapping (type, ex);
						}
						mappings [type] = mapping;
					}
				}
			}
			ErrorDataMapping errMapping = mapping as ErrorDataMapping;
			if (errMapping != null) {
				throw errMapping.InnerException;
			}
			else {
				return mapping;
			}
		}

		/// <summary>
		/// Checks the entity mapping.must execute GetMapping method first
		/// </summary>
		/// <returns>The entity mapping.</returns>
		/// <param name="type">Type.</param>
		public static bool CheckMapping (Type type)
		{
			return _defaultMapping.ContainsKey (type);
		}

		/// <summary>
		/// Creates the mapping.
		/// </summary>
		/// <returns>The mapping.</returns>
		/// <param name="type">Type.</param>
		private static DataEntityMapping CreateMapping (Type type)
		{
			string tableName;
			//			string extentParam;
			bool isEntityTable;
			DataEntityMapping dataMapping;

			IDataTableConfig config = ConfigManager.LoadDataTableConfig (type);
			if (config != null) {
				tableName = config.TableName;
				//				extentParam = config.ExtendParams;
				isEntityTable = config.IsEntityTable;
			}
			else {
				throw new LightDataException (string.Format (RE.TheTypeOfDataEntityIsNoConfig, type.Name));
			}

			if (string.IsNullOrEmpty (tableName)) {
				tableName = type.Name;
			}

			if (type.IsSubclassOf (typeof (DataTableEntity))) {
				dataMapping = new DataTableEntityMapping (type, tableName, true);
			}
			else if (type.IsSubclassOf (typeof (DataEntity))) {
				dataMapping = new DataEntityMapping (type, tableName, true);
			}
			else {
				if (!isEntityTable) {
					dataMapping = new DataEntityMapping (type, tableName, false);
				}
				else {
					dataMapping = new DataTableEntityMapping (type, tableName, false);
				}
			}
			//			dataMapping.ExtentParams = new ExtendParamsCollection (extentParam);
			return dataMapping;
		}

		#endregion

		protected List<CollectionRelationFieldMapping> collectionRelationFields = new List<CollectionRelationFieldMapping> ();

		//protected List<SingleRelationFieldMapping> singleMultiQueryRelationFields = new List<SingleRelationFieldMapping> ();

		protected List<SingleRelationFieldMapping> singleRelationFields = new List<SingleRelationFieldMapping> ();

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
			InitialExtendParams ();
		}

		internal SingleRelationFieldMapping [] GetSingleRelationFieldMappings ()
		{
			//int len = this.singleMultiQueryRelationFields.Count + this.singleJoinTableRelationFields.Count;
			int len = this.singleRelationFields.Count;
			SingleRelationFieldMapping [] array = new SingleRelationFieldMapping [len];
			int index = 0;
			//foreach (SingleRelationFieldMapping item in this.singleMultiQueryRelationFields) {
			//	array [index] = item;
			//	index++;
			//}
			foreach (SingleRelationFieldMapping item in this.singleRelationFields) {
				array [index] = item;
				index++;
			}
			return array;
		}

		//internal SingleRelationFieldMapping [] GetSingleRequeryRelationFieldMappings ()
		//{
		//	int len = this.singleMultiQueryRelationFields.Count;
		//	SingleRelationFieldMapping [] array = new SingleRelationFieldMapping [len];
		//	int index = 0;
		//	foreach (SingleRelationFieldMapping item in this.singleMultiQueryRelationFields) {
		//		array [index] = item;
		//		index++;
		//	}
		//	return array;
		//}

		internal SingleRelationFieldMapping [] GetSingleJoinTableRelationFieldMappings ()
		{
			//int len = this.singleRelationFields.Count;
			//SingleRelationFieldMapping [] array = new SingleRelationFieldMapping [len];
			//int index = 0;
			//foreach (SingleRelationFieldMapping item in this.singleRelationFields) {
			//	array [index] = item;
			//	index++;
			//}
			//return array;
			return this.singleRelationFields.ToArray ();
		}

		internal CollectionRelationFieldMapping [] GetCollectionRelationFieldMappings ()
		{
			return this.collectionRelationFields.ToArray ();
		}

		private void InitialRelationField ()
		{
			PropertyInfo [] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				IRelationFieldConfig config = ConfigManager.LoadRelationFieldConfig (pi);
				if (config != null && config.RelationKeyCount > 0) {
					Type type = pi.PropertyType;
					if (type.IsGenericType) {
						Type frameType = type.GetGenericTypeDefinition ();
						if (frameType.FullName == "Light.Data.LCollection`1" || frameType.FullName == "System.Collections.Generic.ICollection`1") {
							Type [] arguments = type.GetGenericArguments ();
							type = arguments [0];
							PropertyHandler handler = new PropertyHandler (pi);
							RelationKey [] keypairs = config.GetRelationKeys ();
							CollectionRelationFieldMapping rmapping = new CollectionRelationFieldMapping (pi.Name, this, type, keypairs, handler);
							collectionRelationFields.Add (rmapping);
						}
					}
					else {
						PropertyHandler handler = new PropertyHandler (pi);
						RelationKey [] keypairs = config.GetRelationKeys ();
						SingleRelationFieldMapping rmapping = new SingleRelationFieldMapping (pi.Name, this, type, keypairs, handler);
						//if (config.RelationMode == RelationMode.MultiQuery) {
						//	singleMultiQueryRelationFields.Add (rmapping);
						//}
						//else {
						//	singleJoinTableRelationFields.Add (rmapping);
						//}
						singleRelationFields.Add (rmapping);
					}
				}
			}
		}

		void InitialExtendParams ()
		{
			ExtendParamCollection extendParams = ConfigManager.LoadDataTableExtendParamsConfig (ObjectType);
			if (extendParams != null) {
				this.ExtentParams = extendParams;
			}
			else {
				this.ExtentParams = new ExtendParamCollection ();
			}
		}

		//public bool IsSupportInnerPage {
		//	get {
		//		return this.singleJoinTableRelationFields.Count == 0;
		//	}
		//}

		bool _isDataEntity;

		public bool IsDataEntity {
			get {
				return _isDataEntity;
			}
		}

		string _tableName;

		public string TableName {
			get {
				return _tableName;
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
			PropertyInfo [] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
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
				throw new LightDataException (RE.NoDataFields);
			}
			list.Sort ((x, y) => {
				if (x.DataOrder.HasValue && y.DataOrder.HasValue) {
					if (x.DataOrder > y.DataOrder) {
						return 1;
					}
					else if (x.DataOrder < y.DataOrder) {
						return -1;
					}
					else {
						return x.FieldOrder > y.FieldOrder ? 1 : -1;
					}
				}
				else if (x.DataOrder.HasValue && !y.DataOrder.HasValue) {
					return -1;
				}
				else if (!x.DataOrder.HasValue && y.DataOrder.HasValue) {
					return 1;
				}
				else {
					return x.FieldOrder > y.FieldOrder ? 1 : -1;
				}
			});

			for (int i = 0; i < list.Count; i++) {
				FieldInfo info = list [i];
				DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (info.Property, info.Config, i + 1, this);
				_fieldMappingDictionary.Add (mapping.IndexName, mapping);
				if (mapping.Name != mapping.IndexName) {
					_fieldMappingDictionary.Add (mapping.Name, mapping);
				}
				_fieldList.Add (mapping);
			}
		}

		public IEnumerable<DataFieldMapping> DataEntityFields {
			get {
				foreach (DataFieldMapping item in _fieldList) {
					yield return item;
				}
			}
		}

		object joinLock = new object ();

		RelationMap relationMap;

		public RelationMap GetRelationMap ()
		{
			if (this.relationMap == null) {
				lock (joinLock) {
					if (this.relationMap == null) {
						this.relationMap = new RelationMap (this);
					}
				}
			}
			return this.relationMap;
		}

		//public JoinCapsule LoadJoinCapsule (QueryExpression query, OrderExpression order)
		//{
		//	if (singleJoinTableRelationFields.Count == 0) {
		//		//todo add no relate exception
		//		throw new LightDataException ("");
		//	}
		//	if (this.relationMap == null) {
		//		lock (joinLock) {
		//			if (this.relationMap == null) {
		//				this.relationMap = new RelationMap (this);
		//			}
		//		}
		//	}
		//	return this.relationMap.CreateJoinCapsule (query, order);
		//}

		public DataFieldMapping FindDataEntityField (string fieldName)
		{
			FieldMapping mapping;
			_fieldMappingDictionary.TryGetValue (fieldName, out mapping);
			return mapping as DataFieldMapping;
		}

		public bool HasJoinRelateModel {
			get {
				return singleRelationFields.Count > 0;
			}
		}

		//public bool HasMultiRelateModel {
		//	get {
		//		return singleMultiQueryRelationFields.Count > 0;
		//	}
		//}

		public void LoadJoinTableData (DataContext context, IDataReader datareader, object item, QueryState queryState, string fieldPath)
		{
			string aliasName = queryState.GetAliasName (fieldPath);
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datareader, queryState);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
					string name = string.Format ("{0}_{1}", aliasName, field.Name);
					if (queryState.CheckSelectField (name)) {
						object obj = datareader [name];
						object value = field.ToProperty (obj);
						if (!Object.Equals (value, null)) {
							field.Handler.Set (item, value);
						}
					}
				}
			}
			if (collectionRelationFields.Count > 0) {
				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
					mapping.Handler.Set (item, mapping.ToProperty (context, item, true));
				}
			}
			//if (singleMultiQueryRelationFields.Count > 0) {
			//	foreach (SingleRelationFieldMapping mapping in singleMultiQueryRelationFields) {
			//		object value = mapping.ToProperty (context, item, datas);
			//		if (!Object.Equals (value, null)) {
			//			mapping.Handler.Set (item, value);
			//		}
			//	}
			//}

			foreach (SingleRelationFieldMapping mapping in singleRelationFields) {
				string fpath = string.Format ("{0}.{1}", fieldPath, mapping.FieldName);
				object value = mapping.ToProperty (context, datareader, queryState, fpath);
				if (!Object.Equals (value, null)) {
					mapping.Handler.Set (item, value);
				}
			}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
		{
			object item = Activator.CreateInstance (ObjectType);
			QueryState queryState = state as QueryState;
			if (this.singleRelationFields.Count > 0) {
				queryState.InitialJoinData ();
				queryState.SetJoinData (string.Empty, item);
				LoadJoinTableData (context, datareader, item, queryState, string.Empty);
				return item;
			}

			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datareader, state);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
					if (queryState == null) {
						object obj = datareader [field.Name];
						object value = field.ToProperty (obj);
						if (!Object.Equals (value, null)) {
							field.Handler.Set (item, value);
						}
					}
					else if (queryState.CheckSelectField (field.Name)) {
						object obj = datareader [field.Name];
						object value = field.ToProperty (obj);
						if (!Object.Equals (value, null)) {
							field.Handler.Set (item, value);
						}
					}
				}
			}
			if (collectionRelationFields.Count > 0) {
				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
					mapping.Handler.Set (item, mapping.ToProperty (context, item, true));
				}
			}
			//if (singleMultiQueryRelationFields.Count > 0) {
			//	QueryContent datas = state as QueryContent;
			//	foreach (SingleRelationFieldMapping mapping in singleMultiQueryRelationFields) {
			//		object value = mapping.ToProperty (context, item, datas);
			//		if (!Object.Equals (value, null)) {
			//			mapping.Handler.Set (item, value);
			//		}
			//	}
			//}
			if (IsDataEntity) {
				DataEntity de = item as DataEntity;
				de.SetContext (context);
				de.LoadDataComplete ();
			}
			return item;
		}

		public object LoadJoinTableData (DataContext context, IDataReader datareader, QueryState queryState, string aliasName)
		{
			object item = Activator.CreateInstance (ObjectType);
		
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datareader, queryState);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
					string name = string.Format ("{0}_{1}", aliasName, field.Name);
					if (queryState == null) {
						object obj = datareader [name];
						object value = field.ToProperty (obj);
						if (!Object.Equals (value, null)) {
							field.Handler.Set (item, value);
						}
					}
					else if (queryState.CheckSelectField (name)) {
						object obj = datareader [name];
						object value = field.ToProperty (obj);
						if (!Object.Equals (value, null)) {
							field.Handler.Set (item, value);
						}
					}
				}
			}
			if (collectionRelationFields.Count > 0) {
				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
					mapping.Handler.Set (item, mapping.ToProperty (context, item, false));
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

		#endregion

		class FieldInfo
		{
			public FieldInfo (PropertyInfo property, IDataFieldConfig config, int fieldOrder)
			{
				this.property = property;
				this.config = config;
				this.fieldOrder = fieldOrder;
				if (config.DataOrder > 0) {
					this.dataOrder = config.DataOrder;
				}
			}

			readonly PropertyInfo property;

			public PropertyInfo Property {
				get {
					return property;
				}
			}

			readonly IDataFieldConfig config;

			public IDataFieldConfig Config {
				get {
					return config;
				}
			}

			readonly int? dataOrder;

			public int? DataOrder {
				get {
					return dataOrder;
				}
			}

			readonly int fieldOrder;

			public int FieldOrder {
				get {
					return fieldOrder;
				}
			}
		}
	}
}
