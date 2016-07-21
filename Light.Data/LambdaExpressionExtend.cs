using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

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

		internal static object ConvertLambdaObject (object value, CommandFactory factory, bool fullName, bool stringWarp, out DataParameter [] dataParameters)
		{
			DataFieldInfo fieldInfo = value as DataFieldInfo;
			if (!object.Equals (fieldInfo, null)) {
				return fieldInfo.CreateDataFieldSql (factory, fullName, out dataParameters);
			}
			dataParameters = null;
			object obj;
			Delegate dele = value as Delegate;
			if (dele != null) {
				obj = dele.DynamicInvoke (null);
			}
			else {
				obj = value;
			}
			if (stringWarp) {
				obj = factory.CreateStringWrap (obj);
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

		static QueryExpression CreateDataFieldQueryExpression (QueryPredicate predicate, DataFieldInfo leftFieldInfo, DataFieldInfo rightFieldInfo)
		{
			return new DataFieldQueryExpression (leftFieldInfo, predicate, rightFieldInfo, false);
		}

		static QueryExpression CreateQueryPredicateExpression (QueryPredicate predicate, DataFieldInfo fieldInfo, object value, bool isReverse)
		{
			QueryExpression queryExpression = null;
			if (object.Equals (value, null)) {
				if (predicate == QueryPredicate.Eq) {
					queryExpression = new NullQueryExpression (fieldInfo, true);
				}
				else if (predicate == QueryPredicate.NotEq) {
					queryExpression = new NullQueryExpression (fieldInfo, false);
				}
			}
			else if (value is Boolean) {
				bool b = (bool)value;
				if (predicate == QueryPredicate.Eq) {
					queryExpression = new BooleanQueryExpression (fieldInfo, b);
				}
				else if (predicate == QueryPredicate.NotEq) {
					queryExpression = new BooleanQueryExpression (fieldInfo, !b);
				}
			}
			else {
				queryExpression = new SingleParamQueryExpression (fieldInfo, predicate, value, isReverse);
			}
			if (queryExpression == null) {
				throw new LambdaParseException ("");
			}

			return queryExpression;
		}

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

		private static QueryExpression ResolveLambda (LambdaExpression expression)
		{
			ParameterExpression parameter = expression.Parameters [0];
			string name = parameter.Name;
			Type type = parameter.Type;
			DataEntityMapping mapping = DataMapping.GetEntityMapping (type);
			return Resolve (expression.Body, name, mapping);
		}

		private static DataFieldInfo CreateDateDataFieldInfo (DataFieldInfo fieldInfo, MemberInfo member)
		{
			switch (member.Name) {
			case "Date":
				fieldInfo = new DateDataFieldInfo (fieldInfo, null);
				break;
			case "Year":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Year);
				break;
			case "Month":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Month);
				break;
			case "Day":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Day);
				break;
			case "Hour":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Hour);
				break;
			case "Minute":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Minute);
				break;
			case "Second":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.Second);
				break;
			case "DayOfWeek":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.DayOfWeek);
				break;
			case "DayOfYear":
				fieldInfo = new DatePartDataFieldInfo (fieldInfo, DatePart.DayOfYear);
				break;
			default:
				throw new LambdaParseException ("");
			}
			return fieldInfo;
		}

		private static DataFieldInfo CreateStringDataFieldInfo (DataFieldInfo fieldInfo, MemberInfo member)
		{
			switch (member.Name) {
			case "Length":
				fieldInfo = new LengthDataFieldInfo (fieldInfo);
				break;
			default:
				throw new LambdaParseException ("");
			}
			return fieldInfo;
		}

		private static bool CreateMathDataFieldInfo (MethodInfo method, ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, string name, DataEntityMapping mapping, out DataFieldInfo fieldInfo)
		{
			throw new NotImplementedException ();
			//if(arguments.Count > 1)


			//fieldInfo = null;
			//DataFieldInfo baseFieldInfo = null;
			//switch (method.Name) {
			//case "Abs":
			//	if (arguments.Count == 1 && CheckDataFieldInfo (arguments [0], name, mapping, out baseFieldInfo)) {

			//	}
			//	fieldInfo = new LengthDataFieldInfo (fieldInfo);
			//	break;
			//default:
			//	throw new LambdaParseException ("");
			//}
			//return fieldInfo;
		}

		private static bool CheckDataFieldInfo (System.Linq.Expressions.Expression expression, string name, DataEntityMapping mapping, out DataFieldInfo fieldInfo)
		{
			fieldInfo = null;
			if (expression.NodeType == ExpressionType.Constant) {
				return false;
			}
			BinaryExpression binary = expression as BinaryExpression;
			if (binary != null) {
				MathOperator mathOperator;
				if (CheckMathOperator (binary.NodeType, out mathOperator)) {

				}
				else {
					throw new LambdaParseException ("");
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
						if (member.Type == typeof (DateTime)) {
							fieldInfo = CreateDateDataFieldInfo (fieldInfo, member.Member);
							return true;
						}
						else if (member.Type == typeof (string)) {
							fieldInfo = CreateStringDataFieldInfo (fieldInfo, member.Member);
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
				ParameterExpression param = member.Expression as ParameterExpression;
				if (param != null) {
					throw new LambdaParseException ("");
				}
				MethodInfo methodInfo = methodcall.Method;

				if ((methodInfo.Attributes & MethodAttributes.Static) == MethodAttributes.Static) {
					int args = methodcall.Arguments.Count;
					if (methodInfo.DeclaringType == typeof (Math)) {

					}
					else {
						throw new LambdaParseException ("");
					}
				}
				else {
					if (methodInfo.DeclaringType == typeof (DateTime)) {
						if (methodInfo.Name == "ToString") {

						}
					}
					if (methodInfo.DeclaringType == typeof (string)) {

					}
				}
				//return CreateMethodQueryExpression (methodcall, name, mapping);
				//return ResolveLinqToObject (methodcall, true);
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
			try {
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
								return CreateDataFieldQueryExpression (queryPredicate, leftFieldInfo, rightFieldInfo);
							}
							else if (!Object.Equals (leftFieldInfo, null) && Object.Equals (rightFieldInfo, null)) {
								return CreateQueryPredicateExpression (queryPredicate, leftFieldInfo, rightValue, false);
							}
							else if (Object.Equals (leftFieldInfo, null) && !Object.Equals (rightFieldInfo, null)) {
								return CreateQueryPredicateExpression (queryPredicate, rightFieldInfo, leftValue, true);
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
							return new NotQueryExpression (queryExpression);
						}
					}

					//if (unary.Operand is MethodCallExpression)//解析!x=>x.Name.Contains("xxx")或!array.Contains(x.Name)这类
					//	return ResolveLinqToObject (unary.Operand, false);
					//if (unary.Operand is MemberExpression & unary.NodeType == ExpressionType.Not)//解析x=>!x.isDeletion这样的 
					//{
					//	ConstantExpression constant = System.Linq.Expressions.Expression.Constant (false);
					//	return ResolveFunc (unary.Operand, constant, ExpressionType.Equal);
					//}
				}
				MemberExpression member = expression as MemberExpression;
				if (member != null) {
					DataFieldInfo fieldInfo;
					if (!CheckDataFieldInfo (expression, name, mapping, out fieldInfo)) {
						throw new LambdaParseException ("");
					}
					return CreateQueryPredicateExpression (QueryPredicate.Eq, fieldInfo, true, false);
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
			}
			catch (Exception ex) {

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

