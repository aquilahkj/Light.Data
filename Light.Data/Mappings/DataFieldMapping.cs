using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	abstract class DataFieldMapping : FieldMapping
	{
		public static DataFieldMapping CreateDataFieldMapping (Type type, PropertyInfo property, string fieldName, string indexName, IDataFieldConfig config, DataMapping mapping)
		{
			if (!Regex.IsMatch (fieldName, _fieldRegex, RegexOptions.IgnoreCase)) {
				throw new LightDataException (RE.FieldNameIsInvalid);
			}

			DataFieldMapping fieldMapping = null;

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
				EnumFieldMapping enumFieldMapping = new EnumFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType);
				if (config.DefaultValue != null) {
					string str = config.DefaultValue as String;
					if (str != null) {
						enumFieldMapping.DefaultValue = Enum.Parse (type, str, true);
					}
					else if (config.DefaultValue.GetType () == type) {
						enumFieldMapping.DefaultValue = config.DefaultValue;
					}
				}
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
					PrimitiveFieldMapping primitiveFieldMapping = new PrimitiveFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType);
					primitiveFieldMapping.IsIdentity = config.IsIdentity;
					primitiveFieldMapping.IsPrimaryKey = config.IsPrimaryKey;
					if (config.DefaultValue != null) {
						if (config.DefaultValue.GetType () == type) {
							primitiveFieldMapping.DefaultValue = config.DefaultValue;
						}
						else {
							primitiveFieldMapping.DefaultValue = Convert.ChangeType (config.DefaultValue, type);
						}
					}
					fieldMapping = primitiveFieldMapping;
				}
			}
			if (fieldMapping.IsNullable) {
				PropertyInfo specifiedProperty = GetSpecifiedProperty (property.Name + "Specified", mapping.ObjectType);
				if (specifiedProperty != null) {
					fieldMapping._specifiedHandler = new PropertyHandler (specifiedProperty);
				}
			}
			if (config.DataOrder > 0) {
				fieldMapping.DataOrder = config.DataOrder - 1;
			}

			return fieldMapping;
		}

		public static DataFieldMapping CreateAggregateFieldMapping (Type type, PropertyInfo property, string fieldName, string indexName, IAggregateFieldConfig config, DataMapping mapping)
		{
			if (!Regex.IsMatch (fieldName, _fieldRegex, RegexOptions.IgnoreCase)) {
				throw new LightDataException (RE.FieldNameIsInvalid);
			}

			DataFieldMapping fieldMapping = null;
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
				EnumFieldMapping enumFieldMapping = new EnumFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType);
				if (config.DefaultValue != null) {
					string str = config.DefaultValue as String;
					if (str != null) {
						enumFieldMapping.DefaultValue = Enum.Parse (type, str, true);
					}
					else if (config.DefaultValue.GetType () == type) {
						enumFieldMapping.DefaultValue = config.DefaultValue;
					}
				}
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
					PrimitiveFieldMapping primitiveFieldMapping = new PrimitiveFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType);
					if (config.DefaultValue != null) {
						if (config.DefaultValue.GetType () == type) {
							primitiveFieldMapping.DefaultValue = config.DefaultValue;
						}
						else {
							primitiveFieldMapping.DefaultValue = Convert.ChangeType (config.DefaultValue, type);
						}
					}
					fieldMapping = primitiveFieldMapping;
				}
			}
			if (fieldMapping.IsNullable) {
				PropertyInfo specifiedProperty = GetSpecifiedProperty (property.Name + "Specified", mapping.ObjectType);
				if (specifiedProperty != null) {
					fieldMapping._specifiedHandler = new PropertyHandler (specifiedProperty);
				}
			}
			return fieldMapping;
		}

		public static PropertyInfo GetSpecifiedProperty (string specifiedPropertyName, Type mainType)
		{
			PropertyInfo[] propertys = mainType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in propertys) {
				if (pi.Name == specifiedPropertyName && pi.PropertyType == typeof(Boolean)) {
					IDataFieldConfig dfconfig = ConfigManager.LoadDataFieldConfig (pi);
					if (dfconfig != null) {
						continue;
					}

					IAggregateFieldConfig afconfig = ConfigManager.LoadAggregateFieldConfig (pi);
					if (afconfig != null) {
						continue;
					}
					return pi;
				}
			}
			return null;
		}

		PropertyHandler _specifiedHandler;

		protected DataFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{

		}

		internal PropertyHandler SpecifiedHandler {
			get {
				return _specifiedHandler;
			}
		}

		public DataEntityMapping EntityMapping {
			get {
				return TypeMapping as DataEntityMapping;
			}
		}
	}
}
