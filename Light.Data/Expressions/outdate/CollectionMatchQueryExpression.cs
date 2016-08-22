using System;
using System.Collections.Generic;

namespace Light.Data
{
	class CollectionMatchQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo;

		string _value;

		bool _isReverse;

		bool _starts;

		bool _ends;

		bool _isNot;

		IEnumerable<string> _values;

		public CollectionMatchQueryExpression (DataFieldInfo fieldInfo, string value, bool isReverse, bool starts, bool ends, bool isNot)
			: base (fieldInfo.TableMapping)
		{
			if (value == null)
				throw new ArgumentNullException (nameof (value));
			_value = value;
			_isReverse = isReverse;
			_starts = starts;
			_ends = ends;
			_isNot = isNot;
			_fieldInfo = fieldInfo;
		}

		public CollectionMatchQueryExpression (DataFieldInfo fieldInfo, IEnumerable<string> values, bool isReverse, bool starts, bool ends, bool isNot)
			: base (fieldInfo.TableMapping)
		{
			if (values == null)
				throw new ArgumentNullException (nameof (values));
			_values = values;
			_isReverse = isReverse;
			_starts = starts;
			_ends = ends;
			_isNot = isNot;
			_fieldInfo = fieldInfo;
		}

		//internal override string CreateSqlString (CommandFactory factory, bool fullFieldName)
		//{
		//	List<DataParameter> dataParameters1 = new List<DataParameter> ();
		//	if (_values != null) {
		//		foreach (string value in _values) {
		//			string pn = factory.CreateTempParamName ();
		//			dataParameters1.Add (new DataParameter (pn, _fieldInfo.ToParameter (value)));
		//		}
		//		if (dataParameters1.Count == 0) {
		//			string pn = factory.CreateTempParamName ();
		//			dataParameters1.Add (new DataParameter (pn, string.Empty));
		//		}
		//	}
		//	else {
		//		string pn = factory.CreateTempParamName ();
		//		dataParameters1.Add (new DataParameter (pn, _fieldInfo.ToParameter (_value)));
		//	}
		//	//dataParameters = dataParameters1.ToArray ();
		//	return factory.CreateCollectionMatchQuerySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _isReverse, _starts, _ends, _isNot, dataParameters1);
		//}

		//internal override string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	List<DataParameter> dataParameters1 = new List<DataParameter> ();
		//	DataParameter [] dataParameters2 = null;
		//	List<string> list = new List<string> ();
		//	if (_values != null) {
		//		foreach (string value in _values) {
		//			string pn = factory.CreateTempParamName ();
		//			dataParameters1.Add (new DataParameter (pn, _fieldInfo.ToParameter (value)));
		//			list.Add (pn);
		//		}
		//	}
		//	else {
		//		string pn = factory.CreateTempParamName ();
		//		dataParameters1.Add (new DataParameter (pn, _fieldInfo.ToParameter (_value)));
		//		list.Add (pn);
		//	}
		//	string sql = factory.CreateCollectionMatchQuerySql (_fieldInfo.CreateSqlString (factory, isFullName, out dataParameters2), _isReverse, _starts, _ends, _isNot, list);
		//	dataParameters = DataParameter.ConcatDataParameters (dataParameters1, dataParameters2);
		//	return sql;
		//}

		internal override string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			List<string> list = new List<string> ();
			if (_values != null) {
				foreach (string value in _values) {
					string pn = state.AddDataParameter (_fieldInfo.ToParameter (value));
					list.Add (pn);
				}
			}
			else {
				string pn = state.AddDataParameter (_fieldInfo.ToParameter (_value));
				list.Add (pn);
			}

			string sql = factory.CreateCollectionMatchQuerySql (_fieldInfo.CreateSqlString (factory, isFullName, state), _isReverse, _starts, _ends, _isNot, list);

			return sql;
		}

		//protected override bool EqualsDetail (QueryExpression expression)
		//{
		//	if (base.EqualsDetail (expression)) {
		//		CollectionMatchQueryExpression target = expression as CollectionMatchQueryExpression;
		//		return this._fieldInfo.Equals (target._fieldInfo)
		//		&& this._isReverse == target._isReverse
		//		&& this._starts == target._starts
		//		&& this._ends == target._ends
		//		&& this._isNot == target._isNot
		//		&& Utility.EnumableObjectEquals (this._value, target._value);
		//	}
		//	else {
		//		return false;
		//	}
		//}
	}
}
