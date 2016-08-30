using System;
using System.Data;

namespace Light.Data
{
	abstract class AggregateMapping : DataMapping, IJoinTableMapping
	{
		public AggregateMapping (Type type)
			: base (type)
		{
		}

		public abstract object CreateJoinTableData (DataContext context, IDataReader datareader, QueryState queryState, string aliasName);

		//public override object InitialData ()
		//{
		//	throw new NotImplementedException ();
		//}

		//public override object LoadData (DataContext context, IDataReader datareader, object state)
		//{
		//	throw new NotImplementedException ();
		//}
	}
}

