using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate field attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true)]
	public class AggregateFieldAttribute : Attribute, IAggregateFieldConfig
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AggregateFieldAttribute"/> class.
		/// </summary>
		public AggregateFieldAttribute ()
		{
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AggregateFieldAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		public AggregateFieldAttribute (string name)
		{
			this.name = name;
		}
		string name;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return name;
			}

			set {
				name = value;
			}
		}
		bool isNullable;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is nullable.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsNullable {
			get {
				return isNullable;
			}

			set {
				isNullable = value;
			}
		}
		object defaultValue;

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>The default value.</value>
		public object DefaultValue {
			get {
				return defaultValue;
			}

			set {
				defaultValue = value;
			}
		}
	}
}
