using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryMultiTest:BaseTest
	{
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
			InitialUserTable (count);
			List<TeUser> list = context.LQuery<TeUser> ().ToList ();

			List<int> lists = context.LQuery<TeUser> ().QuerySingleFieldList<int> (TeUser.IdField);
			Assert.AreEqual (list.Count, lists.Count);
			for (int i = 0; i < count; i++) {
				Assert.AreEqual (list [i].Id, lists [i]);
			}
			int[] arrays = context.LQuery<TeUser> ().QuerySingleFieldArray<int> (TeUser.IdField);
			Assert.AreEqual (list.Count, arrays.Length);
			for (int i = 0; i < count; i++) {
				Assert.AreEqual (list [i].Id, arrays [i]);
			}
			int index = 0;
			foreach (int id in context.LQuery<TeUser> ().QuerySingleField<int>(TeUser.IdField)) {
				Assert.AreEqual (list [index].Id, id);
				index++;
			}
		}


		[Test ()]
		public void TestCase_WhereOrderBy ()
		{
			InitialUserTable (21);


			List<TeUser> list3 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == 1).OrderBy (TeUser.IdField.OrderByAsc ()).ToList ();
			Assert.AreEqual (10, list3.Count);
			for (int i = 1; i < list3.Count; i++) {
				Assert.Less (list3 [i - 1].Id, list3 [i].Id);
			}

			List<TeUser> list4 = context.LQuery<TeUser> ().Where (TeUser.DeleteFlagField == 1).OrderBy (TeUser.IdField.OrderByDesc ()).ToList ();
			Assert.AreEqual (10, list4.Count);
			for (int i = 1; i < list4.Count; i++) {
				Assert.Greater (list4 [i - 1].Id, list4 [i].Id);
			}
		}
	}
}

