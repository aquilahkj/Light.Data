using System;
using System.Collections.Generic;

namespace Light.Data
{
	class JoinSelector
	{
		Dictionary<string,DataFieldInfo> infoList = new Dictionary<string, DataFieldInfo> ();

		public JoinSelector ()
		{
			
		}

		public void SetDataEntity (DataEntityMapping entityMapping)
		{
			if (entityMapping == null)
				throw new ArgumentNullException ("entityMapping");
			foreach (DataFieldMapping fieldMapping in entityMapping.GetFieldMappings()) {
				if (fieldMapping == null) {
					continue;
				}
				DataFieldInfo info = new DataFieldInfo (fieldMapping);
				this.infoList [info.FieldName] = info;
			}
		}

		public void SetDataField (DataFieldInfo field)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException ("field");
			this.infoList [field.FieldName] = field;
		}

		public void SetAliasDataField (AliasDataFieldInfo field)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException ("field");
			this.infoList [field.Alias] = field;
		}

		public List<DataFieldInfo> GetFieldInfos()
		{
			List<DataFieldInfo> infos = new List<DataFieldInfo> (this.infoList.Values);
			return infos;
		}
	}
}

