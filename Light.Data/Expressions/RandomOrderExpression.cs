using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class RandomOrderExpression : OrderExpression
	{
		public RandomOrderExpression (DataEntityMapping tableMapping)
			: base (tableMapping)
		{
//			IgnoreConsistency = true;
		}

		public void SetTableMapping (DataEntityMapping mapping)
		{
			if (mapping == null) {
				throw new ArgumentNullException ("mapping");
			}
			TableMapping = mapping;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping, fullFieldName);
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping, fullFieldName);
		}
	}
}
