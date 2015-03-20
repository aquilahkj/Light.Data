using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Light.Data.Config;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	abstract class DataFieldMapping : FieldMapping
	{
		public static DataFieldMapping CreateDataFieldMapping (Type type, PropertyInfo property, string fieldName, string indexName, IDataFieldConfig config, DataEntityMapping mapping, Type mainType)
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
//				enumFieldMapping.IsNullable = config.IsNullable;
				if (config.DefaultValue != null) {
					if (config.DefaultValue is String) {
						enumFieldMapping.DefaultValue = Enum.Parse (type, config.DefaultValue as String, true);
					}
					else {
						Array arr = Enum.GetValues (type);
						foreach (object obj in arr) {
							if (obj.Equals (config.DefaultValue)) {
								enumFieldMapping.DefaultValue = config.DefaultValue;
								break;
							}
						}
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
				else if (code == TypeCode.Object && type.FullName != "System.Byte[]") {
					ComplexFieldMapping complexFieldMapping = new ComplexFieldMapping (type, fieldName, indexName, mapping, isNullable);
//					complexFieldMapping.IsNullable = config.IsNullable;
					fieldMapping = complexFieldMapping;
				}
				else {
					PrimitiveFieldMapping primitiveFieldMapping = new PrimitiveFieldMapping (type, fieldName, indexName, mapping, isNullable, dbType);
					primitiveFieldMapping.IsIdentity = config.IsIdentity;
					primitiveFieldMapping.IsPrimaryKey = config.IsPrimaryKey;
//					primitiveFieldMapping.IsNullable = config.IsNullable;
//					primitiveFieldMapping.DBType = config.DBType;
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
//			if (isNullAbleType) {
//				fieldMapping.IsNullable = true;
//			}
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

		public DataFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
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
