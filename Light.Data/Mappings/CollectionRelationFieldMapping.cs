using System;
using System.Globalization;
using System.Reflection;
using System.Data;

namespace Light.Data
{
	class CollectionRelationFieldMapping
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

		DataEntityMapping relateMapping;

		DataFieldInfo[] relateInfos;

		public CollectionRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
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
			for (int i = 0; i < keyPairs.Length; i++) {
				masterMappings [i] = mapping.FindDataEntityField (keyPairs [i].MasterKey);
			}
		}

		readonly object locker = new object ();

		public object ToProperty (DataContext context, object source)
		{
			if (this.relateMapping == null) {
				lock (locker) {
					if (this.relateMapping == null) {
						DataEntityMapping mapping = DataMapping.GetEntityMapping (this.relateType);
						DataFieldInfo[] infos = new DataFieldInfo[keyPairs.Length];
						for (int i = 0; i < keyPairs.Length; i++) {
							DataFieldMapping field = mapping.FindDataEntityField (keyPairs [i].RelateKey);
							infos [i] = new DataFieldInfo (field);
						}
						this.relateInfos = infos;
						this.relateMapping = mapping;

					}
				}
			}
			QueryExpression expression = null;
			for (int i = 0; i < masterMappings.Length; i++) {
				DataFieldInfo info = this.relateInfos [i];
				DataFieldMapping field = this.masterMappings [i];
				expression = QueryExpression.And (expression, info == field.Handler.Get (source));
			}
			Type itemstype = Type.GetType ("Light.Data.LCollection`1");
			Type objectType = itemstype.MakeGenericType (this.relateType);
//			BindingFlags flags = BindingFlags.CreateInstance | BindingFlags.NonPublic;
//			object[] args = new object[]{ context, expression };
//			object target = Activator.CreateInstance (objectType, flags, null, args, null);
//			object target = Activator.CreateInstance(objectType,context, expression);
			object target = null;
			ConstructorInfo defaultConstructorInfo = null;
			ConstructorInfo[] constructorInfoArray = objectType.GetConstructors (BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (ConstructorInfo constructorInfo in constructorInfoArray) {
				ParameterInfo[] parameterInfoArray = constructorInfo.GetParameters ();
				if (parameterInfoArray.Length == 2) {
					defaultConstructorInfo = constructorInfo;
					break;
				}
			}
			if (defaultConstructorInfo != null) {
				object[] args = new object[]{ context, expression };
				target = defaultConstructorInfo.Invoke (args);
			}
			return target;
		}

	}
}

