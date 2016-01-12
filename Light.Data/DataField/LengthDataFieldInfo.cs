using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
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

//		internal override string DBType {
//			get {
//				return string.Empty;
//			}
//		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				LengthDataFieldInfo target = info as LengthDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return true;
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}
	}
}
