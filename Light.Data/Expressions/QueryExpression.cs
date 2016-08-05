using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Query expression.
	/// </summary>
	public class QueryExpression : BaseExpression
	{
		QueryExpression _expression1;

		QueryExpression _expression2;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		internal QueryExpression (DataEntityMapping tableMapping)
		{
			TableMapping = tableMapping;
		}

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="fullFieldName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			DataParameter [] dp1;
			string expressionString1 = _expression1.CreateSqlString (factory, fullFieldName, out dp1);

			DataParameter [] dp2;
			string expressionString2 = _expression2.CreateSqlString (factory, fullFieldName, out dp2);

			if (dp1 == null && dp2 == null) {
				dataParameters = null;
			}
			else if (dp1 != null && dp2 == null) {
				dataParameters = dp1;
			}
			else if (dp1 == null && dp2 != null) {
				dataParameters = dp2;
			}
			else {
				List<DataParameter> list = new List<DataParameter> ();
				list.AddRange (dp1);
				list.AddRange (dp2);
				dataParameters = list.ToArray ();
			}
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		/// <summary>
		/// Catch the specified expression1, operatorType and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="operatorType">Operator type.</param>
		/// <param name="expression2">Expression2.</param>
		internal static QueryExpression Catch (QueryExpression expression1, CatchOperatorsType operatorType, QueryExpression expression2)
		{
			if (expression1 == null && expression2 == null) {
				return null;
			}
			else if (expression1 == null && expression2 != null) {
				return expression2;
			}
			else if (expression1 != null && expression2 == null) {
				return expression1;
			}
			DataEntityMapping demapping = null;
			if (expression1.TableMapping != null) {
				demapping = expression1.TableMapping;
			}
			else if (expression2.TableMapping != null) {
				demapping = expression2.TableMapping;
			}
			QueryExpression newExpression = new QueryExpression (demapping);
			newExpression._expression1 = expression1;
			newExpression._expression2 = expression2;
			newExpression._operatorType = operatorType;
			newExpression.mutliQuery = expression1.mutliQuery | expression2.mutliQuery;
			return newExpression;
		}

		/// <summary>
		/// And the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static QueryExpression And (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// Or the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static QueryExpression Or (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static QueryExpression operator & (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static QueryExpression operator | (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// Exists specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public static QueryExpression Exists (QueryExpression expression)
		{
			return new ExistsQueryExpression (expression, false);
		}

		/// <summary>
		/// Not exists specified expression.
		/// </summary>
		/// <returns>The exists.</returns>
		/// <param name="expression">Expression.</param>
		public static QueryExpression NotExists (QueryExpression expression)
		{
			return new ExistsQueryExpression (expression, true);
		}

		/// <summary>
		/// Not the specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public static QueryExpression Not (QueryExpression expression)
		{
			return new LambdaNotQueryExpression (expression);
		}

		bool mutliQuery;

		internal bool MutliQuery {
			get {
				return mutliQuery;
			}

			set {
				mutliQuery = value;
			}
		}


		///// <summary>
		///// Determines whether the specified <see cref="Light.Data.QueryExpression"/> is equal to the current <see cref="Light.Data.QueryExpression"/>.
		///// </summary>
		///// <param name="target">The <see cref="Light.Data.QueryExpression"/> to compare with the current <see cref="Light.Data.QueryExpression"/>.</param>
		///// <returns><c>true</c> if the specified <see cref="Light.Data.QueryExpression"/> is equal to the current
		///// <see cref="Light.Data.QueryExpression"/>; otherwise, <c>false</c>.</returns>
		//public virtual bool Equals (QueryExpression target)
		//{
		//	if (Object.Equals (target, null)) {
		//		return false;
		//	}
		//	if (Object.ReferenceEquals (this, target)) {
		//		return true;
		//	}
		//	else {
		//		if (this.GetType () == target.GetType ()) {
		//			return EqualsDetail (target);
		//		}
		//		else {
		//			return false;
		//		}
		//	}
		//}
		///// <summary>
		///// Equalses the detail.
		///// </summary>
		///// <returns><c>true</c>, if detail was equalsed, <c>false</c> otherwise.</returns>
		///// <param name="expression">Expression.</param>
		//protected virtual bool EqualsDetail (QueryExpression expression)
		//{
		//	if (this._expression1 != null) {
		//		return this._expression1.Equals (expression._expression1) && this._expression2.Equals (expression._expression2) && this._operatorType == expression._operatorType;
		//	}
		//	else {
		//		return Object.Equals (this.TableMapping, expression.TableMapping);
		//	}
		//}
	}
}
