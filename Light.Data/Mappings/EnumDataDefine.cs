using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Light.Data
{
	class EnumDataDefine : DataDefine
	{
		//		public static EnumDataDefine Create (Type type, EnumFieldType fieldType, string fieldName)
		//		{
		//			EnumDataDefine mapping = CreateType (type, fieldType);
		//			mapping.FieldName = fieldName;
		//			return mapping;
		//		}
		//
		//		public static EnumDataDefine Create (Type type, EnumFieldType fieldType, int fieldOrder)
		//		{
		//			EnumDataDefine mapping = CreateType (type, fieldType);
		//			mapping.FieldOrder = fieldOrder;
		//			return mapping;
		//		}

		//		private static EnumDataDefine CreateType (Type type, EnumFieldType fieldType)
		//		{
		//			Type rawType = null;
		//			bool isNullable = false;
		//			if (type.IsGenericType) {
		//				Type frameType = type.GetGenericTypeDefinition ();
		//				if (frameType.FullName == "System.Nullable`1") {
		//					Type[] arguments = type.GetGenericArguments ();
		//					rawType = arguments [0];
		//					isNullable = true;
		//				}
		//			}
		//			if (rawType == null) {
		//				rawType = type;
		//			}
		//			if (!rawType.IsEnum) {
		//				throw new LightDataException (RE.SingleFieldSelectTypeError);
		//			}
		//			EnumDataDefine mapping = null;
		//			mapping = new EnumDataDefine (type, rawType, fieldType);
		//			mapping.IsNullable = isNullable;
		//			return mapping;
		//		}

		static Dictionary<Type, EnumDataDefine> DefineListNumerics = new Dictionary<Type, EnumDataDefine> ();

		static Dictionary<Type, EnumDataDefine> DefineListString = new Dictionary<Type, EnumDataDefine> ();


		//		public static EnumDataDefine GetDefine (Type type, EnumFieldType enumType)
		//		{
		//			EnumDataDefine define;
		//			if (enumType == EnumFieldType.EnumToInt) {
		//
		//			}
		//			else {
		//
		//			}
		//
		//			DefineList.TryGetValue (type, out define);
		//			return define;
		//		}

		//		public static bool TryParseDefine (Type type, EnumFieldMapping mapping, out EnumDataDefine define)
		//		{
		//			if (type == mapping.ObjectType) {
		//				define = GetDefine (type, mapping.ObjectType, false, mapping.EnumType);
		//				return true;
		//			}
		//			else if (type == mapping.NullableType) {
		//				define = GetDefine (type, mapping.ObjectType, true, mapping.EnumType);
		//				return true;
		//			}
		//			else {
		//				define = null;
		//				return false;
		//			}
		//		}

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

		//		readonly Type _rawType = null;

		readonly object _defaultValue = null;

		//		Dictionary<int, object> _dict = new Dictionary<int, object> ();

		private EnumDataDefine (Type type, bool isNullable, EnumFieldType enumType)
			: base (type, isNullable)
		{
			_enumType = enumType;
//			_rawType = rawType;
			Array values = Enum.GetValues (type);
			_defaultValue = values.GetValue (0);
		}

		//		private EnumDataDefine (Type type, Type rawType, EnumFieldType enumType)
		//			: base (type)
		//		{
		//			_rawType = rawType;
		//			_enumType = enumType;
		//			Array values = Enum.GetValues (_rawType);
		//			_defaultValue = values.GetValue (0);
		//			for (int i = 0; i < values.Length; i++) {
		//				object obj = values.GetValue (i);
		//				_dict.Add (obj.GetHashCode (), obj);
		//			}
		//		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object obj = datareader [0];
//			if (!string.IsNullOrEmpty (FieldName)) {
//				obj = datareader [FieldName];
//			}
//			else {
//				obj = datareader [FieldOrder];
//			}
			return GetValue (obj);
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object obj = datarow [0];
//			if (!string.IsNullOrEmpty (FieldName)) {
//				obj = datarow [FieldName];
//			}
//			else {
//				obj = datarow [FieldOrder];
//			}
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
//					int result = Convert.ToInt32 (obj);
//					if (_dict.ContainsKey (result)) {
//						return _dict [result];
//					}
//					else {
//						throw new LightDataException (string.Format (RE.ValueNotInEnumType, result, _rawType));
//					}
					return value;
				}
			}
		}
	}
}
