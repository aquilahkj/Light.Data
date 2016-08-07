using System;
namespace Light.Data
{
	interface ISelector
	{
		string CreateSelectString (CommandFactory factory, out DataParameter [] dataParameters);
	}
}

