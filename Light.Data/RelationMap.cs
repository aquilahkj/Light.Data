using System.Collections.Generic;

namespace Light.Data
{
	class RelationMap
	{
		readonly DataEntityMapping rootMapping;

		public DataEntityMapping RootMapping {
			get {
				return rootMapping;
			}
		}

		readonly List<JoinItem> itemList = new List<JoinItem> ();

		readonly HashSet<SingleRelationFieldMapping> relateHash = new HashSet<SingleRelationFieldMapping> ();

		readonly HashSet<string> entityHash = new HashSet<string> ();

		public RelationMap (DataEntityMapping rootMapping)
		{
			this.rootMapping = rootMapping;
			this.entityHash.Add (rootMapping.TableName);
			LoadEntityMapping (this.rootMapping);
		}

		void LoadEntityMapping (DataEntityMapping mapping)
		{
			SingleRelationFieldMapping[] relateMappings = mapping.GetSingleJoinTableRelationFieldMappings ();
			foreach (SingleRelationFieldMapping relateMapping in relateMappings) {
				relateMapping.InitialRelation ();
				if (this.entityHash.Contains (relateMapping.RelateMapping.TableName)) {
					if (!this.relateHash.Contains (relateMapping)) {
						bool flag = false;
						foreach (SingleRelationFieldMapping item in this.relateHash) {
							if (item.IsMatch (relateMapping) || item.IsReverseMatch (relateMapping)) {
								flag = true;
								break;
							}
						}
						if (flag) {
							this.relateHash.Add (relateMapping);
						}
					}
				}
				else {
					this.entityHash.Add (relateMapping.RelateMapping.TableName);
					this.itemList.Add (relateMapping.JoinItemInfo);
					this.relateHash.Add (relateMapping);
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
			return this.relateHash.Contains (relationMapping);
		}
	}
}

