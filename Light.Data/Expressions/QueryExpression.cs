using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Expressions;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 查询表达式
	/// </summary>
	public class QueryExpression : Expression
	{
		QueryExpression _expression1 = null;

		QueryExpression _expression2 = null;

		CatchOperatorsType _operatorType = CatchOperatorsType.AND;

		internal QueryExpression (DataEntityMapping tableMapping)
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

			//dataParameters = new DataParameter[dp1.Length + dp2.Length];
			//dp1.CopyTo(dataParameters, 0);
			//dp2.CopyTo(dataParameters, dp1.Length);
			if (dp1 == null && dp2 == null) {
				dataParameters = null;
			}
			else {
				List<DataParameter> list = new List<DataParameter> ();
				list.AddRange (dp1);
				list.AddRange (dp2);
				dataParameters = list.ToArray ();
			}
			return factory.CreateCatchExpressionSql (expressionString1, expressionString2, _operatorType);
		}

		private static QueryExpression Catch (QueryExpression expression1, CatchOperatorsType operatorType, QueryExpression expression2)
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
			//if (expression1.TableMapping != expression2.TableMapping)
			//{
			//    throw new LightDataException(RE.DataMappingOfExpressionIsNotMatch);
			//}
			DataEntityMapping demapping = null;
			//如果expression1不需要检查一致性而expression2需要,则用expression2的查询表
			if (expression1.IgnoreConsistency && !expression2.IgnoreConsistency) {
				demapping = expression2.TableMapping;
			}
			else if (!expression1.IgnoreConsistency && expression2.IgnoreConsistency) {
				demapping = expression1.TableMapping;
			}
			else if (!expression1.IgnoreConsistency && !expression2.IgnoreConsistency) {
				if (expression1.TableMapping.Equals (expression2.TableMapping)) {
					demapping = expression1.TableMapping;
				}
				else {
					throw new LightDataException (RE.DataMappingOfExpressionIsNotMatch);
				}
			}
			QueryExpression newExpression = new QueryExpression (demapping);
			newExpression._expression1 = expression1;
			newExpression._expression2 = expression2;
			newExpression._operatorType = operatorType;
			return newExpression;
		}

		internal static QueryExpression And (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		internal static QueryExpression Or (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// 与连接
		/// </summary>
		/// <param name="expression1"></param>
		/// <param name="expression2"></param>
		/// <returns></returns>
		public static QueryExpression operator & (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.AND, expression2);
		}

		/// <summary>
		/// 或连接
		/// </summary>
		/// <param name="expression1"></param>
		/// <param name="expression2"></param>
		/// <returns></returns>
		public static QueryExpression operator | (QueryExpression expression1, QueryExpression expression2)
		{
			return Catch (expression1, CatchOperatorsType.OR, expression2);
		}

		/// <summary>
		/// Exists语法
		/// </summary>
		/// <param name="expression">内查询表达式</param>
		/// <returns>查询表达式</returns>
		public static QueryExpression Exists (QueryExpression expression)
		{
			return new ExistsQueryExpression (expression, false);
		}

		/// <summary>
		/// Not Exists语法
		/// </summary>
		/// <param name="expression">内查询表达式</param>
		/// <returns>查询表达式</returns>
		public static QueryExpression NotExists (QueryExpression expression)
		{
			return new ExistsQueryExpression (expression, true);
		}

		/// <summary>
		/// 匹配内容是否相等
		/// </summary>
		/// <param name="target">匹配对象</param>
		/// <returns></returns>
		public virtual bool Equals (QueryExpression target)
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
		/// 匹配对象细节是否相等
		/// </summary>
		/// <param name="expression">匹配对象</param>
		/// <returns></returns>
		protected virtual bool EqualsDetail (QueryExpression expression)
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
