using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MssqlTest
{
	[TestFixture ()]
	public class JoinTableOnTest:BaseTest
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
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField & TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LoginTimes == y.Status && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => x.LevelId == y.Id && x.LoginTimes == y.Status);
			}
			);
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> (TeUser.LevelIdField == TeUserLevel.IdField & TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LoginTimes == y.Status && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => x.LevelId == y.Id && x.LoginTimes == y.Status);
			}
			);
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.OnWithAnd (TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField | TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return (x.LevelId == y.Id || x.LoginTimes == y.Status) && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> (TeUser.LevelIdField == TeUserLevel.IdField | TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return (x.LevelId == y.Id || x.LoginTimes == y.Status) && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.OnWithOr (TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return (x.LevelId == y.Id || x.LoginTimes == y.Status) && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => x.LevelId == y.Id && x.LoginTimes == y.Status);
			}
			);
			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField | TeUser.LoginTimesField == TeUserLevel.StatusField)
				.On (TeUser.LevelIdField == TeUserLevel.IdField & TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId == y.Id && x.LoginTimes == y.Status && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));

			listLevelSub = listLevel;
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => x.LevelId == y.Id && x.LoginTimes == y.Status);
			}
			);

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.OnReset()
				.On (TeUser.LevelIdField == TeUserLevel.IdField & TeUser.LoginTimesField == TeUserLevel.StatusField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
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
			listEx = new List<TeUser> ();
			foreach (TeUser x in list) {
				foreach (TeUserLevel y in listLevelSub) {
					if (x.LevelId != y.Id) {
						listEx.Add (x);
					}
				}
			}

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField != TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField > TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField >= TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField < TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
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

			listAc = context.LQuery<TeUser> ().Join<TeUserLevel> ()
				.On (TeUser.LevelIdField <= TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return	listLevelSub.Exists (y => {
					return x.LevelId <= y.Id && x.LevelStatus == y.Status && x.LevelName == y.LevelName && x.Remark == y.Remark;
				});
			}
			));
		}
	}
}

