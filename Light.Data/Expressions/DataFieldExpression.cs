using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Data field expression.
	/// </summary>
	public class DataFieldExpression:Expression
	{
		DataFieldExpression _expression1;

		DataFieldExpression _expression2;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="fullFieldName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			DataParameter[] dp1;
			string expressionString1 = _expression1.CreateSqlString (factory, fullFieldName, out dp1);
			DataParameter[] dp2;
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

		private static DataFieldExpression Catch (DataFieldExpression expression1, CatchOperatorsType operatorType, DataFieldExpression expression2)
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
			DataFieldExpression newExpression = new DataFieldExpression ();
			newExpression._expression1 = expression1;
			newExpression._expression2 = expression2;
			newExpression._operatorType = operatorType;
			return newExpression;
		}

		/// <summary>
		/// And the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static DataFieldExpression And (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// Or the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static DataFieldExpression Or (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static DataFieldExpression operator & (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static DataFieldExpression operator | (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// Converts the query expression.
		/// </summary>
		/// <returns>The query expression.</returns>
		protected virtual QueryExpression ConvertQueryExpression ()
		{
			QueryExpression query1 = _expression1.ConvertQueryExpression ();
			QueryExpression query2 = _expression2.ConvertQueryExpression ();
			return QueryExpression.Catch (query1, _operatorType, query2);
		}

		/// <param name="expression">Expression.</param>
		public static implicit operator QueryExpression (DataFieldExpression expression)
		{
			return expression.ConvertQueryExpression ();
		}

	}
}

