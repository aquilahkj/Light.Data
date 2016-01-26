using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Light.Data
{
	class PrimitiveDataDefine : DataDefine
	{
		//		static Dictionary<Type, bool> DefineList = new Dictionary<Type, bool> ();
		//
		//		static PrimitiveDataDefine ()
		//		{
		//			DefineList.Add (typeof(Boolean), false);
		////			DefineList.Add (typeof(Char), false);
		//			DefineList.Add (typeof(SByte), false);
		//			DefineList.Add (typeof(Byte), false);
		//			DefineList.Add (typeof(Int16), false);
		//			DefineList.Add (typeof(UInt16), false);
		//			DefineList.Add (typeof(Int32), false);
		//			DefineList.Add (typeof(UInt32), false);
		//			DefineList.Add (typeof(Int64), false);
		//			DefineList.Add (typeof(UInt64), false);
		//			DefineList.Add (typeof(Single), false);
		//			DefineList.Add (typeof(Double), false);
		//			DefineList.Add (typeof(Decimal), false);
		//			DefineList.Add (typeof(DateTime), false);
		//			DefineList.Add(typeof(String), true);
		//
		//			DefineList.Add (typeof(Boolean?), true);
		////			DefineList.Add (typeof(Char?), true);
		//			DefineList.Add (typeof(SByte?), true);
		//			DefineList.Add (typeof(Byte?), true);
		//			DefineList.Add (typeof(Int16?), true);
		//			DefineList.Add (typeof(UInt16?), true);
		//			DefineList.Add (typeof(Int32?), true);
		//			DefineList.Add (typeof(UInt32?), true);
		//			DefineList.Add (typeof(Int64?), true);
		//			DefineList.Add (typeof(UInt64?), true);
		//			DefineList.Add (typeof(Single?), true);
		//			DefineList.Add (typeof(Double?), true);
		//			DefineList.Add (typeof(Decimal?), true);
		//			DefineList.Add (typeof(DateTime?), true);
		//		}
		//
		//		public static PrimitiveDataDefine Create (Type type, string fieldName)
		//		{
		//			PrimitiveDataDefine mapping = CreateType (type);
		//			mapping.FieldName = fieldName;
		//			return mapping;
		//		}
		//
		//		public static PrimitiveDataDefine Create (Type type, int fieldOrder)
		//		{
		//			PrimitiveDataDefine mapping = CreateType (type);
		//			mapping.FieldOrder = fieldOrder;
		//			return mapping;
		//		}

		static Dictionary<Type, PrimitiveDataDefine> DefineList = new Dictionary<Type, PrimitiveDataDefine> ();

		static PrimitiveDataDefine ()
		{
			DefineList.Add (typeof(Char), new PrimitiveDataDefine (typeof(Char), false));
			DefineList.Add (typeof(Boolean), new PrimitiveDataDefine (typeof(Boolean), false));
			DefineList.Add (typeof(SByte), new PrimitiveDataDefine (typeof(SByte), false));
			DefineList.Add (typeof(Byte), new PrimitiveDataDefine (typeof(Byte), false));
			DefineList.Add (typeof(Int16), new PrimitiveDataDefine (typeof(Int16), false));
			DefineList.Add (typeof(UInt16), new PrimitiveDataDefine (typeof(UInt16), false));
			DefineList.Add (typeof(Int32), new PrimitiveDataDefine (typeof(Int32), false));
			DefineList.Add (typeof(UInt32), new PrimitiveDataDefine (typeof(UInt32), false));
			DefineList.Add (typeof(Int64), new PrimitiveDataDefine (typeof(Int64), false));
			DefineList.Add (typeof(UInt64), new PrimitiveDataDefine (typeof(UInt64), false));
			DefineList.Add (typeof(Single), new PrimitiveDataDefine (typeof(Single), false));
			DefineList.Add (typeof(Double), new PrimitiveDataDefine (typeof(Double), false));
			DefineList.Add (typeof(Decimal), new PrimitiveDataDefine (typeof(Decimal), false));
			DefineList.Add (typeof(DateTime), new PrimitiveDataDefine (typeof(DateTime), false));

			DefineList.Add (typeof(Char?), new PrimitiveDataDefine (typeof(Char), false));
			DefineList.Add (typeof(String), new PrimitiveDataDefine (typeof(String), true));
			DefineList.Add (typeof(Boolean?), new PrimitiveDataDefine (typeof(Boolean), true));
			DefineList.Add (typeof(SByte?), new PrimitiveDataDefine (typeof(SByte), true));
			DefineList.Add (typeof(Byte?), new PrimitiveDataDefine (typeof(Byte), true));
			DefineList.Add (typeof(Int16?), new PrimitiveDataDefine (typeof(Int16), true));
			DefineList.Add (typeof(UInt16?), new PrimitiveDataDefine (typeof(UInt16), true));
			DefineList.Add (typeof(Int32?), new PrimitiveDataDefine (typeof(Int32), true));
			DefineList.Add (typeof(UInt32?), new PrimitiveDataDefine (typeof(UInt32), true));
			DefineList.Add (typeof(Int64?), new PrimitiveDataDefine (typeof(Int64), true));
			DefineList.Add (typeof(UInt64?), new PrimitiveDataDefine (typeof(UInt64), true));
			DefineList.Add (typeof(Single?), new PrimitiveDataDefine (typeof(Single), true));
			DefineList.Add (typeof(Double?), new PrimitiveDataDefine (typeof(Double), true));
			DefineList.Add (typeof(Decimal?), new PrimitiveDataDefine (typeof(Decimal), true));
			DefineList.Add (typeof(DateTime?), new PrimitiveDataDefine (typeof(DateTime), true));
		}
			

		//		public static PrimitiveDataDefine GetDefine (Type type)
		//		{
		//			PrimitiveDataDefine define;
		//			DefineList.TryGetValue (type, out define);
		//			return define;
		//		}

		//		public static bool TryParseDefine (Type type, PrimitiveFieldMapping mapping, out PrimitiveDataDefine define)
		//		{
		//			return DefineList.TryGetValue (type, out define);
		//		}

		public static PrimitiveDataDefine ParseDefine (Type type, PrimitiveFieldMapping mapping)
		{
			Type otype = mapping.IsNullable ? mapping.NullableType : mapping.ObjectType;
			if (otype != type) {
				throw new LightDataException (RE.UnmatchDataDefineType);
			}
			PrimitiveDataDefine define;
			if (!DefineList.TryGetValue (type, out define)) {
				throw new LightDataException (RE.UnsupportDataDefineType);
			}
			return define;
		}

		public static PrimitiveDataDefine ParseDefine (Type type)
		{
			PrimitiveDataDefine define;
			if (!DefineList.TryGetValue (type, out define)) {

			}
			return define;
		}

		//		private static PrimitiveDataDefine CreateType (Type type)
		//		{
		//			PrimitiveDataDefine mapping = null;
		//			bool isnullable;
		//			if (DefineList.TryGetValue (type, out isnullable)) {
		//				mapping = new PrimitiveDataDefine (type);
		//				mapping.IsNullable = isnullable;
		//				return mapping;
		//			}
		//			if (type == typeof(string)) {
		//				mapping = new PrimitiveDataDefine (typeof(string));
		//			}
		//			else {
		//				throw new LightDataException (RE.SingleFieldSelectTypeError);
		//			}
		//			return mapping;
		//		}

		//		public static PrimitiveDataDefine CreateString (bool isNullable, string fieldName)
		//		{
		//			PrimitiveDataDefine mapping = CreateFromStringType (isNullable);
		//			mapping.FieldName = fieldName;
		//			return mapping;
		//		}
		//
		//		public static PrimitiveDataDefine CreateString (bool isNullable, int fieldOrder)
		//		{
		//			PrimitiveDataDefine mapping = CreateFromStringType (isNullable);
		//			mapping.FieldOrder = fieldOrder;
		//			return mapping;
		//		}
		//
		//		private static PrimitiveDataDefine CreateFromStringType (bool isNullable)
		//		{
		//			PrimitiveDataDefine mapping = new PrimitiveDataDefine (typeof(string));
		//			mapping.IsNullable = isNullable;
		//			return mapping;
		//		}

		//		private PrimitiveDataDefine (Type type)
		//			: base (type)
		//		{
		//			_typeCode = Type.GetTypeCode (type);
		//		}

		private PrimitiveDataDefine (Type type, bool isNullable)
			: base (type, isNullable)
		{
			_typeCode = Type.GetTypeCode (type);
		}

		TypeCode _typeCode;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="datareader"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="datarow"></param>
		/// <returns></returns>
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

		object GetValue (object obj)
		{
			if (Object.Equals (obj, DBNull.Value)) {
				if (!IsNullable) {
					return Utility.GetDefaultValue (_typeCode);
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
