using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class RelationKey
	{
		public RelationKey (string masterKey, string relateKey)
		{
			if (string.IsNullOrEmpty (masterKey)) {
				throw new ArgumentNullException ("masterKey");
			}
			if (string.IsNullOrEmpty (relateKey)) {
				throw new ArgumentNullException ("relateKey");
			}
			this.masterKey = masterKey;
			this.relateKey = relateKey;
		}

		readonly string masterKey;

		public string MasterKey {
			get {
				return masterKey;
			}
		}

		readonly string relateKey;

		public string RelateKey {
			get {
				return relateKey;
			}
		}

		public bool IsReverseMatch (RelationKey rk)
		{
			return this.masterKey == rk.relateKey && this.relateKey == rk.masterKey;
		}

		public bool IsMatch (RelationKey rk)
		{
			return this.masterKey == rk.masterKey && this.relateKey == rk.relateKey;
		}
	}
}
