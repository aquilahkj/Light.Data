using System;

namespace Light.Data
{
	public class RelationPair
	{
		readonly string relateKey;

		public string RelateKey {
			get {
				return relateKey;
			}
		}

		readonly string masterKey;

		public string MasterKey {
			get {
				return masterKey;
			}
		}

		public RelationPair (string masterKey, string relateKey)
		{
			if (string.IsNullOrEmpty (masterKey))
				throw new ArgumentNullException ("masterKey");
			if (string.IsNullOrEmpty (relateKey))
				throw new ArgumentNullException ("relateKey");
			this.masterKey = masterKey;
			this.relateKey = relateKey;
		}
	}
}

