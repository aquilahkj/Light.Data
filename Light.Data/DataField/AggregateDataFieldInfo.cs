using System;
namespace Light.Data
{
	class AggregateDataFieldInfo : LambdaDataFieldInfo, IAliasDataFieldInfo
	{
		DataFieldInfo _fieldInfo;

		string _aggregateName;

		bool _aggregate;

		public AggregateDataFieldInfo (DataFieldInfo fieldInfo, string name, bool aggregate)
			: base (fieldInfo.TableMapping)
		{
			_fieldInfo = fieldInfo;
			_aggregateName = name;
			_aggregate = aggregate;
		}

		public DataFieldInfo FieldInfo {
			get {
				return _fieldInfo;
			}
		}

		public string AggregateName {
			get {
				return _aggregateName;
			}

			set {
				_aggregateName = value;
			}
		}

		public bool Aggregate {
			get {
				return _aggregate;
			}
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return _fieldInfo.CreateSqlString (factory, isFullName, state);
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = _fieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateAliasSql (field, _aggregateName);
		}

		public string CreateAliasDataFieldSql (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string fieldSql = _fieldInfo.CreateSqlString (factory, isFullName, state);
			string sql = factory.CreateAliasSql (fieldSql, _aggregateName);
			return sql;
		}
	}
}

