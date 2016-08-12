﻿using System;
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

		readonly List<RelationLink> linkList = new List<RelationLink> ();

		readonly Dictionary<string, string> cycleDict = new Dictionary<string, string> ();

		readonly Dictionary<string, string []> collectionDict = new Dictionary<string, string []> ();

		readonly Dictionary<string, string []> singleDict = new Dictionary<string, string []> ();

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
				string aliasName = string.Format ("{0}_{1}", rootItem.AliasName, info.FieldName);
				AliasDataFieldInfo alias = new AliasDataFieldInfo (info, aliasName);
				alias.AliasTableName = rootItem.AliasName;
				joinSelector.SetAliasDataField (alias);
				rootInfoList.Add (alias);
				fieldInfoDict.Add (string.Format ("{0}.{1}", items [0].CurrentFieldPath, field.IndexName), alias);
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
					fieldInfoDict.Add (string.Format ("{0}.{1}", string.Empty, fieldMapping.IndexName), field);
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

		void LoadEntityMapping (DataEntityMapping mapping, RelationLink link)
		{
			string path = link != null ? link.LastFieldPath : string.Empty;
			SingleRelationFieldMapping [] relateFielsMappings = mapping.GetSingleJoinTableRelationFieldMappings ();
			foreach (SingleRelationFieldMapping relateFieldMapping in relateFielsMappings) {
				relateFieldMapping.InitialRelation ();
				bool add = false;
				if (link == null) {
					RelationLink mlink = new RelationLink (relateFieldMapping, string.Empty);
					linkList.Add (mlink);
					LoadEntityMapping (relateFieldMapping.RelateMapping, mlink);
					add = true;
				}
				else {
					RelationLink flink = link.Fork ();
					RelationLinkType linkType = flink.TryAddField (relateFieldMapping);
					if (linkType == RelationLinkType.NoMatch) {
						RelationLink mlink = new RelationLink (relateFieldMapping, link.LastFieldPath);
						linkList.Add (mlink);
						LoadEntityMapping (relateFieldMapping.RelateMapping, mlink);
						add = true;
					}
					else if (linkType == RelationLinkType.AddLink) {
						linkList.Add (flink);
						LoadEntityMapping (relateFieldMapping.RelateMapping, flink);
						add = true;
					}
					else {
						cycleDict.Add (flink.LastFieldPath, flink.CycleFieldPath);
					}
				}
				if (add) {
					RelationKey [] kps = relateFieldMapping.GetKeyPairs ();
					string [] relates = new string [kps.Length];
					for (int i = 0; i < kps.Length; i++) {
						relates [i] = string.Format ("{0}.{1}.{2}", path, relateFieldMapping.FieldName, kps [i].RelateKey);
					}
					string relate = string.Format ("{0}.{1}", path, relateFieldMapping.FieldName);
					singleDict [relate] = relates;
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
				collectionDict [collectField] = masters;
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

		public bool CheckIsField (string path)
		{
			return fieldInfoDict.ContainsKey (path);
		}

		public bool CheckIsRelateEntity (string path)
		{
			return tableInfoDict.ContainsKey (path);
		}

		public bool CheckIsEntityCollection (string path)
		{
			return collectionDict.ContainsKey (path);
		}

		public DataFieldInfo CreateFieldInfoForField (string path)
		{
			DataFieldInfo info;
			if (fieldInfoDict.TryGetValue (path, out info)) {
				return info.Clone () as DataFieldInfo;
			}
			else {
				throw new LightDataException ("");
			}
		}

		DataFieldInfo GetFieldInfoForField (string path)
		{
			DataFieldInfo info;
			if (fieldInfoDict.TryGetValue (path, out info)) {
				return info;
			}
			else {
				throw new LightDataException ("");
			}
		}


		//public DataFieldInfo [] GetFieldInfoForCollectionField (string path)
		//{
		//	string [] fields;
		//	if (collectionDict.TryGetValue (path, out fields)) {
		//		DataFieldInfo [] infos = new DataFieldInfo [fields.Length];
		//		for (int i = 0; i < fields.Length; i++) {
		//			infos [i] = GetFieldInfoForField (fields [i]);
		//		}
		//		return infos;
		//	}
		//	else {
		//		throw new LightDataException ("");
		//	}
		//}

		DataFieldInfo [] GetFieldInfoForSingleField (string path)
		{
			string [] fields;
			if (singleDict.TryGetValue (path, out fields)) {
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

		string [] RewritePaths (string [] paths)
		{
			if (collectionDict.Count > 0) {
				HashSet<string> ss = new HashSet<string> (paths);
				foreach (string path in paths) {
					string [] arr;
					if (collectionDict.TryGetValue (path, out arr)) {
						foreach (string item in arr) {
							ss.Add (item);
						}
					}
				}
				string [] newpaths = new string [ss.Count];
				ss.CopyTo (newpaths);
				return newpaths;
			}
			else {
				return paths;
			}
		}

		public ISelector CreateSpecialSelector (params string [] paths)
		{
			paths = RewritePaths (paths);
			HashSet<DataFieldInfo> hash = new HashSet<DataFieldInfo> ();
			HashSet<string> stable = new HashSet<string> ();
			foreach (string path in paths) {
				DataFieldInfo info;
				if (fieldInfoDict.TryGetValue (path, out info)) {
					if (!hash.Contains (info)) {
						hash.Add (info);
						int index = path.LastIndexOf ('.');
						if (index > 0) {
							string t = path.Substring (0, index);
							if (!stable.Contains (t)) {
								stable.Add (t);
								DataFieldInfo [] sinfos = GetFieldInfoForSingleField (t);
								foreach (DataFieldInfo sinfo in sinfos) {
									if (!hash.Contains (sinfo)) {
										hash.Add (sinfo);
									}
								}
							}
						}
					}
					continue;
				}

				DataFieldInfo [] tinfos;
				if (tableInfoDict.TryGetValue (path, out tinfos)) {
					foreach (DataFieldInfo tinfo in tinfos) {
						stable.Add (path);
						if (!hash.Contains (tinfo)) {
							hash.Add (tinfo);
						}
					}
					continue;
				}


				throw new LightDataException ("");
			}
			if (rootMapping.HasJoinRelateModel) {
				JoinSelector jselector = new JoinSelector ();
				foreach (AliasDataFieldInfo finfo in hash) {
					jselector.SetAliasDataField (finfo);
				}
				return jselector;
			}
			else {
				Selector nselector = new Selector ();
				foreach (DataFieldInfo finfo in hash) {
					nselector.SetDataField (finfo);
				}
				return nselector;
			}
		}

	}
}
