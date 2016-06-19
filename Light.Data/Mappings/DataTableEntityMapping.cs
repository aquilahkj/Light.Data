using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Data table entity mapping.
	/// </summary>
	class DataTableEntityMapping : DataEntityMapping
	{
		internal DataTableEntityMapping (Type type, string tableName, bool isDataEntity)
			: base (type, tableName, isDataEntity)
		{
			GetPrimaryKey ();
		}

		DataFieldMapping _identityField;

		public DataFieldMapping IdentityField {
			get {
				return _identityField;
			}
		}

		public IEnumerable<DataFieldMapping> NoIdentityFields {
			get {
				foreach (DataFieldMapping item in _noIdentityFieldList) {
					yield return item;
				}
			}
		}

		readonly List<DataFieldMapping> _noIdentityFieldList = new List<DataFieldMapping> ();

		public IEnumerable<DataFieldMapping> PrimaryKeyFields {
			get {
				foreach (DataFieldMapping item in _primaryKeyFieldList) {
					yield return item;
				}
			}
		}

		readonly List<DataFieldMapping> _primaryKeyFieldList = new List<DataFieldMapping> ();

		public IEnumerable<DataFieldMapping> NoPrimaryKeyFields {
			get {
				foreach (DataFieldMapping item in _noPrimaryKeyFieldList) {
					yield return item;
				}
			}
		}

		readonly List<DataFieldMapping> _noPrimaryKeyFieldList = new List<DataFieldMapping> ();

		/// <summary>
		/// Gets a value indicating whether this instance has identity.
		/// </summary>
		/// <value><c>true</c> if this instance has identity; otherwise, <c>false</c>.</value>
		public bool HasIdentity {
			get {
				return _identityField != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has primary key.
		/// </summary>
		/// <value><c>true</c> if this instance has primary key; otherwise, <c>false</c>.</value>
		public bool HasPrimaryKey {
			get {
				return _primaryKeyFieldList.Count > 0;
			}
		}

		/// <summary>
		/// Gets the primary key count.
		/// </summary>
		/// <value>The primary key count.</value>
		public int PrimaryKeyCount {
			get {
				return _primaryKeyFieldList.Count;
			}
		}

		void GetPrimaryKey ()
		{
			foreach (FieldMapping field in _fieldList) {
				PrimitiveFieldMapping pfmapping = field as PrimitiveFieldMapping;
				if (pfmapping != null) {
					if (pfmapping.IsIdentity) {
						if (IdentityField == null) {
							_identityField = pfmapping;
						}
						else {
							throw new LightDataException (RE.DataTableNotAllowMoreIdentityField);
						}
					}
					else {
						_noIdentityFieldList.Add (pfmapping);
					}
					if (pfmapping.IsPrimaryKey) {
						_primaryKeyFieldList.Add (pfmapping);
					}
					else {
						_noPrimaryKeyFieldList.Add (pfmapping);
					}
				}
				else {
					DataFieldMapping mapping = field as DataFieldMapping;
					_noIdentityFieldList.Add (mapping);
					_noPrimaryKeyFieldList.Add (mapping);
				}
			}
		}
	}
}
