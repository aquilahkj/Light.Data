
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

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	object format = LambdaExpressionExtend.ConvertObject (_format, factory, isFullName, false);
		//	return factory.CreateDateSql (field, (string)format);
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateSqlString (factory, isFullName, out dataParameters);
			return factory.CreateDateSql (field, _format);
		}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string field = BaseFieldInfo.CreateSqlString (factory, isFullName, state);
			return factory.CreateDateSql (field, _format);
		}

		//internal virtual string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	dataParameters = null;
		//	return CreateDataFieldSql (factory, isFullName);
		//}

		internal override string DBType {
			get {
				return "string";
			}
		}

		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	if (base.EqualsDetail (info)) {
		//		DateDataFieldInfo target = info as DateDataFieldInfo;
		//		return this._format == target._format;
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
