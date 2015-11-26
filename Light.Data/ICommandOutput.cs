using System;

namespace Light.Data
{
	public interface ICommandOutput
	{
		void Output (string action, string command, DataParameter[] datas, bool isTransaction, SafeLevel level);
	}
}

