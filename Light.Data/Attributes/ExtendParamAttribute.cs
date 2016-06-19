using System;

namespace Light.Data
{
	/// <summary>
	/// Extend parameter attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public abstract class ExtendParamAttribute: Attribute
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value {
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.ExtendParamAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		protected ExtendParamAttribute (string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}

	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class AggregateExtendParamAttribute: ExtendParamAttribute
	{
		public AggregateExtendParamAttribute (string name, string value) : base (name, value)
		{
		
		}
	}

	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class DataTableExtendParamAttribute: ExtendParamAttribute
	{
		public DataTableExtendParamAttribute (string name, string value) : base (name, value)
		{
		}
	}
}

