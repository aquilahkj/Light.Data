using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.DataField
{
	class LengthDataFieldInfo : ExtendDataFieldInfo
	{
		internal LengthDataFieldInfo (DataFieldInfo info)
			: base (info)
		{

		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateLengthSql (field);
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}
	}
}
