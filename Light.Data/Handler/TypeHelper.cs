using System;

namespace Light.Data
{
	static class TypeHelper
	{
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
