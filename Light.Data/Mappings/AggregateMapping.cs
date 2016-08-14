using System;
using System.Data;

namespace Light.Data
{
	abstract class AggregateMapping : DataMapping
	{
		public AggregateMapping (Type type)
			: base (type)
		{
		}

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

