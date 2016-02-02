using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class SelectInsertTest:BaseTest
	{
		[Test ()]
		public void TestCase_FullInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ().Execute ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Where (TeDataLog.IdField <= 20).Insert<TeDataLogHistory> ().Execute ();
			listEx = list.FindAll (x => x.Id <= 20);
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ().Where (TeDataLog.IdField <= 20).Execute ();
			listEx = list.FindAll (x => x.Id <= 20);
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}


			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ().Where (TeDataLog.IdField <= 20).OrderBy (TeDataLog.IdField.OrderByDesc ()).Execute ();
			listEx = list.FindAll (x => x.Id <= 20);
			listEx.Sort ((x, y) => x.Id < y.Id ? 1 : -1);
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}
		}

		[Test ()]
		public void TestCase_FieldInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ()
				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField)
				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField)
				.Execute ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));
		}

		[Test ()]
		public void TestCase_ConstantInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ()
				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, 3, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, TeDataLog.CheckDataField)
				.Execute ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckId == 3));

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ()
				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, TeDataLog.ArticleIdField, TeDataLog.RecordTimeField, TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField, TeDataLog.CheckIdField, TeDataLog.CheckPointField, TeDataLog.CheckTimeField, SelectFieldInfo.Null)
				.Execute ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.CheckData == null));

			context.TruncateTable<TeDataLogHistory> ();
			context.LQuery<TeDataLog> ().Insert<TeDataLogHistory> ()
				.SetInsertField (TeDataLogHistory.IdField, TeDataLogHistory.UserIdField, TeDataLogHistory.ArticleIdField, TeDataLogHistory.RecordTimeField, TeDataLogHistory.StatusField, TeDataLogHistory.ActionField, TeDataLogHistory.RequestUrlField)
				.SetSelectField (TeDataLog.IdField, TeDataLog.UserIdField, 200, DateTime.Now.Date.AddHours (18), TeDataLog.StatusField, TeDataLog.ActionField, TeDataLog.RequestUrlField)
				.Execute ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.ArticleId == 200 && x.RecordTime == DateTime.Now.Date.AddHours (18) && x.CheckId == null && x.CheckPoint == null && x.CheckTime == null && x.CheckData == null));

		}


	}
}

