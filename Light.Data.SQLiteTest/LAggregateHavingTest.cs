using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class LAggregateHavingTest:BaseTest
	{
		[Test ()]
		public void TestCase_QueryHaving_Base ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int,LevelIdAgg> dict;
			AggregateFunction function = AggregateFunction.Sum (TeUser.LoginTimesField);


			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function > 15)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
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
			Dictionary<int,LevelIdAgg> dict;
			AggregateFunction function = AggregateFunction.Sum (TeUser.LoginTimesField);

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function > 15 & function < 20)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 15 && kv.Value.Data < 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 15 && x.Data < 20));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function > 15)
				.HavingWithAnd (function < 20)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 15 && kv.Value.Data < 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 15 && x.Data < 20));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function <= 15 | function >= 20)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 15 || kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 15 || x.Data >= 20));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function <= 15)
				.HavingWithOr (function >= 20)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 15 || kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 15 || x.Data >= 20));


			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function <= 15)
				.Having (function >= 20)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data >= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data >= 20));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function <= 15)
				.HavingReset ()
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
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
			Dictionary<int,LevelIdAgg> dict;
			AggregateFunction function = AggregateFunction.Sum (TeUser.LoginTimesField);

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function == 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data == 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data == 18));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function != 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data != 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data != 18));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function >= 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data >= 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data >= 18));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function <= 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data <= 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data <= 18));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function > 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data > 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data > 18));


			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function < 18)
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data < 18) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data < 18));

		}

		[Test ()]
		public void TestCase_QueryHaving_Between ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int,LevelIdAgg> dict;
			AggregateFunction function = AggregateFunction.Sum (TeUser.LoginTimesField);

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.Between (15, 20))
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data >= 15 && kv.Value.Data <= 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data >= 15 && x.Data <= 20));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.NotBetween (15, 20))
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (kv.Value.Data < 15 || kv.Value.Data > 20) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => x.Data < 15 || x.Data > 20));

		}

		[Test ()]
		public void TestCase_QueryHaving_AnyAllInArray ()
		{
			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int,LevelIdAgg> dict;
			AggregateFunction function = AggregateFunction.Sum (TeUser.LoginTimesField);

			int[] arrayx = new int[]{ 18, 20 };
			List<int> listx = new List<int> (arrayx);

			//list = context.LQuery<TeUser> ().ToList ();
			//listAc = context.LAggregate<TeUser> ()
			//	.GroupBy (TeUser.LevelIdField)
			//	.Aggregate (function, "Data")
			//	.Having (function.LtAny (arrayx))
			//	.GetObjectList<LevelIdAgg> ();
			//dict = new Dictionary<int, LevelIdAgg> ();
			//listEx = new List<LevelIdAgg> ();
			//foreach (TeUser user in list) {
			//	LevelIdAgg i;
			//	if (!dict.TryGetValue (user.LevelId, out i)) {
			//		i = new LevelIdAgg ();
			//		i.LevelId = user.LevelId;
			//		i.Data = 0;
			//	}
			//	i.Data += user.LoginTimes;
			//	dict [user.LevelId] = i;
			//}
			//foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
			//	if (listx.Exists (x => kv.Value.Data < x)) {
			//		listEx.Add (kv.Value);
			//	}
			//}
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => {
			//	return listx.Exists (y => x.Data < y);
			//}));

			//list = context.LQuery<TeUser> ().ToList ();
			//listAc = context.LAggregate<TeUser> ()
			//	.GroupBy (TeUser.LevelIdField)
			//	.Aggregate (function, "Data")
			//	.Having (function.GtAny (arrayx))
			//	.GetObjectList<LevelIdAgg> ();
			//dict = new Dictionary<int, LevelIdAgg> ();
			//listEx = new List<LevelIdAgg> ();
			//foreach (TeUser user in list) {
			//	LevelIdAgg i;
			//	if (!dict.TryGetValue (user.LevelId, out i)) {
			//		i = new LevelIdAgg ();
			//		i.LevelId = user.LevelId;
			//		i.Data = 0;
			//	}
			//	i.Data += user.LoginTimes;
			//	dict [user.LevelId] = i;
			//}
			//foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
			//	if (listx.Exists (x => kv.Value.Data > x)) {
			//		listEx.Add (kv.Value);
			//	}
			//}
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => {
			//	return listx.Exists (y => x.Data > y);
			//}));

			//list = context.LQuery<TeUser> ().ToList ();
			//listAc = context.LAggregate<TeUser> ()
			//	.GroupBy (TeUser.LevelIdField)
			//	.Aggregate (function, "Data")
			//	.Having (function.LtAll (arrayx))
			//	.GetObjectList<LevelIdAgg> ();
			//dict = new Dictionary<int, LevelIdAgg> ();
			//listEx = new List<LevelIdAgg> ();
			//foreach (TeUser user in list) {
			//	LevelIdAgg i;
			//	if (!dict.TryGetValue (user.LevelId, out i)) {
			//		i = new LevelIdAgg ();
			//		i.LevelId = user.LevelId;
			//		i.Data = 0;
			//	}
			//	i.Data += user.LoginTimes;
			//	dict [user.LevelId] = i;
			//}
			//foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
			//	if (listx.TrueForAll (x => kv.Value.Data < x)) {
			//		listEx.Add (kv.Value);
			//	}
			//}
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => {
			//	return listx.TrueForAll (y => x.Data < y);
			//}));

			//list = context.LQuery<TeUser> ().ToList ();
			//listAc = context.LAggregate<TeUser> ()
			//	.GroupBy (TeUser.LevelIdField)
			//	.Aggregate (function, "Data")
			//	.Having (function.GtAll (arrayx))
			//	.GetObjectList<LevelIdAgg> ();
			//dict = new Dictionary<int, LevelIdAgg> ();
			//listEx = new List<LevelIdAgg> ();
			//foreach (TeUser user in list) {
			//	LevelIdAgg i;
			//	if (!dict.TryGetValue (user.LevelId, out i)) {
			//		i = new LevelIdAgg ();
			//		i.LevelId = user.LevelId;
			//		i.Data = 0;
			//	}
			//	i.Data += user.LoginTimes;
			//	dict [user.LevelId] = i;
			//}
			//foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
			//	if (listx.TrueForAll (x => kv.Value.Data > x)) {
			//		listEx.Add (kv.Value);
			//	}
			//}
			//Assert.AreEqual (listEx.Count, listAc.Count);
			//Assert.IsTrue (listAc.TrueForAll (x => {
			//	return listx.TrueForAll (y => x.Data > y);
			//}));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.In (arrayx))
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (listx.Exists (x => kv.Value.Data == x)) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => {
				return listx.Exists (y => x.Data == y);
			}));

			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.NotIn (arrayx))
				.GetObjectList<LevelIdAgg> ();
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
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
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
			Dictionary<int,LevelIdAggX> dict;
			AggregateFunction function = AggregateFunction.Avg (TeUser.RefereeIdField >= 4 & TeUser.RefereeIdField <= 8, TeUser.LoginTimesField);

			list = context.LQuery<TeUser> ().ToList ();
			listEx = new List<LevelIdAggAvg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.IsNull ())
				.GetObjectList<LevelIdAggAvg> ();
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

			foreach (KeyValuePair<int,LevelIdAggX> kv in dict) {
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

			list = context.LQuery<TeUser> ().ToList ();
			listEx = new List<LevelIdAggAvg> ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.IsNotNull ())
				.GetObjectList<LevelIdAggAvg> ();
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

			foreach (KeyValuePair<int,LevelIdAggX> kv in dict) {
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

		[Test ()]
		public void TestCase_QueryHaving_SubQuery_Match ()
		{
			List<TeUser> list = InitialUserTable (15);
			List<TeUserLevel> listLevel = InitialUserLevelTable (8);
			List<LevelIdAgg> listEx;
			List<LevelIdAgg> listAc;
			Dictionary<int,LevelIdAgg> dict;
			List<TeUserLevel> listSub;
			AggregateFunction function = AggregateFunction.Max (TeUser.LoginTimesField);

			listSub = listLevel.FindAll (x => x.Id >= 2 && x.Id <= 4);
			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.In (TeUserLevel.IdField, TeUserLevel.IdField.Between (2, 4)))
				.GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				if (user.LoginTimes > i.Data) {
					i.Data = user.LoginTimes;
				}
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (listSub.Exists (x => x.Id == kv.Value.Data)) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.LevelId == x.LevelId && y.Data == x.Data)));


			listSub = listLevel.FindAll (x => x.Id >= 2 && x.Id <= 4);
			list = context.LQuery<TeUser> ().ToList ();
			listAc = context.LAggregate<TeUser> ()
				.GroupBy (TeUser.LevelIdField)
				.Aggregate (function, "Data")
				.Having (function.NotIn (TeUserLevel.IdField, TeUserLevel.IdField.Between (2, 4)))
				.GetObjectList<LevelIdAgg> ();
			dict = new Dictionary<int, LevelIdAgg> ();
			listEx = new List<LevelIdAgg> ();
			foreach (TeUser user in list) {
				LevelIdAgg i;
				if (!dict.TryGetValue (user.LevelId, out i)) {
					i = new LevelIdAgg ();
					i.LevelId = user.LevelId;
					i.Data = 0;
				}
				if (user.LoginTimes > i.Data) {
					i.Data = user.LoginTimes;
				}
				dict [user.LevelId] = i;
			}
			foreach (KeyValuePair<int,LevelIdAgg> kv in dict) {
				if (listSub.TrueForAll (x => x.Id != kv.Value.Data)) {
					listEx.Add (kv.Value);
				}
			}
			Assert.AreEqual (listEx.Count, listAc.Count);
			Assert.IsTrue (listAc.TrueForAll (x => listEx.Exists (y => y.LevelId == x.LevelId && y.Data == x.Data)));
		}
	}
}

