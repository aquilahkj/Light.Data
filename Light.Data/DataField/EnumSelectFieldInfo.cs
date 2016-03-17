using System;

namespace Light.Data
{
	class EnumSelectFieldInfo:SelectFieldInfo
	{
		readonly Enum _value;

		Type _enumType;

		public Type EnumType {
			get {
				return _enumType;
			}
		}

		public EnumSelectFieldInfo (Enum value)
		{
			this._value = value;
			this._enumType = value.GetType ();
		}

		#region implemented abstract members of SelectFieldInfo

		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter)
		{
			string pn = factory.CreateTempParamName ();
			dataParameter = new DataParameter (pn, _value);
			return pn;
		}

		internal override DataEntityMapping TableMapping {
			get {
				return null;
			}
		}

		#endregion

		internal string CreateDataFieldSql (CommandFactory factory, EnumFieldType fieldType, out DataParameter dataParameter)
		{
			string pn = factory.CreateTempParamName ();
			if (fieldType == EnumFieldType.EnumToString) {
				dataParameter = new DataParameter (pn, _value.ToString (), "string");
			}
			else {
				dataParameter = new DataParameter (pn, _value);
			}
			return pn;
		}


	}
}

