using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Light.Data.Mappings;

namespace Light.Data
{
	class Mssql : Database
	{
		public Mssql ()
		{
			_factory = new MssqlCommandFactory (this);
		}

		#region IDatabase 成员

		public override IDbConnection CreateConnection ()
		{
			return new SqlConnection ();
		}

		public override IDbConnection CreateConnection (string connectionString)
		{
			return new SqlConnection (connectionString);
		}

		public override IDbDataAdapter CreateDataAdapter (IDbCommand cmd)
		{
			SqlDataAdapter da = new SqlDataAdapter ();
			da.SelectCommand = (SqlCommand)cmd;
			return da;
		}

		public override IDbCommand CreateCommand (string sql)
		{
			SqlCommand command = new SqlCommand ();
			command.CommandText = sql;
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDbCommand CreateCommand ()
		{
			SqlCommand command = new SqlCommand ();
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction)
		{
			string parameterName = name;
			if (!parameterName.StartsWith ("@")) {
				parameterName = "@" + parameterName;
			}
			SqlParameter sp = new SqlParameter (parameterName, value);
			if (value == null)
				sp.Value = DBNull.Value;
			sp.Direction = direction;
			SqlDbType sqltype;
			DbType dType;
			int size;
			if (!string.IsNullOrEmpty (dbType)) {
				if (ParseSqlDbType (dbType, out sqltype)) {
					sp.SqlDbType = sqltype;
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

		}

		#endregion

		bool ParseSqlDbType (string dbType, out SqlDbType type)
		{
			type = SqlDbType.VarChar;
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
				type = (SqlDbType)Enum.Parse (typeof(SqlDbType), typeString, true);
				return true;
			}
			catch {
				return false;
			}
		}

		public override void SetExtentArguments (string arguments)
		{
			ExtendParamsCollection extendParams = new ExtendParamsCollection (arguments);

			if (extendParams ["Version"] != null) {
				int version;
				if (int.TryParse (extendParams ["Version"], out version)) {
					if (version == 8) {
						_factory = new MssqlCommandFactory_2000 (this);
					}
					else if (version >= 10) {
						_factory = new MssqlCommandFactory_2008 (this);
					}
				}
			}

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
