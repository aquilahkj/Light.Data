using System;
using System.Collections.Generic;

namespace Light.Data
{
	class SingleParamQueryExpression : QueryExpression
	{
		readonly static HashSet<TypeCode> SupportTypeCodes = new HashSet<TypeCode> ();

		static SingleParamQueryExpression ()
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

		QueryPredicate _predicate;

		object _value;

		bool _isReverse;

		public SingleParamQueryExpression (DataFieldInfo fieldInfo, QueryPredicate predicate, object value, bool isReverse)
			: base (fieldInfo.TableMapping)
		{
			TypeCode typeCode = Type.GetTypeCode (value.GetType ());
			if (!SupportTypeCodes.Contains (typeCode)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_value = value;
			_isReverse = isReverse;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter [] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _fieldInfo.ToParameter (_value));
			DataParameter [] dataParameters1 = new [] { dataParameter };
			DataParameter [] dataParameters2 = null;
			string sql = factory.CreateSingleParamSql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName, out dataParameters2), _predicate, _isReverse, dataParameter);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		SingleParamQueryExpression target = expression as SingleParamQueryExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo)
		//		&& Object.Equals (this._value, target._value)
		//		&& this._predicate == target._predicate
		//		&& this._isReverse == target._isReverse;
		//	}
		//	else {
		//		return false;
		//	}
		//}

	}
}
