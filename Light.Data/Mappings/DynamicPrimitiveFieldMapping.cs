using System;
namespace Light.Data
{
	class DynamicPrimitiveFieldMapping : DynamicFieldMapping
	{
		public DynamicPrimitiveFieldMapping (Type type, string fieldName, DynamicAggregateMapping mapping)
			: base (type, fieldName, mapping, true)
		{
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				return null;
			}
			else {
				if (ObjectType != null && value.GetType () != ObjectType) {
					return Convert.ChangeType (value, ObjectType);
				}
				else {
					return value;
				}
			}
		}
	}
}

