using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Light.Data.Mappings
{
	class EnumDataDefine : DataDefine
	{
		public static EnumDataDefine Create (Type type, EnumFieldType fieldType, string fieldName)
		{
			EnumDataDefine mapping = CreateType (type, fieldType);
			mapping.FieldName = fieldName;
			return mapping;
		}

		public static EnumDataDefine Create (Type type, EnumFieldType fieldType, int fieldOrder)
		{
			EnumDataDefine mapping = CreateType (type, fieldType);
			mapping.FieldOrder = fieldOrder;
			return mapping;
		}

		private static EnumDataDefine CreateType (Type type, EnumFieldType fieldType)
		{
			Type rawType = null;
			bool isNullable = false;
			if (type.IsGenericType) {
				Type frameType = type.GetGenericTypeDefinition ();
				if (frameType.FullName == "System.Nullable`1") {
					Type[] arguments = type.GetGenericArguments ();
					rawType = arguments [0];
					isNullable = true;
				}
			}
			if (rawType == null) {
				rawType = type;
			}
			if (!rawType.IsEnum) {
				throw new LightDataException (RE.SingleFieldSelectTypeError);
			}
			EnumDataDefine mapping = null;
			mapping = new EnumDataDefine (type, rawType, fieldType);
			mapping.IsNullable = isNullable;
			return mapping;
		}

		EnumFieldType _enumType = EnumFieldType.EnumToInt;

		Type _rawType = null;

		object _defaultValue = null;

		Dictionary<int, object> _dict = new Dictionary<int, object> ();

		private EnumDataDefine (Type type, Type rawType, EnumFieldType enumType)
			: base (type)
		{
			_rawType = rawType;
			_enumType = enumType;
			Array values = Enum.GetValues (_rawType);
			_defaultValue = values.GetValue (0);
			for (int i = 0; i < values.Length; i++) {
				object obj = values.GetValue (i);
				_dict.Add (obj.GetHashCode (), obj);
			}
		}

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			object obj;
			if (!string.IsNullOrEmpty (FieldName)) {
				obj = datareader [FieldName];
			}
			else {
				obj = datareader [FieldOrder];
			}
			return GetValue (obj);
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			object obj;
			if (!string.IsNullOrEmpty (FieldName)) {
				obj = datarow [FieldName];
			}
			else {
				obj = datarow [FieldOrder];
			}
			return GetValue (obj);
		}

		object GetValue (object obj)
		{
			if (Object.Equals (obj, DBNull.Value)) {
				if (!IsNullable) {
					return _defaultValue;
				}
				else {
					return null;
				}
			}
			else {
				if (_enumType == EnumFieldType.EnumToString) {
					return Enum.Parse (_rawType, obj.ToString ());
				}
				else {
					int result = Convert.ToInt32 (obj);
					if (_dict.ContainsKey (result)) {
						return _dict [result];
					}
					else {
						throw new LightDataException (string.Format (RE.ValueNotInEnumType, result, _rawType));
					}
				}
			}
		}
	}
}
