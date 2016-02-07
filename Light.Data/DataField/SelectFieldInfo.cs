using System;

namespace Light.Data
{
	/// <summary>
	/// Select field info.
	/// </summary>
	public abstract class SelectFieldInfo
	{
		//		/// <summary>
		//		/// Gets the db time data field.
		//		/// </summary>
		//		/// <value>The db time data field.</value>
		//		public static DataFieldInfo DbTimeDataField {
		//			get {
		//				return DbTimeDataFieldInfo.Instance;
		//			}
		//		}
		//
		//		/// <summary>
		//		/// Gets the true data field.
		//		/// </summary>
		//		/// <value>The true data field.</value>
		//		public static DataFieldInfo TrueDataField {
		//			get {
		//				return BooleanDataFieldInfo.InstanceTrue;
		//			}
		//		}
		//
		//		/// <summary>
		//		/// Gets the false data field.
		//		/// </summary>
		//		/// <value>The false data field.</value>
		//		public static DataFieldInfo FalseDataField {
		//			get {
		//				return BooleanDataFieldInfo.InstanceFalse;
		//			}
		//		}
		//
		//		/// <summary>
		//		/// Creates the string data field.
		//		/// </summary>
		//		/// <returns>The string data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateStringDataField (string value)
		//		{
		//			return new StringDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the int data field.
		//		/// </summary>
		//		/// <returns>The int data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (int value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the int data field.
		//		/// </summary>
		//		/// <returns>The int data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (long value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the int data field.
		//		/// </summary>
		//		/// <returns>The int data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (short value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (uint value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (ulong value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (ushort value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (decimal value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (float value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}
		//
		//		/// <summary>
		//		/// Creates the number data field.
		//		/// </summary>
		//		/// <returns>The number data field.</returns>
		//		/// <param name="value">Value.</param>
		//		public static DataFieldInfo CreateNumberDataField (double value)
		//		{
		//			return new NumberDataFieldInfo (value);
		//		}


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

		internal abstract string CreateDataFieldSql (CommandFactory factory, out DataParameter dataParameter);

		internal abstract DataEntityMapping TableMapping {
			get;
		}
	}
}

