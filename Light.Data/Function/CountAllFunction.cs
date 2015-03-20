using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class CountAllFunction : AggregateFunction
    {
        internal CountAllFunction()
            : base(null)
        {

        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateCountAllSql();
        }

        protected override bool EqualsDetail(AggregateFunction function)
        {
            return true;
        }
    }
}
