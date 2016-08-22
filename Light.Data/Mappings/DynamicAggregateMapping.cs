using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Light.Data
{
	class DynamicAggregateMapping : AggregateMapping
	{
		protected Dictionary<string, DynamicFieldMapping> _fieldMappingDictionary = new Dictionary<string, DynamicFieldMapping> ();

		protected ReadOnlyCollection<DynamicFieldMapping> _fieldList;

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
			List<DynamicFieldMapping> tmepList = new List<DynamicFieldMapping> ();
			foreach (PropertyInfo pi in propertys) {
				DynamicFieldMapping mapping = DynamicFieldMapping.CreateAggregateFieldMapping (pi, this);
				_fieldMappingDictionary.Add (mapping.IndexName, mapping);
				tmepList.Add (mapping);
			}
			if (tmepList.Count == 0) {
				throw new LightDataException (RE.NoAggregationFields);
			}
			_fieldList = new ReadOnlyCollection<DynamicFieldMapping> (tmepList);
		}

		public int FieldCount {
			get {
				return this._fieldList.Count;
			}
		}

		public ReadOnlyCollection<DynamicFieldMapping> DataEntityFields {
			get {
				return _fieldList;
			}
		}

		public override object InitialData ()
		{
			object [] args = new object [this._fieldList.Count];
			object item = Activator.CreateInstance (ObjectType, args);
			return item;
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

