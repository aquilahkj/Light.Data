using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class SqlExecutorTest:BaseTest
	{
		[Test ()]
		public void TestCase_SqlString_QueryList ()
		{
			List<TeUser> list = InitialUserTable (10);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "select * from Te_User";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> ();
			listEx = list;
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> (5, 3);
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User where id>5 and id<=8";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User where id>@P1 and id<=@P2";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			executor = context.CreateSqlStringExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				listAc = executor.QueryList<TeUser> ();
				trans.CommitTrans ();
			}

			listEx = list;
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}
		}

		[Test ()]
		public void TestCase_SqlString_Query ()
		{
			List<TeUser> list = InitialUserTable (10);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "select * from Te_User";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> ();
			listAc.AddRange (executor.Query<TeUser> ());
			listEx = list;
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> (executor.QueryList<TeUser> (5, 3));
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User where id>5 and id<=8";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> (executor.QueryList<TeUser> ());
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User where id>@P1 and id<=@P2";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			executor = context.CreateSqlStringExecutor (sql, ps);
			listAc = new List<TeUser> (executor.QueryList<TeUser> ());
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "select * from Te_User";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				listAc = new List<TeUser> (executor.QueryList<TeUser> ());
				trans.CommitTrans ();
			}

			listEx = list;
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}
		}

		[Test ()]
		public void TestCase_SqlString_Execute ()
		{
			InitialUserTable (10);
			string sql;
			TeUser user;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "update Te_User set Account='abc' where id=1";
			executor = context.CreateSqlStringExecutor (sql);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (1);
			Assert.NotNull (user);
			Assert.AreEqual ("abc", user.Account);

			sql = "update Te_User set Account=@P2 where id=@P1";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 2);
			ps [1] = new DataParameter ("P2", "bcd");
			executor = context.CreateSqlStringExecutor (sql, ps);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (2);
			Assert.NotNull (user);
			Assert.AreEqual ("bcd", user.Account);

			sql = "update Te_User set Account='abc' where id=3";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				executor.ExecuteNonQuery ();
				trans.CommitTrans ();
			}
			user = context.SelectSingleFromId<TeUser> (3);
			Assert.NotNull (user);
			Assert.AreEqual ("abc", user.Account);
		}

		[Test ()]
		public void TestCase_SqlString_ExecuteScalar ()
		{
			InitialUserTable (10);
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;
			int ac;

			sql = "select count(1) from Te_User";
			executor = context.CreateSqlStringExecutor (sql);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (10, ac);

			sql = "select count(1) from Te_User where id<=@P1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("P1", 5);
			executor = context.CreateSqlStringExecutor (sql, ps);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (5, ac);

			sql = "select count(1) from Te_User";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				ac = Convert.ToInt32 (executor.ExecuteScalar ());
				trans.CommitTrans ();
			}
			Assert.AreEqual (10, ac);
		}

		[Test ()]
		public void TestCase_StoreProcedure_QueryList ()
		{
			List<TeUser> list = InitialUserTable (10);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "sptest1";
			executor = context.CreateStoreProcedureExecutor (sql);
			listAc = executor.QueryList<TeUser> ();
			listEx = list;
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}

			sql = "sptest1";
			executor = context.CreateStoreProcedureExecutor (sql);
			listAc = executor.QueryList<TeUser> (5, 3);
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}


			sql = "sptest2";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			executor = context.CreateSqlStringExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.True (EqualUser (listEx [i], listAc [i], true));
			}
//
//			sql = "select * from Te_User";
//			using (TransDataContext trans = context.CreateTransDataContext ()) {
//				trans.BeginTrans ();
//				executor = trans.CreateSqlStringExecutor (sql);
//				listAc = executor.QueryList<TeUser> ();
//				trans.CommitTrans ();
//			}
//
//			listEx = list;
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			for (int i = 0; i < listEx.Count; i++) {
//				Assert.True (EqualUser (listEx [i], listAc [i], true));
//			}
		}

	}
}

