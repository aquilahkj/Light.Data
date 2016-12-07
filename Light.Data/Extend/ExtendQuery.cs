using System;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Extend query.
	/// </summary>
	public static class ExtendQuery
	{
		/// <summary>
		/// Exists expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool Exists<T> (Expression<Func<T, bool>> expression) where T : class
		{
			return true;
		}

		/// <summary>
		/// Not exists expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool NotExists<T> (Expression<Func<T, bool>> expression) where T : class
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (bool field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (byte field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (short field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (int field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (long field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (ushort field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (uint field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (ulong field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (double field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (float field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (decimal field)
		{
			return true;
		}

		/// <summary>
		/// Is null.
		/// </summary>
		/// <param name="field">If set to <c>true</c> field.</param>
		public static bool IsNull (DateTime field)
		{
			return true;
		}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (bool field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (byte field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (short field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (int field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (long field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (ushort field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (uint field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (ulong field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (double field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (float field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (decimal field)
		//{
		//	return true;
		//}

		///// <summary>
		///// Is not null.
		///// </summary>
		///// <param name="field">If set to <c>true</c> field.</param>
		//public static bool IsNotNull (DateTime field)
		//{
		//	return true;
		//}
	}
}
