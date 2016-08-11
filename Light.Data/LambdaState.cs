using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class LambdaState
	{
		//readonly Dictionary<string, DataEntityMapping> mappingDict = new Dictionary<string, DataEntityMapping> ();

		//readonly Dictionary<string, RelationMap> mapDict = new Dictionary<string, RelationMap> ();

		//readonly Dictionary<string, string> aliasDict = new Dictionary<string, string> ();

		//readonly string singleEntityName;

		//readonly RelationMap singleEntityMap;

		//readonly bool mutliEntity;

		//public LambdaState ()
		//{

		//}

		//public LambdaState (LambdaExpression expression)
		//{
		//	if (expression.Parameters.Count == 0) {
		//		throw new LightDataException ("");
		//	}
		//	else if (expression.Parameters.Count == 1) {
		//		ParameterExpression parameter = expression.Parameters [0];
		//		singleEntityName = parameter.Name;
		//		Type type = parameter.Type;
		//		DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
		//		singleEntityMap = entityMapping.GetRelationMap ();
		//	}
		//	else {
		//		mutliEntity = true;
		//		int index = 0;
		//		foreach (ParameterExpression parameter in expression.Parameters) {
		//			string name = parameter.Name;
		//			Type type = parameter.Type;
		//			DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
		//			//mappingDict [name] = entityMapping;
		//			mapDict [name] = entityMapping.GetRelationMap ();
		//			aliasDict [name] = "T" + index;
		//			index++;
		//		}
		//	}
		//}

		//public bool TryGetEntityMapping (string name, out DataEntityMapping mapping)
		//{
		//	return mappingDict.TryGetValue (name, out mapping);
		//}

		//public DataEntityMapping GetEntityMapping (string name)
		//{
		//	return mappingDict [name];
		//}

		//public bool MutliParameter {
		//	get {
		//		return mapDict.Count > 0;
		//	}
		//}

		//public bool TryGetRelationMap (string name, out RelationMap relationMap)
		//{
		//	return mapDict.TryGetValue (name, out relationMap);
		//}

		//public RelationMap GetRelationMap (string name)
		//{
		//	return mapDict [name];
		//}






		public abstract bool CheckPamramter (string name, Type type);

		public abstract DataFieldInfo GetDataFileInfo (string fullPath);

		public abstract LambdaPathType ParsePath (string fullPath);

		public abstract ISelector CreateSelector (string[] fullPaths);


		bool mutliQuery;

		public bool MutliQuery {
			get {
				return mutliQuery;
			}

			set {
				mutliQuery = value;
			}
		}



	}

	enum LambdaPathType
	{
		None,
		Parameter,
		Field,
		RelateEntity,
		RelateCollection
	}
}

