using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class PrimitiveFieldMapping : DataFieldMapping
	{
		public PrimitiveFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
//			ObjectType = type;
//			Name = fieldName;
//			IndexName = indexName;
//			TypeMapping = mapping;
			if (type != typeof(string)) {
				Type itemstype = System.Type.GetType ("System.Nullable`1");
				nullableType = itemstype.MakeGenericType (type);
			}
			else {
				nullableType = type;
			}
		}

//		bool isString;
//
//		public bool IsString {
//			get {
//				return isString;
//			}
//		}

		Type nullableType;

		public Type NullableType {
			get {
				return nullableType;
			}
		}



		bool _isPrimaryKey;

		public bool IsPrimaryKey {
			get {
				return _isPrimaryKey;
			}
			set {
				_isPrimaryKey = value;
			}
		}

		bool _isIdentity;

		public bool IsIdentity {
			get {
				return _isIdentity;
			}
			set {
				_isIdentity = value;
			}
		}
	}
}
