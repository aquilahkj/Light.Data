using System;

namespace Light.Data
{
	class LengthDataFieldInfo : ExtendDataFieldInfo
	{
		internal LengthDataFieldInfo (DataFieldInfo info)
			: base (info)
		{

		}

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	return factory.CreateLengthSql (field);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateLengthSql (field);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field = BaseFieldInfo.CreateSqlString (factory, isFullName, state);
			return factory.CreateLengthSql (field);
		}

		internal override string DBType {
			get {
				return "int";
			}
		}

		internal override object ToParameter (object value)
		{
			return Convert.ToInt32 (value);
		}

		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	if (base.EqualsDetail (info)) {
		//		LengthDataFieldInfo target = info as LengthDataFieldInfo;
		//		if (!Object.Equals (target, null)) {
		//			return true;
		//		}
		//		else {
		//			return false;
		//		}
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
