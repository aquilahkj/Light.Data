using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class TT_AggregateOrderByTest : BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Base ()
		{
			InitialUserTable (57);
			List<LevelIdAgg> listAgg;

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderBy (x => x.LevelId).ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderByDescending (x => x.LevelId).ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Greater (listAgg [i - 1].LevelId, listAgg [i].LevelId);
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderBy (x => x.Data).ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderByDescending (x => x.Data).ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.GreaterOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

		}

		[Test ()]
		public void TestCase_OrderBy_Catch ()
		{
			InitialUserTable (57);
			List<LevelIdAgg> listAgg;

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderBy (x => x.LevelId)
			.OrderByCatch (x => x.Data)
			.ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderByDescending (x => x.LevelId)
			.OrderByCatch (x => x.Data)
			.ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Greater (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderByDescending (x => x.LevelId)
			.OrderBy (x => x.Data)
			.ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

			listAgg = context.Query<TeUser> ().GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			}).OrderByDescending (x => x.LevelId)
			.OrderByReset()
			.ToList ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId); ;
			}

		}
	}
}

