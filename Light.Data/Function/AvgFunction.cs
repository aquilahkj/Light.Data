using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class AvgFunction : AggregateFunction
    {
        DataFieldInfo _fieldinfo = null;

        bool _isDistinct = false;

        internal AvgFunction(DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
            : base(mapping)
        {
            _fieldinfo = fieldInfo;
            _isDistinct = isDistinct;
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateAvgSql(_fieldinfo.FieldName, _isDistinct);
        }

        protected override bool EqualsDetail(AggregateFunction function)
        {
            if (base.EqualsDetail(function))
            {
                AvgFunction target = function as AvgFunction;
                return this._fieldinfo.Equals(target._fieldinfo) && this._isDistinct == target._isDistinct;
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
        //        AvgFunction target = obj as AvgFunction;
        //        if (Object.Equals(target, null))
        //        {
        //            return false;
        //        }
        //        return Object.Equals(this._fieldinfo, target._fieldinfo) && this._isDistinct == target._isDistinct;
        //    }
        //}

        //public override int GetHashCode()
        //{
        //    int hash = base.GetHashCode();
        //    hash ^= _fieldinfo.GetHashCode();
        //    hash ^= _isDistinct.GetHashCode();
        //    return hash;
        //}
    }
}
