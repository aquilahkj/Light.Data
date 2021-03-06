﻿using System;

namespace Light.Data
{
	class SubStringDataFieldInfo : ExtendDataFieldInfo
	{
		int _start;

		int _size;

		internal SubStringDataFieldInfo (DataFieldInfo info, int start, int size)
			: base (info)
		{
			if (start < 0) {
				throw new ArgumentOutOfRangeException ("start");
			}
			if (size < 0) {
				throw new ArgumentOutOfRangeException ("size");
			}
			_start = start;
			_size = size;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateSubStringSql (field, _start, _size);
		}

		internal override string DBType {
			get {
				return "string";
			}
		}

		internal override object ToParameter (object value)
		{
			if (value is string) {
				return value;
			}
			else {
				return value.ToString ();
			}
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				SubStringDataFieldInfo target = info as SubStringDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return this._start == target._start && this._size == target._size;
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}
	}
}
