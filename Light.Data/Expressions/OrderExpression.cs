using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Order expression.
	/// </summary>
	public class OrderExpression : BaseExpression
	{
		List<OrderExpression> _orderExpressions;

		internal OrderExpression (DataEntityMapping tableMapping)
		{
			TableMapping = tableMapping;
		}

		/// <summary>
		/// Catch the specified expression1 and expression2.
		/// </summary>
		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		internal static OrderExpression Catch (OrderExpression expression1, OrderExpression expression2)
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
			else if (Object.ReferenceEquals (expression1, expression2)) {
				return expression1;
			}
			else if (expression1 is RandomOrderExpression || expression2 is RandomOrderExpression) {
				return expression2;
			}
			DataEntityMapping demapping = null;
			OrderExpression newExpression = new OrderExpression (demapping);
			List<OrderExpression> list = new List<OrderExpression> ();
			if (expression1._orderExpressions == null) {
				list.Add (expression1);
			}
			else {
				list.AddRange (expression1._orderExpressions);
			}
			if (expression2._orderExpressions == null) {
				list.Add (expression2);
			}
			else {
				list.AddRange (expression2._orderExpressions);
			}
			newExpression._orderExpressions = list;
			newExpression.mutliOrder = expression1.mutliOrder | expression2.mutliOrder;
			return newExpression;
		}

		/// <param name="expression1">Expression1.</param>
		/// <param name="expression2">Expression2.</param>
		public static OrderExpression operator & (OrderExpression expression1, OrderExpression expression2)
		{
			return Catch (expression1, expression2);
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
			string [] array = new string [_orderExpressions.Count];
			List<DataParameter> list = new List<DataParameter> ();
			int len = array.Length;
			for (int i = 0; i < len; i++) {
				DataParameter [] dps;
				array [i] = _orderExpressions [i].CreateSqlString (factory, isFullName, out dps);
				if (dps != null && dps.Length > 0) {
					list.AddRange (dps);
				}
			}
			dataParameters = list.ToArray ();

			return factory.CreateCatchExpressionSql (array);
		}

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="fullFieldName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		/// <param name="handler">Handler.</param>
		//internal virtual string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		//{
		//	string[] array = new string[_orderExpressions.Count];
		//	List<DataParameter> list = new List<DataParameter> ();
		//	dataParameters = null;
		//	int len = array.Length;
		//	for (int i = 0; i < len; i++) {
		//		DataParameter[] dps;
		//		array [i] = _orderExpressions [i].CreateSqlString (factory, fullFieldName, out dps, handler);
		//		if (dps != null && dps.Length > 0) {
		//			list.AddRange (dps);
		//		}
		//	}
		//	dataParameters = list.ToArray ();
		//	return factory.CreateCatchExpressionSql (array);
		//}

		//		internal virtual string CreateSqlString (CommandFactory factory, string aliasTableName, out DataParameter[] dataParameters)
		//		{
		//			string[] array = new string[_orderExpressions.Count];
		//			List<DataParameter> list = new List<DataParameter> ();
		//			int len = array.Length;
		//			for (int i = 0; i < len; i++) {
		//				DataParameter[] dps;
		//				array [i] = _orderExpressions [i].CreateSqlString (factory, aliasTableName, out dps);
		//				list.AddRange (dps);
		//			}
		//			dataParameters = list.ToArray ();
		//
		//			return factory.CreateCatchExpressionSql (array);
		//		}

		internal virtual OrderExpression CreateAliasTableNameOrder (string aliasTableName)
		{
			OrderExpression newExpression = new OrderExpression (TableMapping);
			List<OrderExpression> list = new List<OrderExpression> ();
			foreach (OrderExpression item in list) {
				OrderExpression newitem = item.CreateAliasTableNameOrder (aliasTableName);
				list.Add (newitem);
			}
			newExpression._orderExpressions = list;
			return newExpression;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string [] array = new string [_orderExpressions.Count];
			int len = array.Length;
			for (int i = 0; i < len; i++) {
				array [i] = _orderExpressions [i].CreateSqlString (factory, isFullName, state);
			}
			return factory.CreateCatchExpressionSql (array);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Light.Data.OrderExpression"/> is equal to the current <see cref="Light.Data.OrderExpression"/>.
		/// </summary>
		/// <param name="target">The <see cref="Light.Data.OrderExpression"/> to compare with the current <see cref="Light.Data.OrderExpression"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Light.Data.OrderExpression"/> is equal to the current
		/// <see cref="Light.Data.OrderExpression"/>; otherwise, <c>false</c>.</returns>
		//public virtual bool Equals (OrderExpression target)
		//{
		//	if (Object.Equals (target, null)) {
		//		return false;
		//	}
		//	if (Object.ReferenceEquals (this, target)) {
		//		return true;
		//	}
		//	else {
		//		if (this.GetType () == target.GetType ()) {
		//			if (Object.Equals (this._orderExpressions, target._orderExpressions)) {
		//				return true;
		//			}
		//			else {
		//				if (this._orderExpressions.Count == target._orderExpressions.Count) {
		//					int len = this._orderExpressions.Count;
		//					for (int i = 0; i < len; i++) {
		//						if (!this._orderExpressions [i].Equals (target._orderExpressions [i])) {
		//							return false;
		//						}
		//					}
		//					return true;
		//				}
		//				else {
		//					return false;
		//				}
		//			}
		//		}
		//		else {
		//			return false;
		//		}
		//	}
		//}

		bool mutliOrder;

		internal bool MutliOrder {
			get {
				return mutliOrder;
			}

			set {
				mutliOrder = value;
			}
		}
	}
}
