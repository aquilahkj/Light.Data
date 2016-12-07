using System;
using System.Collections.Generic;

namespace Light.Data
{
	class JoinCapsule
	{
		List<EntityJoinModel> models;

		public List<EntityJoinModel> Models {
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

		//RelationMap relationMap;

		//public RelationMap RelationMap {
		//	get {
		//		return relationMap;
		//	}
		//}

		public JoinCapsule (JoinSelector slector, List<EntityJoinModel> models)
		{
			if (slector == null)
				throw new ArgumentNullException (nameof (slector));
			if (models == null)
				throw new ArgumentNullException (nameof (models));
			//if (relationMap == null)
			//	throw new ArgumentNullException (nameof (relationMap));
			this.slector = slector;
			this.models = models;
			//this.relationMap = relationMap;
		}

		//public JoinCapsule CloneCapsule (QueryExpression query, OrderExpression order)
		//{
		//	List<JoinModel> models1 = new List<JoinModel> ();
		//	JoinModel mainModels = this.models [0];
		//	JoinModel model1 = new JoinModel (mainModels.Mapping, null, query, order);
		//	model1.AliasTableName = mainModels.AliasTableName;
		//	models1.Add (model1);

		//	for (int i = 1; i < this.models.Count; i++) {
		//		models1.Add (this.models [i]);
		//	}
		//	JoinCapsule clone = new JoinCapsule (this.slector, models1, this.relationMap);
		//	return clone;
		//}
	}
}

