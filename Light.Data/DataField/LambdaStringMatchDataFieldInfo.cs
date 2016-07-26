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


		public LambdaStringMatchDataFieldInfo (DataFieldInfo info, bool starts, bool ends, object left, object right)
			: base (info)
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

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	object value = LambdaExpressionExtend.ConvertObject (_value, factory, isFullName, true);
		//	return factory.CreateLambdaMatchQuerySql (field, _isReverse, _starts, _ends, _isNot, value);
		//}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = null;
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			object left;
			object right;
			DataFieldInfo leftInfo = _left as DataFieldInfo;
			DataFieldInfo rightInfo = _right as DataFieldInfo;
			if (!Object.Equals (leftInfo, null) && !Object.Equals (rightInfo, null)) {
				left = leftInfo.CreateDataFieldSql (factory, isFullName, out dataParameters1);
				right = rightInfo.CreateDataFieldSql (factory, isFullName, out dataParameters2);
			}
			else if (!Object.Equals (leftInfo, null)) {
				left = leftInfo.CreateDataFieldSql (factory, isFullName, out dataParameters1);
				object rightObject = LambdaExpressionExtend.ConvertLambdaObject (_right);
				string pn = factory.CreateTempParamName ();
				DataParameter dataParameter = new DataParameter (pn, rightObject);
				dataParameters2 = new [] { dataParameter };
				right = dataParameter.ParameterName;
			}
			else if (!Object.Equals (rightInfo, null)) {
				right = rightInfo.CreateDataFieldSql (factory, isFullName, out dataParameters2);
				object leftObject = LambdaExpressionExtend.ConvertLambdaObject (_left);
				string pn = factory.CreateTempParamName ();
				DataParameter dataParameter = new DataParameter (pn, leftObject);
				dataParameters2 = new [] { dataParameter };
				left = dataParameter.ParameterName;
			}
			else {
				throw new LightDataException ("");
			}
			sql = factory.CreateLambdaMatchQuerySql (left, right, _starts, _ends, _isNot);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaMatchQuerryExpression (this);
		}

		//internal override string DBType {
		//	get {
		//		return "bool";
		//	}
		//}

		//internal override object ToParameter (object value)
		//{
		//	if (value is Boolean) {
		//		return value;
		//	}
		//	else {
		//		return Convert.ToBoolean (value);
		//	}
		//}

		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	if (base.EqualsDetail (info)) {
		//		LambdaMatchDataFieldInfo target = info as LambdaMatchDataFieldInfo;
		//		return this._starts == target._starts
		//				   && this._ends == target._ends
		//				   && this._isNot == target._isNot
		//				   && this._left == target._left
		//			       && this._right == target._right;
		//	}
		//	else {
		//		return false;
		//	}
		//}


	}
}

