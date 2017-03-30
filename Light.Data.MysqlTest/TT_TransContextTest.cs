using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_TransContextTest : BaseTest
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
				trans.Insert (user);
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			AssertExtend.AreObjectEqual (user, userAc);

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
				trans.Insert (user);
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
				trans.Insert (user);
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
				trans.Update (user);
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			Assert.AreEqual (user.Account, userAc.Account);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Account = "testmodify";
				trans.Update (user);
				trans.RollbackTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);
			Assert.AreNotEqual (user.Account, userAc.Account);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				user.Account = "testmodify";
				trans.Update (user);
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
				trans.Delete (user);
				trans.CommitTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.Null (userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				trans.Delete (user);
				trans.RollbackTrans ();
			}
			userAc = context.SelectSingleFromId<TeUser> (user.Id);
			Assert.NotNull (userAc);

			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				user = trans.SelectSingleFromId<TeUser> (2);
				trans.Delete (user);
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
				trans.Update (user1);
				user2 = trans.SelectSingleFromId<TeUser> (2);
				trans.Delete (user2);
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
				trans.Update (user1);
				user2 = trans.SelectSingleFromId<TeUser> (4);
				trans.Delete (user2);
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
				trans.Update (user1);
				user2 = trans.SelectSingleFromId<TeUser> (4);
				trans.Delete (user2);
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
				trans.Update (user1);
				trans.CommitTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (2);
				trans.Delete (user2);
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
				trans.Update (user1);
				trans.CommitTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (4);
				trans.Delete (user2);
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
				trans.Update (user1);
				trans.RollbackTrans ();
				trans.BeginTrans ();
				user2 = trans.SelectSingleFromId<TeUser> (6);
				trans.Delete (user2);
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
				trans.BatchInsert (list.ToArray ());
				trans.CommitTrans ();
			}
			listAc = context.Query<TeUser> ().ToList ();
			AssertExtend.AreObjectEqual (list, listAc);

			context.TruncateTable<TeUser> ();
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BatchInsert (list.ToArray ());
				trans.RollbackTrans ();
			}
			listAc = context.Query<TeUser> ().ToList ();
			Assert.AreEqual (0, listAc.Count);


			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BatchInsert (list.ToArray ());
				trans.CommitTrans ();
			}
			listAc = context.Query<TeUser> ().ToList ();
			AssertExtend.AreObjectEqual (list, listAc);

			context.TruncateTable<TeUser> ();
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				trans.BatchInsert (list.ToArray ());
			}
			listAc = context.Query<TeUser> ().ToList ();
			Assert.AreEqual (0, listAc.Count);
		}


		[Test ()]
		public void TestCase_TransBulkInsert_Exception ()
		{

			List<TeUser> list = InitialUserTable (23, false);

			context.TruncateTable<TeUser> ();
			TeUser user = context.CreateNew<TeUser> ();
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
			context.Insert (user);
			try {
				TransDataContext trans = context.CreateTransDataContext ();
				try {

					trans.BeginTrans ();
					trans.BatchInsert (list.ToArray ());
		
					trans.Query<TeUser> ().Where (x => x.Id == 2).Update (x => new TeUser {
						Id = 3
					});

					trans.CommitTrans ();
				}
				catch (Exception e) {
					trans.RollbackTrans ();
					throw e;
				}
				finally {
					trans.Dispose ();
				}
			}
			catch (Exception ex) {
				user.Account = "";
				int ret = context.Update (user);
				Assert.AreEqual (1, ret);
				Console.WriteLine (ex);
			}

		}
	}
}

