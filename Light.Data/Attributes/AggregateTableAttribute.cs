using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate table attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, Inherited = true)]
	public class AggregateTableAttribute : Attribute, IAggregateTableConfig
	{
		string extendParams;

		/// <summary>
		/// Gets or sets the extend parameters.
		/// </summary>
		/// <value>The extend parameters.</value>
		public string ExtendParams {
			get {
				return extendParams;
			}

			set {
				extendParams = value;
			}
		}
	}
}
