using System;
using System.Collections.Generic;

namespace Light.Data
{
	class BetweenParamsAggregateExpression : AggregateHavingExpression
	{
		readonly static HashSet<TypeCode> SupportTypeCodes = new HashSet<TypeCode> ();

		static BetweenParamsAggregateExpression ()
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

		bool _isNot;

		object _fromValue;

		object _toValue;

		public BetweenParamsAggregateExpression (AggregateData function, bool isNot, object fromValue, object toValue)
			: base (function.TableMapping)
		{
			TypeCode typeCode1 = Type.GetTypeCode (fromValue.GetType ());
			if (!SupportTypeCodes.Contains (typeCode1)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			TypeCode typeCode2 = Type.GetTypeCode (toValue.GetType ());
			if (!SupportTypeCodes.Contains (typeCode2)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_function = function;
			_isNot = isNot;
			_fromValue = fromValue;
			_toValue = toValue;
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string pn = factory.CreateTempParamName ();
			string pn1 = factory.CreateTempParamName ();

			DataParameter fromParam = new DataParameter (pn, _fromValue);
			DataParameter toParam = new DataParameter (pn1, _toValue);
			List<DataParameter> list = new List<DataParameter> ();
			DataParameter [] ps;
			string functionSql = _function.CreateSqlString (factory, isFullName, out ps);
			if (ps != null && ps.Length > 0) {
				list.AddRange (ps);
			}
			list.Add (fromParam);
			list.Add (toParam);
			dataParameters = list.ToArray ();
			return factory.CreateBetweenParamsQuerySql (functionSql, _isNot, fromParam.ParameterName, toParam.ParameterName);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string fromParam = state.AddDataParameter (_fromValue);
			string toParam = state.AddDataParameter (_toValue);
			string functionSql = _function.CreateSqlString (factory, isFullName, state);
			return factory.CreateBetweenParamsQuerySql (functionSql, _isNot, fromParam, toParam);
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters, GetAliasHandler handler)
		//{
		//	string alise = handler (_function);
		//	if (string.IsNullOrEmpty (alise)) {
		//		return CreateSqlString (factory, fullFieldName, out dataParameters);
		//	}
		//	string name = factory.CreateDataFieldSql (alise);

		//	string pn = factory.CreateTempParamName ();
		//	string pn1 = factory.CreateTempParamName ();

		//	DataParameter fromParam = new DataParameter (pn, _fromValue);
		//	DataParameter toParam = new DataParameter (pn1, _toValue);
		//	dataParameters = new [] { fromParam, toParam };

		//	return factory.CreateBetweenParamsQuerySql (name, _isNot, fromParam, toParam);

		//}

		//protected override bool EqualsDetail (AggregateHavingExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		BetweenParamsAggregateExpression target = expression as BetweenParamsAggregateExpression;
		//		return this._function.Equals (target._function)
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
