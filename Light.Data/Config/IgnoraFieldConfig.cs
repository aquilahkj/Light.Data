using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	class IgnoraFieldConfig : IConfiguratorFieldConfig
	{
		public IgnoraFieldConfig (string fieldName)
		{
			if (string.IsNullOrEmpty (fieldName)) {
				throw new ArgumentNullException ("FieldName");
			}
			FieldName = fieldName;
		}

		public string FieldName {
			get;
			private set;
		}
	}
}
