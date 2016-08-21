
namespace Light.Data
{
	class CountAllFunction : AggregateData
	{
		internal CountAllFunction ()
			: base (null)
		{

		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter[] dataParameters)
		{
			dataParameters = null;
			return factory.CreateCountAllSql ();
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			return factory.CreateCountAllSql ();
		}

		//protected override bool EqualsDetail (AggregateFunction function)
		//{
		//	return true;
		//}
	}
}
