using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.PostgreTest
{
	[TestFixture ()]
	public class LQueryPageTest : BaseTest
	{
		[Test ()]
		public void LQuery_PageSize_Multi ()
		{
			const int tol = 21;
			const int cnt = 8;
			List<TeUser> list = InitialUserTable (tol);

			List<TeUser> listReslt = null;

			listReslt = context.LQuery<TeUser> ().PageSize (1, cnt).RangeReset ().ToList ();
			Assert.AreEqual (tol, listReslt.Count);
			AssertExtend.AreObjectEqual (list, listReslt);
		}

		[Test ()]
		public void LQuery_PageSizeTest ()
		{
			const int tol = 21;
			const int cnt = 8;
			List<TeUser> list = InitialUserTable (tol);

			List<TeUser> listReslt = null;
			List<TeUser> listEx = null;

			int last = tol % cnt;
			int times = tol / cnt;
			times++;

			for (int i = 0; i < times; i++) {
				listReslt = context.LQuery<TeUser> ().PageSize (i + 1, cnt).ToList ();
				int c = (i == times - 1) ? last : cnt;

				listEx = list.GetRange (i * cnt, c);
				Assert.AreEqual (c, listReslt.Count);
				AssertExtend.AreObjectEqual (listEx, listReslt);
			}

			listReslt = context.LQuery<TeUser> ().PageSize (times * cnt, cnt).ToList ();
			Assert.AreEqual (0, listReslt.Count);

			listReslt = context.LQuery<TeUser> ().Where (TeUser.IdField > cnt).PageSize (1, cnt).ToList ();
			Assert.AreEqual (cnt, listReslt.Count);
			listEx = list.GetRange (cnt, cnt);
			AssertExtend.AreObjectEqual (listEx, listReslt);


			listReslt = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByDesc ()).PageSize (1, cnt).ToList ();
			Assert.AreEqual (cnt, listReslt.Count);
			list.Reverse ();
			listEx = list.GetRange (0, cnt);
			AssertExtend.AreObjectEqual (listEx, listReslt);

		}

		[Test ()]
		public void LQuery_PageTakeSkipTest ()
		{
			const int tol = 21;
			const int cnt = 8;
			List<TeUser> list = InitialUserTable (tol);

			List<TeUser> listReslt = null;
			List<TeUser> listEx = null;

			int last = tol % cnt;
			int times = tol / cnt;
			times++;

			for (int i = 0; i < times; i++) {
				listReslt = context.LQuery<TeUser> ().Skip (i * cnt).Take (cnt).ToList ();
				int c = (i == times - 1) ? last : cnt;

				listEx = list.GetRange (i * cnt, c);
				Assert.AreEqual (c, listReslt.Count);
				AssertExtend.AreObjectEqual (listEx, listReslt);
			}

			listReslt = context.LQuery<TeUser> ().Skip (cnt).ToList ();
			listEx = list.GetRange (cnt, tol - cnt);
			Assert.AreEqual (tol-cnt, listReslt.Count);
			AssertExtend.AreObjectEqual (listEx, listReslt);

			listReslt = context.LQuery<TeUser> ().Skip (times * cnt).Take (cnt).ToList ();
			Assert.AreEqual (0, listReslt.Count);

			listReslt = context.LQuery<TeUser> ().Where (TeUser.IdField > cnt).Take (cnt).ToList ();
			Assert.AreEqual (cnt, listReslt.Count);
			listEx = list.GetRange (cnt, cnt);
			AssertExtend.AreObjectEqual (listEx, listReslt);

			listReslt = context.LQuery<TeUser> ().OrderBy (TeUser.IdField.OrderByDesc ()).Take (cnt).ToList ();
			Assert.AreEqual (cnt, listReslt.Count);
			list.Reverse ();
			listEx = list.GetRange (0, cnt);
			AssertExtend.AreObjectEqual (listEx, listReslt);

		}
	}
}

