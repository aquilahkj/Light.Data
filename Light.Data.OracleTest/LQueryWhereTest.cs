using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class LQueryWhereTest:BaseTest
	{
		[Test ()]
		public void TestCase_QueryWhere_AndOr ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Id >= 5 && x.Id <= 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5 & TeUser.IdField <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 10));

			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5).WhereWithAnd (TeUser.IdField <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 10));

			listEx = list.FindAll (x => x.Id < 5 || x.Id > 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField < 5 | TeUser.IdField > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 5 || x.Id > 10));

			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField < 5).WhereWithOr (TeUser.IdField > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 5 || x.Id > 10));

			listEx = list.FindAll (x => x.Id <= 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5).Where (TeUser.IdField <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id <= 10));

			listEx = list;
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5).WhereReset ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
		}


		[Test ()]
		public void TestCase_QuerySingleParam_Enum ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Gender == GenderType.Female);
			listAc = context.LQuery<TeUser> ().Where (TeUser.GenderField == GenderType.Female).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Gender == GenderType.Female));

			listEx = list.FindAll (x => x.Gender == GenderType.Male);
			listAc = context.LQuery<TeUser> ().Where (TeUser.GenderField != GenderType.Female).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField == 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id == 10));

			listEx = list.FindAll (x => x.Id != 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField != 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id != 10));

			listEx = list.FindAll (x => x.Id > 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10));

			listEx = list.FindAll (x => x.Id >= 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 10));

			listEx = list.FindAll (x => x.Id < 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField < 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id < 10));

			listEx = list.FindAll (x => x.Id <= 10);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField <= 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id <= 10));

			listEx = list.FindAll (x => x.Id > 5 && x.Id < 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField > 5 & TeUser.IdField < 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 5 && x.Id < 15));

			listEx = list.FindAll (x => x.Id >= 5 && x.Id < 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5 & TeUser.IdField < 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id < 15));

			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField > 5 & TeUser.IdField <= 15).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 5 && x.Id <= 15));

			listEx = list.FindAll (x => x.Id >= 5 && x.Id <= 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField >= 5 & TeUser.IdField <= 15).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField == 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate == 1.10d));

			listEx = list.FindAll (x => x.HotRate != 1.10d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField != 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate != 1.10d));

			listEx = list.FindAll (x => x.HotRate > 1.10d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.10d));

			listEx = list.FindAll (x => x.HotRate >= 1.10d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.10d));

			listEx = list.FindAll (x => x.HotRate < 1.10d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField < 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate < 1.10d));

			listEx = list.FindAll (x => x.HotRate <= 1.10d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField <= 1.10d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate <= 1.10d));

			listEx = list.FindAll (x => x.HotRate > 1.05d && x.HotRate < 1.15d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.05d & TeUser.HotRateField < 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.05d && x.HotRate < 1.15d));

			listEx = list.FindAll (x => x.HotRate >= 1.05d && x.HotRate < 1.15d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.05d & TeUser.HotRateField < 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.05d && x.HotRate < 1.15d));

			listEx = list.FindAll (x => x.HotRate > 1.05d && x.HotRate <= 1.15d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField > 1.05d & TeUser.HotRateField <= 1.15d).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate > 1.05d && x.HotRate <= 1.15d));

			listEx = list.FindAll (x => x.HotRate >= 1.05d && x.HotRate <= 1.15d);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField >= 1.05d & TeUser.HotRateField <= 1.15d).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField == dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime == dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime != dt.AddMinutes (10));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField != dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime != dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (10));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (10));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime < dt.AddMinutes (10));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField < dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime < dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime <= dt.AddMinutes (10));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField <= dt.AddMinutes (10)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime <= dt.AddMinutes (10)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (5) & TeUser.RegTimeField < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (5) & TeUser.RegTimeField < dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime < dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField > dt.AddMinutes (5) & TeUser.RegTimeField <= dt.AddMinutes (15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime > dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField >= dt.AddMinutes (5) & TeUser.RegTimeField <= dt.AddMinutes (15)).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField == "test10").ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account == "test10"));

			listEx = list.FindAll (x => x.Account != "test10");
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField != "test10").ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account != "test10"));
		}

		[Test ()]
		public void TestCase_QueryBetween_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.Id >= 5 && x.Id <= 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.Between (5, 15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id >= 5 && x.Id <= 15));

			listEx = list.FindAll (x => !(x.Id >= 5 && x.Id <= 15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.NotBetween (5, 15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !(x.Id >= 5 && x.Id <= 15)));
		}

		[Test ()]
		public void TestCase_QueryBetween_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.HotRate >= 1.05 && x.HotRate <= 1.15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.Between (1.05, 1.15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.HotRate >= 1.05 && x.HotRate <= 1.15));

			listEx = list.FindAll (x => !(x.HotRate >= 1.05 && x.HotRate <= 1.15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.NotBetween (1.05, 1.15)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !(x.HotRate >= 1.05 && x.HotRate <= 1.15)));
		}

		[Test ()]
		public void TestCase_QueryBetween_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);

			listEx = list.FindAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.Between (dt.AddMinutes (5), dt.AddMinutes (15))).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));

			listEx = list.FindAll (x => !(x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15)));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.NotBetween (dt.AddMinutes (5), dt.AddMinutes (15))).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !(x.RegTime >= dt.AddMinutes (5) && x.RegTime <= dt.AddMinutes (15))));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			int[] arrayx = new int[]{ 3, 5, 7 };
			List<int> listx = new List<int> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.Id < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.LtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Id < y)));

			listEx = list.FindAll (x => listx.Exists (y => x.Id > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.GtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Id > y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Id < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.LtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Id < y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Id > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.GtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Id > y)));

			listEx = list.FindAll (x => listx.Exists (y => x.Id == y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.In (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Id == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Id != y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField.NotIn (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Id != y)));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			double[] arrayx = new double[]{ 1.03, 1.05, 1.07 };
			List<double> listx = new List<double> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.HotRate < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.LtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.HotRate < y)));

			listEx = list.FindAll (x => listx.Exists (y => x.HotRate > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.GtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.HotRate > y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.HotRate < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.LtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.HotRate < y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.HotRate > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.GtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.HotRate > y)));

			listEx = list.FindAll (x => listx.Exists (y => x.HotRate == y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.In (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.HotRate == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.HotRate != y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.HotRateField.NotIn (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.HotRate != y)));
		}

		[Test ()]
		public void TestCase_QueryAnyAllInArray_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			DateTime dt = new DateTime (2016, 1, 1, 18, 0, 0);
			DateTime d1 = dt.AddMinutes (3);
			DateTime d2 = dt.AddMinutes (5);
			DateTime d3 = dt.AddMinutes (7);
			DateTime[] arrayx = new DateTime[]{ d1, d2, d3 };
			List<DateTime> listx = new List<DateTime> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.RegTime < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.LtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.RegTime < y)));

			listEx = list.FindAll (x => listx.Exists (y => x.RegTime > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.GtAny (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.RegTime > y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.RegTime < y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.LtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.RegTime < y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.RegTime > y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.GtAll (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.RegTime > y)));

			listEx = list.FindAll (x => listx.Exists (y => x.RegTime == y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.In (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.RegTime == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.RegTime != y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.RegTimeField.NotIn (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.RegTime != y)));
		}

		[Test ()]
		public void TestCase_QueryInArray_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string[] arrayx = new string[]{ "test3", "test5", "test7" };

//			List<string> arrayx = new List<string>{ "test3", "test5", "test7" };
			List<string> listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.Account == y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.In (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Account == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Account != y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotIn (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => x.Account != y)));
		}

		[Test ()]
		public void TestCase_QueryInList_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			List<string> arrayx = new List<string>{ "test3", "test5", "test7" };
			List<string> listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => x.Account == y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.In (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => x.Account == y)));

			listEx = list.FindAll (x => listx.TrueForAll (y => x.Account != y));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotIn (arrayx)).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("test1%")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => !x.Account.StartsWith ("test1"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("test1%")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => x.Account.EndsWith ("5"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("%5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => !x.Account.EndsWith ("5"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("%5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => x.Account.Contains ("est2"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like ("%est2%")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.Contains ("est2")));

			listEx = list.FindAll (x => !x.Account.Contains ("est2"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike ("%est2%")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.Contains ("est2")));

			listEx = list.FindAll (x => x.Account.StartsWith ("test1"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => !x.Account.StartsWith ("test1"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotStartsWith ("test1")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.StartsWith ("test1")));

			listEx = list.FindAll (x => x.Account.EndsWith ("5"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.EndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => !x.Account.EndsWith ("5"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotEndsWith ("5")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.Account.EndsWith ("5")));

			listEx = list.FindAll (x => x.Account.Contains ("est2"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Contains ("est2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.Contains ("est2")));

			listEx = list.FindAll (x => !x.Account.Contains ("est2"));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotContains ("est2")).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike ("mytest2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest2".EndsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest2".EndsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike ("mytest2")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest2".EndsWith (x.Account)));

			listEx = list.FindAll (x => "mytest2a".EndsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike ("mytest2a")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest2a".EndsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest2a".EndsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike ("mytest2a")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest2a".EndsWith (x.Account)));

			listEx = list.FindAll (x => "test11aaaa".StartsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike ("test11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "test11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => !"test11aaaa".StartsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike ("test11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => "mytest11aaaa".StartsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => !"mytest11aaaa".StartsWith (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest11aaaa".StartsWith (x.Account)));

			listEx = list.FindAll (x => "mytest11aaaa".Contains (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => "mytest11aaaa".Contains (x.Account)));

			listEx = list.FindAll (x => !"mytest11aaaa".Contains (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseNotLike ("mytest11aaaa")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"mytest11aaaa".Contains (x.Account)));

			listEx = list.FindAll (x => "test".Contains (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike ("test")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test".Contains (x.Account)));

			listEx = list.FindAll (x => !"test".Contains (x.Account));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseNotLike ("test")).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !"test".Contains (x.Account)));
		}

		[Test ()]
		public void TestCase_QueryLikeMatch_Multi_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string[] arrayx;
			List<string> listx;

			arrayx = new string[] { "test1%", "test2%", "test3%" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.StartsWith (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.StartsWith (item);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.StartsWith (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.StartsWith (item);
			})));

			arrayx = new string[]{ "%2", "%5", "%6" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.EndsWith (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.EndsWith (item);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.EndsWith (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.StartsWith (item);
			})));

			arrayx = new string[]{ "%est1%", "%est2%", "%est3%" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.Contains (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Like (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				string item = y.Trim ('%');
				return x.Account.Contains (item);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.Contains (item);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				string item = y.Trim ('%');
				return !x.Account.Contains (item);
			})));



			arrayx = new string[] { "test1", "test2", "test3" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return x.Account.StartsWith (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return x.Account.StartsWith (y);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !x.Account.StartsWith (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotStartsWith (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !x.Account.StartsWith (y);
			})));

			arrayx = new string[]{ "2", "5", "6" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return x.Account.EndsWith (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.EndsWith (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return x.Account.EndsWith (y);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !x.Account.EndsWith (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotEndsWith (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !x.Account.EndsWith (y);
			})));

			arrayx = new string[]{ "est1", "est2", "est3" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return x.Account.Contains (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.Contains (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return x.Account.Contains (y);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !x.Account.Contains (y);
			}));
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.NotContains (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !x.Account.Contains (y);
			})));

		}

		[Test ()]
		public void TestCase_QueryLikeMatch_Multi_String_Reverse ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string[] arrayx;
			List<string> listx;

			arrayx = new string[] { "mytest1", "mytest2", "mytest3" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return y.EndsWith (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return y.EndsWith (x.Account);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !y.EndsWith (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformEndsWithMatch ()).ReverseNotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !y.EndsWith (x.Account);
			})));

			arrayx = new string[]{ "test11aaaa", "test22aaaa", "test33aaaa" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return y.StartsWith (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return y.StartsWith (x.Account);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !y.StartsWith (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformStartsWithMatch ()).ReverseNotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !y.StartsWith (x.Account);
			})));

			arrayx = new string[]{ "mytest11aaaa", "mytest22aaaa", "mytest33aaaa" };
			listx = new List<string> (arrayx);

			listEx = list.FindAll (x => listx.Exists (y => {
				return y.Contains (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.Exists (y => {
				return y.Contains (x.Account);
			})));

			listEx = list.FindAll (x => listx.TrueForAll (y => {
				return !y.Contains (x.Account);
			}));
			listAc = context.LQuery<TeUser> ().Where ((TeUser.AccountField.TransformContainsMatch ()).ReverseNotLike (arrayx)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listx.TrueForAll (y => {
				return !y.Contains (x.Account);
			})));
		}

		[Test ()]
		public void TestCase_QueryNull_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.RefereeId == null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.RefereeIdField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.RefereeId == null));

			listEx = list.FindAll (x => x.RefereeId != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.RefereeIdField.IsNotNull ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckPointField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckPoint == null));

			listEx = list.FindAll (x => x.CheckPoint != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckPointField.IsNotNull ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.LastLoginTimeField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.LastLoginTime == null));

			listEx = list.FindAll (x => x.LastLoginTime != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LastLoginTimeField.IsNotNull ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.AddressField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Address == null));

			listEx = list.FindAll (x => x.Address != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.AddressField.IsNotNull ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsTrue ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag));

			listEx = list.FindAll (x => !x.DeleteFlag);
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField.IsFalse ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.DeleteFlag));

			listEx = list.FindAll (x => x.DeleteFlag);
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == true).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag));

			listEx = list.FindAll (x => x.DeleteFlag);
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField != false).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.DeleteFlag));

			listEx = list.FindAll (x => !x.DeleteFlag);
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == false).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.DeleteFlag));

			listEx = list.FindAll (x => !x.DeleteFlag);
			listAc = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField != true).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => !x.DeleteFlag));
		}

		[Test ()]
		public void TestCase_QueryNull_Boolean ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUser> listEx;
			List<TeUser> listAc;

			listEx = list.FindAll (x => x.CheckStatus == null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckStatusField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckStatus == null));

			listEx = list.FindAll (x => x.CheckStatus != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckStatusField.IsNotNull ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField.IsNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckLevelType == null));

			listEx = list.FindAll (x => x.CheckLevelType != null);
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField.IsNotNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckLevelType != null));
		}

		[Test ()]
		public void TestCase_Query_SubQuery ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (8);
			List<TeUser> listEx;
			List<TeUser> listAc;
			List<TeUserLevel> listSub;

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id > x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAny (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id > x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id > x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAny (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id > x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id < x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAny (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id < x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id < x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAny (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id < x.LevelId);
			}));


			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id > x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAll (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id > x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id > x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.LtAll (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id > x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id < x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAll (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id < x.LevelId);
			}));

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id < x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.GtAll (TeUserLevel.IdField, TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id < x.LevelId);
			}));

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
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 2);
			listEx = list.FindAll (x => { 
				return listSub.Count > 0;
			});
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.StatusField == 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 1);
			listEx = list.FindAll (x => { 
				return listSub.Count == 0;
			});
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.StatusField == 1)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			listSub = listLevel.FindAll (x => x.Status == 2);
			listEx = list.FindAll (x => { 
				return listSub.Count == 0;
			});
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.StatusField == 2)).ToList ();
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

			listEx = list.FindAll (x => { 
				return x.Id > 10 && x.CheckLevelType == null;
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField > 10 & TeUser.CheckLevelTypeField == null).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			listEx = list.FindAll (x => { 
				return x.Id > 10 && x.CheckLevelType == null;
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField == null & TeUser.IdField > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			listEx = list.FindAll (x => { 
				return x.Id > 10 && x.CheckLevelType == null;
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.CheckLevelTypeField == null & TeUser.IdField > 10).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.CheckLevelType == null));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.Exists (TeUserLevel.IdField == TeUser.LevelIdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (QueryExpression.NotExists (TeUserLevel.IdField == TeUser.LevelIdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.In (TeUserLevel.IdField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listSub.Exists (y => y.Id == x.LevelId);
			}));

			listSub = listLevel;
			listEx = list.FindAll (x => { 
				return listSub.TrueForAll (y => y.Id != x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField.NotIn (TeUserLevel.IdField)).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes + 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField + 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes + 2);

			listEx = list.FindAll (x => x.LevelId == 2 + x.LoginTimes);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField + 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 + x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes - 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField - 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes - 2);

			listEx = list.FindAll (x => x.LevelId == 2 - x.LoginTimes);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 - TeUser.LoginTimesField).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 - x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes * 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField * 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes * 2);

			listEx = list.FindAll (x => x.LevelId == 2 * x.LoginTimes);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 * TeUser.LoginTimesField).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 * x.LoginTimes);

			listEx = list.FindAll (x => Convert.ToDouble (x.LevelId) == Convert.ToDouble (x.LoginTimes) / 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField / 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => Convert.ToDouble (x.LevelId) == Convert.ToDouble (x.LoginTimes) / 2);

			listEx = list.FindAll (x => Convert.ToDouble (x.LevelId) == 2 / Convert.ToDouble (x.LoginTimes));
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 / TeUser.LoginTimesField).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => Convert.ToDouble (x.LevelId) == 2 / Convert.ToDouble (x.LoginTimes));

			listEx = list.FindAll (x => x.LevelId == x.LoginTimes % 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.LoginTimesField % 2).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == x.LoginTimes % 2);

			listEx = list.FindAll (x => x.LevelId == 2 % x.LoginTimes);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 % TeUser.LoginTimesField).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 % x.LoginTimes);

			listEx = list.FindAll (x => x.LevelId == Math.Pow (x.LoginTimes, 2));
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == (TeUser.LoginTimesField ^ 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == Math.Pow (x.LoginTimes, 2));

			listEx = list.FindAll (x => x.LevelId == Math.Pow (2, x.LoginTimes));
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == (2 ^ TeUser.LoginTimesField)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == Math.Pow (2, x.LoginTimes));
		
			listEx = list.FindAll (x => x.LevelId == 2 * (x.LoginTimes + 2));
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 * (TeUser.LoginTimesField + 2)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			listAc.TrueForAll (x => x.LevelId == 2 * (x.LoginTimes + 2));

			listEx = list.FindAll (x => x.LevelId == 2 + x.LoginTimes * 2);
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == 2 + TeUser.LoginTimesField * 2).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.LevelIdField == TeUser.MarkField.TransformAbs ()).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.LoginTimesField.TransformLog () > 1.2d).ToList ();
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
				double d = Math.Exp (x.Mark);
				if (!double.IsInfinity (d) && !double.IsNaN (d)) {
					return d > 5;
				}
				else {
					return false;
				}
			});
			listAc = context.LQuery<TeUser> ().Where (TeUser.MarkField.TransformExp () > 5).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.MarkField.TransformSin () > 0.5d).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.MarkField.TransformCos () > 0.5d).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.MarkField.TransformTan () > 0.5d).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.MarkField.TransformAtan () > 0.5d).ToList ();
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
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1") & TeUser.CheckPointField.IsNotNull ()).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null));
		
			listEx = list.FindAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17);
			listAc = context.LQuery<TeUser> ().Where (TeUser.AccountField.StartsWith ("test1") & TeUser.CheckPointField.IsNotNull () & TeUser.IdField.Between (11, 17)).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Account.StartsWith ("test1") && x.CheckPoint != null && x.Id >= 11 && x.Id <= 17));
		}
	}
}

