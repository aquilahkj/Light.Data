using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryWhereTest:BaseTest
	{
		List<TeUser> InitialUserTable (int count, bool insert = true)
		{
			context.TruncateTable<TeUser> ();
			List<TeUser> list1 = new List<TeUser> ();
			for (int i = 1; i <= count; i++) {
				TeUser userInsert = CreateTestUser (false);
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddMinutes (i);
				userInsert.Gender = i % 2 == 0 ? GenderType.Male : GenderType.Female;
				userInsert.LevelId = i % 10;
				userInsert.HotRate = 1 + i * 0.01d;
				userInsert.DeleteFlag = i % 2 == 0;

				userInsert.Address = i % 2 == 0 ? "addr" + userInsert.Account : null;

				if (i % 2 == 0) {
					userInsert.LastLoginTime = userInsert.RegTime.AddMinutes (i);
					userInsert.Area = i;
					userInsert.RefereeId = i % 6;
					userInsert.CheckPoint = i * 0.01d;
					userInsert.DeleteFlag = true;
					userInsert.CheckStatus = true;
				}
				list1.Add (userInsert);
			}
			if (insert) {
				context.BulkInsert (list1.ToArray ());
			}
			return list1;
		}

		[Test ()]
		public void TestCase_QueryId ()
		{
			InitialUserTable (20);
			TeUser user1 = context.SelectSingleFromId<TeUser> (8);
			Assert.NotNull (user1);
			Assert.AreEqual (8, user1.Id);
			TeUser user2 = context.SelectSingleFromId<TeUser> (15);
			Assert.NotNull (user2);
			Assert.AreEqual (15, user2.Id);
			TeUser user3 = context.SelectSingleFromId<TeUser> (21);
			Assert.Null (user3);
		}

		[Test ()]
		public void TestCase_QueryKey ()
		{
			InitialUserTable (20);
			TeUser user1 = context.SelectSingleFromKey<TeUser> (8);
			Assert.NotNull (user1);
			Assert.AreEqual (8, user1.Id);
			TeUser user2 = context.SelectSingleFromKey<TeUser> (15);
			Assert.NotNull (user2);
			Assert.AreEqual (15, user2.Id);
			TeUser user3 = context.SelectSingleFromKey<TeUser> (21);
			Assert.Null (user3);
		}

		[Test ()]
		public void TeatCase_List_Array_Le ()
		{
			const int count = 57;
			List<TeUser> list1 = InitialUserTable (count);
			List<TeUser> listReslt = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (count, listReslt.Count);
			for (int i = 0; i < count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list1 [i], false));
			}
			TeUser[] arrayResult = context.LQuery<TeUser> ().ToArray ();
			Assert.AreEqual (count, arrayResult.Length);
			for (int i = 0; i < count; i++) {
				Assert.IsTrue (EqualUser (arrayResult [i], list1 [i], false));
			}
			int index = 0;
			foreach (TeUser user in context.LQuery<TeUser> ()) {
				Assert.IsTrue (EqualUser (user, list1 [index], false));
				index++;
			}
		}

		[Test ()]
		public void TestCase_QuerySingleParam_Enum ()
		{
			InitialUserTable (21);
			List<TeUser> listEq = context.LQuery<TeUser> ().Where (TeUser.GenderField == GenderType.Female).ToList ();
			Assert.AreEqual (11, listEq.Count);
			Assert.IsTrue (listEq.TrueForAll (x => x.Gender == GenderType.Female));

			List<TeUser> listNotEq = context.LQuery<TeUser> ().Where (TeUser.GenderField != GenderType.Female).ToList ();
			Assert.AreEqual (10, listNotEq.Count);
			Assert.IsTrue (listNotEq.TrueForAll (x => x.Gender == GenderType.Male));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_Int ()
		{
			InitialUserTable (20);
			List<TeUser> listEq = context.LQuery<TeUser> ().Where (TeUser.IdField == 10).ToList ();
			Assert.AreEqual (1, listEq.Count);
			Assert.IsTrue (listEq.TrueForAll (x => x.Id == 10));

			List<TeUser> listNotEq = context.LQuery<TeUser> ().Where (TeUser.IdField != 10).ToList ();
			Assert.AreEqual (19, listNotEq.Count);
			Assert.IsTrue (listNotEq.TrueForAll (x => x.Id != 10));

			List<TeUser> listGt = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			Assert.AreEqual (10, listGt.Count);
			Assert.IsTrue (listGt.TrueForAll (x => x.Id > 10));

			List<TeUser> listGtEq = context.LQuery<TeUser> ().Where (TeUser.IdField >= 10).ToList ();
			Assert.AreEqual (11, listGtEq.Count);
			Assert.IsTrue (listGtEq.TrueForAll (x => x.Id >= 10));

			List<TeUser> listLt = context.LQuery<TeUser> ().Where (TeUser.IdField < 10).ToList ();
			Assert.AreEqual (9, listLt.Count);
			Assert.IsTrue (listLt.TrueForAll (x => x.Id < 10));

			List<TeUser> listLtEq = context.LQuery<TeUser> ().Where (TeUser.IdField <= 10).ToList ();
			Assert.AreEqual (10, listLtEq.Count);
			Assert.IsTrue (listLtEq.TrueForAll (x => x.Id <= 10));

			List<TeUser> listGtLt = context.LQuery<TeUser> ().Where (TeUser.IdField > 5 & TeUser.IdField < 15).ToList ();
			Assert.AreEqual (9, listGtLt.Count);
			Assert.IsTrue (listGtLt.TrueForAll (x => x.Id > 5 && x.Id < 15));

			List<TeUser> listGtEqLt = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5 & TeUser.IdField < 15).ToList ();
			Assert.AreEqual (10, listGtEqLt.Count);
			Assert.IsTrue (listGtEqLt.TrueForAll (x => x.Id >= 5 && x.Id < 15));

			List<TeUser> listGtLtEq = context.LQuery<TeUser> ().Where (TeUser.IdField > 5 & TeUser.IdField <= 15).ToList ();
			Assert.AreEqual (10, listGtLtEq.Count);
			Assert.IsTrue (listGtLtEq.TrueForAll (x => x.Id > 5 && x.Id <= 15));

			List<TeUser> listGtEqLtEq = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5 & TeUser.IdField <= 15).ToList ();
			Assert.AreEqual (11, listGtEqLtEq.Count);
			Assert.IsTrue (listGtEqLtEq.TrueForAll (x => x.Id >= 5 && x.Id <= 15));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_Double ()
		{
			InitialUserTable (20);
			List<TeUser> listEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField == 1.10d).ToList ();
			Assert.AreEqual (1, listEq.Count);
			Assert.IsTrue (listEq.TrueForAll (x => x.HotRate == 1.10d));

			List<TeUser> listNotEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField != 1.10d).ToList ();
			Assert.AreEqual (19, listNotEq.Count);
			Assert.IsTrue (listNotEq.TrueForAll (x => x.HotRate != 1.10d));

			List<TeUser> listGt = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.10d).ToList ();
			Assert.AreEqual (10, listGt.Count);
			Assert.IsTrue (listGt.TrueForAll (x => x.HotRate > 1.10d));

			List<TeUser> listGtEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.10d).ToList ();
			Assert.AreEqual (11, listGtEq.Count);
			Assert.IsTrue (listGtEq.TrueForAll (x => x.HotRate >= 1.10d));

			List<TeUser> listLt = context.LQuery<TeUser> ().Where (TeUser.HotRateField < 1.10d).ToList ();
			Assert.AreEqual (9, listLt.Count);
			Assert.IsTrue (listLt.TrueForAll (x => x.HotRate < 1.10d));

			List<TeUser> listLtEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField <= 1.10d).ToList ();
			Assert.AreEqual (10, listLtEq.Count);
			Assert.IsTrue (listLtEq.TrueForAll (x => x.HotRate <= 1.10d));

			List<TeUser> listGtLt = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.05d & TeUser.HotRateField < 1.15d).ToList ();
			Assert.AreEqual (9, listGtLt.Count);
			Assert.IsTrue (listGtLt.TrueForAll (x => x.HotRate > 1.05d && x.HotRate < 1.15d));

			List<TeUser> listGtEqLt = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.05d & TeUser.HotRateField < 1.15d).ToList ();
			Assert.AreEqual (10, listGtEqLt.Count);
			Assert.IsTrue (listGtEqLt.TrueForAll (x => x.HotRate >= 1.05d && x.HotRate < 1.15d));

			List<TeUser> listGtLtEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.05d & TeUser.HotRateField <= 1.15d).ToList ();
			Assert.AreEqual (10, listGtLtEq.Count);
			Assert.IsTrue (listGtLtEq.TrueForAll (x => x.HotRate > 1.05d && x.HotRate <= 1.15d));

			List<TeUser> listGtEqLtEq = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.05d & TeUser.HotRateField <= 1.15d).ToList ();
			Assert.AreEqual (11, listGtEqLtEq.Count);
			Assert.IsTrue (listGtEqLtEq.TrueForAll (x => x.HotRate >= 1.05d && x.HotRate <= 1.15d));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_DateTime ()
		{
			InitialUserTable (20);
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);
			List<TeUser> listEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField == dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (1, listEq.Count);
			Assert.IsTrue (listEq.TrueForAll (x => x.RegTime == dt.AddMinutes (10)));

			List<TeUser> listNotEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField != dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (19, listNotEq.Count);
			Assert.IsTrue (listNotEq.TrueForAll (x => x.RegTime != dt.AddMinutes (10)));

			List<TeUser> listGt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (10, listGt.Count);
			Assert.IsTrue (listGt.TrueForAll (x => x.RegTime > dt.AddMinutes (10)));

			List<TeUser> listGtEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (11, listGtEq.Count);
			Assert.IsTrue (listGtEq.TrueForAll (x => x.RegTime >= dt.AddMinutes (10)));

			List<TeUser> listLt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField < dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (9, listLt.Count);
			Assert.IsTrue (listLt.TrueForAll (x => x.RegTime < dt.AddMinutes (10)));

			List<TeUser> listLtEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField <= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (10, listLtEq.Count);
			Assert.IsTrue (listLtEq.TrueForAll (x => x.RegTime <= dt.AddMinutes (10)));

			List<TeUser> listGtLt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (5) & TeUser.RegTimeField < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (9, listGtLt.Count);
			Assert.IsTrue (listGtLt.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			List<TeUser> listGtEqLt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (5) & TeUser.RegTimeField < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (10, listGtEqLt.Count);
			Assert.IsTrue (listGtEqLt.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			List<TeUser> listGtLtEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (5) & TeUser.RegTimeField <= dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (10, listGtLtEq.Count);
			Assert.IsTrue (listGtLtEq.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));

			List<TeUser> listGtEqLtEq = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (5) & TeUser.RegTimeField <= dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (11, listGtEqLtEq.Count);
			Assert.IsTrue (listGtEqLtEq.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_String ()
		{
			InitialUserTable (20);
			List<TeUser> listEq = context.LQuery<TeUser> ().Where (TeUser.AccountField == "test10").ToList ();
			Assert.AreEqual (1, listEq.Count);
			Assert.IsTrue (listEq.TrueForAll (x => x.Account == "test10"));

			List<TeUser> listNotEq = context.LQuery<TeUser> ().Where (TeUser.AccountField != "test10").ToList ();
			Assert.AreEqual (19, listNotEq.Count);
			Assert.IsTrue (listNotEq.TrueForAll (x => x.Account != "test10"));
		}

		[Test ()]
		public void TestCase_QueryBetween_Int ()
		{
			InitialUserTable (20);
			List<TeUser> listBt = context.LQuery<TeUser> ().Where (TeUser.IdField.Between (5, 15)).ToList ();
			Assert.AreEqual (11, listBt.Count);
			Assert.IsTrue (listBt.TrueForAll (x => x.Id >= 5 && x.Id <= 15));

			List<TeUser> listNotBt = context.LQuery<TeUser> ().Where (TeUser.IdField.NotBetween (5, 15)).ToList ();
			Assert.AreEqual (9, listNotBt.Count);
			Assert.IsTrue (listNotBt.TrueForAll (x => !(x.Id >= 5 && x.Id <= 15)));
		}

		[Test ()]
		public void TestCase_QueryBetween_Double ()
		{
			InitialUserTable (20);
			List<TeUser> listBt = context.LQuery<TeUser> ().Where (TeUser.HotRateField.Between (1.05, 1.15)).ToList ();
			Assert.AreEqual (11, listBt.Count);
			Assert.IsTrue (listBt.TrueForAll (x => x.HotRate >= 1.05 && x.HotRate <= 1.15));

			List<TeUser> listNotBt = context.LQuery<TeUser> ().Where (TeUser.HotRateField.NotBetween (1.05, 1.15)).ToList ();
			Assert.AreEqual (9, listNotBt.Count);
			Assert.IsTrue (listNotBt.TrueForAll (x => !(x.HotRate >= 1.05 && x.HotRate <= 1.15)));
		}

		[Test ()]
		public void TestCase_QueryBetween_DateTime ()
		{
			InitialUserTable (20);
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);
			List<TeUser> listBt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.Between (dt.AddMinutes (5), dt.AddMinutes (15))).ToList ();
			Assert.AreEqual (11, listBt.Count);
			Assert.IsTrue (listBt.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));

			List<TeUser> listNotBt = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.NotBetween (dt.AddMinutes (5), dt.AddMinutes (15))).ToList ();
			Assert.AreEqual (9, listNotBt.Count);
			Assert.IsTrue (listNotBt.TrueForAll (x => !(x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15))));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_Int ()
		{
			InitialUserTable (20);
			int[] arrayInt = new int[]{ 3, 5, 7 };
			List<TeUser> listLtAny = context.LQuery<TeUser> ().Where (TeUser.IdField.LtAny (arrayInt)).ToList ();
			Assert.AreEqual (6, listLtAny.Count);
			Assert.IsTrue (listLtAny.TrueForAll (x => x.Id < 3 || x.Id < 5 || x.Id < 7));

			List<TeUser> listGtAny = context.LQuery<TeUser> ().Where (TeUser.IdField.GtAny (arrayInt)).ToList ();
			Assert.AreEqual (17, listGtAny.Count);
			Assert.IsTrue (listGtAny.TrueForAll (x => x.Id > 3 || x.Id > 5 || x.Id > 7));

			List<TeUser> listLtAll = context.LQuery<TeUser> ().Where (TeUser.IdField.LtAll (arrayInt)).ToList ();
			Assert.AreEqual (2, listLtAll.Count);
			Assert.IsTrue (listLtAll.TrueForAll (x => x.Id < 3 && x.Id < 5 && x.Id < 7));

			List<TeUser> listGtAll = context.LQuery<TeUser> ().Where (TeUser.IdField.GtAll (arrayInt)).ToList ();
			Assert.AreEqual (13, listGtAll.Count);
			Assert.IsTrue (listGtAll.TrueForAll (x => x.Id > 3 && x.Id > 5 && x.Id > 7));

			List<TeUser> listIn = context.LQuery<TeUser> ().Where (TeUser.IdField.In (arrayInt)).ToList ();
			Assert.AreEqual (3, listIn.Count);
			Assert.IsTrue (listIn.TrueForAll (x => x.Id == 3 || x.Id == 5 || x.Id == 7));

			List<TeUser> listNotIn = context.LQuery<TeUser> ().Where (TeUser.IdField.NotIn (arrayInt)).ToList ();
			Assert.AreEqual (17, listNotIn.Count);
			Assert.IsTrue (listNotIn.TrueForAll (x => x.Id != 3 && x.Id != 5 && x.Id != 7));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_Double ()
		{
			InitialUserTable (20);
			double[] arrayDouble = new double[]{ 1.03, 1.05, 1.07 };
			List<TeUser> listLtAny = context.LQuery<TeUser> ().Where (TeUser.HotRateField.LtAny (arrayDouble)).ToList ();
			Assert.AreEqual (6, listLtAny.Count);
			Assert.IsTrue (listLtAny.TrueForAll (x => x.HotRate < 1.03 || x.HotRate < 1.05 || x.HotRate < 1.07));

			List<TeUser> listGtAny = context.LQuery<TeUser> ().Where (TeUser.HotRateField.GtAny (arrayDouble)).ToList ();
			Assert.AreEqual (17, listGtAny.Count);
			Assert.IsTrue (listGtAny.TrueForAll (x => x.HotRate > 1.03 || x.HotRate > 1.05 || x.HotRate > 1.07));

			List<TeUser> listLtAll = context.LQuery<TeUser> ().Where (TeUser.HotRateField.LtAll (arrayDouble)).ToList ();
			Assert.AreEqual (2, listLtAll.Count);
			Assert.IsTrue (listLtAll.TrueForAll (x => x.HotRate < 1.03 && x.HotRate < 1.05 && x.HotRate < 1.07));

			List<TeUser> listGtAll = context.LQuery<TeUser> ().Where (TeUser.HotRateField.GtAll (arrayDouble)).ToList ();
			Assert.AreEqual (13, listGtAll.Count);
			Assert.IsTrue (listGtAll.TrueForAll (x => x.HotRate > 1.03 && x.HotRate > 1.05 && x.HotRate > 1.07));

			List<TeUser> listIn = context.LQuery<TeUser> ().Where (TeUser.HotRateField.In (arrayDouble)).ToList ();
			Assert.AreEqual (3, listIn.Count);
			Assert.IsTrue (listIn.TrueForAll (x => x.HotRate == 1.03 || x.HotRate == 1.05 || x.HotRate == 1.07));

			List<TeUser> listNotIn = context.LQuery<TeUser> ().Where (TeUser.HotRateField.NotIn (arrayDouble)).ToList ();
			Assert.AreEqual (17, listNotIn.Count);
			Assert.IsTrue (listNotIn.TrueForAll (x => x.HotRate != 1.03 && x.HotRate != 1.05 && x.HotRate != 1.07));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_DateTime ()
		{
			InitialUserTable (20);
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);
			DateTime d1 = dt.AddMinutes (3);
			DateTime d2 = dt.AddMinutes (5);
			DateTime d3 = dt.AddMinutes (7);
			DateTime[] arrayDateTime = new DateTime[]{ d1, d2, d3 };
			List<TeUser> listLtAny = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.LtAny (arrayDateTime)).ToList ();
			Assert.AreEqual (6, listLtAny.Count);
			Assert.IsTrue (listLtAny.TrueForAll (x => x.RegTime < d1 || x.RegTime < d2 || x.RegTime < d3));

			List<TeUser> listGtAny = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.GtAny (arrayDateTime)).ToList ();
			Assert.AreEqual (17, listGtAny.Count);
			Assert.IsTrue (listGtAny.TrueForAll (x => x.RegTime > d1 || x.RegTime > d2 || x.RegTime > d3));

			List<TeUser> listLtAll = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.LtAll (arrayDateTime)).ToList ();
			Assert.AreEqual (2, listLtAll.Count);
			Assert.IsTrue (listLtAll.TrueForAll (x => x.RegTime < d1 && x.RegTime < d2 && x.RegTime < d3));

			List<TeUser> listGtAll = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.GtAll (arrayDateTime)).ToList ();
			Assert.AreEqual (13, listGtAll.Count);
			Assert.IsTrue (listGtAll.TrueForAll (x => x.RegTime > d1 && x.RegTime > d2 && x.RegTime > d3));

			List<TeUser> listIn = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.In (arrayDateTime)).ToList ();
			Assert.AreEqual (3, listIn.Count);
			Assert.IsTrue (listIn.TrueForAll (x => x.RegTime == d1 || x.RegTime == d2 || x.RegTime == d3));

			List<TeUser> listNotIn = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.NotIn (arrayDateTime)).ToList ();
			Assert.AreEqual (17, listNotIn.Count);
			Assert.IsTrue (listNotIn.TrueForAll (x => x.RegTime != d1 && x.RegTime != d2 && x.RegTime != d3));
		}

		[Test ()]
		public void TestCase_QueryLikeMatch_String ()
		{
			InitialUserTable (20);

			List<TeUser> listlike1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("test1%")).ToList ();
			Assert.AreEqual (11, listlike1.Count);
			Assert.IsTrue (listlike1.TrueForAll (x => x.Account.StartsWith ("test1")));

			List<TeUser> listNotlike1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("test1%")).ToList ();
			Assert.AreEqual (9, listNotlike1.Count);
			Assert.IsTrue (listNotlike1.TrueForAll (x => !x.Account.StartsWith ("test1")));

			List<TeUser> listlike2 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("%5")).ToList ();
			Assert.AreEqual (2, listlike2.Count);
			Assert.IsTrue (listlike2.TrueForAll (x => x.Account.EndsWith ("5")));

			List<TeUser> listNotlike2 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("%5")).ToList ();
			Assert.AreEqual (18, listNotlike2.Count);
			Assert.IsTrue (listNotlike2.TrueForAll (x => !x.Account.EndsWith ("5")));

			List<TeUser> listlike3 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("%est2%")).ToList ();
			Assert.AreEqual (2, listlike3.Count);
			Assert.IsTrue (listlike3.TrueForAll (x => x.Account.Contains ("est2")));

			List<TeUser> listNotlike3 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("%est2%")).ToList ();
			Assert.AreEqual (18, listNotlike3.Count);
			Assert.IsTrue (listNotlike3.TrueForAll (x => !x.Account.Contains ("est2")));



			List<TeUser> listlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, false)).ReverseLike ("mytest2")).ToList ();
			Assert.AreEqual (1, listlike4.Count);
			Assert.IsTrue (listlike4.TrueForAll (x => "mytest2".EndsWith (x.Account)));

			List<TeUser> listNotlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, false)).ReverseNotLike ("mytest2")).ToList ();
			Assert.AreEqual (19, listNotlike4.Count);
			Assert.IsTrue (listNotlike4.TrueForAll (x => !"mytest2".EndsWith (x.Account)));

			List<TeUser> listlike5 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, false)).ReverseLike ("mytest2a")).ToList ();
			Assert.AreEqual (0, listlike5.Count);

			List<TeUser> listNotlike5 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, false)).ReverseNotLike ("mytest2a")).ToList ();
			Assert.AreEqual (20, listNotlike5.Count);
			Assert.IsTrue (listNotlike4.TrueForAll (x => !"mytest2a".EndsWith (x.Account)));



			List<TeUser> listlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (false, true)).ReverseLike ("test11aaaa")).ToList ();
			Assert.AreEqual (2, listlike6.Count);
			Assert.IsTrue (listlike6.TrueForAll (x => "test11aaaa".StartsWith (x.Account)));

			List<TeUser> listNotlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (false, true)).ReverseNotLike ("test11aaaa")).ToList ();
			Assert.AreEqual (18, listNotlike6.Count);
			Assert.IsTrue (listNotlike6.TrueForAll (x => !"test11aaaa".StartsWith (x.Account)));

			List<TeUser> listlike7 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (false, true)).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (0, listlike7.Count);

			List<TeUser> listNotlike7 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (false, true)).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (20, listNotlike7.Count);
			Assert.IsTrue (listNotlike7.TrueForAll (x => !"mytest11aaaa".StartsWith (x.Account)));


			List<TeUser> listlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, true)).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (2, listlike8.Count);
			Assert.IsTrue (listlike8.TrueForAll (x => "mytest11aaaa".Contains (x.Account)));

			List<TeUser> listNotlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, true)).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (18, listNotlike8.Count);
			Assert.IsTrue (listNotlike8.TrueForAll (x => !"mytest11aaaa".Contains (x.Account)));

			List<TeUser> listlike9 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (true, true)).ReverseLike ("test")).ToList ();
			Assert.AreEqual (0, listlike9.Count);

			List<TeUser> listNotlike9 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformMatch (false, true)).ReverseNotLike ("test")).ToList ();
			Assert.AreEqual (20, listNotlike9.Count);
			Assert.IsTrue (listNotlike7.TrueForAll (x => !"test".Contains (x.Account)));




			List<TeUser> listMatch1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Match ("est2")).ToList ();
			Assert.AreEqual (2, listMatch1.Count);
			Assert.IsTrue (listMatch1.TrueForAll (x => x.Account.Contains ("est2")));

			List<TeUser> listNotMatch1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotMatch ("est2")).ToList ();
			Assert.AreEqual (18, listNotMatch1.Count);
			Assert.IsTrue (listNotMatch1.TrueForAll (x => !x.Account.Contains ("est2")));

				
		}

		[Test ()]
		public void TestCase_QueryNull_Int ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.RefereeIdField.IsNull ()).ToList ();
			Assert.AreEqual (11, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.RefereeId == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.RefereeIdField.IsNotNull ()).ToList ();
			Assert.AreEqual (10, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.RefereeId != null));
		}

		[Test ()]
		public void TestCase_QueryNull_Double ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.CheckPointField.IsNull ()).ToList ();
			Assert.AreEqual (11, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.CheckPoint == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.CheckPointField.IsNotNull ()).ToList ();
			Assert.AreEqual (10, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.CheckPoint != null));
		}

		[Test ()]
		public void TestCase_QueryNull_DateTime ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.LastLoginTimeField.IsNull ()).ToList ();
			Assert.AreEqual (11, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.LastLoginTime == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.LastLoginTimeField.IsNotNull ()).ToList ();
			Assert.AreEqual (10, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.LastLoginTime != null));
		}

		[Test ()]
		public void TestCase_QueryNull_String ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.AddressField.IsNull ()).ToList ();
			Assert.AreEqual (11, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.Address == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.AddressField.IsNotNull ()).ToList ();
			Assert.AreEqual (10, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.Address != null));
		}

		[Test ()]
		public void TestCase_Query_Boolean ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsTrue()).ToList ();
			Assert.AreEqual (10, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.DeleteFlag));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsFalse ()).ToList ();
			Assert.AreEqual (11, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => !x.DeleteFlag));
		}

		[Test ()]
		public void TestCase_QueryNull_Boolean ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.CheckStatusField.IsNull ()).ToList ();
			Assert.AreEqual (11, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.CheckStatus == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.CheckStatusField.IsNotNull ()).ToList ();
			Assert.AreEqual (10, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.CheckStatus != null));
		}
	}
}

