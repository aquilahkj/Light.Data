using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;
using Light.Data.Expressions;

namespace Light.Data.DataField
{
    /// <summary>
    /// 模糊匹配字段
    /// </summary>
    public class MatchDataFieldInfo : ExtendDataFieldInfo
    {
        //internal MatchDataFieldInfo(DataFieldMapping fieldMapping, bool left, bool right)
        //    : base(fieldMapping)
        //{
        //    if (fieldMapping.ObjectType != typeof(string))
        //    {
        //        throw new LightDataException(string.Format(RE.TypeUnsupportTheTransform, fieldMapping.ObjectType));
        //    }
        //    _left = left;
        //    _right = right;
        //}

        internal MatchDataFieldInfo(DataFieldInfo info, bool left, bool right)
            : base(info)
        {
            _left = left;
            _right = right;
        }

        bool _left;

        bool _right;

        //string _leftString = null;

        //string _rightString = null;

        internal override string CreateDataFieldSql(CommandFactory factory, bool isFullName)
        {
            string field = BaseFieldInfo.CreateDataFieldSql(factory, isFullName);
            return factory.CreateMatchSql(field, _left, _right);
        }

        /// <summary>
        /// 匹配细节内容是否相等
        /// </summary>
        /// <param name="info">匹配对象</param>
        /// <returns></returns>
        protected override bool EqualsDetail(DataFieldInfo info)
        {
            if (base.EqualsDetail(info))
            {
                MatchDataFieldInfo target = info as MatchDataFieldInfo;
                return this._left == target._left && this._right == target._right;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// like匹配(倒转)
        /// </summary>
        /// <param name="value">匹配值</param>
        /// <returns>查询表达式</returns>
        public QueryExpression ReverseLike(object value)
        {
            return ReverseMatchValue(value, false);
        }

        /// <summary>
        /// not like匹配(倒转)
        /// </summary>
        /// <param name="value">匹配值</param>
        /// <returns>查询表达式</returns>
        public QueryExpression ReverseNotLike(object value)
        {
            return ReverseMatchValue(value, true);
        }

        private QueryExpression ReverseMatchValue(object value, bool isNot)
        {
            if (value == null)
            {
                throw new LightDataException(RE.InputValueIsNotAllowNull);
            }
            QueryExpression exp = new CollectionMatchQueryExpression(this, value, true, false, isNot);
            return exp;
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
        //        MatchDataFieldInfo target = obj as MatchDataFieldInfo;
        //        if (Object.Equals(target, null))
        //        {
        //            return false;
        //        }
        //        return this._leftString == target._leftString && this._rightString == target._rightString;
        //    }
        //}

        //public override int GetHashCode()
        //{
        //    int hash = base.GetHashCode();
        //    if (_leftString != null)
        //    {
        //        hash ^= _leftString.GetHashCode();
        //        hash ^= "_leftString".GetHashCode();
        //    }
        //    if (_rightString != null)
        //    {
        //        hash ^= _rightString.GetHashCode();
        //        hash ^= "_rightString".GetHashCode();
        //    }
        //    return hash;
        //}
    }
}
