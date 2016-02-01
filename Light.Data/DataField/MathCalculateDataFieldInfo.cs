using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class MathCalculateDataFieldInfo : ExtendDataFieldInfo
	{
		object _value = 0;

		MathOperator _opera;

		bool _forward;

		internal MathCalculateDataFieldInfo (DataFieldInfo info, MathOperator opera, object value, bool forward)
			: base (info)
		{
			_forward = forward;
			_opera = opera;
			_value = value;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			string sql = null;
			switch (_opera) {
			case MathOperator.Puls:
				sql = factory.CreatePlusSql (field, _value, _forward);
				break;
			case MathOperator.Minus:
				sql = factory.CreateMinusSql (field, _value, _forward);
				break;
			case MathOperator.Multiply:
				sql = factory.CreateMultiplySql (field, _value, _forward);
				break;
			case MathOperator.Divided:
				sql = factory.CreateDividedSql (field, _value, _forward);
				break;
			case MathOperator.Mod:
				sql = factory.CreateModSql (field, _value, _forward);
				break;
			case MathOperator.Power:
				sql = factory.CreatePowerSql (field, _value, _forward);
				break;
			}
			return sql;
		}

		internal override string DBType {
			get {
//				if (_opera == MathOperator.Divided) {
//					return "double";
//				}
//				else if(_value.GetType()=={
//					return base.DBType;
//				}
				return string.Empty;
			}
		}

		internal override object ToColumn (object value)
		{
			return value;
//			return base.ToColumn (value);
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				MathCalculateDataFieldInfo target = info as MathCalculateDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return this._opera == target._opera && this._forward == target._forward && Object.Equals (this._value, target._value);
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
