
namespace Light.Data
{
	class DateDataFieldInfo : ExtendDataFieldInfo
	{
		string _format;

		internal DateDataFieldInfo (DataFieldInfo info, string format)
			: base (info)
		{
			_format = format;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			return factory.CreateDateSql (field, _format);
		}

		internal override string DBType {
			get {
				return "string";
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
