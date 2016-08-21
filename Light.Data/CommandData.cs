using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Light.Data
{
	class CommandData
	{
		static readonly Regex ParamNameRegex = new Regex ("_param_[a-zA-Z0-9]{32}_", RegexOptions.Compiled);

		readonly List<DataParameter> dataParameters = new List<DataParameter> ();

		//public void AddParameters (IEnumerable<DataParameter> datas)
		//{
		//	if (datas == null)
		//		throw new ArgumentNullException (nameof (datas));
		//	this.dataParameters.AddRange (datas);
		//}

		string commandText;

		object state;

		bool innerPage;

		public object State {
			get {
				return state;
			}
			set {
				state = value;
			}
		}

		public string CommandText {
			get {
				return commandText;
			}
			set {
				if (string.IsNullOrEmpty (value)) {
					throw new ArgumentNullException (nameof (value));
				}
				commandText = value;
			}
		}

		public CommandData (string commandText, IEnumerable<DataParameter> datas)
		{
			this.commandText = commandText;
			if (datas != null) {
				this.dataParameters.AddRange (datas);
			}
		}

		public CommandData (string commandText)
		{
			this.commandText = commandText;
		}

		CommandType commandType = CommandType.Text;

		public CommandType CommandType {
			get {
				return commandType;
			}
			set {
				commandType = value;
			}
		}

		bool transParamName;

		public bool TransParamName {
			get {
				return transParamName;
			}
			set {
				transParamName = value;
			}
		}

		public bool InnerPage {
			get {
				return innerPage;
			}

			set {
				innerPage = value;
			}
		}

		public IDbCommand CreateCommand (Database database)
		{
			if (database == null)
				throw new ArgumentNullException (nameof (database));
			IDataParameter [] idataParameters = null;
			string sql = this.commandText;
			int length = dataParameters.Count;
			if (length > 0) {
				idataParameters = new IDataParameter [length];
				if (this.transParamName) {
					Dictionary<string, string> paramReplaceDict = new Dictionary<string, string> ();
					for (int i = 0; i < length; i++) {
						DataParameter dp = dataParameters [i];
						IDataParameter idp = database.CreateParameter ("P" + i, dp.Value, dp.DbType, dp.Direction);
						idataParameters [i] = idp;
						paramReplaceDict.Add (dp.ParameterName, idp.ParameterName);
					}
					sql = ParamNameRegex.Replace (sql, new MatchEvaluator (delegate (Match match) {
						string value = match.Value;
						if (paramReplaceDict.ContainsKey (value)) {
							return paramReplaceDict [value];
						}
						else {
							return value;
						}
					}));
				}
				else {
					for (int i = 0; i < length; i++) {
						DataParameter dp = dataParameters [i];
						IDataParameter idp = database.CreateParameter (dp.ParameterName, dp.Value, dp.DbType, dp.Direction);
						idataParameters [i] = idp;
					}
				}
			}
			IDbCommand command = database.CreateCommand (sql);
			command.CommandType = this.commandType;
			if (idataParameters != null) {
				foreach (IDataParameter param in idataParameters) {
					command.Parameters.Add (param);
				}
			}
			return command;
		}

		public IDbCommand CreateCommand (Database database, CreateSqlState state)
		{
			if (database == null)
				throw new ArgumentNullException (nameof (database));
			IDataParameter [] idataParameters = null;
			string sql = this.commandText;
			DataParameter [] dps = state.GetDataParameters();
			int length = dps.Length;
			if (length > 0) {
				idataParameters = new IDataParameter [length];
				for (int i = 0; i < length; i++) {
					DataParameter dp = dps [i];
					IDataParameter idp = database.CreateParameter (dp.ParameterName, dp.Value, dp.DbType, dp.Direction);
					idataParameters [i] = idp;
				}
			}
			IDbCommand command = database.CreateCommand (sql);
			command.CommandType = this.commandType;
			if (idataParameters != null) {
				foreach (IDataParameter param in idataParameters) {
					command.Parameters.Add (param);
				}
			}
			return command;
		}
	}
}

