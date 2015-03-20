using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;
using Light.Data.Expressions;

namespace Light.Data.DataField
{
	/// <summary>
	/// 模糊匹配字段
	/// </summary>
	public class MatchDataFieldInfo : ExtendDataFieldInfo
	{
		internal MatchDataFieldInfo (DataFieldInfo info, bool left, bool right)
			: base (info)
		{
			_left = left;
			_right = right;
		}

		bool _left;

		bool _right;

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateMatchSql (field, _left, _right);
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
				return this._left == target._left && this._right == target._right;
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
		public QueryExpression ReverseLike (object value)
		{
			return ReverseMatchValue (value, false);
		}

		/// <summary>
		/// not like匹配(倒转)
		/// </summary>
		/// <param name="value">匹配值</param>
		/// <returns>查询表达式</returns>
		public QueryExpression ReverseNotLike (object value)
		{
			return ReverseMatchValue (value, true);
		}

		private QueryExpression ReverseMatchValue (object value, bool isNot)
		{
			if (value == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, value, true, false, isNot);
			return exp;
		}
	}
}
