using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	class PrimitiveDataDefine : DataDefine
	{
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
