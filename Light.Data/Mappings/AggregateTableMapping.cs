using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class AggregateTableMapping : DataMapping
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, AggregateTableMapping> _defaultMapping = new Dictionary<Type, AggregateTableMapping> ();

		public static AggregateTableMapping GetAggregateMapping (Type type)
		{
			Dictionary<Type, AggregateTableMapping> mappings = _defaultMapping;
			AggregateTableMapping mapping = null;
			if (!mappings.TryGetValue (type, out mapping)) {
				lock (_synobj) {
					if (!mappings.ContainsKey (type)) {
						mapping = CreateMapping (type);
						mappings [type] = mapping;
					}
				}
			}
			return mapping;
		}

		private static AggregateTableMapping CreateMapping (Type type)
		{
//			Type relateType = null;
			string extendParam = null;
			AggregateTableMapping aggregateMapping = null;
			IAggregateTableConfig config = ConfigManager.LoadAggregateTableConfig (type);
			if (config != null) {
//				relateType = config.RelateType;
				extendParam = config.ExtendParams;
			}
//			aggregateMapping = new AggregateTableMapping (type, relateType);
			aggregateMapping = new AggregateTableMapping (type);
			aggregateMapping.ExtentParams = new ExtendParamsCollection (extendParam);
			return aggregateMapping;
		}

		#endregion

//		AggregateTableMapping (Type type, Type relateType)
//			: base (type)
//		{
//			if (relateType != null && relateType != type) {
//				RelateType = relateType;
//			}
//			InitialDataFieldMapping ();
//		}


		AggregateTableMapping (Type type)
			: base (type)
		{
			InitialDataFieldMapping ();
		}

//		public Type RelateType {
//			get;
//			private set;
//		}

		private void InitialDataFieldMapping ()
		{
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				IAggregateFieldConfig config = ConfigManager.LoadAggregateFieldConfig (pi);
				if (config != null) {
					Type type = pi.PropertyType;
					string name = string.IsNullOrEmpty (config.Name) ? pi.Name : config.Name;
					DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (type, pi, name, pi.Name, config, this);
					mapping.Handler = new PropertyHandler (pi);
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					_fieldList.Add (mapping);
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.AggregationFieldsIsNotExists);
			}
		}

//		public override IEnumerable<FieldMapping> GetFieldMappings ()
//		{
//			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingDictionary) {
//				yield return kv.Value;
//			}
//		}

//		public override FieldMapping FindFieldMapping (string fieldName)
//		{
//			if (_fieldMappingDictionary.ContainsKey (fieldName)) {
//				return _fieldMappingDictionary [fieldName];
//			}
//			else {
//				throw new LightDataException (string.Format (RE.FieldMappingIsNotExists, fieldName));
//			}
//		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, datareader);
			return item;
		}

		private void LoadDataField (object source, IFieldCollection collection, IDataReader datareader)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
				if (field == null)
					continue;
				object obj = datareader [field.Name];
				bool isnull = Object.Equals (obj, DBNull.Value);
				if (!isnull) {
					field.Handler.Set (source, field.ToProperty (obj));
				}
			}
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object item = Activator.CreateInstance (ObjectType);
			LoadDataField (item, this, datarow);
			return item;
		}

		private void LoadDataField (object source, IFieldCollection collection, DataRow datarow)
		{
			foreach (DataFieldMapping field in collection.GetFieldMappings()) {
				if (field == null)
					continue;
				object obj = datarow [field.Name];
				bool isnull = Object.Equals (obj, DBNull.Value);
				if (!isnull) {
					field.Handler.Set (source, field.ToProperty (obj));
				}
			}
		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			return item;
		}
	}
}
