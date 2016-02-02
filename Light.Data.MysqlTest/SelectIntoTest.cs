using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class SelectIntoTest:BaseTest
	{
		[Test ()]
		public void TestCase_FullInsert ()
		{
			List<TeDataLog> list = InitialDataLogTable (57);
			List<TeDataLog> listEx;
			List<TeDataLogHistory> listAc;

			context.SelectInto<TeDataLogHistory,TeDataLog> ();
			listEx = list;
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}

			context.TruncateTable<TeDataLogHistory> ();
			context.SelectInto<TeDataLogHistory,TeDataLog> (TeDataLog.IdField <= 20);
			listEx = list.FindAll (x => x.Id <= 20);
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}

			context.TruncateTable<TeDataLogHistory> ();
			context.SelectInto<TeDataLogHistory,TeDataLog> (TeDataLog.IdField <= 20, TeDataLog.IdField.OrderByDesc ());
			listEx = list.FindAll (x => x.Id <= 20);
			listEx.Sort ((x, y) => x.Id < y.Id ? 1 : -1);
			listAc = context.LQuery<TeDataLogHistory> ().ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			for (int i = 0; i < listEx.Count; i++) {
				Assert.IsTrue (EqualLog (listEx [i], listAc [i]));
			}
		}


	}
}

