using System;

namespace Light.Data
{
	/// <summary>
	/// 自定义字段类型，用于建立没有在数据表字段映射的字段信息(DataFieldInfo)
	/// </summary>
	class CustomFieldMapping : DataFieldMapping
	{
		public CustomFieldMapping (string fieldName, DataEntityMapping mapping)
			: base (null, fieldName, null, mapping, true, null)
		{

		}

		#region implemented abstract members of FieldMapping

		public override object ToProperty (object value)
		{
			if (Object.Equals (value, DBNull.Value)) {
				return null;
			}
			else {
				return value;
			}
		}

		public override object ToColumn (object value)
		{
			if (Object.Equals (value, DBNull.Value)) {
				return null;
			}
			else {
				return value;
			}
		}

		#endregion
	}
}
