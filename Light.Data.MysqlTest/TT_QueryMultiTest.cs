using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class LQueryMultiTest : BaseTest
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
		public void TestCase_QueryDoubleKey ()
		{
			context.TruncateTable<TeTagInfo> ();
			List<TeTagInfo> infos = new List<TeTagInfo> ();
			for (int i = 1; i <= 10; i++) {
				for (int j = 1; j <= 10; j++) {
					TeTagInfo tag = new TeTagInfo () { GroupCode = i.ToString (), TagCode = j.ToString (), TagName = "A" + i + "B" + j, Status = 1 };
					infos.Add (tag);
				}
			}
			context.BatchInsert (infos.ToArray ());
			TeTagInfo info;

			info = context.SelectSingleFromKey<TeTagInfo> ("2", "6");
			Assert.IsNotNull (info);
			Assert.AreEqual ("2", info.GroupCode);
			Assert.AreEqual ("6", info.TagCode);

			info = context.SelectSingleFromKey<TeTagInfo> ("9", "9");
			Assert.IsNotNull (info);
			Assert.AreEqual ("9", info.GroupCode);
			Assert.AreEqual ("9", info.TagCode);

			info = context.SelectSingleFromKey<TeTagInfo> ("11", "11");
			Assert.IsNull (info);
		}

		[Test ()]
		public void TeatCase_List_Array_Le ()
		{
			const int count = 57;
			InitialUserTable (count);
			List<TeUser> list = context.Query<TeUser> ().ToList ();

			List<int> lists = context.Query<TeUser> ().SelectField (x => x.Id).ToList ();
			Assert.AreEqual (list.Count, lists.Count);
			for (int i = 0; i < count; i++) {
				Assert.AreEqual (list [i].Id, lists [i]);
			}
			int [] arrays = context.Query<TeUser> ().SelectField (x => x.Id).ToArray ();
			Assert.AreEqual (list.Count, arrays.Length);
			for (int i = 0; i < count; i++) {
				Assert.AreEqual (list [i].Id, arrays [i]);
			}
			int index = 0;
			foreach (int id in context.Query<TeUser> ().SelectField (x => x.Id)) {
				Assert.AreEqual (list [index].Id, id);
				index++;
			}
		}


		[Test ()]
		public void TestCase_WhereOrderBy ()
		{
			InitialUserTable (21);


			List<TeUser> list3 = context.Query<TeUser> ().Where (x => x.DeleteFlag).OrderBy (x => x.Id).ToList ();
			Assert.AreEqual (10, list3.Count);
			for (int i = 1; i < list3.Count; i++) {
				Assert.Less (list3 [i - 1].Id, list3 [i].Id);
			}

			List<TeUser> list4 = context.Query<TeUser> ().Where (x => x.DeleteFlag).OrderByDescending (x => x.Id).ToList ();
			Assert.AreEqual (10, list4.Count);
			for (int i = 1; i < list4.Count; i++) {
				Assert.Greater (list4 [i - 1].Id, list4 [i].Id);
			}
		}
	}
}

