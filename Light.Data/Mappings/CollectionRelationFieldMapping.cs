using System;
using System.Data;

namespace Light.Data
{
	class CollectionRelationFieldMapping
	{
		readonly PropertyHandler handler;

		public PropertyHandler Handler {
			get {
				return handler;
			}
		}

		readonly RelationKey[] keyPairs;

		readonly Type relateType;

		readonly DataMapping masterMapping;

		public DataMapping Mapping {
			get {
				return masterMapping;
			}
		}

		readonly string fieldName;

		DataMapping relateMapping = null;

		DataFieldInfo[] relateInfos = null;

		FieldMapping[] masterMappings = null;

		public CollectionRelationFieldMapping (string fieldName, DataMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
		{
			if (fieldName == null)
				throw new ArgumentNullException ("fieldName");
			if (mapping == null)
				throw new ArgumentNullException ("mapping");
			if (relateType == null)
				throw new ArgumentNullException ("relateType");
			if (keyPairs == null || keyPairs.Length == 0)
				throw new ArgumentNullException ("keyPairs");
			if (handler == null)
				throw new ArgumentNullException ("handler");
			this.fieldName = fieldName;
			this.masterMapping = mapping;
			this.relateType = relateType;
			this.keyPairs = keyPairs;
			this.handler = handler;
			masterMappings = new FieldMapping[keyPairs.Length];
			for (int i = 0; i < keyPairs.Length; i++) {
				masterMappings [i] = mapping.FindFieldMapping (keyPairs [i].MasterKey);
			}
		}

		readonly object locker = new object ();

		public object ToProperty (DataContext context, IDataReader datareader)
		{
			if (this.relateMapping == null) {
				lock (locker) {
					if (this.relateMapping == null) {
						DataEntityMapping mapping = DataMapping.GetEntityMapping (this.relateType);
						DataFieldInfo[] infos = new DataFieldInfo[keyPairs.Length];
						for (int i = 0; i < keyPairs.Length; i++) {
							DataFieldMapping field = mapping.FindFieldMapping (keyPairs [i].RelateKey) as DataFieldMapping;
//							infos[i]=new DataFieldInfo(
						}
					}
				}
			}
			return null;
		}

	}
}

