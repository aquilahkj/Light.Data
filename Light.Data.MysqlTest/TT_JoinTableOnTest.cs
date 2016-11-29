using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableOnTest : BaseTest
	{
		[Test ()]
		public void TestCase_Join_AndOr ()
		{
			List<TeUser> list = InitialUserTable (57);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel;
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => x.LevelId == y.Id && x.LoginTimes == y.Status);
			}
			);

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId == y.Id && x.LoginTimes == y.Status)
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
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LoginTimes == y.Status && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId == y.Id || x.LoginTimes == y.Status) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId == y.Id || x.LoginTimes == y.Status)
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
				return listLevelSub.Exists (y => {
					return (x.LevelId == y.Id || x.LoginTimes == y.Status) && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));
		}

		[Test ()]
		public void TestCase_Join_SingleParam ()
		{
			List<TeUser> list = InitialUserTable (12);
			List<TeUserLevel> listLevel = InitialUserLevelTable (10);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId == y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId == y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId != y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId != y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId != y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId > y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId > y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId > y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId >= y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId >= y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId >= y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId < y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId < y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId < y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId <= y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId <= y.Id)
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
				return listLevelSub.Exists (y => {
					return x.LevelId <= y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));
		}
	}
}

