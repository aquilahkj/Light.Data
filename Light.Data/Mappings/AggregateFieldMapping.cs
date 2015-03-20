using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data.Mappings
{
	class AggregateFieldMapping : FieldMapping
	{
		public AggregateFieldMapping (Type type, string name, string indexName, DataMapping mapping)
			: base (type, name, indexName, mapping, false, null)
		{
			if (!Regex.IsMatch (name, _fieldRegex, RegexOptions.IgnoreCase)) {
				throw new LightDataException (string.Format (RE.FieldNameIsInvalid, name));
			}
//			ObjectType = type;
//			Name = name;
//			IndexName = indexName;
//			TypeMapping = mapping;
		}

		public override bool IsNullable {
			get {
				return false;
			}
		}

		public override object ToColumn (object value)
		{
			return null;
		}
	}
}
