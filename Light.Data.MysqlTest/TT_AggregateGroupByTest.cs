using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_AggregateGroupByTest : BaseTest
	{
		[Test ()]
		public void TestCase_GroupBy_Count ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg_T> listAgg;
			Dictionary<int, int> dict;
			Dictionary<int, HashSet<int>> dictdist;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
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
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.Count (x.Area)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.Area != null) {
					dict [user.LevelId] = i + 1;
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.DistinctCount (x.RefereeId)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null) {
					HashSet<int> hash;
					dictdist.TryGetValue (user.LevelId, out hash);
					if (hash == null) {
						hash = new HashSet<int> ();
						dictdist [user.LevelId] = hash;
					}
					if (hash.Contains (user.RefereeId.Value)) {
						dict [user.LevelId] = i;
					}
					else {
						hash.Add (user.RefereeId.Value);
						dict [user.LevelId] = i + 1;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.Count (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.Area : null)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8 && user.Area != null) {
					dict [user.LevelId] = i + 1;
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.DistinctCount (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.Area : null)
							 }).ToList ();

			dict = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8 && user.Area != null) {
					HashSet<int> hash;
					dictdist.TryGetValue (user.LevelId, out hash);
					if (hash == null) {
						hash = new HashSet<int> ();
						dictdist [user.LevelId] = hash;
					}
					if (hash.Contains (user.Area.Value)) {
						dict [user.LevelId] = i;
					}
					else {
						hash.Add (user.Area.Value);
						dict [user.LevelId] = i + 1;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_CountCondition ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg_T> listAgg;
			Dictionary<int, int> dict;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.CountCondition (x.LoginTimes > 4)
							 }).ToList ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.LoginTimes > 4) {
					dict [user.LevelId] = i + 1;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


		}

		[Test ()]
		public void TestCase_GroupBy_Sum ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg_T> listAgg;
			List<LevelIdAgg_N> listAggN;
			Dictionary<int, int> dict;
			Dictionary<int, int?> dictN;
			Dictionary<int, HashSet<int>> dictdist;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.Sum (x.LoginTimes)
							 }).ToList ();

			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + user.LoginTimes;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.DistinctSum (x.LoginTimes)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				HashSet<int> hash;
				dictdist.TryGetValue (user.LevelId, out hash);
				if (hash == null) {
					hash = new HashSet<int> ();
					dictdist [user.LevelId] = hash;
				}
				if (hash.Contains (user.LoginTimes)) {
					dict [user.LevelId] = i;
				}
				else {
					hash.Add (user.LoginTimes);
					dict [user.LevelId] = i + user.LoginTimes;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.Sum (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes : 0)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					dict [user.LevelId] = i + user.LoginTimes;
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.Query<TeUser> ().ToList ();
			listAggN = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_N () {
								 LevelId = x.LevelId,
								 Data = Function.Sum (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
							 }).ToList ();
			dictN = new Dictionary<int, int?> ();
			foreach (TeUser user in list) {
				int? i;
				dictN.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					if (i == null) {
						dictN [user.LevelId] = user.LoginTimes;
					}
					else {
						dictN [user.LevelId] = i + user.LoginTimes;
					}
				}
				else {
					dictN [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dictN.Count, listAggN.Count);
			foreach (LevelIdAgg_N agg in listAggN) {
				int? i;
				Assert.IsTrue (dictN.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_T () {
								 LevelId = x.LevelId,
								 Data = Function.DistinctSum (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes : 0)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					HashSet<int> hash;
					dictdist.TryGetValue (user.LevelId, out hash);
					if (hash == null) {
						hash = new HashSet<int> ();
						dictdist [user.LevelId] = hash;
					}
					if (hash.Contains (user.LoginTimes)) {
						dict [user.LevelId] = i;
					}
					else {
						hash.Add (user.LoginTimes);
						dict [user.LevelId] = i + user.LoginTimes;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


		}

		[Test ()]
		public void TestCase_GroupBy_Sum_Null_First ()
		{
			context.TruncateTable<TeUser> ();

			var fir = context.Query<TeUser> ().GroupBy (x => new {
				LevelId = x.LevelId,
				SumData = Function.Sum (x.LevelId)
			}).First ();

			Assert.IsNull (fir);
		}

		[Test ()]
		public void TestCase_GroupBy_Avg ()
		{

			InitialUserTable (57);


			List<TeUser> list;
			List<LevelIdAggAvg_T> listAgg;
			Dictionary<int, int> dict;
			Dictionary<int, int> dictCount;
			Dictionary<int, HashSet<int>> dictdist;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAggAvg_T () {
								 LevelId = x.LevelId,
								 Data = Function.Avg (x.LoginTimes)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictCount = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + user.LoginTimes;
				int c;
				dictCount.TryGetValue (user.LevelId, out c);
				dictCount [user.LevelId] = c + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggAvg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				int c;
				Assert.IsTrue (dictCount.TryGetValue (agg.LevelId, out c));
				if (agg.Data.HasValue) {
					Assert.AreEqual ((double)i / c, agg.Data.Value, DELTA);
				}
			}

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAggAvg_T () {
								 LevelId = x.LevelId,
								 Data = Function.DistinctAvg (x.LoginTimes)
							 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictCount = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				HashSet<int> hash;
				dictdist.TryGetValue (user.LevelId, out hash);
				if (hash == null) {
					hash = new HashSet<int> ();
					dictdist [user.LevelId] = hash;
				}
				if (hash.Contains (user.LoginTimes)) {
					dict [user.LevelId] = i;
				}
				else {
					hash.Add (user.LoginTimes);
					dict [user.LevelId] = i + user.LoginTimes;
					int c;
					dictCount.TryGetValue (user.LevelId, out c);
					dictCount [user.LevelId] = c + 1;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggAvg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				int c;
				Assert.IsTrue (dictCount.TryGetValue (agg.LevelId, out c));
				Assert.AreEqual ((double)i / c, agg.Data.Value, DELTA);
			}

			list = context.Query<TeUser> ().ToList ();

			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAggAvg_T () {
					 LevelId = x.LevelId,
					 Data = Function.Avg (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
				 }).ToList ();
			dict = new Dictionary<int, int> ();
			dictCount = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					dict [user.LevelId] = i + user.LoginTimes;
					int c;
					dictCount.TryGetValue (user.LevelId, out c);
					dictCount [user.LevelId] = c + 1;
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggAvg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));

				int c;
				dictCount.TryGetValue (agg.LevelId, out c);
				if (c > 0) {
					Assert.AreEqual ((double)i / c, agg.Data.Value, DELTA);
				}
				else {
					Assert.IsNull (agg.Data);
				}

			}


			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAggAvg_T () {
					 LevelId = x.LevelId,
					 Data = Function.DistinctAvg (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
				 }).ToList ();

			dict = new Dictionary<int, int> ();
			dictCount = new Dictionary<int, int> ();
			dictdist = new Dictionary<int, HashSet<int>> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					HashSet<int> hash;
					dictdist.TryGetValue (user.LevelId, out hash);
					if (hash == null) {
						hash = new HashSet<int> ();
						dictdist [user.LevelId] = hash;
					}
					if (hash.Contains (user.LoginTimes)) {
						dict [user.LevelId] = i;
					}
					else {
						hash.Add (user.LoginTimes);
						dict [user.LevelId] = i + user.LoginTimes;
						int c;
						dictCount.TryGetValue (user.LevelId, out c);
						dictCount [user.LevelId] = c + 1;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggAvg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));

				int c;
				dictCount.TryGetValue (agg.LevelId, out c);
				if (c > 0) {
					Assert.AreEqual ((double)i / c, agg.Data.Value, DELTA);
				}
				else {
					Assert.IsNull (agg.Data);
				}

			}

		}

		[Test ()]
		public void TestCase_GroupBy_Max ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg_T> listAgg;
			List<LevelIdAgg_N> listAggN;
			Dictionary<int, int> dict;
			Dictionary<int, int?> dictN;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAgg_T () {
					 LevelId = x.LevelId,
					 Data = Function.Max (x.LoginTimes)
				 }).ToList ();

			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.LoginTimes > i) {
					dict [user.LevelId] = user.LoginTimes;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAgg_T () {
					 LevelId = x.LevelId,
					 Data = Function.Max (x.RefereeId >= 2 && x.RefereeId <= 8 ? x.LoginTimes : 0)
				 }).ToList ();

			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 2 && user.RefereeId.Value <= 8) {
					if (user.LoginTimes > i) {
						dict [user.LevelId] = user.LoginTimes;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.Query<TeUser> ().ToList ();
			listAggN = context.Query<TeUser> ()
							 .GroupBy (x => new LevelIdAgg_N () {
								 LevelId = x.LevelId,
								 Data = Function.Max (x.RefereeId >= 2 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
							 }).ToList ();

			dictN = new Dictionary<int, int?> ();
			foreach (TeUser user in list) {
				int? i;
				dictN.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 2 && user.RefereeId.Value <= 8) {
					if (i == null || user.LoginTimes > i) {
						dictN [user.LevelId] = user.LoginTimes;
					}
				}
				else {
					dictN [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dictN.Count, listAggN.Count);
			foreach (LevelIdAgg_N agg in listAggN) {
				int? i;
				Assert.IsTrue (dictN.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_Min ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg_T> listAgg;
			List<LevelIdAgg_N> listAggN;
			Dictionary<int, int> dict;
			Dictionary<int, int?> dictN;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAgg_T () {
					 LevelId = x.LevelId,
					 Data = Function.Min (x.LoginTimes)
				 }).ToList ();

			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.LoginTimes < i || i == 0) {
					dict [user.LevelId] = user.LoginTimes;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg_T agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			//list = context.Query<TeUser> ().ToList ();
			//listAgg = context.Query<TeUser> ()
			//	 .GroupBy (x => new LevelIdAgg_T () {
			//		 LevelId = x.LevelId,
			//		 Data = Function.Min (x.RefereeId >= 2 && x.RefereeId <= 8 ? x.LoginTimes : 0)
			//	 }).ToList ();
			//dict = new Dictionary<int, int> ();
			//foreach (TeUser user in list) {
			//	int i;
			//	dict.TryGetValue (user.LevelId, out i);
			//	if (user.RefereeId != null && user.RefereeId.Value >= 2 && user.RefereeId.Value <= 8) {
			//		if (user.LoginTimes < i || i == 0) {
			//			dict [user.LevelId] = user.LoginTimes;
			//		}
			//	}
			//	else {
			//		dict [user.LevelId] = i;
			//	}
			//}
			//Assert.AreEqual (dict.Count, listAgg.Count);
			//foreach (LevelIdAgg_T agg in listAgg) {
			//	int i;
			//	Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
			//	Assert.AreEqual (i, agg.Data);
			//}


			list = context.Query<TeUser> ().ToList ();
			listAggN = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAgg_N () {
					 LevelId = x.LevelId,
					 Data = Function.Min (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
				 }).ToList ();
			dictN = new Dictionary<int, int?> ();
			foreach (TeUser user in list) {
				int? i;
				dictN.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					if (i == null || user.LoginTimes < i || i == 0) {
						dictN [user.LevelId] = user.LoginTimes;
					}
				}
				else {
					dictN [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dictN.Count, listAggN.Count);
			foreach (LevelIdAgg_N agg in listAggN) {
				int? i;
				Assert.IsTrue (dictN.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_Multi ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggMul> listAgg;
			Dictionary<int, int> dict;
			Dictionary<int, int> dictCount;
			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAggMul () {
					 LevelId = x.LevelId,
					 Count = Function.Count (),
					 Avg = Function.Avg (x.LoginTimes)
				 }).ToList ();

			dict = new Dictionary<int, int> ();
			dictCount = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + user.LoginTimes;
				int c;
				dictCount.TryGetValue (user.LevelId, out c);
				dictCount [user.LevelId] = c + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggMul agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				int c;
				Assert.IsTrue (dictCount.TryGetValue (agg.LevelId, out c));
				Assert.AreEqual ((double)i / c, agg.Avg, DELTA);
				Assert.AreEqual (c, agg.Count);
			}

		}

		[Test ()]
		public void TestCase_GroupBy_Enum ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggEnum> listAgg;
			Dictionary<GenderType, int> dict;

			list = context.Query<TeUser> ().ToList ();

			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAggEnum () {
					 Gender = x.Gender,
					 Data = Function.Count ()
				 }).ToList ();

			dict = new Dictionary<GenderType, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.Gender, out i);
				dict [user.Gender] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggEnum agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.Gender, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_Enum_Null ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggEnumNull> listAgg;
			Dictionary<CheckLevelType?, int> dict;

			list = context.Query<TeUser> ().ToList ();
			listAgg = context.Query<TeUser> ()
				 .GroupBy (x => new LevelIdAggEnumNull () {
					 CheckLevelType = x.CheckLevelType,
					 Data = Function.Count ()
				 }).ToList ();

			dict = new Dictionary<CheckLevelType?, int> ();
			int nullCount = 0;
			foreach (TeUser user in list) {
				if (user.CheckLevelType != null) {
					int i;
					dict.TryGetValue (user.CheckLevelType, out i);
					dict [user.CheckLevelType] = i + 1;
				}
				else {
					nullCount++;
				}
			}
			Assert.AreEqual (nullCount == 0 ? dict.Count : dict.Count + 1, listAgg.Count);
			foreach (LevelIdAggEnumNull agg in listAgg) {
				if (agg.CheckLevelType != null) {
					int i;
					Assert.IsTrue (dict.TryGetValue (agg.CheckLevelType, out i));
					Assert.AreEqual (i, agg.Data);
				}
				else {
					Assert.AreEqual (nullCount, agg.Data);
				}
			}
		}
	}
}

