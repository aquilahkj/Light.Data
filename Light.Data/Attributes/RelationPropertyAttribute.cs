using System;

namespace Light.Data
{
	/// <summary>
	/// Relation property attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true)]
	public class RelationPropertyAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the relation mode.
		/// </summary>
		/// <value>The relation mode.</value>
		//public RelationMode RelationMode {
		//	get;
		//	set;
		//}
	}
}
