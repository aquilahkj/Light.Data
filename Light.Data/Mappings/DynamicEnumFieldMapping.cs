using System;
namespace Light.Data
{
	class DynamicEnumFieldMapping : DynamicFieldMapping
	{
		EnumFieldType _enumType;

		public DynamicEnumFieldMapping (Type type, string fieldName, DynamicAggregateMapping mapping, EnumFieldType enumType)
			: base (type, fieldName, mapping, true)
		{
			_enumType = enumType;
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				return null;
			}
			else {
				if (_enumType == EnumFieldType.EnumToString) {
					return Enum.Parse (ObjectType, value.ToString ());
				}
				else {
					Type type = value.GetType ();
					TypeCode code = Type.GetTypeCode (type);
					if (code != this._typeCode) {
						value = Convert.ChangeType (value, this._typeCode);
					}
					return value;
				}
			}
		}
	}
}

