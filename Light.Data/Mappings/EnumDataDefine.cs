using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	class EnumDataDefine : DataDefine
	{
		//static Dictionary<Type, EnumDataDefine> DefineListNumerics = new Dictionary<Type, EnumDataDefine> ();

		TypeCode _typeCode;

		//public static EnumDataDefine ParseDefine (Type type, EnumFieldMapping mapping)
		//{
		//	if (type == mapping.ObjectType) {
		//		//EnumDataDefine define = GetDefine (mapping.ObjectType, false, mapping.EnumType);
		//		EnumDataDefine define = GetDefine (mapping.ObjectType, false);
		//		return define;
		//	}
		//	else if (type == mapping.NullableType) {
		//		//EnumDataDefine define = GetDefine (mapping.ObjectType, true, mapping.EnumType);
		//		EnumDataDefine define = GetDefine (mapping.ObjectType, true);
		//		return define;
		//	}
		//	else {
		//		throw new LightDataException (RE.UnmatchDataDefineType);
		//	}
		//}

		//static EnumDataDefine GetDefine (Type type, bool isNullable)
		//{
		//	EnumDataDefine define;
		//	if (!DefineListNumerics.TryGetValue (type, out define)) {
		//		lock (DefineListNumerics) {
		//			if (!DefineListNumerics.TryGetValue (type, out define)) {
		//				define = new EnumDataDefine (type, isNullable);
		//				DefineListNumerics [type] = define;
		//			}
		//		}
		//	}
		//	return define;
		//}

		//EnumFieldType _enumType;

		readonly object _defaultValue = null;

		public EnumDataDefine (Type type, bool isNullable)
			: base (type, isNullable)
		{
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
