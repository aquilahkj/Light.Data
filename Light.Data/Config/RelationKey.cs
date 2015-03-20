using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    class RelationKey
    {
        public RelationKey(string masterKey, string relateKey)
        {
            if (string.IsNullOrEmpty(masterKey))
            {
                throw new ArgumentNullException("MasterKey");
            }
            if (string.IsNullOrEmpty(relateKey))
            {
                throw new ArgumentNullException("RelateKey");
            }
            MasterKey = masterKey;
            RelateKey = relateKey;
        }

        public string MasterKey
        {
            get;
            private set;
        }

        public string RelateKey
        {
            get;
            set;
        }
    }
}
