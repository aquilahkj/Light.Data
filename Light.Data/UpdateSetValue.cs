using System;

namespace Light.Data
{
	/// <summary>
	/// Update set value in bitch.
	/// </summary>
	public class UpdateSetValue
	{
		readonly DataFieldInfo _dataField = null;

		readonly object _value = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.UpdateSetValue"/> class.
		/// </summary>
		/// <param name="dataField">Data field.</param>
		/// <param name="value">Value.</param>
		public UpdateSetValue (DataFieldInfo dataField, object value)
		{
			if (Object.Equals (dataField, null)) {
				throw new ArgumentNullException (nameof (dataField));
			}
			_dataField = dataField;
			_value = value;
		}

		/// <summary>
		/// Gets the data field.
		/// </summary>
		/// <value>The data field.</value>
		public DataFieldInfo DataField {
			get {
				return _dataField;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value {
			get {
				return _value;
			}
		}
	}
}
