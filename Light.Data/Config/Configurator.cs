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
		public static Configurator LoadConfiguratorFromSystemConfig (string sectionName)
		{
			if (string.IsNullOrEmpty (sectionName)) {
				throw new ArgumentNullException ("sectionName");
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
				throw new ArgumentNullException ("assembly");
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

		public static Configurator[] LoadConfiguratorFromConfigFileDir (string directory, string sectionName)
		{
			if (string.IsNullOrEmpty (directory)) {
				throw new ArgumentNullException ("directory");
			}
			List<Configurator> list = new List<Configurator> ();
			string fullPathConfigDirectory = null;
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
					if (info.Extension == "config") {
						try {
							Configurator config = LoadConfiguratorFromFile (info, sectionName);
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

		public static Configurator LoadConfiguratorFromFile (string fileName, string sectionName)
		{
			if (string.IsNullOrEmpty (fileName)) {
				throw new ArgumentNullException ("fileName");
			}
			Configurator configFile = null;
			string fullPathConfigFile = null;
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

			configFile = LoadConfiguratorFromFile (new FileInfo (fullPathConfigFile), sectionName);
			return configFile;
		}

		public static Configurator LoadConfiguratorFromFile (FileInfo fileInfo, string sectionName)
		{
			if (fileInfo == null) {
				throw new ArgumentNullException ("fileInfo");
			}
			Configurator configFile = null;
			if (fileInfo.Exists) {
				FileStream fs = null;

				fs = fileInfo.Open (FileMode.Open, FileAccess.Read, FileShare.Read);

				if (fs != null) {
					try {
						// Load the configuration from the stream
						configFile = LoadConfiguratorFromStream (fs, sectionName);
					} finally {
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
				throw new ArgumentNullException ("configStream");
			}
			if (string.IsNullOrEmpty (sectionName)) {
				throw new ArgumentNullException ("sectionName");
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
			T config = default(T);
			config = new T ();
			config.LoadConfig (_node);
			return config;
		}
	}

	interface IConfig
	{
		void LoadConfig (XmlNode configNode);
	}
}
