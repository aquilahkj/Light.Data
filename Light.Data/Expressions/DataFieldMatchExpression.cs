using System;

namespace Light.Data
{
	/// <summary>
	/// Data field match expression.
	/// </summary>
	public class DataFieldMatchExpression:DataFieldExpression
	{
		readonly DataFieldInfo leftField;

		readonly DataFieldInfo rightField;

		QueryPredicate predicate;

		internal DataFieldMatchExpression (DataFieldInfo leftField, DataFieldInfo rightField, QueryPredicate predicate)
		{
			this.leftField = leftField;
			this.rightField = rightField;
			this.predicate = predicate;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			string leftFieldSql = leftField.CreateDataFieldSql (factory, true);
			string rightFieldSql = rightField.CreateDataFieldSql (factory, true);
			return factory.CreateJoinOnMatchSql (leftFieldSql, predicate, rightFieldSql);
		}

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

