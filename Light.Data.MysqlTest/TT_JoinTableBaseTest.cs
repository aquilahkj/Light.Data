using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableBaseTest : BaseTest
	{
		[Test ()]
		public void TestCase_LeftJoin ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id < 15).LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list;
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> (x => x.Id < 10, (x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.LevelId > 0 && x.LevelId < 10) {
					return listLevelSub.Exists (y => {
						return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
					});
				}
				else {
					return x.LevelStatus == 0 && x.LevelName == null && x.Remark == null;
				}
			}
			));
		}

		[Test ()]
		public void TestCase_InnerJoin ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.Query<TeUser> ().Join<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.Query<TeUser> ().Where (x => x.Id < 15).Join<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list.FindAll (x => listLevelSub.Exists (y => y.Id == x.LevelId));
			listAc = context.Query<TeUser> ().Join<TeUserLevel> (x => x.Id < 10, (x, y) => x.LevelId == y.Id)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));


		}

		[Test ()]
		public void TestCase_RightJoin ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;
			List<TeUserLevel> listLevelSub;

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.Query<TeUserLevel> ().RightJoin<TeUser> ((x, y) => x.Id == y.LevelId)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = y.Id,
								LevelId = x.Id,
								LevelStatus = x.Status,
								LevelName = x.LevelName,
								Remark = x.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.Query<TeUserLevel> ().RightJoin<TeUser> (x => x.Id < 15, (x, y) => x.Id == y.LevelId)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = y.Id,
								LevelId = x.Id,
								LevelStatus = x.Status,
								LevelName = x.LevelName,
								Remark = x.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list;
			listAc = context.Query<TeUserLevel> ().Where (x => x.Id < 10).RightJoin<TeUser> ((x, y) => x.Id == y.LevelId)
							.Select ((x, y) => new TeUserAndLevelModel {
								Id = y.Id,
								LevelId = x.Id,
								LevelStatus = x.Status,
								LevelName = x.LevelName,
								Remark = x.Remark
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.LevelId > 0 && x.LevelId < 10) {
					return listLevelSub.Exists (y => {
						return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
					});
				}
				else {
					return x.LevelStatus == 0 && x.LevelName == null && x.Remark == null;
				}
			}
			));


		}

		[Test ()]
		public void TestCase_LeftJoin_Multi ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeUserLevel> listLevel = InitialUserLevelTable (12);
			List<TeAreaInfo> listArea = InitialAreaInfoTable (10);

			List<TeUser> listEx;
			List<TeUserAndLevelAndAreaModel> listAc;
			List<TeUserLevel> listLevelSub;
			List<TeAreaInfo> listAreaSub;

			listLevelSub = listLevel;
			listAreaSub = listArea;
			listEx = list;
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.LeftJoin<TeAreaInfo> ((x, y, z) => x.Area == z.Id)
							.Select ((x, y, z) => new TeUserAndLevelAndAreaModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark,
								V1 = z.V1,
								V2 = z.V2,
								V3 = z.V3,
								AreaName = z.Name
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				})
				&& listAreaSub.Exists (z => {
					if (x.AreaName != null) {
						return x.AreaName == z.Name && x.V1 == z.V1 && x.V2 == z.V2 && x.V3 == z.V3;
					}
					else {
						return true;
					}
				});
			}
			));


			listLevelSub = listLevel;
			listAreaSub = listArea.FindAll (x => x.Id <= 6);
			listEx = list;
			listAc = context.Query<TeUser> ().LeftJoin<TeUserLevel> ((x, y) => x.LevelId == y.Id)
							.LeftJoin<TeAreaInfo> (x => x.Id <= 6, (x, y, z) => x.Area == z.Id)
							.Select ((x, y, z) => new TeUserAndLevelAndAreaModel {
								Id = x.Id,
								LevelId = y.Id,
								LevelStatus = y.Status,
								LevelName = y.LevelName,
								Remark = y.Remark,
								V1 = z.V1,
								V2 = z.V2,
								V3 = z.V3,
								AreaName = z.Name
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				})
				&& listAreaSub.Exists (z => {
					if (x.AreaName != null) {
						return x.AreaName == z.Name && x.V1 == z.V1 && x.V2 == z.V2 && x.V3 == z.V3;
					}
					else {
						return true;
					}
				});
			}
			));

		}

	}
}

