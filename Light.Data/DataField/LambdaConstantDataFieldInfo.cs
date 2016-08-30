using System;
namespace Light.Data
{
	class LambdaConstantDataFieldInfo : LambdaDataFieldInfo
	{
		object _value;

		public LambdaConstantDataFieldInfo (object value)
			:base(DataEntityMapping.Default)
		{
			_value = value;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, false);
			if (sql != null) {
				return sql;
			}
			object value = LambdaExpressionExtend.ConvertLambdaObject (_value);
			sql = state.AddDataParameter (value);

			state.SetDataSql (this, false, sql);
			return sql;
		}

	}
}

