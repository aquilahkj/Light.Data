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
		protected List<CollectionRelationFieldMapping> collectionRelationFields = new List<CollectionRelationFieldMapping> ();

		protected List<SingleRelationFieldMapping> singleMultiQueryRelationFields = new List<SingleRelationFieldMapping> ();

		protected List<SingleRelationFieldMapping> singleJoinTableRelationFields = new List<SingleRelationFieldMapping> ();

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

		internal SingleRelationFieldMapping[] GetSingleRelationFieldMappings ()
		{
			int len = this.singleMultiQueryRelationFields.Count + this.singleJoinTableRelationFields.Count;
			SingleRelationFieldMapping[] array = new SingleRelationFieldMapping[len];
			int index = 0;
			foreach (SingleRelationFieldMapping item in  this.singleMultiQueryRelationFields) {
				array [index] = item;
				index++;
			}
			foreach (SingleRelationFieldMapping item in  this.singleJoinTableRelationFields) {
				array [index] = item;
				index++;
			}
			return array;
		}

		internal SingleRelationFieldMapping[] GetSingleRequeryRelationFieldMappings ()
		{
			int len = this.singleMultiQueryRelationFields.Count;
			SingleRelationFieldMapping[] array = new SingleRelationFieldMapping[len];
			int index = 0;
			foreach (SingleRelationFieldMapping item in  this.singleMultiQueryRelationFields) {
				array [index] = item;
				index++;
			}
			return array;
		}

		internal SingleRelationFieldMapping[] GetSingleJoinTableRelationFieldMappings ()
		{
			int len = this.singleJoinTableRelationFields.Count;
			SingleRelationFieldMapping[] array = new SingleRelationFieldMapping[len];
			int index = 0;
			foreach (SingleRelationFieldMapping item in  this.singleJoinTableRelationFields) {
				array [index] = item;
				index++;
			}
			return array;
		}

		private void InitialRelationField ()
		{
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				IRelationFieldConfig config = ConfigManager.LoadRelationFieldConfig (pi);
				if (config != null && config.RelationKeyCount > 0) {
					Type type = pi.PropertyType;
					if (type.IsGenericType) {
						Type frameType = type.GetGenericTypeDefinition ();
						if (frameType.FullName == "Light.Data.LCollection`1" || frameType.FullName == "System.Collections.Generic.ICollection`1") {
							Type[] arguments = type.GetGenericArguments ();
							type = arguments [0];
							PropertyHandler handler = new PropertyHandler (pi);
							RelationKey[] keypairs = config.GetRelationKeys ();
							CollectionRelationFieldMapping rmapping = new CollectionRelationFieldMapping (pi.Name, this, type, keypairs, handler);
							collectionRelationFields.Add (rmapping);
						}
					}
					else {
						PropertyHandler handler = new PropertyHandler (pi);
						RelationKey[] keypairs = config.GetRelationKeys ();
						SingleRelationFieldMapping rmapping = new SingleRelationFieldMapping (pi.Name, this, type, keypairs, handler);
						if (config.RelationMode == RelationMode.MultiQuery) {
							singleMultiQueryRelationFields.Add (rmapping);
						}
						else {
							singleJoinTableRelationFields.Add (rmapping);
						}

					}
				}
			}
		}

		public bool IsSupportInnerPage {
			get {
				return this.singleJoinTableRelationFields.Count == 0;
			}
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
//				if (_aliasName != null) {
//					return DataEntityMapping._aliasName;
//				}
//				else {
//					return _tableName;
//				}
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
				throw new LightDataException (RE.NoDataFields);
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

		//		JoinCapsule baseJoinCapsule;

		RelationMap relationMap;

		//		void InitialRelationMap ()
		//		{
		//			RelationMap rmap = new RelationMap (this);
		//			JoinSelector selector = new JoinSelector ();
		//			List<JoinModel> models = new List<JoinModel> ();
		//			HashSet<string> tables = new HashSet<string> ();
		//			JoinModel mainModel = new JoinModel (this, null, null, null);
		//			mainModel.AliasTableName = "t";
		//			models.Add (mainModel);
		//			foreach (DataFieldMapping field in this._fieldList) {
		//				DataFieldInfo info = new DataFieldInfo (field);
		//				AliasDataFieldInfo alias = new AliasDataFieldInfo (info, string.Format ("{0}_{1}", "t", info.FieldName));
		//				alias.AliasTableName = "t";
		//				selector.SetAliasDataField (alias);
		//			}
		//
		//			foreach (JoinItem item in rmap.GetJoinItems()) {
		//				DataEntityMapping mapping = item.Mapping;
		//				DataFieldExpression expression = item.Expression;
		//				foreach (DataFieldMapping field in mapping._fieldList) {
		//					DataFieldInfo info = new DataFieldInfo (field);
		//					AliasDataFieldInfo alias = new AliasDataFieldInfo (info, string.Format ("{0}_{1}", item.AliasTableName, info.FieldName));
		//					alias.AliasTableName = item.AliasTableName;
		//					selector.SetAliasDataField (alias);
		//				}
		////				if (tables.Contains (mapping.TableName)) {
		////					continue;
		////				}
		////				else {
		////					tables.Add (mapping.TableName);
		////				}
		//				JoinConnect connect = new JoinConnect (JoinType.LeftJoin, expression);
		//				JoinModel model = new JoinModel (mapping, connect, null, null);
		//				model.AliasTableName = item.AliasTableName;
		//				models.Add (model);
		//			}
		//			this.baseJoinCapsule = new JoinCapsule (selector, models, rmap);
		//		}

		public JoinCapsule LoadJoinCapsule (QueryExpression query, OrderExpression order)
		{
			if (singleJoinTableRelationFields.Count == 0) {
				return null;
			}
			if (this.relationMap == null) {
				lock (joinLock) {
					if (this.relationMap == null) {
						this.relationMap = new RelationMap (this);
//						InitialRelationMap ();
					}
				}
			}
			return this.relationMap.CreateJoinCapsule (query, order);
//			return baseJoinCapsule.CloneCapsule (query, order);
		}

		public DataFieldMapping FindDataEntityField (string fieldName)
		{
			FieldMapping mapping;
			_fieldMappingDictionary.TryGetValue (fieldName, out mapping);
			return mapping as DataFieldMapping;
		}

		public bool HasJoinRelateModel {
			get {
				return singleJoinTableRelationFields.Count > 0;
			}
		}

		public bool HasMultiRelateModel {
			get {
				return  singleMultiQueryRelationFields.Count > 0;
			}
		}

		public void LoadJoinTableData (DataContext context, IDataReader datareader, object item, RelationContent datas, string aliasName)
		{
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;

				IFieldCollection fieldCollection = field as IFieldCollection;
				if (fieldCollection != null) {
					IFieldCollection ifc = fieldCollection;
					object obj = ifc.LoadData (context, datareader, datas);
					if (!Object.Equals (obj, null)) {
						field.Handler.Set (item, obj);
					}
				}
				else {
					string name = string.Format ("{0}_{1}", aliasName, field.Name);
					object obj = datareader [name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
				}
			}
			if (collectionRelationFields.Count > 0) {
				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
					mapping.Handler.Set (item, mapping.ToProperty (context, item));
				}
			}
			if (singleMultiQueryRelationFields.Count > 0) {
				foreach (SingleRelationFieldMapping mapping in singleMultiQueryRelationFields) {
					object value = mapping.ToProperty (context, item, datas);
					if (!Object.Equals (value, null)) {
						mapping.Handler.Set (item, value);
					}
				}
			}

			foreach (SingleRelationFieldMapping mapping in singleJoinTableRelationFields) {
				object value = mapping.ToProperty (context, datareader, datas);
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
			if (this.singleJoinTableRelationFields.Count > 0) {
				RelationContent datas = state as RelationContent;
				datas.InitialJoinData ();
				datas.SetRootJoinData (this, item);
				string aliasName = datas.GetRootAliasName ();
				LoadJoinTableData (context, datareader, item, datas, aliasName);
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
					object obj = datareader [field.Name];
					object value = field.ToProperty (obj);
					if (!Object.Equals (value, null)) {
						field.Handler.Set (item, value);
					}
				}
			}
			if (collectionRelationFields.Count > 0) {
				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
					mapping.Handler.Set (item, mapping.ToProperty (context, item));
				}
			}
			if (singleMultiQueryRelationFields.Count > 0) {
				RelationContent datas = state as RelationContent;
				foreach (SingleRelationFieldMapping mapping in singleMultiQueryRelationFields) {
					object value = mapping.ToProperty (context, item, datas);
					if (!Object.Equals (value, null)) {
						mapping.Handler.Set (item, value);
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

		//		public override object LoadData (DataContext context, DataRow datarow)
		//		{
		//			object item = Activator.CreateInstance (ObjectType);
		//			foreach (DataFieldMapping field in this._fieldList) {
		//				if (field == null)
		//					continue;
		//
		//				IFieldCollection fieldCollection = field as IFieldCollection;
		//				if (fieldCollection != null) {
		//					IFieldCollection ifc = fieldCollection;
		//					object obj = ifc.LoadData (context, datarow);
		//					if (!Object.Equals (obj, null)) {
		//						field.Handler.Set (item, obj);
		//					}
		//				}
		//				else {
		//					object obj = datarow [field.Name];
		//					object value = field.ToProperty (obj);
		//					if (!Object.Equals (value, null)) {
		//						field.Handler.Set (item, value);
		//					}
		//				}
		//			}
		//			if (collectionRelationFields.Count > 0) {
		//				foreach (CollectionRelationFieldMapping mapping in collectionRelationFields) {
		//					mapping.Handler.Set (item, mapping.ToProperty (context, item));
		//				}
		//			}
		//			if (singleRequeryRelationFields.Count > 0) {
		//				foreach (SingleRelationFieldMapping mapping in singleRequeryRelationFields) {
		//					mapping.Handler.Set (item, mapping.ToProperty (context, item));
		//				}
		//			}
		//			if (IsDataEntity) {
		//				DataEntity de = item as DataEntity;
		//				de.SetContext (context);
		//				de.LoadDataComplete ();
		//			}
		//			return item;
		//		}
		//
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

		//		[ThreadStatic]
		//		static string _aliasName;
		//
		//		public void SetAliasName (string name)
		//		{
		//			if (string.IsNullOrEmpty (name)) {
		//				throw new ArgumentNullException ("name");
		//			}
		//			DataEntityMapping._aliasName = name;
		//		}
		//
		//		public void ClearAliasName ()
		//		{
		//			DataEntityMapping._aliasName = null;
		//		}

		//		string _tableName;
		//
		//		public string TableName {
		//			get {
		//								if (_aliasName != null) {
		//									return DataEntityMapping._aliasName;
		//								}
		//								else {
		//									return _tableName;
		//								}
		//				return _tableName;
		//			}
		//		}


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
