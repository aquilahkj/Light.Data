using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	class EnumDataDefine : DataDefine
	{
		static Dictionary<Type, EnumDataDefine> DefineListNumerics = new Dictionary<Type, EnumDataDefine> ();

		static Dictionary<Type, EnumDataDefine> DefineListString = new Dictionary<Type, EnumDataDefine> ();

		TypeCode _typeCode;

		public static EnumDataDefine ParseDefine (Type type, EnumFieldMapping mapping)
		{
			if (type == mapping.ObjectType) {
				EnumDataDefine define = GetDefine (mapping.ObjectType, false, mapping.EnumType);
				return define;
			}
			else if (type == mapping.NullableType) {
				EnumDataDefine define = GetDefine (mapping.ObjectType, true, mapping.EnumType);
				return define;
			}
			else {
				throw new LightDataException (RE.UnmatchDataDefineType);
			}
		}

		static EnumDataDefine GetDefine (Type type, bool isNullable, EnumFieldType enumType)
		{
			EnumDataDefine define;
			Dictionary<Type, EnumDataDefine> dict;
			dict = enumType == EnumFieldType.EnumToNumerics ? DefineListNumerics : DefineListString;
			if (!dict.TryGetValue (type, out define)) {
				lock (dict) {
					if (!dict.TryGetValue (type, out define)) {
						define = new EnumDataDefine (type, isNullable, enumType);
						dict [type] = define;
					}
				}
			}

			return define;	
		}

		EnumFieldType _enumType;

		readonly object _defaultValue = null;

		private EnumDataDefine (Type type, bool isNullable, EnumFieldType enumType)
			: base (type, isNullable)
		{
			_enumType = enumType;
			Array values = Enum.GetValues (type);
			_defaultValue = values.GetValue (0);
			_typeCode = Type.GetTypeCode (ObjectType);
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
		{
			object obj = datareader [0];
			return GetValue (obj);
		}

		object GetValue (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				if (!IsNullable) {
					return _defaultValue;
				}
				else {
					return null;
				}
			}
			else {
				if (_enumType == EnumFieldType.EnumToString) {
					return Enum.Parse (ObjectType, value.ToString ());
				}
				else {
//					return value;

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
