using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Light.Data.Config
{
    /// <summary>
    /// 配置节点处理类
    /// </summary>
    public class LightDataConfigurationSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return section;
        }
    }
}
