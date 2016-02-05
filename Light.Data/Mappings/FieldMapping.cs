using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	abstract class FieldMapping
	{
		protected static readonly string _fieldRegex = @"^([a-zA-Z][a-z0-9A-Z_]*)$";

		#region 私有变量

		protected string _dbType = null;

		protected bool _isNullable = false;

		protected Type _objectType = null;

		protected string _name = null;

		protected int? _dataOrder = null;

		protected string _indexName = null;

		protected DataMapping _typeMapping = null;

		protected PropertyHandler _handler = null;

//		protected object _defaultValue = null;

		protected TypeCode _typeCode = TypeCode.Empty;

		#endregion

		#region 公共属性

		public virtual string DBType {
			get {
				return _dbType;
			}
		}

		public virtual bool IsNullable {
			get {
				return _isNullable;
			}
		}

		public Type ObjectType {
			get {
				return _objectType;
			}
		}

		public string Name {
			get {
				return _name;
			}
		}

//		public int? DataOrder {
//			get {
//				return _dataOrder;
//			}
//			protected set {
//				_dataOrder = value;
//			}
//		}
//
//		public PropertyHandler Handler {
//			get {
//				return _handler;
//			}
//			set {
//				_handler = value;
//			}
//		}

		public string IndexName {
			get {
				return _indexName;
			}
		}

		public DataMapping TypeMapping {
			get {
				return _typeMapping;
			}
		}



//		public object DefaultValue {
//			get {
//				return _defaultValue;
//			}
//			set {
//				_defaultValue = value;
//			}
//		}

//		public virtual object DefaultValue {
//			get {
//				return null;
//			}
//		}

		public TypeCode TypeCode {
			get {
				return _typeCode;
			}
		}

		#endregion

		#region 公共方法

		protected FieldMapping (Type objectType, string name, string indexName, DataMapping typeMapping, bool isNullable, string dbType)
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

		public abstract object ToProperty (object value);

		public abstract object ToColumn (object value);

		//		bool isString;
		//
		//		public bool IsString {
		//			get {
		//				return isString;
		//			}
		//		}

//		public virtual object ToProperty (object value)
//		{
//			if (Object.Equals (value, DBNull.Value) || Object.Equals (value, null)) {
//				if (DefaultValue != null) {
//					return DefaultValue;
//				}
//				else if (this._typeCode == TypeCode.String && !IsNullable) {
//					return string.Empty;
//				}
//				else {
//					return null;
//				}
//			}
//			else {
//				if (ObjectType != null && value.GetType () != ObjectType) {
//					value = Convert.ChangeType (value, ObjectType);
//				}
//				return value;
//			}
//		}
//
//		public virtual object ToColumn (object value)
//		{
//			if (Object.Equals (value, null) || Object.Equals (value, DBNull.Value)) {
//				if (IsNullable) {
//					return null;
//				}
//				else {
//					return MinValue;
//				}
//			}
//			else {
//				if (ObjectType != null && value.GetType () != ObjectType) {
//					return Convert.ChangeType (value, ObjectType);
//				}
//				else {
//					return value;
//				}
//			}
//		}

		#endregion
	}
}
