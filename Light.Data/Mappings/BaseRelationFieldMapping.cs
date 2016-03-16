using System;

namespace Light.Data
{
	abstract class BaseRelationFieldMapping
	{
		readonly protected PropertyHandler handler;

		readonly protected RelationKey[] keyPairs;

		readonly protected Type relateType;

		readonly protected  string fieldName;

		readonly protected DataEntityMapping masterEntityMapping;

		readonly protected DataFieldMapping[] masterFieldMappings;

		readonly protected DataFieldInfo[] masterInfos;

		public PropertyHandler Handler {
			get {
				return handler;
			}
		}

		public DataEntityMapping MasterMapping {
			get {
				return masterEntityMapping;
			}
		}

		public string FieldName {
			get {
				return fieldName;
			}
		}

		readonly object locker = new object ();

		protected DataEntityMapping relateEntityMapping;

		public DataEntityMapping RelateMapping {
			get {
				return relateEntityMapping;
			}
		}

		protected DataFieldMapping[] relateFieldMappings;

		protected DataFieldInfo[] relateInfos;

		protected BaseRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
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
			this.masterEntityMapping = mapping;
			this.relateType = relateType;
			this.keyPairs = keyPairs;
			this.handler = handler;
			this.masterFieldMappings = new DataFieldMapping[keyPairs.Length];
			this.masterInfos = new DataFieldInfo[keyPairs.Length];
			for (int i = 0; i < keyPairs.Length; i++) {
				DataFieldMapping field = mapping.FindDataEntityField (keyPairs [i].MasterKey);
				this.masterFieldMappings [i] = field;
				this.masterInfos [i] = new DataFieldInfo (field);
			}
		}

		protected void InitialRelateMapping ()
		{
			if (this.relateEntityMapping == null) {
				lock (locker) {
					if (this.relateEntityMapping == null) {
						InitialRelateMappingInc ();
					}
				}
			}
		}

		protected virtual void InitialRelateMappingInc ()
		{
			DataEntityMapping mapping = DataMapping.GetEntityMapping (this.relateType);
			DataFieldInfo[] infos = new DataFieldInfo[keyPairs.Length];
			DataFieldMapping[] fields = new DataFieldMapping[keyPairs.Length];
			for (int i = 0; i < this.keyPairs.Length; i++) {
				DataFieldMapping field = mapping.FindDataEntityField (keyPairs [i].RelateKey);
				fields [i] = field;
				infos [i] = new DataFieldInfo (field);
			}
			this.relateInfos = infos;
			this.relateFieldMappings = fields;
			this.relateEntityMapping = mapping;
		}

		public bool IsReverseMatch (BaseRelationFieldMapping mapping)
		{
			this.InitialRelateMapping ();
			mapping.InitialRelateMapping ();
			if (this.masterEntityMapping.TableName != mapping.relateEntityMapping.TableName) {
				return false;
			}
			if (this.relateEntityMapping.TableName != mapping.masterEntityMapping.TableName) {
				return false;
			}
			if (this.keyPairs.Length != mapping.keyPairs.Length) {
				return false;
			}
			for (int i = 0; i < this.keyPairs.Length; i++) {
				bool ismatch = false;
				RelationKey master = this.keyPairs [i];
				for (int j = 0; j < mapping.keyPairs.Length; j++) {
					RelationKey relate = mapping.keyPairs [j];
					if (master.IsReverseMatch (relate)) {
						ismatch = true;
						break;
					}
				}
				if (!ismatch) {
					return false;
				}
			}
			return true;
		}

		public bool IsMatch (BaseRelationFieldMapping mapping)
		{
			this.InitialRelateMapping ();
			mapping.InitialRelateMapping ();
			if (this.masterEntityMapping.TableName != mapping.masterEntityMapping.TableName) {
				return false;
			}
			if (this.relateEntityMapping.TableName != mapping.relateEntityMapping.TableName) {
				return false;
			}
			if (this.keyPairs.Length != mapping.keyPairs.Length) {
				return false;
			}
			for (int i = 0; i < this.keyPairs.Length; i++) {
				bool ismatch = false;
				RelationKey master = this.keyPairs [i];
				for (int j = 0; j < mapping.keyPairs.Length; j++) {
					RelationKey relate = mapping.keyPairs [j];
					if (master.IsMatch (relate)) {
						ismatch = true;
						break;
					}
				}
				if (!ismatch) {
					return false;
				}
			}
			return true;
		}



	}
}

