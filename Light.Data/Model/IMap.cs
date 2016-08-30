using System;
namespace Light.Data
{
	interface IMap
	{
		Type Type {
			get;
		}

		bool CheckIsField (string path);

		bool CheckIsRelateEntity (string path);

		bool CheckIsEntityCollection (string path);

		DataFieldInfo CreateFieldInfoForPath (string path);

		ISelector CreateSpecialSelector (string [] paths);


	}
}

