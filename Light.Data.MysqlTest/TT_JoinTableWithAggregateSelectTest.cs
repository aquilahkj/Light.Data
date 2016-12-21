using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data.UnitTest;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_JoinTableWithAggregateSelectTest : BaseTest
	{
		[Test ()]
		public void TestCase_Join_With_AggregateSelect1 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeDataLog> listLog = InitialDataLogTable (50);

			List<TeUserSimpleLogAgg> listEx;
			List<TeUserSimpleLogAgg> listAc;

			List<TeUserSimple> lists = new List<TeUserSimple> ();
			list.ForEach (x => {
				TeUserSimple ts = new TeUserSimple () {
					Id = x.Id,
					Account = x.Account,
					LevelId = x.LevelId,
					RegTime = x.RegTime
				};
				lists.Add (ts);
			});

			Dictionary<int, TeDataLogAgg> dict = new Dictionary<int, TeDataLogAgg> ();

			var aggList = context.Query<TeDataLog> ()
								 .Where (x => x.UserId > 5).GroupBy (x => new TeDataLogAgg {
									 UserId = x.UserId,
									 Count = Function.Count (),
									 LastTime = Function.Max (x.RecordTime),
								 });
			var selectList = context.Query<TeUser> ().Select (x => new TeUserSimple {
				Id = x.Id,
				Account = x.Account,
				LevelId = x.LevelId,
				RegTime = x.RegTime
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
			listEx = new List<TeUserSimpleLogAgg> ();
			lists.ForEach (x => {
				TeUserSimpleLogAgg agg = new TeUserSimpleLogAgg ();
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

			listAc = selectList.LeftJoin (aggList, (x, y) => x.Id == y.UserId)
							.Select ((x, y) => new TeUserSimpleLogAgg () {
								UserId = x.Id,
								User = x,
								LogAgg = y
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			AssertExtend.AreObjectEqual (listEx, listAc);
		}

		[Test ()]
		public void TestCase_Join_With_AggregateSelect2 ()
		{
			List<TeUser> list = InitialUserTable (21);
			List<TeDataLog> listLog = InitialDataLogTable (50);

			List<TeUserSimpleLogAgg> listEx;
			List<TeUserSimpleLogAgg> listAc;

			List<TeUserSimple> lists = new List<TeUserSimple> ();
			list.ForEach (x => {
				TeUserSimple ts = new TeUserSimple () {
					Id = x.Id,
					Account = x.Account,
					LevelId = x.LevelId,
					RegTime = x.RegTime
				};
				lists.Add (ts);
			});

			Dictionary<int, TeDataLogAgg> dict = new Dictionary<int, TeDataLogAgg> ();

			var aggList = context.Query<TeDataLog> ()
								 .Where (x => x.UserId > 5).GroupBy (x => new TeDataLogAgg {
									 UserId = x.UserId,
									 Count = Function.Count (),
									 LastTime = Function.Max (x.RecordTime),
								 });
			var selectList = context.Query<TeUser> ().Select (x => new TeUserSimple {
				Id = x.Id,
				Account = x.Account,
				LevelId = x.LevelId,
				RegTime = x.RegTime
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

			List<TeDataLogAgg> listTmp = new List<TeDataLogAgg> (dict.Values);
			listEx = new List<TeUserSimpleLogAgg> ();
			listTmp.ForEach (x => {
				TeUserSimpleLogAgg agg = new TeUserSimpleLogAgg ();
				agg.LogAgg = x;
				TeUserSimple user = lists.Find (y => y.Id == x.UserId);
				if (user == null) {
					user = new TeUserSimple ();
				}
				agg.User = user;
				agg.UserId = x.UserId;
				listEx.Add (agg);
			});

			listAc = aggList.LeftJoin (selectList, (x, y) => x.UserId == y.Id)
							.Select ((x, y) => new TeUserSimpleLogAgg () {
								UserId = y.Id,
								User = y,
								LogAgg = x
							}).ToList ();
			Assert.AreEqual (listEx.Count, listAc.Count);
			AssertExtend.AreObjectEqual (listEx, listAc);

		}
	}
}

