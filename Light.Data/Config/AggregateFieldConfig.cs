using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate field config.
	/// </summary>
	class AggregateFieldConfig : IAggregateFieldConfig, IConfiguratorFieldConfig
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AggregateFieldConfig"/> class.
		/// </summary>
		/// <param name="fieldName">Field name.</param>
		public AggregateFieldConfig (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException (nameof (fieldName));
			}
			FieldName = fieldName;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		/// <value>The name of the field.</value>
		public string FieldName {
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is nullable.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsNullable {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>The default value.</value>
		public object DefaultValue {
			get;
			set;
		}
	}
}
