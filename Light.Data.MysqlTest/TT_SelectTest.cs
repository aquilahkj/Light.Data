using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_SelectTest : BaseTest
	{
		[Test ()]
		public void TestCase_Select1 ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (21);

			List<TeUser> users;
			List<TeUser> users1;
			users = context.Query<TeUser> ().ToList ();
			users1 = context.Query<TeUser> ().Select (x => x).ToList ();
			AssertExtend.AreObjectEqual (users, users1);

			users = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			users1 = context.Query<TeUser> ().Where (x => x.Id > 10).Select (x => x).ToList ();
			AssertExtend.AreObjectEqual (users, users1);
		}

		[Test ()]
		public void TestCase_Select2 ()
		{
			InitialUserTable (50);
			InitialUserLevelTable (21);

			List<TeUserSimple> users;

			users = context.Query<TeUser> ().Select (x => new TeUserSimple () {
				Id = x.Id,
				Account = x.Account,
				LevelId = x.LevelId,
				RegTime = x.RegTime
			}).ToList ();
			var users1 = context.Query<TeUser> ().Select (x => new {
				x.Id,
				x.Account,
				x.LevelId,
				x.RegTime
			}).ToList ();
			AssertExtend.AreObjectEqual (users, users1);

			users = context.Query<TeUser> ().Where (x => x.Id > 10).Select (x => new TeUserSimple () {
				Id = x.Id,
				Account = x.Account,
				LevelId = x.LevelId,
				RegTime = x.RegTime
			}).ToList ();
			var users2 = context.Query<TeUser> ().Where (x => x.Id > 10).Select (x => new {
				x.Id,
				x.Account,
				x.LevelId,
				x.RegTime
			}).ToList ();
			AssertExtend.AreObjectEqual (users, users2);
		}


	}
}
