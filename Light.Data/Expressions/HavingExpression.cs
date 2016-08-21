using System;
namespace Light.Data
{
	class HavingExpression : BaseExpression
	{
		HavingExpression _expression1;

		HavingExpression _expression2;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		internal HavingExpression (DataEntityMapping tableMapping)
		{
			TableMapping = tableMapping;
		}

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			DataParameter [] dp1;
			string expressionString1 = _expression1.CreateSqlString (factory, isFullName, out dp1);

			DataParameter [] dp2;
			string expressionString2 = _expression2.CreateSqlString (factory, isFullName, out dp2);

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
				dataParameters = new DataParameter [dp1.Length + dp2.Length];
				dp1.CopyTo (dataParameters, 0);
				dp2.CopyTo (dataParameters, dp1.Length);
			}
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string expressionString1 = _expression1.CreateSqlString (factory, isFullName, state);
			string expressionString2 = _expression2.CreateSqlString (factory, isFullName, state);
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		private static HavingExpression Catch (HavingExpression expression1, CatchOperatorsType operatorType, HavingExpression expression2)
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
			if (expression1.TableMapping != expression2.TableMapping) {
				throw new LightDataException (RE.DataMappingOfExpressionIsNotMatch);
			}

			HavingExpression newExpression = new HavingExpression (expression1.TableMapping);
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
		internal static HavingExpression And (HavingExpression expression1, HavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// Or the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static HavingExpression Or (HavingExpression expression1, HavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static HavingExpression operator & (HavingExpression expression1, HavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static HavingExpression operator | (HavingExpression expression1, HavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}
	}
}

