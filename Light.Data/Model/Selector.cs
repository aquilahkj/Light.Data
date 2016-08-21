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

		protected Dictionary<string, DataFieldInfo> infoDict = new Dictionary<string, DataFieldInfo> ();

		public virtual void SetDataField (DataFieldInfo field)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException (nameof (field));
			this.infoDict [field.FieldName] = field;
		}

		public virtual string CreateSelectString (CommandFactory factory, out DataParameter [] dataParameters)
		{
			string [] selectList = new string [this.infoDict.Count];
			int index = 0;
			List<DataParameter> innerParameters = null;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				DataParameter [] dataParameters1 = null;
				selectList [index] = fieldInfo.CreateSqlString (factory, true, out dataParameters1);
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

		public virtual string [] GetSelectFiledNames ()
		{
			string [] fileds = new string [this.infoDict.Count];
			int index = 0;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				fileds [index] = fieldInfo.FieldName;
				index++;
			}
			return fileds;
		}

		public static JoinSelector ComposeSelector (Dictionary<string, Selector> selectors)
		{
			JoinSelector joinSelector = new JoinSelector ();
			foreach (KeyValuePair<string, Selector> selector in selectors) {
				foreach (KeyValuePair<string, DataFieldInfo> kvs in selector.Value.infoDict) {
					DataFieldInfo info = kvs.Value.Clone () as DataFieldInfo;
					string aliasName = string.Format ("{0}_{1}", selector.Key, info.FieldName);
					AliasDataFieldInfo alias = new AliasDataFieldInfo (info, aliasName);
					alias.AliasTableName = selector.Key;
					joinSelector.SetAliasDataField (alias);
				}
			}
			return joinSelector;
		}

		public string CreateSelectString (CommandFactory factory, CreateSqlState state)
		{
			string [] selectList = new string [this.infoDict.Count];
			int index = 0;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				selectList [index] = fieldInfo.CreateSqlString (factory, true, state);
				index++;
			}
			string customSelect = string.Join (",", selectList);
			return customSelect;
		}
	}
}

