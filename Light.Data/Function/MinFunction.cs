using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class MinFunction : AggregateFunction
    {
        //string _fieldName = null;

        DataFieldInfo _fieldinfo = null;

        internal MinFunction(DataEntityMapping mapping, DataFieldInfo fieldInfo)
            : base(mapping)
        {
            //_fieldName = fieldName;
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

        //public override bool Equals(object obj)
        //{
        //    bool result = base.Equals(obj);
        //    if (!result)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        MinFunction target = obj as MinFunction;
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
