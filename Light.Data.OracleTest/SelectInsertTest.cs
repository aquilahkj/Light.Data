//using System;
//using NUnit.Framework;
//using System.Collections.Generic;
//using Light.Data.UnitTest;
//using System.Threading;

//namespace Light.Data.OracleTest
//{
//	[TestFixture ()]
//	public class SelectInsertTest:BaseTest
//	{
//		[Test ()]
//		public void TestCase_FullInsert ()
//		{
//			List<TeDataLog> list = InitialDataLogTable (57);
//			List<TeDataLog> listEx;
//			List<TeDataLogHistory> listAc;

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().Where (TeDataLog.IdField <= 20).Insert<TeDataLogHistory> ();
//			listEx = list.FindAll (x => x.Id <= 20);
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ().Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().Where (TeDataLog.IdField <= 20).CreateInsertor<TeDataLogHistory> ().Execute ();
//			listEx = list.FindAll (x => x.Id <= 20);
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ().Where (TeDataLog.IdField <= 20).Execute ();
//			listEx = list.FindAll (x => x.Id <= 20);
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ().Where (TeDataLog.IdField <= 20).OrderBy (TeDataLog.IdField.OrderByDesc ()).Execute ();
//			listEx = list.FindAll (x => x.Id <= 20);
//			listEx.Sort ((x, y) => x.Id < y.Id ? 1 : -1);
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			//context.TruncateTable<TeDataLogHistory> ();
//			//context.LQuery<TeDataLog> ().Where (TeDataLog.IdField <= 20).CreateInsertor<TeDataLogHistory> ().Execute ();
//			//Thread.Sleep (5000);
//			//context.LQuery<TeDataLog> ().Where (TeDataLog.IdField > 20).CreateInsertor<TeDataLogHistory> ().Execute ();

//			//listEx = list;
//			//listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			//AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);
//		}

//		[Test ()]
//		public void TestCase_FieldInsert ()
//		{
//			List<TeDataLog> list = InitialDataLogTable (57);
//			List<TeDataLog> listEx;
//			List<TeDataLogHistory> listAc;

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField)
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));
//		}

//		[Test ()]
//		public void TestCase_CommonInsert ()
//		{
//			List<TeDataLog> list = InitialDataLogTable (57);
//			List<TeDataLog> listEx;
//			List<TeDataLogHistory> listAc;

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField, TeDataLogHistory.CheckIdField, TeDataLogHistory.CheckPointField, TeDataLogHistory.CheckTimeField, TeDataLogHistory.CheckDataField, TeDataLogHistory.CheckLevelTypeIntField, TeDataLogHistory.CheckLevelTypeStringField)
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, TeDataLog.CheckIdField, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, TeDataLog.CheckDataField, TeDataLog.CheckLevelTypeIntField, TeDataLog.CheckLevelTypeStringField)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, TeDataLog.CheckIdField, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, TeDataLog.CheckDataField, TeDataLog.CheckLevelTypeIntField, TeDataLog.CheckLevelTypeStringField);
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);
//		}

//		[Test ()]
//		public void TestCase_ConstantInsert ()
//		{
//			List<TeDataLog> list = InitialDataLogTable (57);
//			List<TeDataLog> listEx;
//			List<TeDataLogHistory> listAc;

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, 3, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, TeDataLog.CheckDataField, TeDataLog.CheckLevelTypeIntField, TeDataLog.CheckLevelTypeStringField);
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == 3));

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, 3, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, TeDataLog.CheckDataField, TeDataLog.CheckLevelTypeIntField, TeDataLog.CheckLevelTypeStringField)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == 3));

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, TeDataLog.CheckIdField, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, SelectFieldInfo.Null, TeDataLog.CheckLevelTypeIntField, TeDataLog.CheckLevelTypeStringField)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.CheckData == null));

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField)
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, 200, DateTime.Now.Date.AddHours (18), TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.ArticleId == 200 && x.RecordTime == DateTime.Now.Date.AddHours (18) && x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ()
//				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField, TeDataLogHistory.CheckLevelTypeIntField, TeDataLogHistory.CheckLevelTypeStringField)
//				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, 200, DateTime.Now.Date.AddHours (18), TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, CheckLevelType.High, CheckLevelType.Low)
//				.Execute ();
//			listEx = list;
//			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
//			Assert.AreEqual (listEx.Count, listAc.Count);
//			Assert.IsTrue (listAc.TrueForAll (x => x.ArticleId == 200 && x.RecordTime == DateTime.Now.Date.AddHours (18) && x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null && x.CheckLevelTypeInt == CheckLevelType.High && x.CheckLevelTypeString == CheckLevelType.Low));

//		}

//		/*
//		[Test ()]
//		public void TestCase_IdentityInsert ()
//		{
//			InitialDataLogTable (3);
//			List<TeDataLog> listEx;
//			List<TeDataLogHistory2> listAc;
//			List<TeDataLogHistory> listAce;
//			List<TeDataLog> listTemp;

//			context.TruncateTable<TeDataLogHistory2> ();
//			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory2> ();
//			Thread.Sleep (5000);
//			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory2> ();
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listEx = new List<TeDataLog> ();
//			listEx.AddRange (listTemp);
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listTemp.ForEach (x => {
//				x.Id += listTemp.Count;
//			});
//			listEx.AddRange (listTemp);
//			listAc = context.LQuery<TeDataLogHistory2> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory2> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory2> ().Execute ();
//			Thread.Sleep (5000);
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory2> ().Execute ();
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listEx = new List<TeDataLog> ();
//			listEx.AddRange (listTemp);
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listTemp.ForEach (x => {
//				x.Id += listTemp.Count;
//			});
//			listEx.AddRange (listTemp);
//			listAc = context.LQuery<TeDataLogHistory2> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAc);

//			context.TruncateTable<TeDataLogHistory> ();
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ().Execute ();
//			Thread.Sleep (5000);
//			context.LQuery<TeDataLog> ().CreateInsertor<TeDataLogHistory> ().Execute ();
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listEx = new List<TeDataLog> ();
//			listEx.AddRange (listTemp);
//			listTemp = context.LQuery<TeDataLog> ().ToList ();
//			listEx.AddRange (listTemp);
//			listAce = context.LQuery<TeDataLogHistory> ().ToList ();
//			AssertExtend.AreEnumerableTypeEqual<ITeDataLog> (listEx, listAce);
//		}
//		*/
//	}
//}

