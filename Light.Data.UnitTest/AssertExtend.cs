using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Light.Data.UnitTest
{
	public static class AssertExtend
	{
		public static void AreObjectsEqual (object expected, object actual)
		{
			AreObjectsEqual (expected, actual, "object");
		}

		public static void AreTypeEqual<T> (T expected, T actual)
		{
			Type expectedType = typeof (T);
			AreTypeEqual<T> (expected, actual, expectedType.Name);
		}

		public static void AreEnumerableTypeEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual)
		{
			if (Object.Equals (expected, null) && Object.Equals (actual, null)) {
				return;
			}
			if (Object.ReferenceEquals (expected, actual)) {
				return;
			}
			if ((!Object.Equals (expected, null) && Object.Equals (actual, null)) || (Object.Equals (expected, null) && !Object.Equals (actual, null))) {
				Assert.AreEqual (expected, actual, string.Format ("{0} expected value not equal", "enumerable"));
			}

			List<T> list1 = new List<T> ();
			List<T> list2 = new List<T> ();
			foreach (T item in expected) {
				list1.Add (item);
			}
			foreach (T item in actual) {
				list2.Add (item);
			}
			Type expectedType = typeof (T);
			Assert.AreEqual (list1.Count, list2.Count, string.Format ("expected {0} enumerable length not equal", expectedType.Name));
			for (int i = 0; i < list1.Count; i++) {
				AreTypeEqual<T> (list1 [i], list2 [i], string.Format ("{0}'s[{1}]", expectedType.Name, i));
			}
		}

		public static void AreEnumerableEqual (IEnumerable expected, IEnumerable actual)
		{
			if (Object.Equals (expected, null) && Object.Equals (actual, null)) {
				return;
			}
			if (Object.ReferenceEquals (expected, actual)) {
				return;
			}
			if ((!Object.Equals (expected, null) && Object.Equals (actual, null)) || (Object.Equals (expected, null) && !Object.Equals (actual, null))) {
				Assert.AreEqual (expected, actual, string.Format ("{0} expected value not equal", "enumerable"));
			}

			ArrayList list1 = new ArrayList ();
			ArrayList list2 = new ArrayList ();
			foreach (object item in expected) {
				list1.Add (item);
			}
			foreach (object item in actual) {
				list2.Add (item);
			}
			Assert.AreEqual (list1.Count, list2.Count, string.Format ("expected ienumerable length not equal"));
			for (int i = 0; i < list1.Count; i++) {
				AreObjectsEqual (list1 [i], list2 [i], string.Format ("{0}[{1}]", "enumerable", i));
			}
		}

		private static void AreTypeEqual<T> (T expected, T actual, string levelName)
		{
			if (Object.Equals (expected, null) && Object.Equals (actual, null)) {
				return;
			}
			if (Object.ReferenceEquals (expected, actual)) {
				return;
			}

			if ((!Object.Equals (expected, null) && Object.Equals (actual, null)) || (Object.Equals (expected, null) && !Object.Equals (actual, null))) {
				Assert.AreEqual (expected, actual, string.Format ("{0} expected value not equal", levelName));
			}
			Type expectedType = typeof (T);
			PropertyInfo [] properties = expectedType.GetProperties (BindingFlags.Instance | BindingFlags.Public);

			foreach (PropertyInfo property in properties) {
				object obj1 = expectedType.InvokeMember (property.Name,
								  BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
								  null, expected, null);
				object obj2 = expectedType.InvokeMember (property.Name,
								  BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
								  null, actual, null);
				AreObjectsEqual (obj1, obj2, string.Format ("{0}.{1}", levelName, property.Name));
			}
		}

		private static void AreObjectsEqual (object expected, object actual, string levelName)
		{
			// 若为相同引用，则通过验证
			if (Object.Equals (expected, null) && Object.Equals (actual, null)) {
				return;
			}
			if (Object.ReferenceEquals (expected, actual)) {
				return;
			}
			if ((!Object.Equals (expected, null) && Object.Equals (actual, null)) || (Object.Equals (expected, null) && !Object.Equals (actual, null))) {
				Assert.AreEqual (expected, actual, string.Format ("{0} expected value not equal", levelName));
			}
			Type expectedType = expected.GetType ();
			Type actualType = actual.GetType ();
			// 判断类型是否相同  
			Assert.AreEqual (expectedType, actualType, string.Format ("{0} expected type not equal", levelName));

			TypeCode typeCode = Type.GetTypeCode (expectedType);
			if (typeCode == TypeCode.Object) {
				if (expectedType.IsArray) {
					Array arr1 = (Array)expected;
					Array arr2 = (Array)actual;
					Assert.AreEqual (arr1.Length, arr2.Length, string.Format ("{0} expected array length not equal", levelName));
					for (int i = 0; i < arr1.Length; i++) {
						AreObjectsEqual (arr1.GetValue (i), arr2.GetValue (i), string.Format ("{0}[{1}]", levelName, i));
					}
				} else {
					IEnumerable ie1 = expected as IEnumerable;
					if (ie1 != null) {
						IEnumerable ie2 = actual as IEnumerable;
						ArrayList list1 = new ArrayList ();
						ArrayList list2 = new ArrayList ();
						foreach (object item in ie1) {
							list1.Add (item);
						}
						foreach (object item in ie2) {
							list2.Add (item);
						}
						Assert.AreEqual (list1.Count, list2.Count, string.Format ("{0} expected ienumerable length not equal", levelName));
						for (int i = 0; i < list1.Count; i++) {
							AreObjectsEqual (list1 [i], list2 [i], string.Format ("{0}[{1}]", levelName, i));
						}
					} else {
						PropertyInfo [] properties = expectedType.GetProperties (BindingFlags.Instance | BindingFlags.Public);

						foreach (PropertyInfo property in properties) {
							object obj1 = expectedType.InvokeMember (property.Name,
											  BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
											  null, expected, null);
							object obj2 = expectedType.InvokeMember (property.Name,
											  BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
											  null, actual, null);
							AreObjectsEqual (obj1, obj2, string.Format ("{0}.{1}", levelName, property.Name));
						}
					}
				}
			} else if (typeCode == TypeCode.Empty || typeCode == TypeCode.DBNull) {
				return;
			} else {
				if (typeCode == TypeCode.Double) {
					double d1 = (double)expected;
					double d2 = (double)actual;
					Assert.AreEqual (d1, d2, 0.00001d, string.Format ("{0} expected value not equal", levelName));
				} else {
					Assert.AreEqual (expected, actual, string.Format ("{0} expected value not equal", levelName));
				}
			}
			// 测试属性是否相等  


		}

	}
}

