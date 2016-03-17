
namespace Light.Data
{
	/// <summary>
	/// Interface of aggregate field config.
	/// </summary>
	interface IAggregateFieldConfig
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
		/// Gets or sets a value indicating whether this instance is nullable.
		/// </summary>
		/// <value><c>true</c> if this instance is nullable; otherwise, <c>false</c>.</value>
		bool IsNullable
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>The default value.</value>
		object DefaultValue
		{
			get;
			set;
		}
	}
}
