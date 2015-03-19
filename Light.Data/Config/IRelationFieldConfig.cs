using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
    interface IRelationFieldConfig
    {
        ///// <summary>
        ///// 关联键值字典
        ///// </summary>
        //Dictionary<string, string> RelationKeys
        //{
        //    get;
        //}

        IEnumerable<RelationKey> GetRelationKeys();

        int RelationKeyCount
        {
            get;
        }

        /// <summary>
        ///  关联数据表对应字段名
        /// </summary>
        string PropertyName
        {
            get;
        }

    }
}
