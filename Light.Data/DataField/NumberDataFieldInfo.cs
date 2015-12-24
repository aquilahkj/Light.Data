using System;

namespace Light.Data
{
	/// <summary>
	/// Number data field info.
	/// </summary>
	class NumberDataFieldInfo:CommonDataFieldInfo
	{
		readonly object _value;

		internal NumberDataFieldInfo (object value) : base (typeof(NumberType), "Value")
		{
			if (value == null)
				throw new ArgumentNullException ("value");
			this._value = value;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			return factory.CreateNumberSql (_value);
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			NumberDataFieldInfo target = info as NumberDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._value == target._value;
			}
			else {
				return false;
			}
		}

		[DataTable]
		class NumberType
		{
			[DataField]
			public string Value {
				get;
				set;
			}
		}
	}
}

