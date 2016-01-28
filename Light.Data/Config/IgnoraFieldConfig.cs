using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class IgnoraFieldConfig : IConfiguratorFieldConfig
	{
		public IgnoraFieldConfig (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException ("fieldName");
			}
			FieldName = fieldName;
		}

		public string FieldName {
			get;
			private set;
		}
	}
}
