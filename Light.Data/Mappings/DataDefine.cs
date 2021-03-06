﻿using System;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// Data define.
	/// </summary>
	abstract class DataDefine : IDataDefine
	{
		Type _type;

		public Type ObjectType {
			get {
				return _type;
			}
		}

		protected DataDefine (Type type, bool isNullable)
		{
			_type = type;
			_isNullable = isNullable;
		}

		readonly bool _isNullable;

		public bool IsNullable {
			get {
				return _isNullable;
			}
		}

		public abstract object LoadData (DataContext context, IDataReader datareader, object state);

		//		public abstract object LoadData (DataContext context, System.Data.DataRow datarow);
	}
}
