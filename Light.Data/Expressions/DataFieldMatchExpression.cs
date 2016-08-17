using System;

namespace Light.Data
{
	/// <summary>
	/// Data field match expression.
	/// </summary>
	public class DataFieldMatchExpression : DataFieldExpression
	{
		readonly DataFieldInfo leftField;

		readonly DataFieldInfo rightField;

		QueryPredicate predicate;

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.DataFieldMatchExpression"/> class.
		/// </summary>
		/// <param name="leftField">Left field.</param>
		/// <param name="rightField">Right field.</param>
		/// <param name="predicate">Predicate.</param>
		internal DataFieldMatchExpression (DataFieldInfo leftField, DataFieldInfo rightField, QueryPredicate predicate)
		{
			this.leftField = leftField;
			this.rightField = rightField;
			this.predicate = predicate;
		}

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	dataParameters = null;
		//	string leftFieldSql = leftField.CreateDataFieldSql (factory, true);
		//	string rightFieldSql = rightField.CreateDataFieldSql (factory, true);
		//	return factory.CreateJoinOnMatchSql (leftFieldSql, predicate, rightFieldSql);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string leftFieldSql = leftField.CreateDataFieldSql (factory, true, out dataParameters1);
			string rightFieldSql = rightField.CreateDataFieldSql (factory, true, out dataParameters2);
			string sql = factory.CreateJoinOnMatchSql (leftFieldSql, predicate, rightFieldSql);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		/// <summary>
		/// Converts the query expression.
		/// </summary>
		/// <returns>The query expression.</returns>
		protected override QueryExpression ConvertQueryExpression ()
		{
			QueryExpression expression;
			if ((predicate == QueryPredicate.Eq || predicate == QueryPredicate.NotEq) && Object.Equals (rightField, null)) {
				expression = new NullQueryExpression (leftField, predicate == QueryPredicate.Eq);
			}
			else {
				expression = new DataFieldQueryExpression (leftField, predicate, rightField, false);
			}
			return expression;
		}
	}
}

