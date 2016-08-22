using System;
namespace Light.Data
{
	class ConcatStringDataFieldInfo : ExtendDataFieldInfo
	{
		object _value;
		bool _forward;

		internal ConcatStringDataFieldInfo (DataFieldInfo info, object value, bool forward)
			: base (info)
		{
			this._value = value;
			this._forward = forward;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	DataParameter [] dataParameters1 = null;
		//	string field = BaseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters1);
		//	object value = factory.CreateStringWrap (_value);
		//	string sql = factory.CreateDualConcatSql (field, value, _forward);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field = BaseFieldInfo.CreateSqlString (factory, isFullName, state);
			object value = factory.CreateStringWrap (_value);
			string sql = factory.CreateDualConcatSql (field, value, _forward);
			return sql;
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		internal override object ToParameter (object value)
		{
			return value;
		}

		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	if (base.EqualsDetail (info)) {
		//		ConcatStringDataFieldInfo target = info as ConcatStringDataFieldInfo;
		//		if (!Object.Equals (target, null)) {
		//			return this._forward == target._forward && Object.Equals (this._value, target._value);
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

