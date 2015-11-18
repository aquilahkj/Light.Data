using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	interface IFieldCollection : IDataDefine
	{
		IEnumerable<FieldMapping> GetFieldMappings ();

		FieldMapping FindFieldMapping (string fieldName);

		object InitialData ();
	}
}
