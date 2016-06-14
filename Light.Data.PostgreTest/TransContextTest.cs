using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.PostgreTest
{
	[TestFixture ()]
	public class TransContextTest : BaseTest
	{
		[Test ()]
		public void TestCase_TransInsert ()
		{
			context.TruncateTable<TeUser> ();
			TeUser user;
			TeUser userAc;
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.CreateNew<TeUser> ();
				user.Account = "test";
				user.Birthday = new DateTime (2001, 10, 20);
				user.Email = "test@test.com";
				user.Gender = GenderType.Female;
				user.LevelId = 1;
				user.NickName = "nicktest";
				user.Password = "imtest";
				user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
				user.Status = 1;
				user.Telephone = "12345678";
				user.HotRate = 1.0d;
				user.Save ();
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			AssertExtend.AreObjectsEqual (user, userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.CreateNew<TeUser> ();
				user.Account = "test";
				user.Birthday = new DateTime (2001, 10, 20);
				user.Email = "test@test.com";
				user.Gender = GenderType.Female;
				user.LevelId = 1;
				user.NickName = "nicktest";
				user.Password = "imtest";
				user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
				user.Status = 1;
				user.Telephone = "12345678";
				user.HotRate = 1.0d;
				user.Save ();
				trans.RollbackTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.Null (userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.CreateNew<TeUser> ();
				user.Account = "test";
				user.Birthday = new DateTime (2001, 10, 20);
				user.Email = "test@test.com";
				user.Gender = GenderType.Female;
				user.LevelId = 1;
				user.NickName = "nicktest";
				user.Password = "imtest";
				user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
				user.Status = 1;
				user.Telephone = "12345678";
				user.HotRate = 1.0d;
				user.Save ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.Null (userAc);
		}

		[Test ()]
		public void TestCase_TransUpdate ()
		{
			context.TruncateTable<TeUser> ();
			InitialUserTable (10);
			TeUser user;
			TeUser userAc;
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (1);
				user.Account = "testmodify";
				user.Save ();
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			Assert.AreEqual (user.Account, userAc.Account);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Account = "testmodify";
				user.Save ();
				trans.RollbackTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			Assert.AreNotEqual (user.Account, userAc.Account);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Account = "testmodify";
				user.Save ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			Assert.AreNotEqual (user.Account, userAc.Account);
		}

		[Test ()]
		public void TestCase_TransDelete ()
		{
			context.TruncateTable<TeUser> ();
			InitialUserTable (10);
			TeUser user;
			TeUser userAc;
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (1);
				user.Erase ();
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.Null (userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Erase ();
				trans.RollbackTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Erase ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
		}

		[Test ()]
		public void TestCase_TransMulti ()
		{
			context.TruncateTable<TeUser> ();
			InitialUserTable (10);
			TeUser user1;
			TeUser user2;
			TeUser userAc1;
			TeUser userAc2;
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (1);
				user1.Account = "testmodify";
				user1.Save ();
				user2 = trans.SelectSingleFromId<TeUser> (2);
				user2.Erase ();
				trans.CommitTrans ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.Null (userAc2);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (3);
				user1.Account = "testmodify";
				user1.Save ();
				user2 = trans.SelectSingleFromId<TeUser> (4);
				user2.Erase ();
				trans.RollbackTrans ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreNotEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.NotNull (userAc2);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (3);
				user1.Account = "testmodify";
				user1.Save ();
				user2 = trans.SelectSingleFromId<TeUser> (4);
				user2.Erase ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreNotEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.NotNull (userAc2);
		}


		[Test ()]
		public void TestCase_TransMulti2 ()
		{
			context.TruncateTable<TeUser> ();
			InitialUserTable (10);
			TeUser user1;
			TeUser user2;
			TeUser userAc1;
			TeUser userAc2;
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (1);
				user1.Account = "testmodify";
				user1.Save ();
				trans.CommitTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (2);
				user2.Erase ();
				trans.CommitTrans ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.Null (userAc2);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (3);
				user1.Account = "testmodify";
				user1.Save ();
				trans.CommitTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (4);
				user2.Erase ();
				trans.RollbackTrans ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.NotNull (userAc2);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user1 = trans.SelectSingleFromId<TeUser> (5);
				user1.Account = "testmodify";
				user1.Save ();
				trans.RollbackTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (6);
				user2.Erase ();
				trans.CommitTrans ();
			}
			userAc1 = context.SelectSingleFromId<TeUser> (user1.Id);
			Assert.NotNull (userAc1);
			Assert.AreNotEqual (user1.Account, userAc1.Account);

			userAc2 = context.SelectSingleFromId<TeUser> (user2.Id);
			Assert.Null (userAc2);
		}


		[Test ()]
		public void TestCase_TransBulkInsert ()
		{
			List<TeUser> listAc;
			List<TeUser> list = InitialUserTable (23, false);

			context.TruncateTable<TeUser> ();
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BulkInsert (list.ToArray ());
				trans.CommitTrans ();
			}
			listAc = context.LQuery<TeUser> ().ToList ();
			AssertExtend.AreEnumerableEqual (list, listAc);

			context.TruncateTable<TeUser> ();
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BulkInsert (list.ToArray ());
				trans.RollbackTrans ();
			}
			listAc = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (0, listAc.Count);


			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BulkInsert (list.ToArray ());
				trans.CommitTrans ();
			}
			listAc = context.LQuery<TeUser> ().ToList ();
			AssertExtend.AreEnumerableEqual (list, listAc);

			context.TruncateTable<TeUser> ();
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BulkInsert (list.ToArray ());
			}
			listAc = context.LQuery<TeUser> ().ToList ();
			Assert.AreEqual (0, listAc.Count);
		}
	}
}

