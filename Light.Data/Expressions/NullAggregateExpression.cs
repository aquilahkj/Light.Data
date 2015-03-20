using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
    class NullAggregateExpression : AggregateHavingExpression
    {
        AggregateFunction _function = null;

        bool _isNull = false;

        public NullAggregateExpression(AggregateFunction function, bool isNull)
            : base(function.TableMapping)
        {
            _function = function;
            _isNull = isNull;
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            string functionSql = _function.CreateSqlString(factory, out dataParameters);
            return factory.CreateNullQuerySql(functionSql, _isNull);
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
        {
            string alise = handler(_function);
            if (string.IsNullOrEmpty(alise))
            {
                return CreateSqlString(factory, out dataParameters);
            }
            string name = factory.CreateDataFieldSql(alise);
            dataParameters = new DataParameter[0];
            return factory.CreateNullQuerySql(name, _isNull);
        }

        protected override bool EqualsDetail(AggregateHavingExpression expression)
        {
            if (base.EqualsDetail(expression))
            {
                NullAggregateExpression target = expression as NullAggregateExpression;
                return this._function.Equals(target._function)
                && this._isNull == target._isNull;
            }
            else
            {
                return false;
            }
        }
    }
}
