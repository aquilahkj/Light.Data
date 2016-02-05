using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class PrimitiveFieldMapping : DataFieldMapping
	{
		const Int16 MinInt16 = 0;

		const Int32 MinInt32 = 0;

		const Int64 MinInt64 = 0;

		const Decimal MinDecimal = 0;

		const Single MinSingle = 0;

		const Double MinDouble = 0;

		readonly object _minValue = null;
	
		readonly object _defaultValue = null;

		public PrimitiveFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType, object defaultValue, bool isIdentity, bool isPrimaryKey)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
			if (type != typeof(string)) {
				Type itemstype = System.Type.GetType ("System.Nullable`1");
				_nullableType = itemstype.MakeGenericType (type);
			}
			else {
				_nullableType = type;
			}
			if (defaultValue != null) {
				if (defaultValue.GetType () == type) {
					this._defaultValue = defaultValue;
				}
				else {
					this._defaultValue = Convert.ChangeType (defaultValue, type);
				}
			}
			_isIdentity = isIdentity;
			_isPrimaryKey = isPrimaryKey;
			if (_typeCode == TypeCode.String) {
				_minValue = string.Empty;
			}
			else if (_typeCode == TypeCode.Boolean) {
				_minValue = false;
			}
			else if (_typeCode == TypeCode.DateTime) {
				_minValue = DateTime.MinValue;
			}
			else if (_typeCode == TypeCode.Char) {
				_minValue = Char.MinValue;
			}
			else if (_typeCode == TypeCode.Int16) {
				_minValue = MinInt16;
			}
			else if (_typeCode == TypeCode.Int32) {
				_minValue = MinInt32;
			}
			else if (_typeCode == TypeCode.Int64) {
				_minValue = MinInt64;
			}
			else if (_typeCode == TypeCode.UInt16) {
				_minValue = UInt16.MinValue;
			}
			else if (_typeCode == TypeCode.UInt32) {
				_minValue = UInt32.MinValue;
			}
			else if (_typeCode == TypeCode.Int64) {
				_minValue = UInt64.MinValue;
			}
			else if (_typeCode == TypeCode.Decimal) {
				_minValue = MinDecimal;
			}
			else if (_typeCode == TypeCode.Single) {
				_minValue = MinSingle;
			}
			else if (_typeCode == TypeCode.Double) {
				_minValue = MinDouble;
			}
		}

		Type _nullableType;

		public Type NullableType {
			get {
				return _nullableType;
			}
		}

		bool _isPrimaryKey;

		public bool IsPrimaryKey {
			get {
				return _isPrimaryKey;
			}
		}

		bool _isIdentity;

		public bool IsIdentity {
			get {
				return _isIdentity;
			}
		}

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
				if (_defaultValue != null) {
					return _defaultValue;
				}
				else if (this._typeCode == TypeCode.String && !IsNullable) {
					return string.Empty;
				}
				else {
					return null;
				}
			}
			else {
				if (ObjectType != null && value.GetType () != ObjectType) {
					value = Convert.ChangeType (value, ObjectType);
				}
				return value;
			}
		}

		public override object ToColumn (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				if (IsNullable) {
					return null;
				}
				else {
					return _minValue;
				}
			}
			else {
				if (ObjectType != null && value.GetType () != ObjectType) {
					return Convert.ChangeType (value, ObjectType);
				}
				else {
					return value;
				}
			}
		}
	}
}
