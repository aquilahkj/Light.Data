using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.PostgreTest
{
	[TestFixture ()]
	public class LAggregateWhereTest:BaseTest
	{
		[Test ()]
		public void TestCase_QueryWhere_Base ()
		{
			InitialUserTable (21);
			List<TeUser> list;
			List<LevelIdAgg> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField >= 5)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField >= 5)
				.GetObjectList<LevelIdAgg> ();
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
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField >= 5 & TeUser.IdField <= 10)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField >= 5 & TeUser.IdField <= 10)
				.GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField >= 5)
				.WhereWithAnd (TeUser.IdField <= 10)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField >= 5)
				.WhereWithAnd (TeUser.IdField <= 10)
				.GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField < 5 | TeUser.IdField > 10)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField < 5 | TeUser.IdField > 10)
				.GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField < 5)
				.WhereWithOr (TeUser.IdField > 10)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField < 5)
				.WhereWithOr (TeUser.IdField > 10)
				.GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField >= 5)
				.Where (TeUser.IdField <= 10)
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField >= 5)
				.Where (TeUser.IdField <= 10)
				.GetObjectList<LevelIdAgg> ();
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

			list = context.LQuery<TeUser> ()
				.Where (TeUser.IdField >= 5)
				.WhereReset()
				.ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.Where (TeUser.IdField >= 5)
				.WhereReset()
				.GetObjectList<LevelIdAgg> ();
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

