using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	class DynamicMultiDataMapping : DataMapping
	{
		public static DynamicMultiDataMapping CreateDynamicMultiDataMapping (Type type, List<JoinModel> models)
		{
			Tuple<string, DataEntityMapping> [] array = new Tuple<string, DataEntityMapping> [models.Count];
			for (int i = 0; i < models.Count; i++) {
				JoinModel model = models [i];
				Tuple<string, DataEntityMapping> tuple = new Tuple<string, DataEntityMapping> (model.AliasTableName, model.Mapping);
				array [i] = tuple;
			}
			DynamicMultiDataMapping mapping = new DynamicMultiDataMapping (type, array);
			return mapping;
		}

		DataEntityMapping [] mappings;

		string [] aliasNames;

		public DynamicMultiDataMapping (Type type, Tuple<string, DataEntityMapping> [] targetMappings)
			: base (type)
		{
			mappings = new DataEntityMapping [targetMappings.Length];
			aliasNames = new string [targetMappings.Length];
			for (int i = 0; i < targetMappings.Length; i++) {
				aliasNames [i] = targetMappings [i].Item1;
				mappings [i] = targetMappings [i].Item2;
			}
		}

		public override object InitialData ()
		{
			object [] objects = new object [mappings.Length];
			for (int i = 0; i < mappings.Length; i++) {
				objects [i] = mappings [i].InitialData ();
			}
			return objects;
		}

		public override object LoadData (DataContext context, IDataReader datareader, object state)
		{
			QueryState queryState = state as QueryState;
			if (queryState == null) {
				throw new LightDataException ("");
			}
			object [] objects = new object [mappings.Length];
			for (int i = 0; i < mappings.Length; i++) {
				string aliasName = aliasNames [i];
				objects [i] = mappings [i].LoadJoinTableData (context, datareader, queryState, aliasName);
			}
			return objects;
		}
	}
}

