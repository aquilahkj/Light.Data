using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
	class EnumFieldMapping : DataFieldMapping
	{
		EnumFieldType _enumType = EnumFieldType.EnumToInt;

		object _defaultValue = null;

		Dictionary<int, object> _dict = new Dictionary<int, object> ();

		public EnumFieldType EnumType {
			get {
				return _enumType;
			}
			private set {
				_enumType = value;
			}
		}

		public EnumFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
//			ObjectType = type;
//			Name = fieldName;
//			IndexName = indexName;
//			TypeMapping = mapping;
			if (string.IsNullOrEmpty (dbType) || dbType.IndexOf ("char", 0, StringComparison.OrdinalIgnoreCase) == -1) {
				EnumType = EnumFieldType.EnumToInt;
			}
			else {
				EnumType = EnumFieldType.EnumToString;
			}
//			DBType = dbType;

			Array values = Enum.GetValues (ObjectType);
			_defaultValue = values.GetValue (0);

			for (int i = 0; i < values.Length; i++) {
				object obj = values.GetValue (i);
				_dict.Add (Convert.ToInt32 (obj), obj);
			}
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
					int result = Convert.ToInt32 (value);//value.GetHashCode();
					if (_dict.ContainsKey (result)) {
						return _dict [result];
					}
					else {
						throw new LightDataException (string.Format (RE.ValueNotInEnumType, result, ObjectType));
					}
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
				return Convert.ToInt32 (value);
			}
		}


	}


}
