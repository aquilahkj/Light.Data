using System;

namespace Light.Data
{
	public class EnumSelectFieldInfo:SelectFieldInfo
	{
		readonly Enum _value;

		public EnumSelectFieldInfo (Enum value)
		{
			this._value = value;
		}

		#region implemented abstract members of SelectFieldInfo

		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter)
		{
			string pn = factory.CreateTempParamName ();
			dataParameter = new DataParameter (pn, _value, null);
			return pn;
		}

		#endregion
	}
}

