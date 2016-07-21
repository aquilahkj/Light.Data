using System;
namespace Light.Data
{
	class CastStringDataFielfInfo : ExtendDataFieldInfo
	{
		string _format;

		public CastStringDataFielfInfo (DataFieldInfo info, string format)
			: base (info)
		{
			_format = format;
		}

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	object format = LambdaExpressionExtend.ConvertObject (_format, factory, isFullName, false);
		//	return factory.CreateCastStringSql (field, (string)format);
		//}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters);
			//object format = LambdaExpressionExtend.ConvertObject (_format, factory, isFullName, false);
			return factory.CreateCastStringSql (field, _format);
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

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				CastStringDataFielfInfo target = info as CastStringDataFielfInfo;
				return this._format == target._format;
			}
			else {
				return false;
			}
		}
	}
}

