
namespace Light.Data
{
	/// <summary>
	/// Basic field info.
	/// </summary>
	public abstract class BasicFieldInfo
	{
		DataFieldMapping _dataField = null;

		internal DataFieldMapping DataField {
			get {
				return _dataField;
			}
			set {
				_dataField = value;
			}
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
			set {
				_tableMapping = value;
			}
		}

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
