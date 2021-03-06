﻿using System;

namespace Light.Data
{
	class MathFunctionDataFieldInfo : ExtendDataFieldInfo
	{
		MathFunction _function;

		internal MathFunctionDataFieldInfo (DataFieldInfo info, MathFunction function)
			: base (info)
		{
			_function = function;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			string field = BaseFieldInfo.CreateDataFieldSql (factory, isFullName);
			string sql = null;
			switch (_function) {
			case MathFunction.Abs:
				sql = factory.CreateAbsSql (field);
				break;
			case MathFunction.Log:
				sql = factory.CreateLogSql (field);
				break;
			case MathFunction.Exp:
				sql = factory.CreateExpSql (field);
				break;
			case MathFunction.Sin:
				sql = factory.CreateSinSql (field);
				break;
			case MathFunction.Cos:
				sql = factory.CreateCosSql (field);
				break;
			case MathFunction.Tan:
				sql = factory.CreateTanSql (field);
				break;
			case MathFunction.Atan:
				sql = factory.CreateAtanSql (field);
				break;
			}
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
				MathFunctionDataFieldInfo target = info as MathFunctionDataFieldInfo;
				if (!Object.Equals (target, null)) {
					return this._function == target._function;
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
