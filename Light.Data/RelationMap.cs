using System;
using System.Collections.Generic;

namespace Light.Data
{
	class RelationMap
	{
		readonly DataEntityMapping masterMapping;

		public DataEntityMapping MasterMapping {
			get {
				return masterMapping;
			}
		}

		readonly List<JoinItem> itemList = new List<JoinItem> ();

		readonly HashSet<SingleRelationFieldMapping> referHash = new HashSet<SingleRelationFieldMapping> ();

		readonly HashSet<string> entityHash = new HashSet<string> ();

		public RelationMap (DataEntityMapping masterMapping)
		{
			this.masterMapping = masterMapping;
			this.entityHash.Add (masterMapping.TableName);
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
//						else {
//							string d = relateMapping.FieldName;
//						}
					}
				}
				else {
					this.entityHash.Add (relateMapping.RelateMapping.TableName);
					this.itemList.Add (relateMapping.JoinItemInfo);
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

