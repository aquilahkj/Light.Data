using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryWhereOrderByTest:BaseTest
	{
		[Test ()]
		public void TestCase_WhereOrderBy ()
		{
			InitialUserTable (21);


			List<TeUser> list3 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == 1).OrderBy (TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (10, list3.Count);
			for (int i = 1; i < list3.Count; i++) {
				Assert.Less (list3 [i - 1].Id, list3 [i].Id);
			}

			List<TeUser> list4 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == 1).OrderBy (TeUser.IdField.OrderByDesc ()).ToList ();
			Assert.AreEqual (10, list4.Count);
			for (int i = 1; i < list4.Count; i++) {
				Assert.Greater (list4 [i - 1].Id, list4 [i].Id);
			}
		}
	}
}

