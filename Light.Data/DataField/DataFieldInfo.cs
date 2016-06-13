using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Data field info.
	/// </summary>
	public class DataFieldInfo<T> : DataFieldInfo where T : class, new()
	{
		/// <summary>
		/// Create the specified name.
		/// </summary>
		/// <param name="name">Name.</param>
		public static DataFieldInfo<T> Create (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}
			return new DataFieldInfo<T> (name);
		}

		private DataFieldInfo (string name)
			: base (typeof(T), name)
		{
			
		}
	}

	/// <summary>
	/// Data field info.
	/// </summary>
	public class DataFieldInfo : BasicFieldInfo,ICloneable
	{
		#region ICloneable implementation

		public object Clone ()
		{
			return this.MemberwiseClone ();
		}

		#endregion

		internal DataFieldInfo (Type type, string name) :
			this (DataMapping.GetEntityMapping (type), name)
		{

		}

		internal DataFieldInfo (DataEntityMapping mapping, string name)
		{
			TableMapping = mapping;
			DataField = TableMapping.FindDataEntityField (name);
			if (DataField == null) {
				DataField = new CustomFieldMapping (name, mapping);
			}
		}

		internal DataFieldInfo (DataFieldMapping fieldMapping)
		{
			TableMapping = fieldMapping.EntityMapping;
			DataField = fieldMapping;
		}

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>The position.</value>
		public int Position {
			get {
				if (DataField != null) {
					return DataField.PositionOrder;
				}
				else {
					return -1;
				}
			}
		}

		/// <summary>
		/// Equal the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression Eq (object value)
		{
			if (Object.Equals (value, null)) {
				return IsNull ();
			}
			else if (value is System.Collections.IEnumerable && !(value is string)) {
				return In ((System.Collections.IEnumerable)value);
			}
			else if (value is Boolean) {
				bool b = (bool)value;
				return b ? IsTrue () : IsFalse ();
			}
			else {
				return SingleParam (QueryPredicate.Eq, value);
			}
		}

		/// <summary>
		/// Less than or equal the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression LtEq (object value)
		{
			return SingleParam (QueryPredicate.LtEq, value);
		}

		/// <summary>
		/// Less than the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression Lt (object value)
		{
			return SingleParam (QueryPredicate.Lt, value);
		}

		/// <summary>
		/// Greater than the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression Gt (object value)
		{
			return SingleParam (QueryPredicate.Gt, value);
		}

		/// <summary>
		/// Greater than or equal the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression GtEq (object value)
		{
			return SingleParam (QueryPredicate.GtEq, value);
		}

		/// <summary>
		/// Not equal the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression NotEq (object value)
		{
			if (Object.Equals (value, null)) {
				return IsNotNull ();
			}
			else if (value is System.Collections.IEnumerable && !(value is string)) {
				return NotIn ((System.Collections.IEnumerable)value);
			}
			else if (value is Boolean) {
				bool b = (bool)value;
				return !b ? IsTrue () : IsFalse ();
			}
			else {
				return SingleParam (QueryPredicate.NotEq, value);
			}
		}

		/// <summary>
		/// In the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression In (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.In, values);
		}

		/// <summary>
		/// Not in the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotIn (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, values);
		}

		/// <summary>
		/// In the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression In (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.In, field, expression);
		}

		/// <summary>
		/// In the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression In (DataFieldInfo field)
		{
			return In (field, null);
		}

		/// <summary>
		/// Not in the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression NotIn (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.NotIn, field, expression);
		}


		/// <summary>
		/// Not in the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression NotIn (DataFieldInfo field)
		{
			return NotIn (field, null);
		}

		/// <summary>
		/// Greater than all the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression GtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAll, field, expression);
		}

		/// <summary>
		/// Greater than all the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression GtAll (DataFieldInfo field)
		{
			return GtAll (field, null);
		}

		/// <summary>
		/// Greater than all the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression GtAll (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.GtAll, values);
		}

		/// <summary>
		/// Less than all the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression LtAll (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAll, field, expression);
		}

		/// <summary>
		/// Less than all the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression LtAll (DataFieldInfo field)
		{
			return LtAll (field, null);
		}

		/// <summary>
		/// Less than all the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression LtAll (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.LtAll, values);
		}

		/// <summary>
		/// Greater than any the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression GtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.GtAny, field, expression);
		}

		/// <summary>
		/// Greater than any the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression GtAny (DataFieldInfo field)
		{
			return GtAny (field, null);
		}

		/// <summary>
		/// Greater than any the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression GtAny (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.GtAny, values);
		}

		/// <summary>
		/// Less than any the specified field and expression.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		/// <param name="expression">Expression.</param>
		public QueryExpression LtAny (DataFieldInfo field, QueryExpression expression)
		{
			return CollectionParams (QueryCollectionPredicate.LtAny, field, expression);
		}

		/// <summary>
		/// Less than any the specified field.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="field">Field.</param>
		public QueryExpression LtAny (DataFieldInfo field)
		{
			return LtAny (field, null);
		}

		/// <summary>
		/// Less than any the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression LtAny (System.Collections.IEnumerable values)
		{
			return CollectionParams (QueryCollectionPredicate.LtAny, values);
		}

		/// <summary>
		/// Between the specified fromValue and toValue.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">To value.</param>
		public QueryExpression Between (object fromValue, object toValue)
		{
			return BetweenParams (true, fromValue, toValue);
		}

		/// <summary>
		/// Nots the between fromValue and toValue.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">To value.</param>
		public QueryExpression NotBetween (object fromValue, object toValue)
		{
			return BetweenParams (false, fromValue, toValue);
		}

		/// <summary>
		/// Like the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression Like (string value)
		{
			return MatchValue (value, false, false, false);
		}

		/// <summary>
		/// Like the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression Like (params string[] values)
		{
			return MatchValue (values, false, false, false);
		}

		/// <summary>
		/// Like the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression Like (IEnumerable<string> values)
		{
			return MatchValue (values, false, false, false);
		}

		/// <summary>
		/// Not like the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression NotLike (string value)
		{
			return MatchValue (value, false, false, true);
		}

		/// <summary>
		/// Not like the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotLike (params string[] values)
		{
			return MatchValue (values, false, false, true);
		}

		/// <summary>
		/// Not like the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotLike (IEnumerable<string> values)
		{
			return MatchValue (values, false, false, true);
		}

		/// <summary>
		/// Contains the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression Contains (string value)
		{
			return MatchValue (value, true, true, false);
		}

		/// <summary>
		/// Contains the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression Contains (params string[] values)
		{
			return MatchValue (values, true, true, false);
		}

		/// <summary>
		/// Contains the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression Contains (IEnumerable<string> values)
		{
			return MatchValue (values, true, true, false);
		}

		/// <summary>
		/// Not contains the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression NotContains (string value)
		{
			return MatchValue (value, true, true, true);
		}

		/// <summary>
		/// Not contains the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotContains (params string[] values)
		{
			return MatchValue (values, true, true, true);
		}

		/// <summary>
		/// Not contains the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotContains (IEnumerable<string> values)
		{
			return MatchValue (values, true, true, true);
		}

		/// <summary>
		/// End with the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression EndsWith (string value)
		{
			return MatchValue (value, true, false, false);
		}

		/// <summary>
		/// End with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression EndsWith (params string[] values)
		{
			return MatchValue (values, true, false, false);
		}

		/// <summary>
		/// End with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression EndsWith (IEnumerable<string> values)
		{
			return MatchValue (values, true, false, false);
		}

		/// <summary>
		/// Not end with the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Values.</param>
		public QueryExpression NotEndsWith (string value)
		{
			return MatchValue (value, true, false, true);
		}

		/// <summary>
		/// Not end with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotEndsWith (params string[] values)
		{
			return MatchValue (values, true, false, true);
		}

		/// <summary>
		/// Not end with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotEndsWith (IEnumerable<string> values)
		{
			return MatchValue (values, true, false, true);
		}

		/// <summary>
		/// Start with the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Value.</param>
		public QueryExpression StartsWith (string value)
		{
			return MatchValue (value, false, true, false);
		}

		/// <summary>
		/// Start with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression StartsWith (params string[] values)
		{
			return MatchValue (values, false, true, false);
		}

		/// <summary>
		/// Start with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression StartsWith (IEnumerable<string> values)
		{
			return MatchValue (values, false, true, false);
		}

		/// <summary>
		/// Not start with the specified value.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="value">Values.</param>
		public QueryExpression NotStartsWith (string value)
		{
			return MatchValue (value, false, true, true);
		}

		/// <summary>
		/// Not start with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotStartsWith (params string[] values)
		{
			return MatchValue (values, false, true, true);
		}

		/// <summary>
		/// Not start with the specified values.
		/// </summary>
		/// <returns>The expression</returns>
		/// <param name="values">Values.</param>
		public QueryExpression NotStartsWith (IEnumerable<string> values)
		{
			return MatchValue (values, false, true, true);
		}

		/// <summary>
		/// Determines whether this field is null.
		/// </summary>
		/// <returns>The expression</returns>
		public QueryExpression IsNull ()
		{
			return Null (true);
		}

		/// <summary>
		/// Determines whether this field is not null.
		/// </summary>
		/// <returns>The expression</returns>
		public QueryExpression IsNotNull ()
		{
			return Null (false);
		}

		/// <summary>
		/// Determines whether this field is true.
		/// </summary>
		/// <returns>The expression</returns>
		public QueryExpression IsTrue ()
		{
			return Boolean (true);
		}

		/// <summary>
		/// Determines whether this field is false.
		/// </summary>
		/// <returns>The expression</returns>
		public QueryExpression IsFalse ()
		{
			return Boolean (false);
		}

		private QueryExpression SingleParam (QueryPredicate predicate, object value)
		{
			return SingleParam (predicate, value, false);
		}

		private QueryExpression SingleParam (QueryPredicate predicate, object value, bool isReverse)
		{
			if (Object.Equals (value, null)) {
				throw new ArgumentNullException ("value");
			}
			QueryExpression exp;
			DataFieldInfo dataFieldInfo = value as DataFieldInfo;
			if (!Object.Equals (dataFieldInfo, null)) {
				exp = new DataFieldQueryExpression (this, predicate, dataFieldInfo, isReverse);
			}
			else {
				exp = new SingleParamQueryExpression (this, predicate, value, isReverse);
			}
			return exp;
		}

		private QueryExpression CollectionParams (QueryCollectionPredicate predicate, System.Collections.IEnumerable values)
		{
			if (Object.Equals (values, null)) {
				throw new ArgumentNullException ("values");
			}
			QueryExpression exp = new CollectionParamsQueryExpression (this, predicate, values);
			return exp;
		}

		private QueryExpression CollectionParams (QueryCollectionPredicate predicate, DataFieldInfo field, QueryExpression expression)
		{
			if (Object.Equals (field, null)) {
				throw new ArgumentNullException ("field");
			}
			QueryExpression exp = new SubQueryExpression (this, predicate, field, expression);
			return exp;
		}

		private QueryExpression BetweenParams (bool isNot, object fromValue, object toValue)
		{
			if (Object.Equals (fromValue, null)) {
				throw new ArgumentNullException ("fromValue");
			}
			if (Object.Equals (toValue, null)) {
				throw new ArgumentNullException ("toValue");
			}
			QueryExpression exp = new BetweenParamsQueryExpression (this, isNot, fromValue, toValue);
			return exp;
		}

		private QueryExpression MatchValue (string value, bool starts, bool ends, bool isNot)
		{
			if (value == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, value, false, starts, ends, isNot);
			return exp;
		}

		private QueryExpression MatchValue (IEnumerable<string> values, bool starts, bool ends, bool isNot)
		{
			if (values == null) {
				throw new LightDataException (RE.InputValueIsNotAllowNull);
			}
			QueryExpression exp = new CollectionMatchQueryExpression (this, values, false, starts, ends, isNot);
			return exp;
		}

		private QueryExpression Null (bool isNull)
		{
			QueryExpression exp = new NullQueryExpression (this, isNull);
			return exp;
		}

		private QueryExpression Boolean (bool isTrue)
		{
			QueryExpression exp = new BooleanQueryExpression (this, isTrue);
			return exp;
		}

		/// <summary>
		/// Order by asc.
		/// </summary>
		/// <returns>The expression</returns>
		public OrderExpression OrderByAsc ()
		{
			return OrderBy (OrderType.ASC);
		}

		/// <summary>
		/// Order by desc.
		/// </summary>
		/// <returns>The expression</returns>
		public OrderExpression OrderByDesc ()
		{
			return OrderBy (OrderType.DESC);
		}

		private OrderExpression OrderBy (OrderType type)
		{
			OrderExpression exp = new FieldOrderExpression (this, type);
			return exp;
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator == (DataFieldInfo field, object value)
		{
			return field.Eq (value);
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator != (DataFieldInfo field, object value)
		{
			return field.NotEq (value);
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator > (DataFieldInfo field, object value)
		{
			return field.Gt (value);
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator >= (DataFieldInfo field, object value)
		{
			return field.GtEq (value);
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator < (DataFieldInfo field, object value)
		{
			return field.Lt (value);
		}

		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static QueryExpression operator <= (DataFieldInfo field, object value)
		{
			return field.LtEq (value);
		}

		/// <summary>
		/// Gets the DBtype of the field.
		/// </summary>
		/// <value>The type of the DB.</value>
		internal virtual string DBType {
			get {
				return DataField.DBType;
			}
		}

		/// <summary>
		/// Creates the data field sql.
		/// </summary>
		/// <returns>The data field sql.</returns>
		/// <param name="factory">Factory.</param>
		internal virtual string CreateDataFieldSql (CommandFactory factory)
		{
			return CreateDataFieldSql (factory, false);
		}

		string _aliasTableName;

		internal virtual string AliasTableName {
			get {
				return _aliasTableName;
			}
			set {
				_aliasTableName = value;
			}
		}

		/// <summary>
		/// Creates the data field sql.
		/// </summary>
		/// <returns>The data field sql.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> is full name.</param>
		internal virtual string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			if (isFullName) {
				string tableName = this._aliasTableName ?? TableMapping.TableName;
				return factory.CreateFullDataFieldSql (tableName, FieldName);
			}
			else {
				return factory.CreateDataFieldSql (FieldName);
			}
		}

//		/// <summary>
//		/// Creates the data field sql.
//		/// </summary>
//		/// <returns>The data field sql.</returns>
//		/// <param name="factory">Factory.</param>
//		/// <param name="aliasTableName">Alias table name.</param>
//		internal virtual string CreateDataFieldSql (CommandFactory factory, string aliasTableName)
//		{
//			return factory.CreateFullDataFieldSql (aliasTableName, FieldName);
//		}


		/// <summary>
		/// Transforms the starts with match.
		/// </summary>
		/// <returns>The match field info.</returns>
		public MatchDataFieldInfo TransformEndsWithMatch ()
		{
			return new MatchDataFieldInfo (this, true, false);
		}

		/// <summary>
		/// Transforms the endss with match.
		/// </summary>
		/// <returns>The match field info.</returns>
		public MatchDataFieldInfo TransformStartsWithMatch ()
		{
			return new MatchDataFieldInfo (this, false, true);
		}

		/// <summary>
		/// Transforms the contains match.
		/// </summary>
		/// <returns>The match field info.</returns>
		public MatchDataFieldInfo TransformContainsMatch ()
		{
			return new MatchDataFieldInfo (this, true, true);
		}

		/// <summary>
		/// Transforms the date.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformDate ()
		{
			return new DateDataFieldInfo (this, null);
		}

		/// <summary>
		/// Transforms the date string, default format "Y-M-D"
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformDateString ()
		{
			return new DateDataFieldInfo (this, "Y-M-D");
		}

		/// <summary>
		/// Transforms the date string, support format YMD|YM|Y-M-D|Y-M|D-M-Y|M-D-Y|Y/M/D|Y/M|D/M/Y|M/D/Y
		/// </summary>
		/// <returns>The field info.</returns>
		/// <param name="format">Format.</param>
		public DataFieldInfo TransformDateString (string format)
		{
			if (string.IsNullOrEmpty (format)) {
				throw new ArgumentNullException (format);
			}
			return new DateDataFieldInfo (this, format);
		}

		/// <summary>
		/// Transforms the year.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformYear ()
		{
			//return new YearDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Year);
		}

		/// <summary>
		/// Transforms the month.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformMonth ()
		{
			//return new MonthDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Month);
		}

		/// <summary>
		/// Transforms the day.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformDay ()
		{
			//return new DayDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Day);
		}

		/// <summary>
		/// Transforms the hour.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformHour ()
		{
			//return new HourDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Hour);
		}

		/// <summary>
		/// Transforms the minute.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformMinute ()
		{
			//return new MinuteDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Minute);
		}

		/// <summary>
		/// Transforms the second.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformSecond ()
		{
			//return new SecondDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Second);
		}

		/// <summary>
		/// Transforms the week.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformWeek ()
		{
			//return new WeekDataFieldInfo(this);
			return new DatePartDataFieldInfo (this, DatePart.Week);
		}

		/// <summary>
		/// Transforms the weekday.
		/// </summary>
		/// <returns>The week day.</returns>
		public DataFieldInfo TransformWeekDay ()
		{
			return new DatePartDataFieldInfo (this, DatePart.WeekDay);
		}

		/// <summary>
		/// Transforms the string length.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformLength ()
		{
			return new LengthDataFieldInfo (this);
		}

		/// <summary>
		/// Transforms the substring.
		/// </summary>
		/// <returns>The field info.</returns>
		/// <param name="start">Start.</param>
		/// <param name="size">Size.</param>
		public DataFieldInfo TransformSubString (int start, int size)
		{
			if (size <= 0) {
				throw new ArgumentOutOfRangeException ("size");
			}
			return new SubStringDataFieldInfo (this, start, size);
		}

		/// <summary>
		/// Transforms the substring.
		/// </summary>
		/// <returns>The field info.</returns>
		/// <param name="start">Start.</param>
		public DataFieldInfo TransformSubString (int start)
		{
			return new SubStringDataFieldInfo (this, start, 0);
		}

		#region math operate int
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, int value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (int value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate long
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, long value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (long value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate short
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, short value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (short value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate uint
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, uint value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (uint value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate ulong
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, ulong value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (ulong value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate ushort
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, ushort value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (ushort value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate float
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, float value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (float value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate double
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, double value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (double value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		#region math operate decimal
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator + (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator + (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Puls, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator - (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator - (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Minus, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator * (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator * (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Multiply, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator / (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator / (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Divided, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator % (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator % (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Mod, value, false);
		}
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		public static DataFieldInfo operator ^ (DataFieldInfo field, decimal value)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, true);
		}
		/// <param name="value">Value.</param>
		/// <param name="field">Field.</param>
		public static DataFieldInfo operator ^ (decimal value, DataFieldInfo field)
		{
			return field.TransformMathCalculate (MathOperator.Power, value, false);
		}

		#endregion

		private DataFieldInfo TransformMathCalculate (MathOperator opera, object value, bool forward)
		{
			return new MathCalculateDataFieldInfo (this, opera, value, forward);
		}

		/// <summary>
		/// Transforms the abs.
		/// </summary>
		/// <returns>The field info.</returns>
		public DataFieldInfo TransformAbs ()
		{
			return TransformMathFunction (MathFunction.Abs);
		}

		/// <summary>
		/// Transforms the log.
		/// </summary>
		/// <returns>The log.</returns>
		public DataFieldInfo TransformLog ()
		{
			return TransformMathFunction (MathFunction.Log);
		}

		/// <summary>
		/// Transforms the exp.
		/// </summary>
		/// <returns>The exp.</returns>
		public DataFieldInfo TransformExp ()
		{
			return TransformMathFunction (MathFunction.Exp);
		}

		/// <summary>
		/// Transforms the sin.
		/// </summary>
		/// <returns>The sin.</returns>
		public DataFieldInfo TransformSin ()
		{
			return TransformMathFunction (MathFunction.Sin);
		}

		/// <summary>
		/// Transforms the cos.
		/// </summary>
		/// <returns>The cos.</returns>
		public DataFieldInfo TransformCos ()
		{
			return TransformMathFunction (MathFunction.Cos);
		}

		/// <summary>
		/// Transforms the tan.
		/// </summary>
		/// <returns>The tan.</returns>
		public DataFieldInfo TransformTan ()
		{
			return TransformMathFunction (MathFunction.Tan);
		}

		/// <summary>
		/// Transforms the atan.
		/// </summary>
		/// <returns>The atan.</returns>
		public DataFieldInfo TransformAtan ()
		{
			return TransformMathFunction (MathFunction.Atan);
		}

		private DataFieldInfo TransformMathFunction (MathFunction function)
		{
			return new MathFunctionDataFieldInfo (this, function);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Light.Data.DataFieldInfo"/> is equal to the current <see cref="Light.Data.DataFieldInfo"/>.
		/// </summary>
		/// <param name="target">The <see cref="Light.Data.DataFieldInfo"/> to compare with the current <see cref="Light.Data.DataFieldInfo"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Light.Data.DataFieldInfo"/> is equal to the current
		/// <see cref="Light.Data.DataFieldInfo"/>; otherwise, <c>false</c>.</returns>
		public virtual bool Equals (DataFieldInfo target)
		{
			if (Object.Equals (target, null)) {
				return false;
			}
			if (Object.ReferenceEquals (this, target)) {
				return true;
			}
			else {
				if (this.GetType () == target.GetType ()) {
					return EqualsDetail (target);
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// Equalses the detail.
		/// </summary>
		/// <returns><c>true</c>, if detail was equalsed, <c>false</c> otherwise.</returns>
		/// <param name="info">Info.</param>
		protected virtual bool EqualsDetail (DataFieldInfo info)
		{
			bool result = Object.Equals (this.DataField, info.DataField);
			if (!result) {
				if (this.DataField is CustomFieldMapping && info.DataField is CustomFieldMapping) {
					return this.DataField.Name == info.DataField.Name && Object.Equals (this.DataField.EntityMapping, info.DataField.EntityMapping);
				}
			}
			return result;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Light.Data.DataFieldInfo"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Light.Data.DataFieldInfo"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="Light.Data.DataFieldInfo"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj)
		{
			return object.ReferenceEquals (this, obj);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Light.Data.DataFieldInfo"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

		/// <summary>
		/// Equal the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression Eq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Eq, field);
		}

		/// <summary>
		/// Less than or equal the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression LtEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.LtEq, field);
		}

		/// <summary>
		/// Less than the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression Lt (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Lt, field);
		}

		/// <summary>
		/// Greater than the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression Gt (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.Gt, field);
		}

		/// <summary>
		/// Greater than or equal the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression GtEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.GtEq, field);
		}

		/// <summary>
		/// Not equal the specified field.
		/// </summary>
		/// <returns>The expression.</returns>
		/// <param name="field">Field.</param>
		public DataFieldExpression NotEq (DataFieldInfo field)
		{
			return OnDataFieldMatch (QueryPredicate.NotEq, field);
		}

		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator == (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Eq (mfield);
		}
		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator != (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.NotEq (mfield);
		}
		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator > (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Gt (mfield);
		}
		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator >= (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.GtEq (mfield);
		}
		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator < (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.Lt (mfield);
		}
		/// <param name="field">Field.</param>
		/// <param name="mfield">Mfield.</param>
		public static DataFieldExpression operator <= (DataFieldInfo field, DataFieldInfo mfield)
		{
			return field.LtEq (mfield);
		}
		/// <summary>
		/// Raises the data field match event.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		/// <param name="field">Field.</param>
		private DataFieldExpression OnDataFieldMatch (QueryPredicate predicate, DataFieldInfo field)
		{
			if (Object.Equals (field, null) && predicate != QueryPredicate.Eq && predicate != QueryPredicate.NotEq) {
				throw new ArgumentNullException ("field");
			}
			DataFieldMatchExpression exp = new DataFieldMatchExpression (this, field, predicate);
			return exp;
		}

		/// <summary>
		/// Tos the parameter.
		/// </summary>
		/// <returns>The parameter.</returns>
		/// <param name="value">Value.</param>
		internal virtual object ToParameter (object value)
		{
			return base.DataField.ToParameter (value);
		}
	}
}
