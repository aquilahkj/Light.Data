using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Light.Data
{
	static class ConnectionExtendManager
	{
		static readonly string SECTION_NAME = "connectionExtendConfig";

		static readonly Dictionary<string,ExtendParamCollection> paramDict = new Dictionary<string, ExtendParamCollection> ();

		static ConnectionExtendManager ()
		{
			object obj = ConfigurationManager.GetSection (SECTION_NAME);
			XmlNode node = obj as XmlNode;
			if (node != null) {
				foreach (XmlNode fieldNode in node.ChildNodes) {
					if (fieldNode.Name == "connection") {
						if (fieldNode.Attributes ["name"] != null) {
							string name = fieldNode.Attributes ["name"].Value;
							ExtendParamCollection value = ExtendParamCollection.CreateExtendParamsCollection (fieldNode);
							if (!string.IsNullOrEmpty (name) && value != null) {
								paramDict [name] = value;
							}
						}
					}
				}
			}
		}

		internal static ExtendParamCollection GetExtendParams (string connectionName)
		{
			ExtendParamCollection collection;
			paramDict.TryGetValue (connectionName, out collection);
			return collection;
		}

		//		public ConnectionExtendManager ()
		//		{
		//
		//		}
	}
}

