using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	interface IFieldCollection : IDataDefine
	{
//		FieldMapping[] GetFieldMappings ();

		IEnumerable<FieldMapping> FieldMappings {
			get;
		}

		FieldMapping FindFieldMapping (string fieldName);

		object InitialData ();
	}
}
