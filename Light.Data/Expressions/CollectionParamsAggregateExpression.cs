using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	class CollectionParamsAggregateExpression : AggregateHavingExpression
	{
		readonly static HashSet<TypeCode> SupportTypeCodes = new HashSet<TypeCode> ();

		static CollectionParamsAggregateExpression ()
		{
			SupportTypeCodes.Add (TypeCode.Byte);
			SupportTypeCodes.Add (TypeCode.Char);
			SupportTypeCodes.Add (TypeCode.DateTime);
			SupportTypeCodes.Add (TypeCode.Decimal);
			SupportTypeCodes.Add (TypeCode.Double);
			SupportTypeCodes.Add (TypeCode.Int16);
			SupportTypeCodes.Add (TypeCode.Int32);
			SupportTypeCodes.Add (TypeCode.Int64);
			SupportTypeCodes.Add (TypeCode.SByte);
			SupportTypeCodes.Add (TypeCode.Single);
			SupportTypeCodes.Add (TypeCode.UInt16);
			SupportTypeCodes.Add (TypeCode.UInt32);
			SupportTypeCodes.Add (TypeCode.UInt64);
		}

		AggregateData _function;

		QueryCollectionPredicate _predicate;

		IEnumerable _values;

		public CollectionParamsAggregateExpression (AggregateData function, QueryCollectionPredicate predicate, IEnumerable values)
			: base (function.TableMapping)
		{
			Type type = values.GetType ();
			Type elementType;
			if (type.IsArray) {
				elementType = type.GetElementType ();
			}
			else if (type.IsGenericType) {
				Type[] arguments = type.GetGenericArguments ();
				elementType = arguments [0];
			}
			else {
				throw new LightDataException (RE.UnsupportValueType);
			}
			TypeCode typeCode = Type.GetTypeCode (elementType);
			if (!SupportTypeCodes.Contains (typeCode)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_function = function;
			_predicate = predicate;
			_values = values;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			List<DataParameter> list = new List<DataParameter> ();

			DataParameter[] ps;
			string functionSql = _function.CreateSqlString (factory, fullFieldName, out ps);
			if (ps != null && ps.Length > 0) {
				list.AddRange (ps);
			}

			foreach (object value in _values) {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, value));
			}

			dataParameters = list.ToArray ();
			return factory.CreateCollectionParamsQuerySql (functionSql, _predicate, list);
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		{
			string alise = handler (_function);
			if (string.IsNullOrEmpty (alise)) {
				return CreateSqlString (factory, fullFieldName, out dataParameters);
			}
			string name = factory.CreateDataFieldSql (alise);

			List<DataParameter> list = new List<DataParameter> ();
			foreach (object value in _values) {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, value));
			}
			dataParameters = list.ToArray ();
			return factory.CreateCollectionParamsQuerySql (name, _predicate, list);
		}

		//protected override bool EqualsDetail (AggregateHavingExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		CollectionParamsAggregateExpression target = expression as CollectionParamsAggregateExpression;
		//		return this._function.Equals (target._function)
		//		&& this._predicate == target._predicate
		//		&& Utility.EnumableObjectEquals (this._values, target._values);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
