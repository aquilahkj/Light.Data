using System;
using NUnit.Framework;
using System.Collections.Generic;
using Light.Data;

namespace Light.Data.SQLiteTest
{
	[TestFixture ()]
	public class ConfigMappingTest:BaseTest
	{	
		const double DEMO_DOUBLE = 0.02f;
		
//		[Test ()]
//		public void TestCase_ConfigCombine ()
//		{
//			LightDataConfig dataConfig = new LightDataConfig ();
//			DataTableConfig dt1 = null;
//			DataTableConfig dt2 = null;
//			DataTableConfig dtl = null;
//
//			Configurator systemConfiguator = Configurator.LoadConfiguratorFromSystemConfig ();
//			if (systemConfiguator != null) {
//				LightDataConfig config = systemConfiguator.CreateConfig<LightDataConfig> ();
//				dt1 = config.GetDataTableConfig (typeof(TeCheckValueConfigS1));
//				dataConfig.CombineConfig (config);
//			}
//
//			Configurator defaultFileConfiguator = Configurator.LoadConfiguratorFromFile ("lightdata.config");
//			if (defaultFileConfiguator != null) {
//				LightDataConfig config = defaultFileConfiguator.CreateConfig<LightDataConfig> ();
//				dt2 = config.GetDataTableConfig (typeof(TeCheckValueConfigS1));
//				dataConfig.CombineConfig (config);
//			}
//			dtl = dataConfig.GetDataTableConfig (typeof(TeCheckValueConfigS1));
//			Assert.AreNotEqual (dt1, dtl);
//			Assert.AreEqual (dt2, dtl);
//			Assert.AreEqual ("config2", dtl.ExtendParams);
//		}
//
//		[Test ()]
//		public void TestCase_ConfigCombine_Agg ()
//		{
//			LightDataConfig dataConfig = new LightDataConfig ();		
//
//			AggregateTableConfig at1 = null;
//			AggregateTableConfig at2 = null;
//			AggregateTableConfig atl = null;
//			Configurator systemConfiguator = Configurator.LoadConfiguratorFromSystemConfig ();
//			if (systemConfiguator != null) {
//				LightDataConfig config = systemConfiguator.CreateConfig<LightDataConfig> ();
//				at1 = config.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
//				dataConfig.CombineConfig (config);
//			}
//
//			Configurator defaultFileConfiguator = Configurator.LoadConfiguratorFromFile ("lightdata.config");
//			if (defaultFileConfiguator != null) {
//				LightDataConfig config = defaultFileConfiguator.CreateConfig<LightDataConfig> ();
//				at2 = config.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
//				dataConfig.CombineConfig (config);
//			}
//			atl = dataConfig.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
//			Assert.AreNotEqual (at1, atl);
//			Assert.AreEqual (at2, atl);
//			Assert.AreEqual ("config2", atl.ExtendParams);
//		}

