using System;
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

		List<RelationCycle> totalCycle = new List<RelationCycle> ();

		int index;

		readonly JoinSelector selector = new JoinSelector ();

		readonly List<JoinModel> models = new List<JoinModel> ();

		readonly Dictionary<DataEntityMapping,string> entityInfoDict = new Dictionary<DataEntityMapping, string> ();

		readonly string rootAliasName;

		public string RootAliasName {
			get {
				return rootAliasName;
			}
		}

		public RelationMap (DataEntityMapping rootMapping)
		{
			this.rootMapping = rootMapping;
			this.rootAliasName = "T" + index;
			this.entityInfoDict.Add (this.rootMapping, this.rootAliasName);
			foreach (DataFieldMapping field in this.rootMapping.FieldMappings) {
				DataFieldInfo info = new DataFieldInfo (field);
				AliasDataFieldInfo alias = new AliasDataFieldInfo (info, string.Format ("{0}_{1}", this.rootAliasName, info.FieldName));
				alias.AliasTableName = this.rootAliasName;
				this.selector.SetAliasDataField (alias);
			}

			LoadEntityMapping (this.rootMapping, null);
			foreach (RelationCycle cycle in totalCycle) {
				SingleRelationFieldMapping[] fieldMappings = cycle.GetSingleRelationFieldMapping ();
				foreach (SingleRelationFieldMapping fieldMapping in fieldMappings) {
					DataEntityMapping mapping = fieldMapping.RelateMapping;
					DataFieldExpression expression = null;
					DataFieldInfoRelation[] relations = fieldMapping.GetDataFieldInfoRelations ();
					string malias = this.entityInfoDict [fieldMapping.MasterMapping];
					string ralias = this.entityInfoDict [fieldMapping.RelateMapping];
					foreach (DataFieldInfoRelation relation in relations) {
						DataFieldInfo minfo = relation.MasterInfo;
						minfo.AliasTableName = malias;
						DataFieldInfo rinfo = relation.RelateInfo;
						rinfo.AliasTableName = ralias;
						expression = DataFieldExpression.And (expression, minfo == rinfo);
					}

					foreach (DataFieldMapping field in mapping.FieldMappings) {
						DataFieldInfo info = new DataFieldInfo (field);
						AliasDataFieldInfo alias = new AliasDataFieldInfo (info, string.Format ("{0}_{1}", ralias, info.FieldName));
						alias.AliasTableName = ralias;
						this.selector.SetAliasDataField (alias);
					}

					JoinConnect connect = new JoinConnect (JoinType.LeftJoin, expression);
					JoinModel model = new JoinModel (mapping, connect, null, null);
					model.AliasTableName = ralias;
					this.models.Add (model);
				}
			}
		}

		public JoinCapsule CreateJoinCapsule (QueryExpression query, OrderExpression order)
		{
			List<JoinModel> models1 = new List<JoinModel> ();

			JoinModel model1 = new JoinModel (rootMapping, null, query, order);
			model1.AliasTableName = rootAliasName;
			models1.Add (model1);

			models1.AddRange (this.models);
			JoinCapsule clone = new JoinCapsule (this.selector, models1, this);
			return clone;
		}

		Dictionary<SingleRelationFieldMapping,RelationCycle> fieldInfoDict = new Dictionary<SingleRelationFieldMapping, RelationCycle> ();

		void LoadEntityMapping (DataEntityMapping mapping, RelationCycle cycle)
		{
			List<RelationCycle> levelCycle = new List<RelationCycle> ();
			SingleRelationFieldMapping[] relateFielsMappings = mapping.GetSingleJoinTableRelationFieldMappings ();
			foreach (SingleRelationFieldMapping relateFieldMapping in relateFielsMappings) {
				relateFieldMapping.InitialRelation ();
				bool exists = this.entityInfoDict.ContainsKey (relateFieldMapping.RelateMapping);
				RelationCycle mycycle = null;
				if (cycle != null && cycle.TryAddCycle (relateFieldMapping, exists)) {
					mycycle = cycle;
				}
				if (mycycle == null) {
					foreach (RelationCycle cycleItem in levelCycle) {
						if (cycleItem.TryAddCycle (relateFieldMapping, exists)) {
							mycycle = cycleItem;
							break;
						}
					}
				}

				if (mycycle == null) {
					if (!exists) {
						mycycle = new RelationCycle (relateFieldMapping);
						levelCycle.Add (mycycle);
						this.totalCycle.Add (mycycle);
						index++;
						this.entityInfoDict.Add (relateFieldMapping.RelateMapping, "T" + index);
						this.fieldInfoDict.Add (relateFieldMapping, mycycle);
						LoadEntityMapping (relateFieldMapping.RelateMapping, mycycle);
					}
				}
				else {
					this.fieldInfoDict.Add (relateFieldMapping, mycycle);
					if (!exists) {
						index++;
						this.entityInfoDict.Add (relateFieldMapping.RelateMapping, "T" + index);
						LoadEntityMapping (relateFieldMapping.RelateMapping, mycycle);
					}
				}
			}
		}

		public bool CheckValid (SingleRelationFieldMapping relationMapping, out string aliasName)
		{
			if (this.fieldInfoDict.ContainsKey (relationMapping)) {
				aliasName = this.entityInfoDict [relationMapping.RelateMapping];
				return true;
			}
			else {
				aliasName = null;
				return false;
			}
		}
	}
}

