using System;
using System.Collections.Generic;

namespace Light.Data
{
	class AggregateModel
	{
		//Dictionary<string, AggregateDataInfo> _dataFieldInfoDictionary = new Dictionary<string, AggregateDataInfo> ();

		//Dictionary<string, AggregateDataInfo> _aggregateFunctionDictionary = new Dictionary<string, AggregateDataInfo> ();

		Dictionary<string, AggregateData> _aggregateDict = new Dictionary<string, AggregateData> ();

		List<AggregateDataInfo> _fieldInfoList = new List<AggregateDataInfo> ();

		List<AggregateDataInfo> _aggregateFunctionList = new List<AggregateDataInfo> ();

		public AggregateModel (AggregateMapping mapping)
		{
			_mapping = mapping;
		}

		AggregateMapping _mapping;

		public AggregateMapping Mapping {
			get {
				return _mapping;
			}
		}

		internal List<AggregateDataInfo> FieldInfoList {
			get {
				return _fieldInfoList;
			}
		}

		internal List<AggregateDataInfo> AggregateFunctionList {
			get {
				return _aggregateFunctionList;
			}
		}

		public void AddGroupByField (string name, DataFieldInfo fieldInfo)
		{
			AggregateDataFieldInfo dataFieldInfo = new AggregateDataFieldInfo (fieldInfo);
			AggregateDataInfo data = new AggregateDataInfo (dataFieldInfo, name);
			_fieldInfoList.Add (data);
			_aggregateDict.Add (name, dataFieldInfo);
		}

		public void AddFunction (string name, AggregateData function)
		{
			AggregateDataInfo info = new AggregateDataInfo (function, name);
			_aggregateFunctionList.Add (info);
			_aggregateDict.Add (name, function);
		}

		public AggregateData GetAggregateData (string name)
		{
			//AggregateData data;
			//if(
			return _aggregateDict [name];
		}
	}
}

