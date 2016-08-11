using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class MutliEntityLambdaState : LambdaState
	{
		readonly Dictionary<string, RelationMap> mapDict = new Dictionary<string, RelationMap> ();

		readonly Dictionary<string, string> aliasDict = new Dictionary<string, string> ();

		readonly Dictionary<string, DataFieldInfo> infoDict = new Dictionary<string, DataFieldInfo> ();

		public MutliEntityLambdaState (ICollection<ParameterExpression> paramters)
		{
			int index = 0;
			foreach (ParameterExpression parameter in paramters) {
				string name = parameter.Name;
				Type type = parameter.Type;
				DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
				mapDict [name] = entityMapping.GetRelationMap ();
				aliasDict [name] = "T" + index;
				index++;
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
				//AliasDataFieldInfo alias = new AliasDataFieldInfo (info, string.Format ("{0}_{1}", aliasTableName, info.FieldName));
				//alias.AliasTableName = aliasTableName;
				//return alias;
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
			throw new NotImplementedException ();
		}
	}
}

