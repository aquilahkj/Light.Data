using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
	class MssqlCommandFactory_2000 : MssqlCommandFactory
	{
		public MssqlCommandFactory_2000 ()
		{
			_canInnerPage = false;
		}

		/// <summary>
		/// 创建内容Exists查询命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="query">查询表达式</param>
		/// <returns></returns>
		public override CommandData CreateExistsCommand (DataEntityMapping mapping, QueryExpression query)
		{
			return this.CreateSelectBaseCommand (mapping, "top 1", query, null, null);
		}
	}
}
