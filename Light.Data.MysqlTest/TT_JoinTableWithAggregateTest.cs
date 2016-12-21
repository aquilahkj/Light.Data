using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableWithAggregateTest : BaseTest
	{
		[Test ()]
		public void TestCase_Join_With_Aggregate1 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeDataLog> listLog = InitialDataLogTable (50);

			List<TeUserLogAgg> listEx;
			List<TeUserLogAgg> listAc;

			Dictionary<int, TeDataLogAgg> dict = new Dictionary<int, TeDataLogAgg> ();

			var aggList = context.Query<TeDataLog> ()
								 .Where (x => x.UserId > 5).GroupBy (x => new TeDataLogAgg {
									 UserId = x.UserId,
									 Count = Function.Count (),
									 LastTime = Function.Max (x.RecordTime),
								 });

			foreach (TeDataLog item in listLog) {
				if (item.UserId > 5) {
					TeDataLogAgg agg;
					if (dict.TryGetValue (item.UserId, out agg)) {
						agg.Count++;
						if (agg.LastTime < item.RecordTime) {
							agg.LastTime = item.RecordTime;
						}
					}
					else {
						agg = new TeDataLogAgg ();
						agg.UserId = item.UserId;
						agg.Count = 1;
						agg.LastTime = item.RecordTime;
						dict [item.UserId] = agg;
					}
				}
			}
			listEx = new List<TeUserLogAgg> ();
			list.ForEach (x => {
				TeUserLogAgg agg = new TeUserLogAgg ();
				agg.User = x;
				TeDataLogAgg logagg;
				if (dict.TryGetValue (x.Id, out logagg)) {
					agg.LogAgg = logagg;
				}
				else {
					agg.LogAgg = new TeDataLogAgg ();
				}
				agg.UserId = x.Id;
				listEx.Add (agg);
			});

			listAc = context.Query<TeUser> ().LeftJoin (aggList, (x, y) => x.Id == y.UserId)
							.Select ((x, y) => new TeUserLogAgg () {
								UserId = x.Id,
								User = x,
								LogAgg = y
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			AssertExtend.AreObjectEqual (listEx, listAc);

		}


		[Test ()]
		public void TestCase_Join_With_Aggregate2 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeDataLog> listLog = InitialDataLogTable (50);

			List<TeUserLogAgg> listEx;
			List<TeUserLogAgg> listAc;

			Dictionary<int, TeDataLogAgg> dict = new Dictionary<int, TeDataLogAgg> ();

			var aggList = context.Query<TeDataLog> ()
								 .Where (x => x.UserId > 5).GroupBy (x => new TeDataLogAgg {
									 UserId = x.UserId,
									 Count = Function.Count (),
									 LastTime = Function.Max (x.RecordTime),
								 });

			foreach (TeDataLog item in listLog) {
				if (item.UserId > 5) {
					TeDataLogAgg agg;
					if (dict.TryGetValue (item.UserId, out agg)) {
						agg.Count++;
						if (agg.LastTime < item.RecordTime) {
							agg.LastTime = item.RecordTime;
						}
					}
					else {
						agg = new TeDataLogAgg ();
						agg.UserId = item.UserId;
						agg.Count = 1;
						agg.LastTime = item.RecordTime;
						dict [item.UserId] = agg;
					}
				}
			}
			listEx = new List<TeUserLogAgg> ();

			List<TeDataLogAgg> listTmp = new List<TeDataLogAgg> (dict.Values);

			listTmp.ForEach (x => {
				TeUserLogAgg agg = new TeUserLogAgg ();
				agg.LogAgg = x;
				TeUser user = list.Find (y => y.Id == x.UserId);
				if (user == null) {
					user = new TeUser ();
				}
				agg.User = user;
				agg.UserId = x.UserId;
				listEx.Add (agg);
			});

			listAc = aggList.LeftJoin<TeUser> ((x, y) => x.UserId == y.Id)
						  .Select ((x, y) => new TeUserLogAgg () {
							  UserId = x.UserId,
							  User = y,
							  LogAgg = x
						  }).ToList ();

			Assert.AreEqual (listEx.Count, listAc.Count);
			AssertExtend.AreObjectEqual (listEx, listAc);

		}
	}
}

