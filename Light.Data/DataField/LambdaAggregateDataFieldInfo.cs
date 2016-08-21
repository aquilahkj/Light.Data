using System;
namespace Light.Data
{
	class LambdaAggregateDataFieldInfo : LambdaDataFieldInfo
	{
		DataFieldInfo _baseFieldInfo;

		AggregateType _type;

		QueryExpression _query;

		bool _distinct;

		public LambdaAggregateDataFieldInfo (DataFieldInfo fieldInfo, AggregateType type, bool distinct, QueryExpression query)
			: base (fieldInfo.TableMapping)
		{
			_baseFieldInfo = fieldInfo;
			_type = type;
			_distinct = distinct;
			_query = query;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string expression = null;
			if (_query != null) {
				expression = _query.CreateSqlString (factory, isFullName, out dataParameters1);
			}
			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters2);
			string sql = null;
			switch (_type) {
			case AggregateType.COUNT:
				if (expression != null) {
					sql = factory.CreateConditionCountSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateCountSql (field, _distinct);
				}
				break;
			case AggregateType.SUM:
				if (expression != null) {
					sql = factory.CreateConditionSumSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateSumSql (field, _distinct);
				}
				break;
			case AggregateType.AVG:
				if (expression != null) {
					sql = factory.CreateConditionAvgSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateAvgSql (field, _distinct);
				}
				break;
			case AggregateType.MAX:
				if (expression != null) {
					sql = factory.CreateConditionMaxSql (expression, field);
				}
				else {
					sql = factory.CreateMaxSql (field);
				}
				break;
			case AggregateType.MIN:
				if (expression != null) {
					sql = factory.CreateConditionMinSql (expression, field);
				}
				else {
					sql = factory.CreateMinSql (field);
				}
				break;
			}
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}
			string expression = null;
			if (_query != null) {
				expression = _query.CreateSqlString (factory, isFullName, state);
			}
			string field = _baseFieldInfo.CreateSqlString (factory, isFullName, state);

			switch (_type) {
			case AggregateType.COUNT:
				if (expression != null) {
					sql = factory.CreateConditionCountSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateCountSql (field, _distinct);
				}
				break;
			case AggregateType.SUM:
				if (expression != null) {
					sql = factory.CreateConditionSumSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateSumSql (field, _distinct);
				}
				break;
			case AggregateType.AVG:
				if (expression != null) {
					sql = factory.CreateConditionAvgSql (expression, field, _distinct);
				}
				else {
					sql = factory.CreateAvgSql (field, _distinct);
				}
				break;
			case AggregateType.MAX:
				if (expression != null) {
					sql = factory.CreateConditionMaxSql (expression, field);
				}
				else {
					sql = factory.CreateMaxSql (field);
				}
				break;
			case AggregateType.MIN:
				if (expression != null) {
					sql = factory.CreateConditionMinSql (expression, field);
				}
				else {
					sql = factory.CreateMinSql (field);
				}
				break;
			}
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

