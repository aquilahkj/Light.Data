using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class JoinTableOrderByTest:BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Base ()
		{
			List<TeUser> list = InitialUserTable (21);
			InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;

			listEx = new List<TeUser> (list);
			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderBy (TeUser.IdField.OrderByDesc ())
				.ToList<TeUserAndLevelModel> ();
			listEx.Reverse ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 1; i < listAc.Count; i++) {
				Assert.Greater (listAc [i - 1].Id, listAc [i].Id);
			}

			listEx = new List<TeUser> (list);
			listAc = context.LQuery<TeUser> ()
				.OrderBy (TeUser.IdField.OrderByDesc ())
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.ToList<TeUserAndLevelModel> ();
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
			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderBy (TeUserLevel.StatusField.OrderByAsc () & TeUser.IdField.OrderByAsc ())
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			for (int i = 1; i < listAc.Count; i++) {
				Assert.LessOrEqual (listAc [i - 1].LevelStatus, listAc [i].LevelStatus);
				if (listAc [i - 1].LevelStatus == listAc [i].LevelStatus) {
					Assert.Less (listAc [i - 1].Id, listAc [i].Id);
				}
			}

			listEx = new List<TeUser> (list);
			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderBy (TeUserLevel.StatusField.OrderByAsc())
				.OrderByCatch (TeUser.IdField.OrderByAsc ())
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			for (int i = 1; i < listAc.Count; i++) {
				Assert.LessOrEqual (listAc [i - 1].LevelStatus, listAc [i].LevelStatus);
				if (listAc [i - 1].LevelStatus == listAc [i].LevelStatus) {
					Assert.Less (listAc [i - 1].Id, listAc [i].Id);
				}
			}

			listEx = new List<TeUser> (list);
			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderBy (TeUserLevel.StatusField.OrderByAsc())
				.OrderByReset()
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);

			for (int i = 1; i < listAc.Count; i++) {
				Assert.Less (listAc [i - 1].Id, listAc [i].Id);
			}
		}


		[Test ()]
		public void TestCase_OrderBy_Random ()
		{
			List<TeUser> list = InitialUserTable (21);
			InitialUserLevelTable (12);

			List<TeUser> listEx;
			List<TeUserAndLevelModel> listAc;



			listEx = new List<TeUser> (list);

			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderByRandom()
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			int[] array1 = new int[listAc.Count];
			for (int i = 0; i < listAc.Count; i++) {
				array1 [i] = listAc [i].Id;
			}



			listAc = context.LQuery<TeUser> ()
				.LeftJoin<TeUserLevel> ()
				.On (TeUser.LevelIdField == TeUserLevel.IdField)
				.SelectAll<TeUser> ()
				.Select (TeUserLevel.LevelNameField, TeUserLevel.RemarkField)
				.SelectAlias (TeUserLevel.StatusField, "LevelStatus")
				.OrderByRandom()
				.ToList<TeUserAndLevelModel> ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			int[] array2 = new int[listAc.Count];
			for (int i = 0; i < listAc.Count; i++) {
				array2 [i] = listAc [i].Id;
			}

			string s1 = string.Join ("-", array1);
			string s2 = string.Join ("-", array2);
			Assert.AreNotEqual (s1, s2);
		}
	}


}

