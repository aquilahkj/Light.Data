using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.MysqlTest
{
	[TestFixture ()]
	public class DefaultValueTest:BaseTest
	{
		[Test ()]
		public void TestCase_MiniValue ()
		{
			context.TruncateTable<TeCheckValueMini> ();
			TeCheckValueMini value;
			TeCheckValueMini valueAc;

			value = context.CreateNew<TeCheckValueMini> ();
			value.Save ();
			valueAc = context.SelectSingleFromId<TeCheckValueMini> (value.Id);

			Assert.AreEqual (0, valueAc.CheckId);
			Assert.AreEqual (0, valueAc.CheckRate);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckTime);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckDate);
			Assert.AreEqual ("", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.Low, valueAc.CheckLevel);
		}

		[Test ()]
		public void TestCase_DefauleValue ()
		{
			context.TruncateTable<TeCheckValueDefault> ();
			TeCheckValueDefault value;
			TeCheckValueDefault valueAc;

			value = context.CreateNew<TeCheckValueDefault> ();
			value.Save ();
			valueAc = context.SelectSingleFromId<TeCheckValueDefault> (value.Id);

			Assert.AreEqual (2, valueAc.CheckId);
			Assert.AreEqual (0.02, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("test", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);
		}

		[Test ()]
		public void TestCase_DefauleValue2 ()
		{
			context.TruncateTable<TeCheckValueDefault2> ();
			TeCheckValueDefault2 value;
			TeCheckValueDefault2 valueAc;

			value = context.CreateNew<TeCheckValueDefault2> ();
			value.Save ();
			valueAc = context.SelectSingleFromId<TeCheckValueDefault2> (value.Id);

			Assert.AreEqual (2, valueAc.CheckId);
			Assert.AreEqual (0.02, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("test", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);
		}
	
		[Test ()]
		public void TestCase_MiniValue_Bulk ()
		{
			context.TruncateTable<TeCheckValueMini> ();
			const int count = 20;
			List<TeCheckValueMini> list = new List<TeCheckValueMini> ();
			for (int i = 0; i < count; i++) {
				TeCheckValueMini value = context.CreateNew<TeCheckValueMini> ();
				list.Add (value);
			}
			context.BulkInsert (list.ToArray ());
			List<TeCheckValueMini> listAc = context.LQuery<TeCheckValueMini> ().ToList ();
			Assert.AreEqual (list.Count, listAc.Count);
			foreach (TeCheckValueMini valueAc in listAc) {
				Assert.AreEqual (0, valueAc.CheckId);
				Assert.AreEqual (0, valueAc.CheckRate);
				Assert.AreEqual (DateTime.MinValue, valueAc.CheckTime);
				Assert.AreEqual (DateTime.MinValue, valueAc.CheckDate);
				Assert.AreEqual ("", valueAc.CheckData);
				Assert.AreEqual (CheckLevelType.Low, valueAc.CheckLevel);
			}
		}

		[Test ()]
		public void TestCase_DefauleValue_Bulk ()
		{
			context.TruncateTable<TeCheckValueDefault> ();
			const int count = 20;
			List<TeCheckValueDefault> list = new List<TeCheckValueDefault> ();
			for (int i = 0; i < count; i++) {
				TeCheckValueDefault value = context.CreateNew<TeCheckValueDefault> ();
				list.Add (value);
			}
			context.BulkInsert (list.ToArray ());
			List<TeCheckValueDefault> listAc = context.LQuery<TeCheckValueDefault> ().ToList ();
			Assert.AreEqual (list.Count, listAc.Count);
			foreach (TeCheckValueDefault valueAc in listAc) {
				Assert.AreEqual (2, valueAc.CheckId);
				Assert.AreEqual (0.02, valueAc.CheckRate);
				Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
				Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
				Assert.AreEqual ("test", valueAc.CheckData);
				Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);
			}
		}

		[Test ()]
		public void TestCase_DefauleValue2_Bulk ()
		{
			context.TruncateTable<TeCheckValueDefault2> ();
			const int count = 20;
			List<TeCheckValueDefault2> list = new List<TeCheckValueDefault2> ();
			for (int i = 0; i < count; i++) {
				TeCheckValueDefault2 value = context.CreateNew<TeCheckValueDefault2> ();
				list.Add (value);
			}
			context.BulkInsert (list.ToArray ());
			List<TeCheckValueDefault2> listAc = context.LQuery<TeCheckValueDefault2> ().ToList ();
			Assert.AreEqual (list.Count, listAc.Count);
			foreach (TeCheckValueDefault2 valueAc in listAc) {
				Assert.AreEqual (2, valueAc.CheckId);
				Assert.AreEqual (0.02, valueAc.CheckRate);
				Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime).TotalSeconds, 1);
				Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
				Assert.AreEqual ("test", valueAc.CheckData);
				Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);
			}
		}
	}
}

