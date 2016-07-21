using System;
namespace Light.Data
{
	class ConcatStringDataFieldInfo : ExtendDataFieldInfo
	{
		object _value;
		bool _forward;

		internal ConcatStringDataFieldInfo (DataFieldInfo info, object value, bool forward)
			: base (info)
		{
			this._value = value;
			this._forward = forward;
		}

		//internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		//{
		//	string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
		//	//string sql =  factory.CreateConcatSql (field, _value, _forward);
		//	object value = LambdaExpressionExtend.ConvertObject (_value, factory, isFullName, true);
		//	string sql = factory.CreateConcatSql (field, value, _forward);
		//	return sql;
		//}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			DataParameter [] dataParameters1 = null;
			DataParameter [] dataParameters2 = null;
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters1);
			//string sql =  factory.CreateConcatSql (field, _value, _forward);
			object value = LambdaExpressionExtend.ConvertLambdaObject (_value, factory, isFullName, true, out dataParameters2);
			string sql = factory.CreateConcatSql (field, value, _forward);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}


		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		internal override object ToParameter (object value)
		{
			return value;
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				ConcatStringDataFieldInfo target = info as ConcatStringDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return this._forward == target._forward && Object.Equals (this._value, target._value);
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}
	}
}

