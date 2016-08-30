using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		protected Dictionary<string, DataFieldMapping> _fieldMappingDictionary = new Dictionary<string, DataFieldMapping> ();

		//protected List<DataFieldMapping> _fieldList = new List<DataFieldMapping> ();

		protected ReadOnlyCollection<DataFieldMapping> _fieldList;

		SpecialAggregateMapping (Type type)
			: base (type)
		{
			InitialDataFieldMapping ();
		}

		private void InitialDataFieldMapping ()
		{
			PropertyInfo [] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			List<DataFieldMapping> tmepList = new List<DataFieldMapping> ();
			foreach (PropertyInfo pi in propertys) {
				DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (pi, this);
				if (mapping != null) {
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					tmepList.Add (mapping);
				}
			}
			if (tmepList.Count == 0) {
				throw new LightDataException (RE.NoAggregationFields);
			}
			_fieldList = new ReadOnlyCollection<DataFieldMapping> (tmepList);
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

		public override object CreateJoinTableData (DataContext context, IDataReader datareader, QueryState queryState, string aliasName)
		{
			object item = Activator.CreateInstance (ObjectType);
			foreach (DataFieldMapping field in this._fieldList) {
				if (field == null)
					continue;
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
			return item;
		}

		public override object InitialData ()
		{
			object item = Activator.CreateInstance (ObjectType);
			return item;
		}
	}
}

