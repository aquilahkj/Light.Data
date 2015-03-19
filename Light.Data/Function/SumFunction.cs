﻿using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Function
{
    class SumFunction : AggregateFunction
    {
        //string _fieldName = null;
        DataFieldInfo _fieldinfo = null;

        bool _isDistinct = false;

        internal SumFunction(DataEntityMapping mapping, DataFieldInfo fieldInfo, bool isDistinct)
            : base(mapping)
        {
            _fieldinfo = fieldInfo;
            //_fieldName = fieldName;
            _isDistinct = isDistinct;
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateSumSql(_fieldinfo.FieldName, _isDistinct);
        }

        protected override bool EqualsDetail(AggregateFunction function)
        {
            if (base.EqualsDetail(function))
            {
                SumFunction target = function as SumFunction;
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
        //        SumFunction target = obj as SumFunction;
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
