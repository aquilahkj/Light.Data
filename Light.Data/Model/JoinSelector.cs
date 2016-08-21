using System;
using System.Collections.Generic;

namespace Light.Data
{
	class JoinSelector : ISelector
	{
		Dictionary<string, DataFieldInfo> infoDict = new Dictionary<string, DataFieldInfo> ();

		HashSet<string> aliasTableHash = new HashSet<string> ();

		public void SetDataEntity (DataEntityMapping entityMapping)
		{
			if (entityMapping == null)
				throw new ArgumentNullException (nameof (entityMapping));
			foreach (DataFieldMapping fieldMapping in entityMapping.DataEntityFields) {
				if (fieldMapping != null) {
					DataFieldInfo field = new DataFieldInfo (fieldMapping);
					AliasDataFieldInfo aliasField = new AliasDataFieldInfo (field, field.FieldName);
					this.infoDict [aliasField.Alias] = aliasField;
				}
			}
		}

		public void SetDataField (DataFieldInfo field)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException (nameof (field));
			AliasDataFieldInfo aliasField = new AliasDataFieldInfo (field, field.FieldName);
			this.infoDict [aliasField.Alias] = aliasField;
			if (field.AliasTableName != null) {
				aliasTableHash.Add (field.AliasTableName);
			}
		}

		public void SetAliasDataField (AliasDataFieldInfo aliasField)
		{
			if (Object.Equals (aliasField, null))
				throw new ArgumentNullException (nameof (aliasField));
			this.infoDict [aliasField.Alias] = aliasField;
			if (aliasField.AliasTableName != null) {
				aliasTableHash.Add (aliasField.AliasTableName);
			}
		}

		//public List<DataFieldInfo> GetFieldInfos ()
		//{
		//	//if (infoList.Count > 0) {
		//	//	List<DataFieldInfo> infos = new List<DataFieldInfo> (this.infoList);
		//	//	return infos;
		//	//}
		//	//else {
		//	List<DataFieldInfo> infos = new List<DataFieldInfo> (this.infoDict.Values);
		//	return infos;
		//	//}
		//}

		/// <summary>
		/// Clones the with except DataEntityMapping,bucause mpping will referer the owin.
		/// </summary>
		/// <returns>The with except clone.</returns>
		/// <param name="exceptMappings">Except mappings.</param>
		//internal JoinSelector CloneWithExcept (DataEntityMapping [] exceptMappings)
		//{
		//	JoinSelector target = new JoinSelector ();
		//	foreach (KeyValuePair<string, DataFieldInfo> kv in this.infoDict) {
		//		DataEntityMapping mapping = kv.Value.TableMapping;
		//		bool isexcept = false;
		//		if (exceptMappings != null && exceptMappings.Length > 0) {
		//			foreach (DataEntityMapping exceptMapping in exceptMappings) {
		//				if (exceptMapping == mapping) {
		//					isexcept = true;
		//					break;
		//				}
		//			}
		//		}
		//		if (!isexcept) {
		//			target.infoDict [kv.Key] = kv.Value;
		//		}
		//	}
		//	return target;
		//}

		/// <summary>
		/// Creates the select string.
		/// </summary>
		/// <returns>The select string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="dataParameters">Data parameters.</param>
		public string CreateSelectString (CommandFactory factory, out DataParameter [] dataParameters)
		{
			string [] selectList = new string [this.infoDict.Count];
			int index = 0;
			List<DataParameter> innerParameters = null;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				AliasDataFieldInfo aliasInfo = fieldInfo as AliasDataFieldInfo;
				DataParameter [] dataParameters1 = null;
				if (!Object.Equals (aliasInfo, null)) {
					selectList [index] = aliasInfo.CreateAliasDataFieldSql (factory, true, out dataParameters1);
				}
				else {
					selectList [index] = fieldInfo.CreateSqlString (factory, true, out dataParameters1);
				}
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

		/// <summary>
		/// Gets the select filed names.
		/// </summary>
		/// <returns>The select filed names.</returns>
		public string [] GetSelectFiledNames ()
		{
			string [] fileds = new string [this.infoDict.Count + aliasTableHash.Count];
			int index = 0;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				AliasDataFieldInfo aliasInfo = fieldInfo as AliasDataFieldInfo;
				if (!Object.Equals (aliasInfo, null)) {
					fileds [index] = aliasInfo.Alias;
				}
				else {
					fileds [index] = fieldInfo.FieldName;
				}
				index++;
			}
			foreach (string alias in aliasTableHash) {
				fileds [index] = alias;
				index++;
			}
			return fileds;
		}

		public string CreateSelectString (CommandFactory factory, CreateSqlState state)
		{
			string [] selectList = new string [this.infoDict.Count];
			int index = 0;
			foreach (DataFieldInfo fieldInfo in this.infoDict.Values) {
				IAliasDataFieldInfo aliasInfo = fieldInfo as IAliasDataFieldInfo;
				if (!Object.Equals (aliasInfo, null)) {
					selectList [index] = aliasInfo.CreateAliasDataFieldSql (factory, true, state);
				}
				else {
					selectList [index] = fieldInfo.CreateSqlString (factory, true, state);
				}
				index++;
			}
			string customSelect = string.Join (",", selectList);
			return customSelect;
		}
	}
}

