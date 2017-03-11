using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// Data define.
	/// </summary>
	abstract class DataDefine : IDataDefine
	{
		static object _synobj = new object ();

		static Dictionary<Type, DataDefine> _defaultDefine = new Dictionary<Type, DataDefine> ();

		public static DataDefine GetDefine (Type type)
		{
			Dictionary<Type, DataDefine> defines = _defaultDefine;
			DataDefine define;
			defines.TryGetValue (type, out define);
			if (define == null) {
				lock (_synobj) {
					defines.TryGetValue (type, out define);
					if (define == null) {
						define = CreateMapping (type);
						defines [type] = define;
					}
				}
			}
			return define;
		}

		static DataDefine CreateMapping (Type type)
		{
			bool isNullable = false;
			if (type.IsGenericType) {
				Type frameType = type.GetGenericTypeDefinition ();
				if (frameType.FullName == "System.Nullable`1") {
					Type [] arguments = type.GetGenericArguments ();
					type = arguments [0];
					isNullable = true;
				}
			}

			DataDefine define;
			if (type.IsEnum) {
				define = new EnumDataDefine (type, isNullable);
			}
			else {
				TypeCode code = Type.GetTypeCode (type);
				switch (code) {
				case TypeCode.DBNull:
				case TypeCode.Empty:
				case TypeCode.Object:
					throw new LightDataException (RE.TheTypeOfDataFieldIsNotRight);
				case TypeCode.String:
					define = new PrimitiveDataDefine (type, true);
					break;
				default:
					define = new PrimitiveDataDefine (type, isNullable);
					break;
				}
			}
			return define;
		}

		protected Type _objectType;

		public Type ObjectType {
			get {
				return _objectType;
			}
		}

		protected DataDefine (Type type, bool isNullable)
		{
			_objectType = type;
			_isNullable = isNullable;
		}

		readonly bool _isNullable;

		public bool IsNullable {
			get {
				return _isNullable;
			}
		}

		public abstract object LoadData (DataContext context, IDataReader datareader, object state);
	}
}
