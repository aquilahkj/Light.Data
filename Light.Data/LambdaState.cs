using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LambdaState
	{
		readonly Dictionary<string, DataEntityMapping> mappingDict = new Dictionary<string, DataEntityMapping> ();

		readonly Dictionary<string, RelationMap> mapDict = new Dictionary<string, RelationMap> ();

		public LambdaState ()
		{

		}

		public LambdaState (LambdaExpression expression)
		{
			foreach (ParameterExpression parameter in expression.Parameters) {
				string name = parameter.Name;
				Type type = parameter.Type;
				DataEntityMapping entityMapping = DataEntityMapping.GetEntityMapping (type);
				mappingDict [name] = entityMapping;
				mapDict [name] = entityMapping.GetRelationMap ();
			}
		}

		//public void Set (string name, Type type)
		//{
		//	if (name == null)
		//		throw new ArgumentNullException (nameof (name));

		//	if (type == null)
		//		throw new ArgumentNullException (nameof (type));
		//	mappingDict [name] = DataEntityMapping.GetEntityMapping (type);
		//}

		public bool TryGetEntityMapping (string name, out DataEntityMapping mapping)
		{
			return mappingDict.TryGetValue (name, out mapping);
		}

		public DataEntityMapping GetEntityMapping (string name)
		{
			return mappingDict [name];
		}

		public bool TryGetRelationMap (string name, out RelationMap relationMap)
		{
			return mapDict.TryGetValue (name, out relationMap);
		}

		public RelationMap GetRelationMap (string name)
		{
			return mapDict [name];
		}



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
}

