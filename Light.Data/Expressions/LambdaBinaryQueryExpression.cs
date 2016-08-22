using System;
namespace Light.Data
{
	class LambdaBinaryQueryExpression : QueryExpression
	{
		QueryPredicate _predicate;

		object _left;

		object _right;

		public LambdaBinaryQueryExpression (DataEntityMapping mapping, QueryPredicate predicate, object left, object right)
			: base (mapping)
		{
			//_fieldInfo = fieldInfo;
			_predicate = predicate;
			_left = left;
			_right = right;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string sql = null;
		//	DataParameter [] dataParameters1 = null;
		//	DataParameter [] dataParameters2 = null;

		//	DataFieldInfo leftInfo = _left as DataFieldInfo;
		//	DataFieldInfo rightInfo = _right as DataFieldInfo;
		//	if (!Object.Equals (leftInfo, null) && !Object.Equals (rightInfo, null)) {
		//		string leftSql = leftInfo.CreateSqlString (factory, isFullName, out dataParameters1);
		//		string rightSql = rightInfo.CreateSqlString (factory, isFullName, out dataParameters2);
		//		sql = factory.CreateSingleParamSql (leftSql, _predicate, rightSql);
		//	}
		//	else if (!Object.Equals (leftInfo, null)) {
		//		string leftSql = leftInfo.CreateSqlString (factory, isFullName, out dataParameters1);
		//		object right = LambdaExpressionExtend.ConvertLambdaObject (_right);
		//		if (Object.Equals (right, null)) {
		//			bool predicate;
		//			if (_predicate == QueryPredicate.Eq) {
		//				predicate = true;
		//			}
		//			else if (_predicate == QueryPredicate.NotEq) {
		//				predicate = false;
		//			}
		//			else {
		//				throw new LightDataException ("");
		//			}
		//			sql = factory.CreateNullQuerySql (leftSql, predicate);
		//		}
		//		else if (right is bool) {
		//			bool predicate;
		//			if (_predicate == QueryPredicate.Eq) {
		//				predicate = true;
		//			}
		//			else if (_predicate == QueryPredicate.NotEq) {
		//				predicate = false;
		//			}
		//			else {
		//				throw new LightDataException ("");
		//			}
		//			bool ret = (bool)right;
		//			sql = factory.CreateBooleanQuerySql (leftSql, ret, predicate, false);
		//		}
		//		else {
		//			string pn = factory.CreateTempParamName ();
		//			DataParameter dataParameter = new DataParameter (pn, right);
		//			dataParameters2 = new [] { dataParameter };
		//			sql = factory.CreateSingleParamSql (leftSql, _predicate, dataParameter.ParameterName);
		//		}
		//	}
		//	else if (!Object.Equals (rightInfo, null)) {
		//		string rightSql = rightInfo.CreateSqlString (factory, isFullName, out dataParameters2);
		//		object left = LambdaExpressionExtend.ConvertLambdaObject (_left);
		//		if (Object.Equals (left, null)) {
		//			bool predicate;
		//			if (_predicate == QueryPredicate.Eq) {
		//				predicate = true;
		//			}
		//			else if (_predicate == QueryPredicate.NotEq) {
		//				predicate = false;
		//			}
		//			else {
		//				throw new LightDataException ("");
		//			}
		//			sql = factory.CreateNullQuerySql (rightSql, predicate);
		//		}
		//		else if (left is bool) {
		//			bool predicate;
		//			if (_predicate == QueryPredicate.Eq) {
		//				predicate = true;
		//			}
		//			else if (_predicate == QueryPredicate.NotEq) {
		//				predicate = false;
		//			}
		//			else {
		//				throw new LightDataException ("");
		//			}
		//			bool ret = (bool)left;
		//			sql = factory.CreateBooleanQuerySql (rightSql, ret, predicate, true);
		//		}
		//		else {
		//			string pn = factory.CreateTempParamName ();
		//			DataParameter dataParameter = new DataParameter (pn, left);
		//			dataParameters1 = new [] { dataParameter };
		//			sql = factory.CreateSingleParamSql (dataParameter.ParameterName, _predicate, rightSql);
		//		}
		//	}
		//	else {
		//		throw new LightDataException ("");
		//	}
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = null;

			DataFieldInfo leftInfo = _left as DataFieldInfo;
			DataFieldInfo rightInfo = _right as DataFieldInfo;
			if (!Object.Equals (leftInfo, null) && !Object.Equals (rightInfo, null)) {
				string leftSql = leftInfo.CreateSqlString (factory, isFullName, state);
				string rightSql = rightInfo.CreateSqlString (factory, isFullName, state);
				sql = factory.CreateSingleParamSql (leftSql, _predicate, rightSql);
			}
			else if (!Object.Equals (leftInfo, null)) {
				string leftSql = leftInfo.CreateSqlString (factory, isFullName, state);
				object right = LambdaExpressionExtend.ConvertLambdaObject (_right);
				if (Object.Equals (right, null)) {
					bool predicate;
					if (_predicate == QueryPredicate.Eq) {
						predicate = true;
					}
					else if (_predicate == QueryPredicate.NotEq) {
						predicate = false;
					}
					else {
						throw new LightDataException ("");
					}
					sql = factory.CreateNullQuerySql (leftSql, predicate);
				}
				else if (right is bool) {
					bool predicate;
					if (_predicate == QueryPredicate.Eq) {
						predicate = true;
					}
					else if (_predicate == QueryPredicate.NotEq) {
						predicate = false;
					}
					else {
						throw new LightDataException ("");
					}
					bool ret = (bool)right;
					sql = factory.CreateBooleanQuerySql (leftSql, ret, predicate, false);
				}
				else {
					string name = state.AddDataParameter (right);
					sql = factory.CreateSingleParamSql (leftSql, _predicate, name);
				}
			}
			else if (!Object.Equals (rightInfo, null)) {
				string rightSql = rightInfo.CreateSqlString (factory, isFullName, state);
				object left = LambdaExpressionExtend.ConvertLambdaObject (_left);
				if (Object.Equals (left, null)) {
					bool predicate;
					if (_predicate == QueryPredicate.Eq) {
						predicate = true;
					}
					else if (_predicate == QueryPredicate.NotEq) {
						predicate = false;
					}
					else {
						throw new LightDataException ("");
					}
					sql = factory.CreateNullQuerySql (rightSql, predicate);
				}
				else if (left is bool) {
					bool predicate;
					if (_predicate == QueryPredicate.Eq) {
						predicate = true;
					}
					else if (_predicate == QueryPredicate.NotEq) {
						predicate = false;
					}
					else {
						throw new LightDataException ("");
					}
					bool ret = (bool)left;
					sql = factory.CreateBooleanQuerySql (rightSql, ret, predicate, true);
				}
				else {
					string name = state.AddDataParameter (left);
					sql = factory.CreateSingleParamSql (name, _predicate, rightSql);
				}
			}
			else {
				throw new LightDataException ("");
			}

			return sql;
		}
	}
}

