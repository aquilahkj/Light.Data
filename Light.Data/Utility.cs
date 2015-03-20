using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	static class Utility
	{
		static Type StringType = typeof(string);

		public static bool ParseDbType (string dbType, out DbType type)
		{
			type = DbType.String;
			int index = dbType.IndexOf ('(');
			string typeString = string.Empty;
			if (index < 0) {
				typeString = dbType;
			}
			else if (index == 0) {
				return false;
			}
			else {
				typeString = dbType.Substring (0, index);
			}
			bool result = false;
			try {
				type = (DbType)Enum.Parse (typeof(DbType), typeString, true);
				result = true;
			}
			catch {
				result = false;
			}
			if (typeString.Equals ("int", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.Int32;
				return true;
			}
			else if (typeString.Equals ("short", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.Int16;
				return true;
			}
			else if (typeString.Equals ("long", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.Int64;
				return true;
			}
			else if (typeString.Equals ("uint", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.UInt32;
				return true;
			}
			else if (typeString.Equals ("ushort", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.UInt16;
				return true;
			}
			else if (typeString.Equals ("ulong", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.UInt64;
				return true;
			}
			else if (typeString.Equals ("float", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.Double;
				return true;
			}
			else if (typeString.Equals ("bool", StringComparison.OrdinalIgnoreCase)) {
				type = DbType.Boolean;
				return true;
			}

			return result;
		}

		public static bool ParseSize (string dbType, out int size)
		{
			size = 0;
			string result = Regex.Match (dbType, "(?<=\\u0028).*?(?=\\u0029)").Value;
			if (string.IsNullOrEmpty (result)) {
				return false;
			}
			string[] arr = result.Split (',');
			if (int.TryParse (arr [0], out size)) {
				return true;
			}
			return false;
		}

		public static bool EnumableObjectEquals (object value1, object value2)
		{
			if (Object.Equals (value1, value2)) {
				return true;
			}
			Type objType1 = value1.GetType ();
			Type objType2 = value2.GetType ();
			if (objType1 != objType2) {
				return false;
			}
			if (objType1 == StringType) {
				return string.Equals (value1, value2);
			}
			if (value1 is IEnumerable) {
				System.Collections.IEnumerator e1 = (value1 as IEnumerable).GetEnumerator ();
				System.Collections.IEnumerator e2 = (value2 as IEnumerable).GetEnumerator ();

				while (true) {
					bool b1 = e1.MoveNext ();
					bool b2 = e2.MoveNext ();
					if (b1 && b2) {
						if (!Object.Equals (e1.Current, e2.Current)) {
							return false;
						}
					}
					else if (!b1 && !b2) {
						return true;
					}
					else {
						return false;
					}
				}
			}
			else {
				return false;
			}
		}

		public static int EnumableHashCode (IEnumerable e)
		{
			int result = 0;
			foreach (object obj in e) {
				result ^= obj.GetHashCode ();
			}
			return result;
		}
	}
}
