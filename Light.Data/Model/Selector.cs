using System;
using System.Collections.Generic;

namespace Light.Data
{
	class Selector : ISelector
	{
		//public Selector (DataEntityMapping entityMapping)
		//{
		//	if (entityMapping == null)
		//		throw new ArgumentNullException (nameof (entityMapping));
		//	foreach (DataFieldMapping fieldMapping in entityMapping.DataEntityFields) {
		//		if (fieldMapping != null) {
		//			DataFieldInfo field = new DataFieldInfo (fieldMapping);
		//			this.infoDict [field.FieldName] = field;
		//		}
		//	}
		//}

		Dictionary<string, DataFieldInfo> infoDict = new Dictionary<string, DataFieldInfo> ();

		public void SetDataField (DataFieldInfo field)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException (nameof (field));
			this.infoDict [field.FieldName] = field;
		}

		public string CreateSelectString (CommandFactory factory, out DataParameter [] dataParameters)
		{
			string [] selectList = new string [this.infoDict.Count];
			int index = 0;
			List<DataParameter> innerParameters = null;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				DataParameter [] dataParameters1 = null;
				selectList [index] = fieldInfo.CreateDataFieldSql (factory, true, out dataParameters1);
				if (dataParameters1 != null && dataParameters1.Length > 0) {
					if (innerParameters == null) {
						innerParameters = new List<DataParameter> ();
					}
					innerParameters.AddRange (dataParameters1);
				}
				index++;
			}
			string customSelect = string.Join (",", selectList);
			dataParameters = innerParameters != null ? innerParameters.ToArray () : null;
			return customSelect;
		}
	}
}

