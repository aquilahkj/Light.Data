using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Light.Data.Mappings
{
	interface IDataDefine
	{
		object LoadData (DataContext context, IDataReader datareader);

		object LoadData (DataContext context, DataRow datarow);
	}
}
