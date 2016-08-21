using System;
namespace Light.Data
{
	class LambdaStringMatchDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		bool _starts;

		bool _ends;

		bool _isNot;

		object _left;

		object _right;

		public LambdaStringMatchDataFieldInfo (DataEntityMapping mapping, bool starts, bool ends, object left, object right)
			: base (mapping)
		{
			_starts = starts;
			_ends = ends;
			_left = left;
			_right = right;
		}

		public void SetNot ()
		{
			_isNot = !_isNot;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = null;
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			object left;
			object right;
			DataFieldInfo leftInfo = _left as DataFieldInfo;
			DataFieldInfo rightInfo = _right as DataFieldInfo;
			if (!Object.Equals (leftInfo, null) && !Object.Equals (rightInfo, null)) {
				left = leftInfo.CreateSqlString (factory, isFullName, out dataParameters1);
				right = rightInfo.CreateSqlString (factory, isFullName, out dataParameters2);
			}
			else if (!Object.Equals (leftInfo, null)) {
				left = leftInfo.CreateSqlString (factory, isFullName, out dataParameters1);
				object rightObject = LambdaExpressionExtend.ConvertLambdaObject (_right);
				string pn = factory.CreateTempParamName ();
				DataParameter dataParameter = new DataParameter (pn, rightObject);
				dataParameters2 = new [] { dataParameter };
				right = dataParameter.ParameterName;
			}
			else if (!Object.Equals (rightInfo, null)) {
				right = rightInfo.CreateSqlString (factory, isFullName, out dataParameters2);
				object leftObject = LambdaExpressionExtend.ConvertLambdaObject (_left);
				string pn = factory.CreateTempParamName ();
				DataParameter dataParameter = new DataParameter (pn, leftObject);
				dataParameters1 = new [] { dataParameter };
				left = dataParameter.ParameterName;
			}
			else {
				throw new LightDataException ("");
			}
			sql = factory.CreateLikeMatchQuerySql (left, right, _starts, _ends, _isNot);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			object left;
			object right;
			DataFieldInfo leftInfo = _left as DataFieldInfo;
			DataFieldInfo rightInfo = _right as DataFieldInfo;
			if (!Object.Equals (leftInfo, null) && !Object.Equals (rightInfo, null)) {
				left = leftInfo.CreateSqlString (factory, isFullName, state);
				right = rightInfo.CreateSqlString (factory, isFullName, state);
			}
			else if (!Object.Equals (leftInfo, null)) {
				left = leftInfo.CreateSqlString (factory, isFullName, state);
				object rightObject = LambdaExpressionExtend.ConvertLambdaObject (_right);
				right = state.AddDataParameter (rightObject);
			}
			else if (!Object.Equals (rightInfo, null)) {
				right = rightInfo.CreateSqlString (factory, isFullName, state);
				object leftObject = LambdaExpressionExtend.ConvertLambdaObject (_left);
				left = state.AddDataParameter (leftObject);
			}
			else {
				throw new LightDataException ("");
			}
			sql = factory.CreateLikeMatchQuerySql (left, right, _starts, _ends, _isNot);
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaMatchQuerryExpression (this);
		}
	}
}

