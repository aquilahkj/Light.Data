using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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


		DataFieldInfo _fieldInfo = null;

		QueryCollectionPredicate _predicate;

		IEnumerable _values = null;

		public CollectionParamsQueryExpression (DataFieldInfo fieldInfo, QueryCollectionPredicate predicate, IEnumerable values)
			: base (fieldInfo.TableMapping)
		{
			Type type = values.GetType ();
			Type elementType = type.GetElementType ();
			TypeCode typeCode = Type.GetTypeCode (elementType);
			if (!SupportTypeCodes.Contains (typeCode)) {
				throw new LightDataException (RE.UnsupportValueType);
			}
			_fieldInfo = fieldInfo;
			_predicate = predicate;
			_values = values;
		}

		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			List<DataParameter> list = new List<DataParameter> ();
			foreach (object value in _values) {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, _fieldInfo.ToColumn (value), _fieldInfo.DBType));
			}
			dataParameters = list.ToArray ();
			return factory.CreateCollectionParamsQuerySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _predicate, list);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				CollectionParamsQueryExpression target = expression as CollectionParamsQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._predicate == target._predicate
				&& Utility.EnumableObjectEquals (this._values, target._values);
			}
			else {
				return false;
			}
		}
	}
}
