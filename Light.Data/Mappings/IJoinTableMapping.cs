using System;
using System.Data;

namespace Light.Data
{
	interface IJoinTableMapping
	{
		object InitialData ();

		object CreateJoinTableData (DataContext context, IDataReader datareader, QueryState queryState, string aliasName);
	}
}

