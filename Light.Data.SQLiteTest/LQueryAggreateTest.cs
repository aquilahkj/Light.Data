using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class LQueryAggreateTest:BaseTest
	{
		[Test ()]
		public void TestCase_Exists ()
		{
			InitialUserTable (21);
			bool result1 = context.LQuery<TeUser> ().Exists;
			Assert.IsTrue (result1);

			bool result2 = context.LQuery<TeUser> ().Where (TeUser.IdField > 1).Exists;
			Assert.IsTrue (result2);

			bool result3 = context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Exists;
			Assert.IsFalse (result3);

			bool result4 = context.LQuery<TeUser> ().Where (TeUser.IdField > 1 & TeUser.CheckPointField.IsNotNull ()).Exists;
			Assert.IsTrue (result4);

			bool result5 = context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Exists;
			Assert.IsFalse (result5);
		}

		[Test ()]
		public void TestCase_Count ()
		{
			InitialUserTable (21);
			int result1 = context.LQuery<TeUser> ().Count;
			Assert.AreEqual (21, result1);

			int result2 = context.LQuery<TeUser> ().Where (TeUser.IdField > 1).Count;
			Assert.AreEqual (20, result2);

			int result3 = context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Count;
			Assert.AreEqual (0, result3);

			int result4 = context.LQuery<TeUser> ().Where (TeUser.IdField > 1 & TeUser.CheckPointField.IsNotNull ()).Count;
			Assert.AreEqual (10, result4);

			int result5 = context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Count;
			Assert.AreEqual (0, result5);
		}

		[Test ()]
		public void TestCase_Count_Field ()
		{
			InitialUserTable (21);
			int result1 = context.LQuery<TeUser> ().CountField (TeUser.LevelIdField);
			Assert.AreEqual (21, result1);

			int result2 = context.LQuery<TeUser> ().CountField (TeUser.LevelIdField, true);
			Assert.AreEqual (10, result2);

			int result3 = context.LQuery<TeUser> ().Where (TeUser.IdField.Between (5, 12)).CountField (TeUser.LevelIdField);
			Assert.AreEqual (8, result3);

			int result4 = context.LQuery<TeUser> ().CountField (TeUser.RefereeIdField);
			Assert.AreEqual (10, result4);

			int result5 = context.LQuery<TeUser> ().CountField (TeUser.RefereeIdField, true);
			Assert.AreEqual (3, result5);
		}

		[Test ()]
		public void TestCase_Avg ()
		{
			InitialUserTable (21);
			double sum1 = CalAvg (1, 21, 10);
			double result1 = Convert.ToDouble (context.LQuery<TeUser> ().Avg (TeUser.LevelIdField));
			Assert.AreEqual (FormatDouble (sum1), FormatDouble (result1));

			double sum1d = CalAvg (1, 21, 10, true);
			double result1d = Convert.ToDouble (context.LQuery<TeUser> ().Avg (TeUser.LevelIdField, true));
			Assert.AreEqual (FormatDouble (sum1d), FormatDouble (result1d));

			double sum2 = CalAvg (6, 18, 10);
			double result2 = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField.Between (6, 18)).Avg (TeUser.LevelIdField));
			Assert.AreEqual (FormatDouble (sum2), FormatDouble (result2));

			double sum2d = CalAvg (6, 18, 10, true);
			double result2d = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField.Between (6, 18)).Avg (TeUser.LevelIdField, true));
			Assert.AreEqual (FormatDouble (sum2d), FormatDouble (result2d));

			double result3 = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Avg (TeUser.LevelIdField));
			Assert.AreEqual (0d, result3);

			double result3d = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Avg (TeUser.LevelIdField, true));
			Assert.AreEqual (0d, result3d);

			double sum4 = CalAvg (7, 16, 10);
			double result4 = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField >= 7 & TeUser.IdField <= 16).Avg (TeUser.LevelIdField));
			Assert.AreEqual (FormatDouble (sum4), FormatDouble (result4));

			double sum4d = CalAvg (7, 16, 10, true);
			double result4d = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField >= 7 & TeUser.IdField <= 16).Avg (TeUser.LevelIdField, true));
			Assert.AreEqual (FormatDouble (sum4d), FormatDouble (result4d));

			double result5 = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Avg (TeUser.LevelIdField));
			Assert.AreEqual (0d, result5);

			double result5d = Convert.ToDouble (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Avg (TeUser.LevelIdField, true));
			Assert.AreEqual (0d, result5d);

		}

		[Test ()]
		public void TestCase_Sum ()
		{
			InitialUserTable (21);
			int sum1 = CalSum (1, 21, 10);
			int result1 = Convert.ToInt32 (context.LQuery<TeUser> ().Sum (TeUser.LevelIdField));
			Assert.AreEqual (sum1, result1);

			int sum1d = CalSum (1, 21, 10, true);
			int result1d = Convert.ToInt32 (context.LQuery<TeUser> ().Sum (TeUser.LevelIdField, true));
			Assert.AreEqual (sum1d, result1d);

			int sum2 = CalSum (2, 21, 10);
			int result2 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 1).Sum (TeUser.LevelIdField));
			Assert.AreEqual (sum2, result2);

			int sum2d = CalSum (2, 21, 10, true);
			int result2d = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 1).Sum (TeUser.LevelIdField, true));
			Assert.AreEqual (sum2d, result2d);

			int result3 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Sum (TeUser.LevelIdField));
			Assert.AreEqual (0, result3);

			int result3d = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Sum (TeUser.LevelIdField, true));
			Assert.AreEqual (0, result3d);

			int sum4 = CalSum (7, 16, 10);
			int result4 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField >= 7 & TeUser.IdField <= 16).Sum (TeUser.LevelIdField));
			Assert.AreEqual (sum4, result4);

			int sum4d = CalSum (7, 16, 10, true);
			int result4d = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField >= 7 & TeUser.IdField <= 16).Sum (TeUser.LevelIdField, true));
			Assert.AreEqual (sum4d, result4d);

			int result5 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Sum (TeUser.LevelIdField));
			Assert.AreEqual (0, result5);

			int result5d = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Sum (TeUser.LevelIdField, true));
			Assert.AreEqual (0, result5d);

		}

		[Test ()]
		public void TestCase_Max ()
		{
			InitialUserTable (20);
			int result1 = Convert.ToInt32 (context.LQuery<TeUser> ().Max (TeUser.IdField));
			Assert.AreEqual (20, result1);

			int result2 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField < 15).Max (TeUser.IdField));
			Assert.AreEqual (14, result2);

			int result3 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 21).Max (TeUser.IdField));
			Assert.AreEqual (0, result3);

			int result4 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField < 15 & TeUser.CheckPointField.IsNull ()).Max (TeUser.IdField));
			Assert.AreEqual (13, result4);

			int result5 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Max (TeUser.IdField));
			Assert.AreEqual (0, result5);

		}

		[Test ()]
		public void TestCase_Min ()
		{
			InitialUserTable (20);
			int result1 = Convert.ToInt32 (context.LQuery<TeUser> ().Min (TeUser.IdField));
			Assert.AreEqual (1, result1);

			int result2 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 3).Min (TeUser.IdField));
			Assert.AreEqual (4, result2);

			int result3 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField < 1).Min (TeUser.IdField));
			Assert.AreEqual (0, result3);

			int result4 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField > 3 & TeUser.CheckPointField.IsNull ()).Min (TeUser.IdField));
			Assert.AreEqual (5, result4);

			int result5 = Convert.ToInt32 (context.LQuery<TeUser> ().Where (TeUser.IdField == 1 & TeUser.CheckPointField.IsNotNull ()).Min (TeUser.IdField));
			Assert.AreEqual (0, result5);

		}


		static int CalSum (int start, int end, int max, bool distinct = false)
		{
			int result = 0;
			HashSet<int> hash = new HashSet<int> ();
			for (int i = start; i <= end; i++) {
				int t = i % max;
				if (t == 0) {
					t = max;
				}
				if (distinct) {
					if (hash.Contains (t)) {
						continue;
					}
					else {
						hash.Add (t);
					}
				}
				result += t;
			}
			return result;
		}

		static double CalAvg (int start, int end, int max, bool distinct = false)
		{
			int result = 0;
			HashSet<int> hash = new HashSet<int> ();
			for (int i = start; i <= end; i++) {
				int t = i % max;
				if (t == 0) {
					t = max;
				}
				if (distinct) {
					if (hash.Contains (t)) {
						continue;
					}
					else {
						hash.Add (t);
					}
				}
				result += t;
			}
			if (distinct) {
				return (double)result / hash.Count;
			}
			else {
				return (double)result / (end - start + 1);
			}
		}



	}
}

