using System;

namespace Light.Data
{
	class BooleanDataFieldInfo:CommonDataFieldInfo
	{
		static BooleanDataFieldInfo instanceTrue = new BooleanDataFieldInfo (true);

		internal static BooleanDataFieldInfo InstanceTrue {
			get {
				return instanceTrue;
			}
		}

		static BooleanDataFieldInfo instanceFalse = new BooleanDataFieldInfo (false);

		internal static BooleanDataFieldInfo InstanceFalse {
			get {
				return instanceFalse;
			}
		}

		readonly bool _value;

		private BooleanDataFieldInfo (bool value) : base (typeof(BolleanType), "Value")
		{
			this._value = value;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			return factory.CreateBooleanSql (_value);
		}

		/// <summary>
		/// 匹配细节内容是否相等
		/// </summary>
		/// <param name="info">匹配对象</param>
		/// <returns></returns>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			BooleanDataFieldInfo target = info as BooleanDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._value == target._value;
			}
			else {
				return false;
			}
		}

		[DataTable]
		class BolleanType
		{
			[DataField]
			public string Value {
				get;
				set;
			}
		}
	}
}

