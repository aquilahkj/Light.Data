using System;

namespace Light.Data
{
	/// <summary>
	/// Random order expression.
	/// </summary>
	class RandomOrderExpression : OrderExpression
	{
		public RandomOrderExpression (DataEntityMapping tableMapping)
			: base (tableMapping)
		{
			
		}

		string _aliasTableName;

		public void SetTableMapping (DataEntityMapping mapping)
		{
			if (mapping == null) {
				throw new ArgumentNullException ("mapping");
			}
			TableMapping = mapping;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping, this._aliasTableName, fullFieldName);
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			dataParameters = new DataParameter[0];
			return factory.CreateRandomOrderBySql (TableMapping, this._aliasTableName, fullFieldName);
		}

		internal override OrderExpression CreateAliasTableNameOrder (string aliasTableName)
		{
			RandomOrderExpression expression = new RandomOrderExpression (this.TableMapping);
			expression._aliasTableName = aliasTableName;
			return expression;
		}

		public override bool Equals (OrderExpression target)
		{
			if (Object.Equals (target, null)) {
				return false;
			}
			if (Object.ReferenceEquals (this, target)) {
				return true;
			}
			if (this.GetType () == target.GetType ()) {
				RandomOrderExpression exp = target as RandomOrderExpression;
				return this._aliasTableName == exp._aliasTableName;
			}
			else {
				return false;
			}
		}
	}
}
