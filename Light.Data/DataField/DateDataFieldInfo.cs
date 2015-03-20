using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.DataField
{
	/// <summary>
	/// 日期格式数据字段
	/// </summary>
	class DateDataFieldInfo : ExtendDataFieldInfo
	{
		string _format = null;

		internal DateDataFieldInfo (DataFieldInfo info, string format)
			: base (info)
		{
			DataFieldMapping fieldMapping = info.DataField;
			_format = format;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateDateSql (field, _format);
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				DateDataFieldInfo target = info as DateDataFieldInfo;
				return this._format == target._format;
			}
			else {
				return false;
			}
		}
	}
}
