using System;
using System.Collections.Generic;
using System.Collections;

namespace Light.Data
{
	class RelationContent
	{
		public RelationContent ()
		{
			
		}

		SingleRelationFieldMapping collectionRelateReferFieldMapping = null;

		public SingleRelationFieldMapping CollectionRelateReferFieldMapping {
			get {
				return collectionRelateReferFieldMapping;
			}
		}

		object collectionRelateReferFieldValue = null;

		public object CollectionRelateReferFieldValue {
			get {
				return collectionRelateReferFieldValue;
			}
		}

		RelationMap relationMap;

		readonly Dictionary<DataEntityMapping,object> joinDatas = new Dictionary<DataEntityMapping, object> ();

		readonly Dictionary<DataEntityMapping,Hashtable> queryDatas = new Dictionary<DataEntityMapping, Hashtable> ();

		//		public bool GetCollectionValue (SingleRelationFieldMapping collectionFieldName, out object value)
		//		{
		//			if (this.collectionRelateReferFieldMapping != null && this.collectionRelateReferFieldMapping == collectionFieldName) {
		//				value = this.collectionRelateReferFieldValue;
		//				return true;
		//			}
		//			else {
		//				value = null;
		//				return false;
		//			}
		//		}

		public void SetCollectionValue (SingleRelationFieldMapping collectionFieldName, object value)
		{
			this.collectionRelateReferFieldMapping = collectionFieldName;
			this.collectionRelateReferFieldValue = value;
		}

		public bool GetQueryData (DataEntityMapping mapping, object key, out object value)
		{
			value = null;
			Hashtable table;
			if (!queryDatas.TryGetValue (mapping, out table)) {
				return false;
			}
			if (table.Contains (key)) {
				value = table [key];
				return true;
			}
			else {
				return false;
			}
		}

		public void SetQueryData (DataEntityMapping mapping, object key, object value)
		{
			Hashtable table;
			if (!queryDatas.TryGetValue (mapping, out table)) {
				table = new Hashtable ();
				queryDatas.Add (mapping, table);
			}
			table.Add (key, value);
		}

		public void InitialJoinData ()
		{
			this.joinDatas.Clear ();
		}

		public void SetRelationMap (RelationMap relationMap)
		{
			this.relationMap = relationMap;
		}

		public void SetJoinMasterData (DataEntityMapping mapping, object value)
		{
			if (mapping == relationMap.MasterMapping) {
				joinDatas [mapping] = value;
			}
		}

		public void SetJoinData (SingleRelationFieldMapping mapping, object value)
		{
			if (!joinDatas.ContainsKey (mapping.RelateMapping)) {
				joinDatas [mapping.RelateMapping] = value;
			}
		}

		public bool CheckJoinData (SingleRelationFieldMapping mapping)
		{
			return this.relationMap.CheckValid (mapping);
		}

		public bool GetJoinData (SingleRelationFieldMapping mapping, out object value)
		{
			if (joinDatas.TryGetValue (mapping.RelateMapping, out value)) {
				return true;
			}
			else {
				return false;
			}
		}

	}
}

