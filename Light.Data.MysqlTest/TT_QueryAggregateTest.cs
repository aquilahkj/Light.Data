using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_QueryAggregateTest : BaseTest
	{
		[Test ()]
		public void TestCase_Exists ()
		{
			InitialUserTable (21);
			bool result1 = context.Query<TeUser> ().Exists;
			Assert.IsTrue (result1);

			bool result2 = context.Query<TeUser> ().Where (x => x.Id > 1).Exists;
			Assert.IsTrue (result2);

			bool result3 = context.Query<TeUser> ().Where (x => x.Id > 21).Exists;
			Assert.IsFalse (result3);

			bool result4 = context.Query<TeUser> ().Where (x => x.Id > 1 & x.CheckPoint != null).Exists;
			Assert.IsTrue (result4);

			bool result5 = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).Exists;
			Assert.IsFalse (result5);
		}

		[Test ()]
		public void TestCase_Count ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;
			ex = list.Count;
			ac = context.Query<TeUser> ().Count;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1).Count;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 21).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 21).Count;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1 & x.CheckPoint != null).Count;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).Count;
			Assert.AreEqual (ex, ac);
		}

		[Test ()]
		public void TestCase_LongCount ()
		{
			List<TeUser> list = InitialUserTable (21);
			long ex = 0;
			long ac = 0;
			ex = list.Count;
			ac = context.Query<TeUser> ().LongCount;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1).LongCount;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 21).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 21).LongCount;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1 & x.CheckPoint != null).LongCount;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).LongCount;
		}

		[Test ()]
		public void TestCase_CountFieldBase ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;
			ex = list.Count;
			ac = context.Query<TeUser> ().Count;
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 21).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 21).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id > 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id > 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);
		}

		[Test ()]
		public void TestCase_CountField_Bool ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.DeleteFlag);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id == 1 & x.CheckPoint != null).ForEach (x => {
				if (x.CheckStatus != null) {
					ex++;
				}
			});
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.CheckStatus);
			Assert.AreEqual (ex, ac);

			ac = context.Query<TeUser> ().Where (x => x.CheckStatus == null).AggregateFunction ().Count (x => x.CheckStatus);
			Assert.AreEqual (0, ac);
		}

		[Test ()]
		public void TestCase_CountField_Double ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.HotRate);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id == 1 & x.CheckPoint != null).ForEach (x => {
				if (x.CheckPoint != null) {
					ex++;
				}
			});
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.CheckPoint);
			Assert.AreEqual (ex, ac);

			ac = context.Query<TeUser> ().Where (x => x.CheckPoint == null).AggregateFunction ().Count (x => x.CheckPoint);
			Assert.AreEqual (0, ac);
		}

		[Test ()]
		public void TestCase_CountField_String ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id == 1 & x.CheckPoint != null).ForEach (x => {
				if (x.Account != null) {
					ex++;
				}
			});
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ac = context.Query<TeUser> ().Where (x => x.Account == null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (0, ac);
		}

		[Test ()]
		public void TestCase_CountField_Int ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Account);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id == 1 & x.CheckPoint != null).ForEach (x => {
				if (x.Area != null) {
					ex++;
				}
			});
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Area);
			Assert.AreEqual (ex, ac);

			ac = context.Query<TeUser> ().Where (x => x.Area == null).AggregateFunction ().Count (x => x.Area);
			Assert.AreEqual (0, ac);
		}

		[Test ()]
		public void TestCase_CountField_DateTime ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = list.FindAll (x => x.Id == 1 & x.CheckPoint != null).Count;
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.Birthday);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id == 1 & x.CheckPoint != null).ForEach (x => {
				if (x.LastLoginTime != null) {
					ex++;
				}
			});
			ac = context.Query<TeUser> ().Where (x => x.Id == 1 & x.CheckPoint != null).AggregateFunction ().Count (x => x.LastLoginTime);
			Assert.AreEqual (ex, ac);

			ac = context.Query<TeUser> ().Where (x => x.LastLoginTime == null).AggregateFunction ().Count (x => x.LastLoginTime);
			Assert.AreEqual (0, ac);
		}

		[Test ()]
		public void TestCase_SumField ()
		{
			List<TeUser> list = InitialUserTable (21);
			int ex = 0;
			int ac = 0;

			ex = 0;
			list.ForEach (x => {
				ex += x.LevelId;
			});
			ac = context.Query<TeUser> ().AggregateFunction ().Sum (x => x.LevelId);
			Assert.AreEqual (ex, ac);

			ex = 0;
			list.FindAll (x => x.Id > 10).ForEach (x => {
				ex += x.LevelId;
			});
			ac = context.Query<TeUser> ().Where (x => x.Id > 10).AggregateFunction ().Sum (x => x.LevelId);
			Assert.AreEqual (ex, ac);

			int? ex1 = null;
			int? ac1 = null;

			ex = 0;
			ex1 = null;
			list.FindAll (x => x.Id > 10).ForEach (x => {
				if (x.Area != null) {
					ex += x.Area.Value;
				}
			});
			if (ex > 0) {
				ex1 = ex;
			}
			ac1 = context.Query<TeUser> ().Where (x => x.Id > 10).AggregateFunction ().Sum (x => x.Area);
			Assert.AreEqual (ex1, ac1);

			ac1 = context.Query<TeUser> ().Where (x => x.Area == null).AggregateFunction ().Sum (x => x.Area);
			Assert.IsNull (ac1);

			ac = context.Query<TeUser> ().Where (x => x.Area == null).AggregateFunction ().Sum (x => x.Area.Value);
			Assert.AreEqual (0, ac);
		}
	}
}
