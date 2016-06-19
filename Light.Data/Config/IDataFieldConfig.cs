
namespace Light.Data
{
	/// <summary>
	/// Interface of data field config.
	/// </summary>
	interface IDataFieldConfig
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the data order.
		/// </summary>
		/// <value>The data order.</value>
		int DataOrder {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is nullable.
		/// </summary>
		/// <value><c>true</c> if this instance is nullable; otherwise, <c>false</c>.</value>
		bool IsNullable {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is primary key.
		/// </summary>
		/// <value><c>true</c> if this instance is primary key; otherwise, <c>false</c>.</value>
		bool IsPrimaryKey {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is identity.
		/// </summary>
		/// <value><c>true</c> if this instance is identity; otherwise, <c>false</c>.</value>
		bool IsIdentity {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the DB.
		/// </summary>
		/// <value>The type of the DB.</value>
		string DBType {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>The default value.</value>
		object DefaultValue {
			get;
			set;
		}
	}
}
