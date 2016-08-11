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
		public static object ConvertLambdaObject (object value)
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

		public static object ConvertObject (Expression expression)
		{
			ConstantExpression constant = expression as ConstantExpression;
			if (constant != null) {
				return constant.Value;
			}
			else {
				LambdaExpression lambda = Expression.Lambda (expression);
				Delegate fn = lambda.Compile ();
				return fn;
			}
		}

		private	static LambdaState CreateLambdaState (LambdaExpression expression)
		{
			if (expression.Parameters.Count == 0) {
				throw new LightDataException ("");
			}
			else if (expression.Parameters.Count == 1) {
				return new SingleEntityLambdaState (expression.Parameters [0]);
			}
			else {
				return new MutliEntityLambdaState (expression.Parameters);
			}
		}

		public static ISelector CreateSelector (LambdaExpression expression)
		{
			LambdaState state = CreateLambdaState (expression);

			try {
				NewExpression newExpression = expression.Body as NewExpression;
				if (newExpression == null) {
					throw new LambdaParseException ("");
				}
				List<string> list;

				if (ParseNewArguments (newExpression, state, out list)) {
					return state.CreateSelector (list.ToArray ());
				}
				else {
					throw new LambdaParseException ("");
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse select expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
		}

		public static OrderExpression ResolveLambdaOrderByExpression (LambdaExpression expression, OrderType orderType)
		{
			LambdaState state = CreateLambdaState (expression);
			DataFieldInfo dataFieldInfo;
			bool ret;
			try {
				ret = ParseDataFieldInfo (expression.Body, state, out dataFieldInfo);
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse order expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
			if (ret) {
				CheckFieldInfo (dataFieldInfo);
				OrderExpression exp = new FieldOrderExpression (dataFieldInfo, orderType);
				exp.MutliOrder = true;
				return exp;
			}
			else {
				throw new LightDataException (string.Format ("not valid field in order expression \"{0}\"", expression));
			}
		}

		public static QueryExpression ResolveLambdaQueryExpression (LambdaExpression expression)
		{
			LambdaState state = CreateLambdaState (expression);
			try {
				QueryExpression query = ResolveQueryExpression (expression.Body, state);
				query.MutliQuery = state.MutliQuery;
				return query;
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse query expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
		}

		public static void ResolveLambdaSelectExpression (LambdaExpression expression)
		{
			LambdaState state = CreateLambdaState (expression);
			try {
				ResolveQueryExpression (expression.Body, state);
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format ("parse query expression \"{0}\" error,message:{1}", expression, ex.Message), ex);
			}
		}

		private static bool ParseNewArguments (Expression expression, LambdaState state, out List<string> pathList)
		{
			ParameterExpression paramObj = expression as ParameterExpression;
			if (paramObj != null) {
				pathList = new List<string> ();
				pathList.Add (paramObj.Name);
				return true;
			}
			MemberExpression memberObj = expression as MemberExpression;
			if (memberObj != null) {
				if (memberObj.Expression != null) {
					ParameterExpression param = memberObj.Expression as ParameterExpression;
					if (param != null) {
						if (state.CheckPamramter (param.Name, param.Type)) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							if (pathType == LambdaPathType.Field) {
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								pathList = null;
								return true;
							}
							else if (pathType == LambdaPathType.RelateCollection) {
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							}
							else if (pathType == LambdaPathType.Parameter) {
								throw new LambdaParseException ("");
							}
							else {
								pathList = new List<string> ();
								pathList.Add (param.Name);
								return true;
							}
						}
						else {
							throw new LambdaParseException (string.Format ("parameter not correct,name:{0},type:{1}", param.Name, param.Type));
						}

						//RelationMap relationMap;
						//if (state.TryGetRelationMap (param.Name, out relationMap) && param.Type == relationMap.RootMapping.ObjectType) {
						//	string path = "." + memberObj.Member.Name;
						//	if (relationMap.CheckIsField (path)) {
						//		pathList = new List<string> ();
						//		pathList.Add (memberObj.ToString ());
						//		return true;
						//	}
						//	else if (relationMap.CheckIsRelateEntity (path)) {
						//		pathList = null;
						//		return true;
						//	}
						//	else if (relationMap.CheckIsEntityCollection (path)) {
						//		pathList = new List<string> ();
						//		pathList.Add (memberObj.ToString ());
						//		return true;
						//	}
						//	else {
						//		pathList = new List<string> ();
						//		pathList.Add (param.Name);
						//		return true;
						//	}
						//}
						//else {
						//	throw new LambdaParseException (string.Format ("parameter not correct,name:{0},type:{1}", param.Name, param.Type));
						//}
					}
					List<string> memberList = null;
					if (ParseNewArguments (memberObj.Expression, state, out memberList)) {
						if (memberList == null) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							if (pathType == LambdaPathType.Field) {
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								pathList = null;
								return true;
							}
							else if (pathType == LambdaPathType.RelateCollection) {
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							}
							else {
								pathList = new List<string> ();
								pathList.Add (memberObj.Expression.ToString ());
								return true;
							}

							//int index = fullPath.IndexOf (".", StringComparison.Ordinal);
							//string paramName = fullPath.Substring (0, index);
							//string path = fullPath.Substring (index);
							//RelationMap relationMap;
							//state.TryGetRelationMap (paramName, out relationMap);
							//if (relationMap.CheckIsField (path)) {
							//	pathList = new List<string> ();
							//	pathList.Add (memberObj.ToString ());
							//	return true;
							//}
							//else if (relationMap.CheckIsRelateEntity (path)) {
							//	pathList = null;
							//	return true;
							//}
							//else if (relationMap.CheckIsEntityCollection (path)) {
							//	pathList = new List<string> ();
							//	pathList.Add (memberObj.ToString ());
							//	return true;
							//}
							//else {
							//	pathList = new List<string> ();
							//	pathList.Add (memberObj.Expression.ToString ());
							//	return true;
							//}
						}
						else {
							pathList = memberList;
							return true;
						}
					}
					else {
						pathList = null;
						return false;
					}
				}
				else {
					//static member
					pathList = null;
					return false;
				}
			}
			UnaryExpression unaryObj = expression as UnaryExpression;
			if (unaryObj != null) {
				return ParseNewArguments (unaryObj.Operand, state, out pathList);
			}
			MethodCallExpression methodcallObj = expression as MethodCallExpression;
			if (methodcallObj != null) {
				bool ret = false;
				List<string> methodList = new List<string> ();

				if (methodcallObj.Arguments != null) {
					foreach (Expression arg in methodcallObj.Arguments) {
						List<string> argList = null;
						if (ParseNewArguments (arg, state, out argList)) {
							ret = true;
							if (argList == null) {
								methodList.Add (arg.ToString ());
							}
							else {
								methodList.AddRange (argList);
							}
						}
					}
				}
				if (methodcallObj.Object != null) {
					List<string> callList = null;
					if (ParseNewArguments (methodcallObj.Object, state, out callList)) {
						ret = true;
						if (callList == null) {
							methodList.Add (methodcallObj.Object.ToString ());
						}
						else {
							methodList.AddRange (callList);
						}
					}
				}
				if (ret) {
					pathList = methodList;
					return true;
				}
				else {
					pathList = null;
					return false;
				}
			}
			NewArrayExpression newarrayObj = expression as NewArrayExpression;
			if (newarrayObj != null) {
				if (newarrayObj.Expressions != null) {
					bool ret = false;
					List<string> newarrayList = new List<string> ();
					List<string> argList = null;
					foreach (Expression arg in newarrayObj.Expressions) {
						if (ParseNewArguments (arg, state, out argList)) {
							ret = true;
							if (argList == null) {
								newarrayList.Add (arg.ToString ());
							}
							else {
								newarrayList.AddRange (argList);
							}
						}
					}
					if (ret) {
						pathList = newarrayList;
						return true;
					}
					else {
						pathList = null;
						return false;
					}
				}
				else {
					pathList = null;
					return false;
				}
			}
			NewExpression newObj = expression as NewExpression;
			if (newObj != null) {
				if (newObj.Arguments != null) {
					bool ret = false;
					List<string> newobjList = new List<string> ();
					List<string> argList = null;
					foreach (Expression arg in newObj.Arguments) {
						if (ParseNewArguments (arg, state, out argList)) {
							ret = true;
							if (argList == null) {
								newobjList.Add (arg.ToString ());
							}
							else {
								newobjList.AddRange (argList);
							}
						}
					}
					if (ret) {
						pathList = newobjList;
						return true;
					}
					else {
						pathList = null;
						return false;
					}
				}
				else {
					pathList = null;
					return false;
				}
			}
			ConstantExpression constantObj = expression as ConstantExpression;
			if (constantObj != null) {
				pathList = null;
				return false;
			}
			BinaryExpression binaryObj = expression as BinaryExpression;
			if (binaryObj != null) {
				bool ret = false;
				List<string> binaryList = new List<string> ();
				List<string> leftList = null;
				if (ParseNewArguments (binaryObj.Left, state, out leftList)) {
					ret = true;
					if (leftList == null) {
						binaryList.Add (binaryObj.Left.ToString ());
					}
					else {
						binaryList.AddRange (leftList);
					}
				}
				List<string> rightList = null;
				if (ParseNewArguments (binaryObj.Right, state, out rightList)) {
					ret = true;
					if (rightList == null) {
						binaryList.Add (binaryObj.Right.ToString ());
					}
					else {
						binaryList.AddRange (rightList);
					}
				}
				if (ret) {
					pathList = binaryList;
					return true;
				}
				else {
					pathList = null;
					return false;
				}
			}
			IndexExpression indexObj = expression as IndexExpression;
			if (indexObj != null) {
				bool ret = false;
				List<string> indexList = new List<string> ();

				if (methodcallObj.Arguments != null) {
					foreach (Expression arg in indexObj.Arguments) {
						List<string> argList = null;
						if (ParseNewArguments (arg, state, out argList)) {
							ret = true;
							if (argList == null) {
								indexList.Add (arg.ToString ());
							}
							else {
								indexList.AddRange (argList);
							}
						}
					}
				}
				if (indexObj.Object != null) {
					List<string> callList = null;
					if (ParseNewArguments (indexObj.Object, state, out callList)) {
						ret = true;
						if (callList == null) {
							indexList.Add (indexObj.Object.ToString ());
						}
						else {
							indexList.AddRange (callList);
						}
					}
				}
				if (ret) {
					pathList = indexList;
					return true;
				}
				else {
					pathList = null;
					return false;
				}
			}
			DynamicExpression dynamicObj = expression as DynamicExpression;
			if (dynamicObj != null) {
				if (dynamicObj.Arguments != null) {
					bool ret = false;
					List<string> aynamicList = new List<string> ();
					List<string> argList = null;
					foreach (Expression arg in dynamicObj.Arguments) {
						if (ParseNewArguments (arg, state, out argList)) {
							ret = true;
							if (argList == null) {
								aynamicList.Add (arg.ToString ());
							}
							else {
								aynamicList.AddRange (argList);
							}
						}
					}
					if (ret) {
						pathList = aynamicList;
						return true;
					}
					else {
						pathList = null;
						return false;
					}
				}
				else {
					pathList = null;
					return false;
				}
			}
			DefaultExpression defaultObj = expression as DefaultExpression;
			if (defaultObj != null) {
				pathList = null;
				return false;
			}
			ConditionalExpression conditionObj = expression as ConditionalExpression;
			if (conditionObj != null) {
				bool ret = false;
				List<string> conditionList = new List<string> ();
				List<string> testList = null;
				if (ParseNewArguments (conditionObj.Test, state, out testList)) {
					ret = true;
					if (testList == null) {
						conditionList.Add (conditionObj.Test.ToString ());
					}
					else {
						conditionList.AddRange (testList);
					}
				}
				List<string> trueList = null;
				if (ParseNewArguments (conditionObj.IfTrue, state, out trueList)) {
					ret = true;
					if (trueList == null) {
						conditionList.Add (conditionObj.IfTrue.ToString ());
					}
					else {
						conditionList.AddRange (trueList);
					}
				}
				List<string> falseList = null;
				if (ParseNewArguments (conditionObj.IfFalse, state, out falseList)) {
					ret = true;
					if (falseList == null) {
						conditionList.Add (conditionObj.IfFalse.ToString ());
					}
					else {
						conditionList.AddRange (falseList);
					}
				}
				if (ret) {
					pathList = conditionList;
					return true;
				}
				else {
					pathList = null;
					return false;
				}
			}
			TypeBinaryExpression typeBinaryObj = expression as TypeBinaryExpression;
			if (typeBinaryObj != null) {
				return ParseNewArguments (typeBinaryObj.Expression, state, out pathList);
			}
			ListInitExpression listInitObj = expression as ListInitExpression;
			if (listInitObj != null) {
				bool ret = false;
				List<string> lisInitList = new List<string> ();
				if (listInitObj.Initializers != null) {
					foreach (ElementInit init in listInitObj.Initializers) {
						List<string> argList = null;
						foreach (Expression arg in init.Arguments) {
							if (ParseNewArguments (arg, state, out argList)) {
								ret = true;
								if (argList == null) {
									lisInitList.Add (arg.ToString ());
								}
								else {
									lisInitList.AddRange (argList);
								}
							}
						}
					}
				}
				if (listInitObj.NewExpression != null) {
					List<string> newList = null;
					if (ParseNewArguments (listInitObj.NewExpression, state, out newList)) {
						ret = true;
						if (newList == null) {
							lisInitList.Add (listInitObj.NewExpression.ToString ());
						}
						else {
							lisInitList.AddRange (newList);
						}
					}
				}
				if (ret) {
					pathList = lisInitList;
					return true;
				}
				else {
					pathList = null;
					return false;
				}
			}
			throw new LambdaParseException (string.Format ("lambda expression [{0}] not support", expression));
		}

		private static void CheckFieldInfo (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new LambdaParseException ("");
			}
		}


		private static bool ParseDataFieldInfo (Expression expression, LambdaState state, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			if (expression.NodeType == ExpressionType.Constant) {
				return false;
			}
			BinaryExpression binaryObj = expression as BinaryExpression;
			if (binaryObj != null) {
				if (binaryObj.Method != null && binaryObj.NodeType == ExpressionType.Add && binaryObj.Method.DeclaringType == typeof (string) && binaryObj.Method.Name == "Concat") {
					DataFieldInfo leftFieldInfo;
					object leftValue = null;
					bool left;
					if (ParseDataFieldInfo (binaryObj.Left, state, out leftFieldInfo)) {
						left = true;
						CheckFieldInfo (leftFieldInfo);
					}
					else {
						left = false;
						leftValue = ConvertObject (binaryObj.Left);
					}

					DataFieldInfo rightFieldInfo;
					object rightValue = null;
					bool right;
					if (ParseDataFieldInfo (binaryObj.Right, state, out rightFieldInfo)) {
						right = true;
						CheckFieldInfo (rightFieldInfo);
					}
					else {
						right = false;
						rightValue = ConvertObject (binaryObj.Right);
					}

					if (left && right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo, leftFieldInfo, rightFieldInfo);
						return true;
					}
					else if (left && !right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo, leftFieldInfo, rightValue);
						return true;
					}
					else if (!left && right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (rightFieldInfo, leftValue, rightFieldInfo);
						return true;
					}
					else {
						return false;
					}
				}
				else {
					MathOperator mathOperator;
					if (CheckMathOperator (binaryObj.NodeType, out mathOperator)) {
						DataFieldInfo leftFieldInfo;
						object leftValue = null;
						bool left;
						if (ParseDataFieldInfo (binaryObj.Left, state, out leftFieldInfo)) {
							left = true;
							CheckFieldInfo (leftFieldInfo);
						}
						else {
							left = false;
							leftValue = ConvertObject (binaryObj.Left);
						}

						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						bool right;
						if (ParseDataFieldInfo (binaryObj.Right, state, out rightFieldInfo)) {
							right = true;
							CheckFieldInfo (rightFieldInfo);
						}
						else {
							right = false;
							rightValue = ConvertObject (binaryObj.Right);
						}

						if (left && right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo, mathOperator, leftFieldInfo, rightFieldInfo);
							return true;
						}
						else if (left && !right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo, mathOperator, leftFieldInfo, rightValue);
							return true;
						}
						else if (!left && right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (rightFieldInfo, mathOperator, leftValue, rightFieldInfo);
							return true;
						}
						else {
							return false;
						}
					}
					else {
						throw new LambdaParseException (string.Format ("not support node type {0}", binaryObj.NodeType));
					}
				}
			}
			UnaryExpression unaryObj = expression as UnaryExpression;
			if (unaryObj != null) {
				if (unaryObj.NodeType == ExpressionType.Not) {
					DataFieldInfo notfieldInfo;
					if (ParseDataFieldInfo (unaryObj.Operand, state, out notfieldInfo)) {
						CheckFieldInfo (notfieldInfo);
					}
					else {
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
				else if (unaryObj.NodeType == ExpressionType.Convert) {
					DataFieldInfo convertfieldInfo;
					if (ParseDataFieldInfo (unaryObj.Operand, state, out convertfieldInfo)) {
						CheckFieldInfo (convertfieldInfo);
						fieldInfo = convertfieldInfo;
						return true;
					}
					else {
						return false;
					}
				}
			}
			MemberExpression memberObj = expression as MemberExpression;
			if (memberObj != null) {
				if (memberObj.Expression != null) {
					ParameterExpression param = memberObj.Expression as ParameterExpression;
					if (param != null) {
						if (state.CheckPamramter (param.Name, param.Type)) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							if (pathType == LambdaPathType.Field) {
								DataFieldInfo myinfo = state.GetDataFileInfo (fullPath);
								fieldInfo = myinfo;
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								return true;
							}
							else {
								throw new LambdaParseException ("");
							}
						}
						else {
							throw new LambdaParseException (string.Format ("parameter not correct,name:{0},type:{1}", param.Name, param.Type));
						}

						//RelationMap relationMap;
						//if (state.TryGetRelationMap (param.Name, out relationMap) && param.Type == relationMap.RootMapping.ObjectType) {
						//	string path = "." + member.Member.Name;
						//	if (relationMap.CheckIsField (path)) {
						//		DataFieldInfo myinfo = relationMap.GetFieldInfoForField (path);
						//		fieldInfo = myinfo;
						//		return true;
						//	}
						//	else if (relationMap.CheckIsRelateEntity (path)) {
						//		return true;
						//	}
						//	else {
						//		throw new LambdaParseException ("");
						//	}
						//}
						//else {
						//	throw new LambdaParseException (string.Format ("parameter not correct,name:{0},type:{1}", param.Name, param.Type));
						//}
					}
					if (ParseDataFieldInfo (memberObj.Expression, state, out fieldInfo)) {
						if (Object.Equals (fieldInfo, null)) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							if (pathType == LambdaPathType.Field) {
								DataFieldInfo myinfo = state.GetDataFileInfo (fullPath);
								fieldInfo = myinfo;
								state.MutliQuery = true;
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								return true;
							}
							else {
								throw new LambdaParseException ("");
							}



							//int index = fullPath.IndexOf (".", StringComparison.Ordinal);
							//string paramName = fullPath.Substring (0, index);
							//string path = fullPath.Substring (index);
							//RelationMap relationMap;
							//state.TryGetRelationMap (paramName, out relationMap);
							//if (relationMap.CheckIsField (path)) {
							//	DataFieldInfo myinfo = relationMap.GetFieldInfoForField (path);
							//	fieldInfo = myinfo;
							//	state.MutliQuery = true;
							//	return true;
							//}
							//else if (relationMap.CheckIsRelateEntity (path)) {
							//	return true;
							//}
							//else {
							//	throw new LambdaParseException ("");
							//}
						}
						else {
							if (memberObj.Expression.Type == typeof (DateTime)) {
								fieldInfo = CreateDateDataFieldInfo (memberObj.Member, fieldInfo);
								return true;
							}
							else if (memberObj.Expression.Type == typeof (string)) {
								fieldInfo = CreateStringMemberDataFieldInfo (memberObj.Member, fieldInfo);
								return true;
							}
							else {
								throw new LambdaParseException (string.Format ("{0} type not support", memberObj.Expression.Type));
							}
						}
					}
					else {
						return false;
					}
				}
				else {
					//static member
					return false;
				}
			}
			MethodCallExpression methodcallObj = expression as MethodCallExpression;
			if (methodcallObj != null) {
				MethodInfo methodInfo = methodcallObj.Method;
				if ((methodInfo.Attributes & MethodAttributes.Static) == MethodAttributes.Static) {
					object [] argObjects;
					DataFieldInfo mainFieldInfo;
					if (ParseArguments (methodcallObj.Arguments, state, out argObjects, out mainFieldInfo)) {
						CheckFieldInfo (mainFieldInfo);
						if (methodInfo.DeclaringType == typeof (Math)) {
							fieldInfo = ParseMathFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, state);
							return true;
						}
						if (methodInfo.DeclaringType == typeof (string)) {
							fieldInfo = ParseStaticeStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, state);
							return true;
						}
						else {
							throw new LambdaParseException (string.Format ("{0} type not support", memberObj.Expression.Type));
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
					if (ParseDataFieldInfo (methodcallObj.Object, state, out callFieldInfo)) {
						CheckFieldInfo (callFieldInfo);
						mainFieldInfo = callFieldInfo;
						callObject = callFieldInfo;
					}
					else {
						callObject = ConvertObject (methodcallObj.Object);
					}

					object [] argObjects;
					DataFieldInfo mainArgFieldInfo;
					if (ParseArguments (methodcallObj.Arguments, state, out argObjects, out mainArgFieldInfo)) {
						if (Object.Equals (mainFieldInfo, null)) {
							mainFieldInfo = mainArgFieldInfo;
						}
					}
					if (Object.Equals (mainFieldInfo, null)) {
						return false;
					}

					if (methodcallObj.Object.Type == typeof (string)) {
						fieldInfo = ParseInstanceStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, state);
						return true;
					}
					if (methodcallObj.Object.Type == typeof (DateTime)) {
						fieldInfo = ParseInstanceDateTimeFunctionDataFieldInfo (methodInfo, mainFieldInfo, callObject, argObjects, state);
						return true;
					}
					if (Object.Equals (callFieldInfo, null) && argObjects != null && argObjects.Length == 1 && methodInfo.Name == "Contains" && typeof (IEnumerable).IsAssignableFrom (methodInfo.DeclaringType)) {
						fieldInfo = ParseContainsDataFieldInfo (methodInfo, mainFieldInfo, callObject, state);
						return true;
					}
				}
			}
			NewArrayExpression newarrayObj = expression as NewArrayExpression;
			if (newarrayObj != null) {
				object [] argsObjects;
				DataFieldInfo arrayFieldInfo;
				if (ParseArguments (newarrayObj.Expressions, state, out argsObjects, out arrayFieldInfo)) {
					fieldInfo = new LambdaNewArrayDataFieldInfo (arrayFieldInfo, argsObjects);
					return true;
				}
				else {
					return false;
				}
			}
			throw new LambdaParseException (string.Format ("lambda expression [{0}] not support", expression));
		}

		private static bool ParseArguments (ReadOnlyCollection<Expression> arguments, LambdaState state, out object [] argObjects, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			argObjects = null;
			if (arguments.Count == 0) {
				return false;
			}
			object [] array = new object [arguments.Count];
			bool hasFieldInfo = false;
			for (int i = 0; i < arguments.Count; i++) {
				Expression arg = arguments [i];
				DataFieldInfo argFieldInfo;
				if (ParseDataFieldInfo (arg, state, out argFieldInfo)) {
					CheckFieldInfo (argFieldInfo);
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

		private static DataFieldInfo ParseMathFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaState state)
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

		private static DataFieldInfo ParseStaticeStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaState state)
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

		private static DataFieldInfo ParseInstanceStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, LambdaState state)
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

		private static DataFieldInfo ParseInstanceDateTimeFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, LambdaState state)
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

		private static DataFieldInfo ParseContainsDataFieldInfo (MethodInfo methodInfo, DataFieldInfo mainFieldInfo, object collections, LambdaState state)
		{
			return new LambdaContainsDataFieldInfo (mainFieldInfo, collections);
		}

		private static QueryExpression ResolveQueryExpression (Expression expression, LambdaState state)
		{
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				CatchOperatorsType catchType;
				if (CheckCatchOperatorsType (binary.NodeType, out catchType)) {
					var left = ResolveQueryExpression (binary.Left, state);
					var right = ResolveQueryExpression (binary.Right, state);
					return QueryExpression.Catch (left, catchType, right);
				}
				else {
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binary.NodeType, out queryPredicate)) {
						DataFieldInfo leftFieldInfo;
						object leftValue = null;
						bool left;
						if (ParseDataFieldInfo (binary.Left, state, out leftFieldInfo)) {
							left = true;
							CheckFieldInfo (leftFieldInfo);
						}
						else {
							left = false;
							leftValue = ConvertObject (binary.Left);
						}

						DataFieldInfo rightFieldInfo;
						object rightValue = null;
						bool right;
						if (ParseDataFieldInfo (binary.Right, state, out rightFieldInfo)) {
							right = true;
							CheckFieldInfo (rightFieldInfo);
						}
						else {
							right = false;
							rightValue = ConvertObject (binary.Right);
						}

						if (left && right) {
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightFieldInfo);
						}
						else if (left && !right) {
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightValue);
						}
						else if (!left && right) {
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
					QueryExpression queryExpression = ResolveQueryExpression (unary.Operand, state);
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
				if (!ParseDataFieldInfo (expression, state, out fieldInfo)) {
					throw new LambdaParseException (string.Format ("not allow constant member in MemberExpression \"{0}\"", member));
				}
				CheckFieldInfo (fieldInfo);
				return new LambdaBinaryQueryExpression (fieldInfo.TableMapping, QueryPredicate.Eq, fieldInfo, true);
			}
			MethodCallExpression methodcall = expression as MethodCallExpression;
			if (methodcall != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, state, out fieldInfo)) {
					throw new LambdaParseException (string.Format ("not allow constant method in MethodCallExpression \"{0}\"", methodcall));
				}
				CheckFieldInfo (fieldInfo);
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

