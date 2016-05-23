using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Light.Data
{
	/// <summary>
	/// Configurator.
	/// </summary>
	class Configurator
	{
		static readonly string SECTION_NAME = "lightDataConfig";

		/// <summary>
		/// Loads the configurator from system config.
		/// </summary>
		/// <returns>The configurator from system config.</returns>
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

		/// <summary>
		/// Loads the configurator from app setting.
		/// </summary>
		/// <returns>The configurator from app setting.</returns>
		public static Configurator[] LoadConfiguratorFromAppSetting ()
		{
			Configurator[] configFiles = null;
			string configPath;
			if (ConfigurationManager.AppSettings ["lighDataConfig"] != null) {
				configPath = ConfigurationManager.AppSettings ["lighDataConfig"];
				configFiles = LoadConfiguratorFromPath (configPath);
			}
			return configFiles;
		}

		/// <summary>
		/// Loads the configurator from path.
		/// </summary>
		/// <returns>The configurator from path.</returns>
		/// <param name="configPath">Config path.</param>
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

		/// <summary>
		/// Loads the configurator from assembly.
		/// </summary>
		/// <returns>The configurator from assembly.</returns>
		public static Configurator[] LoadConfiguratorFromAssembly ()
		{
			Assembly assembly;
			assembly = Assembly.GetEntryAssembly ();
			if (assembly == null) {
				assembly = Assembly.GetExecutingAssembly ();
			}
			return LoadConfiguratorFromAssembly (assembly);
		}

		/// <summary>
		/// Loads the configurator from assembly.
		/// </summary>
		/// <returns>The configurator from assembly.</returns>
		/// <param name="assembly">Assembly.</param>
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

		/// <summary>
		/// Loads the configurator from config file dir.
		/// </summary>
		/// <returns>The configurator from config file dir.</returns>
		/// <param name="directory">Directory.</param>
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

		/// <summary>
		/// Loads the configurator from file.
		/// </summary>
		/// <returns>The configurator from file.</returns>
		/// <param name="fileName">File name.</param>
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

		/// <summary>
		/// Loads the configurator from file.
		/// </summary>
		/// <returns>The configurator from file.</returns>
		/// <param name="fileInfo">File info.</param>
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

		/// <summary>
		/// Loads the configurator from stream.
		/// </summary>
		/// <returns>The configurator from stream.</returns>
		/// <param name="configStream">Config stream.</param>
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

		/// <summary>
		/// Loads the configurator from xml.
		/// </summary>
		/// <returns>The configurator from xml.</returns>
		/// <param name="configNode">Config node.</param>
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
