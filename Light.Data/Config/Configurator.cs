using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Handler;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Configuration;

namespace Light.Data.Config
{
	class Configurator
	{
		public static Configurator LoadConfiguratorFromSystemConfig (string sectionName)
		{
			if (string.IsNullOrEmpty (sectionName)) {
				throw new ArgumentNullException ("SectionName");
			}
			object obj = ConfigurationManager.GetSection (sectionName);
			XmlNode node = obj as XmlNode;
			if (node != null) {
				return LoadConfiguratorFromXml (node);
			}
			else {
				return null;
			}
		}

		public static Configurator LoadConfiguratorFromAssembly (Assembly assembly, string sectionName)
		{
			if (assembly == null) {
				throw new ArgumentNullException ("Assembly");
			}
			Configurator configFile = null;
			XmlConfiguratorAttribute[] attribute = AttributeCore.GetAssemblyAttributes<XmlConfiguratorAttribute> (assembly, true);
			if (attribute.Length > 0) {
				if (!string.IsNullOrEmpty (attribute [0].ConfigPath)) {
					configFile = LoadConfiguratorFromFile (attribute [0].ConfigPath, sectionName);
				}
			}
			return configFile;
		}

		public static Configurator LoadConfiguratorFromFile (string fileName, string sectionName)
		{
			if (string.IsNullOrEmpty (fileName)) {
				throw new ArgumentNullException ("FileName");
			}
			Configurator configFile = null;
			string fullPath2ConfigFile = null;
			string applicationBaseDirectory = null;
			try {
				applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			}
			catch {

			}
			if (applicationBaseDirectory != null) {
				fullPath2ConfigFile = Path.Combine (applicationBaseDirectory, fileName);
			}
			else {
				fullPath2ConfigFile = fileName;
			}

			if (!string.IsNullOrEmpty (fullPath2ConfigFile)) {
				configFile = LoadConfiguratorFromFile (new FileInfo (fullPath2ConfigFile), sectionName);
			}
			return configFile;
		}

		public static Configurator LoadConfiguratorFromFile (FileInfo fileInfo, string sectionName)
		{
			if (fileInfo == null) {
				throw new ArgumentNullException ("FileInfo");
			}
			Configurator configFile = null;
			if (fileInfo.Exists) {
				FileStream fs = null;

				fs = fileInfo.Open (FileMode.Open, FileAccess.Read, FileShare.Read);

				if (fs != null) {
					try {
						// Load the configuration from the stream
						configFile = LoadConfiguratorFromStream (fs, sectionName);
					}
					finally {
						// Force the file closed whatever happens
						fs.Close ();
					}
				}
			}
			return configFile;
		}

		public static Configurator LoadConfiguratorFromStream (Stream configStream, string sectionName)
		{
			if (configStream == null) {
				throw new ArgumentNullException ("Stream");
			}
			if (string.IsNullOrEmpty (sectionName)) {
				throw new ArgumentNullException ("SectionName");
			}
			Configurator configFile = null;
			XmlDocument doc = new XmlDocument ();

			XmlReaderSettings setting = new XmlReaderSettings ();
			setting.ValidationType = ValidationType.None;
			XmlReader xmlReader = XmlReader.Create (configStream, setting);

			doc.Load (xmlReader);
			XmlNodeList configNodeList = doc.GetElementsByTagName (sectionName);
			if (configNodeList.Count == 1) {
				configFile = LoadConfiguratorFromXml (configNodeList [0] as XmlElement);
			}

			return configFile;
		}

		public static Configurator LoadConfiguratorFromXml (XmlNode configNode)
		{
			if (configNode == null) {
				throw new ArgumentNullException ("ConfigNode");
			}
			Configurator configFile = new Configurator (configNode);
			return configFile;
		}

		XmlNode _node = null;

		private Configurator (XmlNode node)
		{
			_node = node;
		}

		public T CreateConfig<T> () where T : IConfig, new()
		{
			T config = default(T);
			config = new T ();
			config.LoadConfig (_node);
			return config;
		}

		//Dictionary<Type, DataTableConfig> _dataTableConfigs = null;

		//Dictionary<Type, AggregateTableConfig> _aggregateTableConfig = null;


	}

	interface IConfig
	{
		void LoadConfig (XmlNode configNode);
	}
}
