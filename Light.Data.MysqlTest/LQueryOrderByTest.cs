using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryOrderByTest:BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Int ()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list1.Count);
			for (int i = 1; i < list1.Count; i++) {
				Assert.Less (list1 [i - 1].Id, list1 [i].Id);
			}

			List<TeUser> list2 = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByDesc ()).ToList ();
			Assert.AreEqual (21, list2.Count);
			for (int i = 1; i < list2.Count; i++) {
				Assert.Greater (list2 [i - 1].Id, list2 [i].Id);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_Double ()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.LQuery<TeUser> ().OrderBy (TeUser.HotRateField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list1.Count);
			for (int i = 1; i < list1.Count; i++) {
				Assert.Less (list1 [i - 1].HotRate, list1 [i].HotRate);
			}

			List<TeUser> list2 = context.LQuery<TeUser> ().OrderBy (TeUser.HotRateField.OrderByDesc ()).ToList ();
			Assert.AreEqual (21, list2.Count);
			for (int i = 1; i < list2.Count; i++) {
				Assert.Greater (list2 [i - 1].HotRate, list2 [i].HotRate);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_DateTime ()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.LQuery<TeUser> ().OrderBy (TeUser.RegTimeField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list1.Count);
			for (int i = 1; i < list1.Count; i++) {
				Assert.Less (list1 [i - 1].RegTime, list1 [i].RegTime);
			}

			List<TeUser> list2 = context.LQuery<TeUser> ().OrderBy (TeUser.RegTimeField.OrderByDesc ()).ToList ();
			Assert.AreEqual (21, list2.Count);
			for (int i = 1; i < list2.Count; i++) {
				Assert.Greater (list2 [i - 1].RegTime, list2 [i].RegTime);
			}
		}
	
		[Test ()]
		public void TestCase_OrderBy_Multi ()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.LQuery<TeUser> ().OrderBy (TeUser.LevelIdField.OrderByAsc () & TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list1.Count);
			for (int i = 1; i < list1.Count; i++) {
				Assert.LessOrEqual (list1 [i - 1].LevelId, list1 [i].LevelId);
				if (list1 [i - 1].LevelId == list1 [i].LevelId) {
					Assert.Less (list1 [i - 1].Id, list1 [i].Id);
				}
			}

			List<TeUser> list2 = context.LQuery<TeUser> ().OrderBy (TeUser.LevelIdField.OrderByDesc () & TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list2.Count);
			for (int i = 1; i < list2.Count; i++) {
				Assert.GreaterOrEqual (list2 [i - 1].LevelId, list2 [i].LevelId);
				if (list2 [i - 1].LevelId == list2 [i].LevelId) {
					Assert.Less (list2 [i - 1].Id, list2 [i].Id);
				}
			}

			List<TeUser> list3 = context.LQuery<TeUser> ().OrderBy (TeUser.LevelIdField.OrderByAsc ()).OrderBy (TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list3.Count);
			for (int i = 1; i < list3.Count; i++) {
				Assert.LessOrEqual (list3 [i - 1].LevelId, list3 [i].LevelId);
				if (list3 [i - 1].LevelId == list3 [i].LevelId) {
					Assert.Less (list3 [i - 1].Id, list3 [i].Id);
				}
			}

			List<TeUser> list4 = context.LQuery<TeUser> ().OrderBy (TeUser.LevelIdField.OrderByDesc ()).OrderBy (TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (21, list4.Count);
			for (int i = 1; i < list4.Count; i++) {
				Assert.GreaterOrEqual (list4 [i - 1].LevelId, list4 [i].LevelId);
				if (list4 [i - 1].LevelId == list4 [i].LevelId) {
					Assert.Less (list4 [i - 1].Id, list4 [i].Id);
				}
			}
		}
	
		[Test ()]
		public void TestCase_OrderBy_Random()
		{
			InitialUserTable (21);
			List<TeUser> list1 = context.LQuery<TeUser> ().OrderByRandom ().ToList();
			Assert.AreEqual (21, list1.Count);
			List<TeUser> list2 = context.LQuery<TeUser> ().OrderByRandom ().ToList();
			Assert.AreEqual (21, list1.Count);
			int[] array1 = new int[list1.Count];
			for (int i = 0; i < list1.Count; i++) {
				array1 [i] = list1 [i].Id;
			}
			int[] array2 = new int[list2.Count];
			for (int i = 0; i < list2.Count; i++) {
				array2 [i] = list2 [i].Id;
			}

			string s1 = string.Join ("-", array1);
			string s2 = string.Join ("-", array2);
			Assert.AreNotEqual (s1, s2);
		}
	}
}

