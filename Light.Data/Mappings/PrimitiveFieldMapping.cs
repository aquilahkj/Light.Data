using System;

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

		readonly DefaultTimeFunction _defaultTimeFunction = null;

		public PrimitiveFieldMapping (Type type, string fieldName, string indexName, DataMapping mapping, bool isNullable, string dbType, object defaultValue, bool isIdentity, bool isPrimaryKey)
			: base (type, fieldName, indexName, mapping, isNullable, dbType)
		{
			if (type != typeof(string)) {
				Type itemstype = Type.GetType ("System.Nullable`1");
				_nullableType = itemstype.MakeGenericType (type);
			}
			else {
				_nullableType = type;
			}
			if (defaultValue != null) {
				Type defaultValueType = defaultValue.GetType ();
				if (_typeCode == TypeCode.DateTime) {
					if (defaultValueType == typeof(DefaultTime)) {
						this._defaultTimeFunction = DefaultTimeFunction.GetFunction ((DefaultTime)defaultValue);
					}
					else if (defaultValueType == typeof(DateTime)) {
						this._defaultValue = defaultValue;
					}
					else if (defaultValueType == typeof(string)) {
						string str = defaultValue as string;
						DateTime dt;
						if (DateTime.TryParse (str, out dt)) {
							this._defaultValue = dt;
						}
					}
				}
				else if (defaultValueType == type) {
					this._defaultValue = defaultValue;
				}
				else {
					this._defaultValue = Convert.ChangeType (defaultValue, type);
				}
			}
			if (isIdentity) {
				if (_typeCode == TypeCode.Int32 || _typeCode == TypeCode.Int64 || _typeCode == TypeCode.UInt32 || _typeCode == TypeCode.UInt64) {
					_isIdentity = true;
				}
				else {
					throw new LightDataException (RE.TheTypeOfIdentityFieldError);
				}
			}

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
				if (this._typeCode == TypeCode.String && !IsNullable) {
					return string.Empty;
				}
				else {
					return null;
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

		public override object ToParameter (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				return null;
			}
			else {
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
					if (_typeCode == TypeCode.DateTime && _defaultTimeFunction != null) {
						return _defaultTimeFunction.GetValue ();
					}
					else if (_defaultValue != null) {
						return _defaultValue;
					}
					else {
						return _minValue;
					}
				}
			}
			else if (_typeCode == TypeCode.DateTime && Object.Equals (value, DateTime.MinValue)) {
				if (_defaultTimeFunction != null) {
					return this._defaultTimeFunction.GetValue ();
				}
				else if (_defaultValue != null) {
					return _defaultValue;
				}
				else {
					return value;
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
