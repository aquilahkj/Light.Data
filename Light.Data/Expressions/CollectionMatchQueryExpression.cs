using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	class CollectionMatchQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		string _value = null;

		bool _isReverse = false;

		bool _starts = false;

		bool _ends = false;

		bool _isNot = false;

		IEnumerable<string> _values = null;

		public CollectionMatchQueryExpression (DataFieldInfo fieldInfo, string value, bool isReverse, bool starts, bool ends, bool isNot)
			: base (fieldInfo.TableMapping)
		{
			if (value == null)
				throw new ArgumentNullException ("value");
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
				throw new ArgumentNullException ("values");
			_values = values;
			_isReverse = isReverse;
			_starts = starts;
			_ends = ends;
			_isNot = isNot;
			_fieldInfo = fieldInfo;
		}


		internal override string CreateSqlString (CommandFactory factory, bool fullFieldName, out DataParameter[] dataParameters)
		{
			List<DataParameter> list = new List<DataParameter> ();
//			Type objType = _value.GetType ();
//			object obj = null;
//			if (objType == typeof(byte[])) {
//				obj = Encoding.UTF8.GetString ((byte[])_value);
//			}
//			else if (objType == typeof(char[])) {
//				obj = new string ((char[])_value);
//			}
//			else {
//				obj = _value;
//			}
//			if (_value is System.Collections.IEnumerable && obj.GetType () != typeof(string)) {
//				System.Collections.IEnumerable values = (System.Collections.IEnumerable)obj;
//				foreach (object value in values) {
//					string pn = factory.CreateTempParamName ();
//					list.Add (new DataParameter (pn, _fieldInfo.DataField.ToColumn (value), _fieldInfo.DBType));
//				}
//			}
			if (_values != null) {
				foreach (string value in _values) {
					string pn = factory.CreateTempParamName ();
					list.Add (new DataParameter (pn, value, _fieldInfo.DBType));
				}
				if (list.Count == 0) {
					string pn = factory.CreateTempParamName ();
					list.Add (new DataParameter (pn, string.Empty, _fieldInfo.DBType));
				}
			}
			else {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, _value, _fieldInfo.DBType));
			}
			dataParameters = list.ToArray ();
			return factory.CreateCollectionMatchQuerySql (_fieldInfo.CreateDataFieldSql (factory, fullFieldName), _isReverse, _starts, _ends, _isNot, list);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				CollectionMatchQueryExpression target = expression as CollectionMatchQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._isReverse == target._isReverse
				&& this._starts == target._starts
				&& this._ends == target._ends
				&& this._isNot == target._isNot
				&& Utility.EnumableObjectEquals (this._value, target._value);
			}
			else {
				return false;
			}
		}
	}
}
