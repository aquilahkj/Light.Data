using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// Match data field info.
	/// </summary>
	public class MatchDataFieldInfo : ExtendDataFieldInfo
	{
		internal MatchDataFieldInfo (DataFieldInfo info, bool starts, bool ends)
			: base (info)
		{
			_starts = starts;
			_ends = ends;
		}

		bool _starts;

		bool _ends;

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateMatchSql (field, _starts, _ends);
		}

		/// <summary>
		/// Equalses the detail.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="info">Info.</param>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				MatchDataFieldInfo target = info as MatchDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return this._starts == target._starts && this._ends == target._ends;
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}

		internal override string DBType {
			get {
				return "string";
			}
		}

		/// <summary>
		/// Reverses like the specified value.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="value">Value.</param>
		public QueryExpression ReverseLike (string value)
		{
			return ReverseMatchValue (value, false);
		}

		/// <summary>
		/// Reverses like the specified values.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseLike (params string[] values)
		{
			return ReverseMatchValue (values, false);
		}

		/// <summary>
		/// Reverses like the specified values.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseLike (IEnumerable<string> values)
		{
			return ReverseMatchValue (values, false);
		}

		/// <summary>
		/// Reverses not like the specified value.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="value">Value.</param>
		public QueryExpression ReverseNotLike (string value)
		{
			return ReverseMatchValue (value, true);
		}

		/// <summary>
		/// Reverses not like the specified values.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseNotLike (params string[] values)
		{
			return ReverseMatchValue (values, true);
		}

		/// <summary>
		/// Reverses not like the specified values.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="values">Value.</param>
		public QueryExpression ReverseNotLike (IEnumerable<string> values)
		{
			return ReverseMatchValue (values, true);
		}

		private QueryExpression ReverseMatchValue (string value, bool isNot)
		{
			if (value == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, value, true, false, false, isNot);
			return exp;
		}

		private QueryExpression ReverseMatchValue (IEnumerable<string> values, bool isNot)
		{
			if (values == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, values, true, false, false, isNot);
			return exp;
		}
	}
}
