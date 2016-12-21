using System;
using NUnit.Framework;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_QueryAggregateTest : BaseTest
	{
		[Test ()]
		public void TestCase_Exists ()
		{
			InitialUserTable (21);
			bool result1 = context.Query<TeUser> ().Exists;
			Assert.IsTrue (result1);

			bool result2 = context.Query<TeUser> ().Where (x => x.Id > 1).Exists;
			Assert.IsTrue (result2);

			bool result3 = context.Query<TeUser> ().Where (x => x.Id > 21).Exists;
			Assert.IsFalse (result3);

			bool result4 = context.Query<TeUser> ().Where (x => x.Id > 1 & x.CheckPoint != null).Exists;
			Assert.IsTrue (result4);

			bool result5 = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).Exists;
			Assert.IsFalse (result5);
		}

		[Test ()]
		public void TestCase_Count ()
		{
			InitialUserTable (21);
			int result1 = context.Query<TeUser> ().Count;
			Assert.AreEqual (21, result1);

			int result2 = context.Query<TeUser> ().Where (x => x.Id  > 1).Count;
			Assert.AreEqual (20, result2);

			int result3 = context.Query<TeUser> ().Where (x => x.Id  > 21).Count;
			Assert.AreEqual (0, result3);

			int result4 = context.Query<TeUser> ().Where (x => x.Id  > 1 & x.CheckPoint != null).Count;
			Assert.AreEqual (10, result4);

			int result5 = context.Query<TeUser> ().Where (x => x.Id  == 1 & x.CheckPoint != null).Count;
			Assert.AreEqual (0, result5);
		}

	}
}
