
namespace Light.Data
{
	class CountAllFunction : AggregateFunction
	{
		internal CountAllFunction ()
			: base (null)
		{

		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			dataParameters = null;
			return factory.CreateCountAllSql ();
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	return true;
		//}
	}
}
