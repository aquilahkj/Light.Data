using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_QueryWhereTest : BaseTest
	{
		[Test ()]
		public void TestCase_QueryWhere_AndOr ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Id >= 5 && x.Id <= 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5 && x.Id <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 10));

			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5).WhereWithAnd (x => x.Id <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 10));

			listEx = list.FindAll (x => x.Id < 5 || x.Id > 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id < 5 || x.Id > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 5 || x.Id > 10));

			listAc = context.Query<TeUser> ().Where (x => x.Id < 5).WhereWithOr (x => x.Id > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 5 || x.Id > 10));

			listEx = list.FindAll (x => x.Id <= 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5).Where (x => x.Id <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id <= 10));

			listEx = list;
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5).WhereReset ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
		}


		[Test ()]
		public void TestCase_QuerySingleParam_Enum ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Gender == GenderType.Female);
			listAc = context.Query<TeUser> ().Where (x => x.Gender == GenderType.Female).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Gender == GenderType.Female));

			listEx = list.FindAll (x => x.Gender == GenderType.Male);
			listAc = context.Query<TeUser> ().Where (x => x.Gender != GenderType.Female).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Gender == GenderType.Male));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Id == 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id == 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id == 10));

			listEx = list.FindAll (x => x.Id != 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id != 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id != 10));

			listEx = list.FindAll (x => x.Id > 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10));

			listEx = list.FindAll (x => x.Id >= 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 10));

			listEx = list.FindAll (x => x.Id < 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id < 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 10));

			listEx = list.FindAll (x => x.Id <= 10);
			listAc = context.Query<TeUser> ().Where (x => x.Id <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id <= 10));

			listEx = list.FindAll (x => x.Id > 5 && x.Id < 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id > 5 && x.Id < 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 5 && x.Id < 15));

			listEx = list.FindAll (x => x.Id >= 5 && x.Id < 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5 && x.Id < 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id < 15));

			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id > 5 && x.Id <= 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 5 && x.Id <= 15));

			listEx = list.FindAll (x => x.Id >= 5 && x.Id <= 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id >= 5 && x.Id <= 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 15));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.HotRate == 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate == 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate == 1.10d));

			listEx = list.FindAll (x => x.HotRate != 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate != 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate != 1.10d));

			listEx = list.FindAll (x => x.HotRate > 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate > 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.10d));

			listEx = list.FindAll (x => x.HotRate >= 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate >= 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.10d));

			listEx = list.FindAll (x => x.HotRate < 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate < 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate < 1.10d));

			listEx = list.FindAll (x => x.HotRate <= 1.10d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate <= 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate <= 1.10d));

			listEx = list.FindAll (x => x.HotRate > 1.05d && x.HotRate < 1.15d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate > 1.05d && x.HotRate < 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.05d && x.HotRate < 1.15d));

			listEx = list.FindAll (x => x.HotRate >= 1.05d && x.HotRate < 1.15d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate >= 1.05d && x.HotRate < 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.05d && x.HotRate < 1.15d));

			listEx = list.FindAll (x => x.HotRate > 1.05d && x.HotRate <= 1.15d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate > 1.05d && x.HotRate <= 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.05d && x.HotRate <= 1.15d));

			listEx = list.FindAll (x => x.HotRate >= 1.05d && x.HotRate <= 1.15d);
			listAc = context.Query<TeUser> ().Where (x => x.HotRate >= 1.05d && x.HotRate <= 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.05d && x.HotRate <= 1.15d));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);

			listEx = list.FindAll (x => x.RegTime == dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime == dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime == dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime != dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime != dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime != dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime > dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime >= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime < dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime < dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime < dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime <= dt.AddMinutes (10));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime <= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime <= dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15));
			listAc = context.Query<TeUser> ().Where (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));
		}

		[Test ()]
		public void TestCase_QuerySingleParam_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Account == "test10");
			listAc = context.Query<TeUser> ().Where (x => x.Account == "test10").ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account == "test10"));

			listEx = list.FindAll (x => x.Account != "test10");
			listAc = context.Query<TeUser> ().Where (x => x.Account != "test10").ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account != "test10"));
		}



		[Test ()]
		public void TestCase_QueryIn_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			int [] arrayx = new int [] { 3, 5, 7 };
			List<int> listx = new List<int> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.Id == y));
			listAc = context.Query<TeUser> ().Where (x => listx.Contains (x.Id)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Id == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Id != y));
			listAc = context.Query<TeUser> ().Where (x => !listx.Contains (x.Id)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Id != y)));
		}

		[Test ()]
		public void TestCase_QueryIn_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			double [] arrayx = new double [] { 1.03, 1.05, 1.07 };
			List<double> listx = new List<double> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.HotRate == y));
			listAc = context.Query<TeUser> ().Where (x => listx.Contains (x.HotRate)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.HotRate == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.HotRate != y));
			listAc = context.Query<TeUser> ().Where (x => !listx.Contains (x.HotRate)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.HotRate != y)));
		}

		[Test ()]
		public void TestCase_QueryIn_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);
			DateTime d1 = dt.AddMinutes (3);
			DateTime d2 = dt.AddMinutes (5);
			DateTime d3 = dt.AddMinutes (7);
			DateTime [] arrayx = new DateTime [] { d1, d2, d3 };
			List<DateTime> listx = new List<DateTime> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.RegTime == y));
			listAc = context.Query<TeUser> ().Where (x => listx.Contains (x.RegTime)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.RegTime == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.RegTime != y));
			listAc = context.Query<TeUser> ().Where (x => !listx.Contains (x.RegTime)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.RegTime != y)));
		}

		[Test ()]
		public void TestCase_QueryIn_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string [] arrayx = new string [] { "test3", "test5", "test7" };

			List<string> listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.Account == y));
			listAc = context.Query<TeUser> ().Where (x => listx.Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Account == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Account != y));
			listAc = context.Query<TeUser> ().Where (x => !listx.Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Account != y)));
		}


		[Test ()]
		public void TestCase_QueryLikeMatch_Single_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Account.StartsWith ("test1"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.StartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => !x.Account.StartsWith ("test1"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.StartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => x.Account.EndsWith ("5"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.EndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => !x.Account.EndsWith ("5"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.EndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => x.Account.Contains ("est2"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.Contains ("est2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.Contains ("est2")));

			listEx = list.FindAll (x => !x.Account.Contains ("est2"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.Contains ("est2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.Contains ("est2")));

			listEx = list.FindAll (x => x.Account.StartsWith ("test1"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.StartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => !x.Account.StartsWith ("test1"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.StartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => x.Account.EndsWith ("5"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.EndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => !x.Account.EndsWith ("5"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.EndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => x.Account.Contains ("est2"));
			listAc = context.Query<TeUser> ().Where (x => x.Account.Contains ("est2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.Contains ("est2")));

			listEx = list.FindAll (x => !x.Account.Contains ("est2"));
			listAc = context.Query<TeUser> ().Where (x => !x.Account.Contains ("est2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.Contains ("est2")));

		}

		[Test ()]
		public void TestCAse_QueryLikeMatch_Single_String_Reverse ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => "mytest2".EndsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "mytest2".EndsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest2".EndsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest2".EndsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"mytest2".EndsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest2".EndsWith (x.Account)));

			listEx = list.FindAll (x => "mytest2a".EndsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "mytest2a".EndsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest2a".EndsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest2a".EndsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"mytest2a".EndsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest2a".EndsWith (x.Account)));

			listEx = list.FindAll (x => "test11aaaa".StartsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "test11aaaa".StartsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "test11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => !"test11aaaa".StartsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"test11aaaa".StartsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => "mytest11aaaa".StartsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "mytest11aaaa".StartsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest11aaaa".StartsWith (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"mytest11aaaa".StartsWith (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => "mytest11aaaa".Contains (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "mytest11aaaa".Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest11aaaa".Contains (x.Account)));

			listEx = list.FindAll (x => !"mytest11aaaa".Contains (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"mytest11aaaa".Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest11aaaa".Contains (x.Account)));

			listEx = list.FindAll (x => "test".Contains (x.Account));
			listAc = context.Query<TeUser> ().Where (x => "test".Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test".Contains (x.Account)));

			listEx = list.FindAll (x => !"test".Contains (x.Account));
			listAc = context.Query<TeUser> ().Where (x => !"test".Contains (x.Account)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test".Contains (x.Account)));
		}



		[Test ()]
		public void TestCase_QueryNull_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.RefereeId == null);
			listAc = context.Query<TeUser> ().Where (x => x.RefereeId == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RefereeId == null));

			listEx = list.FindAll (x => x.RefereeId != null);
			listAc = context.Query<TeUser> ().Where (x => x.RefereeId != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RefereeId != null));
		}

		[Test ()]
		public void TestCase_QueryNull_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.CheckPoint == null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckPoint == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckPoint == null));

			listEx = list.FindAll (x => x.CheckPoint != null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckPoint != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckPoint != null));
		}

		[Test ()]
		public void TestCase_QueryNull_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.LastLoginTime == null);
			listAc = context.Query<TeUser> ().Where (x => x.LastLoginTime == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.LastLoginTime == null));

			listEx = list.FindAll (x => x.LastLoginTime != null);
			listAc = context.Query<TeUser> ().Where (x => x.LastLoginTime != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.LastLoginTime != null));
		}

		[Test ()]
		public void TestCase_QueryNull_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Address == null);
			listAc = context.Query<TeUser> ().Where (x => x.Address == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Address == null));

			listEx = list.FindAll (x => x.Address != null);
			listAc = context.Query<TeUser> ().Where (x => x.Address != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Address != null));
		}

		[Test ()]
		public void TestCase_Query_Boolean ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.DeleteFlag);
			listAc = context.Query<TeUser> ().Where (x => x.DeleteFlag).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag));

			listEx = list.FindAll (x => !x.DeleteFlag);
			listAc = context.Query<TeUser> ().Where (x => !x.DeleteFlag).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.DeleteFlag));

			listEx = list.FindAll (x => x.DeleteFlag);
			listAc = context.Query<TeUser> ().Where (x => x.DeleteFlag).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag));

			listEx = list.FindAll (x => x.DeleteFlag != false);
			listAc = context.Query<TeUser> ().Where (x => x.DeleteFlag != false).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag != false));

			listEx = list.FindAll (x => x.DeleteFlag == false);
			listAc = context.Query<TeUser> ().Where (x => x.DeleteFlag == false).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag == false));

			listEx = list.FindAll (x => x.DeleteFlag != true);
			listAc = context.Query<TeUser> ().Where (x => x.DeleteFlag != true).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag != true));
		}

		[Test ()]
		public void TestCase_QueryNull_Boolean ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.CheckStatus == null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckStatus == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckStatus == null));

			listEx = list.FindAll (x => x.CheckStatus != null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckStatus != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckStatus != null));
		}

		[Test ()]
		public void TestCase_QueryNull_Enum ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.CheckLevelType == null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckLevelType == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckLevelType == null));

			listEx = list.FindAll (x => x.CheckLevelType != null);
			listAc = context.Query<TeUser> ().Where (x => x.CheckLevelType != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckLevelType != null));
		}

		[Test ()]
		public void TestCase_Query_Exists ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (8);
			List<TeUser> listEx;
			List<TeUser> listAc;
			List<TeUserLevel> listSub;

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => {
				return listSub.Count > 0;
			});
			listAc = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 2);
			listEx = list.FindAll (x => {
				return listSub.Count > 0;
			});
			listAc = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Status == 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => {
				return listSub.Count == 0;
			});
			listAc = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 2);
			listEx = list.FindAll (x => {
				return listSub.Count == 0;
			});
			listAc = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Status == 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
		}

		[Test ()]
		public void TestCase_Query_SubQuery_Match ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (8);
			List<TeUser> listEx;
			List<TeUser> listAc;
			List<TeUserLevel> listSub;

			//listEx = list.FindAll (x => {
			//	return x.Id > 10 && x.CheckLevelType == null;
			//});
			//listAc = context.Query<TeUser> ().Where (TeUser.IdField > 10 & TeUser.CheckLevelTypeField == null).ToList ();
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			//listEx = list.FindAll (x => {
			//	return x.Id > 10 && x.CheckLevelType == null;
			//});
			//listAc = context.Query<TeUser> ().Where (TeUser.CheckLevelTypeField == null & TeUser.IdField > 10).ToList ();
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			//listEx = list.FindAll (x => {
			//	return x.Id > 10 && x.CheckLevelType == null;
			//});
			//listAc = context.Query<TeUser> ().Where (TeUser.CheckLevelTypeField == null & TeUser.IdField > 10).ToList ();
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			listSub = listLevel;
			listEx = list.FindAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => {
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			});
			listAc = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			}));


		}

		[Test ()]
		public void TestCase_Query_FieldMatch ()
		{
			List<TeUser> list = InitialUserTable (51);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == x.LoginTimes).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes + 2);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == x.LoginTimes + 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes + 2);

			listEx = list.FindAll (x => x.LevelId == 2 + x.LoginTimes);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 + x.LoginTimes).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 + x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes - 2);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == x.LoginTimes - 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes - 2);

			listEx = list.FindAll (x => x.LevelId == 2 - x.LoginTimes);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 - x.LoginTimes).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 - x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes * 2);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == x.LoginTimes * 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes * 2);

			listEx = list.FindAll (x => x.LevelId == 2 * x.LoginTimes);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 * x.LoginTimes).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 * x.LoginTimes);

			listEx = list.FindAll (x => x.CheckPoint >= x.HotRate / 2);
			listAc = context.Query<TeUser> ().Where (x => x.CheckPoint >= x.HotRate / 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.CheckPoint >= x.HotRate / 2);

			listEx = list.FindAll (x => x.CheckPoint >= 2 / x.HotRate);
			listAc = context.Query<TeUser> ().Where (x => x.CheckPoint >= 2 / x.HotRate).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.CheckPoint >= 2 / x.HotRate);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes % 2);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == x.LoginTimes % 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes % 2);

			listEx = list.FindAll (x => x.LevelId == 2 % x.LoginTimes);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 % x.LoginTimes).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 % x.LoginTimes);

			listEx = list.FindAll (x => x.HotRate >= Math.Pow (x.LoginTimes, 2));
			listAc = context.Query<TeUser> ().Where (x => x.HotRate >= Math.Pow (x.LoginTimes, 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.HotRate >= Math.Pow (x.LoginTimes, 2));

			listEx = list.FindAll (x => x.HotRate >= Math.Pow (2, x.LoginTimes));
			listAc = context.Query<TeUser> ().Where (x => x.HotRate >= Math.Pow (2, x.LoginTimes)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.HotRate >= Math.Pow (2, x.LoginTimes));

			listEx = list.FindAll (x => x.LevelId == 2 * (x.LoginTimes + 2));
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 * (x.LoginTimes + 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 * (x.LoginTimes + 2));

			listEx = list.FindAll (x => x.LevelId == 2 + x.LoginTimes * 2);
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == 2 + x.LoginTimes * 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 + x.LoginTimes * 2);
		}

		[Test ()]
		public void TestCase_Query_MathMatch ()
		{
			List<TeUser> list = InitialUserTable (51);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.LevelId == Math.Abs (x.Mark));
			listAc = context.Query<TeUser> ().Where (x => x.LevelId == Math.Abs (x.Mark)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == Math.Abs (x.Mark));

			listEx = list.FindAll (x => {
				double d = Math.Log (x.LoginTimes);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 1.2d;
				}
				else {
					return false;
				}
			});

			listAc = context.Query<TeUser> ().Where (x => Math.Log (x.LoginTimes) > 1.2d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Log (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 1.2d;
				}
				else {
					return false;
				}
			});

			listEx = list.FindAll (x => {
				double d = Math.Log10 (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 1.2d;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Log10 (x.Mark) > 1.2d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Log10 (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 1.2d;
				}
				else {
					return false;
				}
			});


			listEx = list.FindAll (x => {
				double d = Math.Exp (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 5;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Exp (x.Mark) > 5).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Exp (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 5;
				}
				else {
					return false;
				}
			});

			listEx = list.FindAll (x => {
				double d = Math.Sin (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Sin (x.Mark) > 0.5d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Sin (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});

			listEx = list.FindAll (x => {
				double d = Math.Cos (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Cos (x.Mark) > 0.5d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Cos (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});

			listEx = list.FindAll (x => {
				double d = Math.Tan (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Tan (x.Mark) > 0.5d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Tan (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});

			listEx = list.FindAll (x => {
				double d = Math.Atan (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});
			listAc = context.Query<TeUser> ().Where (x => Math.Atan (x.Mark) > 0.5d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => {
				double d = Math.Atan (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 0.5d;
				}
				else {
					return false;
				}
			});
		}

		[Test ()]
		public void TestCase_Query_Multi ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null);
			listAc = context.Query<TeUser> ().Where (x => x.Account.StartsWith ("test1") && x.CheckPoint != null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null));

			listEx = list.FindAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17);
			listAc = context.Query<TeUser> ().Where (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17));
		}
	}
}

