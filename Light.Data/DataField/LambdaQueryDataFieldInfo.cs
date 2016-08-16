using System;
namespace Light.Data
{
	class LambdaQueryDataFieldInfo : LambdaDataFieldInfo
	{
		QueryExpression _query;

		public LambdaQueryDataFieldInfo (QueryExpression query)
			: base (query.TableMapping)
		{
			_query = query;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _query.CreateSqlString (factory, isFullName, out dataParameters);
		}
	}
}

