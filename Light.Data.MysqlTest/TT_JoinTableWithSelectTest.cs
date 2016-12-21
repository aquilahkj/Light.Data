using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableWithSelectTest : BaseTest
	{
		[Test ()]
		public void TestCase_Join_With_Select1 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			var selectList = context.Query<TeUserLevel> ().Select (x => new {
				x.Id,
				x.LevelName,
				x.Remark,
				LevelStatus = x.Status
			});

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.Query<TeUser> ().LeftJoin (selectList, (x, y) => x.LevelId == y.Id)
							.Where ((x, y) => x.Id > 10 && y.LevelStatus < 5)
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
								LevelStatus = y.LevelStatus
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

		}


		[Test ()]
		public void TestCase_Join_With_Select2 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			var selectList = context.Query<TeUser> ().Select (x => new {
				x.Id,
				x.Account,
				x.Password,
				x.NickName,
				x.Gender,
				x.Birthday,
				x.Telephone,
				x.Email,
				x.Address,
				x.LevelId,
				x.RegTime,
				x.LastLoginTime,
				x.Status,
				x.HotRate,
				x.Area,
				x.DeleteFlag,
				x.RefereeId,
			});

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = selectList.LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
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
								LevelName = y.LevelName,
								Remark = y.Remark,
								LevelStatus = y.Status
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

		}
	}
}

