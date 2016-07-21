using System;
namespace Light.Data
{
	class LambdaMatchDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		bool _starts;

		bool _ends;

		bool _isNot;

		object _value;

		bool _isReverse;

		public LambdaMatchDataFieldInfo (DataFieldInfo info, bool starts, bool ends, object value, bool isReverse)
			: base (info)
		{
			_starts = starts;
			_ends = ends;
			_value = value;
			_isReverse = isReverse;
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
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters1);
			object value = LambdaExpressionExtend.ConvertLambdaObject (_value, factory, isFullName, true, out dataParameters2);
			string sql = factory.CreateLambdaMatchQuerySql (field, _isReverse, _starts, _ends, _isNot, value);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaMatchExpression (this);
		}

		internal override string DBType {
			get {
				return "bool";
			}
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				LambdaMatchDataFieldInfo target = info as LambdaMatchDataFieldInfo;
				return this._starts == target._starts
						   && this._ends == target._ends
						   && this._isNot == target._isNot
						   && this._value == target._value
						   && this._isReverse == target._isReverse;
			}
			else {
				return false;
			}
		}


	}
}

