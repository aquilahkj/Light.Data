using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class SingleEntityLambdaState : LambdaState
	{
		readonly string singleEntityName;

		readonly RelationMap singleEntityMap;

		public SingleEntityLambdaState (ParameterExpression parameter)
		{
			singleEntityName = parameter.Name;
			Type type = parameter.Type;
			DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
			singleEntityMap = entityMapping.GetRelationMap ();
		}

		public override bool CheckPamramter (string name, Type type)
		{
			return singleEntityName == name && singleEntityMap.RootMapping.ObjectType == type;
		}

		public override DataFieldInfo GetDataFileInfo (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index < 0) {
				throw new LambdaParseException ("");
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index);
			if (singleEntityName != name) {
				throw new LambdaParseException ("");
			}
			DataFieldInfo info = singleEntityMap.CreateFieldInfoForField (path);
			return info;
		}

		public override LambdaPathType ParsePath (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index == -1) {
				if (fullPath == singleEntityName) {
					return LambdaPathType.Parameter;
				}
				else {
					throw new LambdaParseException ("");
				}
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index);
			if (singleEntityName != name) {
				throw new LambdaParseException ("");
			}
			if (singleEntityMap.CheckIsField (path)) {
				return LambdaPathType.Field;
			}
			else if (singleEntityMap.CheckIsRelateEntity (path)) {
				return LambdaPathType.RelateEntity;
			}
			else if (singleEntityMap.CheckIsEntityCollection (path)) {
				return LambdaPathType.RelateCollection;
			}
			else {
				return LambdaPathType.None;
			}
		}

		public override ISelector CreateSelector (string [] fullPaths)
		{
			List<string> list = new List<string> ();
			foreach (string fullPath in fullPaths) {
				int index = fullPath.IndexOf (".", StringComparison.Ordinal);
				if (index < 0) {
					if (fullPath == singleEntityName) {
						if (!list.Contains (fullPath)) {
							list.Add (fullPath);
						}
					}
					else {
						throw new LambdaParseException ("");
					}
				}
				else {
					string name = fullPath.Substring (0, index);
					string path = fullPath.Substring (index);
					if (name == singleEntityName) {
						if (!list.Contains (name)) {
							list.Add (path);
						}
					}
					else {
						throw new LambdaParseException ("");
					}
				}
			}
			return singleEntityMap.CreateSpecialSelector (list.ToArray ());
		}

	}
}

