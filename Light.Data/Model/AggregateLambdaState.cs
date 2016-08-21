using System;
using System.Linq.Expressions;

namespace Light.Data
{
	class AggregateLambdaState : LambdaState
	{
		readonly string aggregateName;

		readonly Type aggregateType;

		readonly DataEntityMapping entityMapping;

		readonly AggregateGroup aggregateGroup;

		public AggregateLambdaState (ParameterExpression parameter, AggregateGroup group)
		{
			aggregateGroup = group;
			aggregateName = parameter.Name;
			aggregateType = parameter.Type;
			entityMapping = group.EntityMapping;
		}

		public override DataEntityMapping MainMapping {
			get {
				return entityMapping;
			}
		}

		public override bool CheckPamramter (string name, Type type)
		{
			return aggregateName == name && aggregateType == type;
		}

		public override ISelector CreateSelector (string [] fullPaths)
		{
			throw new NotImplementedException ();
		}

		public override DataFieldInfo GetDataFileInfo (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index < 0) {
				throw new LambdaParseException ("");
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index + 1);
			if (aggregateName != name) {
				throw new LambdaParseException ("");
			}
			DataFieldInfo info = aggregateGroup.GetAggregateData (path);
			return info;
		}

		public override LambdaPathType ParsePath (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index == -1) {
				if (fullPath == aggregateName) {
					return LambdaPathType.Parameter;
				}
				else {
					throw new LambdaParseException ("");
				}
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index + 1);
			if (aggregateName != name) {
				throw new LambdaParseException ("");
			}
			if (aggregateGroup.CheckName (path)) {
				return LambdaPathType.Field;
			}
			else {
				return LambdaPathType.None;
			}
		}
	}
}

