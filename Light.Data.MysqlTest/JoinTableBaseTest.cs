using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class JoinTableBaseTest:BaseTest
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
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField < 15).LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list;
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> (TeUserLevel.IdField < 10)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.LevelId < 10) {
					return listLevelSub.Exists (y => {
						return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
					});
				}
				else {
					return x.LevelStatus == 0 && x.LevelName == string.Empty && x.Remark == null;
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
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.LQuery<TeUser> ().Where (TeUser.IdField < 15).Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list.FindAll (x => listLevelSub.Exists (y => y.Id == x.LevelId));
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> (TeUserLevel.IdField < 10)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
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
			listAc = context.LQuery<TeUserLevel> ().RightJoin<TeUser> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list;
			listAc = context.LQuery<TeUserLevel> ().RightJoin<TeUser> (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => x.Id < 15);
			listAc = context.LQuery<TeUserLevel> ().RightJoin<TeUser> (TeUser.IdField < 15)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listLevelSub.Exists (y => {
					return x.Id < 15 && x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel.FindAll (x => x.Id < 10);
			listEx = list;
			listAc = context.LQuery<TeUserLevel> ().Where (TeUserLevel.IdField < 10).RightJoin<TeUser> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				if (x.LevelId < 10) {
					return listLevelSub.Exists (y => {
						return x.LevelId == y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
					});
				}
				else {
					return x.LevelStatus == 0 && x.LevelName == string.Empty && x.Remark == null;
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
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.LeftJoin<TeAreaInfo> ()
				.On (TeUser.AreaField == TeAreaInfo.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Select (TeAreaInfo.V1Field, TeAreaInfo.V2Field, TeAreaInfo.V3Field)
				.SelectAlias (TeAreaInfo.NameField, "AreaName")
				.ToList<TeUserAndLevelAndAreaModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.LeftJoin<TeAreaInfo> (TeAreaInfo.IdField <= 6)
				.On (TeUser.AreaField == TeAreaInfo.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Select (TeAreaInfo.V1Field, TeAreaInfo.V2Field, TeAreaInfo.V3Field)
				.SelectAlias (TeAreaInfo.NameField, "AreaName")
				.ToList<TeUserAndLevelAndAreaModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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

