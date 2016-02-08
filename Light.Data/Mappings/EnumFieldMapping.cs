using System;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class EnumFieldMapping : DataFieldMapping
	{
		EnumFieldType _enumType = EnumFieldType.EnumToNumerics;

		readonly object _minValue = null;

		readonly object _defaultValue = null;

		public EnumFieldType EnumType {
			get {
				return _enumType;
			}
		}

		Type _nullableType;

		public Type NullableType {
			get {
				return _nullableType;
			}
		}

		Regex textRegex = new Regex ("char|text|string", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public EnumFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType, object defaultValue)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
			if (dbType != null && textRegex.IsMatch (dbType)) {
				_enumType = EnumFieldType.EnumToString;
			}
			else {
				_enumType = EnumFieldType.EnumToNumerics;
			}
			Type itemstype = Type.GetType ("System.Nullable`1");
			_nullableType = itemstype.MakeGenericType (type);
			Array values = Enum.GetValues (ObjectType);
			object value = values.GetValue (0);

			if (_enumType == EnumFieldType.EnumToString) {
				_minValue = value.ToString ();
			}
			else {
				_minValue = value;
			}
			if (defaultValue != null) {
				string str = defaultValue as String;
				if (str != null) {
					_defaultValue = Enum.Parse (type, str, true);
				}
				else if (defaultValue.GetType () == type) {
					_defaultValue = defaultValue;
				}
			}
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				if (_defaultValue != null) {
					return _defaultValue;
				}
				else {
					return null;
				}
			}
			else {
				if (EnumType == EnumFieldType.EnumToString) {
					return Enum.Parse (ObjectType, value.ToString ());
				}
				else {
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
					return _minValue;
				}
			}
			else {
				if (_enumType == EnumFieldType.EnumToString) {
					return value.ToString ();
				}
				else {
					return value;
				}
			}
		}
	}
}
