using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 统计后查询表达式
	/// </summary>
	public class AggregateHavingExpression : Expression
	{
		AggregateHavingExpression _expression1 = null;

		AggregateHavingExpression _expression2 = null;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		internal AggregateHavingExpression (DataEntityMapping tableMapping)
		{
			if (tableMapping == null) {
				IgnoreConsistency = true;
			}
			else {
				TableMapping = tableMapping;
			}
		}

		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			DataParameter[] dp1 = null;
			string expressionString1 = _expression1.CreateSqlString (factory, out dp1);

			DataParameter[] dp2 = null;
			string expressionString2 = _expression2.CreateSqlString (factory, out dp2);

			dataParameters = new DataParameter[dp1.Length + dp2.Length];
			dp1.CopyTo (dataParameters, 0);
			dp2.CopyTo (dataParameters, dp1.Length);
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		internal virtual string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			DataParameter[] dp1 = null;
			string expressionString1 = _expression1.CreateSqlString (factory, out dp1, handler);

			DataParameter[] dp2 = null;
			string expressionString2 = _expression2.CreateSqlString (factory, out dp2, handler);

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

		internal static AggregateHavingExpression And (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		internal static AggregateHavingExpression Or (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// And结合
		/// </summary>
		/// <param name="expression1">表达式1</param>
		/// <param name="expression2">表达式2</param>
		/// <returns>新表达式</returns>
		public static AggregateHavingExpression operator & (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// Or结合
		/// </summary>
		/// <param name="expression1">表达式1</param>
		/// <param name="expression2">表达式2</param>
		/// <returns>新表达式</returns>
		public static AggregateHavingExpression operator | (AggregateHavingExpression expression1, AggregateHavingExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// 匹配内容是否相等
		/// </summary>
		/// <param name="target">匹配对象</param>
		/// <returns></returns>
		public virtual bool Equals (AggregateHavingExpression target)
		{
			if (Object.Equals (target, null)) {
				return false;
			}
			if (Object.ReferenceEquals (this, target)) {
				return true;
			}
			else {
				if (this.GetType () == target.GetType ()) {
					return EqualsDetail (target);
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// 匹配内容细节是否相等
		/// </summary>
		/// <param name="expression">匹配对象</param>
		/// <returns></returns>
		protected virtual bool EqualsDetail (AggregateHavingExpression expression)
		{
			if (this._expression1 != null) {
				return this._expression1.Equals (expression._expression1) && this._expression2.Equals (expression._expression2) && this._operatorType == expression._operatorType;
			}
			else {
				return Object.Equals (this.TableMapping, expression.TableMapping);
			}
		}
	}
}
