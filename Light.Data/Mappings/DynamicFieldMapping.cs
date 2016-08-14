using System;
using System.Reflection;

namespace Light.Data
{
	abstract class DynamicFieldMapping : FieldMapping
	{
		public static DynamicFieldMapping CreateAggregateFieldMapping (PropertyInfo property, DynamicAggregateMapping mapping)
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
				DynamicEnumFieldMapping enumFieldMapping = new DynamicEnumFieldMapping (type, fieldName, mapping, EnumFieldType.EnumToNumerics);
				fieldMapping = enumFieldMapping;
			}
			else {
				TypeCode code = Type.GetTypeCode (type);
				if (code == TypeCode.DBNull) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				else if (code == TypeCode.Empty) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				else if (code == TypeCode.Object) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				else {
					DynamicPrimitiveFieldMapping primitiveFieldMapping = new DynamicPrimitiveFieldMapping (type, fieldName, mapping);
					fieldMapping = primitiveFieldMapping;
				}
			}
			return fieldMapping;
		}

		public DynamicFieldMapping (Type type, string fieldName, DynamicAggregateMapping mapping, bool isNullable)
			: base (type, fieldName, fieldName, mapping, isNullable, null)
		{

		}


	}
}

