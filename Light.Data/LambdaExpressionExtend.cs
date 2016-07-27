using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

namespace Light.Data
{
	static class LambdaExpressionExtend
	{
		static LambdaExpressionExtend ()
		{

		}

		internal static object ConvertLambdaObject (object value)
		{
			object obj;
			Delegate dele = value as Delegate;
			if (dele != null) {
				obj = dele.DynamicInvoke (null);
			}
			else {
				obj = value;
			}
			return obj;
		}

		private static bool CheckCatchOperatorsType (ExpressionType expressionType, out CatchOperatorsType catchType)
		{
			if (expressionType == ExpressionType.And || expressionType == ExpressionType.AndAlso) {
				catchType = CatchOperatorsType.AND;
				return true;
			}
			else if (expressionType == ExpressionType.Or || expressionType == ExpressionType.OrElse) {
				catchType = CatchOperatorsType.OR;
				return true;
			}
			else {
				catchType = CatchOperatorsType.AND;
				return false;
			}
		}

		private static bool CheckQueryPredicate (ExpressionType expressionType, out QueryPredicate queryPredicate)
		{
			bool ret;
			switch (expressionType) {
			case ExpressionType.Equal:
				queryPredicate = QueryPredicate.Eq;
				ret = true;
				break;
			case ExpressionType.NotEqual:
				queryPredicate = QueryPredicate.NotEq;
				ret = true;
				break;
			case ExpressionType.GreaterThan:
				queryPredicate = QueryPredicate.Gt;
				ret = true;
				break;
			case ExpressionType.GreaterThanOrEqual:
				queryPredicate = QueryPredicate.GtEq;
				ret = true;
				break;
			case ExpressionType.LessThan:
				queryPredicate = QueryPredicate.Lt;
				ret = true;
				break;
			case ExpressionType.LessThanOrEqual:
				queryPredicate = QueryPredicate.LtEq;
				ret = true;
				break;
			default:
				queryPredicate = QueryPredicate.Eq;
				ret = false;
				break;
			}
			return ret;
		}

		private static bool CheckMathOperator (ExpressionType expressionType, out MathOperator mathOperator)
		{
			bool ret;
			switch (expressionType) {
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
				mathOperator = MathOperator.Puls;
				ret = true;
				break;
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				mathOperator = MathOperator.Minus;
				ret = true;
				break;
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				mathOperator = MathOperator.Multiply;
				ret = true;
				break;
			case ExpressionType.Divide:
				mathOperator = MathOperator.Divided;
				ret = true;
				break;
			case ExpressionType.Modulo:
				mathOperator = MathOperator.Mod;
				ret = true;
				break;
			case ExpressionType.Power:
			case ExpressionType.ExclusiveOr:
				mathOperator = MathOperator.Power;
				ret = true;
				break;
			default:
				mathOperator = MathOperator.Puls;
				ret = false;
				break;
			}
			return ret;
		}

