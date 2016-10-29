using System;

namespace Light.Data
{
	/// <summary>
	/// Extend parameter attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public abstract class ExtendParamAttribute: Attribute
	{
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
		string value;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value {
			get {
				return this.value;
			}

			set {
				this.value = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.ExtendParamAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		protected ExtendParamAttribute (string name, string value)
		{
			this.name = name;
			this.value = value;
		}
	}

	/// <summary>
	/// Aggregate extend parameter attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class AggregateExtendParamAttribute: ExtendParamAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Light.Data.AggregateExtendParamAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public AggregateExtendParamAttribute (string name, string value) : base (name, value)
		{
		
		}
	}

	/// <summary>
	/// Data table extend parameter attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class DataTableExtendParamAttribute: ExtendParamAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Light.Data.DataTableExtendParamAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public DataTableExtendParamAttribute (string name, string value) : base (name, value)
		{
		}
	}
}

