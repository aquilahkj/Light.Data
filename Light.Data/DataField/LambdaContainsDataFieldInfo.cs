﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Data
{
	class LambdaContainsDataFieldInfo : LambdaDataFieldInfo, ISupportNotDefine, IDataFieldInfoConvert
	{
		object _collection;

		bool _isNot;

		public LambdaContainsDataFieldInfo (DataFieldInfo info, object collection)
			: base (info)
		{
			if (collection == null)
				throw new ArgumentNullException (nameof (collection));
			this._collection = collection;
		}

		public void SetNot ()
		{
			_isNot = !_isNot;
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		{
			string sql = null;
			List<DataParameter []> parameterList = new List<DataParameter []> ();
			DataParameter [] dataParameters1 = null;

			object obj = BaseFieldInfo.CreateDataFieldSql (factory, isFullName, out dataParameters1);

			IEnumerable values = LambdaExpressionExtend.ConvertLambdaObject (_collection) as IEnumerable;
			List<DataParameter> list = new List<DataParameter> ();
			if (values == null) {
				throw new LightDataException ("");
			}
			foreach (object value in values) {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, value));
			}
			DataParameter [] dataParameters2 = list.ToArray ();
			sql = factory.CreateCollectionParamsQuerySql (obj, _isNot ? QueryCollectionPredicate.NotIn : QueryCollectionPredicate.In, list);
			dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
			return sql;
		}

		public QueryExpression ConvertToExpression ()
		{
			return new LambdaContainsQueryExpression (this);
		}
	}
}
