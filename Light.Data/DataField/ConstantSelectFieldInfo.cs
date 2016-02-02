using System;

namespace Light.Data
{
	class ConstantSelectFieldInfo:SelectFieldInfo
	{
		readonly object _value;

		public ConstantSelectFieldInfo (object value)
		{
			_value = value;
		}

		#region implemented abstract members of SelectFieldInfo

		internal override string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter)
		{
			if (_value != null) {
				string pn = factory.CreateTempParamName ();
				dataParameter = new DataParameter (pn, _value, null);
				return pn;
			}
			else {
				dataParameter = null;
				return factory.CreateNullSql ();
			}
		}

		#endregion
	}
}

