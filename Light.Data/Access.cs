using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using Light.Data.Mappings;
namespace Light.Data
{
    class Access : Database
    {
        public Access()
        {
            _factory = new AccessCommandFactory(this);
        }

        #region IDatabase 成员

        public override IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        public override IDbConnection CreateConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }

        public override IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = (OleDbCommand)cmd;
            return da;
        }

        public override IDbCommand CreateCommand(string sql)
        {
            OleDbCommand command = new OleDbCommand();
            command.CommandText = sql;
            command.CommandTimeout = _commandTimeOut;
            return command;
        }

        public override IDbCommand CreateCommand()
        {
            OleDbCommand command = new OleDbCommand();
            command.CommandTimeout = _commandTimeOut;
            return command;
        }

        public override IDataParameter CreateParameter(string name, object value, string dbType, ParameterDirection direction)
        {
            OleDbParameter sp = new OleDbParameter("?", value);
            if (value == null)
                sp.Value = DBNull.Value;
            sp.Direction = direction;
            OleDbType oletype;
            DbType dType;
            int size;
            if (!string.IsNullOrEmpty(dbType))
            {
                if (ParseOleDbType(dbType, out oletype))
                {
                    sp.OleDbType = oletype;
                }
                else if (Utility.ParseDbType(dbType, out dType))
                {
                    sp.DbType = dType;
                }
                if (Utility.ParseSize(dbType, out size))
                {
                    sp.Size = size;
                }
            }
            return sp;
        }

        public override void FormatStoredProcedureParameter(IDataParameter dataParmeter)
        {

        }

        #endregion

        bool ParseOleDbType(string dbType, out OleDbType type)
        {
            type = OleDbType.VarChar;
            int index = dbType.IndexOf('(');
            string typeString = string.Empty;
            if (index < 0)
            {
                typeString = dbType;
            }
            else if (index == 0)
            {
                return false;
            }
            else
            {
                typeString = dbType.Substring(0, index);
            }

            if (typeString.Equals("datetime", StringComparison.OrdinalIgnoreCase))
            {
                type = OleDbType.Date;
                return true;
            }

            try
            {
                type = (OleDbType)Enum.Parse(typeof(OleDbType), typeString, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void SetExtentArguments(string arguments)
        {
            ExtendParamsCollection extendParams = new ExtendParamsCollection(arguments);
            if (extendParams["CompatibleSQL92"] != null)
            {
                if (extendParams["CompatibleSQL92"].ToLower() == "false")
                {
                    AccessCommandFactory accessFactory = _factory as AccessCommandFactory;
                    accessFactory.UseAccessWildcards();
                }
            }

            if (extendParams["TimeOut"] != null)
            {
                int timeout;
                if (int.TryParse(extendParams["TimeOut"], out timeout))
                {
                    CommandTimeOut = timeout;
                }
            }
        }
    }
}
