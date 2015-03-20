using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 基本字段信息
	/// </summary>
	public abstract class BasicFieldInfo
	{
		DataFieldMapping _dataField = null;

		internal DataFieldMapping DataField {
			get {
				return _dataField;
			}
			set {
				_dataField = value;
			}
		}

		DataEntityMapping _tableMapping = null;

		internal DataEntityMapping TableMapping {
			get {
				return _tableMapping;
			}
			set {
				_tableMapping = value;
			}
		}

		/// <summary>
		/// 获取字段名称
		/// </summary>
		public string FieldName {
			get {
				return DataField.Name;
			}
		}

	}
}
