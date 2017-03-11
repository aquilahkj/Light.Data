using System;
namespace Light.Data
{
	class DynamicPrimitiveFieldMapping : DynamicFieldMapping
	{
		public DynamicPrimitiveFieldMapping (Type type, string fieldName, DynamicCustomMapping mapping)
			: base (type, fieldName, mapping, true)
		{
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				return null;
			}
			else {
				IConvertible ic = value as IConvertible;
				if (ic != null) {
					if (ic.GetTypeCode () != _typeCode) {
						return Convert.ChangeType (value, _typeCode);
					}
					else {
						return value;
					}
				}
				else {
					return value;
				}


				//if (ObjectType != null && value.GetType () != ObjectType) {
				//	return Convert.ChangeType (value, ObjectType);
				//}
				//else {
				//	return value;
				//}
			}
		}
	}
}

