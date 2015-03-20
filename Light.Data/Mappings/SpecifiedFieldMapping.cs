using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	class SpecifiedFieldMapping
	{
		Type _objectType;

		public Type ObjectType {
			get {
				return _objectType;
			}
			protected set {
				_objectType = value;
			}
		}

		string _name;

		public string Name {
			get {
				return _name;
			}
			protected set {
				_name = value;
			}
		}

		FieldMapping _relateFieldMapping;

		public FieldMapping RelateFieldMapping {
			get {
				return _relateFieldMapping;
			}
			internal set {
				_relateFieldMapping = value;
			}
		}

		DataMapping _typeMapping;

		public DataMapping TypeMapping {
			get {
				return _typeMapping;
			}
			protected set {
				_typeMapping = value;
			}
		}

		PropertyHandler _handler;

		public PropertyHandler Handler {
			get {
				return _handler;
			}
			set {
				_handler = value;
			}
		}
	}
}
