using System;
using System.Reflection;

namespace Light.Data
{
	/// <summary>
	/// Collection relation field mapping.
	/// </summary>
	class CollectionRelationFieldMapping:BaseRelationFieldMapping
	{
		SingleRelationFieldMapping relateReferFieldMapping;

		public CollectionRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey[] keyPairs, PropertyHandler handler)
			: base (fieldName, mapping, relateType, keyPairs, handler)
		{
			
		}

		protected override void InitialRelateMappingInc ()
		{
			base.InitialRelateMappingInc ();
			SingleRelationFieldMapping[] fields = this.relateEntityMapping.GetSingleRelationFieldMappings ();
			foreach (SingleRelationFieldMapping item in fields) {
				if (this.IsReverseMatch (item)) {
					this.relateReferFieldMapping = item;
				}
			}
		}

		public object ToProperty (DataContext context, object source)
		{
			InitialRelateMapping ();
			QueryExpression expression = null;
			for (int i = 0; i < masterFieldMappings.Length; i++) {
				DataFieldInfo info = this.relateInfos [i];
				DataFieldMapping field = this.masterFieldMappings [i];
				expression = QueryExpression.And (expression, info == field.Handler.Get (source));
			}
			Type itemstype = Type.GetType ("Light.Data.LCollection`1");
			Type objectType = itemstype.MakeGenericType (this.relateType);
			object target = null;
			ConstructorInfo defaultConstructorInfo = null;
			ConstructorInfo[] constructorInfoArray = objectType.GetConstructors (BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (ConstructorInfo constructorInfo in constructorInfoArray) {
				ParameterInfo[] parameterInfoArray = constructorInfo.GetParameters ();
				if (parameterInfoArray.Length == 4) {
					defaultConstructorInfo = constructorInfo;
					break;
				}
			}
			if (defaultConstructorInfo != null) {
				object[] args = { context, source, expression, this.relateReferFieldMapping };
				target = defaultConstructorInfo.Invoke (args);
			}
			return target;
		}
	}
}

