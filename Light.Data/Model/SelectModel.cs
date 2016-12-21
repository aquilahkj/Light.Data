using System;
using System.Collections.Generic;

namespace Light.Data
{
	class SelectModel
	{
		public SelectModel (DataEntityMapping entityMapping, CustomMapping outputMapping)
		{
			_entityMapping = entityMapping;
			_ouputMapping = outputMapping;
		}

		DataEntityMapping _entityMapping;

		CustomMapping _ouputMapping;

		public CustomMapping OutputMapping {
			get {
				return _ouputMapping;
			}
		}

		public DataEntityMapping EntityMapping {
			get {
				return _entityMapping;
			}
		}

		readonly Dictionary<string, DataFieldInfo> _selectDict = new Dictionary<string, DataFieldInfo> ();

		public void AddSelectField (string name, DataFieldInfo fieldInfo)
		{
			if (name != fieldInfo.FieldName) {
				SpecialDataFieldInfo selectInfo = new SpecialDataFieldInfo (fieldInfo, name);
				_selectDict.Add (name, selectInfo);
			}
			else {
				_selectDict.Add (name, fieldInfo);
			}
		}

		public virtual DataFieldInfo [] GetDataFieldInfos ()
		{
			DataFieldInfo [] infos = new DataFieldInfo [_selectDict.Count];
			int index = 0;
			foreach (KeyValuePair<string, DataFieldInfo> kvp in _selectDict) {
				infos [index] = kvp.Value;
				index++;
			}
			return infos;
		}


		public DataFieldInfo GetFieldData (string name)
		{
			DataFieldInfo info;
			if (_selectDict.TryGetValue (name, out info)) {
				return info;
			}
			else {
				return null;
			}
		}

		public bool CheckName (string name)
		{
			return _selectDict.ContainsKey (name);
		}

		public Selector CreateSelector ()
		{
			SpecialSelector selecor = new SpecialSelector ();
			foreach (DataFieldInfo item in _selectDict.Values) {
				selecor.SetSelectField (item);
			}
			return selecor;
		}
	}
}
