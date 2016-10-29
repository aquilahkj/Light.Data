
namespace Light.Data
{
	/// <summary>
	/// Constant select field info.
	/// </summary>
	class ConstantSelectFieldInfo : SelectFieldInfo
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

		internal override string CreateSqlString (CommandFactory factory, CreateSqlState state)
		{
			if (_value != null) {
				string pn = state.AddDataParameter (_value);
				return pn;
			}
			else {
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

