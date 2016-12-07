using System;
using System.Collections.Generic;

namespace Light.Data
{
	class AggregateModel
	{
		public AggregateModel (DataEntityMapping entityMapping, CustomMapping aggregateMapping)
		{
			_entityMapping = entityMapping;
			_outputMapping = aggregateMapping;
		}

		DataEntityMapping _entityMapping;

		CustomMapping _outputMapping;

		public CustomMapping OutputMapping {
			get {
				return _outputMapping;
			}
		}

		public DataEntityMapping EntityMapping {
			get {
				return _entityMapping;
			}
		}

		readonly Dictionary<string, AggregateDataFieldInfo> _aggregateDict = new Dictionary<string, AggregateDataFieldInfo> ();

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
			_aggregateDict.Values.CopyTo (array, 0);
			return array;
		}
	}
}

