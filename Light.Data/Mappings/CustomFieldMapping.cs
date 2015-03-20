using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Mappings
{
	/// <summary>
	/// 自定义字段类型，用于建立没有在数据表字段映射的字段信息(DataFieldInfo)
	/// </summary>
	class CustomFieldMapping : DataFieldMapping
	{
		public CustomFieldMapping (string fieldName, DataEntityMapping mapping)
			: base (null, fieldName, null, mapping, false, null)
		{
//			Name = fieldName;
//			TypeMapping = mapping;
		}
	}
}
