using System;
namespace Light.Data
{
	class LambdaAggregateCountAllDataFieldInfo : LambdaDataFieldInfo
	{
		public LambdaAggregateCountAllDataFieldInfo ()
			: base (DataEntityMapping.Default)
		{
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	dataParameters = null;
		//	return factory.CreateCountAllSql ();
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			sql = factory.CreateCountAllSql ();

			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

