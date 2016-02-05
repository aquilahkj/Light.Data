using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Light.Data
{
	class DataTableEntityMapping : DataEntityMapping
	{
		internal DataTableEntityMapping (Type type, string tableName, bool isDataEntity)
			: base (type, tableName, isDataEntity)
		{
			GetPrimaryKey ();
		}

		PrimitiveFieldMapping _identityField;

		public PrimitiveFieldMapping IdentityField {
			get {
				return _identityField;
			}
		}

		//		<PrimitiveFieldMapping> _primaryKeyFields;

		DataFieldMapping[] _primaryKeyFields;

		public DataFieldMapping[] PrimaryKeyFields {
			get {
				
				return _primaryKeyFields;
			}
		}

		void GetPrimaryKey ()
		{
			List<PrimitiveFieldMapping> primaryKeys = new List<PrimitiveFieldMapping> ();
			foreach (FieldMapping field in _fieldList) {
				PrimitiveFieldMapping mapping = field as PrimitiveFieldMapping;
				if (mapping != null) {
					if (mapping.IsIdentity) {
						if (IdentityField == null) {
							TypeCode code = Type.GetTypeCode (mapping.ObjectType);
							if (code == TypeCode.Int32 || code == TypeCode.Int64 || code == TypeCode.UInt32 || code == TypeCode.UInt64) {
								_identityField = mapping;
							}
							else {
								throw new LightDataException (RE.TheTypeOfIdentityFieldError);
							}
						}
						else {
							throw new LightDataException (RE.DataTableNotAllowMoreIdentityField);
						}
					}
					if (mapping.IsPrimaryKey) {
						primaryKeys.Add (mapping);
					}
				}
			}
			_primaryKeyFields = primaryKeys.ToArray ();
		}
	}
}
