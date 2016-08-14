using System;
using System.Data;

namespace Light.Data
{
	class DynamicMultiDataMapping : DataMapping
	{
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

