using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableWhereTest : BaseTest
	{
		[Test ()]
		public void TestCase_JoinWhere_AndOr ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10 && y.Status < 5)
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
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10)
							.WhereWithAnd ((x, y) => y.Status < 5)
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
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 || listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10 || y.Status < 5)
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
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 || x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 || listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10)
							.WhereWithOr ((x, y) => y.Status < 5)
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
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 || x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10)
							.Where ((x, y) => y.Status < 5)
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
			Assert.IsTrue (listAc.TrueForAll (x => x.LevelStatus < 5));

			listEx = list;
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10)
							.WhereReset ()
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
		}

		[Test ()]
		public void TestCase_JoinWhere_Complexe ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel.FindAll (x => x.Status > 2 && x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10;
			});

			listAc = context.Query<TeUser> ().Where (x => x.Id > 10).LeftJoin<TeUserLevel> (y => y.Status > 2 & y.Status < 5, (x, y) => x.LevelId == y.Id)
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
			Assert.IsTrue (listAc.TrueForAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => {
					if (x.LevelId == y.Id)
						return x.Status > 2 && x.LevelStatus < 5;
					else
						return true;
				});
			}));


			listLevelSub = listLevel.FindAll (x => x.Status > 2 && x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 15;
			});

			listAc = context.Query<TeUser> ().Where (x => x.Id > 10).LeftJoin<TeUserLevel> (y => y.Status > 2 & y.Status < 5, (x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 15)
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
			Assert.IsTrue (listAc.TrueForAll (x => {
				return x.Id > 15 && listLevelSub.Exists (y => {
					if (x.LevelId == y.Id)
						return x.Status > 2 && x.LevelStatus < 5;
					else
						return true;
				});
			}));


			listLevelSub = listLevel.FindAll (x => x.Status > 2 && x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && x.Id < 18;
			});
			var userTable = context.Query<TeUser> ().Where (x => x.Id > 10 & x.Id < 18);
			var levelTable = context.Query<TeUserLevel> ().Where (x => x.Status > 2 & x.Status < 5);
			listAc = userTable.LeftJoin (levelTable, (x, y) => x.LevelId == y.Id).Select ((x, y) => new TeUserAndLevelModel () {
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
			Assert.IsTrue (listAc.TrueForAll (x => {
				return x.Id > 10 && x.Id < 18 && listLevelSub.Exists (y => {
					if (x.LevelId == y.Id)
						return x.Status > 2 && x.LevelStatus < 5;
					else
						return true;
				});
			}));


		}
	}
}

