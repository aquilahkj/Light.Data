using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_AggregateWhereTest : BaseTest
	{
		[Test ()]
		public void TestCase_QueryWhere_Base ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			List<LevelIdAgg> listAgg;
			Dictionary<int, int> dict;

			list = context.Query<TeUser> ()
						  .Where (x => x.Id >= 5)
						  .ToList ();
			listAgg = context.Query<TeUser> ()
							 .Where (x => x.Id >= 5)
							 .GroupBy (x => new LevelIdAgg () {
								 LevelId = x.LevelId,
								 Data = Function.Count ()
							 }).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

		}


		[Test ()]
		public void TestCase_QueryWhere_AndOr ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			List<LevelIdAgg> listAgg;
			Dictionary<int, int> dict;

			list = context.Query<TeUser> ()
						  .Where (x => x.Id >= 5 & x.Id <= 10)
				.ToList ();
			listAgg = context.Query<TeUser> ()
							 .Where (x => x.Id >= 5 & x.Id <= 10)
							 .GroupBy (x => new LevelIdAgg () {
								 LevelId = x.LevelId,
								 Data = Function.Count ()
							 }).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ()
						.Where (x => x.Id >= 5)
						.WhereWithAnd (x => x.Id <= 10)
						.ToList ();
			listAgg = context.Query<TeUser> ()
						.Where (x => x.Id >= 5)
						.WhereWithAnd (x => x.Id <= 10)
						.GroupBy (x => new LevelIdAgg () {
							LevelId = x.LevelId,
							Data = Function.Count ()
						}).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ()
						  .Where (x => x.Id < 5 || x.Id > 10)
						  .ToList ();
			listAgg = context.Query<TeUser> ()
							 .Where (x => x.Id < 5 || x.Id > 10)
							 .GroupBy (x => new LevelIdAgg () {
								 LevelId = x.LevelId,
								 Data = Function.Count ()
							 }).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ()
							.Where (x => x.Id < 5)
							.WhereWithOr (x => x.Id > 10)
							.ToList ();
			listAgg = context.Query<TeUser> ()
						.Where (x => x.Id < 5)
						.WhereWithOr (x => x.Id > 10)
						.GroupBy (x => new LevelIdAgg () {
							LevelId = x.LevelId,
							Data = Function.Count ()
						}).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ()
						  .Where (x => x.Id >= 5)
						  .Where (x => x.Id <= 10)
						  .ToList ();
			listAgg = context.Query<TeUser> ()
						.Where (x => x.Id >= 5)
						.Where (x => x.Id <= 10)
						.GroupBy (x => new LevelIdAgg () {
							LevelId = x.LevelId,
							Data = Function.Count ()
						}).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ()
				.Where (x => x.Id >= 5)
				.WhereReset ()
				.ToList ();
			listAgg = context.Query<TeUser> ()
						.Where (x => x.Id >= 5)
						.WhereReset ()
						.GroupBy (x => new LevelIdAgg () {
							LevelId = x.LevelId,
							Data = Function.Count ()
						}).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}
	}
}

