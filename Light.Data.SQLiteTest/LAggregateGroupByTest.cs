using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class LAggregateGroupByTest:BaseTest
	{
		[Test ()]
		public void TestCase_GroupBy_Count ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listAgg;
			Dictionary<int,int> dict;
			Dictionary<int,HashSet<int>> dictdist;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (TeUser.AreaField), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (TeUser.RefereeIdField, true), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					dict [user.LevelId] = i + 1;
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.AreaField), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.AreaField, true), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
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
			List<LevelIdAgg> listAgg;
			Dictionary<int,int> dict;
			Dictionary<int,HashSet<int>> dictdist;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Sum (TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + user.LoginTimes;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Sum (TeUser.LoginTimesField, true), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Sum (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Sum (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField, true), "Data").GetObjectList<LevelIdAgg> ();
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
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


		}

		[Test ()]
		public void TestCase_GroupBy_Avg ()
		{
			
			InitialUserTable (57);

			
			List<TeUser> list;
			List<LevelIdAggAvg> listAgg;
			Dictionary<int,int> dict;
			Dictionary<int,int> dictCount;
			Dictionary<int,HashSet<int>> dictdist;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Avg (TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAggAvg> ();
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
			foreach (LevelIdAggAvg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				int c;
				Assert.IsTrue (dictCount.TryGetValue (agg.LevelId, out c));
				if (agg.Data.HasValue) {
					Assert.AreEqual (FormatDouble ((double)i / c), FormatDouble (agg.Data.Value));
				}
			}
			
			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Avg (TeUser.LoginTimesField, true), "Data").GetObjectList<LevelIdAggAvg> ();
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
			foreach (LevelIdAggAvg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				int c;
				Assert.IsTrue (dictCount.TryGetValue (agg.LevelId, out c));
				Assert.AreEqual (FormatDouble ((double)i / c), FormatDouble (agg.Data.Value));
			}

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Avg (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAggAvg> ();
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
			foreach (LevelIdAggAvg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));

				int c;
				dictCount.TryGetValue (agg.LevelId, out c);
				if (c > 0) {
					Assert.AreEqual (FormatDouble ((double)i / c), FormatDouble (agg.Data.Value));
				}
				else {
					Assert.IsNull (agg.Data);
				}

			}
			
			
			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Avg (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField, true), "Data").GetObjectList<LevelIdAggAvg> ();
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
			foreach (LevelIdAggAvg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
			
				int c;
				dictCount.TryGetValue (agg.LevelId, out c);
				if (c > 0) {
					Assert.AreEqual (FormatDouble ((double)i / c), FormatDouble (agg.Data.Value));
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
			List<LevelIdAgg> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Max (TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.LoginTimes > i) {
					dict [user.LevelId] = user.LoginTimes;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Max (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					if (user.LoginTimes > i) {
						dict [user.LevelId] = user.LoginTimes;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_Min ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Min (TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.LoginTimes < i || i == 0) {
					dict [user.LevelId] = user.LoginTimes;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}


			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Min (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField), "Data").GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, int> ();
			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					if (user.LoginTimes < i || i == 0) {
						dict [user.LevelId] = user.LoginTimes;
					}
				}
				else {
					dict [user.LevelId] = i;
				}
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAgg agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_GroupBy_Multi ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggMul> listAgg;
			Dictionary<int,int> dict;
			Dictionary<int,int> dictCount;
			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Count")
				.Aggregate (AggregateFunction.Avg (TeUser.LoginTimesField), "Avg")
				.GetObjectList<LevelIdAggMul> ();
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
				Assert.AreEqual (FormatDouble ((double)i / c), FormatDouble (agg.Avg));
				Assert.AreEqual (c, agg.Count);
			}

		}

		[Test ()]
		public void TestCase_GroupBy_Enum ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggEnum> listAgg;
			Dictionary<GenderType,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.GenderField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAggEnum> ();
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
			Dictionary<CheckLevelType?,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.CheckLevelTypeField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAggEnumNull> ();
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

