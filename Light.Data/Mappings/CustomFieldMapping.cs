using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
    class CustomFieldMapping : DataFieldMapping
    {
        public CustomFieldMapping(string fieldName, DataEntityMapping mapping)
        {
            Name = fieldName;
            TypeMapping = mapping;
        }
    }
}
