//using System;
//using System.Collections.Generic;

//namespace Light.Data
//{
//	class SpecialOrderExpression : BaseExpression
//	{
//		List<SpecialOrderExpression> _orderExpressions;

//		internal SpecialOrderExpression (DataEntityMapping tableMapping)
//		{
//			TableMapping = tableMapping;
//		}

//		/// <summary>
//		/// Catch the specified expression1 and expression2.
//		/// </summary>
//		/// <param name="expression1">Expression1.</param>
//		/// <param name="expression2">Expression2.</param>
//		internal static SpecialOrderExpression Catch (SpecialOrderExpression expression1, SpecialOrderExpression expression2)
//		{
//			if (expression1 == null && expression2 == null) {
//				return null;
//			}
//			else if (expression1 == null && expression2 != null) {
//				return expression2;
//			}
//			else if (expression1 != null && expression2 == null) {
//				return expression1;
//			}
//			else if (Object.ReferenceEquals (expression1, expression2)) {
//				return expression1;
//			}

//			DataEntityMapping demapping = null;
//			SpecialOrderExpression newExpression = new SpecialOrderExpression (demapping);
//			List<SpecialOrderExpression> list = new List<SpecialOrderExpression> ();
//			if (expression1._orderExpressions == null) {
//				list.Add (expression1);
//			}
//			else {
//				list.AddRange (expression1._orderExpressions);
//			}
//			if (expression2._orderExpressions == null) {
//				list.Add (expression2);
//			}
//			else {
//				list.AddRange (expression2._orderExpressions);
//			}
//			newExpression._orderExpressions = list;
//			newExpression.mutliOrder = expression1.mutliOrder | expression2.mutliOrder;
//			return newExpression;
//		}

//		/// <param name="expression1">Expression1.</param>
//		/// <param name="expression2">Expression2.</param>
//		public static SpecialOrderExpression operator & (SpecialOrderExpression expression1, SpecialOrderExpression expression2)
//		{
//			return Catch (expression1, expression2);
//		}

//		/// <summary>
//		/// Creates the sql string.
//		/// </summary>
//		/// <returns>The sql string.</returns>
//		/// <param name="factory">Factory.</param>
//		/// <param name="isFullName">If set to <c>true</c> full field name.</param>
//		/// <param name="dataParameters">Data parameters.</param>
//		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
//		{
//			string [] array = new string [_orderExpressions.Count];
//			List<DataParameter> list = new List<DataParameter> ();
//			int len = array.Length;
//			for (int i = 0; i < len; i++) {
//				DataParameter [] dps;
//				array [i] = _orderExpressions [i].CreateSqlString (factory, isFullName, out dps);
//				if (dps != null && dps.Length > 0) {
//					list.AddRange (dps);
//				}
//			}
//			dataParameters = list.ToArray ();
//			return factory.CreateCatchExpressionSql (array);
//		}

//		bool mutliOrder;

//		internal bool MutliOrder {
//			get {
//				return mutliOrder;
//			}

//			set {
//				mutliOrder = value;
//			}
//		}
//	}
//}

