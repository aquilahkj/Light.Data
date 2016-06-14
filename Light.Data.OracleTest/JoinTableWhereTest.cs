using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class JoinTableWhereTest:BaseTest
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
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10 & TeUserLevel.StatusField < 5)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 && listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10)
				.WhereWithAnd (TeUserLevel.StatusField < 5)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 && x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 || listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10 | TeUserLevel.StatusField < 5)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 || x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return x.Id > 10 || listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10)
				.WhereWithOr (TeUserLevel.StatusField < 5)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Id > 10 || x.LevelStatus < 5));

			listLevelSub = listLevel.FindAll (x => x.Status < 5);
			listEx = list.FindAll (x => {
				return listLevelSub.Exists (y => y.Id == x.LevelId);
			});
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10)
				.Where (TeUserLevel.StatusField < 5)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.LevelStatus < 5));


			listEx = list;
			listAc = context.LQuery<TeUser> ().LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 10)
				.WhereReset ()
				.ToList<TeUserAndLevelModel> ();
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
			listAc = context.LQuery<TeUser> ()
				.Where (TeUser.IdField > 10)
				.LeftJoin<TeUserLevel> (TeUserLevel.StatusField > 2 & TeUserLevel.StatusField < 5)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return  x.Id > 10 && listLevelSub.Exists (y => {
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
			listAc = context.LQuery<TeUser> ()
				.Where (TeUser.IdField > 10)
				.LeftJoin<TeUserLevel> (TeUserLevel.StatusField > 2 & TeUserLevel.StatusField < 5)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.Where (TeUser.IdField > 15)
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return  x.Id > 15 && listLevelSub.Exists (y => {
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
			LEnumerable<TeUser> userTable = context.LQuery<TeUser> ().Where (TeUser.IdField > 10 & TeUser.IdField < 18);
			LEnumerable<TeUserLevel> levelTable = context.LQuery<TeUserLevel> ().Where (TeUserLevel.StatusField > 2 & TeUserLevel.StatusField < 5);
			listAc = userTable
				.LeftJoin<TeUserLevel> (levelTable)
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return  x.Id > 10 && x.Id < 18 && listLevelSub.Exists (y => {
					if (x.LevelId == y.Id)
						return x.Status > 2 && x.LevelStatus < 5;
					else
						return true;
				});
			}));


		}
	}
}

