using System;
using System.Collections.Generic;

namespace Light.Data
{
	class JoinCapsule
	{
		List<JoinModel> models;

		public List<JoinModel> Models {
			get {
				return models;
			}
		}

		JoinSelector slector;

		public JoinSelector Slector {
			get {
				return slector;
			}
		}

		public JoinCapsule (JoinSelector slector, List<JoinModel> models)
		{
			if (slector == null)
				throw new ArgumentNullException ("slector");
			if (models == null)
				throw new ArgumentNullException ("models");
			this.slector = slector;
			this.models = models;
		}

		public JoinCapsule CloneCapsule (QueryExpression query, OrderExpression order)
		{
			List<JoinModel> models1 = new List<JoinModel> ();
			JoinModel mainModels = this.models [0];
			models1.Add (new JoinModel (mainModels.Mapping, null, query, order));
			for (int i = 1; i < this.models.Count; i++) {
				models1.Add(this.models [i]);
			}
			JoinCapsule clone = new JoinCapsule (this.slector, models1);
			return clone;
		}
	}
}

