using System;
using System.Collections.Generic;

namespace Light.Data
{
    class RelationFieldConfig : IRelationFieldConfig, IConfiguratorFieldConfig
    {
        readonly List<RelationKey> _relationKeys = new List<RelationKey>();

        public RelationFieldConfig(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException ("fieldName");
            }
            FieldName = fieldName;
        }

        public void AddRelationKeys(string masterKey, string relateKey)
        {
            _relationKeys.Add(new RelationKey(masterKey, relateKey));
        }

        public string PropertyName
        {
            get;
            set;
        }

        public string FieldName
        {
            get;
            private set;
        }

        public RelationKey[] GetRelationKeys()
        {
//            foreach (RelationKey key in _relationKeys)
//            {
//                yield return key;
//            }
			return _relationKeys.ToArray();
        }

        public int RelationKeyCount
        {
            get
            {
                return _relationKeys.Count;
            }
        }
    }
}
