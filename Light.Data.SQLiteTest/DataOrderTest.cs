using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class DataOrderTest
	{
		[Test ()]
		public void TestCase_Order1 ()
		{
			Assert.AreEqual (1, TeCheckValueOrder1.IdField.Position);
			Assert.AreEqual (2, TeCheckValueOrder1.CheckIdField.Position);
			Assert.AreEqual (3, TeCheckValueOrder1.CheckRateField.Position);
			Assert.AreEqual (4, TeCheckValueOrder1.CheckTimeField.Position);
			Assert.AreEqual (5, TeCheckValueOrder1.CheckDateField.Position);
			Assert.AreEqual (6, TeCheckValueOrder1.CheckDataField.Position);
			Assert.AreEqual (7, TeCheckValueOrder1.CheckLevelField.Position);
		}

		[Test ()]
		public void TestCase_Order2 ()
		{
			Assert.AreEqual (7, TeCheckValueOrder2.IdField.Position);
			Assert.AreEqual (6, TeCheckValueOrder2.CheckIdField.Position);
			Assert.AreEqual (5, TeCheckValueOrder2.CheckRateField.Position);
			Assert.AreEqual (4, TeCheckValueOrder2.CheckTimeField.Position);
			Assert.AreEqual (3, TeCheckValueOrder2.CheckDateField.Position);
			Assert.AreEqual (2, TeCheckValueOrder2.CheckDataField.Position);
			Assert.AreEqual (1, TeCheckValueOrder2.CheckLevelField.Position);
		}

		[Test ()]
		public void TestCase_Order3 ()
		{
			Assert.AreEqual (5, TeCheckValueOrder3.IdField.Position);
			Assert.AreEqual (6, TeCheckValueOrder3.CheckIdField.Position);
			Assert.AreEqual (7, TeCheckValueOrder3.CheckRateField.Position);
			Assert.AreEqual (1, TeCheckValueOrder3.CheckTimeField.Position);
			Assert.AreEqual (2, TeCheckValueOrder3.CheckDateField.Position);
			Assert.AreEqual (3, TeCheckValueOrder3.CheckDataField.Position);
			Assert.AreEqual (4, TeCheckValueOrder3.CheckLevelField.Position);
		}

		[Test ()]
		public void TestCase_Order4 ()
		{
			Assert.AreEqual (4, TeCheckValueOrder4.IdField.Position);
			Assert.AreEqual (5, TeCheckValueOrder4.CheckIdField.Position);
			Assert.AreEqual (3, TeCheckValueOrder4.CheckRateField.Position);
			Assert.AreEqual (2, TeCheckValueOrder4.CheckTimeField.Position);
			Assert.AreEqual (1, TeCheckValueOrder4.CheckDateField.Position);
			Assert.AreEqual (6, TeCheckValueOrder4.CheckDataField.Position);
			Assert.AreEqual (7, TeCheckValueOrder4.CheckLevelField.Position);
		}
	}
}

