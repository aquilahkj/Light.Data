using System;
using System.Collections.Generic;
using System.Collections;

namespace Light.Data
{
	abstract class TableConfig //: IEnumerable<IConfiguratorFieldConfig>
	{
		readonly Dictionary<string, IConfiguratorFieldConfig> _fieldConfigDictionary = new Dictionary<string, IConfiguratorFieldConfig> ();

		public void SetField (IConfiguratorFieldConfig config)
		{
			this.SetField (config.FieldName, config);
		}

		public void SetField (string fieldName, IConfiguratorFieldConfig config)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException ("fieldName");
			}
			if (config == null) {
				throw new ArgumentNullException ("config");
			}
			_fieldConfigDictionary.Add (fieldName, config);
		}

		public IConfiguratorFieldConfig GetField (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException ("fieldName");
			}
			IConfiguratorFieldConfig config;
			_fieldConfigDictionary.TryGetValue (fieldName, out config);
			return config;
		}

		public IConfiguratorFieldConfig this [string fieldName] {
			get {
				return GetField (fieldName);
			}
			set {
				SetField (fieldName, value);
			}
		}

		public IEnumerator<IConfiguratorFieldConfig> GetEnumerator ()
		{
			foreach (KeyValuePair<string, IConfiguratorFieldConfig> kv in _fieldConfigDictionary) {
				yield return kv.Value;
			}
		}

//		IEnumerator IEnumerable.GetEnumerator ()
//		{
//			foreach (KeyValuePair<string, IConfiguratorFieldConfig> kv in _fieldConfigDictionary) {
//				yield return kv.Value;
//			}
//		}
	}
}
