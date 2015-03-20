using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class MinFunction : AggregateFunction
    {
        DataFieldInfo _fieldinfo = null;

        internal MinFunction(DataEntityMapping mapping, DataFieldInfo fieldInfo)
            : base(mapping)
        {
            _fieldinfo = fieldInfo;
        }


        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateMinSql(_fieldinfo.FieldName);
        }

        protected override bool EqualsDetail(AggregateFunction function)
        {
            if (base.EqualsDetail(function))
            {
                MinFunction target = function as MinFunction;
                return this._fieldinfo.Equals(target._fieldinfo);
            }
            else
            {
                return false;
            }
        }
    }
}
