using System;
using System.Collections.Generic;

namespace Light.Data
{
	class LambdaStringConcatDataFieldInfo : LambdaDataFieldInfo
	{
		object [] _values;

		public LambdaStringConcatDataFieldInfo (DataEntityMapping mapping, params object [] values)
			: base (mapping)
		{
			if (values == null)
				throw new ArgumentNullException (nameof (values));
			//if (values.Length < 2)
			//	throw new ArgumentOutOfRangeException (nameof (values), "length less than 2");
			this._values = values;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	string sql = null;

		//	List<DataParameter []> parameterList = new List<DataParameter []> ();
		//	List<object> objectList = new List<object> ();
		//	foreach (object item in _values) {
		//		object obj1;
		//		DataParameter [] dataParameters1 = null;
		//		DataFieldInfo info1 = item as DataFieldInfo;
		//		if (!Object.Equals (info1, null)) {
		//			obj1 = info1.CreateSqlString (factory, isFullName, out dataParameters1);
		//		}
		//		else {
		//			obj1 = LambdaExpressionExtend.ConvertLambdaObject (item);
		//			if (obj1 == null) {
		//				obj1 = string.Empty;
		//			}
		//			else if(!(obj1 is string)){
		//				obj1 = obj1.ToString ();
		//			}
		//			string pn = factory.CreateTempParamName ();
		//			DataParameter dataParameter = new DataParameter (pn, obj1);
		//			dataParameters1 = new [] { dataParameter };
		//			obj1 = dataParameter.ParameterName;
		//		}
		//		if (dataParameters1 != null) {
		//			parameterList.Add (dataParameters1);
		//		}
		//		objectList.Add (obj1);
		//	}
		//	sql = factory.CreateConcatSql (objectList.ToArray());
		//	dataParameters = DataParameter.ConcatDataParameters (parameterList.ToArray());
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			string sql = state.GetDataSql (this, isFullName);
			if (sql != null) {
				return sql;
			}

			List<object> objectList = new List<object> ();
			foreach (object item in _values) {
				object obj1;
				DataFieldInfo info1 = item as DataFieldInfo;
				if (!Object.Equals (info1, null)) {
					obj1 = info1.CreateSqlString (factory, isFullName, state);
				}
				else {
					obj1 = LambdaExpressionExtend.ConvertLambdaObject (item);
					if (obj1 == null) {
						obj1 = string.Empty;
					}
					else if (!(obj1 is string)) {
						obj1 = obj1.ToString ();
					}
					obj1 = state.AddDataParameter (obj1);
				}
				objectList.Add (obj1);
			}
			sql = factory.CreateConcatSql (objectList.ToArray ());
			state.SetDataSql (this, isFullName, sql);
			return sql;
		}
	}
}

