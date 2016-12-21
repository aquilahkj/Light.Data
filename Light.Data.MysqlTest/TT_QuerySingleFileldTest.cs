using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_QuerySingleFileldTest : BaseTest
	{
		[Test ()]
		public void TeatCase_List_Array_Le ()
		{
			const int count = 57;
			List<TeUser> list1 = InitialUserTable (count);
			List<TeUser> listReslt = context.Query<TeUser> ().ToList ();
			AssertExtend.AreObjectEqual (list1, listReslt);

			TeUser [] arrayResult = context.Query<TeUser> ().ToArray ();
			AssertExtend.AreObjectEqual (list1.ToArray (), arrayResult);

			int index = 0;
			foreach (TeUser user in context.Query<TeUser> ()) {
				AssertExtend.AreObjectEqual (list1 [index], user);
				index++;
			}
		}


		[Test ()]
		public void TestCase_Field_Int ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<int> list_s;
			List<int?> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().QueryFieldList (x => x.Id);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.Id);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().QueryFieldList (x => x.LevelId);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id <= 5).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id <= 5).QueryFieldList (x => x.LevelId);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().QueryFieldList (x => x.Area);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Area, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Area > 5).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Area > 5).QueryFieldList (x => x.Area);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Area, list_null_s [i]);
			}
		}


		[Test ()]
		public void TestCase_Field_Double ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<double> list_s;
			List<double?> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().QueryFieldList (x => x.HotRate);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.HotRate);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().QueryFieldList (x => x.CheckPoint);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckPoint, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.CheckPoint);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckPoint, list_null_s [i]);
			}
		}

		[Test ()]
		public void TestCase_Field_DateTime ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<DateTime> list_s;
			List<DateTime?> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().QueryFieldList (x => x.RegTime);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.RegTime);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().QueryFieldList (x => x.LastLoginTime);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LastLoginTime, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.LastLoginTime);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LastLoginTime, list_null_s [i]);
			}
		}


		[Test ()]
		public void TestCase_Field_String ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<string> list_s;
			List<string> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().QueryFieldList (x => x.Account);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.Account);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().QueryFieldList (x => x.Address);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).QueryFieldList (x => x.Address);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}
		}


	}
}

