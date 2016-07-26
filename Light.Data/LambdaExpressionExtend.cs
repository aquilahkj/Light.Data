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
		//public static bool In<T> (this T obj, T [] array)
		//{
		//	return true;
		//}
		//public static bool NotIn<T> (this T obj, T [] array)
		//{
		//	return true;
		//}
		//public static bool Like (this string str, string likeStr)
		//{
		//	return true;
		//}
		//public static bool NotLike (this string str, string likeStr)
		//{
		//	return true;
		//}

		//internal static object ConvertLambdaObject (object value, CommandFactory factory, bool fullName, bool stringWarp, out DataParameter [] dataParameters)
		//{
		//	DataFieldInfo fieldInfo = value as DataFieldInfo;
		//	if (!object.Equals (fieldInfo, null)) {
		//		return fieldInfo.CreateDataFieldSql (factory, fullName, out dataParameters);
		//	}
		//	dataParameters = null;
		//	object obj;
		//	Delegate dele = value as Delegate;
		//	if (dele != null) {
		//		obj = dele.DynamicInvoke (null);
		//	}
		//	else {
		//		obj = value;
		//	}
		//	if (stringWarp) {
		//		obj = factory.CreateStringWrap (obj);
		//	}
		//	return obj;
		//}

		static HashSet<MethodInfo> Method_IndexOf = new HashSet<MethodInfo> ();

		static HashSet<MethodInfo> Method_Trim = new HashSet<MethodInfo> ();

		static LambdaExpressionExtend ()
		{
			Type stringType = typeof (string);
			MethodInfo [] stringMethods = stringType.GetMethods ();
			foreach (MethodInfo method in stringMethods) {
				if (method.Name == "IndexOf") {
					ParameterInfo [] parameters = method.GetParameters ();
					if (parameters.Length == 1) {
						Method_IndexOf.Add (method);
					}
					else if (parameters.Length == 2 && parameters [1].ParameterType == typeof (int)) {
						Method_IndexOf.Add (method);
					}
				}
				else if (method.Name == "Trim") {
					ParameterInfo [] parameters = method.GetParameters ();
					if (parameters.Length == 0) {
						Method_Trim.Add (method);
					}
				}
			}
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

		//static QueryExpression CreateDataFieldQueryExpression (QueryPredicate predicate, DataFieldInfo leftFieldInfo, DataFieldInfo rightFieldInfo)
		//{
		//	return new DataFieldQueryExpression (leftFieldInfo, predicate, rightFieldInfo, false);
		//}

		//static QueryExpression CreateQueryPredicateExpression (QueryPredicate predicate, DataFieldInfo fieldInfo, object value, bool isReverse)
		//{
		//	QueryExpression queryExpression = null;
		//	if (object.Equals (value, null)) {
		//		if (predicate == QueryPredicate.Eq) {
		//			queryExpression = new NullQueryExpression (fieldInfo, true);
		//		}
		//		else if (predicate == QueryPredicate.NotEq) {
		//			queryExpression = new NullQueryExpression (fieldInfo, false);
		//		}
		//	}
		//	else if (value is Boolean) {
		//		bool b = (bool)value;
		//		if (predicate == QueryPredicate.Eq) {
		//			queryExpression = new BooleanQueryExpression (fieldInfo, b);
		//		}
		//		else if (predicate == QueryPredicate.NotEq) {
		//			queryExpression = new BooleanQueryExpression (fieldInfo, !b);
		//		}
		//	}
		//	else {
		//		queryExpression = new LambdaBinaryQueryExpression (fieldInfo, predicate, value, isReverse);
		//	}
		//	if (queryExpression == null) {
		//		throw new LambdaParseException ("");
		//	}

		//	return queryExpression;
		//}

		//private static QueryPredicate ReverseQueryPredicate (QueryPredicate queryPredicate)
		//{
		//	switch (queryPredicate) {
		//	case QueryPredicate.Eq:
		//	case QueryPredicate.NotEq:
		//		return queryPredicate;
		//	case QueryPredicate.Gt:
		//		return QueryPredicate.Lt;
		//	case QueryPredicate.GtEq:
		//		return QueryPredicate.LtEq;
		//	case QueryPredicate.Lt:
		//		return QueryPredicate.Gt;
		//	case QueryPredicate.LtEq:
		//		return QueryPredicate.GtEq;
		//	default:
		//		return QueryPredicate.Eq;
		//	}
		//}

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

		public static QueryExpression ResolveLambda (LambdaExpression expression)
		{
			ParameterExpression parameter = expression.Parameters [0];
			string name = parameter.Name;
			Type type = parameter.Type;
			DataEntityMapping mapping = DataMapping.GetEntityMapping (type);
			//try {
			return Resolve (expression.Body, name, mapping);
			//}
			//catch (Exception ex) {
			//	throw ex;
			//}
		}

		private static DataFieldInfo CreateDateDataFieldInfo (DataFieldInfo fieldInfo, MemberInfo member)
		{
			if (member.Name == "Date") {
				return fieldInfo = new DateDataFieldInfo (fieldInfo, null);
			}
			else {
				DatePart datePart;
				if (!Enum.TryParse<DatePart> (member.Name, out datePart)) {
					throw new LambdaParseException ("");
				}
				return new DatePartDataFieldInfo (fieldInfo, datePart);
			}
		}

		private static DataFieldInfo CreateStringMemberDataFieldInfo (DataFieldInfo fieldInfo, MemberInfo member)
		{
			switch (member.Name) {
			case "Length":
				fieldInfo = new LambdaStringFunctionDataFieldInfo (fieldInfo, StringFunction.Lenght, fieldInfo);
				break;
			default:
				throw new LambdaParseException ("");
			}
			return fieldInfo;
		}

		private static bool ParseArguments (ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, string name, DataEntityMapping mapping, out object [] argObjects, out DataFieldInfo fieldInfo)
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
				if (CheckDataFieldInfo (arg, name, mapping, out argFieldInfo)) {
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

		private static DataFieldInfo ParseMathFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, string name, DataEntityMapping mapping)
		{
			MathFunction mathFunction;
			if (!Enum.TryParse<MathFunction> (method.Name, out mathFunction)) {
				throw new LambdaParseException ("");
			}
			DataFieldInfo fieldInfo = new LambdaMathFunctionDataFieldInfo (mainFieldInfo, mathFunction, argObjects);
			return fieldInfo;
		}


		private static DataFieldInfo ParseStaticeStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, string name, DataEntityMapping mapping)
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
			throw new LambdaParseException ("");
		}

		private static DataFieldInfo ParseInstanceStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, string name, DataEntityMapping mapping)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			if (method.Name == "StartsWith" && parameterInfos.Length == 1) {
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, true, false, callObject, argObjects [0]);
			}
			else if (method.Name == "EndsWith" && parameterInfos.Length == 1) {
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, false, true, callObject, argObjects [0]);
			}
			else if (method.Name == "Contains" && parameterInfos.Length == 1) {
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo, true, true, callObject, argObjects [0]);
			}
			else {
				StringFunction stringFunction;
				if (!Enum.TryParse<StringFunction> (method.Name, out stringFunction)) {
					throw new LambdaParseException ("");
				}
				if (stringFunction == StringFunction.IndexOf && !Method_IndexOf.Contains (method)) {
					throw new LambdaParseException ("");
				}
				if (stringFunction == StringFunction.Trim && !Method_Trim.Contains (method)) {
					throw new LambdaParseException ("");
				}
				DataFieldInfo fieldInfo = new LambdaStringFunctionDataFieldInfo (mainFieldInfo, stringFunction, callObject, argObjects);
				return fieldInfo;
			}
		}

		private static DataFieldInfo ParseInstanceDateTimeFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, string name, DataEntityMapping mapping)
		{
			throw new NotImplementedException ();
		}

		static DataFieldInfo ParseContainsDataFieldInfo (MethodInfo methodInfo, DataFieldInfo mainFieldInfo, object collections, string name, DataEntityMapping mapping)
		{
			return new LambdaContainsDataFieldInfo (mainFieldInfo, collections);
		}

		private static bool CheckDataFieldInfo (System.Linq.Expressions.Expression expression, string name, DataEntityMapping mapping, out DataFieldInfo fieldInfo)
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
					if (!CheckDataFieldInfo (binary.Left, name, mapping, out leftFieldInfo)) {
						leftValue = ConvertObject (binary.Left);
					}
					DataFieldInfo rightFieldInfo;
					object rightValue = null;
					if (!CheckDataFieldInfo (binary.Right, name, mapping, out rightFieldInfo)) {
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
						if (!CheckDataFieldInfo (binary.Left, name, mapping, out leftFieldInfo)) {
							leftValue = ConvertObject (binary.Left);
						}
						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						if (!CheckDataFieldInfo (binary.Right, name, mapping, out rightFieldInfo)) {
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
						throw new LambdaParseException ("");
					}
				}
			}
			UnaryExpression unary = expression as UnaryExpression;
			if (unary != null) {
				if (unary.NodeType == ExpressionType.Not) {
					DataFieldInfo notfieldInfo;
					if (!CheckDataFieldInfo (unary.Operand, name, mapping, out notfieldInfo)) {
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
					if (!CheckDataFieldInfo (unary.Operand, name, mapping, out convertfieldInfo)) {
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
						if (param.Name == name && param.Type == mapping.ObjectType) {
							fieldInfo = new DataFieldInfo (param.Type, member.Member.Name);
							return true;
						}
						else {
							throw new LambdaParseException ("");
						}
					}
					if (CheckDataFieldInfo (member.Expression, name, mapping, out fieldInfo)) {
						if (member.Expression.Type == typeof (DateTime)) {
							fieldInfo = CreateDateDataFieldInfo (fieldInfo, member.Member);
							return true;
						}
						else if (member.Expression.Type == typeof (string)) {
							fieldInfo = CreateStringMemberDataFieldInfo (fieldInfo, member.Member);
							return true;
						}
						else {
							throw new LambdaParseException ("");
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
					if (ParseArguments (methodcall.Arguments, name, mapping, out argObjects, out mainFieldInfo)) {
						if (methodInfo.DeclaringType == typeof (Math)) {
							fieldInfo = ParseMathFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, name, mapping);
							return true;
						}
						if (methodInfo.DeclaringType == typeof (string)) {
							fieldInfo = ParseStaticeStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, name, mapping);
							return true;
						}
						else {
							throw new LambdaParseException ("");
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
					if (CheckDataFieldInfo (methodcall.Object, name, mapping, out callFieldInfo)) {
						mainFieldInfo = callFieldInfo;
						callObject = callFieldInfo;
					}
					else {
						callObject = ConvertObject (methodcall.Object);
					}

					object [] argObjects;
					DataFieldInfo mainArgFieldInfo;
					if (ParseArguments (methodcall.Arguments, name, mapping, out argObjects, out mainArgFieldInfo)) {
						if (Object.Equals (mainFieldInfo, null)) {
							mainFieldInfo = mainArgFieldInfo;
						}
					}
					if (Object.Equals (mainFieldInfo, null)) {
						return false;
					}

					if (methodInfo.DeclaringType == typeof (string)) {
						fieldInfo = ParseInstanceStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, name, mapping);
						return true;
					}
					if (methodInfo.DeclaringType == typeof (DateTime)) {
						fieldInfo = ParseInstanceDateTimeFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, name, mapping);
						return true;
					}
					if (Object.Equals (callFieldInfo, null) && argObjects != null && argObjects.Length == 1 && methodInfo.Name == "Contains" && typeof (IEnumerable).IsAssignableFrom (methodInfo.DeclaringType)) {
						fieldInfo = ParseContainsDataFieldInfo (methodInfo, mainFieldInfo, callObject, name, mapping);
						return true;
					}
				}
			}
			NewArrayExpression newarray = expression as NewArrayExpression;
			if (newarray != null) {
				object [] argsObjects;
				DataFieldInfo arrayFieldInfo;
				if (ParseArguments (newarray.Expressions, name, mapping, out argsObjects, out arrayFieldInfo)) {
					fieldInfo = new LambdaNewArrayDataFieldInfo (arrayFieldInfo, argsObjects);
					return true;
				}
				else {
					return false;
				}
			}
			throw new NotImplementedException ();
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
				//return fn.DynamicInvoke (null);
			}
		}

		private static QueryExpression Resolve (System.Linq.Expressions.Expression expression, string name, DataEntityMapping mapping)
		{
			LambdaExpression lambda = expression as LambdaExpression;
			if (lambda != null) {
				return ResolveLambda (lambda);
			}
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				CatchOperatorsType catchType;
				if (CheckCatchOperatorsType (binary.NodeType, out catchType)) {
					var left = Resolve (binary.Left, name, mapping);
					var right = Resolve (binary.Right, name, mapping);
					return QueryExpression.Catch (left, catchType, right);
				}
				else {
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binary.NodeType, out queryPredicate)) {
						DataFieldInfo leftFieldInfo;
						object leftValue = null;
						if (!CheckDataFieldInfo (binary.Left, name, mapping, out leftFieldInfo)) {
							leftValue = ConvertObject (binary.Left);
						}
						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						if (!CheckDataFieldInfo (binary.Right, name, mapping, out rightFieldInfo)) {
							rightValue = ConvertObject (binary.Right);
						}
						if (!Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (mapping, queryPredicate, leftFieldInfo, rightFieldInfo);
						}
						else if (!Object.Equals (leftFieldInfo, null) && Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (mapping, queryPredicate, leftFieldInfo, rightValue);
						}
						else if (Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
							return new LambdaBinaryQueryExpression (mapping, queryPredicate, rightFieldInfo, leftValue);
						}
						else {
							throw new LambdaParseException ("");
						}
					}
				}
			}
			UnaryExpression unary = expression as UnaryExpression;
			if (unary != null) {
				if (unary.NodeType == ExpressionType.Not) {
					QueryExpression queryExpression = Resolve (unary.Operand, name, mapping);
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
				if (!CheckDataFieldInfo (expression, name, mapping, out fieldInfo)) {
					throw new LambdaParseException ("");
				}
				return new LambdaBinaryQueryExpression (mapping, QueryPredicate.Eq, fieldInfo, true);
				//return CreateQueryPredicateExpression (QueryPredicate.Eq, fieldInfo, true, false);
				//ConstantExpression constant = System.Linq.Expressions.Expression.Constant (true);
				//return ResolveFunc (member, constant, ExpressionType.Equal);
			}
			MethodCallExpression methodcall = expression as MethodCallExpression;
			if (methodcall != null)//x=>x.Name.Contains("xxx")或array.Contains(x.Name)这类
			{
				DataFieldInfo fieldInfo;
				if (!CheckDataFieldInfo (expression, name, mapping, out fieldInfo)) {
					throw new LambdaParseException ("");
				}
				IDataFieldInfoConvert convertFieldInfo = fieldInfo as IDataFieldInfoConvert;
				if (Object.Equals (convertFieldInfo, null)) {
					throw new LambdaParseException ("");
				}
				else {
					return convertFieldInfo.ConvertToExpression ();
				}
			}

			throw new LambdaParseException ("");
			//LambdaParseException ("")sion as BinaryExpression;
			//if (body == null)
			//	throw new Exception ("无法解析" + expression);
			//var Operator = GetOperator (body.NodeType);
			//var Left = Resolve (body.Left);
			//var Right = Resolve (body.Right);
			//string Result = string.Format ("({0} {1} {2})", Left, Operator, Right);
			//return Result;
		}

		//private static QueryExpression ResolveFunc (MemberExpression left, ConstantExpression right, ExpressionType expressiontype)
		//{
		//	var Name = left.Member.Name;
		//	var Value = right.Value;
		//	var Operator = GetOperator (expressiontype);
		//	string CompName = SetArgument (Name, Value.ToString ());

		//}

		//private static QueryExpression ResolveLinqToObject (System.Linq.Expressions.Expression expression, object value, ExpressionType? expressiontype = null)
		//{
		//	var MethodCall = expression as MethodCallExpression;
		//	var MethodName = MethodCall.Method.Name;
		//	switch (MethodName)//这里其实还可以改成反射调用，不用写switch
		//	{
		//	case "Contains":
		//		if (MethodCall.Object != null)
		//			return Like (MethodCall);
		//		return In (MethodCall, value);
		//	case "Count":
		//		return Len (MethodCall, value, expressiontype.Value);
		//	case "LongCount":
		//		return Len (MethodCall, value, expressiontype.Value);
		//	default:
		//		throw new Exception (string.Format ("不支持{0}方法的查找！", MethodName));
		//	}
		//}


		//public static QueryExpression Where<T> (T entity, Expression<Func<T, bool>> func) where T : class, new()
		//{

		//	//if (func.Body is BinaryExpression) {
		//	//	BinaryExpression be = ((BinaryExpression)func.Body);
		//	//	entity.whereStr = BinarExpressionProvider (be.Left, be.Right, be.NodeType);
		//	//}
		//	//else
		//	//	entity.whereStr = string.Empty;
		//}

		//static QueryExpression BinarExpressionProvider (System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.ExpressionType type)
		//{
		//	QueryExpression expression;



		//	string sb = "(";
		//	//先处理左边
		//	sb += ExpressionRouter (left);

		//	sb += ExpressionTypeCast (type);

		//	//再处理右边
		//	string tmpStr = ExpressionRouter (right);
		//	if (tmpStr == "null") {
		//		if (sb.EndsWith (" ="))
		//			sb = sb.Substring (0, sb.Length - 2) + " is null";
		//		else if (sb.EndsWith ("<>"))
		//			sb = sb.Substring (0, sb.Length - 2) + " is not null";
		//	}
		//	else
		//		sb += tmpStr;
		//	return sb += ")";
		//}

		//static QueryExpression ExpressionRouter (System.Linq.Expressions.Expression exp)
		//{
		//	string sb = string.Empty;
		//	if (exp is BinaryExpression) {
		//		BinaryExpression be = ((BinaryExpression)exp);
		//		return BinarExpressionProvider (be.Left, be.Right, be.NodeType);
		//	}
		//	else if (exp is MemberExpression) {
		//		MemberExpression me = ((MemberExpression)exp);
		//		return me.Member.Name;
		//	}
		//	else if (exp is NewArrayExpression) {
		//		NewArrayExpression ae = ((NewArrayExpression)exp);
		//		StringBuilder tmpstr = new StringBuilder ();
		//		foreach (Expression ex in ae.Expressions) {
		//			tmpstr.Append (ExpressionRouter (ex));
		//			tmpstr.Append (",");
		//		}
		//		return tmpstr.ToString (0, tmpstr.Length - 1);
		//	}
		//	else if (exp is MethodCallExpression) {
		//		MethodCallExpression mce = (MethodCallExpression)exp;
		//		if (mce.Method.Name == "Like")
		//			return string.Format ("({0} like {1})", ExpressionRouter (mce.Arguments [0]), ExpressionRouter (mce.Arguments [1]));
		//		else if (mce.Method.Name == "NotLike")
		//			return string.Format ("({0} Not like {1})", ExpressionRouter (mce.Arguments [0]), ExpressionRouter (mce.Arguments [1]));
		//		else if (mce.Method.Name == "In")
		//			return string.Format ("{0} In ({1})", ExpressionRouter (mce.Arguments [0]), ExpressionRouter (mce.Arguments [1]));
		//		else if (mce.Method.Name == "NotIn")
		//			return string.Format ("{0} Not In ({1})", ExpressionRouter (mce.Arguments [0]), ExpressionRouter (mce.Arguments [1]));

		//	}
		//	else if (exp is ConstantExpression) {
		//		ConstantExpression ce = ((ConstantExpression)exp);
		//		if (ce.Value == null)
		//			return "null";
		//		else if (ce.Value is ValueType)
		//			return ce.Value.ToString ();
		//		else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
		//			return string.Format ("'{0}'", ce.Value.ToString ());
		//	}
		//	else if (exp is UnaryExpression) {
		//		UnaryExpression ue = ((UnaryExpression)exp);
		//		return ExpressionRouter (ue.Operand);
		//	}
		//	return null;
		//}

		//static CatchOperatorsType ExpressionTypeCast (ExpressionType type)
		//{
		//	switch (type) {
		//	case ExpressionType.And:
		//	case ExpressionType.AndAlso:
		//		return CatchOperatorsType.AND;
		//	case ExpressionType.Equal:
		//		return " =";
		//	case ExpressionType.GreaterThan:
		//		return " >";
		//	case ExpressionType.GreaterThanOrEqual:
		//		return ">=";
		//	case ExpressionType.LessThan:
		//		return "<";
		//	case ExpressionType.LessThanOrEqual:
		//		return "<=";
		//	case ExpressionType.NotEqual:
		//		return "<>";
		//	case ExpressionType.Or:
		//	case ExpressionType.OrElse:
		//		return " Or ";
		//	case ExpressionType.Add:
		//	case ExpressionType.AddChecked:
		//		return "+";
		//	case ExpressionType.Subtract:
		//	case ExpressionType.SubtractChecked:
		//		return "-";
		//	case ExpressionType.Divide:
		//		return "/";
		//	case ExpressionType.Multiply:
		//	case ExpressionType.MultiplyChecked:
		//		return "*";
		//	default:
		//		return null;
		//	}
		//}
	}
}

