using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_BaseCommandTest2 : BaseTest
	{
		[Test ()]
		public void TestCase_CUD_Single ()
		{
			context.TruncateTable<TeUser2> ();

			TeUser2 userInsert = CreateTestUser2 ();
			context.Insert (userInsert);
			Assert.Greater (userInsert.Id, 0);
			TeUser2 user1 = context.SelectSingleFromId<TeUser2> (userInsert.Id);
			Assert.NotNull (user1);
			AssertExtend.AreTypeEqual (userInsert, user1);
			user1.LastLoginTime = GetNow ();
			user1.Status = 2;
			context.Update (user1);
			Assert.AreEqual (userInsert.Id, user1.Id);
			TeUser2 user2 = context.SelectSingleFromId<TeUser2> (userInsert.Id);
			Assert.NotNull (user2);
			AssertExtend.AreTypeEqual (user1, user2);
			context.Delete (user2);
			TeUser2 user3 = context.SelectSingleFromId<TeUser2> (userInsert.Id);
			Assert.Null (user3);
		}

		[Test ()]
		public void TestCase_InsertOrUpdate_Single ()
		{
			context.TruncateTable<TeUser2> ();

			TeUser2 userInsert = CreateTestUser2 ();
			context.Insert (userInsert);
			Assert.Greater (userInsert.Id, 0);
			int id = userInsert.Id;
			userInsert.Account = "abc";
			context.InsertOrUpdate (userInsert);
			context.InsertOrUpdate (userInsert);
			TeUser2 user1 = context.SelectSingleFromId<TeUser2> (userInsert.Id);
			AssertExtend.AreTypeEqual (userInsert, user1);

			userInsert.Id = 0;
			context.InsertOrUpdate (userInsert);
			Assert.AreEqual (id + 1, userInsert.Id);
			TeUser2 user2 = context.SelectSingleFromId<TeUser2> (userInsert.Id);
			AssertExtend.AreTypeEqual (userInsert, user2);

		}

		[Test ()]
		public void TestCase_BulkInsert ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 35;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);
			listAc = context.Query<TeUser2> ().ToList ();
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			foreach (TeUser2 item in listEx) {
				item.Account = "myAccount1";
			}
			result = context.BatchUpdate (listEx.ToArray ());
			listAc = context.Query<TeUser2> ().ToList ();
			AssertExtend.AreEnumerableEqual (listEx, listAc);


			result = context.BatchDelete (listEx.ToArray ());
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (0, listAc.Count);


			//context.TruncateTable<TeUser2> ();
			//result = context.BulkInsert (listEx.ToArray (), 20);
			//Assert.AreEqual (count, result);
			//listAc = context.Query<TeUser2> ().ToList ();
			//AssertExtend.AreEnumerableEqual (listEx, listAc);

			//context.TruncateTable<TeUser2> ();
			//result = context.BulkInsert (listEx.ToArray (), 100);
			//Assert.AreEqual (count, result);
			//listAc = context.Query<TeUser2> ().ToList ();
			//AssertExtend.AreEnumerableEqual (listEx, listAc);
		}



		[Test ()]
		public void TestCase_UpdateMass ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 33;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);

			DateTime uptime = GetNow ();

			result = context.Query<TeUser2> ().Update (x => new TeUser2 {
				LastLoginTime = uptime,
				Status = 2
			});
			Assert.AreEqual (count, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.LastLoginTime == uptime && x.Status == 2));

			const int rdd = 20;

			result = context.Query<TeUser2> ().Where (x => x.Id >= listEx [0].Id && x.Id <= listEx [0].Id + rdd - 1)
							.Update (x => new TeUser2 {
								Status = 3
							});
			Assert.AreEqual (rdd, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.Id <= rdd) {
					return x.Status == 3;
				}
				else {
					return true;
				}
			}));

			result = context.Query<TeUser2> ().Update (x => new TeUser2 {
				Area = 4,
				CheckLevelType = CheckLevelType.High
			});
			Assert.AreEqual (count, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Area == 4 && x.CheckLevelType == CheckLevelType.High));

			result = context.Query<TeUser2> ().Where (x => x.Id >= listEx [0].Id && x.Id <= listEx [0].Id + rdd - 1).Update (x => new TeUser2 {
				Status = 6
			});
			Assert.AreEqual (rdd, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.Id <= rdd) {
					return x.Status == 6;
				}
				else {
					return true;
				}
			}));
		}

		[Test ()]
		public void TestCase_DeleteMass1 ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 33;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);

			result = context.Query<TeUser2> ().Delete ();
			Assert.AreEqual (count, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (0, listAc.Count);
		}

		[Test ()]
		public void TestCase_DeleteMass2 ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 33;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);

			result = context.Query<TeUser2> ().Delete ();
			Assert.AreEqual (count, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (0, listAc.Count);
		}

		[Test ()]
		public void TestCase_DeleteMass3 ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 33;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;
			const int rdd = 20;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);

			result = context.Query<TeUser2> ().Where (x => x.Id >= listEx [0].Id && x.Id <= listEx [0].Id + rdd - 1).Delete ();
			Assert.AreEqual (rdd, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count - rdd, listAc.Count);
		}

		[Test ()]
		public void TestCase_DeleteMass4 ()
		{
			List<TeUser2> listEx = new List<TeUser2> ();
			const int count = 33;
			for (int i = 0; i < count; i++) {
				TeUser2 userInsert = CreateTestUser2 ();
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				listEx.Add (userInsert);
			}

			int result;
			List<TeUser2> listAc;
			int rdd = 20;

			context.TruncateTable<TeUser2> ();
			result = context.BatchInsert (listEx.ToArray ());
			Assert.AreEqual (count, result);

			result = context.Query<TeUser2> ().Where (x => x.Id >= listEx [0].Id && x.Id <= listEx [0].Id + rdd - 1).Delete ();
			Assert.AreEqual (rdd, result);
			listAc = context.Query<TeUser2> ().ToList ();
			Assert.AreEqual (count - rdd, listAc.Count);
		}
	}
}

