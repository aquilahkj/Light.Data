using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

namespace Light.Data.PostgreTest
{
	[TestFixture ()]
	public class LQuerySingleFileldTest:BaseTest
	{
		[Test ()]
		public void TeatCase_List_Array_Le ()
		{
			const int count = 57;
			List<TeUser> list1 = InitialUserTable (count);
			List<TeUser> listReslt = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (count, listReslt.Count);
			for (int i = 0; i < count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list1 [i], true));
			}
			TeUser[] arrayResult = context.LQuery<TeUser> ().ToArray ();
			Assert.AreEqual (count, arrayResult.Length);
			for (int i = 0; i < count; i++) {
				Assert.IsTrue (EqualUser (arrayResult [i], list1 [i], true));
			}
			int index = 0;
			foreach (TeUser user in context.LQuery<TeUser> ()) {
				Assert.IsTrue (EqualUser (user, list1 [index], true));
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

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<int> (TeUser.IdField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<int> (TeUser.IdField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Id, list_s [i]);
			}

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<int> (TeUser.LevelIdField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.LevelIdField <= 5).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.LevelIdField <= 5).QuerySingleFieldList<int> (TeUser.LevelIdField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LevelId, list_s [i]);
			}

			list = context.LQuery<TeUser> ().ToList ();
			list_null_s = context.LQuery<TeUser> ().QuerySingleFieldList<int?> (TeUser.AreaField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Area, list_null_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.AreaField > 5).ToList ();
			list_null_s = context.LQuery<TeUser> ().Where (TeUser.AreaField > 5).QuerySingleFieldList<int?> (TeUser.AreaField);
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

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<double> (TeUser.HotRateField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<double> (TeUser.HotRateField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].HotRate, list_s [i]);
			}
				

			list = context.LQuery<TeUser> ().ToList ();
			list_null_s = context.LQuery<TeUser> ().QuerySingleFieldList<double?> (TeUser.CheckPointField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckPoint, list_null_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_null_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<double?> (TeUser.CheckPointField);
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

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<DateTime> (TeUser.RegTimeField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<DateTime> (TeUser.RegTimeField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].RegTime, list_s [i]);
			}


			list = context.LQuery<TeUser> ().ToList ();
			list_null_s = context.LQuery<TeUser> ().QuerySingleFieldList<DateTime?> (TeUser.LastLoginTimeField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].LastLoginTime, list_null_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_null_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<DateTime?> (TeUser.LastLoginTimeField);
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

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<string> (TeUser.AccountField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<string> (TeUser.AccountField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Account, list_s [i]);
			}


			list = context.LQuery<TeUser> ().ToList ();
			list_null_s = context.LQuery<TeUser> ().QuerySingleFieldList<string> (TeUser.AddressField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_null_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<string> (TeUser.AddressField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Address, list_null_s [i]);
			}
		}

		[Test ()]
		public void TestCase_Field_Enum ()
		{
			InitialUserTable (21);

			List<TeUser> list;
			List<GenderType> list_s;
			List<CheckLevelType?> list_null_s;

			list = context.LQuery<TeUser> ().ToList ();
			list_s = context.LQuery<TeUser> ().QuerySingleFieldList<GenderType> (TeUser.GenderField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Gender, list_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<GenderType> (TeUser.GenderField);
			Assert.AreEqual (list.Count, list_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].Gender, list_s [i]);
			}


			list = context.LQuery<TeUser> ().ToList ();
			list_null_s = context.LQuery<TeUser> ().QuerySingleFieldList<CheckLevelType?> (TeUser.CheckLevelTypeField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckLevelType, list_null_s [i]);
			}

			list = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).ToList ();
			list_null_s = context.LQuery<TeUser> ().Where (TeUser.IdField > 10).QuerySingleFieldList<CheckLevelType?> (TeUser.CheckLevelTypeField);
			Assert.AreEqual (list.Count, list_null_s.Count);
			for (int i = 0; i < list.Count; i++) {
				Assert.AreEqual (list [i].CheckLevelType, list_null_s [i]);
			}
		}
	}
}

