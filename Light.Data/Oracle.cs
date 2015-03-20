using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Text.RegularExpressions;
using Light.Data.Mappings;

namespace Light.Data
{
	class Oracle : Database
	{
		public Oracle ()
		{
			_factory = new OracleCommandFactory (this);
		}

		#region IDatabase 成员

		public override IDbConnection CreateConnection ()
		{
			return new OracleConnection ();
		}

		public override IDbConnection CreateConnection (string connectionString)
		{
			return new OracleConnection (connectionString);
		}

		public override IDbDataAdapter CreateDataAdapter (IDbCommand cmd)
		{
			OracleDataAdapter da = new OracleDataAdapter ();
			da.SelectCommand = (OracleCommand)cmd;
			return da;
		}

		public override IDbCommand CreateCommand (string sql)
		{
			OracleCommand command = new OracleCommand ();
			command.CommandText = sql;
			command.CommandTimeout = _commandTimeOut;
			return command;
		}


		public override IDbCommand CreateCommand ()
		{
			OracleCommand command = new OracleCommand ();
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction)
		{
			string parameterName = name;
			if (!parameterName.StartsWith (":")) {
				parameterName = ":" + parameterName;
			}
			OracleParameter sp = new OracleParameter (parameterName, value);
			if (value == null)
				sp.Value = DBNull.Value;
			sp.Direction = direction;
			OracleType oracletype;
			DbType dType;
			int size;
			if (!string.IsNullOrEmpty (dbType)) {
				if (ParseOracleType (dbType, out oracletype)) {
					sp.OracleType = oracletype;
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
			if (dataParmeter.ParameterName.StartsWith (":")) {
				dataParmeter.ParameterName = dataParmeter.ParameterName.Substring (1);
			}
		}

		#endregion


		bool ParseOracleType (string dbType, out OracleType type)
		{
			type = OracleType.VarChar;
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
			try {
				type = (OracleType)Enum.Parse (typeof(OracleType), typeString, true);
				return true;
			}
			catch {
				return false;
			}
		}

		public override void SetExtentArguments (string arguments)
		{
			ExtendParamsCollection extendParams = new ExtendParamsCollection (arguments);
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

			if (extendParams ["RoundScale"] != null) {
				byte roundScale;
				if (byte.TryParse (extendParams ["RoundScale"], out roundScale)) {
					OracleCommandFactory oracleFactory = _factory as OracleCommandFactory;
					oracleFactory.SetRoundScale (roundScale);
				}
			}
		}
	}
}
