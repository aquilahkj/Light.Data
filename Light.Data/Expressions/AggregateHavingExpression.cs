using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate having expression.
	/// </summary>
	public class AggregateHavingExpression : BaseExpression
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

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter[] dataParameters)
		{
			DataParameter[] dp1;
			string expressionString1 = _expression1.CreateSqlString (factory, isFullName, out dp1);

			DataParameter[] dp2;
			string expressionString2 = _expression2.CreateSqlString (factory, isFullName, out dp2);

			dataParameters = new DataParameter[dp1.Length + dp2.Length];
			dp1.CopyTo (dataParameters, 0);
			dp2.CopyTo (dataParameters, dp1.Length);
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

		/// <summary>
		/// Determines whether the specified <see cref="Light.Data.AggregateHavingExpression"/> is equal to the current <see cref="Light.Data.AggregateHavingExpression"/>.
		/// </summary>
		/// <param name="target">The <see cref="Light.Data.AggregateHavingExpression"/> to compare with the current <see cref="Light.Data.AggregateHavingExpression"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Light.Data.AggregateHavingExpression"/> is equal to the current
		/// <see cref="Light.Data.AggregateHavingExpression"/>; otherwise, <c>false</c>.</returns>
		//public virtual bool Equals (AggregateHavingExpression target)
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
		//protected virtual bool EqualsDetail (AggregateHavingExpression expression)
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
