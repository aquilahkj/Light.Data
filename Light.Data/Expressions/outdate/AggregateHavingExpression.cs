using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate having expression.
	/// </summary>
	public class AggregateHavingExpression : LightExpression
	{
		AggregateHavingExpression _expression1;

		AggregateHavingExpression _expression2;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AggregateHavingExpression"/> class.
		/// </summary>
		/// <param name="tableMapping">Table mapping.</param>
		internal AggregateHavingExpression (DataEntityMapping tableMapping)
		{
			TableMapping = tableMapping;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string expressionString1 = _expression1.CreateSqlString (factory, isFullName, state);
			string expressionString2 = _expression2.CreateSqlString (factory, isFullName, state);
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		private static AggregateHavingExpression Catch (AggregateHavingExpression expression1, CatchOperatorsType operatorType, AggregateHavingExpression expression2)
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

			AggregateHavingExpression newExpression = new AggregateHavingExpression (expression1.TableMapping);
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
		internal static AggregateHavingExpression And (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// Or the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static AggregateHavingExpression Or (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static AggregateHavingExpression operator & (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static AggregateHavingExpression operator | (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}
	}
}
