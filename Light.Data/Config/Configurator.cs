using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Configuration;

namespace Light.Data
{
	class Configurator
	{
		static readonly string SECTION_NAME = "lightDataConfig";

		public static Configurator LoadConfiguratorFromSystemConfig ()
		{
			object obj = ConfigurationManager.GetSection (SECTION_NAME);
			XmlNode node = obj as XmlNode;
			if (node != null) {
				return LoadConfiguratorFromXml (node);
			}
			else {
				return null;
			}
		}

		public static Configurator[] LoadConfiguratorFromAppSetting ()
		{
			Configurator[] configFiles = null;
			string configPath = null;
			if (ConfigurationManager.AppSettings ["liaghDataConfig"] != null) {
				configPath = ConfigurationManager.AppSettings ["liaghDataConfig"];
				configFiles = LoadConfiguratorFromPath (configPath);
			}
			return configFiles;
		}

		public static Configurator[] LoadConfiguratorFromPath (string configPath)
		{
			Configurator[] configFiles = null;
			if (!string.IsNullOrEmpty (configPath)) {
				if (configPath.EndsWith (".config")) {
					Configurator configFile = LoadConfiguratorFromFile (configPath);
					configFiles = new []{ configFile };
				}
				else {
					configFiles = LoadConfiguratorFromConfigFileDir (configPath);
				}
			}
			return configFiles;
		}

		public static Configurator[] LoadConfiguratorFromAssembly ()
		{
			Assembly assembly = null;
			assembly = Assembly.GetEntryAssembly ();
			if (assembly == null) {
				assembly = Assembly.GetExecutingAssembly ();
			}
			return LoadConfiguratorFromAssembly (assembly);
		}

		public static Configurator[] LoadConfiguratorFromAssembly (Assembly assembly)
		{
			if (assembly == null) {
				return null;
			}
			Configurator[] configFiles = null;
			XmlConfiguratorAttribute[] attribute = AttributeCore.GetAssemblyAttributes<XmlConfiguratorAttribute> (assembly, true);
			if (attribute.Length > 0) {
				string configPath = attribute [0].ConfigPath;
				configFiles = LoadConfiguratorFromPath (configPath);
			}
			return configFiles;
		}


		public static Configurator[] LoadConfiguratorFromConfigFileDir (string directory)
		{
			if (string.IsNullOrEmpty (directory)) {
				throw new ArgumentNullException ("directory");
			}
			List<Configurator> list = new List<Configurator> ();
			string fullPathConfigDirectory;
			string applicationBaseDirectory = null;
			try {
				applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			} catch {
			}
			if (applicationBaseDirectory != null) {
				fullPathConfigDirectory = Path.Combine (applicationBaseDirectory, directory);
			}
			else {
				fullPathConfigDirectory = directory;
			}
			DirectoryInfo dir = new DirectoryInfo (fullPathConfigDirectory);
			if (dir.Exists) {
				FileInfo[] infos = dir.GetFiles ();
				foreach (FileInfo info in infos) {
					if (info.Extension == ".config") {
						try {
							Configurator config = LoadConfiguratorFromFile (info);
							if (config != null) {
								list.Add (config);
							}
						} catch {
								
						}
					}
				}

			}
			return list.ToArray ();
		}

		public static Configurator LoadConfiguratorFromFile (string fileName)
		{
			if (string.IsNullOrEmpty (fileName)) {
				throw new ArgumentNullException ("fileName");
			}
			Configurator configFile;
			string fullPathConfigFile;
			string applicationBaseDirectory = null;
			try {
				applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			} catch {
			}
			if (applicationBaseDirectory != null) {
				fullPathConfigFile = Path.Combine (applicationBaseDirectory, fileName);
			}
			else {
				fullPathConfigFile = fileName;
			}

			configFile = LoadConfiguratorFromFile (new FileInfo (fullPathConfigFile));
			return configFile;
		}

		public static Configurator LoadConfiguratorFromFile (FileInfo fileInfo)
		{
			if (fileInfo == null) {
				throw new ArgumentNullException ("fileInfo");
			}
			Configurator configFile = null;
			if (fileInfo.Exists) {
				FileStream fs;

				fs = fileInfo.Open (FileMode.Open, FileAccess.Read, FileShare.Read);

				if (fs != null) {
					try {
						// Load the configuration from the stream
						configFile = LoadConfiguratorFromStream (fs);
					} finally {
						// Force the file closed whatever happens
						fs.Close ();
					}
				}
			}
			return configFile;
		}

		public static Configurator LoadConfiguratorFromStream (Stream configStream)
		{
			if (configStream == null) {
				throw new ArgumentNullException ("configStream");
			}
			Configurator configFile = null;
			XmlDocument doc = new XmlDocument ();

			XmlReaderSettings setting = new XmlReaderSettings ();
			setting.ValidationType = ValidationType.None;
			XmlReader xmlReader = XmlReader.Create (configStream, setting);

			doc.Load (xmlReader);
			XmlNodeList configNodeList = doc.GetElementsByTagName (SECTION_NAME);
			if (configNodeList.Count == 1) {
				configFile = LoadConfiguratorFromXml (configNodeList [0]);
			}

			return configFile;
		}

		public static Configurator LoadConfiguratorFromXml (XmlNode configNode)
		{
			if (configNode == null) {
				throw new ArgumentNullException ("configNode");
			}
			Configurator configFile = new Configurator (configNode);
			return configFile;
		}

		readonly XmlNode _node = null;

		private Configurator (XmlNode node)
		{
			_node = node;
		}

		public T CreateConfig<T> () where T : IConfig, new()
		{
			T config = new T ();
			config.LoadConfig (_node);
			return config;
		}
	}

	interface IConfig
	{
		void LoadConfig (XmlNode configNode);
	}
}
