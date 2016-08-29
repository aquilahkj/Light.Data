using System;
namespace Light.Data
{
	class ConstantDataFieldInfo : DataFieldInfo
	{
		object _value;

		public ConstantDataFieldInfo (DataEntityMapping mapping, object value)
			: base (mapping)
		{
			if (value != null) {
				Type type = value.GetType ();
				if (type.IsEnum) {
					TypeCode code = Type.GetTypeCode (type);
					value = Convert.ChangeType (value, code);
				}
			}
			_value = value;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.AddDataParameter (_value);
			return sql;
		}
	}
}

