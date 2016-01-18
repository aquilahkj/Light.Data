using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 模糊匹配字段
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
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
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

		/// <summary>
		/// like匹配(倒转)
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression ReverseLike (string value)
		{
			return ReverseMatchValue (value, false);
		}

		/// <summary>
		/// Reverses the like.
		/// </summary>
		/// <returns>The like.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseLike (params string[] values)
		{
			return ReverseMatchValue (values, false);
		}

		/// <summary>
		/// Reverses the like.
		/// </summary>
		/// <returns>The like.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseLike (IEnumerable<string> values)
		{
			return ReverseMatchValue (values, false);
		}

		/// <summary>
		/// not like匹配(倒转)
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression ReverseNotLike (string value)
		{
			return ReverseMatchValue (value, true);
		}

		/// <summary>
		/// Reverses the not like.
		/// </summary>
		/// <returns>The not like.</returns>
		/// <param name="values">Values.</param>
		public QueryExpression ReverseNotLike (params string[] values)
		{
			return ReverseMatchValue (values, true);
		}

		/// <summary>
		/// Reverses the not like.
		/// </summary>
		/// <returns>The not like.</returns>
		/// <param name="values">Values.</param>
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
