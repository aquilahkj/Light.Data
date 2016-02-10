using System;

namespace Light.Data
{
	/// <summary>
	/// 批量更新设置值
	/// </summary>
	public class UpdateSetValue
	{
		readonly DataFieldInfo _dataField = null;

		readonly object _value = null;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dataField">要更新的数据字段</param>
		/// <param name="value">更新值</param>
		public UpdateSetValue (DataFieldInfo dataField, object value)
		{
			if (Object.Equals (dataField, null)) {
				throw new ArgumentNullException ("DataField");
			}
			_dataField = dataField;
			_value = value;
		}

		/// <summary>
		/// 数据字段
		/// </summary>
		public DataFieldInfo DataField {
			get {
				return _dataField;
			}
		}

		/// <summary>
		/// 数据值
		/// </summary>
		public object Value {
			get {
				return _value;
			}
		}
	}
}
