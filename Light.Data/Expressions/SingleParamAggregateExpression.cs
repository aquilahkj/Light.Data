using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class SingleParamAggregateExpression : AggregateHavingExpression
	{
		AggregateFunction _function = null;

		QueryPredicate _predicate;

		object _value = null;

		bool _isReverse = false;

		public SingleParamAggregateExpression (AggregateFunction function, QueryPredicate predicate, object value, bool isReverse)
			: base (function.TableMapping)
		{
			_function = function;
			_predicate = predicate;
			_value = value;
			_isReverse = isReverse;
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _value, null);
			List<DataParameter> list = new List<DataParameter> ();
			DataParameter[] ps = null;
			string functionSql = _function.CreateSqlString (factory, out ps);
			list.AddRange (ps);
			list.Add (dataParameter);
			dataParameters = list.ToArray ();
			return factory.CreateSingleParamSql (functionSql, _predicate, _isReverse, dataParameter);
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, out dataParameters);
			}
			string name = factory.CreateDataFieldSql (alise);

			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _value, null);
			dataParameters = new DataParameter[] { dataParameter };
			return factory.CreateSingleParamSql (name, _predicate, _isReverse, dataParameter);
		}

		protected override bool EqualsDetail (AggregateHavingExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				SingleParamAggregateExpression target = expression as SingleParamAggregateExpression;
				return this._function.Equals (target._function)
				&& Object.Equals (this._value, target._value)
				&& this._predicate == target._predicate
				&& this._isReverse == target._isReverse;
			}
			else {
				return false;
			}
		}
	}
}
