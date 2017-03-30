using System;
using System.Collections.Generic;
using System.Reflection;

namespace Light.Data
{
	/// <summary>
	/// Collection relation field mapping.
	/// </summary>
	class CollectionRelationFieldMapping : BaseRelationFieldMapping
	{
		ConstructorInfo defaultConstructorInfo;

		public CollectionRelationFieldMapping (string fieldName, DataEntityMapping mapping, Type relateType, RelationKey [] keyPairs, PropertyHandler handler)
			: base (fieldName, mapping, relateType, keyPairs, handler)
		{

		}

		string [] fieldPaths;

		protected override void InitialRelateMappingInc ()
		{
			base.InitialRelateMappingInc ();
			List<string> list = new List<string> ();
			SingleRelationFieldMapping [] fields = this.relateEntityMapping.GetSingleRelationFieldMappings ();
			foreach (SingleRelationFieldMapping item in fields) {
				if (this.IsReverseMatch (item)) {
					list.Add ("." + item.FieldName);
				}
			}
			fieldPaths = list.ToArray ();
			Type itemstype = Type.GetType ("Light.Data.LCollection`1");
			Type objectType = itemstype.MakeGenericType (this.relateType);
			ConstructorInfo [] constructorInfoArray = objectType.GetConstructors (BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (ConstructorInfo constructorInfo in constructorInfoArray) {
				ParameterInfo [] parameterInfoArray = constructorInfo.GetParameters ();
				if (parameterInfoArray.Length == 4) {
					defaultConstructorInfo = constructorInfo;
					break;
				}
			}
		}

		public object ToProperty (DataContext context, object source, bool exceptOwner)
		{
			InitialRelateMapping ();
			QueryExpression expression = null;
			for (int i = 0; i < masterFieldMappings.Length; i++) {
				DataFieldInfo info = this.relateInfos [i];
				DataFieldMapping field = this.masterFieldMappings [i];
				QueryExpression keyExpression = new LambdaBinaryQueryExpression (relateEntityMapping, QueryPredicate.Eq, info, field.Handler.Get (source));
				expression = QueryExpression.And (expression, keyExpression);
				//expression = QueryExpression.And (expression, info == field.Handler.Get (source));
			}

			object target = null;
			if (defaultConstructorInfo != null) {
				object [] args = { context, source, expression, exceptOwner ? this.fieldPaths : null };
				target = defaultConstructorInfo.Invoke (args);
			}
			return target;
		}
	}
}

