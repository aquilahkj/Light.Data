using System;
using System.Collections.Generic;

namespace Light.Data
{
	class RelationCycle
	{
		SingleRelationFieldMapping rootRelationMapping;

		public SingleRelationFieldMapping RootRelationMapping {
			get {
				return rootRelationMapping;
			}
		}

		HashSet<SingleRelationFieldMapping> mappings = new HashSet<SingleRelationFieldMapping> ();

		List<SingleRelationFieldMapping> hitMappings = new List<SingleRelationFieldMapping> ();

		Dictionary<DataEntityMapping, string []> entityMappingDict = new Dictionary<DataEntityMapping, string []> ();

		int keyCount;

		public RelationCycle (SingleRelationFieldMapping rootRelationMapping)
		{
			if (rootRelationMapping == null)
				throw new ArgumentNullException (nameof (rootRelationMapping));
			this.rootRelationMapping = rootRelationMapping;
			this.mappings.Add (rootRelationMapping);
			this.hitMappings.Add (rootRelationMapping);
			RelationKey [] keys = rootRelationMapping.GetKeyPairs ();
			this.keyCount = keys.Length;
			string [] masters = new string [this.keyCount];
			string [] relates = new string [this.keyCount];
			for (int i = 0; i < this.keyCount; i++) {
				masters [i] = keys [i].MasterKey;
				relates [i] = keys [i].RelateKey;
			}
			entityMappingDict.Add (rootRelationMapping.MasterMapping, masters);
			entityMappingDict.Add (rootRelationMapping.RelateMapping, relates);
		}

		public bool TryAddCycle (SingleRelationFieldMapping relateMapping, bool exist)
		{
			if (this.mappings.Contains (relateMapping)) {
				throw new LightDataException (string.Format (RE.TheRelationFieldIsExists, relateMapping.FieldName));
			}

			string [] masterkeys;
			if (!entityMappingDict.TryGetValue (relateMapping.MasterMapping, out masterkeys)) {
				return false;
			}
			RelationKey [] pairs = relateMapping.GetKeyPairs ();
			if (pairs.Length != masterkeys.Length) {
				return false;
			}
			string [] relatePairkeys = new string [pairs.Length];
			for (int i = 0; i < masterkeys.Length; i++) {
				for (int j = 0; j < pairs.Length; j++) {
					if (pairs [j].MasterKey == masterkeys [i]) {
						relatePairkeys [i] = pairs [j].RelateKey;
						break;
					}
				}
				if (relatePairkeys [i] == null) {
					return false;
				}
			}
			string [] relatekeys;
			if (!entityMappingDict.TryGetValue (relateMapping.RelateMapping, out relatekeys)) {
				if (exist) {
					return false;
				}
				else {
					this.mappings.Add (relateMapping);
					this.hitMappings.Add (relateMapping);
					entityMappingDict.Add (relateMapping.RelateMapping, relatePairkeys);
					return true;
				}
			}
			else {
				for (int i = 0; i < relatekeys.Length; i++) {
					if (relatekeys [i] != relatePairkeys [i]) {
						return false;
					}
				}
				this.mappings.Add (relateMapping);
				return true;
			}
		}

		public bool TryAddCycle (SingleRelationFieldMapping relateMapping)
		{
			if (this.mappings.Contains (relateMapping)) {
				throw new LightDataException (string.Format (RE.TheRelationFieldIsExists, relateMapping.FieldName));
			}

			string [] masterkeys;
			if (!entityMappingDict.TryGetValue (relateMapping.MasterMapping, out masterkeys)) {
				return false;
			}
			RelationKey [] pairs = relateMapping.GetKeyPairs ();
			if (pairs.Length != masterkeys.Length) {
				return false;
			}
			string [] relatePairkeys = new string [pairs.Length];
			for (int i = 0; i < masterkeys.Length; i++) {
				for (int j = 0; j < pairs.Length; j++) {
					if (pairs [j].MasterKey == masterkeys [i]) {
						relatePairkeys [i] = pairs [j].RelateKey;
						break;
					}
				}
				if (relatePairkeys [i] == null) {
					return false;
				}
			}
			string [] relatekeys;
			if (!entityMappingDict.TryGetValue (relateMapping.RelateMapping, out relatekeys)) {
				this.mappings.Add (relateMapping);
				this.hitMappings.Add (relateMapping);
				entityMappingDict.Add (relateMapping.RelateMapping, relatePairkeys);
				return true;
			}
			else {
				for (int i = 0; i < relatekeys.Length; i++) {
					if (relatekeys [i] != relatePairkeys [i]) {
						return false;
					}
				}
				this.mappings.Add (relateMapping);
				return true;
			}
		}

		public SingleRelationFieldMapping [] GetSingleRelationFieldMapping ()
		{
			return this.hitMappings.ToArray ();
		}
	}
}

