using System.Data;

namespace Light.Data
{
	/// <summary>
	/// ICommand output.
	/// </summary>
	public interface ICommandOutput
	{
		/// <summary>
		/// Output the specified action, command, datas, commandType, isTransaction and level.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="command">Command.</param>
		/// <param name="datas">Datas.</param>
		/// <param name="commandType">Command type.</param>
		/// <param name="isTransaction">If set to <c>true</c> is transaction.</param>
		/// <param name="level">Level.</param>
		void Output (string action, string command, DataParameter[] datas, CommandType commandType, bool isTransaction, SafeLevel level);
	}
}

