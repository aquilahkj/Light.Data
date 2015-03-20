using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.DataField
{
	class MathCalculateDataFieldInfo : ExtendDataFieldInfo
	{
		object _value = 0;

		MathOperator _opera;

		internal MathCalculateDataFieldInfo (DataFieldInfo info, MathOperator opera, object value)
			: base (info)
		{
			_opera = opera;
			_value = value;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			string sql = null;
			switch (_opera) {
				case MathOperator.Puls:
					sql = factory.CreatePlusSql (field, _value);
					break;
				case MathOperator.Minus:
					sql = factory.CreateMinusSql (field, _value);
					break;
				case MathOperator.Multiply:
					sql = factory.CreateMultiplySql (field, _value);
					break;
				case MathOperator.Divided:
					sql = factory.CreateDividedSql (field, _value);
					break;
				case MathOperator.Mod:
					sql = factory.CreateModSql (field, _value);
					break;
				case MathOperator.Power:
					sql = factory.CreatePowerSql (field, _value);
					break;
			}
			return sql;
		}

		internal override string DBType {
			get {
				return string.Empty;
			}
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			if (base.EqualsDetail (info)) {
				MathCalculateDataFieldInfo target = info as MathCalculateDataFieldInfo;
				return this._opera == target._opera && Object.Equals (this._value, target._value);
			}
			else {
				return false;
			}
		}
	}
}
