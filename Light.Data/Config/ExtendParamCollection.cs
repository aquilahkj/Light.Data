using System.Collections.Specialized;
using System.Xml;
using System;

namespace Light.Data
{
	class ExtendParamCollection : NameObjectCollectionBase
	{
		public static ExtendParamCollection CreateExtendParamsCollection (XmlNode node)
		{
			ExtendParamCollection collection = new ExtendParamCollection ();
			foreach (XmlNode fieldNode in node.ChildNodes) {
				if (fieldNode.Name == "parameter") {
					if (fieldNode.Attributes ["name"] != null && fieldNode.Attributes ["value"] != null) {
						string name = fieldNode.Attributes ["name"].Value;
						string value = fieldNode.Attributes ["value"].Value;
						if (!string.IsNullOrEmpty (name) && !string.IsNullOrEmpty (value)) {
							collection.BaseAdd (name, value);
						}
					}
				}
			}
			return collection;
		}

		public static ExtendParamCollection CreateExtendParamsCollection (Type type)
		{
			ExtendParamCollection collection = new ExtendParamCollection ();
			ExtendParamAttribute[] attributes = AttributeCore.GetTypeAttributes<ExtendParamAttribute> (type, true);
			if (attributes != null && attributes.Length > 0) {
				foreach (ExtendParamAttribute attribute in attributes) {
					if (!string.IsNullOrEmpty (attribute.Name) && !string.IsNullOrEmpty (attribute.Value)) {
						collection.BaseAdd (attribute.Name, attribute.Value);
					}
				}
			}
			return collection;
		}

		public static ExtendParamCollection CreateExtendParamsCollection<T> (Type type) where T:ExtendParamAttribute
		{
			ExtendParamCollection collection = new ExtendParamCollection ();
			T[] attributes = AttributeCore.GetTypeAttributes<T> (type, true);
			if (attributes != null && attributes.Length > 0) {
				foreach (ExtendParamAttribute attribute in attributes) {
					if (!string.IsNullOrEmpty (attribute.Name) && !string.IsNullOrEmpty (attribute.Value)) {
						collection.BaseAdd (attribute.Name, attribute.Value);
					}
				}
			}
			return collection;
		}



//		public ExtendParamsCollection ()
//		{
//
//		}
//
//		public ExtendParamsCollection (string paramsContent)
//		{
//			if (string.IsNullOrEmpty (paramsContent)) {
//				return;
//			}
//			string[] arr = paramsContent.Split ('&');
//			foreach (string str in arr) {
//				string[] nvs = str.Split ('=');
//				string key = nvs [0];
//				string value = null;
//				if (nvs.Length == 2) {
//					value = nvs [1];
//				}
//				BaseAdd (key, value);
//			}
//		}

		public string this [int index] {
			get {
				return BaseGet (index).ToString ();
			}
		}

		public string this [string key] {
			get {
				object obj = BaseGet (key);
				if (obj == null)
					return null;
				else
					return obj.ToString ();
			}
		}
	}
}
