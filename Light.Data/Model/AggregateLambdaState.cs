using System;
using System.Linq.Expressions;

namespace Light.Data
{
	class AggregateLambdaState : LambdaState
	{
		readonly string aggregateName;

		readonly Type aggregateType;

		readonly DataEntityMapping entityMapping;

		readonly AggregateModel aggregateGroup;

		public AggregateLambdaState (ParameterExpression parameter, AggregateModel group)
		{
			aggregateGroup = group;
			aggregateName = parameter.Name;
			aggregateType = parameter.Type;
			entityMapping = group.EntityMapping;
		}

		public DataEntityMapping MainMapping {
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
			throw new NotSupportedException ();
		}

		public override DataFieldInfo GetDataFieldInfo (string fullPath)
		{
			int index = fullPath.IndexOf (".", StringComparison.Ordinal);
			if (index < 0) {
				throw new LambdaParseException (LambdaParseMessage.ExpressionFieldPathError, fullPath);
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index + 1);
			if (aggregateName != name) {
				throw new LambdaParseException (LambdaParseMessage.ExpressionFieldPathNotExists, fullPath);
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
					throw new LambdaParseException (LambdaParseMessage.ExpressionFieldPathError, fullPath);
				}
			}
			string name = fullPath.Substring (0, index);
			string path = fullPath.Substring (index + 1);
			if (aggregateName != name) {
				throw new LambdaParseException (LambdaParseMessage.ExpressionFieldPathNotExists, fullPath);
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

