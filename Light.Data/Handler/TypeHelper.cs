using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Handler
{
	static class TypeHelper
	{
		/// <summary>
		/// 判断target类型是否继承于parent类型
		/// </summary>
		/// <param name="target">目标类型</param>
		/// <param name="parent">父类型</param>
		/// <returns>是否</returns>
		public static bool IsParentType (Type target, Type parent)
		{
			if (target == null || parent == null || target == parent || target.BaseType == null) {
				return false;
			}
			if (parent.IsInterface) {
				foreach (Type t in target.GetInterfaces()) {
					if (t == parent) {
						return true;
					}
				}
			}
			else {
				do {
					if (target.BaseType == parent) {
						return true;
					}
					target = target.BaseType;
				}
				while (target != null);
			}
			return false;
		}
	}
}
