using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	class DataTableEntityMapping : DataEntityMapping
	{
		internal DataTableEntityMapping (Type type, string tableName, bool isDataEntity)
			: base (type, tableName, isDataEntity)
		{
			GetPrimaryKey ();
		}

		public PrimitiveFieldMapping IdentityField {
			get;
			private set;
		}

		public PrimitiveFieldMapping[] PrimaryKeyFields {
			get;
			private set;
		}

		void GetPrimaryKey ()
		{
			List<PrimitiveFieldMapping> primaryKeys = new List<PrimitiveFieldMapping> ();
			foreach (DataFieldMapping field in GetFieldMappings()) {
				PrimitiveFieldMapping mapping = field as PrimitiveFieldMapping;
				if (mapping != null) {
					if (mapping.IsIdentity) {
						if (IdentityField == null) {
							TypeCode code = Type.GetTypeCode (mapping.ObjectType);
							if (code == TypeCode.Int32 || code == TypeCode.Int64 || code == TypeCode.UInt32 || code == TypeCode.UInt64) {
								IdentityField = mapping;
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
			PrimaryKeyFields = primaryKeys.ToArray ();
		}
	}
}