		public static OrderExpression ResolveLambdaOrderByExpression (LambdaExpression expression, OrderType orderType)
		{
			LambdaParameterGroup parameters = new LambdaParameterGroup (expression);
			DataFieldInfo dataFieldInfo;
			bool ret;
			try {
				ret = ParseDataFieldInfo (expression.Body, parameters, out dataFieldInfo);
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse order expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
			if (ret) {
				OrderExpression exp = new FieldOrderExpression (dataFieldInfo, orderType);
				return exp;
			}
			else {
				throw new LightDataException (string.Format ("not valid field in order expression \"{0}\"", expression));
			}
		}

		public static QueryExpression ResolveLambdaQueryExpression (LambdaExpression expression)
		{
			LambdaParameterGroup parameters = new LambdaParameterGroup (expression);
			try {
				return ResolveQueryExpression (expression.Body, parameters);
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse query expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
		}

		private static DataFieldInfo CreateDateDataFieldInfo (MemberInfo member, DataFieldInfo fieldInfo)
		{
			if (member.Name == "Date") {
				return fieldInfo = new LambdaDateDataFieldInfo (fieldInfo);
			}
			else {
				DatePart datePart;
				if (Enum.TryParse<DatePart> (member.Name, out datePart)) {
					return new LambdaDatePartDataFieldInfo (fieldInfo, datePart);
				}
			}
			throw new LambdaParseException (string.Format ("DateTime.{0} member not support", member.Name));
		}

		private static DataFieldInfo CreateStringMemberDataFieldInfo (MemberInfo member, DataFieldInfo fieldInfo)
		{
			switch (member.Name) {
			case "Length":
				fieldInfo = new LambdaStringLengthDataFieldInfo (fieldInfo);
				break;
			default:
				throw new LambdaParseException (string.Format ("String.{0} member not support", member.Name));
			}
			return fieldInfo;
		}

		private static bool ParseArguments (ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, LambdaParameterGroup parameters, out object [] argObjects, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			argObjects = null;
			if (arguments.Count == 0) {
				return false;
			}
			object [] array = new object [arguments.Count];
			bool hasFieldInfo = false;
			for (int i = 0; i < arguments.Count; i++) {
				System.Linq.Expressions.Expression arg = arguments [i];
				DataFieldInfo argFieldInfo;
				if (ParseDataFieldInfo (arg, parameters, out argFieldInfo)) {
					hasFieldInfo = true;
					if (Object.Equals (fieldInfo, null)) {
						fieldInfo = argFieldInfo;
					}
					array [i] = argFieldInfo;
				}
				else {
					array [i] = ConvertObject (arg);
				}
			}
			if (!hasFieldInfo) {
				argObjects = array;
				return false;
			}
			else {
				argObjects = array;
				return true;
			}
		}

		private static DataFieldInfo ParseMathFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaParameterGroup parameters)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			MathFunction mathFunction;
			if (Enum.TryParse<MathFunction> (method.Name, out mathFunction)) {
				if (parameterInfos == null || parameterInfos.Length == 0) {
					throw new LambdaParseException (string.Format ("Math.{0} method args error", method.Name));
				}
				else if (mathFunction == MathFunction.Atan2 || mathFunction == MathFunction.Max || mathFunction == MathFunction.Min || mathFunction == MathFunction.Pow) {
					if (parameterInfos.Length != 2) {
						throw new LambdaParseException (string.Format ("Math.{0} method args error", method.Name));
					}
				}
				else if (mathFunction == MathFunction.Log || mathFunction == MathFunction.Round) {
					if (parameterInfos.Length > 2) {
						throw new LambdaParseException (string.Format ("Math.{0} method args error", method.Name));
					}
				}
				return new LambdaMathFunctionDataFieldInfo (mainFieldInfo, mathFunction, argObjects);
			}
			throw new LambdaParseException (string.Format ("Math.{0} method not support", method.Name));
		}

		private static DataFieldInfo ParseStaticeStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaParameterGroup parameters)
		{
			if (method.Name == "Concat") {
				if (argObjects.Length == 1) {
					LambdaNewArrayDataFieldInfo newarray = argObjects [0] as LambdaNewArrayDataFieldInfo;
					if (!Object.Equals (newarray, null)) {
						return new LambdaStringConcatDataFieldInfo (newarray.BaseFieldInfo, newarray.Values);
					}
					else {
						return new LambdaStringConcatDataFieldInfo (mainFieldInfo, argObjects);
					}
				}
				else {
					return new LambdaStringConcatDataFieldInfo (mainFieldInfo, argObjects);
				}
			}
			throw new LambdaParseException (string.Format ("String.{0} method not support", method.Name));
		}

