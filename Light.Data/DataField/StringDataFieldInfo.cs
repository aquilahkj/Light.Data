using System;

namespace Light.Data
{
	/// <summary>
	/// String data field info.
	/// </summary>
	class StringDataFieldInfo:CommonDataFieldInfo
	{
		readonly string _value;

		internal StringDataFieldInfo (string value) : base (typeof(StringType), "Value")
		{
			if (value == null)
				throw new ArgumentNullException ("value");
			this._value = value;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			return factory.CreateStringSql (_value);
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			StringDataFieldInfo target = info as StringDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._value == target._value;
			}
			else {
				return false;
			}
		}

		[DataTable]
		class StringType
		{
			[DataField]
			public string Value {
				get;
				set;
			}
		}
	}
}

