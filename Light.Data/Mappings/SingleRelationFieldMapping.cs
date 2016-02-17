using System;
using System.Data;
using System.Collections.Generic;

namespace Light.Data
{
	class SingleRelationFieldMapping
	{
		readonly PropertyHandler handler;

		public PropertyHandler Handler {
			get {
				return handler;
			}
		}

		readonly RelationKey[] keyPairs;

		readonly Type relateType;

		readonly DataEntityMapping masterMapping;

		public DataEntityMapping Mapping {
			get {
				return masterMapping;
			}
		}

		readonly string fieldName;

		public string FieldName {
			get {
				return fieldName;
			}
		}

		readonly DataFieldMapping[] masterMappings = null;

		DataFieldInfo[] masterInfos;

		DataEntityMapping relateMapping;

		DataFieldInfo[] relateInfos;

		//		DataFieldExpression fieldExpression;

		JoinItem item;

		public SingleRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
		{
			if (fieldName == null)
				throw new ArgumentNullException ("fieldName");
			if (mapping == null)
				throw new ArgumentNullException ("mapping");
			if (relateType == null)
				throw new ArgumentNullException ("relateType");
			if (keyPairs == null || keyPairs.Length == 0)
				throw new ArgumentNullException ("keyPairs");
			if (handler == null)
				throw new ArgumentNullException ("handler");
			this.fieldName = fieldName;
			this.masterMapping = mapping;
			this.relateType = relateType;
			this.keyPairs = keyPairs;
			this.handler = handler;
			masterMappings = new DataFieldMapping[keyPairs.Length];
			masterInfos = new DataFieldInfo[keyPairs.Length];
			for (int i = 0; i < keyPairs.Length; i++) {
				DataFieldMapping field = mapping.FindDataEntityField (keyPairs [i].MasterKey);
				masterMappings [i] = field;
				masterInfos [i] = new DataFieldInfo (field);
			}
		}

		readonly object locker = new object ();

		void InitialRelateMapping ()
		{
			if (this.relateMapping == null) {
				lock (locker) {
					if (this.relateMapping == null) {
						DataEntityMapping mapping = DataMapping.GetEntityMapping (this.relateType);
						DataFieldInfo[] infos = new DataFieldInfo[keyPairs.Length];
						DataFieldExpression expression = null;
						for (int i = 0; i < keyPairs.Length; i++) {
							DataFieldMapping field = mapping.FindDataEntityField (keyPairs [i].RelateKey);
							DataFieldInfo rinfo = new DataFieldInfo (field);
//							rinfo = new AliasDataFieldInfo (rinfo, string.Format ("{0}_{1}", mapping.TableName, rinfo.FieldName));
							infos [i] = rinfo;
							DataFieldInfo minfo = this.masterInfos [i];
							expression = DataFieldExpression.And (expression, minfo == rinfo);
						}
						this.relateInfos = infos;
						this.relateMapping = mapping;
//						this.fieldExpression = expression;
						this.item = new JoinItem (this.relateMapping, this.relateInfos, expression);
					}
				}
			}
		}

		public object ToProperty (DataContext context, object source)
		{
			InitialRelateMapping ();
			string[] relatePairs = new string[masterMappings.Length];
//			string[] masterPairs = new string[masterMappings.Length];
			object[] obj = new object[masterMappings.Length];
			for (int i = 0; i < masterMappings.Length; i++) {
				DataFieldMapping field = this.masterMappings [i];
				object value = field.Handler.Get (source);
				obj [i] = value;
				relatePairs [i] = string.Format ("{0}={1}", this.relateInfos [i].FieldName, value);
//				masterPairs [i] = string.Format ("{0}={1}", this.masterInfos [i].FieldName, value);
			}
			string relateKey = string.Join ("&", relatePairs);
//			string masterKey = string.Join ("&", masterPairs);
			object target;
			if (!context.GetRelationData (this.relateMapping, relateKey, out target)) {
				QueryExpression expression = null;
				for (int i = 0; i < masterMappings.Length; i++) {
					DataFieldInfo info = this.relateInfos [i];
					object value = obj [i];
					expression = QueryExpression.And (expression, info == value);
				}
				target = context.SelectFirst (this.relateMapping, expression);
				context.SetRelationData (this.relateMapping, relateKey, target);
				return target;
			}
			else {
				return target;
			}
		}

		public object ToProperty (DataContext context, IDataReader datareader, Dictionary<string,object> datas)
		{
			object value;
			if (!datas.TryGetValue (this.relateMapping.TableName, out value)) {
				value = this.relateMapping.LoadJoinTableData (context, datareader, this.relateInfos, datas);
				datas.Add (this.relateMapping.TableName, value);
			}
			return value;
		}

		public void LoadJoinModels (Dictionary<string,JoinItem> relates)
		{
			InitialRelateMapping ();
			if (!relates.ContainsKey (this.relateMapping.TableName)) {
				relates.Add (this.relateMapping.TableName, this.item);
			}
			if (this.relateMapping.HasJoinTableModel) {
				this.relateMapping.LoadJoinModels (relates);
			}
		}


	}
}

