using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	class CollectionParamsQueryExpression : QueryExpression
	{
		readonly static HashSet<TypeCode> SupportTypeCodes = new HashSet<TypeCode> ();

		static CollectionParamsQueryExpression ()
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
			SupportTypeCodes.Add (TypeCode.String);
		}


		DataFieldInfo _fieldInfo;

		QueryCollectionPredicate _predicate;

		IEnumerable _values;

		public CollectionParamsQueryExpression (DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, IEnumerable values)
			: base (fieldInfo.TableMapping)
		{
			Type type = values.GetType ();
			Type elementType;
			if (type.IsArray) {
				elementType = type.GetElementType ();
			}
			else if (type.IsGenericType) {
				Type [] arguments = type.GetGenericArguments ();
				elementType = arguments [0];
			}
			else {
				throw new LightDataException (RE.UnsupportValueType);
			}
			TypeCode typeCode = Type.GetTypeCode (elementType);
			if (!SupportTypeCodes.Contains (typeCode)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_values = values;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	List<DataParameter> parameters = new List<DataParameter> ();
		//	List<string> list = new List<string> ();
		//	foreach (object value in _values) {
		//		string pn = factory.CreateTempParamName ();
		//		parameters.Add (new DataParameter (pn, _fieldInfo.ToParameter (value)));
		//		list.Add (pn);
		//	}
		//	DataParameter [] dataParameters1 = parameters.ToArray ();
		//	DataParameter [] dataParameters2 = null;
		//	string sql = factory.CreateCollectionParamsQuerySql (_fieldInfo.CreateSqlString (factory, isFullName, out dataParameters2), _predicate, list);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			List<string> list = new List<string> ();
			foreach (object value in _values) {
				string pn = state.AddDataParameter (_fieldInfo.ToParameter (value));
				list.Add (pn);
			}
			string sql = factory.CreateCollectionParamsQuerySql (_fieldInfo.CreateSqlString (factory, isFullName, state), _predicate, list);
			return sql;
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		CollectionParamsQueryExpression target = expression as CollectionParamsQueryExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo)
		//		&& this._predicate == target._predicate
		//		&& Utility.EnumableObjectEquals (this._values, target._values);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
