using System;
using System.Data;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.OracleTest
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

			sql = "select * from \"Te_User\"";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> ();
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\"";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> (5, 3);
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			Assert.AreEqual (listEx.Count, listAc.Count);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\" where \"Id\">5 and \"Id\"<=8";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\" where \"Id\">:P1 and \"Id\"<=:P2";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			executor = context.CreateSqlStringExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\"";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				listAc = executor.QueryList<TeUser> ();
				trans.CommitTrans ();
			}

			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);
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

			sql = "select * from \"Te_User\"";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> ();
			listAc.AddRange (executor.Query<TeUser> ());
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\"";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> (executor.QueryList<TeUser> (5, 3));
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\" where \"Id\">5 and \"Id\"<=8";
			executor = context.CreateSqlStringExecutor (sql);
			listAc = new List<TeUser> (executor.QueryList<TeUser> ());
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\" where \"Id\">:P1 and \"Id\"<=:P2";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			executor = context.CreateSqlStringExecutor (sql, ps);
			listAc = new List<TeUser> (executor.QueryList<TeUser> ());
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "select * from \"Te_User\"";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				listAc = new List<TeUser> (executor.QueryList<TeUser> ());
				trans.CommitTrans ();
			}

			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_SqlString_Execute ()
		{
			InitialUserTable (10);
			string sql;
			TeUser user;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "update \"Te_User\" set \"Account\"='abc' where \"Id\"=1";
			executor = context.CreateSqlStringExecutor (sql);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (1);
			Assert.NotNull (user);
			Assert.AreEqual ("abc", user.Account);

			sql = "update \"Te_User\" set \"Account\"=:P2 where \"Id\"=:P1";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 2);
			ps [1] = new DataParameter ("P2", "bcd");
			executor = context.CreateSqlStringExecutor (sql, ps);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (2);
			Assert.NotNull (user);
			Assert.AreEqual ("bcd", user.Account);

			sql = "update \"Te_User\" set \"Account\"=:P2 where \"Id\"=:P1";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 3);
			ps [1] = new DataParameter ("P2", "abc");
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql, ps);
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

			sql = "select count(1) from \"Te_User\"";
			executor = context.CreateSqlStringExecutor (sql);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (10, ac);

			sql = "select count(1) from \"Te_User\" where \"Id\"<=:P1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("P1", 5);
			executor = context.CreateSqlStringExecutor (sql, ps);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (5, ac);

			sql = "select count(1) from \"Te_User\"";
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateSqlStringExecutor (sql);
				ac = Convert.ToInt32 (executor.ExecuteScalar ());
				trans.CommitTrans ();
			}
			Assert.AreEqual (10, ac);
		}

		[Test ()]
		public void TestCase_StoreProcedure_Execute_OutParameter ()
		{
			InitialUserTable (10);
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "TestPackage.sptest7";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 0, ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			executor.ExecuteNonQuery ();
			Assert.AreEqual (5, Convert.ToInt32 (ps [1].OutputValue));

			sql = "TestPackage.sptest7";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 0, ParameterDirection.Output);
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = context.CreateStoreProcedureExecutor (sql, ps);
				executor.ExecuteNonQuery ();
				trans.CommitTrans ();
			}
			Assert.AreEqual (5, Convert.ToInt32 (ps [1].OutputValue));
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

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> ();
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> (5, 3);
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest2";
			ps = new DataParameter[3];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			ps [2] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = executor.QueryList<TeUser> ();
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateStoreProcedureExecutor (sql, ps);
				listAc = executor.QueryList<TeUser> ();
				trans.CommitTrans ();
			}
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_StoreProcedure_Query ()
		{
			List<TeUser> list = InitialUserTable (10);
			List<TeUser> listEx;
			List<TeUser> listAc;
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = new List<TeUser> ();
			listAc.AddRange (executor.Query<TeUser> ());
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = new List<TeUser> (executor.QueryList<TeUser> (5, 3));
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest2";
			ps = new DataParameter[3];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("P2", 8);
			ps [2] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			listAc = new List<TeUser> (executor.QueryList<TeUser> ());
			listEx = list.FindAll (x => x.Id > 5 && x.Id <= 8);
			AssertExtend.AreEnumerableEqual (listEx, listAc);

			sql = "TestPackage.sptest1";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret_cursor", null, "RefCursor", ParameterDirection.Output);
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateStoreProcedureExecutor (sql, ps);
				listAc = new List<TeUser> (executor.QueryList<TeUser> ());
				trans.CommitTrans ();
			}
			listEx = list;
			AssertExtend.AreEnumerableEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_StoreProcedure_Execute ()
		{
			InitialUserTable (10);
			string sql;
			TeUser user;
			SqlExecutor executor;
			DataParameter[] ps;

			sql = "TestPackage.sptest3";
			executor = context.CreateStoreProcedureExecutor (sql);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (1);
			Assert.NotNull (user);
			Assert.AreEqual ("abc", user.Account);

			sql = "TestPackage.sptest4";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 2);
			ps [1] = new DataParameter ("P2", "bcd");
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			executor.ExecuteNonQuery ();
			user = context.SelectSingleFromId<TeUser> (2);
			Assert.NotNull (user);
			Assert.AreEqual ("bcd", user.Account);

			sql = "TestPackage.sptest4";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 3);
			ps [1] = new DataParameter ("P2", "abc");
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateStoreProcedureExecutor (sql, ps);
				executor.ExecuteNonQuery ();
				trans.CommitTrans ();
			}
			user = context.SelectSingleFromId<TeUser> (3);
			Assert.NotNull (user);
			Assert.AreEqual ("abc", user.Account);
		}

		/*
		[Test ()]
		public void TestCase_StoreProcedure_ExecuteScalar ()
		{
			InitialUserTable (10);
			string sql;
			SqlExecutor executor;
			DataParameter[] ps;
			int ac;

			sql = "TestPackage.sptest5";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret", null, "int", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (10, ac);

			sql = "TestPackage.sptest6";
			ps = new DataParameter[2];
			ps [0] = new DataParameter ("P1", 5);
			ps [1] = new DataParameter ("ret", null, "int", ParameterDirection.Output);
			executor = context.CreateStoreProcedureExecutor (sql, ps);
			ac = Convert.ToInt32 (executor.ExecuteScalar ());
			Assert.AreEqual (5, ac);

			sql = "TestPackage.sptest5";
			ps = new DataParameter[1];
			ps [0] = new DataParameter ("ret", null, "int", ParameterDirection.Output);
			using (TransDataContext trans = context.CreateTransDataContext ()) {
				trans.BeginTrans ();
				executor = trans.CreateStoreProcedureExecutor (sql, ps);
				ac = Convert.ToInt32 (executor.ExecuteScalar ());
				trans.CommitTrans ();
			}
			Assert.AreEqual (10, ac);
		}
		*/
	}
}

