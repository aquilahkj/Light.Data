using System;
using NUnit.Framework;

namespace Light.Data.UnitTest
{
	[TestFixture ()]
	public class ConfigMappingTest
	{
		[Test ()]
		public void TestCase_ConfigCombine ()
		{
			LightDataConfig dataConfig = new LightDataConfig ();
			DataTableConfig dt1 = null;
			DataTableConfig dt2 = null;
			DataTableConfig dtl = null;

			Configurator systemConfiguator = Configurator.LoadConfiguratorFromSystemConfig ();
			if (systemConfiguator != null) {
				LightDataConfig config = systemConfiguator.CreateConfig<LightDataConfig> ();
				dt1 = config.GetDataTableConfig (typeof(TeCheckValueConfigS1));
				dataConfig.CombineConfig (config);
			}

			Configurator defaultFileConfiguator = Configurator.LoadConfiguratorFromFile ("lightdata.config");
			if (defaultFileConfiguator != null) {
				LightDataConfig config = defaultFileConfiguator.CreateConfig<LightDataConfig> ();
				dt2 = config.GetDataTableConfig (typeof(TeCheckValueConfigS1));
				dataConfig.CombineConfig (config);
			}
			dtl = dataConfig.GetDataTableConfig (typeof(TeCheckValueConfigS1));
			Assert.AreNotEqual (dt1, dtl);
			Assert.AreEqual (dt2, dtl);
			Assert.AreEqual (2, dt1.ExtendParams.Count);
			Assert.AreEqual ("true", dt1.ExtendParams["config1"]);
			Assert.AreEqual ("false", dt1.ExtendParams["config2"]);
		}

		[Test ()]
		public void TestCase_ConfigCombine_Agg ()
		{
			LightDataConfig dataConfig = new LightDataConfig ();		

			AggregateTableConfig at1 = null;
			AggregateTableConfig at2 = null;
			AggregateTableConfig atl = null;
			Configurator systemConfiguator = Configurator.LoadConfiguratorFromSystemConfig ();
			if (systemConfiguator != null) {
				LightDataConfig config = systemConfiguator.CreateConfig<LightDataConfig> ();
				at1 = config.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
				dataConfig.CombineConfig (config);
			}

			Configurator defaultFileConfiguator = Configurator.LoadConfiguratorFromFile ("lightdata.config");
			if (defaultFileConfiguator != null) {
				LightDataConfig config = defaultFileConfiguator.CreateConfig<LightDataConfig> ();
				at2 = config.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
				dataConfig.CombineConfig (config);
			}
			atl = dataConfig.GetAggregateTableConfig (typeof(LevelIdAggConfig0));
			Assert.AreNotEqual (at1, atl);
			Assert.AreEqual (at2, atl);
			Assert.AreEqual (2, at1.ExtendParams.Count);
			Assert.AreEqual ("true", at1.ExtendParams["config1"]);
			Assert.AreEqual ("false", at1.ExtendParams["config2"]);
		}

	}
}

