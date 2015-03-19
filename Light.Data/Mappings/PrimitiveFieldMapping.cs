using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
    class PrimitiveFieldMapping : DataFieldMapping
    {
        public PrimitiveFieldMapping(Type type, string fieldName, string indexName, DataMapping mapping)
        {
            ObjectType = type;
            Name = fieldName;
            IndexName = indexName;
            TypeMapping = mapping;
        }

        public bool IsPrimaryKey
        {
            get;
            set;
        }

        public bool IsIdentity
        {
            get;
            set;
        }



    }
}
