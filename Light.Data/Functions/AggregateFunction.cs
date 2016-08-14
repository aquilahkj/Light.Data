using System;
namespace Light.Data
{
	/// <summary>
	/// Aggregate function.
	/// </summary>
	public static class AggregateFunction
	{
		/// <summary>
		/// Count Function.
		/// </summary>
		public static AggregateData Count ()
		{
			return new CountAllFunction ();
		}

		///// <summary>
		///// Count Function in the specified expression.
		///// </summary>
		///// <returns>The function</returns>
		///// <param name="expression">Expression.</param>
		//public static AggregateData Count (QueryExpression expression)
		//{
		//	if (expression == null) {
		//		throw new ArgumentNullException (nameof (expression));
		//	}
		//	return new ConditionCountFunction (expression, false);
		//}

		/// <summary>
		/// Count Function in the specified expression, fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Count (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException (nameof (expression));
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new ConditionCountFunction (fieldInfo, expression, isDistinct);
		}

		/// <summary>
		/// Count Function in the specified expression and fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Count (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			return Count (expression, fieldInfo, false);
		}

		/// <summary>
		/// Count Function in the fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Count (DataFieldInfo fieldInfo)
		{
			return Count (fieldInfo, false);
		}

		/// <summary>
		/// Count Function in the specified fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Count (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new CountFunction (fieldInfo, isDistinct);
		}

		/// <summary>
		/// Sum Function in the fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Sum (DataFieldInfo fieldInfo)
		{
			return Sum (fieldInfo, false);
		}

		/// <summary>
		/// Sum Function in the specified fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Sum (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new SumFunction (fieldInfo, isDistinct);
		}

		/// <summary>
		/// Sum Function in the specified expression and fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Sum (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			return Sum (expression, fieldInfo, false);
		}

		/// <summary>
		/// Sum Function in the specified expression, fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Sum (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException (nameof (expression));
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new ConditionSumFunction (fieldInfo, expression, isDistinct);
		}

		/// <summary>
		/// Avg Function in the fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Avg (DataFieldInfo fieldInfo)
		{
			return Avg (fieldInfo, false);
		}

		/// <summary>
		/// Avg Function in the specified fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Avg (DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new AvgFunction (fieldInfo, isDistinct);
		}

		/// <summary>
		/// Sum Function in the specified expression and fieldInfo.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Avg (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			return Avg (expression, fieldInfo, false);
		}

		/// <summary>
		/// Avg Function in the specified expression, fieldInfo and isDistinct.
		/// </summary>
		/// <returns>The function</returns>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		public static AggregateData Avg (QueryExpression expression, DataFieldInfo fieldInfo, bool isDistinct)
		{
			if (expression == null) {
				throw new ArgumentNullException (nameof (expression));
			}
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new ConditionAvgFunction (fieldInfo, expression, isDistinct);
		}

		/// <summary>
		/// Max Function in the specified fieldInfo.
		/// </summary>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Max (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new MaxFunction (fieldInfo);
		}

		/// <summary>
		/// Max Function in the specified expression and fieldInfo.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Max (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new ConditionMaxFunction (fieldInfo, expression);
		}

		/// <summary>
		/// Minimum Function in the specified fieldInfo.
		/// </summary>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Min (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new MinFunction (fieldInfo);
		}

		/// <summary>
		/// Minimum Function in the specified expression and fieldInfo.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <param name="fieldInfo">Field info.</param>
		public static AggregateData Min (QueryExpression expression, DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			return new ConditionMinFunction (fieldInfo, expression);
		}
	}
}

