using System;
using System.Collections;

namespace Light.Data
{
	class DynamicEnumFieldMapping : DynamicFieldMapping
	{
		//Hashtable hashTable = new Hashtable ();

		public DynamicEnumFieldMapping (Type type, string fieldName, DynamicCustomMapping mapping)
			: base (type, fieldName, mapping, true)
		{

		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				return null;
			}
			else {
				//string str = value as string;
				//if (str != null) {
				//	return Enum.Parse (ObjectType, str);
				//}
				//else {
				//	Type type = value.GetType ();
				//	TypeCode code = Type.GetTypeCode (type);
				//	if (code != this._typeCode) {
				//		value = Convert.ChangeType (value, this._typeCode);
				//	}
				//	return value;
				//}
				//Type type = value.GetType ();
				//TypeCode code = Type.GetTypeCode (type);
				//if (code != this._typeCode) {
				//	value = Convert.ChangeType (value, this._typeCode);
				//}
				//value = Convert.ChangeType (value, _objectType);
				value = Enum.ToObject (_objectType, value);
				return value;
			}
		}
	}
}

