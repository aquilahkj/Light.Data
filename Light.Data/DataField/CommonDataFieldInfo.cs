using System;

namespace Light.Data
{
	abstract class CommonDataFieldInfo:DataFieldInfo
	{
		protected CommonDataFieldInfo (Type type, string name) : base (type, name)
		{
		}
	}
}

