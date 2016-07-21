using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Table config.
	/// </summary>
	abstract class TableConfig
	{
		readonly Dictionary<string, IConfiguratorFieldConfig> _fieldConfigDictionary = new Dictionary<string, IConfiguratorFieldConfig> ();

		ExtendParamCollection _extendParams = null;

		public ExtendParamCollection ExtendParams {
			get {
				return _extendParams;
			}
			set {
				_extendParams = value;
			}
		}

		/// <summary>
		/// Sets the field.
		/// </summary>
		/// <param name="config">Config.</param>
		public void SetField (IConfiguratorFieldConfig config)
		{
			this.SetField (config.FieldName, config);
		}

		/// <summary>
		/// Sets the field.
		/// </summary>
		/// <param name="fieldName">Field name.</param>
		/// <param name="config">Config.</param>
		public void SetField (string fieldName, IConfiguratorFieldConfig config)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException (nameof (fieldName));
			}
			if (config == null) {
				throw new ArgumentNullException (nameof (config));
			}
			_fieldConfigDictionary.Add (fieldName, config);
		}

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <returns>The field.</returns>
		/// <param name="fieldName">Field name.</param>
		public IConfiguratorFieldConfig GetField (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException (nameof (fieldName));
			}
			IConfiguratorFieldConfig config;
			_fieldConfigDictionary.TryGetValue (fieldName, out config);
			return config;
		}

		/// <summary>
		/// Gets or sets the <see cref="Light.Data.TableConfig"/> with the specified fieldName.
		/// </summary>
		/// <param name="fieldName">Field name.</param>
		public IConfiguratorFieldConfig this [string fieldName] {
			get {
				return GetField (fieldName);
			}
			set {
				SetField (fieldName, value);
			}
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<IConfiguratorFieldConfig> GetEnumerator ()
		{
			foreach (KeyValuePair<string, IConfiguratorFieldConfig> kv in _fieldConfigDictionary) {
				yield return kv.Value;
			}
		}
	}
}
