using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	abstract class FieldMapping
	{
		protected static readonly string _fieldRegex = @"^([a-zA-Z][a-z0-9A-Z_]*)$";

		#region 私有变量

		string _dbType = null;

		bool _isNullable = false;

		Type _objectType = null;

		string _name = null;

		int? _dataOrder = null;

		string _indexName = null;

		DataMapping _typeMapping = null;

		PropertyHandler _handler = null;

		object _defaultValue = null;

		TypeCode _typeCode = TypeCode.Empty;

		#endregion

		#region 公共属性

		public virtual string DBType {
			get {
				return _dbType;
			}
//			protected set {
//				_dBType = value;
//			}
		}

		public virtual bool IsNullable {
			get {
				return _isNullable;
			}
//			protected set {
//				_isNullable = value;
//			}
		}

		public Type ObjectType {
			get {
				return _objectType;
			}
//			protected set {
//				_objectType = value;
//				_typeCode = Type.GetTypeCode (ObjectType);
//			}
		}

		public string Name {
			get {
				return _name;
			}
//			protected set {
//				_name = value;
//			}
		}

		public int? DataOrder {
			get {
				return _dataOrder;
			}
			protected set {
				_dataOrder = value;
			}
		}

		public string IndexName {
			get {
				return _indexName;
			}
//			protected set {
//				_indexName = value;
//			}
		}

		public DataMapping TypeMapping {
			get {
				return _typeMapping;
			}
//			protected set {
//				_typeMapping = value;
//			}
		}

		public PropertyHandler Handler {
			get {
				return _handler;
			}
			set {
				_handler = value;
			}
		}

		public object DefaultValue {
			get {
				return _defaultValue;
			}
			set {
				_defaultValue = value;
			}
		}

		#endregion

		#region 公共方法

		public FieldMapping (Type objectType, string name, string indexName, DataMapping typeMapping, bool isNullable, string dbType)
		{
			this._objectType = objectType;
			if (objectType != null) {
				this._typeCode = Type.GetTypeCode (objectType);
			}
			this._name = name;
			this._indexName = indexName;
			this._typeMapping = typeMapping;
			this._isNullable = isNullable;
			this._dbType = dbType;
		}

		public virtual object ToProperty (object value)
		{
			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
				TypeCode code = Type.GetTypeCode (ObjectType);
				if (code == TypeCode.String) {
					if (IsNullable)
						return null;
					else
						return string.Empty;
				}
				else if (code == TypeCode.Boolean) {
					return false;
				}
				else if (code == TypeCode.DateTime) {
					return DateTime.MinValue;
				}
				else if (code == TypeCode.Char) {
					return Char.MinValue;
				}
				else {
					return 0;
				}
			}
			else {
				if (ObjectType != null && value.GetType () != ObjectType) {
					value = Convert.ChangeType (value, ObjectType);
				}
				return value;
			}
		}

		public virtual object ToColumn (object value)
		{
			if (Object.Equals (value, null)) {
				if (IsNullable) {
					return null;
				}
				else {
					if (_typeCode == TypeCode.Object || _typeCode == TypeCode.Empty || _typeCode == TypeCode.DBNull) {
						return null;
					}
					if (_typeCode == TypeCode.String) {
						return string.Empty;
					}
					else if (_typeCode == TypeCode.Boolean) {
						return false;
					}
					else if (_typeCode == TypeCode.DateTime) {
						return DateTime.MinValue;
					}
					else if (_typeCode == TypeCode.Char) {
						return Char.MinValue;
					}
					else {
						return 0;
					}
				}
			}
			else {
				if (ObjectType != null && value.GetType () != ObjectType) {
					value = Convert.ChangeType (value, ObjectType);
				}
				return value;
			}
		}

		#endregion
	}
}
