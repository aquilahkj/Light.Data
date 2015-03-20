using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
    class BooleanQueryExpression : QueryExpression
    {
        DataFieldInfo _fieldInfo = null;

        bool _isTrue = false;

        public BooleanQueryExpression(DataFieldInfo fieldInfo, bool isTrue)
            : base(fieldInfo.TableMapping)
        {
            _fieldInfo = fieldInfo;
            _isTrue = isTrue;
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            dataParameters = new DataParameter[0];
            return factory.CreateBooleanQuerySql(_fieldInfo.CreateDataFieldSql(factory), _isTrue);
        }

        protected override bool EqualsDetail(QueryExpression expression)
        {
            if (base.EqualsDetail(expression))
            {
                BooleanQueryExpression target = expression as BooleanQueryExpression;
                return this._fieldInfo.Equals(target._fieldInfo)
                && this._isTrue == target._isTrue;
            }
            else
            {
                return false;
            }
        }
    }
}
