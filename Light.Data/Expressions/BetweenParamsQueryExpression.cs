using System;
using System.Collections.Generic;

namespace Light.Data
{
	class BetweenParamsQueryExpression : QueryExpression
	{
		readonly static HashSet<TypeCode> SupportTypeCodes = new HashSet<TypeCode> ();

		static BetweenParamsQueryExpression ()
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

		DataFieldInfo _fieldInfo;

		bool _isNot;

		object _fromValue;

		object _toValue;

		public BetweenParamsQueryExpression (DataFieldInfo fieldInfo, bool isNot, object fromValue, object toValue)
			: base (fieldInfo.TableMapping)
		{
			TypeCode typeCode1 = Type.GetTypeCode (fromValue.GetType ());
			if (!SupportTypeCodes.Contains (typeCode1)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			TypeCode typeCode2 = Type.GetTypeCode (toValue.GetType ());
			if (!SupportTypeCodes.Contains (typeCode2)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_fieldInfo = fieldInfo;
			_isNot = isNot;
			_fromValue = fromValue;
			_toValue = toValue;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		//{
		//	string pn = factory.CreateTempParamName ();
		//	string pn1 = factory.CreateTempParamName ();

		//	DataParameter fromParam = new DataParameter (pn, _fieldInfo.ToParameter (_fromValue));
		//	DataParameter toParam = new DataParameter (pn1, _fieldInfo.ToParameter (_toValue));
		//	dataParameters = new [] { fromParam, toParam };
		//	return factory.CreateBetweenParamsQuerySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _isNot, fromParam, toParam);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			string pn1 = factory.CreateTempParamName ();

			DataParameter fromParam = new DataParameter (pn, _fieldInfo.ToParameter (_fromValue));
			DataParameter toParam = new DataParameter (pn1, _fieldInfo.ToParameter (_toValue));
			DataParameter [] dataParameters1 = new [] { fromParam, toParam };
			DataParameter [] dataParameters2 = null;
			string sql = factory.CreateBetweenParamsQuerySql (_fieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters2), _isNot, fromParam, toParam);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		BetweenParamsQueryExpression target = expression as BetweenParamsQueryExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo)
		//		&& Object.Equals (this._fromValue, target._fromValue)
		//		&& Object.Equals (this._toValue, target._toValue)
		//		&& this._isNot == target._isNot;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
