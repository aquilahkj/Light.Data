using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	abstract class DataDefine : IDataDefine
	{
//		public static DataDefine CreateDefine (Type type, DataFieldMapping mapping)
//		{
//			Type objType = null;
//			if (type.IsGenericType) {
//				Type frameType = type.GetGenericTypeDefinition ();
//				if (frameType.FullName == "System.Nullable`1") {
//					Type[] arguments = type.GetGenericArguments ();
//					objType = arguments [0];
//				}
//				else {
//					throw new LightDataException (RE.OnlyPrimitiveFieldCanSelectSingle);
//				}
//			}
//			else {
//				objType = type;
//			}
//
//			if (objType.IsEnum) {
//				EnumFieldMapping enumMapping = mapping as EnumFieldMapping;
//				if (objType != enumMapping.ObjectType) {
//					throw new LightDataException (RE.OnlyPrimitiveFieldCanSelectSingle);
//				}
//				EnumDataDefine define = EnumDataDefine.Create (type, enumMapping.EnumType);
//			}
//		}
//

		Type _type = null;

		public Type ObjectType {
			get {
				return _type;
			}
		}

		protected DataDefine (Type type, bool isNullable)
		{
			_type = type;
			_isNullable = isNullable;
		}

//		string _fieldName = null;
//
//		protected string FieldName {
//			get {
//				return _fieldName;
//			}
//			set {
//				_fieldName = value;
//			}
//		}
//
//		int _fieldOrder = 0;
//
//		public int FieldOrder {
//			get {
//				return _fieldOrder;
//			}
//			set {
//				_fieldOrder = value;
//			}
//		}
//
		bool _isNullable = false;

		public bool IsNullable {
			get {
				return _isNullable;
			}
//			set {
//				_isNullable = value;
//			}
		}

		public abstract object LoadData (DataContext context, System.Data.IDataReader datareader);

		public abstract object LoadData (DataContext context, System.Data.DataRow datarow);
	}
}
