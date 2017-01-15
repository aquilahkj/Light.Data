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
			list_s = context.Query<TeUser> ().SelectField (x => x.Id).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.Id).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().SelectField (x => x.LevelId).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id <= 5).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id <= 5).SelectField (x => x.LevelId).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().SelectField (x => x.Area).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Area, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Area > 5).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Area > 5).SelectField (x => x.Area).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Area, list_null_s [i]);
			}

			var ex = context.Query<TeUser> ().First ().Id;
			var ac = context.Query<TeUser> ().SelectField (x => x.Id).First ();
			Assert.AreEqual (ex, ac);
			var exn = context.Query<TeUser> ().Where (x => x.Area == null).First ().Area;
			var acn = context.Query<TeUser> ().Where (x => x.Area == null).SelectField (x => x.Area).First ();
			Assert.AreEqual (exn, acn);
		}


		[Test ()]
		public void TestCase_Field_Double ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<double> list_s;
			List<double?> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().SelectField (x => x.HotRate).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.HotRate).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().SelectField (x => x.CheckPoint).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckPoint, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.CheckPoint).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckPoint, list_null_s [i]);
			}

			var ex = context.Query<TeUser> ().First ().HotRate;
			var ac = context.Query<TeUser> ().SelectField (x => x.HotRate).First ();
			Assert.AreEqual (ex, ac);
			var exn = context.Query<TeUser> ().Where (x => x.CheckPoint == null).First ().CheckPoint;
			var acn = context.Query<TeUser> ().Where (x => x.CheckPoint == null).SelectField (x => x.CheckPoint).First ();
			Assert.AreEqual (exn, acn);
		}

		[Test ()]
		public void TestCase_Field_DateTime ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<DateTime> list_s;
			List<DateTime?> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().SelectField (x => x.RegTime).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.RegTime).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().SelectField (x => x.LastLoginTime).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LastLoginTime, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.LastLoginTime).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LastLoginTime, list_null_s [i]);
			}

			var ex = context.Query<TeUser> ().First ().RegTime;
			var ac = context.Query<TeUser> ().SelectField (x => x.RegTime).First ();
			Assert.AreEqual (ex, ac);
			var exn = context.Query<TeUser> ().Where (x => x.CheckPoint == null).First ().CheckPoint;
			var acn = context.Query<TeUser> ().Where (x => x.LastLoginTime == null).SelectField (x => x.LastLoginTime).First ();
			Assert.AreEqual (exn, acn);
		}


		[Test ()]
		public void TestCase_Field_String ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<string> list_s;
			List<string> list_null_s;

			list = context.Query<TeUser> ().ToList ();
			list_s = context.Query<TeUser> ().SelectField (x => x.Account).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.Account).ToList ();
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}


			list = context.Query<TeUser> ().ToList ();
			list_null_s = context.Query<TeUser> ().SelectField (x => x.Address).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}

			list = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			list_null_s = context.Query<TeUser> ().Where (x => x.Id > 10).SelectField (x => x.Address).ToList ();
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}

			var ex = context.Query<TeUser> ().First ().Account;
			var ac = context.Query<TeUser> ().SelectField (x => x.Account).First ();
			Assert.AreEqual (ex, ac);
			var exn = context.Query<TeUser> ().Where (x => x.Address == null).First ().CheckPoint;
			var acn = context.Query<TeUser> ().Where (x => x.Address == null).SelectField (x => x.Address).First ();
			Assert.AreEqual (exn, acn);
		}


	}
}

