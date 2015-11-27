using System;
using System.Data;

namespace Light.Data
{
	public class CommandOutputEventArgs : EventArgs
	{
		string commandInfo;

		public string CommandInfo {
			get {
				return commandInfo;
			}
			set {
				commandInfo = value;
			}
		}

		string runnableCommand;

		public string RunnableCommand {
			get {
				return runnableCommand;
			}
			set {
				runnableCommand = value;
			}
		}
	}


	
}

