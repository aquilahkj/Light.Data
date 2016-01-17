using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class BaseCommandTest:BaseTest
	{
		[Test ()]
		public void TestCase_CUD_Single ()
		{
			context.TruncateTable<TeUser> ();

			TeUser userInsert = CreateTestUser (true);
			userInsert.Save ();
			Assert.AreEqual (userInsert.Id, 1);
			TeUser user1 = context.SelectSingleFromId<TeUser> (userInsert.Id);
			Assert.NotNull (user1);
			Assert.True (EqualUser (userInsert, user1));
			user1.LastLoginTime = GetNow ();
			user1.Status = 2;
			user1.Save ();
			Assert.AreEqual (userInsert.Id, 1);
			TeUser user2 = context.SelectSingleFromId<TeUser> (userInsert.Id);
			Assert.NotNull (user2);
			Assert.True (EqualUser (user1, user2));
			user2.Erase ();
			TeUser user3 = context.SelectSingleFromId<TeUser> (userInsert.Id);
			Assert.Null (user3);
		}

		[Test ()]
		public void TestCase_CUD_Bluck ()
		{
			context.TruncateTable<TeUser> ();
			List<TeUser> list1 = new List<TeUser> ();
			const int count = 57;
			const int rdd = 10;
			for (int i = 0; i < count; i++) {
				TeUser userInsert = CreateTestUser (false);
				userInsert.Account += i;
				userInsert.RegTime = userInsert.RegTime.AddSeconds (i);
				list1.Add (userInsert);
			}
			int resultInsert = context.BulkInsert (list1.ToArray ());

			Assert.AreEqual (resultInsert, count);

			List<TeUser> list2 = context.LQuery<TeUser> ().ToList ();
			for (int i = 0; i < count; i++) {
				Assert.True (EqualUser (list1 [i], list2 [i], false));
			}

			List<UpdateSetValue> updates1 = new List<UpdateSetValue> ();
			DateTime uptime = GetNow ();
			updates1.Add (new UpdateSetValue (TeUser.LastLoginTimeField, uptime));
			updates1.Add (new UpdateSetValue (TeUser.StatusField, 2));
			int resultUpdate1 = context.UpdateMass<TeUser> (updates1.ToArray ());
			Assert.AreEqual (count, resultUpdate1);
			List<TeUser> list3 = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (count, list3.Count);
			Assert.IsTrue (list3.TrueForAll (x => x.LastLoginTime == uptime && x.Status == 2));


			List<UpdateSetValue> updates2 = new List<UpdateSetValue> ();
			updates2.Add (new UpdateSetValue (TeUser.StatusField, 3));
			int resultUpdate2 = context.UpdateMass<TeUser> (updates2.ToArray (), TeUser.IdField.Between (1, rdd));
			Assert.AreEqual (rdd, resultUpdate2);
			List<TeUser> list4 = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (count, list4.Count);
			Assert.IsTrue (list4.TrueForAll (x => {
				if (x.Id <= rdd) {
					return x.Status == 3;
				}
				else {
					return x.Status == 2;
				}
			}));


			int resultDelete1 = context.DeleteMass<TeUser> (TeUser.IdField.Between (1, rdd));
			Assert.AreEqual (rdd, resultDelete1);
			List<TeUser> list5 = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (count - rdd, list5.Count);

			Assert.IsTrue (list3.TrueForAll (x => x.Status == 2));

			int resultDelete2 = context.DeleteMass<TeUser> ();
			Assert.AreEqual (count - rdd, resultDelete2);
			List<TeUser> list6 = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (0, list6.Count);

		}
	

	}
}

