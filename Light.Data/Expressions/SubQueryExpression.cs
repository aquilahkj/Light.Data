using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.Expressions
{
    class SubQueryExpression : QueryExpression
    {
        DataFieldInfo _fieldInfo = null;

        QueryCollectionPredicate _predicate;

        QueryExpression _queryExpression = null;

        DataFieldInfo _queryFieldInfo = null;

        public SubQueryExpression(DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, DataFieldInfo queryFieldInfo, QueryExpression queryExpression)
            : base(fieldInfo.TableMapping)
        {
            _fieldInfo = fieldInfo;
            _predicate = predicate;
            _queryFieldInfo = queryFieldInfo;
            _queryExpression = queryExpression;
            //if (_queryFieldInfo.TableMapping != queryExpression.TableMapping)
            //{
            //    throw new LightDataException(RE.DataMappingIsNotMatchQueryExpression);
            //}
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            string queryString = null;
            if (_queryExpression == null)
            {
                dataParameters = new DataParameter[0];
            }
            else
            {
                queryString = _queryExpression.CreateSqlString(factory, out dataParameters);
            }
            return factory.CreateSubQuerySql(_fieldInfo.CreateDataFieldSql(factory), _predicate, _queryFieldInfo.CreateDataFieldSql(factory), factory.CreateDataTableSql(_queryFieldInfo.TableMapping), queryString);
        }

        protected override bool EqualsDetail(QueryExpression expression)
        {
            if (base.EqualsDetail(expression))
            {
                SubQueryExpression target = expression as SubQueryExpression;
                return this._fieldInfo.Equals(target._fieldInfo)
                && this._predicate == target._predicate
                && this._queryExpression.Equals(target._queryExpression)
                && this._queryFieldInfo.Equals(target._queryFieldInfo);
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
        //        SubQueryExpression target = obj as SubQueryExpression;
        //        if (Object.Equals(target, null))
        //        {
        //            return false;
        //        }
        //        return Object.Equals(this._fieldInfo, target._fieldInfo)
        //        && this._predicate == target._predicate
        //        && Object.Equals(this._queryExpression, target._queryExpression)
        //        && Object.Equals(this._queryFieldInfo, target._queryFieldInfo);
        //    }
        //}
    }
}
