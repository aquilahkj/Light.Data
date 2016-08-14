using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Light.Data
{
	class DynamicAggregateMapping : AggregateMapping
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, DynamicAggregateMapping> _defaultMapping = new Dictionary<Type, DynamicAggregateMapping> ();

		public static DynamicAggregateMapping GetAggregateMapping (Type type)
		{
			Dictionary<Type, DynamicAggregateMapping> mappings = _defaultMapping;
			DynamicAggregateMapping mapping;
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

		private static DynamicAggregateMapping CreateMapping (Type type)
		{
			DynamicAggregateMapping aggregateMapping = new DynamicAggregateMapping (type);
			return aggregateMapping;
		}

		#endregion


		public DynamicAggregateMapping (Type type)
			: base (type)
		{
			InitialAggregateFieldMapping ();
		}

		private void InitialAggregateFieldMapping ()
		{
			PropertyInfo [] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				DynamicFieldMapping mapping = DynamicFieldMapping.CreateAggregateFieldMapping (pi, this);
				_fieldMappingDictionary.Add (mapping.IndexName, mapping);
				_fieldList.Add (mapping);
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.NoAggregationFields);
			}
		}


		public override object InitialData ()
		{
			object [] args = new object [this._fieldList.Count];
			object item = Activator.CreateInstance (ObjectType, args);
			return args;
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
		{
			object [] args = new object [this._fieldList.Count];
			int index = 0;
			foreach (DynamicFieldMapping field in this._fieldList) {
				object obj = datareader [field.Name];
				object value = field.ToProperty (obj);
				args [index] = value;
				index++;
			}
			object item = Activator.CreateInstance (ObjectType, args);
			return item;
		}
	}
}