		[Test ()]
		public void TestCase_ConfigReplace ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfig));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);

			context.TruncateTable<TeCheckValueConfig> ();
			TeCheckValueConfig value;
			TeCheckValueConfig valueAc;

			value = context.CreateNew<TeCheckValueConfig> ();
			value.Save ();
			valueAc = context.SelectSingleFromId<TeCheckValueConfig> (value.Id);

			Assert.AreEqual (0, valueAc.CheckId);
			Assert.AreEqual (0, valueAc.CheckRate);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckTime);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckDate);
			Assert.AreEqual ("", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.Low, valueAc.CheckLevel);

			value.CheckId = 2;
			value.Save ();
			valueAc = context.SelectSingleFromId<TeCheckValueConfig> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			value.Erase ();
			valueAc = context.SelectSingleFromId<TeCheckValueConfig> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigReplace_Agg ()
		{
			AggregateTableMapping mapping = AggregateTableMapping.GetAggregateMapping (typeof(LevelIdAggConfig));
			Assert.NotNull (mapping);

			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggConfig> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAggConfig> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggConfig agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_ConfigSystemConfig ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfig0));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);

			context.TruncateTable<TeCheckValueConfig0> ();
			TeCheckValueConfig0 value;
			TeCheckValueConfig0 valueAc;

			value = new TeCheckValueConfig0 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig0> (value.Id);

			Assert.AreEqual (0, valueAc.CheckId);
			Assert.AreEqual (0, valueAc.CheckRate);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckTime);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckDate);
			Assert.AreEqual ("", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.Low, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig0> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig0> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigOnly ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfig1));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);

			context.TruncateTable<TeCheckValueConfig1> ();
			TeCheckValueConfig1 value;
			TeCheckValueConfig1 valueAc;

			value = new TeCheckValueConfig1 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig1> (value.Id);

			Assert.AreEqual (0, valueAc.CheckId);
			Assert.AreEqual (0, valueAc.CheckRate);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckTime);
			Assert.AreEqual (DateTime.MinValue, valueAc.CheckDate);
			Assert.AreEqual ("", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.Low, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig1> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig1> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigDir1 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfigC2));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfigC2> ();
			TeCheckValueConfigC2 value;
			TeCheckValueConfigC2 valueAc;

			value = new TeCheckValueConfigC2 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC2", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigDir1_Agg ()
		{
			AggregateTableMapping mapping = AggregateTableMapping.GetAggregateMapping (typeof(LevelIdAggConfig1));
			Assert.NotNull (mapping);

			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggConfig1> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAggConfig1> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggConfig1 agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}
			
		[Test ()]
		public void TestCase_ConfigDir2 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfigC2));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfigC2> ();
			TeCheckValueConfigC2 value;
			TeCheckValueConfigC2 valueAc;

			value = new TeCheckValueConfigC2 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC2", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC2> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigDir3 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfigC3));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfigC3> ();
			TeCheckValueConfigC3 value;
			TeCheckValueConfigC3 valueAc;

			value = new TeCheckValueConfigC3 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC3> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC3", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC3> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC3> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigDir4 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfigC4));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfigC4> ();
			TeCheckValueConfigC4 value;
			TeCheckValueConfigC4 valueAc;

			value = new TeCheckValueConfigC4 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC4> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC4", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC4> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigC4> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigAssembly1 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfigA1));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfigA1> ();
			TeCheckValueConfigA1 value;
			TeCheckValueConfigA1 valueAc;

			value = new TeCheckValueConfigA1 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigA1> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigA1", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigA1> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfigA1> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigAssembly1_Agg ()
		{
			AggregateTableMapping mapping = AggregateTableMapping.GetAggregateMapping (typeof(LevelIdAggConfigA1));
			Assert.NotNull (mapping);

			InitialUserTable (57);
			List<TeUser> list;
			List<LevelIdAggConfigA1> listAgg;
			Dictionary<int,int> dict;

			list = context.LQuery<TeUser> ().ToList ();
			listAgg = context.LAggregate<TeUser> ().GroupBy (TeUser.LevelIdField).Aggregate (AggregateFunction.Count (), "Data").GetObjectList<LevelIdAggConfigA1> ();
			dict = new Dictionary<int, int> ();

			foreach (TeUser user in list) {
				int i;
				dict.TryGetValue (user.LevelId, out i);
				dict [user.LevelId] = i + 1;
			}
			Assert.AreEqual (dict.Count, listAgg.Count);
			foreach (LevelIdAggConfigA1 agg in listAgg) {
				int i;
				Assert.IsTrue (dict.TryGetValue (agg.LevelId, out i));
				Assert.AreEqual (i, agg.Data);
			}
		}

		[Test ()]
		public void TestCase_ConfigDefault ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfig2));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfig2> ();
			TeCheckValueConfig2 value;
			TeCheckValueConfig2 valueAc;

			value = new TeCheckValueConfig2 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig2> (value.Id);

			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.LessOrEqual ((DateTime.Now - valueAc.CheckTime.Value).TotalSeconds, 1);
			Assert.AreEqual (DateTime.Now.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC1", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig2> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig2> (value.Id);
			Assert.IsNull (valueAc);
		}

		[Test ()]
		public void TestCase_ConfigDefault2 ()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(TeCheckValueConfig3));
			Assert.NotNull (mapping);
			Assert.AreEqual ("Te_CheckValue", mapping.TableName);


			context.TruncateTable<TeCheckValueConfig3> ();
			TeCheckValueConfig3 value;
			TeCheckValueConfig3 valueAc;

			value = new TeCheckValueConfig3 ();
			context.Insert (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig3> (value.Id);
			DateTime dt = new DateTime (2016, 1, 19, 22, 10, 10);
			Assert.AreEqual (1, valueAc.CheckId);
			Assert.AreEqual (DEMO_DOUBLE, valueAc.CheckRate);
			Assert.AreEqual (dt, valueAc.CheckTime);
			Assert.AreEqual (dt.Date, valueAc.CheckDate);
			Assert.AreEqual ("TeCheckValueConfigC2", valueAc.CheckData);
			Assert.AreEqual (CheckLevelType.High, valueAc.CheckLevel);

			value.CheckId = 2;
			context.Update (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig3> (value.Id);
			Assert.AreEqual (value.CheckId, valueAc.CheckId);

			context.Delete (value);
			valueAc = context.SelectSingleFromId<TeCheckValueConfig3> (value.Id);
			Assert.IsNull (valueAc);
		}
	}
}

