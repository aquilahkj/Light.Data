using System;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class EnumFieldMapping : DataFieldMapping
	{
		//EnumFieldType _enumType = EnumFieldType.EnumToNumerics;

		readonly object _minValue = null;

		readonly object _defaultValue = null;

		//public EnumFieldType EnumType {
		//	get {
		//		return _enumType;
		//	}
		//}

		Type _nullableType;

		public Type NullableType {
			get {
				return _nullableType;
			}
		}

		bool auto = false;

		Regex textRegex = new Regex ("char|text|string", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public EnumFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType, object defaultValue)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
			//if (dbType != null && textRegex.IsMatch (dbType)) {
			//	_enumType = EnumFieldType.EnumToString;
			//}
			//else {
			//	_enumType = EnumFieldType.EnumToNumerics;
			//}
			Type itemstype = Type.GetType ("System.Nullable`1");
			_nullableType = itemstype.MakeGenericType (type);
			Array values = Enum.GetValues (ObjectType);
			//_typeCode = Type.GetTypeCode (ObjectType);
			object value = values.GetValue (0);

			//if (_enumType == EnumFieldType.EnumToString) {
			//	_minValue = value.ToString ();
			//}
			//else {
				_minValue = Convert.ChangeType (value, _typeCode);
			//}
			if (defaultValue != null) {
				string str = defaultValue as String;
				if (str != null) {
					object dvalue = Enum.Parse (type, str, true);
					//if (_enumType == EnumFieldType.EnumToString) {
					//	_defaultValue = dvalue.ToString ();
					//}
					//else {
						_defaultValue = Convert.ChangeType (dvalue, _typeCode);
					//}
				}
				else if (defaultValue.GetType () == type) {
					//if (_enumType == EnumFieldType.EnumToString) {
					//	_defaultValue = defaultValue.ToString ();
					//}
					//else {
						_defaultValue = Convert.ChangeType (defaultValue, _typeCode);
					//}
				}
			}
		}

		public EnumFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable)
			: base (type, fieldName, indexName, mapping, isNullable, null)
		{
			Type itemstype = Type.GetType ("System.Nullable`1");
			_nullableType = itemstype.MakeGenericType (type);
			Array values = Enum.GetValues (ObjectType);
			object value = values.GetValue (0);
			auto = true;
			//if (_enumType == EnumFieldType.EnumToString) {
			//	_minValue = value.ToString ();
			//}
			//else {
				_minValue = Convert.ChangeType (value, _typeCode);
			//}

		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				return null;
			}
			else {
				if (auto) {
					string str = value as string;
					if (str != null) {
						return Enum.Parse (ObjectType, str);
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
				else {
					//if (_enumType == EnumFieldType.EnumToString) {
					//	return Enum.Parse (ObjectType, value.ToString ());
					//}
					//else {
						Type type = value.GetType ();
						TypeCode code = Type.GetTypeCode (type);
						if (code != this._typeCode) {
							value = Convert.ChangeType (value, this._typeCode);
						}
						return value;
					//}
				}
			}
		}

		public override object ToParameter (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				return null;
			}
			else {
				//if (_enumType == EnumFieldType.EnumToString) {
				//	return value.ToString ();
				//}
				//else {
					return Convert.ChangeType (value, _typeCode);
				//}
			}
		}

		#region implemented abstract members of DataFieldMapping

		public override object ToColumn (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				if (IsNullable) {
					return null;
				}
				else {
					if (_defaultValue != null) {
						return _defaultValue;
					}
					else {
						return _minValue;
					}
				}
			}
			else {
				//if (_enumType == EnumFieldType.EnumToString) {
				//	return value.ToString ();
				//}
				//else {
					return Convert.ChangeType (value, _typeCode);
				//}
			}
		}

		#endregion
	}
}
