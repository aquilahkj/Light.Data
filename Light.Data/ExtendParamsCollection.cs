using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Light.Data
{
	class ExtendParamsCollection : NameObjectCollectionBase
	{
		public ExtendParamsCollection ()
			: this (string.Empty)
		{

		}

		public ExtendParamsCollection (string paramsContent)
		{
			if (string.IsNullOrEmpty (paramsContent)) {
				return;
			}
			string[] arr = paramsContent.Split ('&');
			foreach (string str in arr) {
				string[] nvs = str.Split ('=');
				string key = nvs [0];
				string value = null;
				if (nvs.Length == 2) {
					value = nvs [1];
				}
				BaseAdd (key, value);
			}
		}

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
