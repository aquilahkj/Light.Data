using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
    class CollectionParamsAggregateExpression : AggregateHavingExpression
    {
        AggregateFunction _function = null;

        QueryCollectionPredicate _predicate;

        IEnumerable _values = null;

        public CollectionParamsAggregateExpression(AggregateFunction function, QueryCollectionPredicate predicate, IEnumerable values)
            : base(function.TableMapping)
        {
            _function = function;
            _predicate = predicate;
            _values = values;
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            List<DataParameter> list = new List<DataParameter>();

            DataParameter[] ps = null;
            string functionSql = _function.CreateSqlString(factory, out ps);
            list.AddRange(ps);

            foreach (object value in _values)
            {
                string pn = factory.CreateTempParamName();
                list.Add(new DataParameter(pn, value, null));
            }

            dataParameters = list.ToArray();
            return factory.CreateCollectionParamsQuerySql(functionSql, _predicate, list);
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
        {
            string alise = handler(_function);
            if (string.IsNullOrEmpty(alise))
            {
                return CreateSqlString(factory, out dataParameters);
            }
            string name = factory.CreateDataFieldSql(alise);

            List<DataParameter> list = new List<DataParameter>();
            foreach (object value in _values)
            {
                string pn = factory.CreateTempParamName();
                list.Add(new DataParameter(pn, value, null));
            }
            dataParameters = list.ToArray();
            return factory.CreateCollectionParamsQuerySql(name, _predicate, list);
        }

        protected override bool EqualsDetail(AggregateHavingExpression expression)
        {
            if (base.EqualsDetail(expression))
            {
                CollectionParamsAggregateExpression target = expression as CollectionParamsAggregateExpression;
                return this._function.Equals(target._function)
                && this._predicate == target._predicate
                && Utility.EnumableObjectEquals(this._values, target._values);
            }
            else
            {
                return false;
            }
        }
    }
}
