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

		readonly Dictionary<DataEntityMapping,object> joinDatas = new Dictionary<DataEntityMapping, object> ();

		readonly Dictionary<DataEntityMapping,Hashtable> queryDatas = new Dictionary<DataEntityMapping, Hashtable> ();

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

		public bool GetJoinData (DataEntityMapping mapping, out object value)
		{
			return joinDatas.TryGetValue (mapping, out value);
		}

		public void SetJoinData (DataEntityMapping mapping, object value)
		{
			joinDatas [mapping] = value;
		}
	}
}

