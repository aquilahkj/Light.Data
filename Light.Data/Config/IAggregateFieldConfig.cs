using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	interface IAggregateFieldConfig
	{
		/// <summary>
		/// 查询别名
		/// </summary>
		string Name {
			get;
			set;
		}
	}
}
