using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

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
			AggregateTableMapping mapping;
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
			string extendParam = null;
			AggregateTableMapping aggregateMapping;
			IAggregateTableConfig config = ConfigManager.LoadAggregateTableConfig (type);
			if (config != null) {
				extendParam = config.ExtendParams;
			}
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
//					Type type = pi.PropertyType;
//					string name = string.IsNullOrEmpty (config.Name) ? pi.Name : config.Name;
//					DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (type, pi, name, pi.Name, config, this);
//					mapping.Handler = new PropertyHandler (pi);
					DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (pi, config, this);
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
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;
				object obj = datareader [field.Name];
				object value = field.ToProperty (obj);
				if (!Object.Equals (value, null)) {
					field.Handler.Set (item, value);
				}
			}
			return item;
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object item = Activator.CreateInstance (ObjectType);
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;
				object obj = datarow [field.Name];
				object value = field.ToProperty (obj);
				if (!Object.Equals (value, null)) {
					field.Handler.Set (item, value);
				}
			}
			return item;
		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			return item;
		}
	}
}
