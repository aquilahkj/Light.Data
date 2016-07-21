using System;
using System.Data;
using Mono.Data.Sqlite;

namespace Light.Data.SQLiteAdapter
{
	class SQLite: Database
	{
		public SQLite ()
		{
			_factory = new SQLiteCommandFactory ();
		}

		#region IDatabase 成员

		public override IDbConnection CreateConnection ()
		{
			return new SqliteConnection (); 
		}

		public override IDbConnection CreateConnection (string connectionString)
		{
			return new SqliteConnection (connectionString);
		}

		public override IDbDataAdapter CreateDataAdapter (IDbCommand cmd)
		{
			SqliteDataAdapter da = new SqliteDataAdapter ();
			da.SelectCommand = (SqliteCommand)cmd;
			return da;
		}

		public override IDbCommand CreateCommand (string sql)
		{
			SqliteCommand command = new SqliteCommand ();
			command.CommandText = sql;
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDbCommand CreateCommand ()
		{
			SqliteCommand command = new SqliteCommand ();
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction)
		{
			string parameterName = name;
			if (!parameterName.StartsWith ("@", StringComparison.Ordinal)) {
				parameterName = "@" + parameterName;
			}
			SqliteParameter sp = new SqliteParameter (parameterName, value);
			if (value == null)
				sp.Value = DBNull.Value;
			sp.Direction = direction;
			DbType dType;
			int size;
			if (!string.IsNullOrEmpty (dbType)) {
				if (Utility.ParseDbType (dbType, out dType)) {
					sp.DbType = dType;
				}
				if (Utility.ParseSize (dbType, out size)) {
					sp.Size = size;
				}
			}
			return sp;
		}

		public override void FormatStoredProcedureParameter (IDataParameter dataParmeter)
		{
			if (dataParmeter.ParameterName.StartsWith ("@", StringComparison.Ordinal)) {
				dataParmeter.ParameterName = dataParmeter.ParameterName.TrimStart ('@');
			}
		}

		#endregion

		public override void SetExtendParams (ExtendParamCollection extendParams)
		{
//			ExtendParamsCollection extendParams = new ExtendParamsCollection (arguments);

			if (extendParams ["InnerPager"] != null) {
				if (extendParams ["InnerPager"].ToLower () == "true") {
					InnerPager = true;
				}
			}

			if (extendParams ["TimeOut"] != null) {
				int timeout;
				if (int.TryParse (extendParams ["TimeOut"], out timeout)) {
					CommandTimeOut = timeout;
				}
			}
		}
	}
}

