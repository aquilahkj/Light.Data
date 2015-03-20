using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
	abstract class DataDefine : IDataDefine
	{
		Type _type = null;

		public Type ObjectType {
			get {
				return _type;
			}
		}

		protected DataDefine (Type type)
		{
			_type = type;
		}

		string _fieldName = null;

		protected string FieldName {
			get {
				return _fieldName;
			}
			set {
				_fieldName = value;
			}
		}

		int _fieldOrder = 0;

		public int FieldOrder {
			get {
				return _fieldOrder;
			}
			set {
				_fieldOrder = value;
			}
		}

		bool _isNullable = false;

		public bool IsNullable {
			get {
				return _isNullable;
			}
			set {
				_isNullable = value;
			}
		}

		public abstract object LoadData (DataContext context, System.Data.IDataReader datareader);

		public abstract object LoadData (DataContext context, System.Data.DataRow datarow);
	}
}
