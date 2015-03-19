using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
    /// <summary>
    /// 基类表达式
    /// </summary>
    public abstract class Expression
    {
        internal DataEntityMapping TableMapping
        {
            get;
            set;
        }

        /// <summary>
        /// 是否检查表达式与查询表的一致性
        /// </summary>
        public bool IgnoreConsistency
        {
            get;
            protected set;
        }

        internal abstract string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters);

        //public virtual bool Equals(Expression target)
        //{
        //    if (Object.Equals(target, null))
        //    {
        //        return false;
        //    }
        //    if (Object.ReferenceEquals(this, target))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        if (this.GetType() == target.GetType())
        //        {
        //            return EqualsDetail(target);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //protected virtual bool EqualsDetail(Expression expression)
        //{
        //    return Object.Equals(this.TableMapping, expression.TableMapping);
        //}
    }
}
