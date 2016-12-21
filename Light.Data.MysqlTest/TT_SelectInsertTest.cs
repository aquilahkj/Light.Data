using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_SelectInsertTest : BaseTest
	{
		[Test ()]
		public void TestCase_FullInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLog> ().Insert<TeDataLogHistory> ();
			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			AssertExtend.AreObjectEqual (listEx, listAc);

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLog> ().Where (x => x.Id <= 20).Insert<TeDataLogHistory> ();
			listEx = list.FindAll (x => x.Id <= 20);
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			AssertExtend.AreObjectEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_FieldInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();

			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl
			});
			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));
		}

		[Test ()]
		public void TestCase_CommonInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = x.CheckId,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = x.CheckData,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			AssertExtend.AreObjectEqual (listEx, listAc);


		}

		[Test ()]
		public void TestCase_ConstantInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = 3,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = x.CheckData,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == 3));

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = x.CheckId,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = null,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});
			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckData == null));

			context.TruncateTable<TeDataLogHistory> ();

			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = 200,
				RecordTime = DateTime.Now.Date.AddHours (18),
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.ArticleId == 200 && x.RecordTime == DateTime.Now.Date.AddHours (18) && x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));

			context.TruncateTable<TeDataLogHistory> ();

			context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = 200,
				RecordTime = DateTime.Now.Date.AddHours (18),
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckLevelTypeInt = CheckLevelType.High,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.ArticleId == 200 && x.RecordTime == DateTime.Now.Date.AddHours (18) && x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null && x.CheckLevelTypeInt == CheckLevelType.High));

		}

		[Test ()]
		public void TestCase_IdentityInsert ()
		{
			InitialDataLogTable (3);
			List<TeDataLog> listEx;
			List<TeDataLogHistory2> listAc;
			List<TeDataLog> listTemp;

			context.TruncateTable<TeDataLogHistory2> ();
			context.Query<TeDataLog> ().Insert<TeDataLogHistory2> ();
			context.Query<TeDataLog> ().Insert<TeDataLogHistory2> ();
			listTemp = context.Query<TeDataLog> ().ToList ();
			listEx = new List<TeDataLog> ();
			listEx.AddRange (listTemp);
			listTemp = context.Query<TeDataLog> ().ToList ();
			listTemp.ForEach (x => {
				x.Id += listTemp.Count;
			});
			listEx.AddRange (listTemp);
			listAc = context.Query<TeDataLogHistory2> ().ToList ();
			AssertExtend.AreObjectEqual (listEx, listAc);
		}


		[Test ()]
		public void TestCase_RelateInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;
			List<TeUser> listUser;

			context.TruncateTable<TeDataLogHistory> ();
			context.Query<TeDataLogWithUser> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = x.CheckId,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = x.User.Account,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			listUser = context.Query<TeUser> ().ToList ();
			foreach (TeDataLog log in listEx) {
				var user = listUser.Find (x => x.Id == log.UserId);
				if (user != null) {
					log.CheckData = user.Account;
				}
			}
			AssertExtend.AreObjectEqual (listEx, listAc);
		}


		[Test ()]
		public void TestCase_RelateJoin ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;
			List<TeUser> listUser;

			context.TruncateTable<TeDataLog> ();
			context.Query<TeDataLog> ().LeftJoin<TeUser> ((x, y) => x.UserId == y.Id).SelectInsert ((x, y) => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = x.CheckId,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = y.Account,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			listUser = context.Query<TeUser> ().ToList ();
			foreach (TeDataLog log in listEx) {
				var user = listUser.Find (x => x.Id == log.UserId);
				if (user != null) {
					log.CheckData = user.Account;
				}
			}
			AssertExtend.AreObjectEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_RelateJoin2 ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;
			List<TeUser> listUser;

			var ter = context.Query<TeUser> ()
							 .SetDistinct (true)
							 .Select (x => new {
								 x.Id,
								 Accounts = x.Account,
								 x.LevelId
							 });

			context.TruncateTable<TeDataLog> ();
			context.Query<TeDataLog> ().LeftJoin (ter, (x, y) => x.UserId == y.Id).SelectInsert ((x, y) => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = Math.Abs (x.Action + y.LevelId),
				RequestUrl = x.RequestUrl,
				CheckId = x.CheckId,
				CheckPoint = x.CheckPoint,
				CheckTime = x.CheckTime,
				CheckData = y.Accounts,
				CheckLevelTypeInt = x.CheckLevelTypeInt,
			});

			listEx = list;
			listAc = context.Query<TeDataLogHistory> ().ToList ();
			listUser = context.Query<TeUser> ().ToList ();
			foreach (TeDataLog log in listEx) {
				var user = listUser.Find (x => x.Id == log.UserId);
				if (user != null) {
					log.CheckData = user.Account;
				}
			}
			AssertExtend.AreObjectEqual (listEx, listAc);
		}
	}
}

