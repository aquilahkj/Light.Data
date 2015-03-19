using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
    class SpecifiedFieldMapping
    {
        public Type ObjectType
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public FieldMapping RelateFieldMapping
        {
            get;
            internal set;
        }

        public DataMapping TypeMapping
        {
            get;
            protected set;
        }

        public Light.Data.Handler.PropertyHandler Handler
        {
            get;
            set;
        }
    }
}
