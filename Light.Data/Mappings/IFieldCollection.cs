using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
	interface IFieldCollection : IDataDefine
	{
		IEnumerable<FieldMapping> GetFieldMappings ();

		FieldMapping FindFieldMapping (string fieldName);

		object InitialData ();
	}
}
