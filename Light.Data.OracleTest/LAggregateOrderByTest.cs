using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.OracleTest
{
	[TestFixture ()]
	public class LAggregateOrderByTest:BaseTest
	{
		[Test ()]
		public void TestCase_OrderBy_Base ()
		{
			InitialUserTable (57);
			List<LevelIdAgg> listAgg;

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByDesc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Greater (listAgg [i - 1].LevelId, listAgg [i].LevelId);
			}
				
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (AggregateFunction.Count ().OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (AggregateFunction.Count ().OrderByDesc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.GreaterOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

			AggregateFunction countFunction = AggregateFunction.Count ();

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (countFunction, "Data")
				.OrderBy (countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (countFunction, "Data")
				.OrderBy (countFunction.OrderByDesc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.GreaterOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}
		}

		[Test ()]
		public void TestCase_OrderBy_Catch ()
		{
			InitialUserTable (57);
			List<LevelIdAgg> listAgg;
			AggregateFunction countFunction = AggregateFunction.Count ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByAsc () & countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByDesc () & countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Greater (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByAsc ())
				.OrderByCatch (countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByDesc ())
				.OrderByCatch (countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.Greater (listAgg [i - 1].LevelId, listAgg [i].LevelId);
				if (listAgg [i - 1].LevelId == listAgg [i].LevelId) {
					Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
				}
			}

			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
				.Aggregate (AggregateFunction.Count (), "Data")
				.OrderBy (TeUser.LevelIdField.OrderByDesc ())
				.OrderBy (countFunction.OrderByAsc ())
				.GetObjectList<LevelIdAgg> ();

			for (int i = 1; i < listAgg.Count; i++) {
				Assert.LessOrEqual (listAgg [i - 1].Data, listAgg [i].Data);
			}

//			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField)
//				.Aggregate (AggregateFunction.Count (), "Data")
//				.OrderBy (TeUser.LevelIdField.OrderByDesc ())
//				.OrderByReset()
//				.GetObjectList<LevelIdAgg> ();
//
//			for (int i = 1; i < listAgg.Count; i++) {
//				Assert.Less (listAgg [i - 1].LevelId, listAgg [i].LevelId);;
//			}

		}
	}
}

