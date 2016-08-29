using System;
namespace Light.Data
{
	interface ISelector
	{
		string [] GetSelectFiledNames ();

		//string CreateSelectString (CommandFactory factory, out DataParameter [] dataParameters);

		string CreateSelectString (CommandFactory factory, bool isFullName, CreateSqlState state);
	}
}

