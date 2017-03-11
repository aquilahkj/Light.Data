using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	class EnumDataDefine : DataDefine
	{
		readonly TypeCode _typeCode;

		readonly object _defaultValue = null;

		public EnumDataDefine (Type type, bool isNullable)
			: base (type, isNullable)
		{
			Array values = Enum.GetValues (type);
			_defaultValue = values.GetValue (0);
			_typeCode = Type.GetTypeCode (ObjectType);
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
		{
			object obj = datareader [0];
			return GetValue (obj);
		}

		object GetValue (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				if (!IsNullable) {
					return _defaultValue;
				}
				else {
					return null;
				}
			}
			else {
				//Type type = value.GetType ();
				//TypeCode code = Type.GetTypeCode (type);
				//if (code != this._typeCode) {
				//	value = Convert.ChangeType (value, this._typeCode);
				//}
				//return value;
				value = Enum.ToObject (_objectType, value);
				return value;
			}
		}
	}
}
