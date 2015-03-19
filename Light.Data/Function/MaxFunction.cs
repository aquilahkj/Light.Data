using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class MaxFunction : AggregateFunction
    {
        //string _fieldName = null;

        DataFieldInfo _fieldinfo = null;

        internal MaxFunction(DataEntityMapping mapping, DataFieldInfo fieldInfo)
            : base(mapping)
        {
            //_fieldName = fieldName;
            _fieldinfo = fieldInfo;
        }


        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateMaxSql(_fieldinfo.FieldName);
        }

        protected override bool EqualsDetail(AggregateFunction function)
        {
            if (base.EqualsDetail(function))
            {
                MaxFunction target = function as MaxFunction;
                return this._fieldinfo.Equals(target._fieldinfo);
            }
            else
            {
                return false;
            }
        }

        //public override bool Equals(object obj)
        //{
        //    bool result = base.Equals(obj);
        //    if (!result)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        MaxFunction target = obj as MaxFunction;
        //        if (Object.Equals(target, null))
        //        {
        //            return false;
        //        }
        //        return Object.Equals(this._fieldinfo, target._fieldinfo);
        //    }
        //}

        //public override int GetHashCode()
        //{
        //    int hash = base.GetHashCode();
        //    hash ^= _fieldinfo.GetHashCode();
        //    return hash;
        //}
    }
}
