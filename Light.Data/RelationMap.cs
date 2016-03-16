using System;
using System.Collections.Generic;

namespace Light.Data
{
	class RelationMap
	{
		//		Dictionary<DataEntityMapping, JoinItem> relates = new Dictionary<DataEntityMapping, JoinItem> ();

		readonly DataEntityMapping masterMapping;

		public DataEntityMapping MasterMapping {
			get {
				return masterMapping;
			}
		}

		//		Dictionary<DataEntityMapping, SingleRelationFieldMapping> relateMappingDict = new Dictionary<DataEntityMapping, SingleRelationFieldMapping> ();

		readonly List<JoinItem> itemList = new List<JoinItem> ();



		//		List<DataEntityMapping> mappingList = new List<DataEntityMapping> ();
		//
		//		List<SingleRelationFieldMapping> relateList = new List<SingleRelationFieldMapping> ();

		//		List<SingleRelationFieldMapping> referList = new List<SingleRelationFieldMapping> ();

		readonly HashSet<SingleRelationFieldMapping> referHash = new HashSet<SingleRelationFieldMapping> ();

		readonly HashSet<string> entityHash = new HashSet<string> ();
		//		readonly HashSet<DataEntityMapping> entityHash = new HashSet<DataEntityMapping> ();


		public RelationMap (DataEntityMapping masterMapping)
		{
			this.masterMapping = masterMapping;
			this.entityHash.Add (masterMapping.TableName);
//			this.relates.Add (masterMapping, null);
			LoadEntityMapping (this.masterMapping);
		}

		void LoadEntityMapping (DataEntityMapping mapping)
		{
			SingleRelationFieldMapping[] relateMappings = mapping.GetSingleJoinTableRelationFieldMappings ();
			foreach (SingleRelationFieldMapping relateMapping in relateMappings) {
				relateMapping.InitialRelation ();
				if (this.entityHash.Contains (relateMapping.RelateMapping.TableName)) {
					if (!this.referHash.Contains (relateMapping)) {
						bool flag = false;
						foreach (SingleRelationFieldMapping item in this.referHash) {
							if (item.IsMatch (relateMapping) || item.IsReverseMatch (relateMapping)) {
								flag = true;
								break;
							}
						}
						if (flag) {
							this.referHash.Add (relateMapping);
						}
					}
				}
				else {
					this.entityHash.Add (relateMapping.RelateMapping.TableName);
					this.itemList.Add (relateMapping.Item);
					this.referHash.Add (relateMapping);
					if (relateMapping.RelateMapping.HasJoinRelateModel) {
						LoadEntityMapping (relateMapping.RelateMapping);
					}
				}
			}
		}

		public JoinItem[] GetJoinItems ()
		{
			return itemList.ToArray ();
		}

		public bool CheckValid (SingleRelationFieldMapping relationMapping)
		{
			return this.referHash.Contains (relationMapping);
		}

	}
}

