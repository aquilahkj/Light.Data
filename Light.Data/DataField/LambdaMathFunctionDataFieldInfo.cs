using System;
using System.Collections.Generic;

namespace Light.Data
{
	class LambdaMathFunctionDataFieldInfo : LambdaDataFieldInfo
	{
		MathFunction _function;

		object [] _argsObjects;

		public LambdaMathFunctionDataFieldInfo (DataFieldInfo info, MathFunction function, params object [] argsObjects)
			: base (info)
		{
			if (argsObjects == null || argsObjects.Length == 0)
				throw new ArgumentNullException (nameof (argsObjects));
			if (function == MathFunction.Atan2 || function == MathFunction.Max || function == MathFunction.Min || function == MathFunction.Pow) {
				if (argsObjects.Length != 2) {
					throw new ArgumentNullException (nameof (argsObjects));
				}
			}
			if (function == MathFunction.Log || function == MathFunction.Round) {
				if (argsObjects.Length > 2) {
					throw new ArgumentNullException (nameof (argsObjects));
				}
			}
			_function = function;
			_argsObjects = argsObjects;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = null;

			List<DataParameter []> parameterList = new List<DataParameter []> ();
			List<object> objectList = new List<object> ();
			foreach (object item in _argsObjects) {
				object obj1;
				DataParameter [] dataParameters1 = null;
				DataFieldInfo info1 = item as DataFieldInfo;
				if (!Object.Equals (info1, null)) {
					obj1 = info1.CreateDataFieldSql (factory, isFullName, out dataParameters1);
				}
				else {
					obj1 = LambdaExpressionExtend.ConvertLambdaObject (item);
					string pn = factory.CreateTempParamName ();
					DataParameter dataParameter = new DataParameter (pn, obj1);
					dataParameters1 = new [] { dataParameter };
					obj1 = dataParameter.ParameterName;
				}
				if (dataParameters1 != null) {
					parameterList.Add (dataParameters1);
				}
				objectList.Add (obj1);
			}
			switch (_function) {
			case MathFunction.Abs:
				sql = factory.CreateAbsSql (objectList [0]);
				break;
			case MathFunction.Sign:
				sql = factory.CreateSignSql (objectList [0]);
				break;
			case MathFunction.Sin:
				sql = factory.CreateSinSql (objectList [0]);
				break;
			case MathFunction.Cos:
				sql = factory.CreateCosSql (objectList [0]);
				break;
			case MathFunction.Tan:
				sql = factory.CreateTanSql (objectList [0]);
				break;
			case MathFunction.Atan:
				sql = factory.CreateAtanSql (objectList [0]);
				break;
			case MathFunction.ASin:
				sql = factory.CreateSinSql (objectList [0]);
				break;
			case MathFunction.ACos:
				sql = factory.CreateCosSql (objectList [0]);
				break;
			case MathFunction.Atan2:
				sql = factory.CreateAtan2Sql (objectList [0], objectList [1]);
				break;
			case MathFunction.Ceiling:
				sql = factory.CreateSinSql (objectList [0]);
				break;
			case MathFunction.Floor:
				sql = factory.CreateFloorSql (objectList [0]);
				break;
			case MathFunction.Round:
				sql = factory.CreateRoundSql (objectList [0], objectList [1] != null ? objectList [1] : 0);
				break;
			case MathFunction.Truncate:
				sql = factory.CreateTruncateSql (objectList [0]);
				break;
			case MathFunction.Log:
				if (objectList.Count == 2) {
					sql = factory.CreateLogSql (objectList [0], objectList [1]);
				}
				else {
					sql = factory.CreateLogSql (objectList [0]);
				}
				break;
			case MathFunction.Log10:
				sql = factory.CreateLog10Sql (objectList [0]);
				break;
			case MathFunction.Exp:
				sql = factory.CreateExpSql (objectList [0]);
				break;
			case MathFunction.Pow:
				sql = factory.CreatePowSql (objectList [0], objectList [1]);
				break;
			case MathFunction.Sqrt:
				sql = factory.CreateSqrtSql (objectList [0]);
				break;
			case MathFunction.Max:
				sql = factory.CreateMaxSql (objectList [0], objectList [1]);
				break;
			case MathFunction.Min:
				sql = factory.CreateMinSql (objectList [0], objectList [1]);
				break;
			}
			dataParameters = DataParameter.ConcatDataParameters (parameterList.ToArray ());
			return sql;
		}





		//protected override bool EqualsDetail (DataFieldInfo info)
		//{
		//	if (base.EqualsDetail (info)) {
		//		LambdaMathFunctionDataFieldInfo target = info as LambdaMathFunctionDataFieldInfo;
		//		if (!Object.Equals (target, null)) {
		//			return this._function == target._function
		//				       && Utility.EnumableObjectEquals (this._values, target._values); ;
		//		}
		//		else {
		//			return false;
		//		}
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}

