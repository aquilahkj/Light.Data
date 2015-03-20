using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    class RelationFieldConfig : IRelationFieldConfig, IConfiguratorFieldConfig
    {
        List<RelationKey> _relationKeys = new List<RelationKey>();

        public RelationFieldConfig(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("FieldName");
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

        public IEnumerable<RelationKey> GetRelationKeys()
        {
            foreach (RelationKey key in _relationKeys)
            {
                yield return key;
            }
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
