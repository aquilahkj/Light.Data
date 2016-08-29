using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Light.Data.PostgreAdapter
{
	class Postgre:Database
	{
		public Postgre ()
		{
			_factory = new PostgreCommandFactory ();
		}

		#region implemented abstract members of Database

		public override IDbConnection CreateConnection ()
		{
			return new NpgsqlConnection (); 
		}

		public override IDbConnection CreateConnection (string connectionString)
		{
			return new NpgsqlConnection (connectionString);
		}

		public override IDbDataAdapter CreateDataAdapter (IDbCommand cmd)
		{
			NpgsqlDataAdapter da = new NpgsqlDataAdapter ();
			da.SelectCommand = (NpgsqlCommand)cmd;
			return da;
		}

		public override IDbCommand CreateCommand (string sql)
		{
			NpgsqlCommand command = new NpgsqlCommand ();
			command.CommandText = sql;
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDbCommand CreateCommand ()
		{
			NpgsqlCommand command = new NpgsqlCommand ();
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction)
		{
			string parameterName = name;
			if (!parameterName.StartsWith (":", StringComparison.Ordinal)) {
				parameterName = ":" + parameterName;
			}
			NpgsqlParameter sp = new NpgsqlParameter (parameterName, value);
			if (value == null)
				sp.Value = DBNull.Value;
			sp.Direction = direction;
			NpgsqlDbType sqltype;
			DbType dType;
			int size;
			if (!string.IsNullOrEmpty (dbType)) {
				if (ParseSqlDbType (dbType, out sqltype)) {
					sp.NpgsqlDbType = sqltype;
				}
				else if (Utility.ParseDbType (dbType, out dType)) {
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
			if (dataParmeter.ParameterName.StartsWith (":", StringComparison.Ordinal)) {
				dataParmeter.ParameterName = dataParmeter.ParameterName.TrimStart (':');
			}
		}

		#endregion

		bool ParseSqlDbType (string dbType, out NpgsqlDbType type)
		{
			type = NpgsqlDbType.Varchar;
			int index = dbType.IndexOf ('(');
			string typeString = string.Empty;
			if (index < 0) {
				typeString = dbType;
			}
			else if (index == 0) {
				return false;
			}
			else {
				typeString = dbType.Substring (0, index);
			}
			return Enum.TryParse<NpgsqlDbType> (typeString, true, out type);
		}

		public override void SetExtendParams (ExtendParamCollection extendParams)
		{
//			ExtendParamsCollection extendParams = new ExtendParamsCollection (arguments);

			//if (extendParams ["InnerPager"] != null) {
			//	if (extendParams ["InnerPager"].ToLower () == "true") {
			//		InnerPager = true;
			//	}
			//}

			if (extendParams ["TimeOut"] != null) {
				int timeout;
				if (int.TryParse (extendParams ["TimeOut"], out timeout)) {
					CommandTimeOut = timeout;
				}
			}

			if (extendParams ["StrictMode"] != null) {
				bool strictMode;
				if (bool.TryParse (extendParams ["StrictMode"], out strictMode)) {
					PostgreCommandFactory oracleFactory = _factory as PostgreCommandFactory;
					oracleFactory.SetStrictMode (strictMode);
				}
			}
		}
	}
}

