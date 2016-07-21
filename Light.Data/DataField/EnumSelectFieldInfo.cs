using System;

namespace Light.Data
{
	class EnumSelectFieldInfo : SelectFieldInfo
	{
		readonly Enum _value;

		Type _enumType;

		public Type EnumType {
			get {
				return _enumType;
			}
		}

		TypeCode _typeCode;


		public EnumSelectFieldInfo (Enum value)
		{
			this._value = value;
			this._enumType = value.GetType ();
			this._typeCode = Type.GetTypeCode (this._enumType);
		}

		#region implemented abstract members of SelectFieldInfo

		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _value);
			dataParameters = new [] { dataParameter };
			return pn;
		}

		internal override DataEntityMapping TableMapping {
			get {
				return null;
			}
		}

		#endregion

		internal string CreateDataFieldSql (CommandFactory factory, EnumFieldType fieldType, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			if (fieldType == EnumFieldType.EnumToString) {
				DataParameter dataParameter = new DataParameter (pn, _value.ToString (), "string");
				dataParameters = new [] { dataParameter };
			}
			else {
				DataParameter dataParameter = new DataParameter (pn, Convert.ChangeType (_value, _typeCode));
				dataParameters = new [] { dataParameter };
			}
			return pn;
		}


	}
}

