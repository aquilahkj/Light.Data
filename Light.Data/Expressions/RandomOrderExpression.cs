using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Expressions
{
	class RandomOrderExpression : OrderExpression
	{
		public RandomOrderExpression ()
			: base (null)
		{
			IgnoreConsistency = true;
		}

		public void SetTableMapping (DataEntityMapping mapping)
		{
			if (mapping == null) {
				throw new ArgumentNullException ("DataEntityMapping");
			}
			TableMapping = mapping;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping);
		}
	}
}
