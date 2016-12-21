
namespace Light.Data
{
	/// <summary>
	/// Basic field info.
	/// </summary>
	public abstract class BasicFieldInfo
	{
		internal BasicFieldInfo (DataEntityMapping tableMapping)
		{
			if (tableMapping == null)
				throw new System.ArgumentNullException (nameof (tableMapping));
			_tableMapping = tableMapping;
		}

		internal BasicFieldInfo (DataEntityMapping tableMapping, DataFieldMapping dataField)
		{
			if (tableMapping == null)
				throw new System.ArgumentNullException (nameof (tableMapping));
			if (dataField == null)
				throw new System.ArgumentNullException (nameof (dataField));
			_tableMapping = tableMapping;
			_dataField = dataField;
		}

		internal BasicFieldInfo (DataEntityMapping tableMapping, bool customName, string name)
		{
			if (tableMapping == null)
				throw new System.ArgumentNullException (nameof (tableMapping));
			if (name == null)
				throw new System.ArgumentNullException (nameof (name));
			_tableMapping = tableMapping;
			if (customName) {
				_dataField = new CustomFieldMapping (name, tableMapping);
			}
			else {
				_dataField = TableMapping.FindDataEntityField (name);
				if (DataField == null) {
					_dataField = new CustomFieldMapping (name, tableMapping);
				}
			}
		}


		DataFieldMapping _dataField = null;

		internal DataFieldMapping DataField {
			get {
				return _dataField;
			}
			//set {
			//	_dataField = value;
			//}
		}

		DataEntityMapping _tableMapping = null;

		/// <summary>
		/// Gets or sets the table mapping.
		/// </summary>
		/// <value>The table mapping.</value>
		internal DataEntityMapping TableMapping {
			get {
				return _tableMapping;
			}
			//set {
			//	_tableMapping = value;
			//}
		}

		string _fieldName = null;

		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		/// <value>The name of the field.</value>
		public virtual string FieldName {
			get {
				if (_dataField != null) {
					return _dataField.Name;
				}
				else {
					return null;
				}
			}
		}
	}
}
