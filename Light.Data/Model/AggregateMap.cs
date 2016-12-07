using System;
namespace Light.Data
{
	class AggregateMap : IMap
	{
		readonly AggregateModel _model;

		public AggregateMap (AggregateModel model)
		{
			this._model = model;
		}

		public Type Type {
			get {
				return _model.OutputMapping.ObjectType;
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
			return _model.CheckName (name);
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
			DataFieldInfo info = _model.GetAggregateData (name);
			if (!Object.Equals (info, null)) {
				DataFieldInfo nameInfo = new DataFieldInfo (info.TableMapping, name);
				return nameInfo;
			}
			else {
				//return null;
				throw new LightDataException (string.Format (RE.CanNotFindFieldInfoViaSpecialPath, path));
			}
		}

		public ISelector CreateSelector (string [] paths)
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
				//DataFieldInfo info = _model.GetAggregateData (name);
				//if (!Object.Equals (info, null)) {
				//	DataFieldInfo nameInfo = new DataFieldInfo (info.TableMapping, name);
				//	selector.SetSelectField (nameInfo);
				//}
				//else {
				//	throw new LightDataException (string.Format (RE.CanNotFindFieldInfoViaSpecialPath, path));
				//}

				//DataFieldInfo info = _model.GetAggregateData (name);
				if (_model.CheckName(name)) {
					DataFieldInfo nameInfo = new DataFieldInfo (_model.EntityMapping, name);
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

