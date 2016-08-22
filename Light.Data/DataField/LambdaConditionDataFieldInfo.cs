using System;
namespace Light.Data
{
	class LambdaConditionDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		object _ifTrue;

		object _ifFalse;

		QueryExpression _query;

		bool _isNot;

		public LambdaConditionDataFieldInfo (QueryExpression query, object ifTrue, object ifFalse)
			: base (query.TableMapping)
		{
			_query = query;
			_ifTrue = ifTrue;
			_ifFalse = ifFalse;
		}

		public void SetNot ()
		{
			_isNot = !_isNot;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string sql = null;
		//	DataParameter [] dataParameters0 = null;
		//	DataParameter [] dataParameters1 = null;
		//	DataParameter [] dataParameters2 = null;
		//	string query = _query.CreateSqlString (factory, isFullName, out dataParameters0);
		//	object ifTrue;
		//	object ifFalse;
		//	DataFieldInfo ifTrueInfo = _ifTrue as DataFieldInfo;
		//	DataFieldInfo ifFalseInfo = _ifFalse as DataFieldInfo;
		//	if (!Object.Equals (ifTrueInfo, null) && !Object.Equals (ifFalseInfo, null)) {
		//		ifTrue = ifTrueInfo.CreateSqlString (factory, isFullName, out dataParameters1);
		//		ifFalse = ifFalseInfo.CreateSqlString (factory, isFullName, out dataParameters2);
		//	}
		//	else if (!Object.Equals (ifTrueInfo, null)) {
		//		ifTrue = ifTrueInfo.CreateSqlString (factory, isFullName, out dataParameters1);
		//		object ifFalseObject = LambdaExpressionExtend.ConvertLambdaObject (_ifFalse);
		//		string pn = factory.CreateTempParamName ();
		//		DataParameter dataParameter = new DataParameter (pn, ifFalseObject);
		//		dataParameters2 = new [] { dataParameter };
		//		ifFalse = dataParameter.ParameterName;
		//	}
		//	else if (!Object.Equals (ifFalseInfo, null)) {
		//		ifFalse = ifFalseInfo.CreateSqlString (factory, isFullName, out dataParameters2);
		//		object ifTrueObject = LambdaExpressionExtend.ConvertLambdaObject (_ifTrue);
		//		string pn = factory.CreateTempParamName ();
		//		DataParameter dataParameter = new DataParameter (pn, ifTrueObject);
		//		dataParameters1 = new [] { dataParameter };
		//		ifTrue = dataParameter.ParameterName;
		//	}
		//	else {
		//		object ifTrueObject = LambdaExpressionExtend.ConvertLambdaObject (_ifTrue);
		//		string pn1 = factory.CreateTempParamName ();
		//		DataParameter dataParameter1 = new DataParameter (pn1, ifTrueObject);
		//		dataParameters1 = new [] { dataParameter1 };
		//		ifTrue = dataParameter1.ParameterName;
		//		object ifFalseObject = LambdaExpressionExtend.ConvertLambdaObject (_ifFalse);
		//		string pn2 = factory.CreateTempParamName ();
		//		DataParameter dataParameter2 = new DataParameter (pn2, ifFalseObject);
		//		dataParameters2 = new [] { dataParameter2 };
		//		ifFalse = dataParameter2.ParameterName;
		//	}
		//	sql = factory.CreateConditionSql (query, ifTrue, ifFalse);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters0,dataParameters1, dataParameters2);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			string query = _query.CreateSqlString (factory, isFullName, state);

			object ifTrue;
			object ifFalse;
			DataFieldInfo ifTrueInfo = _ifTrue as DataFieldInfo;
			DataFieldInfo ifFalseInfo = _ifFalse as DataFieldInfo;
			if (!Object.Equals (ifTrueInfo, null) && !Object.Equals (ifFalseInfo, null)) {
				ifTrue = ifTrueInfo.CreateSqlString (factory, isFullName, state);
				ifFalse = ifFalseInfo.CreateSqlString (factory, isFullName, state);
			}
			else if (!Object.Equals (ifTrueInfo, null)) {
				ifTrue = ifTrueInfo.CreateSqlString (factory, isFullName, state);
				object ifFalseObject = LambdaExpressionExtend.ConvertLambdaObject (_ifFalse);
				string pn = factory.CreateTempParamName ();
				ifFalse = state.AddDataParameter (ifFalseObject);
			}
			else if (!Object.Equals (ifFalseInfo, null)) {
				ifFalse = ifFalseInfo.CreateSqlString (factory, isFullName, state);
				object ifTrueObject = LambdaExpressionExtend.ConvertLambdaObject (_ifTrue);
				string pn = factory.CreateTempParamName ();
				ifTrue = state.AddDataParameter (ifTrueObject);
			}
			else {
				object ifTrueObject = LambdaExpressionExtend.ConvertLambdaObject (_ifTrue);
				object ifFalseObject = LambdaExpressionExtend.ConvertLambdaObject (_ifFalse);
				ifTrue = state.AddDataParameter (ifTrueObject);
				ifFalse = state.AddDataParameter (ifFalseObject);
			}

			sql = factory.CreateConditionSql (query, ifTrue, ifFalse);
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaConditionQueryExpression (this);
		}
	}
}

