using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Light.Data
{
	class SpecialAggregateMapping : AggregateMapping
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, SpecialAggregateMapping> _defaultMapping = new Dictionary<Type, SpecialAggregateMapping> ();

		public static SpecialAggregateMapping GetAggregateMapping (Type type)
		{
			Dictionary<Type, SpecialAggregateMapping> mappings = _defaultMapping;
			SpecialAggregateMapping mapping;
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

		private static SpecialAggregateMapping CreateMapping (Type type)
		{
			SpecialAggregateMapping aggregateMapping = new SpecialAggregateMapping (type);
			return aggregateMapping;
		}

		#endregion

		SpecialAggregateMapping (Type type)
			: base (type)
		{
			InitialDataFieldMapping ();
		}

		private void InitialDataFieldMapping ()
		{
			PropertyInfo [] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (pi, this);
				if (mapping != null) {
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					_fieldList.Add (mapping);
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.NoAggregationFields);
			}
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
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

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			return item;
		}
	}
}

