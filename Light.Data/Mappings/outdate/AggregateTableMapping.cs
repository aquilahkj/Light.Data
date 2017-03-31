using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Light.Data
{
	/// <summary>
	/// Aggregate table mapping.
	/// </summary>
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
			IAggregateTableConfig config = ConfigManager.LoadAggregateTableConfig (type);
			if (config == null) {
				throw new LightDataException (string.Format (RE.TheTypeOfAggregateTableIsNoConfig, type.Name));
			}
			AggregateTableMapping aggregateMapping = new AggregateTableMapping (type);
			return aggregateMapping;
		}

		#endregion

		protected Dictionary<string, DataFieldMapping> _fieldMappingDictionary = new Dictionary<string, DataFieldMapping> ();

		protected List<DataFieldMapping> _fieldList = new List<DataFieldMapping> ();

		AggregateTableMapping (Type type)
			: base (type)
		{
			InitialDataFieldMapping ();
			InitialExtendParams ();
		}

		private void InitialDataFieldMapping ()
		{
			PropertyInfo[] propertys = ObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				IAggregateFieldConfig config = ConfigManager.LoadAggregateFieldConfig (pi);
				if (config != null) {
					DataFieldMapping mapping = DataFieldMapping.CreateAggregateFieldMapping (pi, config, this);
					_fieldMappingDictionary.Add (mapping.IndexName, mapping);
					_fieldList.Add (mapping);
				}
			}
			if (_fieldMappingDictionary.Count == 0) {
				throw new LightDataException (RE.NoAggregationFields);
			}
		}

		private void InitialExtendParams ()
		{
			ExtendParamCollection extendParams = ConfigManager.LoadAggregateExtendParamsConfig (ObjectType);
			if (extendParams != null) {
				this.ExtentParams = extendParams;
			}
			else {
				this.ExtentParams = new ExtendParamCollection ();
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
