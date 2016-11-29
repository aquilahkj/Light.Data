using System;
using System.Collections.Generic;

namespace Light.Data
{
	class AggregateGroup
	{
		public AggregateGroup (DataEntityMapping entityMapping, AggregateMapping aggregateMapping)
		{
			_entityMapping = entityMapping;
			_aggregateMapping = aggregateMapping;
		}

		DataEntityMapping _entityMapping;

		AggregateMapping _aggregateMapping;

		public AggregateMapping AggregateMapping {
			get {
				return _aggregateMapping;
			}
		}

		public DataEntityMapping EntityMapping {
			get {
				return _entityMapping;
			}
		}

		Dictionary<string, AggregateDataFieldInfo> _aggregateDict = new Dictionary<string, AggregateDataFieldInfo> ();

		public void AddGroupByField (string name, DataFieldInfo fieldInfo)
		{
			AggregateDataFieldInfo agg = new AggregateDataFieldInfo (fieldInfo, name, false);
			_aggregateDict.Add (name, agg);
		}

		public void AddAggregateField (string name, DataFieldInfo fieldInfo)
		{
			AggregateDataFieldInfo agg = new AggregateDataFieldInfo (fieldInfo, name, true);
			_aggregateDict.Add (name, agg);
		}

		public DataFieldInfo GetAggregateData (string name)
		{
			AggregateDataFieldInfo info;
			if (_aggregateDict.TryGetValue (name, out info)) {
				return info.FieldInfo;
			}
			else {
				return null;
			}
		}

		public bool CheckName (string name)
		{
			return _aggregateDict.ContainsKey (name);
		}

		public AggregateDataFieldInfo [] GetAggregateDataFieldInfos ()
		{
			AggregateDataFieldInfo [] array = new AggregateDataFieldInfo [_aggregateDict.Count];
			int i = 0;
			foreach (AggregateDataFieldInfo item in _aggregateDict.Values) {
				array [i] = item;
				i++;
			}
			return array;
		}
	}
}

