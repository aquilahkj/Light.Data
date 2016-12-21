using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableOrderByTest : BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Base ()
		{
			List<TeUser> list = InitialUserTable (21);
			InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;

			listEx = new List<TeUser> (list);
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id && x.LoginTimes == y.Status)
							.OrderByDescending ((x, y) => x.Id)
							.Select ((x, y) => new TeUserAndLevelModel () {
								Id = x.Id,
								Account = x.Account,
								Password = x.Password,
								NickName = x.NickName,
								Gender = x.Gender,
								Birthday = x.Birthday,
								Telephone = x.Telephone,
								Email = x.Email,
								Address = x.Address,
								LevelId = x.LevelId,
								RegTime = x.RegTime,
								LastLoginTime = x.LastLoginTime,
								Status = x.Status,
								HotRate = x.HotRate,
								Area = x.Area,
								DeleteFlag = x.DeleteFlag,
								RefereeId = x.RefereeId,
								CheckPoint = x.CheckPoint,
								CheckStatus = x.CheckStatus,
								CheckLevelType = x.CheckLevelType,
								LoginTimes = x.LoginTimes,
								Mark = x.Mark,
								LevelName = y.LevelName,
								Remark = y.Remark,
								LevelStatus = y.Status
							}).ToList ();
			listEx.Reverse ();

			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 1; i < listAc.Count; i++) {
				Assert.Greater (listAc [i - 1].Id, listAc [i].Id);
			}

			listEx = new List<TeUser> (list);
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id && x.LoginTimes == y.Status)
							.OrderBy ((x, y) => x.Status)
							.OrderByDescending ((x, y) => x.Id)
							.Select ((x, y) => new TeUserAndLevelModel () {
								Id = x.Id,
								Account = x.Account,
								Password = x.Password,
								NickName = x.NickName,
								Gender = x.Gender,
								Birthday = x.Birthday,
								Telephone = x.Telephone,
								Email = x.Email,
								Address = x.Address,
								LevelId = x.LevelId,
								RegTime = x.RegTime,
								LastLoginTime = x.LastLoginTime,
								Status = x.Status,
								HotRate = x.HotRate,
								Area = x.Area,
								DeleteFlag = x.DeleteFlag,
								RefereeId = x.RefereeId,
								CheckPoint = x.CheckPoint,
								CheckStatus = x.CheckStatus,
								CheckLevelType = x.CheckLevelType,
								LoginTimes = x.LoginTimes,
								Mark = x.Mark,
								LevelName = y.LevelName,
								Remark = y.Remark,
								LevelStatus = y.Status
							}).ToList ();
			listEx.Reverse ();

			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 1; i < listAc.Count; i++) {
				Assert.Greater (listAc [i - 1].Id, listAc [i].Id);
			}

		}

		[Test ()]
		public void TestCase_OrderBy_Catch ()
		{
			List<TeUser> list = InitialUserTable (21);
			InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;

			listEx = new List<TeUser> (list);
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id && x.LoginTimes == y.Status)
							.OrderBy ((x, y) => y.Status)
							.OrderByCatch ((x, y) => x.Id)
							.Select ((x, y) => new TeUserAndLevelModel () {
								Id = x.Id,
								Account = x.Account,
								Password = x.Password,
								NickName = x.NickName,
								Gender = x.Gender,
								Birthday = x.Birthday,
								Telephone = x.Telephone,
								Email = x.Email,
								Address = x.Address,
								LevelId = x.LevelId,
								RegTime = x.RegTime,
								LastLoginTime = x.LastLoginTime,
								Status = x.Status,
								HotRate = x.HotRate,
								Area = x.Area,
								DeleteFlag = x.DeleteFlag,
								RefereeId = x.RefereeId,
								CheckPoint = x.CheckPoint,
								CheckStatus = x.CheckStatus,
								CheckLevelType = x.CheckLevelType,
								LoginTimes = x.LoginTimes,
								Mark = x.Mark,
								LevelName = y.LevelName,
								Remark = y.Remark,
								LevelStatus = y.Status
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			for (int i = 1; i < listAc.Count; i++) {
				Assert.LessOrEqual (listAc [i - 1].LevelStatus, listAc [i].LevelStatus);
				if (listAc [i - 1].LevelStatus == listAc [i].LevelStatus) {
					Assert.Less (listAc [i - 1].Id, listAc [i].Id);
				}
			}

			listEx = new List<TeUser> (list);
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id && x.LoginTimes == y.Status)
								.OrderBy ((x, y) => y.Status)
								.OrderByReset ()
								.Select ((x, y) => new TeUserAndLevelModel () {
									Id = x.Id,
									Account = x.Account,
									Password = x.Password,
									NickName = x.NickName,
									Gender = x.Gender,
									Birthday = x.Birthday,
									Telephone = x.Telephone,
									Email = x.Email,
									Address = x.Address,
									LevelId = x.LevelId,
									RegTime = x.RegTime,
									LastLoginTime = x.LastLoginTime,
									Status = x.Status,
									HotRate = x.HotRate,
									Area = x.Area,
									DeleteFlag = x.DeleteFlag,
									RefereeId = x.RefereeId,
									CheckPoint = x.CheckPoint,
									CheckStatus = x.CheckStatus,
									CheckLevelType = x.CheckLevelType,
									LoginTimes = x.LoginTimes,
									Mark = x.Mark,
									LevelName = y.LevelName,
									Remark = y.Remark,
									LevelStatus = y.Status
								}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			for (int i = 1; i < listAc.Count; i++) {
				Assert.Less (listAc [i - 1].Id, listAc [i].Id);
			}
		}
	}
}

