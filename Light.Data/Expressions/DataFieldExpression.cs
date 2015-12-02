using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Data field expression.
	/// </summary>
	public class DataFieldExpression:Expression
	{
		DataFieldExpression _expression1 = null;

		DataFieldExpression _expression2 = null;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			DataParameter[] dp1 = null;
			string expressionString1 = _expression1.CreateSqlString (factory, fullFieldName, out dp1);
			DataParameter[] dp2 = null;
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

		internal static DataFieldExpression And (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		internal static DataFieldExpression Or (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// 与连接
		/// </summary>
		/// <param name="expression1"></param>
		/// <param name="expression2"></param>
		/// <returns></returns>
		public static DataFieldExpression operator & (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// 或连接
		/// </summary>
		/// <param name="expression1"></param>
		/// <param name="expression2"></param>
		/// <returns></returns>
		public static DataFieldExpression operator | (DataFieldExpression expression1, DataFieldExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		//		/// <summary>
		//		/// 匹配内容是否相等
		//		/// </summary>
		//		/// <param name="target">匹配对象</param>
		//		/// <returns></returns>
		//		public virtual bool Equals (JoinOnExpression target)
		//		{
		//			if (Object.Equals (target, null)) {
		//				return false;
		//			}
		//			if (Object.ReferenceEquals (this, target)) {
		//				return true;
		//			}
		//			else {
		//				if (this.GetType () == target.GetType ()) {
		//					return EqualsDetail (target);
		//				}
		//				else {
		//					return false;
		//				}
		//			}
		//		}
		//
		//		/// <summary>
		//		/// 匹配对象细节是否相等
		//		/// </summary>
		//		/// <param name="expression">匹配对象</param>
		//		/// <returns></returns>
		//		protected virtual bool EqualsDetail (JoinOnExpression expression)
		//		{
		//			if (this._expression1 != null) {
		//				return this._expression1.Equals (expression._expression1) && this._expression2.Equals (expression._expression2) && this._operatorType == expression._operatorType;
		//			}
		//			else {
		////				return Object.Equals (this.TableMapping, expression.TableMapping);
		//			}
		//		}
	}
}

