using System;
using System.Collections.Generic;

namespace Light.Data
{
	class LambdaStringFunctionDataFieldInfo : LambdaDataFieldInfo
	{

		StringFunction _function;

		object _callObject;

		object [] _argsObjects;

		public LambdaStringFunctionDataFieldInfo (DataFieldInfo info, StringFunction function, object callObject, params object [] argsObjects)
			: base (info)
		{
			if (callObject == null)
				throw new ArgumentNullException (nameof (callObject));
			if (function == StringFunction.ToLower || function == StringFunction.ToUpper || function == StringFunction.Trim) {
				if (argsObjects != null && argsObjects.Length > 0) {
					throw new ArgumentNullException (nameof (argsObjects));
				}
			}
			if (function == StringFunction.Replace) {
				if (argsObjects == null || argsObjects.Length != 2) {
					throw new ArgumentNullException (nameof (argsObjects));
				}
			}
			if (function == StringFunction.Substring || function == StringFunction.IndexOf) {
				if (argsObjects == null || argsObjects.Length > 2) {
					throw new ArgumentNullException (nameof (argsObjects));
				}
			}
			_function = function;
			_callObject = callObject;
			_argsObjects = argsObjects;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = null;

			List<DataParameter []> parameterList = new List<DataParameter []> ();
			List<object> objectList = new List<object> ();

			object obj;
			DataParameter [] dataParameters0 = null;
			DataFieldInfo info = _callObject as DataFieldInfo;
			if (!Object.Equals (info, null)) {
				obj = info.CreateDataFieldSql (factory, isFullName, out dataParameters0);
			}
			else {
				obj = LambdaExpressionExtend.ConvertLambdaObject (_callObject);
				string pn = factory.CreateTempParamName ();
				DataParameter dataParameter = new DataParameter (pn, obj);
				dataParameters0 = new [] { dataParameter };
				obj = dataParameter.ParameterName;
			}
			if (dataParameters0 != null) {
				parameterList.Add (dataParameters0);
			}
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
			case StringFunction.Substring:
				if (objectList.Count == 2) {
					sql = factory.CreateSubStringSql (obj, objectList [0], objectList [1]);
				}
				else {
					sql = factory.CreateSubStringSql (obj, objectList [0], null);
				}
				break;
			case StringFunction.IndexOf:
				if (objectList.Count == 2) {
					sql = factory.CreateIndexOfSql (obj, objectList [0], objectList [1]);
				}
				else {
					sql = factory.CreateIndexOfSql (obj, objectList [0], null);
				}
				break;
			case StringFunction.Replace:
				sql = factory.CreateReplaceSql (obj, objectList [0], objectList [1]);
				break;
			case StringFunction.ToLower:
				sql = factory.CreateToLowerSql (obj);
				break;
			case StringFunction.ToUpper:
				sql = factory.CreateToUpperSql (obj);
				break;
			case StringFunction.Trim:
				sql = factory.CreateTrimSql (obj);
				break;
			//case StringFunction.Lenght:
			//	sql = factory.CreateLengthSql (obj);
			//	break;

			}
			dataParameters = DataParameter.ConcatDataParameters (parameterList.ToArray ());
			return sql;
		}
	}
}

