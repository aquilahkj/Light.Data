using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Light.Data.Config;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	class ComplexFieldMapping : DataFieldMapping, IFieldCollection
	{
		public ComplexFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable)
			: base (type, fieldName, indexName, mapping, isNullable, null)
		{
//			ObjectType = type;
//			Name = fieldName;
//			IndexName = indexName;
//			TypeMapping = mapping;
			ParseSubDataField ();
		}

		private void ParseSubDataField ()
		{
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				IDataFieldConfig config = ConfigManager.LoadDataFieldConfig (pi);
				if (config != null) {
					Type type = pi.PropertyType;
					string name = string.IsNullOrEmpty (config.Name) ? pi.Name : config.Name;
					string filedName = string.Format ("{0}_{1}", Name, name);
					string indexName = string.Format ("{0}_{1}", IndexName, pi.Name);
					DataFieldMapping mapping = DataFieldMapping.CreateDataFieldMapping (type, pi, filedName, indexName, config, EntityMapping, ObjectType);
					PrimitiveFieldMapping primitiveFieldMapping = mapping as PrimitiveFieldMapping;
					if (primitiveFieldMapping != null) {
						primitiveFieldMapping.IsIdentity = false;
						primitiveFieldMapping.IsPrimaryKey = false;
					}
					mapping.Handler = new PropertyHandler (pi);
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					if (mapping.Name != mapping.IndexName) {
						_fieldMappingAlterNameDictionary.Add (mapping.Name, mapping);
					}
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.ComplexFieldHaveNotSubFields);
			}
		}

		Dictionary<string, FieldMapping> _fieldMappingDictionary = new Dictionary<string, FieldMapping> ();

		Dictionary<string, FieldMapping> _fieldMappingAlterNameDictionary = new Dictionary<string, FieldMapping> ();

		public override object ToProperty (object value)
		{
			return null;
		}

		public override object ToColumn (object value)
		{
			return null;
		}

		public override string DBType {
			get {
				return null;
			}
		}

		public override bool IsNullable {
			get {
				return false;
			}
		}

		#region IFieldCollection 成员

		public IEnumerable<FieldMapping> GetFieldMappings ()
		{
			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingDictionary) {
				yield return kv.Value;
			}
		}

		public FieldMapping FindFieldMapping (string fieldName)
		{
			if (_fieldMappingDictionary.ContainsKey (fieldName)) {
				FieldMapping m = _fieldMappingDictionary [fieldName];
				if (m is PrimitiveFieldMapping || m is EnumFieldMapping) {
					return m;
				}
			}

			if (_fieldMappingAlterNameDictionary.ContainsKey (fieldName)) {
				FieldMapping m = _fieldMappingAlterNameDictionary [fieldName];
				if (m is PrimitiveFieldMapping || m is EnumFieldMapping) {
					return m;
				}
			}

			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingDictionary) {
				if (fieldName.StartsWith (kv.Key + "_") && kv.Value is ComplexFieldMapping) {
					return ((ComplexFieldMapping)kv.Value).FindFieldMapping (fieldName);
				}
			}

			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingAlterNameDictionary) {
				if (fieldName.StartsWith (kv.Key + "_") && kv.Value is ComplexFieldMapping) {
					return ((ComplexFieldMapping)kv.Value).FindFieldMapping (fieldName);
				}
			}
			return null;
		}

		public object LoadData (DataContext context, IDataReader datareader)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, context, datareader);
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

		public object LoadData (DataContext context, DataRow datarow)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, context, datarow);
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

		public object InitialData ()
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

		#endregion
	}
}
