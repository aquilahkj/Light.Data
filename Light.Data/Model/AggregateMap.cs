using System;
namespace Light.Data
{
	class AggregateMap : IMap
	{
		readonly AggregateGroup _group;

		public AggregateMap (AggregateGroup group)
		{
			this._group = group;
		}

		public Type Type {
			get {
				return _group.AggregateMapping.ObjectType;
			}
		}

		public bool CheckIsEntityCollection (string path)
		{
			return false;
		}

		public bool CheckIsField (string path)
		{
			string name;
			if (path.StartsWith (".", StringComparison.Ordinal)) {
				name = path.Substring (1);
			}
			else {
				name = path;
			}
			return _group.CheckName (name);
		}

		public bool CheckIsRelateEntity (string path)
		{
			return false;
		}

		public DataFieldInfo CreateFieldInfoForPath (string path)
		{
			string name;
			if (path.StartsWith (".", StringComparison.Ordinal)) {
				name = path.Substring (1);
			}
			else {
				name = path;
			}
			DataFieldInfo info = _group.GetAggregateData (name);
			if (!Object.Equals (info, null)) {
				info = info.Clone () as DataFieldInfo;
				NameDataFieldInfo nameInfo = new NameDataFieldInfo (info, name);
				return nameInfo;
			}
			else {
				throw new LightDataException (string.Format (RE.CanNotFindFieldInfoViaSpecialPath, path));
			}
		}

		public ISelector CreateSpecialSelector (string [] paths)
		{
			Selector selector = new Selector ();
			foreach (string path in paths) {
				string name;
				if (path.StartsWith (".", StringComparison.Ordinal)) {
					name = path.Substring (1);
				}
				else {
					name = path;
				}
				DataFieldInfo info = _group.GetAggregateData (name);
				if (!Object.Equals (info, null)) {
					NameDataFieldInfo nameInfo = new NameDataFieldInfo (info, name);
					selector.SetSelectField (nameInfo);
				}
				else {
					throw new LightDataException (string.Format (RE.CanNotFindFieldInfoViaSpecialPath, path));
				}
			}
			return selector;
		}
	}
}

