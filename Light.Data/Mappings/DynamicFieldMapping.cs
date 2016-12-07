using System;
using System.Reflection;

namespace Light.Data
{
	abstract class DynamicFieldMapping : FieldMapping
	{
		public static DynamicFieldMapping CreateAggregateFieldMapping (PropertyInfo property, DynamicCustomMapping mapping)
		{
			DynamicFieldMapping fieldMapping;
			Type type = property.PropertyType;
			string fieldName = property.Name;
			if (type.IsGenericType) {
				Type frameType = type.GetGenericTypeDefinition ();
				if (frameType.FullName == "System.Nullable`1") {
					Type [] arguments = type.GetGenericArguments ();
					type = arguments [0];
				}
			}

			if (type.IsArray) {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsGenericParameter | type.IsGenericTypeDefinition) {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsEnum) {
				DynamicEnumFieldMapping enumFieldMapping = new DynamicEnumFieldMapping (type, fieldName, mapping);
				fieldMapping = enumFieldMapping;
			}
			else {
				TypeCode code = Type.GetTypeCode (type);
				switch (code) {
				case TypeCode.DBNull:
				case TypeCode.Empty:
				case TypeCode.Object:
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				default:
					DynamicPrimitiveFieldMapping primitiveFieldMapping = new DynamicPrimitiveFieldMapping (type, fieldName, mapping);
					fieldMapping = primitiveFieldMapping;
					break;
				}
			}
			return fieldMapping;
		}

		protected DynamicFieldMapping (Type type, string fieldName, DynamicCustomMapping mapping, bool isNullable)
			: base (type, fieldName, fieldName, mapping, isNullable, null)
		{

		}
	}
}

