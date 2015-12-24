using System;
using System.Data;

namespace Light.Data
{
	class ErrorDataMapping:DataMapping
	{
		public ErrorDataMapping (Exception ex) : base (null)
		{
			this.innerException = ex;
		}

		#region implemented abstract members of DataMapping

		public override object LoadData (DataContext context, IDataReader datareader)
		{
			throw new NotImplementedException ();
		}

		public override object LoadData (DataContext context, DataRow datarow)
		{
			throw new NotImplementedException ();
		}

		public override object InitialData ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		readonly Exception innerException;

		public Exception InnerException {
			get {
				return innerException;
			}
		}
	}
}

