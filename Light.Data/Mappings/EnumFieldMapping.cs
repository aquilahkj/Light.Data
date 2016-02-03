using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class EnumFieldMapping : DataFieldMapping
	{
		EnumFieldType _enumType = EnumFieldType.EnumToNumerics;

		readonly object _defaultValue = null;

		public EnumFieldType EnumType {
			get {
				return _enumType;
			}
			private set {
				_enumType = value;
			}
		}

		Type nullableType;

		public Type NullableType {
			get {
				return nullableType;
			}
		}

		Regex textRegex = new Regex ("char|text|string", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public EnumFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
			if (dbType != null && textRegex.IsMatch (dbType)) {
				EnumType = EnumFieldType.EnumToString;
			}
			else {
				EnumType = EnumFieldType.EnumToNumerics;
			}
			Type itemstype = System.Type.GetType ("System.Nullable`1");
			nullableType = itemstype.MakeGenericType (type);
			Array values = Enum.GetValues (ObjectType);
			_defaultValue = values.GetValue (0);
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				if (IsNullable) {
					return null;
				}
				else {
					return _defaultValue;
				}
			}
			else {
				if (EnumType == EnumFieldType.EnumToString) {
					return Enum.Parse (ObjectType, value.ToString ());
				}
				else {
//					int result = Convert.ToInt32 (value);//value.GetHashCode();
//					if (_dict.ContainsKey (result)) {
//						return _dict [result];
//					}
//					else {
//						throw new LightDataException (string.Format (RE.ValueNotInEnumType, result, ObjectType));
//					}
					return value;
				}
			}
		}

		public override object ToColumn (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				if (IsNullable) {
					return null;
				}
				else {
					value = _defaultValue;
				}
			}
			if (EnumType == EnumFieldType.EnumToString) {
				return value.ToString ();
			}
			else {
				return value;
			}
		}
	}
}
