using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
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
