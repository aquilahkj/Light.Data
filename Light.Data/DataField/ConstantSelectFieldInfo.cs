
namespace Light.Data
{
	/// <summary>
	/// Constant select field info.
	/// </summary>
	class ConstantSelectFieldInfo:SelectFieldInfo
	{
		readonly object _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.ConstantSelectFieldInfo"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public ConstantSelectFieldInfo (object value)
		{
			_value = value;
		}

		#region implemented abstract members of SelectFieldInfo
		/// <summary>
		/// Creates the data field sql.
		/// </summary>
		/// <returns>The data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="dataParameter">Data parameter.</param>
		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter)
		{
			if (_value != null) {
				string pn = factory.CreateTempParamName ();
				dataParameter = new DataParameter (pn, _value);
				return pn;
			}
			else {
				dataParameter = null;
				return factory.CreateNullSql ();
			}
		}

		/// <summary>
		/// Gets the table mapping.
		/// </summary>
		/// <value>The table mapping.</value>
		internal override DataEntityMapping TableMapping {
			get {
				return null;
			}
		}

		#endregion
	}
}