		private static DataFieldInfo ParseInstanceStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, LambdaParameterGroup parameters)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			if (method.Name == "StartsWith") {
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (string.Format ("String.{0} method args error", method.Name));
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, true, false, callObject, argObjects [0]);
			}
			else if (method.Name == "EndsWith") {
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (string.Format ("String.{0} method args error", method.Name));
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, false, true, callObject, argObjects [0]);
			}
			else if (method.Name == "Contains") {
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (string.Format ("String.{0} method args error", method.Name));
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, true, true, callObject, argObjects [0]);
			}
			else {
				StringFunction stringFunction;
				if (Enum.TryParse<StringFunction> (method.Name, out stringFunction)) {
					if (stringFunction == StringFunction.IndexOf && !(parameterInfos.Length == 1 || (parameterInfos.Length == 2 && parameterInfos [1].ParameterType == typeof (int)))) {
						throw new LambdaParseException (string.Format ("String.{0} method args error", method.Name));
					}
					else if (stringFunction == StringFunction.Trim && parameterInfos.Length > 0) {
						throw new LambdaParseException (string.Format ("String.{0} method args error", method.Name));
					}
					return new LambdaStringFunctionDataFieldInfo (mainFieldInfo, stringFunction, callObject, argObjects);
				}
			}
			throw new LambdaParseException (string.Format ("String.{0} method not support", method.Name));
		}

		private static DataFieldInfo ParseInstanceDateTimeFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, LambdaParameterGroup parameters)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			if (method.Name == "ToString") {
				if (parameterInfos.Length == 0) {
					return new LambdaDateFormatDataFieldInfo (mainFieldInfo, null);
				}
				else if (parameterInfos.Length == 1) {
					if (parameterInfos [0].ParameterType == typeof (string)) {
						object o = ConvertLambdaObject (argObjects [0]);
						return new LambdaDateFormatDataFieldInfo (mainFieldInfo, o as string);
					}
					else {
						throw new LambdaParseException (string.Format ("DateTime.{0} method args error", method.Name));
					}
				}
				else {
					throw new LambdaParseException (string.Format ("DateTime.{0} method args error", method.Name));
				}
			}
			throw new LambdaParseException (string.Format ("DateTime.{0} method not support", method.Name));
		}

		static DataFieldInfo ParseContainsDataFieldInfo (MethodInfo methodInfo, DataFieldInfo mainFieldInfo, object collections, LambdaParameterGroup parameters)
		{
			return new LambdaContainsDataFieldInfo (mainFieldInfo, collections);
		}

		private static bool ParseDataFieldInfo (System.Linq.Expressions.Expression expression, LambdaParameterGroup parameters, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			if (expression.NodeType == ExpressionType.Constant) {
				return false;
			}
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				if (binary.Method != null && binary.NodeType == ExpressionType.Add && binary.Method.DeclaringType == typeof (string) && binary.Method.Name == "Concat") {
					DataFieldInfo leftFieldInfo;
					object leftValue = null;
					if (!ParseDataFieldInfo (binary.Left, parameters, out leftFieldInfo)) {
						leftValue = ConvertObject (binary.Left);
					}
					DataFieldInfo rightFieldInfo;
					object rightValue = null;
					if (!ParseDataFieldInfo (binary.Right, parameters, out rightFieldInfo)) {
						rightValue = ConvertObject (binary.Right);
					}
					if (!Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo, leftFieldInfo, rightFieldInfo);
						return true;
					}
					else if (!Object.Equals (leftFieldInfo, null) && Object.Equals (rightFieldInfo, null)) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo, leftFieldInfo, rightValue);
						return true;
					}
					else if (Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (rightFieldInfo, leftValue, rightFieldInfo);
						return true;
					}
					else {
						return false;
					}
				}
				else {
					MathOperator mathOperator;
					if (CheckMathOperator (binary.NodeType, out mathOperator)) {
						DataFieldInfo leftFieldInfo;
						object leftValue = null;
						if (!ParseDataFieldInfo (binary.Left, parameters, out leftFieldInfo)) {
							leftValue = ConvertObject (binary.Left);
						}
						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						if (!ParseDataFieldInfo (binary.Right, parameters, out rightFieldInfo)) {
							rightValue = ConvertObject (binary.Right);
						}

						if (!Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo, mathOperator, leftFieldInfo, rightFieldInfo);
							return true;
						}
						else if (!Object.Equals (leftFieldInfo, null) && Object.Equals (rightFieldInfo, null)) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo, mathOperator, leftFieldInfo, rightValue);
							return true;
						}
						else if (Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (rightFieldInfo, mathOperator, leftValue, rightFieldInfo);
							return true;
						}
						else {
							return false;
						}
					}
					else {
						throw new LambdaParseException (string.Format ("not support node type {0}", binary.NodeType));
					}
				}
			}
			UnaryExpression unary = expression as UnaryExpression;
			if (unary != null) {
				if (unary.NodeType == ExpressionType.Not) {
					DataFieldInfo notfieldInfo;
					if (!ParseDataFieldInfo (unary.Operand, parameters, out notfieldInfo)) {
						return false;
					}
					ISupportNotDefine notDefine = notfieldInfo as ISupportNotDefine;
					if (notDefine != null) {
						notDefine.SetNot ();
						fieldInfo = notfieldInfo;
					}
					else {
						fieldInfo = new LambdaNotDataFieldInfo (notfieldInfo);
					}
					return true;
				}
				else if (unary.NodeType == ExpressionType.Convert) {
					DataFieldInfo convertfieldInfo;
					if (!ParseDataFieldInfo (unary.Operand, parameters, out convertfieldInfo)) {
						return false;
					}
					else {
						fieldInfo = convertfieldInfo;
						return true;
					}
				}
			}
			MemberExpression member = expression as MemberExpression;
			if (member != null) {
				if (member.Expression != null) {
					ParameterExpression param = member.Expression as ParameterExpression;
					if (param != null) {
						DataEntityMapping mapping;
						if (parameters.TryGet (param.Name, out mapping) && param.Type == mapping.ObjectType) {
							fieldInfo = new DataFieldInfo (param.Type, member.Member.Name);
							return true;
						}
						else {
							throw new LambdaParseException (string.Format ("parameter not correct,name:{0},type:{1}", param.Name, param.Type));
						}
					}
					if (ParseDataFieldInfo (member.Expression, parameters, out fieldInfo)) {
						if (member.Expression.Type == typeof (DateTime)) {
							fieldInfo = CreateDateDataFieldInfo (member.Member, fieldInfo);
							return true;
						}
						else if (member.Expression.Type == typeof (string)) {
							fieldInfo = CreateStringMemberDataFieldInfo (member.Member, fieldInfo);
							return true;
						}
						else {
							throw new LambdaParseException (string.Format ("{0} type not support", member.Expression.Type));
						}
					}
					else {
						return false;
					}
				}
				else {
					return false;
				}
			}
			MethodCallExpression methodcall = expression as MethodCallExpression;
			if (methodcall != null) {
				MethodInfo methodInfo = methodcall.Method;
				if ((methodInfo.Attributes & MethodAttributes.Static) == MethodAttributes.Static) {
					object [] argObjects;
					DataFieldInfo mainFieldInfo;
					if (ParseArguments (methodcall.Arguments, parameters, out argObjects, out mainFieldInfo)) {
						if (methodInfo.DeclaringType == typeof (Math)) {
							fieldInfo = ParseMathFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, parameters);
							return true;
						}
						if (methodInfo.DeclaringType == typeof (string)) {
							fieldInfo = ParseStaticeStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, parameters);
							return true;
						}
						else {
							throw new LambdaParseException (string.Format ("{0} type not support", member.Expression.Type));
						}
					}
					else {
						return false;
					}
				}
				else {
					DataFieldInfo mainFieldInfo = null;
					DataFieldInfo callFieldInfo;
					object callObject = null;
					if (ParseDataFieldInfo (methodcall.Object, parameters, out callFieldInfo)) {
						mainFieldInfo = callFieldInfo;
						callObject = callFieldInfo;
					}
					else {
						callObject = ConvertObject (methodcall.Object);
					}

					object [] argObjects;
					DataFieldInfo mainArgFieldInfo;
					if (ParseArguments (methodcall.Arguments, parameters, out argObjects, out mainArgFieldInfo)) {
						if (Object.Equals (mainFieldInfo, null)) {
							mainFieldInfo = mainArgFieldInfo;
						}
					}
					if (Object.Equals (mainFieldInfo, null)) {
						return false;
					}

					if (methodcall.Object.Type == typeof (string)) {
						fieldInfo = ParseInstanceStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, parameters);
						return true;
					}
					if (methodcall.Object.Type == typeof (DateTime)) {
						fieldInfo = ParseInstanceDateTimeFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, parameters);
						return true;
					}
					if (Object.Equals (callFieldInfo, null) && argObjects != null && argObjects.Length == 1 && methodInfo.Name == "Contains" && typeof (IEnumerable).IsAssignableFrom (methodInfo.DeclaringType)) {
						fieldInfo = ParseContainsDataFieldInfo (methodInfo, mainFieldInfo, callObject, parameters);
						return true;
					}
				}
			}
			NewArrayExpression newarray = expression as NewArrayExpression;
			if (newarray != null) {
				object [] argsObjects;
				DataFieldInfo arrayFieldInfo;
				if (ParseArguments (newarray.Expressions, parameters, out argsObjects, out arrayFieldInfo)) {
					fieldInfo = new LambdaNewArrayDataFieldInfo (arrayFieldInfo, argsObjects);
					return true;
				}
				else {
					return false;
				}
			}
			throw new LambdaParseException (string.Format ("lambda expression [{0}] not support", expression));
		}

		public static object ConvertObject (System.Linq.Expressions.Expression expression)
		{
			ConstantExpression constant = expression as ConstantExpression;
			if (constant != null) {
				return constant.Value;
			}
			else {
				LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda (expression);
				Delegate fn = lambda.Compile ();
				return fn;
			}
		}

		private static QueryExpression ResolveQueryExpression (System.Linq.Expressions.Expression expression, LambdaParameterGroup parameters)
		{
			//LambdaExpression lambda = expression as LambdaExpression;
			//if (lambda != null) {
			//	return ResolveLambdaQueryExpression (lambda);
			//}
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				CatchOperatorsType catchType;
				if (CheckCatchOperatorsType (binary.NodeType, out catchType)) {
					var left = ResolveQueryExpression (binary.Left, parameters);
					var right = ResolveQueryExpression (binary.Right, parameters);
					return QueryExpression.Catch (left, catchType, right);
				}
				else {
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binary.NodeType, out queryPredicate)) {
						DataFieldInfo leftFieldInfo;
						object leftValue = null;
						if (!ParseDataFieldInfo (binary.Left, parameters, out leftFieldInfo)) {
							leftValue = ConvertObject (binary.Left);
						}
						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						if (!ParseDataFieldInfo (binary.Right, parameters, out rightFieldInfo)) {
							rightValue = ConvertObject (binary.Right);
						}
						if (!Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightFieldInfo);
						}
						else if (!Object.Equals (leftFieldInfo, null) && Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightValue);
						}
						else if (Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (rightFieldInfo.TableMapping, queryPredicate, rightFieldInfo, leftValue);
						}
						else {
							throw new LambdaParseException (string.Format ("not allow two constant value in BinaryExpression \"{0}\"", binary));
						}
					}
				}
			}
			UnaryExpression unary = expression as UnaryExpression;
			if (unary != null) {
				if (unary.NodeType == ExpressionType.Not) {
					QueryExpression queryExpression = ResolveQueryExpression (unary.Operand, parameters);
					ISupportNotDefine notDefine = queryExpression as ISupportNotDefine;
					if (notDefine != null) {
						notDefine.SetNot ();
						return queryExpression;
					}
					else {
						return new LambdaNotQueryExpression (queryExpression);
					}
				}
			}
			MemberExpression member = expression as MemberExpression;
			if (member != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, parameters, out fieldInfo)) {
					throw new LambdaParseException (string.Format ("not allow constant member in MemberExpression \"{0}\"", member));
				}
				return new LambdaBinaryQueryExpression (fieldInfo.TableMapping, QueryPredicate.Eq, fieldInfo, true);
			}
			MethodCallExpression methodcall = expression as MethodCallExpression;
			if (methodcall != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, parameters, out fieldInfo)) {
					throw new LambdaParseException (string.Format ("not allow constant method in MethodCallExpression \"{0}\"", methodcall));
				}
				IDataFieldInfoConvert convertFieldInfo = fieldInfo as IDataFieldInfoConvert;
				if (Object.Equals (convertFieldInfo, null)) {
					throw new LambdaParseException (string.Format ("not allow method in MethodCallExpression \"{0}\"", methodcall));
				}
				else {
					return convertFieldInfo.ConvertToExpression ();
				}
			}

			throw new LambdaParseException (string.Format ("lambda expression [{0}] not support", expression));
		}
	}
}

