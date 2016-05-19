using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class LQueryPageTest:BaseTest
	{
		[Test ()]
		public void LQuery_PageSize_Multi ()
		{
			List<TeUser> list = InitialUserTable (21);

			List<TeUser> listReslt = null;

			listReslt = context.LQuery<TeUser> ().PageSize (1, 8).RangeReset ().ToList ();
			Assert.AreEqual (21, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i]));
			}

		}

		[Test ()]
		public void LQuery_PageSizeTest ()
		{
			List<TeUser> list = InitialUserTable (21);

			List<TeUser> listReslt = null;

			listReslt = context.LQuery<TeUser> ().PageSize (1, 8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i]));
			}

			listReslt = context.LQuery<TeUser> ().PageSize (2, 8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}

			listReslt = context.LQuery<TeUser> ().PageSize (3, 8).ToList ();
			Assert.AreEqual (5, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 16]));
			}

			listReslt = context.LQuery<TeUser> ().PageSize (4, 8).ToList ();
			Assert.AreEqual (0, listReslt.Count);

			listReslt = context.LQuery<TeUser> ().Where (TeUser.IdField > 8).PageSize (1, 8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}

			listReslt = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByDesc ()).PageSize (1, 8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [20 - i]));
			}
		}

		[Test ()]
		public void LQuery_PageTakeSkipTest ()
		{
			List<TeUser> list = InitialUserTable (21);

			List<TeUser> listReslt = null;

			listReslt = context.LQuery<TeUser> ().Take (8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i]));
			}

			listReslt = context.LQuery<TeUser> ().Skip (8).Take (8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}

			listReslt = context.LQuery<TeUser> ().Take (8).Skip (8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}

			listReslt = context.LQuery<TeUser> ().Skip (16).Take (8).ToList ();
			Assert.AreEqual (5, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 16]));
			}

			listReslt = context.LQuery<TeUser> ().Skip (24).Take (8).ToList ();
			Assert.AreEqual (0, listReslt.Count);

			listReslt = context.LQuery<TeUser> ().Where (TeUser.IdField > 8).Take (8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}

			listReslt = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByDesc ()).Take (8).ToList ();
			Assert.AreEqual (8, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [20 - i]));
			}

			listReslt = context.LQuery<TeUser> ().Skip (8).ToList ();
			Assert.AreEqual (13, listReslt.Count);
			for (int i = 0; i < listReslt.Count; i++) {
				Assert.IsTrue (EqualUser (listReslt [i], list [i + 8]));
			}
		}
	}
}

