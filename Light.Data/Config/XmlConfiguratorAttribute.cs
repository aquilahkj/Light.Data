using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Config
{
	/// <summary>
	/// XML配置特性
	/// </summary>
	[AttributeUsage (AttributeTargets.Assembly)]
	public class XmlConfiguratorAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configPath">配置文件路径</param>
		public XmlConfiguratorAttribute (string configPath)
		{
			ConfigPath = configPath;
		}

		/// <summary>
		/// 配置文件路径
		/// </summary>
		public string ConfigPath {
			get;
			private set;
		}
	}
}
