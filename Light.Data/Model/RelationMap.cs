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

		ISelector selector;

		readonly List<JoinModel> models = new List<JoinModel> ();

		readonly Dictionary<string, RelationItem> mapDict = new Dictionary<string, RelationItem> ();

		readonly Dictionary<string, DataFieldInfo []> tableInfoDict = new Dictionary<string, DataFieldInfo []> ();

		readonly Dictionary<string, DataFieldInfo> fieldInfoDict = new Dictionary<string, DataFieldInfo> ();

		void LoadJoinRelate ()
		{
			LoadEntityMapping (this.rootMapping, null);
			JoinSelector joinSelector = new JoinSelector ();
			List<RelationItem> items = new List<RelationItem> ();
			Dictionary<string, RelationItem> relationItemDict = new Dictionary<string, RelationItem> ();
			int tindex = 0;
			foreach (RelationLink link in linkList) {
				RelationItem [] sitems = link.GetRelationItems ();
				foreach (RelationItem item in sitems) {
					if (!items.Contains (item)) {
						if (item.FieldMapping == null && tindex != 0) {
							continue;
						}
						item.AliasName = "T" + tindex;
						tindex++;
						items.Add (item);
						relationItemDict.Add (item.CurrentFieldPath, item);
					}
				}
			}
			if (this.rootMapping != items [0].DataMapping) {
				throw new LightDataException ("");
			}

			RelationItem rootItem = items [0];
			mapDict.Add (items [0].CurrentFieldPath, items [0]);
			List<DataFieldInfo> rootInfoList = new List<DataFieldInfo> ();
			foreach (DataFieldMapping field in this.rootMapping.FieldMappings) {
				DataFieldInfo info = new DataFieldInfo (field);
				//info.AliasTableName = this.rootAliasName;
				//joinSelector.SetInnerDataField (info);
				string aliasName = string.Format ("{0}_{1}", rootItem.AliasName, info.FieldName);
				AliasDataFieldInfo alias = new AliasDataFieldInfo (info, aliasName);
				alias.AliasTableName = rootItem.AliasName;
				joinSelector.SetAliasDataField (alias);
				rootInfoList.Add (alias);
				fieldInfoDict.Add (string.Format ("{0}.{1}", items [0].CurrentFieldPath, field.Name), alias);
			}
			tableInfoDict.Add (items [0].CurrentFieldPath, rootInfoList.ToArray ());

			for (int i = 1; i < items.Count; i++) {
				RelationItem mitem = items [i];
				mapDict.Add (mitem.CurrentFieldPath, mitem);
				SingleRelationFieldMapping fieldMapping = mitem.FieldMapping;
				DataEntityMapping mapping = fieldMapping.RelateMapping;
				DataFieldExpression expression = null;
				DataFieldInfoRelation [] relations = fieldMapping.GetDataFieldInfoRelations ();

				RelationItem ritem = mapDict [mitem.PrevFieldPath];
				string malias = ritem.AliasName;
				string ralias = mitem.AliasName;
				foreach (DataFieldInfoRelation relation in relations) {
					DataFieldInfo minfo = relation.MasterInfo;
					minfo.AliasTableName = malias;
					DataFieldInfo rinfo = relation.RelateInfo;
					rinfo.AliasTableName = ralias;
					expression = DataFieldExpression.And (expression, minfo == rinfo);
				}
				List<DataFieldInfo> infoList = new List<DataFieldInfo> ();
				foreach (DataFieldMapping field in mapping.FieldMappings) {
					DataFieldInfo info = new DataFieldInfo (field);
					//info.AliasTableName = ralias;
					//joinSelector.SetInnerDataField (info);
					string aliasName = string.Format ("{0}_{1}", ralias, info.FieldName);
					AliasDataFieldInfo alias = new AliasDataFieldInfo (info, aliasName);
					alias.AliasTableName = ralias;
					joinSelector.SetAliasDataField (alias);
					infoList.Add (alias);
					fieldInfoDict.Add (string.Format ("{0}.{1}", mitem.CurrentFieldPath, field.Name), alias);
				}
				tableInfoDict.Add (mitem.CurrentFieldPath, infoList.ToArray ());

				JoinConnect connect = new JoinConnect (JoinType.LeftJoin, expression);
				JoinModel model = new JoinModel (mapping, ralias, connect, null, null);
				this.selector = joinSelector;
				this.models.Add (model);
			}
		}

		void LoadNormal ()
		{
			List<DataFieldInfo> rootInfoList = new List<DataFieldInfo> ();
			Selector dataSelector = new Selector ();
			foreach (DataFieldMapping fieldMapping in this.rootMapping.DataEntityFields) {
				if (fieldMapping != null) {
					DataFieldInfo field = new DataFieldInfo (fieldMapping);
					fieldInfoDict.Add (string.Format ("{0}.{1}", string.Empty, fieldMapping.Name), field);
					rootInfoList.Add (field);
					dataSelector.SetDataField (field);
				}
			}
			tableInfoDict.Add (string.Empty, rootInfoList.ToArray ());
			string path = string.Empty;
			CollectionRelationFieldMapping [] collectFieldMappings = rootMapping.GetCollectionRelationFieldMappings ();
			foreach (CollectionRelationFieldMapping collectFieldMapping in collectFieldMappings) {
				RelationKey [] kps = collectFieldMapping.GetKeyPairs ();
				string [] masters = new string [kps.Length];
				for (int i = 0; i < kps.Length; i++) {
					masters [i] = string.Format ("{0}.{1}", path, kps [i].MasterKey);
				}
				string collectField = string.Format ("{0}.{1}", path, collectFieldMapping.FieldName);
				collectionDict.Add (collectField, masters);
			}
			this.selector = dataSelector;
		}

		public RelationMap (DataEntityMapping rootMapping)
		{
			this.rootMapping = rootMapping;
			if (rootMapping.HasJoinRelateModel) {
				LoadJoinRelate ();
			}
			else {
				LoadNormal ();
			}
		}

		//public JoinCapsule CreateJoinCapsule (QueryExpression query, OrderExpression order)
		//{
		//	List<JoinModel> models1 = new List<JoinModel> ();

		//	JoinModel model1 = new JoinModel (rootMapping, "T0", null, query, order);
		//	//model1.AliasTableName = "T0";//rootAliasName;
		//	models1.Add (model1);

		//	models1.AddRange (this.models);
		//	JoinCapsule clone = new JoinCapsule (this.selector, models1);
		//	return clone;
		//}


		public List<JoinModel> CreateJoinModels (QueryExpression query, OrderExpression order)
		{
			if (!rootMapping.HasJoinRelateModel) {
				throw new LightDataException ("");
			}
			List<JoinModel> joinModels = new List<JoinModel> ();
			JoinModel model1 = new JoinModel (rootMapping, "T0", null, query, order);
			joinModels.Add (model1);
			joinModels.AddRange (this.models);
			return joinModels;
		}

		List<RelationLink> linkList = new List<RelationLink> ();

		Dictionary<string, string> cycleDict = new Dictionary<string, string> ();

		Dictionary<string, string []> collectionDict = new Dictionary<string, string []> ();

		void LoadEntityMapping (DataEntityMapping mapping, RelationLink link)
		{
			string path = link != null ? link.LastFieldPath : string.Empty;
			SingleRelationFieldMapping [] relateFielsMappings = mapping.GetSingleJoinTableRelationFieldMappings ();
			foreach (SingleRelationFieldMapping relateFieldMapping in relateFielsMappings) {
				relateFieldMapping.InitialRelation ();
				if (link == null) {
					RelationLink mlink = new RelationLink (relateFieldMapping, string.Empty);
					linkList.Add (mlink);
					LoadEntityMapping (relateFieldMapping.RelateMapping, mlink);
				}
				else {
					RelationLink flink = link.Fork ();
					RelationLinkType linkType = flink.TryAddField (relateFieldMapping);
					if (linkType == RelationLinkType.NoMatch) {
						RelationLink mlink = new RelationLink (relateFieldMapping, link.LastFieldPath);
						linkList.Add (mlink);
						LoadEntityMapping (relateFieldMapping.RelateMapping, mlink);
					}
					else if (linkType == RelationLinkType.AddLink) {
						linkList.Add (flink);
						LoadEntityMapping (relateFieldMapping.RelateMapping, flink);
					}
					else {
						cycleDict.Add (flink.LastFieldPath, flink.CycleFieldPath);
					}
				}
			}
			CollectionRelationFieldMapping [] collectFieldMappings = mapping.GetCollectionRelationFieldMappings ();
			foreach (CollectionRelationFieldMapping collectFieldMapping in collectFieldMappings) {
				RelationKey [] kps = collectFieldMapping.GetKeyPairs ();
				string [] masters = new string [kps.Length];
				for (int i = 0; i < kps.Length; i++) {
					masters [i] = string.Format ("{0}.{1}", path, kps [i].MasterKey);
				}
				string collectField = string.Format ("{0}.{1}", path, collectFieldMapping.FieldName);
				collectionDict.Add (collectField, masters);
			}
		}

		public bool CheckValid (string fieldPath, out string aliasName)
		{
			RelationItem item;
			bool ret = mapDict.TryGetValue (fieldPath, out item);
			if (ret) {
				aliasName = item.AliasName;
			}
			else {
				aliasName = null;
			}
			return ret;
		}

		public bool TryGetCycleFieldPath (string fieldPath, out string cycleFieldPath)
		{
			if (cycleDict.Count == 0) {
				cycleFieldPath = null;
				return false;
			}
			return cycleDict.TryGetValue (fieldPath, out cycleFieldPath);
		}

		public DataFieldInfo GetFieldInfoForField (string path)
		{
			DataFieldInfo info;
			if (fieldInfoDict.TryGetValue (path, out info)) {
				return info;
			}
			else {
				throw new LightDataException ("");
			}
		}

		public DataFieldInfo[] GetFieldInfoForCollectionField (string path)
		{
			string [] fields;
			if (collectionDict.TryGetValue (path, out fields)) {
				DataFieldInfo [] infos = new DataFieldInfo [fields.Length];
				for (int i = 0; i < fields.Length; i++) {
					infos [i] = GetFieldInfoForField (fields [i]);
				}
				return infos;
			}
			else {
				throw new LightDataException ("");
			}
		}

		public ISelector GetDefaultSelector ()
		{
			return selector;
		}

		//public DataFieldInfo[] GetFieldInfoForEntity (string path)
		//{
		//	DataFieldInfo[] infos;
		//	if (tableInfoDict.TryGetValue (path, out infos)) {
		//		return infos;
		//	}
		//	else {
		//		return null;
		//	}
		//}


		//public bool CheckValid (SingleRelationFieldMapping relationMapping, out string aliasName)
		//{
		//	if (this.fieldInfoDict.ContainsKey (relationMapping)) {
		//		aliasName = this.entityInfoDict [relationMapping.RelateMapping];
		//		return true;
		//	}
		//	else {
		//		aliasName = null;
		//		return false;
		//	}
		//}
	}
}

