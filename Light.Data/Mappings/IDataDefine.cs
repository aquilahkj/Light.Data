using System.Data;

namespace Light.Data
{
	interface IDataDefine
	{
		object LoadData (DataContext context, IDataReader datareader);

		object LoadData (DataContext context, DataRow datarow);
	}
}
