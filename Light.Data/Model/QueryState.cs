using System.Collections.Generic;
using System.Collections;

namespace Light.Data
{
	class QueryState
	{
		RelationMap relationMap;

		readonly Dictionary<string, object> joinDatas = new Dictionary<string, object> ();

		readonly Dictionary<string, object> extendDatas = new Dictionary<string, object> ();

		HashSet<string> fieldHash = null;

		public void InitialJoinData ()
		{
			this.joinDatas.Clear ();
			if (this.extendDatas.Count > 0) {
				foreach (KeyValuePair<string, object> kvs in this.extendDatas) {
					joinDatas.Add (kvs.Key, kvs.Value);
				}
			}
		}

		public void SetRelationMap (RelationMap relationMap)
		{
			this.relationMap = relationMap;
		}


		public void SetSelector (ISelector selector)
		{
			if (selector != null) {
				this.fieldHash = new HashSet<string> (selector.GetSelectFieldNames ());
			}
		}

		public void SetExtendData (string fieldPath, object value)
		{
			extendDatas [fieldPath] = value;
		}

		public void SetJoinData (string fieldPath, object value)
		{
			joinDatas [fieldPath] = value;
		}

		public bool GetJoinData (string fieldPath, out object value)
		{
			string m;
			if (relationMap.TryGetCycleFieldPath (fieldPath, out m)) {
				return joinDatas.TryGetValue (m, out value);
			}
			else {
				return joinDatas.TryGetValue (fieldPath, out value);
			}
		}

		public string GetAliasName (string fieldPath)
		{
			string alias;
			if (this.relationMap.CheckValid (fieldPath, out alias)) {
				return alias;
			}
			else {
				throw new LightDataException (string.Format (RE.CanNotFindAliasNameViaSpecialPath, fieldPath));
			}
		}

		public bool CheckSelectField (string fieldName)
		{
			if (fieldHash != null) {
				return fieldHash.Contains (fieldName);
			}
			else {
				return true;
			}
		}
	}
}

