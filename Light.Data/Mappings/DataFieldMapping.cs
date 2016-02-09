using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Light.Data
{
	abstract class DataFieldMapping : FieldMapping
	{
		public static DataFieldMapping CreateDataFieldMapping (PropertyInfo property, IDataFieldConfig config, int positionOeder, DataMapping mapping)
		{
			Type type = property.PropertyType;
			string indexName = property.Name;
			string fieldName = string.IsNullOrEmpty (config.Name) ? property.Name : config.Name;
			if (!Regex.IsMatch (fieldName, _fieldRegex, RegexOptions.IgnoreCase)) {
				throw new LightDataException (RE.FieldNameIsInvalid);
			}

			DataFieldMapping fieldMapping;

			bool isNullable = false;
			string dbType = config.DBType;
			if (type.IsGenericType) {
				Type frameType = type.GetGenericTypeDefinition ();
				if (frameType.FullName == "System.Nullable`1") {
					Type[] arguments = type.GetGenericArguments ();
					type = arguments [0];
					isNullable = true;
				}
			}
			isNullable = isNullable || config.IsNullable;
			if (type.IsArray && type.FullName != "System.Byte[]") {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsGenericParameter | type.IsGenericTypeDefinition) {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsEnum) {
				EnumFieldMapping enumFieldMapping = new EnumFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType, config.DefaultValue);
				fieldMapping = enumFieldMapping;
			}
			else {
				TypeCode code = Type.GetTypeCode (type);
				if (code == TypeCode.DBNull) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				if (code == TypeCode.Empty) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				else {
					PrimitiveFieldMapping primitiveFieldMapping = new PrimitiveFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType, config.DefaultValue, config.IsIdentity, config.IsPrimaryKey);
					fieldMapping = primitiveFieldMapping;
				}
			}
			if (config.DataOrder > 0) {
				fieldMapping._dataOrder = config.DataOrder - 1;
			}
			fieldMapping._positionOrder = positionOeder;
			fieldMapping._handler = new PropertyHandler (property);
			return fieldMapping;
		}

		public static DataFieldMapping CreateAggregateFieldMapping (PropertyInfo property, IAggregateFieldConfig config, DataMapping mapping)
		{
			Type type = property.PropertyType;
			string indexName = property.Name;
			string fieldName = string.IsNullOrEmpty (config.Name) ? property.Name : config.Name;
			if (!Regex.IsMatch (fieldName, _fieldRegex, RegexOptions.IgnoreCase)) {
				throw new LightDataException (RE.FieldNameIsInvalid);
			}

			DataFieldMapping fieldMapping;
			string dbType = null;
			bool isNullable = false;
			if (type.IsGenericType) {
				Type frameType = type.GetGenericTypeDefinition ();
				if (frameType.FullName == "System.Nullable`1") {
					Type[] arguments = type.GetGenericArguments ();
					type = arguments [0];
					isNullable = true;
				}
			}

			if (type.IsArray && type.FullName != "System.Byte[]") {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsGenericParameter | type.IsGenericTypeDefinition) {
				throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
			}
			else if (type.IsEnum) {
				EnumFieldMapping enumFieldMapping = new EnumFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType, config.DefaultValue);
				fieldMapping = enumFieldMapping;
			}
			else {
				TypeCode code = Type.GetTypeCode (type);
				if (code == TypeCode.DBNull) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
				if (code == TypeCode.Empty) {
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				}
//				else if (code == TypeCode.Object && type.FullName != "System.Byte[]") {
//					ComplexFieldMapping complexFieldMapping = new ComplexFieldMapping (type, fieldName, indexName, mapping, isNullable);
//					fieldMapping = complexFieldMapping;
//				}
				else {
					PrimitiveFieldMapping primitiveFieldMapping = new PrimitiveFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType, config.DefaultValue, false, false);
					fieldMapping = primitiveFieldMapping;
				}
			}
			fieldMapping._handler = new PropertyHandler (property);
			return fieldMapping;
		}

		protected int? _dataOrder;

		protected int _positionOrder;

		protected int _fieldOreder;

		public int? DataOrder {
			get {
				return _dataOrder;
			}
		}

		public int PositionOrder {
			get {
				return _positionOrder;
			}
		}

		public int FieldOreder {
			get {
				return _fieldOreder;
			}
		}

		public PropertyHandler Handler {
			get {
				return _handler;
			}
		}

		protected DataFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{

		}

		public DataEntityMapping EntityMapping {
			get {
				return TypeMapping as DataEntityMapping;
			}
		}

		public abstract object ToColumn (object value);
	}
}
