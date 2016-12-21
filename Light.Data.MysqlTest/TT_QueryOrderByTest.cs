using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_QueryOrderByTest : BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Int ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			list = context.Query<TeUser> ().OrderBy (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].Id, list [i].Id);
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Greater (list [i - 1].Id, list [i].Id);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_Double ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			list = context.Query<TeUser> ().OrderBy (x => x.HotRate).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].HotRate, list [i].HotRate);
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.HotRate).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Greater (list [i - 1].HotRate, list [i].HotRate);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_DateTime ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			list = context.Query<TeUser> ().OrderBy (x => x.RegTime).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].RegTime, list [i].RegTime);
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.RegTime).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Greater (list [i - 1].RegTime, list [i].RegTime);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_Catch ()
		{
			InitialUserTable (21);
			List<TeUser> list;

			list = context.Query<TeUser> ().OrderBy (x => x.LevelId).OrderByCatch (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.LessOrEqual (list [i - 1].LevelId, list [i].LevelId);
				if (list [i - 1].LevelId == list [i].LevelId) {
					Assert.Less (list [i - 1].Id, list [i].Id);
				}
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.LevelId).OrderByCatch (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.GreaterOrEqual (list [i - 1].LevelId, list [i].LevelId);
				if (list [i - 1].LevelId == list [i].LevelId) {
					Assert.Less (list [i - 1].Id, list [i].Id);
				}
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.LevelId).OrderBy (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].Id, list [i].Id);
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.LevelId).OrderByReset ().ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].Id, list [i].Id);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_Random ()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.Query<TeUser> ().OrderByRandom ().ToList ();
			Assert.AreEqual (21, list1.Count);
			List<TeUser> list2 = context.Query<TeUser> ().OrderByRandom ().ToList ();
			Assert.AreEqual (21, list1.Count);
			int [] array1 = new int [list1.Count];
			for (int i = 0; i < list1.Count; i++) {
				array1 [i] = list1 [i].Id;
			}
			int [] array2 = new int [list2.Count];
			for (int i = 0; i < list2.Count; i++) {
				array2 [i] = list2 [i].Id;
			}

			string s1 = string.Join ("-", array1);
			string s2 = string.Join ("-", array2);
			Assert.AreNotEqual (s1, s2);
		}

		[Test ()]
		public void TestCase_OrderBy_Random_Multi ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			list = context.Query<TeUser> ().OrderByRandom ().OrderByDescending (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Greater (list [i - 1].Id, list [i].Id);
			}

			list = context.Query<TeUser> ().OrderByRandom ().OrderByDescending (x => x.Id).OrderByReset ().ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].Id, list [i].Id);
			}

			list = context.Query<TeUser> ().OrderByRandom ().OrderByDescending (x => x.Id).OrderBy (x => x.Id).ToList ();
			Assert.AreEqual (21, list.Count);
			for (int i = 1; i < list.Count; i++) {
				Assert.Less (list [i - 1].Id, list [i].Id);
			}

			list = context.Query<TeUser> ().OrderByDescending (x => x.Id).OrderByRandom ().ToList ();
			Assert.AreEqual (21, list.Count);
			int [] array1 = new int [list.Count];
			for (int i = 0; i < list.Count; i++) {
				array1 [i] = list [i].Id;
			}
			list = context.Query<TeUser> ().OrderByDescending (x => x.Id).OrderByRandom ().ToList ();
			Assert.AreEqual (21, list.Count);
			int [] array2 = new int [list.Count];
			for (int i = 0; i < list.Count; i++) {
				array2 [i] = list [i].Id;
			}
			string s1 = string.Join ("-", array1);
			string s2 = string.Join ("-", array2);
			Assert.AreNotEqual (s1, s2);
		}
	}
}

