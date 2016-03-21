using System;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// Single relation field mapping.
	/// </summary>
	class SingleRelationFieldMapping:BaseRelationFieldMapping
	{
//		JoinItem joinItem;
//
//		public JoinItem JoinItemInfo {
//			get {
//				return joinItem;
//			}
//		}

		public SingleRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
			: base (fieldName, mapping, relateType, keyPairs, handler)
		{

		}

		public void InitialRelation ()
		{
			InitialRelateMapping ();
		}

//		protected override void InitialRelateMappingInc ()
//		{
//			base.InitialRelateMappingInc ();
//			DataFieldExpression expression = null;
//			for (int i = 0; i < this.relateFieldMappings.Length; i++) {
//				DataFieldInfo minfo = this.masterInfos [i];
//				DataFieldInfo rinfo = this.relateInfos [i];
//				expression = DataFieldExpression.And (expression, minfo == rinfo);
//			}
//			this.joinItem = new JoinItem (this.relateEntityMapping, this.masterInfos, this.relateInfos);
//		}

		public object ToProperty (DataContext context, object source, RelationContent datas)
		{
			InitialRelateMapping ();
			if (Object.ReferenceEquals (this, datas.CollectionRelateReferFieldMapping)) {
				return datas.CollectionRelateReferFieldValue;
			}
			object value;
			string[] relatePairs = new string[masterFieldMappings.Length];
			object[] obj = new object[masterFieldMappings.Length];
			for (int i = 0; i < masterFieldMappings.Length; i++) {
				DataFieldMapping field = this.masterFieldMappings [i];
				value = field.Handler.Get (source);
				obj [i] = value;
				relatePairs [i] = string.Format ("{0}={1}", this.relateInfos [i].FieldName, value);
			}
			string relateKey = string.Join ("&", relatePairs);

			if (!datas.GetQueryData (this.relateEntityMapping, relateKey, out value)) {
				QueryExpression expression = null;
				for (int i = 0; i < masterFieldMappings.Length; i++) {
					DataFieldInfo info = this.relateInfos [i];
					value = obj [i];
					expression = QueryExpression.And (expression, info == value);
				}
				value = context.SelectFirst (this.relateEntityMapping, expression, datas);
				datas.SetQueryData (this.relateEntityMapping, relateKey, value);
				return value;
			}
			else {
				return value;
			}
		}

		public object ToProperty (DataContext context, IDataReader datareader, RelationContent datas)
		{
			if (Object.ReferenceEquals (this, datas.CollectionRelateReferFieldMapping)) {
				return datas.CollectionRelateReferFieldValue;
			}
			string aliasName;
			if (!datas.CheckJoinData (this, out aliasName)) {
				return null;
			}
			else {
				object value;
				if (!datas.GetJoinData (this, out value)) {
					foreach (DataFieldInfo info in this.relateInfos) {
						string name = string.Format ("{0}_{1}", aliasName, info.FieldName);
						object obj = datareader [name];
						if (Object.Equals (obj, DBNull.Value) || Object.Equals (obj, null)) {
							datas.SetJoinData (this, null);
							return null;
						}
					}
					object item = Activator.CreateInstance (this.RelateMapping.ObjectType);
					datas.SetJoinData (this, item);
					this.relateEntityMapping.LoadJoinTableData (context, datareader, item, datas, aliasName);
					value = item;

				}
				return value;
			}
		}
	}
}

