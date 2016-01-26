using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryWhereTest:BaseTest
	{



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
		public void TestCase_QueryInArray_String ()
		{
			InitialUserTable (20);
			string[] arrayString = new string[]{ "test3", "test5", "test7" };
			List<TeUser> listIn = context.LQuery<TeUser> ().Where (TeUser.AccountField.In (arrayString)).ToList ();
			Assert.AreEqual (3, listIn.Count);
			Assert.IsTrue (listIn.TrueForAll (x => x.Account == "test3" || x.Account == "test5" || x.Account == "test7"));

			List<TeUser> listNotIn = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotIn (arrayString)).ToList ();
			Assert.AreEqual (17, listNotIn.Count);
			Assert.IsTrue (listNotIn.TrueForAll (x => x.Account != "test3" && x.Account != "test5" && x.Account != "test7"));
		}

		[Test ()]
		public void TestCase_QueryLikeMatch_Single_String ()
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





			List<TeUser> listEnd1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1")).ToList ();
			Assert.AreEqual (11, listEnd1.Count);
			Assert.IsTrue (listEnd1.TrueForAll (x => x.Account.StartsWith ("test1")));

			List<TeUser> listNotEnd1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotStartsWith ("test1")).ToList ();
			Assert.AreEqual (9, listNotEnd1.Count);
			Assert.IsTrue (listNotEnd1.TrueForAll (x => !x.Account.StartsWith ("test1")));

			List<TeUser> listStart1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.EndsWith ("5")).ToList ();
			Assert.AreEqual (2, listStart1.Count);
			Assert.IsTrue (listStart1.TrueForAll (x => x.Account.EndsWith ("5")));

			List<TeUser> listNotStart1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotEndsWith ("5")).ToList ();
			Assert.AreEqual (18, listNotStart1.Count);
			Assert.IsTrue (listNotStart1.TrueForAll (x => !x.Account.EndsWith ("5")));

			List<TeUser> listContains1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Contains ("est2")).ToList ();
			Assert.AreEqual (2, listContains1.Count);
			Assert.IsTrue (listContains1.TrueForAll (x => x.Account.Contains ("est2")));

			List<TeUser> listNotContains1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotContains ("est2")).ToList ();
			Assert.AreEqual (18, listNotContains1.Count);
			Assert.IsTrue (listNotContains1.TrueForAll (x => !x.Account.Contains ("est2")));

		}

		[Test ()]
		public void TestCAse_QueryLikeMatch_Single_String_Reverse ()
		{
			InitialUserTable (20);


			List<TeUser> listlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike ("mytest2")).ToList ();
			Assert.AreEqual (1, listlike4.Count);
			Assert.IsTrue (listlike4.TrueForAll (x => "mytest2".EndsWith (x.Account)));

			List<TeUser> listNotlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike ("mytest2")).ToList ();
			Assert.AreEqual (19, listNotlike4.Count);
			Assert.IsTrue (listNotlike4.TrueForAll (x => !"mytest2".EndsWith (x.Account)));

			List<TeUser> listlike5 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike ("mytest2a")).ToList ();
			Assert.AreEqual (0, listlike5.Count);

			List<TeUser> listNotlike5 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike ("mytest2a")).ToList ();
			Assert.AreEqual (20, listNotlike5.Count);
			Assert.IsTrue (listNotlike4.TrueForAll (x => !"mytest2a".EndsWith (x.Account)));


			List<TeUser> listlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike ("test11aaaa")).ToList ();
			Assert.AreEqual (2, listlike6.Count);
			Assert.IsTrue (listlike6.TrueForAll (x => "test11aaaa".StartsWith (x.Account)));

			List<TeUser> listNotlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike ("test11aaaa")).ToList ();
			Assert.AreEqual (18, listNotlike6.Count);
			Assert.IsTrue (listNotlike6.TrueForAll (x => !"test11aaaa".StartsWith (x.Account)));

			List<TeUser> listlike7 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (0, listlike7.Count);

			List<TeUser> listNotlike7 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (20, listNotlike7.Count);
			Assert.IsTrue (listNotlike7.TrueForAll (x => !"mytest11aaaa".StartsWith (x.Account)));



			List<TeUser> listlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (2, listlike8.Count);
			Assert.IsTrue (listlike8.TrueForAll (x => "mytest11aaaa".Contains (x.Account)));

			List<TeUser> listNotlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (18, listNotlike8.Count);
			Assert.IsTrue (listNotlike8.TrueForAll (x => !"mytest11aaaa".Contains (x.Account)));

			List<TeUser> listlike9 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike ("test")).ToList ();
			Assert.AreEqual (0, listlike9.Count);

			List<TeUser> listNotlike9 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseNotLike ("test")).ToList ();
			Assert.AreEqual (20, listNotlike9.Count);
			Assert.IsTrue (listNotlike7.TrueForAll (x => !"test".Contains (x.Account)));
		}

		[Test ()]
		public void TestCase_QueryLikeMatch_Multi_String ()
		{
			InitialUserTable (20);
			string[] like1Array = new string[]{ "test1%", "test2%", "test3%" };
			List<TeUser> listlike1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (like1Array)).ToList ();
			Assert.AreEqual (14, listlike1.Count);
			Assert.IsTrue (listlike1.TrueForAll (x => x.Account.StartsWith ("test1") || x.Account.StartsWith ("test2") || x.Account.StartsWith ("test3")));

			List<TeUser> listNotlike1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (like1Array)).ToList ();
			Assert.AreEqual (6, listNotlike1.Count);
			Assert.IsTrue (listNotlike1.TrueForAll (x => !x.Account.StartsWith ("test1") && !x.Account.StartsWith ("test2") && !x.Account.StartsWith ("test3")));

			string[] like2Array = new string[]{ "%2", "%5", "%6" };
			List<TeUser> listlike2 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (like2Array)).ToList ();
			Assert.AreEqual (6, listlike2.Count);
			Assert.IsTrue (listlike2.TrueForAll (x => x.Account.EndsWith ("2") || x.Account.EndsWith ("5") || x.Account.EndsWith ("6")));

			List<TeUser> listNotlike2 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (like2Array)).ToList ();
			Assert.AreEqual (14, listNotlike2.Count);
			Assert.IsTrue (listNotlike2.TrueForAll (x => !x.Account.EndsWith ("2") && !x.Account.EndsWith ("5") && !x.Account.EndsWith ("6")));

			string[] like3Array = new string[]{ "%est1%", "%est2%", "%est3%" };
			List<TeUser> listlike3 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (like3Array)).ToList ();
			Assert.AreEqual (14, listlike3.Count);
			Assert.IsTrue (listlike3.TrueForAll (x => x.Account.Contains ("est1") || x.Account.Contains ("est2") || x.Account.Contains ("est3")));

			List<TeUser> listNotlike3 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (like3Array)).ToList ();
			Assert.AreEqual (6, listNotlike3.Count);
			Assert.IsTrue (listNotlike3.TrueForAll (x => !x.Account.Contains ("est1") && !x.Account.Contains ("est2") && !x.Account.Contains ("est3")));


			List<TeUser> listStart1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith (like1Array)).ToList ();
			Assert.AreEqual (14, listStart1.Count);
			Assert.IsTrue (listStart1.TrueForAll (x => x.Account.StartsWith ("test1") || x.Account.StartsWith ("test2") || x.Account.StartsWith ("test3")));

			List<TeUser> listNotStart1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotStartsWith (like1Array)).ToList ();
			Assert.AreEqual (6, listNotStart1.Count);
			Assert.IsTrue (listNotStart1.TrueForAll (x => !x.Account.StartsWith ("test1") && !x.Account.StartsWith ("test2") && !x.Account.StartsWith ("test3")));

			List<TeUser> listEnd1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.EndsWith (like2Array)).ToList ();
			Assert.AreEqual (6, listEnd1.Count);
			Assert.IsTrue (listEnd1.TrueForAll (x => x.Account.EndsWith ("2") || x.Account.EndsWith ("5") || x.Account.EndsWith ("6")));

			List<TeUser> listNotEnd1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotEndsWith (like2Array)).ToList ();
			Assert.AreEqual (14, listNotEnd1.Count);
			Assert.IsTrue (listNotEnd1.TrueForAll (x => !x.Account.EndsWith ("2") && !x.Account.EndsWith ("5") && !x.Account.EndsWith ("6")));

			List<TeUser> listContain1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.Contains (like3Array)).ToList ();
			Assert.AreEqual (14, listContain1.Count);
			Assert.IsTrue (listContain1.TrueForAll (x => x.Account.Contains ("est1") || x.Account.Contains ("est2") || x.Account.Contains ("est3")));

			List<TeUser> listNotContain1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotContains (like3Array)).ToList ();
			Assert.AreEqual (6, listNotContain1.Count);
			Assert.IsTrue (listNotContain1.TrueForAll (x => !x.Account.Contains ("est1") && !x.Account.Contains ("est2") && !x.Account.Contains ("est3")));

		}

		[Test ()]
		public void TestCase_QueryLikeMatch_Multi_String_Reverse ()
		{
			InitialUserTable (20);
			string[] like1Array = new string[]{ "mytest1", "mytest2", "mytest3" };

			List<TeUser> listlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike (like1Array)).ToList ();
			Assert.AreEqual (3, listlike4.Count);
			Assert.IsTrue (listlike4.TrueForAll (x => "mytest1".EndsWith (x.Account) || "mytest2".EndsWith (x.Account) || "mytest3".EndsWith (x.Account)));

			List<TeUser> listNotlike4 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike (like1Array)).ToList ();
			Assert.AreEqual (17, listNotlike4.Count);
			Assert.IsTrue (listNotlike4.TrueForAll (x => !"mytest1".EndsWith (x.Account) && !"mytest2".EndsWith (x.Account) && !"mytest3".EndsWith (x.Account)));
		
			string[] like2Array = new string[]{ "test11aaaa", "test22aaaa", "test33aaaa" };
			List<TeUser> listlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike (like2Array)).ToList ();
			Assert.AreEqual (4, listlike6.Count);
			Assert.IsTrue (listlike6.TrueForAll (x => "test11aaaa".StartsWith (x.Account) || "test22aaaa".StartsWith (x.Account) || "test33aaaa".StartsWith (x.Account)));

			List<TeUser> listNotlike6 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike (like2Array)).ToList ();
			Assert.AreEqual (16, listNotlike6.Count);
			Assert.IsTrue (listNotlike6.TrueForAll (x => !"test11aaaa".StartsWith (x.Account) && !"test22aaaa".StartsWith (x.Account) && !"test33aaaa".StartsWith (x.Account)));

			string[] like3Array = new string[]{ "mytest11aaaa", "mytest22aaaa", "mytest33aaaa" };
			List<TeUser> listlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike (like3Array)).ToList ();
			Assert.AreEqual (4, listlike8.Count);
			Assert.IsTrue (listlike8.TrueForAll (x => "mytest11aaaa".Contains (x.Account) || "mytest22aaaa".Contains (x.Account) || "mytest33aaaa".Contains (x.Account)));

			List<TeUser> listNotlike8 = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike (like2Array)).ToList ();
			Assert.AreEqual (16, listNotlike8.Count);
			Assert.IsTrue (listNotlike8.TrueForAll (x => !"mytest11aaaa".Contains (x.Account) && !"mytest22aaaa".Contains (x.Account) && !"mytest33aaaa".Contains (x.Account)));

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
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsTrue ()).ToList ();
			Assert.AreEqual (10, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.DeleteFlag));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsFalse ()).ToList ();
			Assert.AreEqual (11, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => !x.DeleteFlag));

			List<TeUser> listNull1 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == true).ToList ();
			Assert.AreEqual (10, listNull1.Count);
			Assert.IsTrue (listNull1.TrueForAll (x => x.DeleteFlag));

			List<TeUser> listNull2 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField != false).ToList ();
			Assert.AreEqual (10, listNull2.Count);
			Assert.IsTrue (listNull2.TrueForAll (x => x.DeleteFlag));

			List<TeUser> listNotNull1 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == false).ToList ();
			Assert.AreEqual (11, listNotNull1.Count);
			Assert.IsTrue (listNotNull1.TrueForAll (x => !x.DeleteFlag));

			List<TeUser> listNotNull2 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField != true).ToList ();
			Assert.AreEqual (11, listNotNull2.Count);
			Assert.IsTrue (listNotNull2.TrueForAll (x => !x.DeleteFlag));
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

		[Test ()]
		public void TestCase_QueryNull_Enum ()
		{
			InitialUserTable (21);
			List<TeUser> listNull = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField.IsNull ()).ToList ();
			Assert.AreEqual (6, listNull.Count);
			Assert.IsTrue (listNull.TrueForAll (x => x.CheckLevelType == null));

			List<TeUser> listNotNull = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField.IsNotNull ()).ToList ();
			Assert.AreEqual (15, listNotNull.Count);
			Assert.IsTrue (listNotNull.TrueForAll (x => x.CheckLevelType != null));
		}

		[Test ()]
		public void TestCase_Query_SubQuery ()
		{
			InitialUserTable (21);
			InitialUserLevelTable (8);
			List<TeUserLevel> listSub1 = context.LQuery<TeUserLevel> ().ToList ();
			List<TeUserLevel> listSub2 = context.LQuery<TeUserLevel> ().Where (TeUserLevel.StatusField == 1).ToList ();

			List<TeUser> list1 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (17, list1.Count);
			Assert.IsTrue (list1.TrueForAll (x => {
				return listSub1.Exists (y => y.Id == x.LevelId);
			}));

			List<TeUser> list2 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (9, list2.Count);
			Assert.IsTrue (list2.TrueForAll (x => {
				return listSub2.Exists (y => y.Id == x.LevelId);
			}));

			List<TeUser> list1n = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (4, list1n.Count);
			Assert.IsTrue (list1n.TrueForAll (x => {
				return listSub1.TrueForAll (y => y.Id != x.LevelId);
			}));

			List<TeUser> list2n = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (12, list2n.Count);
			Assert.IsTrue (list2n.TrueForAll (x => {
				return listSub2.TrueForAll (y => y.Id != x.LevelId);
			}));

			List<TeUser> list3 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAny (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (15, list3.Count);
			Assert.IsTrue (list3.TrueForAll (x => {
				return listSub1.Exists (y => y.Id > x.LevelId);
			}));

			List<TeUser> list4 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAny (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (13, list4.Count);
			Assert.IsTrue (list4.TrueForAll (x => {
				return listSub2.Exists (y => y.Id > x.LevelId);
			}));

			List<TeUser> list5 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAny (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (18, list5.Count);
			Assert.IsTrue (list5.TrueForAll (x => {
				return listSub1.Exists (y => y.Id < x.LevelId);
			}));

			List<TeUser> list6 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAny (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (18, list6.Count);
			Assert.IsTrue (list6.TrueForAll (x => {
				return listSub2.Exists (y => y.Id < x.LevelId);
			}));


			List<TeUser> list7 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAll (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (0, list7.Count);
	

			List<TeUser> list8 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAll (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (0, list8.Count);


			List<TeUser> list9 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAll (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (4, list9.Count);
			Assert.IsTrue (list9.TrueForAll (x => {
				return listSub1.TrueForAll (y => y.Id < x.LevelId);
			}));

			List<TeUser> list10 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAll (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (6, list10.Count);
			Assert.IsTrue (list10.TrueForAll (x => {
				return listSub2.TrueForAll (y => y.Id < x.LevelId);
			}));


		}

		[Test ()]
		public void TestCase_Query_Exists ()
		{
			InitialUserTable (21);
			InitialUserLevelTable (8);

			List<TeUser> list1 = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (21, list1.Count);

			List<TeUser> list2 = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.StatusField == 2)).ToList ();
			Assert.AreEqual (0, list2.Count);

			List<TeUser> list3 = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (0, list3.Count);

			List<TeUser> list4 = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.StatusField == 2)).ToList ();
			Assert.AreEqual (21, list4.Count);
		}

		[Test ()]
		public void TestCase_Query_SubQuery_Match ()
		{
			InitialUserTable (21);
			InitialUserLevelTable (8);
			List<TeUserLevel> listSub1 = context.LQuery<TeUserLevel> ().ToList ();
			List<TeUser> lists1 = context.LQuery<TeUser> ().Where (TeUser.IdField > 10 & TeUser.CheckLevelTypeField == null).ToList ();
			Assert.AreEqual (4, lists1.Count);
			Assert.IsTrue (lists1.TrueForAll (x => x.CheckLevelType == null));

			List<TeUser> lists2 = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField != null & TeUser.IdField > 10).ToList ();
			Assert.AreEqual (7, lists2.Count);
			Assert.IsTrue (lists2.TrueForAll (x => x.CheckLevelType != null));

			List<TeUser> lists3 = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField != null & TeUser.CheckPointField != null & TeUser.IdField > 10).ToList ();
			Assert.AreEqual (5, lists3.Count);
			Assert.IsTrue (lists3.TrueForAll (x => x.CheckLevelType != null));

			List<TeUser> list1 = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.IdField == TeUser.LevelIdField)).ToList ();
			Assert.AreEqual (17, list1.Count);
			Assert.IsTrue (list1.TrueForAll (x => {
				return listSub1.Exists (y => y.Id == x.LevelId);
			}));

			List<TeUser> list2 = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.IdField == TeUser.LevelIdField)).ToList ();
			Assert.AreEqual (4, list2.Count);
			Assert.IsTrue (list2.TrueForAll (x => {
				return listSub1.TrueForAll (y => y.Id != x.LevelId);
			}));

			List<TeUser> list3 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (17, list3.Count);
			Assert.IsTrue (list1.TrueForAll (x => {
				return listSub1.Exists (y => y.Id == x.LevelId);
			}));

			List<TeUser> list4 = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (4, list4.Count);
			Assert.IsTrue (list4.TrueForAll (x => {
				return listSub1.TrueForAll (y => y.Id != x.LevelId);
			}));
		}

		[Test ()]
		public void TestCase_Query_Multi ()
		{
			InitialUserTable (21);

			List<TeUser> list1 = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1") & TeUser.CheckPointField.IsNotNull ()).ToList ();
			Assert.AreEqual (5, list1.Count);
			Assert.IsTrue (list1.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null));
		
			List<TeUser> list2 = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1") & TeUser.CheckPointField.IsNotNull () & TeUser.IdField.Between (11, 17)).ToList ();
			Assert.AreEqual (3, list2.Count);
			Assert.IsTrue (list2.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17));
		}
	}
}

