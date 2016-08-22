using System;

namespace Light.Data
{
	/// <summary>
	/// Xml configurator attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Assembly, AllowMultiple = false)]
	public class XmlConfiguratorAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.XmlConfiguratorAttribute"/> class.
		/// </summary>
		/// <param name="configPath">Config path.</param>
		public XmlConfiguratorAttribute (string configPath)
		{
			this.configPath = configPath;
		}
		string configPath;

		/// <summary>
		/// Gets the config path.
		/// </summary>
		/// <value>The config path.</value>
		public string ConfigPath {
			get {
				return configPath;
			}

			//set {
			//	configPath = value;
			//}
		}
	}
}
