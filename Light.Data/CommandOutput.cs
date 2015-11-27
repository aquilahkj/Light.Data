using System;
using System.Data;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// Command output.
	/// </summary>
	public class CommandOutput:ICommandOutput
	{
		/// <summary>
		/// Occurs when on command output.
		/// </summary>
		public event CommandOutputEventHandle OnCommandOutput;

		bool outputFullCommand;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Light.Data.CommandOutput"/> output runnable command.
		/// </summary>
		/// <value><c>true</c> if output runnable command; otherwise, <c>false</c>.</value>
		public bool OutputFullCommand {
			get {
				return outputFullCommand;
			}
			set {
				outputFullCommand = value;
			}
		}

		bool useConsoleOutput;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Light.Data.CommandOutput"/> use console output.
		/// </summary>
		/// <value><c>true</c> if use console output; otherwise, <c>false</c>.</value>
		public bool UseConsoleOutput {
			get {
				return useConsoleOutput;
			}
			set {
				useConsoleOutput = value;
			}
		}

		bool enable;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Light.Data.CommandOutput"/> is enable.
		/// </summary>
		/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
		public bool Enable {
			get {
				return enable;
			}
			set {
				enable = value;
			}
		}

		public CommandOutput () : this (true)
		{

		}

		public CommandOutput (bool defaultEnable)
		{
			this.enable = defaultEnable;
		}

		#region ICommandOutput implementation

		/// <summary>
		/// Output the specified action, command, datas, commandType, isTransaction and level.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="command">Command.</param>
		/// <param name="datas">Datas.</param>
		/// <param name="commandType">Command type.</param>
		/// <param name="isTransaction">If set to <c>true</c> is transaction.</param>
		/// <param name="level">Level.</param>
		public void Output (string action, string command, DataParameter[] datas, CommandType commandType, bool isTransaction, SafeLevel level)
		{
			if (this.enable && (OnCommandOutput != null || this.useConsoleOutput)) {
				StringBuilder sb = new StringBuilder ();
				sb.AppendLine ("action:" + action);
				sb.AppendLine ("type:" + commandType);
				sb.AppendLine ("level:" + level);
				sb.AppendLine ("transaction:" + (isTransaction ? "true" : "false"));
				if (datas != null && datas.Length > 0) {
					sb.AppendLine ("parameter:");
					foreach (DataParameter data in datas) {
						sb.AppendLine (string.Format ("{2},{3},{0}={1}", data.ParameterName, data.Value, data.Direction, data.DbType));
					}
				}
				sb.AppendLine ("command:");
				sb.AppendLine (command);
				string commandInfo = sb.ToString ();
				string runnableCommand = null;
				if (this.outputFullCommand) {
					if (datas != null && datas.Length > 0) {
						string temp = command;
						foreach (DataParameter data in datas) {
							string value = null;
							TypeCode code = Type.GetTypeCode (data.Value.GetType ());
							if (code == TypeCode.String || code == TypeCode.Char) {
								string content = data.Value.ToString ();
								content = content.Replace ("'", "''");
								value = "'" + content + "'";
							}
							else if (code == TypeCode.DateTime) {
								DateTime dt = (DateTime)data.Value;
								value = "'" + dt.ToString ("u") + "'";
							}
							else {
								value = data.Value.ToString ();
							}
							temp = temp.Replace (data.ParameterName, value);
						}
						runnableCommand = temp;
					}
					else {
						runnableCommand = command;
					}
				}

				if (OnCommandOutput != null) {
					CommandOutputEventArgs args = new CommandOutputEventArgs ();
					args.CommandInfo = commandInfo;
					args.RunnableCommand = runnableCommand;
					OnCommandOutput (this, args);
				}
				if (this.useConsoleOutput) {
					if (runnableCommand != null) {
						sb.AppendLine ("--------------------");
						sb.AppendLine (runnableCommand);
					}
					Console.WriteLine (sb);
				}
			}
		}

		#endregion
	}
}

