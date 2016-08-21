using System;

namespace Light.Data
{
	/// <summary>
	/// Select field info.
	/// </summary>
	public abstract class SelectFieldInfo
	{
		/// <param name="info">Info.</param>
		public static implicit operator SelectFieldInfo (DataFieldInfo info)
		{
			return new DataSelectFieldInfo (info);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (int value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (long value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (short value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (uint value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (ulong value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (ushort value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (float value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (double value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (decimal value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (char value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (DateTime value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">If set to <c>true</c> value.</param>
		public static implicit operator SelectFieldInfo (bool value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		/// <param name="value">If set to <c>true</c> value.</param>
		public static implicit operator SelectFieldInfo (Enum value)
		{
			return new EnumSelectFieldInfo (value);
		}

		/// <param name="value">Value.</param>
		public static implicit operator SelectFieldInfo (string value)
		{
			return new ConstantSelectFieldInfo (value);
		}

		static readonly ConstantSelectFieldInfo NullInstance = new ConstantSelectFieldInfo (null);

		/// <summary>
		/// Gets the null.
		/// </summary>
		/// <value>The null.</value>
		public static SelectFieldInfo Null {
			get {
				return NullInstance;
			}
		}

		internal abstract string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters);

		internal abstract string CreateSqlString (CommandFactory factory, CreateSqlState state);

		internal abstract DataEntityMapping TableMapping {
			get;
		}
	}
}

