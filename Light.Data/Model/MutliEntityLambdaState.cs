using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class MutliEntityLambdaState : LambdaState
	{
		readonly Dictionary<string, RelationMap> mapDict = new Dictionary<string, RelationMap> ();

		readonly Dictionary<string, string> aliasDict = new Dictionary<string, string> ();

		//readonly Dictionary<string, DataFieldInfo> infoDict = new Dictionary<string, DataFieldInfo> ();

		readonly DataEntityMapping firstMapping = null;

		public MutliEntityLambdaState (ICollection<ParameterExpression> paramters)
		{
			int index = 0;
			foreach (ParameterExpression parameter in paramters) {
				string name = parameter.Name;
				Type type = parameter.Type;
				DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
				if (firstMapping != null) {
					firstMapping = entityMapping;
				}
				mapDict [name] = entityMapping.GetRelationMap ();
				aliasDict [name] = "T" + index;
				index++;
			}

		}

		public override DataEntityMapping MainMapping {
			get {
				return firstMapping;
			}
		}

		public override bool CheckPamramter (string name, Type type)
		{
			RelationMap map;
			if (mapDict.TryGetValue (name, out map)) {
				return map.RootMapping.ObjectType == type;
			}
			else {
				return false;
			}
		}

		public override DataFieldInfo GetDataFileInfo (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index < 0) {
				throw new LambdaParseException ("");
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index);
			RelationMap map;
			if (mapDict.TryGetValue (name, out map)) {
				DataFieldInfo info = map.CreateFieldInfoForField (path);
				string aliasTableName = aliasDict [name];
				info.AliasTableName = aliasTableName;
				return info;
			}
			else {
				throw new LambdaParseException ("");
			}
		}

		public override LambdaPathType ParsePath (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index == -1) {
				if (mapDict.ContainsKey (fullPath)) {
					return LambdaPathType.Parameter;
				}
				else {
					throw new LambdaParseException ("");
				}
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index);
			RelationMap map;
			if (mapDict.TryGetValue (name, out map)) {
				if (map.CheckIsField (path)) {
					return LambdaPathType.Field;
				}
				else if (map.CheckIsRelateEntity (path)) {
					return LambdaPathType.RelateEntity;
				}
				else if (map.CheckIsEntityCollection (path)) {
					return LambdaPathType.RelateCollection;
				}
				else {
					return LambdaPathType.None;
				}
			}
			else {
				throw new LambdaParseException ("");
			}
		}

		public override ISelector CreateSelector (string [] fullPaths)
		{
			Dictionary<string, List<string>> dict = new Dictionary<string, List<string>> ();
			foreach (string fullPath in fullPaths) {
				int index = fullPath.IndexOf (".", StringComparison.Ordinal);
				if (index < 0) {
					if (mapDict.ContainsKey (fullPath)) {
						List<string> list;
						if (!dict.TryGetValue (fullPath, out list)) {
							list = new List<string> ();
							dict.Add (fullPath, list);
						}
						if (!list.Contains (string.Empty)) {
							list.Add (string.Empty);
						}
					}
					else {
						throw new LambdaParseException ("");
					}
				}
				else {
					string name = fullPath.Substring (0, index);
					string path = fullPath.Substring (index);
					if (mapDict.ContainsKey (name)) {
						List<string> list;
						if (!dict.TryGetValue (name, out list)) {
							list = new List<string> ();
							dict.Add (name, list);
						}
						if (!list.Contains (path)) {
							list.Add (path);
						}
					}
					else {
						throw new LambdaParseException ("");
					}
				}
			}
			Dictionary<string, Selector> selectDict = new Dictionary<string, Selector> ();
			foreach (KeyValuePair<string, List<string>> kvs in dict) {
				RelationMap map = mapDict [kvs.Key];
				string alias = aliasDict [kvs.Key];
				Selector selector = map.CreateSpecialSelector (kvs.Value.ToArray ()) as Selector;
				if (selector == null) {
					throw new LightDataException ("");
				}
				selectDict.Add (alias, selector);
			}
			JoinSelector joinSelector = Selector.ComposeSelector (selectDict);
			return joinSelector;
		}
	}
}

