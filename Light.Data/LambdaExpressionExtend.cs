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
			if (obj != null && Object.Equals (obj, DBNull.Value)) {
				obj = null;
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

		public static AggregateGroup CreateAggregateGroup (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				SingleParameterLambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);
				AggregateGroup group = null;
				MemberInitExpression memberInitObj = expression.Body as MemberInitExpression;
				if (memberInitObj != null) {
					group = ParseAggregateGroup (memberInitObj, state);
				}
				else {
					NewExpression newObj = expression.Body as NewExpression;
					if (newObj != null) {
						group = ParseAggregateGroup (newObj, state);
					}
				}
				if (group != null) {
					return group;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "group by", expression, ex.Message), ex);
			}
		}

		public static ISelector CreateSelector (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);
				Expression bodyExpression = expression.Body;
				if (bodyExpression is MemberInitExpression || bodyExpression is NewExpression) {
					List<string> list;
					if (ParseNewArguments (bodyExpression, state, out list)) {
						return state.CreateSelector (list.ToArray ());
					}
					else {
						throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
					}
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "select", expression, ex.Message), ex);
			}
		}

		public static ISelector CreateMutliSelector (LambdaExpression expression, List<IMap> maps)
		{
			//try {
			if (expression.Parameters.Count <= 1) {
				throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
			}
			LambdaState state = new MutliParameterLambdaState (expression.Parameters, maps);
			Expression bodyExpression = expression.Body;
			if (bodyExpression is MemberInitExpression || bodyExpression is NewExpression) {
				List<string> list;
				if (ParseNewArguments (bodyExpression, state, out list)) {
					return state.CreateSelector (list.ToArray ());
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
				}
			}
			else {
				throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
			}
			//}
			//catch (Exception ex) {
			//	throw new LightDataException (string.Format (RE.ParseExpressionError, "select", expression, ex.Message), ex);
			//}
		}

		public static OrderExpression ResolveLambdaOrderByExpression (LambdaExpression expression, OrderType orderType)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);
				DataFieldInfo dataFieldInfo;
				if (ParseDataFieldInfo (expression.Body, state, out dataFieldInfo)) {
					CheckFieldInfo (dataFieldInfo);
					OrderExpression exp = new DataFieldOrderExpression (dataFieldInfo, orderType);
					exp.MutliOrder = true;
					return exp;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "order", expression, ex.Message), ex);
			}
		}

		public static OrderExpression ResolveLambdaMutliOrderByExpression (LambdaExpression expression, OrderType orderType, List<IMap> maps)
		{
			try {
				if (expression.Parameters.Count <= 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new MutliParameterLambdaState (expression.Parameters, maps);
				DataFieldInfo dataFieldInfo;
				if (ParseDataFieldInfo (expression.Body, state, out dataFieldInfo)) {
					CheckFieldInfo (dataFieldInfo);
					OrderExpression exp = new DataFieldOrderExpression (dataFieldInfo, orderType);
					exp.MutliOrder = true;
					return exp;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "order", expression, ex.Message), ex);
			}
		}

		public static QueryExpression ResolveLambdaQueryExpression (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);
				QueryExpression query = ResolveQueryExpression (expression.Body, state);
				query.MutliQuery = state.MutliEntity;
				return query;
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "query", expression, ex.Message), ex);
			}
		}

		public static QueryExpression ResolveLambdaMutliQueryExpression (LambdaExpression expression, List<IMap> maps)
		{
			try {
				if (expression.Parameters.Count <= 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new MutliParameterLambdaState (expression.Parameters, maps);
				QueryExpression query = ResolveQueryExpression (expression.Body, state);
				query.MutliQuery = state.MutliEntity;
				return query;
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "query", expression, ex.Message), ex);
			}
		}

		public static QueryExpression ResolveLambdaHavingExpression (LambdaExpression expression, AggregateGroup group)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new AggregateLambdaState (expression.Parameters [0], group);
				QueryExpression query = ResolveQueryExpression (expression.Body, state);
				//query.MutliQuery = state.MutliQuery;
				return query;
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "having", expression, ex.Message), ex);
			}
		}

		public static OrderExpression ResolveLambdaAggregateOrderByExpression (LambdaExpression expression, OrderType orderType, AggregateGroup group)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				DataFieldInfo dataFieldInfo;
				LambdaState state = new AggregateLambdaState (expression.Parameters [0], group);
				if (ParseDataFieldInfo (expression.Body, state, out dataFieldInfo)) {
					CheckFieldInfo (dataFieldInfo);
					OrderExpression exp = new DataFieldOrderExpression (dataFieldInfo, orderType);
					//exp.MutliOrder = true;
					return exp;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "aggregate order", expression, ex.Message), ex);
			}
		}

		public static DataFieldExpression ResolvelambdaOnExpression (LambdaExpression expression, List<IMap> maps)
		{
			try {
				if (expression.Parameters.Count <= 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				LambdaState state = new MutliParameterLambdaState (expression.Parameters, maps);
				DataFieldExpression on = ResolveOnExpression (expression.Body, state);
				return on;
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "on", expression, ex.Message), ex);
			}
		}

		public static InsertSelector CreateInsertSelector (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				SingleParameterLambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);

				MemberInitExpression memberInitObj = expression.Body as MemberInitExpression;
				if (memberInitObj != null) {
					DataTableEntityMapping insertMapping = DataEntityMapping.GetTableMapping (memberInitObj.Type);
					RelationMap map = insertMapping.GetRelationMap ();
					InsertSelector selector = new InsertSelector (insertMapping, state.MainMapping);
					if (memberInitObj.Bindings != null) {
						foreach (MemberBinding binding in memberInitObj.Bindings) {
							MemberAssignment ass = binding as MemberAssignment;
							if (ass != null) {
								Expression innerExpression = ass.Expression;
								DataFieldInfo selectField;
								if (!ParseDataFieldInfo (innerExpression, state, out selectField)) {
									object obj = ConvertObject (innerExpression);
									selectField = new LambdaConstantDataFieldInfo (obj);
								}
								string mypath = "." + ass.Member.Name;
								DataFieldInfo insertField = map.CreateFieldInfoForPath (mypath);
								//if (Object.Equals (insertField, null)) {
								//	throw new LambdaParseException (LambdaParseMessage.CanNotFindFieldInfoViaSpecialPath, mypath);
								//}
								selector.SetInsertField (insertField);
								selector.SetSelectField (selectField);
							}
							else {
								throw new LambdaParseException (LambdaParseMessage.ExpressionBindingError, binding.Member);
							}
						}
					}
					else {
						throw new LambdaParseException (LambdaParseMessage.ExpressionNoMember);
					}
					return selector;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "insert", expression, ex.Message), ex);
			}
		}

		public static MassUpdator CreateMassUpdator (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				SingleParameterLambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);

				MemberInitExpression memberInitObj = expression.Body as MemberInitExpression;
				if (memberInitObj != null) {
					DataTableEntityMapping updateMapping = DataEntityMapping.GetTableMapping (memberInitObj.Type);
					RelationMap map = updateMapping.GetRelationMap ();
					MassUpdator updator = new MassUpdator (updateMapping);
					if (memberInitObj.Bindings != null && memberInitObj.Bindings.Count > 0) {
						foreach (MemberBinding binding in memberInitObj.Bindings) {
							MemberAssignment ass = binding as MemberAssignment;
							if (ass != null) {
								Expression innerExpression = ass.Expression;
								DataFieldInfo valueField;
								if (!ParseDataFieldInfo (innerExpression, state, out valueField)) {
									object obj = ConvertObject (innerExpression);
									valueField = new LambdaConstantDataFieldInfo (obj);
								}
								else if (state.MutliEntity) {
									throw new LambdaParseException (LambdaParseMessage.ExpressionUnsupportRelateField);
								}
								string mypath = "." + ass.Member.Name;
								DataFieldInfo keyField = map.CreateFieldInfoForPath (mypath);
								//if (Object.Equals (keyField, null)) {
								//	throw new LambdaParseException (LambdaParseMessage.CanNotFindFieldInfoViaSpecialPath, mypath);
								//}
								updator.SetUpdateData (keyField, valueField);
							}
							else {
								throw new LambdaParseException (LambdaParseMessage.ExpressionBindingError, binding.Member);
							}
						}
					}
					else {
						throw new LambdaParseException (LambdaParseMessage.ExpressionNoMember);
					}
					return updator;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "updator", expression, ex.Message), ex);
			}
		}

		public static DataFieldInfo ResolveSingleField (LambdaExpression expression)
		{
			try {
				if (expression.Parameters.Count != 1) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
				}
				SingleParameterLambdaState state = new SingleParameterLambdaState (expression.Parameters [0]);
				DataFieldInfo fieldInfo;
				if (ParseDataFieldInfo (expression.Body, state, out fieldInfo)) {
					if (state.MutliEntity) {
						throw new LambdaParseException (LambdaParseMessage.ExpressionUnsupportRelateField);
					}
					return fieldInfo;
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
				}
			}
			catch (Exception ex) {
				throw new LightDataException (string.Format (RE.ParseExpressionError, "single field", expression, ex.Message), ex);
			}
		}

		//private static LambdaState CreateLambdaState (LambdaExpression expression)
		//{
		//	if (expression.Parameters.Count == 0) {
		//		throw new LambdaParseException (LambdaParseMessage.ExpressionParameterCountError);
		//	}
		//	else if (expression.Parameters.Count == 1) {
		//		return new SingleParameterLambdaState (expression.Parameters [0]);
		//	}
		//	else {
		//		return new MutliParameterLambdaState (expression.Parameters);
		//	}
		//}

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
							switch (pathType) {
							case LambdaPathType.Field:
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							case LambdaPathType.RelateEntity:
								pathList = null;
								return true;
							case LambdaPathType.RelateCollection:
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							case LambdaPathType.Parameter:
								throw new LambdaParseException (LambdaParseMessage.ExpressionMemberInvalid);
							default:
								pathList = new List<string> ();
								pathList.Add (param.Name);
								return true;
							}
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.ExpressionParameterTypeError, param.Name, param.Type);
						}
					}
					List<string> memberList = null;
					if (ParseNewArguments (memberObj.Expression, state, out memberList)) {
						if (memberList == null) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							switch (pathType) {
							case LambdaPathType.Field:
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							case LambdaPathType.RelateEntity:
								pathList = null;
								return true;
							case LambdaPathType.RelateCollection:
								pathList = new List<string> ();
								pathList.Add (fullPath);
								return true;
							default:
								pathList = new List<string> ();
								pathList.Add (memberObj.Expression.ToString ());
								return true;
							}
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
			MemberInitExpression memberInitObj = expression as MemberInitExpression;
			if (memberInitObj != null) {
				bool ret = false;
				List<string> memberInitList = new List<string> ();

				if (memberInitObj.Bindings != null && memberInitObj.Bindings.Count > 0) {
					foreach (MemberAssignment ass in memberInitObj.Bindings) {
						if (ass != null) {
							List<string> argList = null;
							if (ParseNewArguments (ass.Expression, state, out argList)) {
								ret = true;
								if (argList == null) {
									memberInitList.Add (ass.Expression.ToString ());
								}
								else {
									memberInitList.AddRange (argList);
								}
							}
						}
					}
				}
				if (memberInitObj.NewExpression != null) {
					List<string> newList = null;
					if (ParseNewArguments (memberInitObj.NewExpression, state, out newList)) {
						ret = true;
						if (newList == null) {
							memberInitList.Add (methodcallObj.Object.ToString ());
						}
						else {
							memberInitList.AddRange (newList);
						}
					}
				}
				if (ret) {
					pathList = memberInitList;
					return true;
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
			throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
		}

		private static void CheckFieldInfo (DataFieldInfo fieldInfo)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new LambdaParseException (LambdaParseMessage.ExpressionParseFieldFailed);
			}
		}

		private static AggregateGroup ParseAggregateGroup (MemberInitExpression expression, SingleParameterLambdaState state)
		{
			DataEntityMapping entityMapping = state.MainMapping;
			SpecialAggregateMapping arrgregateMapping = SpecialAggregateMapping.GetAggregateMapping (expression.Type);
			AggregateGroup model = new AggregateGroup (entityMapping, arrgregateMapping);
			if (expression.Bindings != null && expression.Bindings.Count > 0) {
				foreach (MemberBinding binding in expression.Bindings) {
					MemberAssignment ass = binding as MemberAssignment;
					if (ass != null) {
						Expression innerExpression = ass.Expression;
						DataFieldInfo fieldInfo;
						state.AggregateField = false;
						if (ParseDataFieldInfo (innerExpression, state, out fieldInfo)) {
							if (state.MutliEntity) {
								throw new LambdaParseException (LambdaParseMessage.ExpressionUnsupportRelateField);
							}
							if (state.AggregateField) {
								model.AddAggregateField (ass.Member.Name, fieldInfo);
							}
							else {
								model.AddGroupByField (ass.Member.Name, fieldInfo);
							}
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
						}
					}
					else {
						throw new LambdaParseException (LambdaParseMessage.ExpressionBindingError, binding.Member);
					}
				}
			}
			else {
				throw new LambdaParseException (LambdaParseMessage.ExpressionNoMember);
			}
			return model;
		}

		private static AggregateGroup ParseAggregateGroup (NewExpression expression, SingleParameterLambdaState state)
		{
			DataEntityMapping entityMapping = state.MainMapping;
			DynamicAggregateMapping arrgregateMapping = DynamicAggregateMapping.GetAggregateMapping (expression.Type);
			AggregateGroup model = new AggregateGroup (entityMapping, arrgregateMapping);
			if (expression.Arguments != null && expression.Arguments.Count > 0) {
				int index = 0;
				foreach (Expression arg in expression.Arguments) {
					MemberInfo member = expression.Members [index];
					Expression innerExpression = arg;
					DataFieldInfo fieldInfo;
					state.AggregateField = false;
					if (ParseDataFieldInfo (innerExpression, state, out fieldInfo)) {
						if (state.MutliEntity) {
							throw new LambdaParseException (LambdaParseMessage.ExpressionUnsupportRelateField);
						}
						if (state.AggregateField) {
							model.AddAggregateField (member.Name, fieldInfo);
						}
						else {
							model.AddGroupByField (member.Name, fieldInfo);
						}
						index++;
					}
					else {
						throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
					}
				}
			}
			else {
				throw new LambdaParseException (LambdaParseMessage.ExpressionNoArguments);
			}
			return model;
		}

		private static bool ParseDataFieldInfo (Expression expression, LambdaState state, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			if (expression.NodeType == ExpressionType.Constant) {
				return false;
			}
			BinaryExpression binaryObj = expression as BinaryExpression;
			if (binaryObj != null) {
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
				if (!left && !right) {
					return false;
				}

				if (binaryObj.Method != null && binaryObj.NodeType == ExpressionType.Add && binaryObj.Method.DeclaringType == typeof (string) && binaryObj.Method.Name == "Concat") {
					if (left && right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo.TableMapping, leftFieldInfo, rightFieldInfo);
						return true;
					}
					else if (left && !right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (leftFieldInfo.TableMapping, leftFieldInfo, rightValue);
						return true;
					}
					else if (!left && right) {
						fieldInfo = new LambdaStringConcatDataFieldInfo (rightFieldInfo.TableMapping, leftValue, rightFieldInfo);
						return true;
					}
				}
				else {
					MathOperator mathOperator;
					if (CheckMathOperator (binaryObj.NodeType, out mathOperator)) {
						if (left && right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo.TableMapping, mathOperator, leftFieldInfo, rightFieldInfo);
							return true;
						}
						else if (left && !right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (leftFieldInfo.TableMapping, mathOperator, leftFieldInfo, rightValue);
							return true;
						}
						else if (!left && right) {
							fieldInfo = new LambdaMathCalculateDataFieldInfo (rightFieldInfo.TableMapping, mathOperator, leftValue, rightFieldInfo);
							return true;
						}
					}
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binaryObj.NodeType, out queryPredicate)) {
						QueryExpression queryExpression;
						if (left && right) {
							queryExpression = new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightFieldInfo);
						}
						else if (left && !right) {
							queryExpression = new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightValue);
						}
						else if (!left && right) {
							queryExpression = new LambdaBinaryQueryExpression (rightFieldInfo.TableMapping, queryPredicate, rightFieldInfo, leftValue);
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.BinaryExpressionNotAllowBothConstantValue);
						}
						fieldInfo = new LambdaQueryDataFieldInfo (queryExpression);
						return true;
					}
					throw new LambdaParseException (LambdaParseMessage.ExpressionNodeTypeUnsuppore, binaryObj.NodeType);
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
								DataFieldInfo myinfo = state.GetDataFieldInfo (fullPath);
								fieldInfo = myinfo;
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								return true;
							}
							else {
								throw new LambdaParseException (LambdaParseMessage.ExpressionMemberInvalid);
							}
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.ExpressionParameterTypeError, param.Name, param.Type);
						}
					}
					if (ParseDataFieldInfo (memberObj.Expression, state, out fieldInfo)) {
						if (Object.Equals (fieldInfo, null)) {
							string fullPath = memberObj.ToString ();
							LambdaPathType pathType = state.ParsePath (fullPath);
							if (pathType == LambdaPathType.Field) {
								DataFieldInfo myinfo = state.GetDataFieldInfo (fullPath);
								fieldInfo = myinfo;
								state.MutliEntity = true;
								return true;
							}
							else if (pathType == LambdaPathType.RelateEntity) {
								return true;
							}
							else {
								throw new LambdaParseException (LambdaParseMessage.ExpressionMemberInvalid);
							}
						}
						else {
							if (memberObj.Expression.Type.IsGenericType) {
								Type frameType = memberObj.Expression.Type.GetGenericTypeDefinition ();
								if (frameType.FullName == "System.Nullable`1" && memberObj.Member.Name == "Value") {
									return true;
								}
							}

							if (memberObj.Expression.Type == typeof (DateTime)) {
								fieldInfo = CreateDateDataFieldInfo (memberObj.Member, fieldInfo);
								return true;
							}
							else if (memberObj.Expression.Type == typeof (string)) {
								fieldInfo = CreateStringMemberDataFieldInfo (memberObj.Member, fieldInfo);
								return true;
							}
							else {
								throw new LambdaParseException (LambdaParseMessage.MemberExpressionTypeUnsupport, memberObj.Expression.Type);
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
					if (methodInfo.DeclaringType == typeof (Function)) {
						fieldInfo = ParseAggregateData (methodcallObj, state);
						return true;
					}

					object [] argObjects;
					DataFieldInfo mainFieldInfo;
					if (ParseArguments (methodcallObj.Arguments, state, out argObjects, out mainFieldInfo)) {
						CheckFieldInfo (mainFieldInfo);
						if (methodInfo.DeclaringType == typeof (Math)) {
							fieldInfo = ParseMathFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, state);
							return true;
						}
						if (methodInfo.DeclaringType == typeof (string)) {
							fieldInfo = ParseStaticStringFunctionDataFieldInfo (methodInfo, mainFieldInfo, argObjects, state);
							return true;
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.MethodExpressionTypeUnsupport, methodInfo.DeclaringType);
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
					fieldInfo = new LambdaNewArrayDataFieldInfo (arrayFieldInfo.TableMapping, argsObjects);
					return true;
				}
				else {
					return false;
				}
			}
			ConditionalExpression conditionObj = expression as ConditionalExpression;
			if (conditionObj != null) {
				QueryExpression query = ResolveQueryExpression (conditionObj.Test, state);
				DataFieldInfo ifTrueFieldInfo;
				object ifTrueValue = null;
				if (ParseDataFieldInfo (conditionObj.IfTrue, state, out ifTrueFieldInfo)) {
					CheckFieldInfo (ifTrueFieldInfo);
					ifTrueValue = ifTrueFieldInfo;
				}
				else {
					ifTrueValue = ConvertObject (conditionObj.IfTrue);
				}
				DataFieldInfo ifFalseFieldInfo;
				object ifFalseValue = null;
				if (ParseDataFieldInfo (conditionObj.IfFalse, state, out ifFalseFieldInfo)) {
					CheckFieldInfo (ifFalseFieldInfo);
					ifFalseValue = ifFalseFieldInfo;
				}
				else {
					ifFalseValue = ConvertObject (conditionObj.IfFalse);
				}
				fieldInfo = new LambdaConditionDataFieldInfo (query, ifTrueValue, ifFalseValue);
				return true;
			}
			throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
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
			switch (member.Name) {
			case "Date":
				fieldInfo = new LambdaDateDataFieldInfo (fieldInfo);
				break;
			default:
				DatePart datePart;
				if (Enum.TryParse<DatePart> (member.Name, out datePart)) {
					fieldInfo = new LambdaDatePartDataFieldInfo (fieldInfo, datePart);
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.MemberExpressionMemberUnsupport, "DateTime", member.Name);
				}
				break;
			}
			return fieldInfo;
		}

		private static DataFieldInfo CreateStringMemberDataFieldInfo (MemberInfo member, DataFieldInfo fieldInfo)
		{
			switch (member.Name) {
			case "Length":
				fieldInfo = new LambdaStringLengthDataFieldInfo (fieldInfo);
				break;
			default:
				throw new LambdaParseException (LambdaParseMessage.MemberExpressionMemberUnsupport, "string", member.Name);
			}
			return fieldInfo;
		}

		private static DataFieldInfo ParseMathFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaState state)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			MathFunction mathFunction;
			if (Enum.TryParse<MathFunction> (method.Name, out mathFunction)) {
				if (parameterInfos == null || parameterInfos.Length == 0) {
					throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "Math", method.Name);
				}
				else if (mathFunction == MathFunction.Atan2 || mathFunction == MathFunction.Max || mathFunction == MathFunction.Min || mathFunction == MathFunction.Pow) {
					if (parameterInfos.Length != 2) {
						throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "Math", method.Name);
					}
				}
				else if (mathFunction == MathFunction.Log || mathFunction == MathFunction.Round) {
					if (parameterInfos.Length > 2) {
						throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "Math", method.Name);
					}
				}
				return new LambdaMathFunctionDataFieldInfo (mainFieldInfo.TableMapping, mathFunction, argObjects);
			}
			throw new LambdaParseException (LambdaParseMessage.MethodExpressionMethodUnsupport, "Math", method.Name);
		}

		private static DataFieldInfo ParseStaticStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object [] argObjects, LambdaState state)
		{
			if (method.Name == "Concat") {
				if (argObjects.Length == 1) {
					LambdaNewArrayDataFieldInfo newarray = argObjects [0] as LambdaNewArrayDataFieldInfo;
					if (!Object.Equals (newarray, null)) {
						return new LambdaStringConcatDataFieldInfo (newarray.TableMapping, newarray.Values);
					}
					else {
						return new LambdaStringConcatDataFieldInfo (mainFieldInfo.TableMapping, argObjects);
					}
				}
				else {
					return new LambdaStringConcatDataFieldInfo (mainFieldInfo.TableMapping, argObjects);
				}
			}
			throw new LambdaParseException (LambdaParseMessage.MethodExpressionMethodUnsupport, "string", method.Name);
		}

		private static DataFieldInfo ParseInstanceStringFunctionDataFieldInfo (MethodInfo method, DataFieldInfo mainFieldInfo, object callObject, object [] argObjects, LambdaState state)
		{
			ParameterInfo [] parameterInfos = method.GetParameters ();
			switch (method.Name) {
			case "StartsWith":
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "string", method.Name);
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo.TableMapping, true, false, callObject, argObjects [0]);
			case "EndsWith":
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "string", method.Name);
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo.TableMapping, false, true, callObject, argObjects [0]);
			case "Contains":
				if (parameterInfos.Length != 1) {
					throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "string", method.Name);
				}
				return new LambdaStringMatchDataFieldInfo (mainFieldInfo.TableMapping, true, true, callObject, argObjects [0]);
			default:
				StringFunction stringFunction;
				if (Enum.TryParse<StringFunction> (method.Name, out stringFunction)) {
					if (stringFunction == StringFunction.IndexOf && !(parameterInfos.Length == 1 || (parameterInfos.Length == 2 && parameterInfos [1].ParameterType == typeof (int)))) {
						throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "string", method.Name);
					}
					else if (stringFunction == StringFunction.Trim && parameterInfos.Length > 0) {
						throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "string", method.Name);
					}
					return new LambdaStringFunctionDataFieldInfo (mainFieldInfo.TableMapping, stringFunction, callObject, argObjects);
				}

				break;
			}

			throw new LambdaParseException (LambdaParseMessage.MethodExpressionMethodUnsupport, "string", method.Name);
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
						throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "DateTime", method.Name);
					}
				}
				else {
					throw new LambdaParseException (LambdaParseMessage.MethodExpressionArgumentError, "DateTime", method.Name);
				}
			}
			throw new LambdaParseException (LambdaParseMessage.MethodExpressionMethodUnsupport, "DateTime", method.Name);
		}

		private static DataFieldInfo ParseContainsDataFieldInfo (MethodInfo methodInfo, DataFieldInfo mainFieldInfo, object collections, LambdaState state)
		{
			return new LambdaContainsDataFieldInfo (mainFieldInfo, collections);
		}

		private static DataFieldInfo ParseAggregateData (MethodCallExpression expression, LambdaState state)
		{
			MethodInfo method = expression.Method;

			ReadOnlyCollection<Expression> paramExpressions = expression.Arguments;
			DataFieldInfo data = null;
			DataFieldInfo fieldInfo = null;
			QueryExpression queryExpression = null;

			ParameterInfo [] infos = method.GetParameters ();

			for (int i = 0; i < infos.Length; i++) {
				ParameterInfo info = infos [i];
				if (info.Name == "field") {
					if (!ParseDataFieldInfo (paramExpressions [i], state, out fieldInfo)) {
						throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
					}
				}
				else if (info.Name == "expression") {
					queryExpression = ResolveQueryExpression (paramExpressions [i], state);
				}
			}

			//if (paramExpressions.Count >= 1) {
			//	if (!ParseDataFieldInfo (paramExpressions [0], state, out fieldInfo)) {
			//		throw new LambdaParseException (LambdaParseMessage.ExpressionNotContainDataField);
			//	}
			//}
			//if (paramExpressions.Count == 2) {
			//	queryExpression = ResolveQueryExpression (paramExpressions [1], state);
			//}

			switch (method.Name) {
			case "CountAll":
			case "LongCountAll":
				switch (paramExpressions.Count) {
				case 0:
					data = new LambdaAggregateCountAllDataFieldInfo (null);
					break;
				case 1:
					data = new LambdaAggregateCountAllDataFieldInfo (queryExpression);
					break;
				}
				break;
			case "Count":
			case "LongCount":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.COUNT, false, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.COUNT, false, queryExpression);
					break;
				}
				break;
			case "DistinctCount":
			case "DistinctLongCount":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.COUNT, true, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.COUNT, true, queryExpression);
					break;
				}

				break;
			case "Sum":
			case "LongSum":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.SUM, false, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.SUM, false, queryExpression);
					break;
				}

				break;
			case "DistinctSum":
			case "DistinctLongSum":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.SUM, true, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.SUM, true, queryExpression);
					break;
				}
				break;
			case "Avg":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.AVG, false, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.AVG, false, queryExpression);
					break;
				}

				break;
			case "DistinctAvg":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.AVG, true, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.AVG, true, queryExpression);
					break;
				}

				break;
			case "Max":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.MAX, false, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.MAX, false, queryExpression);
					break;
				}

				break;
			case "Min":
				switch (paramExpressions.Count) {
				case 1:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.MIN, false, null);
					break;
				case 2:
					data = new LambdaAggregateDataFieldInfo (fieldInfo, AggregateType.MIN, false, queryExpression);
					break;
				}

				break;
			}
			CheckFieldInfo (data);
			state.AggregateField = true;
			return data;
		}

		private static QueryExpression ResolveQueryExpression (Expression expression, LambdaState state)
		{
			BinaryExpression binaryObj = expression as BinaryExpression;
			if (binaryObj != null) {
				CatchOperatorsType catchType;
				if (CheckCatchOperatorsType (binaryObj.NodeType, out catchType)) {
					var left = ResolveQueryExpression (binaryObj.Left, state);
					var right = ResolveQueryExpression (binaryObj.Right, state);
					return QueryExpression.Catch (left, catchType, right);
				}
				else {
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binaryObj.NodeType, out queryPredicate)) {
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
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightFieldInfo);
						}
						else if (left && !right) {
							return new LambdaBinaryQueryExpression (leftFieldInfo.TableMapping, queryPredicate, leftFieldInfo, rightValue);
						}
						else if (!left && right) {
							return new LambdaBinaryQueryExpression (rightFieldInfo.TableMapping, queryPredicate, rightFieldInfo, leftValue);
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.BinaryExpressionNotAllowBothConstantValue);
						}
					}
				}
			}
			UnaryExpression unaryObj = expression as UnaryExpression;
			if (unaryObj != null) {
				if (unaryObj.NodeType == ExpressionType.Not) {
					QueryExpression queryExpression = ResolveQueryExpression (unaryObj.Operand, state);
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
			MemberExpression memberObj = expression as MemberExpression;
			if (memberObj != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, state, out fieldInfo)) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotAllowNoDataField, memberObj);
				}
				CheckFieldInfo (fieldInfo);
				return new LambdaBinaryQueryExpression (fieldInfo.TableMapping, QueryPredicate.Eq, fieldInfo, true);
			}
			MethodCallExpression methodcallObj = expression as MethodCallExpression;
			if (methodcallObj != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, state, out fieldInfo)) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotAllowNoDataField, methodcallObj);
				}
				CheckFieldInfo (fieldInfo);
				IDataFieldInfoConvert convertFieldInfo = fieldInfo as IDataFieldInfoConvert;
				if (Object.Equals (convertFieldInfo, null)) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotAllowNoDataField, methodcallObj);
				}
				else {
					return convertFieldInfo.ConvertToExpression ();
				}
			}
			ConditionalExpression conditionObj = expression as ConditionalExpression;
			if (conditionObj != null) {
				DataFieldInfo fieldInfo;
				if (!ParseDataFieldInfo (expression, state, out fieldInfo)) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotAllowNoDataField, methodcallObj);
				}
				CheckFieldInfo (fieldInfo);
				IDataFieldInfoConvert convertFieldInfo = fieldInfo as IDataFieldInfoConvert;
				if (Object.Equals (convertFieldInfo, null)) {
					throw new LambdaParseException (LambdaParseMessage.ExpressionNotAllowNoDataField, methodcallObj);
				}
				else {
					return convertFieldInfo.ConvertToExpression ();
				}
			}
			throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
		}

		private static DataFieldExpression ResolveOnExpression (Expression expression, LambdaState state)
		{
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				CatchOperatorsType catchType;
				if (CheckCatchOperatorsType (binary.NodeType, out catchType)) {
					var left = ResolveOnExpression (binary.Left, state);
					var right = ResolveOnExpression (binary.Right, state);
					return DataFieldExpression.Catch (left, catchType, right);
				}
				else {
					QueryPredicate queryPredicate;
					if (CheckQueryPredicate (binary.NodeType, out queryPredicate)) {
						DataFieldInfo leftFieldInfo; ;
						bool left;
						if (ParseDataFieldInfo (binary.Left, state, out leftFieldInfo)) {
							left = true;
							CheckFieldInfo (leftFieldInfo);
						}
						else {
							left = false;
						}

						DataFieldInfo rightFieldInfo;
						bool right;
						if (ParseDataFieldInfo (binary.Right, state, out rightFieldInfo)) {
							right = true;
							CheckFieldInfo (rightFieldInfo);
						}
						else {
							right = false;
						}

						if (left && right) {
							return new DataFieldMatchExpression (leftFieldInfo, rightFieldInfo, queryPredicate);
						}
						else {
							throw new LambdaParseException (LambdaParseMessage.BinaryExpressionNotAllowBothConstantValue);
						}
					}
				}
			}

			throw new LambdaParseException (LambdaParseMessage.ExpressionTypeInvalid);
		}
	}
}

