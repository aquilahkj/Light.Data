using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Light.Data.Mappings
{
	class PrimitiveDataDefine : DataDefine
	{
		const sbyte MIN_SBYTE = 0;

		const byte MIN_BYTE = 0;

		const short MIN_SHORT = 0;

		const ushort MIN_USHORT = 0;

		static Dictionary<Type, bool> DefineList = new Dictionary<Type, bool> ();

		static PrimitiveDataDefine ()
		{
			DefineList.Add (typeof(Boolean), false);
			DefineList.Add (typeof(Char), false);
			DefineList.Add (typeof(SByte), false);
			DefineList.Add (typeof(Byte), false);
			DefineList.Add (typeof(Int16), false);
			DefineList.Add (typeof(UInt16), false);
			DefineList.Add (typeof(Int32), false);
			DefineList.Add (typeof(UInt32), false);
			DefineList.Add (typeof(Int64), false);
			DefineList.Add (typeof(UInt64), false);
			DefineList.Add (typeof(Single), false);
			DefineList.Add (typeof(Double), false);
			DefineList.Add (typeof(Decimal), false);
			DefineList.Add (typeof(DateTime), false);
			//DefineList.Add(typeof(String), true);

			DefineList.Add (typeof(Nullable<Boolean>), true);
			DefineList.Add (typeof(Nullable<Char>), true);
			DefineList.Add (typeof(Nullable<SByte>), true);
			DefineList.Add (typeof(Nullable<Byte>), true);
			DefineList.Add (typeof(Nullable<Int16>), true);
			DefineList.Add (typeof(Nullable<UInt16>), true);
			DefineList.Add (typeof(Nullable<Int32>), true);
			DefineList.Add (typeof(Nullable<UInt32>), true);
			DefineList.Add (typeof(Nullable<Int64>), true);
			DefineList.Add (typeof(Nullable<UInt64>), true);
			DefineList.Add (typeof(Nullable<Single>), true);
			DefineList.Add (typeof(Nullable<Double>), true);
			DefineList.Add (typeof(Nullable<Decimal>), true);
			DefineList.Add (typeof(Nullable<DateTime>), true);
		}

		public static PrimitiveDataDefine Create (Type type, string fieldName)
		{
			PrimitiveDataDefine mapping = CreateType (type);
			mapping.FieldName = fieldName;
			return mapping;
		}

		public static PrimitiveDataDefine Create (Type type, int fieldOrder)
		{
			PrimitiveDataDefine mapping = CreateType (type);
			mapping.FieldOrder = fieldOrder;
			return mapping;
		}

		private static PrimitiveDataDefine CreateType (Type type)
		{
			PrimitiveDataDefine mapping = null;
			if (DefineList.ContainsKey (type)) {
				mapping = new PrimitiveDataDefine (type);
				mapping.IsNullable = DefineList [type];
				return mapping;
			}
			if (type == typeof(string)) {
				mapping = new PrimitiveDataDefine (typeof(string));
			}
			else {
				throw new LightDataException (RE.SingleFieldSelectTypeError);
			}
			return mapping;
		}

		public static PrimitiveDataDefine CreateString (bool isNullable, string fieldName)
		{
			PrimitiveDataDefine mapping = CreateFromStringType (isNullable);
			mapping.FieldName = fieldName;
			return mapping;
		}

		public static PrimitiveDataDefine CreateString (bool isNullable, int fieldOrder)
		{
			PrimitiveDataDefine mapping = CreateFromStringType (isNullable);
			mapping.FieldOrder = fieldOrder;
			return mapping;
		}

		private static PrimitiveDataDefine CreateFromStringType (bool isNullable)
		{
			PrimitiveDataDefine mapping = null;
			mapping = new PrimitiveDataDefine (typeof(string));
			mapping.IsNullable = isNullable;
			return mapping;
		}

		private PrimitiveDataDefine (Type type)
			: base (type)
		{
			_typeCode = Type.GetTypeCode (type);
		}

		TypeCode _typeCode = TypeCode.Empty;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="datareader"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="datarow"></param>
		/// <returns></returns>
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

		object GetDefaultValue ()
		{
			object obj;
			switch (_typeCode) {
				case TypeCode.String:
					obj = string.Empty;
					break;
				case TypeCode.Boolean:
					obj = false;
					break;
				case TypeCode.Char:
					obj = Char.MinValue;
					break;
				case TypeCode.SByte:
					obj = MIN_SBYTE;
					break;
				case TypeCode.Byte:
					obj = MIN_BYTE;
					break;
				case TypeCode.Int16:
					obj = MIN_SHORT;
					break;
				case TypeCode.UInt16:
					obj = MIN_USHORT;
					break;
				case TypeCode.Int32:
					obj = 0;
					break;
				case TypeCode.UInt32:
					obj = 0u;
					break;
				case TypeCode.Int64:
					obj = 0L;
					break;
				case TypeCode.UInt64:
					obj = 0uL;
					break;
				case TypeCode.Single:
					obj = 0f;
					break;
				case TypeCode.Double:
					obj = 0d;
					break;
				case TypeCode.Decimal:
					obj = 0m;
					break;
				case TypeCode.DateTime:
					obj = DateTime.MinValue;
					break;
				default:
					obj = 0;
					break;
			}
			return obj;
		}

		object GetValue (object obj)
		{
			if (Object.Equals (obj, DBNull.Value)) {
				if (!IsNullable) {
					return GetDefaultValue ();
				}
				else {
					return null;
				}
			}
			else {
				return Convert.ChangeType (obj, ObjectType);
			}
		}

	}
}
