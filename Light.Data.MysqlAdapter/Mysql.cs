﻿using System;
using System.Data;
using MySql.Data.MySqlClient;
 
namespace Light.Data.MysqlAdapter
{
	class Mysql : Database
	{
		public Mysql ()
		{
			_factory = new MysqlCommandFactory ();
		}

		#region IDatabase 成员

		public override IDbConnection CreateConnection ()
		{
			return new MySqlConnection (); 
		}

		public override IDbConnection CreateConnection (string connectionString)
		{
			return new MySqlConnection (connectionString);
		}

		public override IDbDataAdapter CreateDataAdapter (IDbCommand cmd)
		{
			MySqlDataAdapter da = new MySqlDataAdapter ();
			da.SelectCommand = (MySqlCommand)cmd;
			return da;
		}

		public override IDbCommand CreateCommand (string sql)
		{
			MySqlCommand command = new MySqlCommand ();
			command.CommandText = sql;
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDbCommand CreateCommand ()
		{
			MySqlCommand command = new MySqlCommand ();
			command.CommandTimeout = _commandTimeOut;
			return command;
		}

		public override IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction)
		{
			string parameterName = name;
			if (!parameterName.StartsWith ("?")) {
				parameterName = "?" + parameterName;
			}
			MySqlParameter sp = new MySqlParameter (parameterName, value);
			if (value == null)
				sp.Value = DBNull.Value;
			sp.Direction = direction;
			MySqlDbType sqltype;
			DbType dType;
			int size;
			if (!string.IsNullOrEmpty (dbType)) {
				if (ParseSqlDbType (dbType, out sqltype)) {
					sp.MySqlDbType = sqltype;
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
			if (dataParmeter.ParameterName.StartsWith ("?")) {
				dataParmeter.ParameterName = dataParmeter.ParameterName.TrimStart ('?');
			}
		}

		#endregion

		bool ParseSqlDbType (string dbType, out MySqlDbType type)
		{
			type = MySqlDbType.VarChar;
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
			return Enum.TryParse<MySqlDbType> (typeString, true, out type);
		}

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

