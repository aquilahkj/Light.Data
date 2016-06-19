
namespace Light.Data
{
	/// <summary>
	/// Interface of data table config.
	/// </summary>
	interface IDataTableConfig
	{
		/// <summary>
		/// Gets or sets the name of the table.
		/// </summary>
		/// <value>The name of the table.</value>
		string TableName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the extend parameters.
		/// </summary>
		/// <value>The extend parameters.</value>
//		string ExtendParams {
//			get;
//			set;
//		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is entity table.
		/// </summary>
		/// <value><c>true</c> if this instance is entity table; otherwise, <c>false</c>.</value>
		bool IsEntityTable {
			get;
			set;
		}
	}
}
