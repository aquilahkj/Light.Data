using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data.Expressions
{
	class CollectionMatchQueryExpression : QueryExpression
	{
		DataFieldInfo _fieldInfo = null;

		object _value = null;

		bool _isReverse = false;

		bool _isMatch = false;

		bool _isNot = false;

		public CollectionMatchQueryExpression (DataFieldInfo fieldInfo, object value, bool isReverse, bool isMatch, bool isNot)
			: base (fieldInfo.TableMapping)
		{
			_value = value;
			_isReverse = isReverse;
			_isMatch = isMatch;
			_isNot = isNot;
			_fieldInfo = fieldInfo;
		}


		internal override string CreateSqlString (CommandFactory factory, out DataParameter[] dataParameters)
		{
			List<DataParameter> list = new List<DataParameter> ();
			Type objType = _value.GetType ();
			object obj = null;
			if (objType == typeof(byte[])) {
				obj = Encoding.UTF8.GetString ((byte[])_value);
			}
			else if (objType == typeof(char[])) {
				obj = new string ((char[])_value);
			}
			else {
				obj = _value;
			}
			if (_value is System.Collections.IEnumerable && obj.GetType () != typeof(string)) {
				System.Collections.IEnumerable values = (System.Collections.IEnumerable)obj;
				foreach (object value in values) {
					string pn = factory.CreateTempParamName ();
					list.Add (new DataParameter (pn, _fieldInfo.DataField.ToColumn (value), _fieldInfo.DBType));
				}
			}
			else {
				string pn = factory.CreateTempParamName ();
				list.Add (new DataParameter (pn, _fieldInfo.DataField.ToColumn (obj), _fieldInfo.DBType));
			}
			dataParameters = list.ToArray ();
			return factory.CreateCollectionMatchQuerySql (_fieldInfo.CreateDataFieldSql (factory), _isReverse, _isMatch, _isNot, list);
		}

		protected override bool EqualsDetail (QueryExpression expression)
		{
			if (base.EqualsDetail (expression)) {
				CollectionMatchQueryExpression target = expression as CollectionMatchQueryExpression;
				return this._fieldInfo.Equals (target._fieldInfo)
				&& this._isReverse == target._isReverse
				&& this._isMatch == target._isMatch
				&& this._isNot == target._isNot
				&& Utility.EnumableObjectEquals (this._value, target._value);
			}
			else {
				return false;
			}
		}
	}
}
