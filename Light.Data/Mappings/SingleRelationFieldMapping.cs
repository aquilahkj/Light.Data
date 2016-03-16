using System;
using System.Data;

namespace Light.Data
{
	class SingleRelationFieldMapping:BaseRelationFieldMapping
	{
		JoinItem item;

		public JoinItem Item {
			get {
				return item;
			}
		}

		public SingleRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
			: base (fieldName, mapping, relateType, keyPairs, handler)
		{

		}

		public void InitialRelation ()
		{
			InitialRelateMapping ();
		}

		protected override void InitialRelateMappingInc ()
		{
			base.InitialRelateMappingInc ();
			DataFieldExpression expression = null;
			for (int i = 0; i < this.relateFieldMappings.Length; i++) {
				DataFieldInfo minfo = this.masterInfos [i];
				DataFieldInfo rinfo = this.relateInfos [i];
				expression = DataFieldExpression.And (expression, minfo == rinfo);
			}
			this.item = new JoinItem (this.relateEntityMapping, this.relateInfos, expression);
		}

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
			if (!datas.CheckJoinData (this)) {
				return null;
			}
			else {
				object value;
				if (!datas.GetJoinData (this, out value)) {
					value = this.relateEntityMapping.LoadJoinTableData (context, datareader, this.relateInfos, datas);
					datas.SetJoinData (this, value);
				}
				return value;
			}
		}
	}
}

