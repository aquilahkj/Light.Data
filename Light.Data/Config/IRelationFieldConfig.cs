using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
    interface IRelationFieldConfig
    {
		RelationKey[]  GetRelationKeys();

		/// <summary>
		/// 关联数据表对应字段数
		/// </summary>
		/// <value>The relation key count.</value>
        int RelationKeyCount
        {
            get;
        }

        /// <summary>
        ///  关联数据表对应字段名
        /// </summary>
//        string PropertyName
//        {
//            get;
//        }

    }
}
