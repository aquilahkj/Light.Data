using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_AggregateHavingTest : BaseTest
	{
		[Test ()]
		public void TestCase_QueryHaving_Base ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int, LevelIdAgg> dict;

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data > 15)
			.ToList ();

			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 15) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 15));
		}

		[Test ()]
		public void TestCase_QueryHaving_AndOr ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int, LevelIdAgg> dict;

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data > 15 && y.Data < 20)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 15 && kv.Value.Data < 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 15 && x.Data < 20));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data > 15)
			.HavingWithAnd (y => y.Data < 20)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 15 && kv.Value.Data < 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 15 && x.Data < 20));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data <= 15 || y.Data >= 20)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 15 || kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 15 || x.Data >= 20));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data <= 15)
			.HavingWithOr (y => y.Data >= 20)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 15 || kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 15 || x.Data >= 20));


			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data <= 15)
			.Having (y => y.Data >= 20)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data >= 20));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data <= 15)
			.HavingReset ()
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				listEx.Add (kv.Value);
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
		}

		[Test ()]
		public void TestCase_QuerySingleParam ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int, LevelIdAgg> dict;

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data == 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data == 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data == 18));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data != 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data != 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data != 18));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data >= 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data >= 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data >= 18));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data <= 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 18));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data > 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 18));


			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data < 18)
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (kv.Value.Data < 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data < 18));

		}

		[Test ()]
		public void TestCase_QueryHaving_AnyAllInArray ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int, LevelIdAgg> dict;

			int [] arrayx = new int [] { 18, 20 };
			List<int> listx = new List<int> (arrayx);

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => listx.Contains (y.Data))
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (listx.Exists (x => kv.Value.Data == x)) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listx.Exists (y => x.Data == y);
			}));

			list = context.Query<TeUser> ().ToList ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => !listx.Contains (y.Data))
			.ToList ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				i.Data += user.LoginTimes;
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int, LevelIdAgg> kv in dict) {
				if (listx.TrueForAll (x => kv.Value.Data != x)) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listx.TrueForAll (y => x.Data != y);
			}));

		}

		[Test ()]
		public void TestCase_QueryHaving_Null ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggAvg> listEx;
			List<LevelIdAggAvg> listAc;
			Dictionary<int, LevelIdAggX> dict;

			list = context.Query<TeUser> ().ToList ();
			listEx = new List<LevelIdAggAvg> ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAggAvg () {
				LevelId = x.LevelId,
				Data = Function.Avg (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
			})
			.Having (y => y.Data == null)
			.ToList ();
			dict = new Dictionary<int, LevelIdAggX> ();
			foreach (TeUser user in list) {
				LevelIdAggX i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAggX ();
					i.LevelId = user.LevelId;
					i.Count = 0;
					i.Sum = 0;
				}
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					i.Count += 1;
					i.Sum += user.LoginTimes;
				}
				dict [user.LevelId] = i;
			}

			foreach (KeyValuePair<int, LevelIdAggX> kv in dict) {
				if (kv.Value.Count == 0) {
					LevelIdAggAvg l = new LevelIdAggAvg ();
					l.LevelId = kv.Value.LevelId;
					listEx.Add (l);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listEx.Exists (y => y.LevelId == x.LevelId) && x.Data == null;
			}));

			list = context.Query<TeUser> ().ToList ();
			listEx = new List<LevelIdAggAvg> ();
			listAc = context.Query<TeUser> ().GroupBy (x => new LevelIdAggAvg () {
				LevelId = x.LevelId,
				Data = Function.Avg (x.RefereeId >= 4 && x.RefereeId <= 8 ? x.LoginTimes as int? : null)
			})
			.Having (y => y.Data != null)
			.ToList ();
			dict = new Dictionary<int, LevelIdAggX> ();
			foreach (TeUser user in list) {
				LevelIdAggX i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAggX ();
					i.LevelId = user.LevelId;
					i.Count = 0;
					i.Sum = 0;
				}
				if (user.RefereeId != null && user.RefereeId.Value >= 4 && user.RefereeId.Value <= 8) {
					i.Count += 1;
					i.Sum += user.LoginTimes;
				}
				dict [user.LevelId] = i;
			}

			foreach (KeyValuePair<int, LevelIdAggX> kv in dict) {
				if (kv.Value.Count > 0) {
					LevelIdAggAvg l = new LevelIdAggAvg ();
					l.LevelId = kv.Value.LevelId;
					listEx.Add (l);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listEx.Exists (y => y.LevelId == x.LevelId) && x.Data != null;
			}));
		}
	}
}

