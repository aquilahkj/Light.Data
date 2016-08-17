﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Lenumerable.
	/// </summary>
	public class LightQuery<T> : IQuery<T> where T : class, new()
	{
		#region IEnumerable implementation

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			foreach (T item in _context.QueryDataMappingEnumerable (typeof (T), null, _query, _order, _region, _level)) {
				yield return item;
			}
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _context.QueryDataMappingEnumerable (typeof (T), null, _query, _order, _region, _level).GetEnumerator ();
		}

		#endregion

		DataEntityMapping _mapping;

		internal DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		QueryExpression _query;

		internal QueryExpression Query {
			get {
				return _query;
			}
		}

		OrderExpression _order;

		internal OrderExpression Order {
			get {
				return _order;
			}
		}

		Region _region;

		internal Region Region {
			get {
				return _region;
			}
		}

		DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		SafeLevel _level = SafeLevel.None;

		internal SafeLevel Level {
			get {
				return _level;
			}
		}

		internal LightQuery (DataContext dataContext)
		{
			_context = dataContext;
			_mapping = DataEntityMapping.GetEntityMapping (typeof (T));
		}

		#region LQuery<T> 成员

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IQuery<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// Where the specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public IQuery<T> Where (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = queryExpression;
			return this;
		}


		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IQuery<T> WhereWithAnd (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.And (_query, queryExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		public IQuery<T> WhereWithOr (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.Or (_query, queryExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		public IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = orderExpression;
			return this;
		}

		/// <summary>
		/// Orders the by.
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		public IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = orderExpression;
			return this;
		}

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IQuery<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IQuery<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="count">Count.</param>
		public IQuery<T> Take (int count)
		{
			int start;
			int size = count;
			if (_region == null) {
				start = 0;
			}
			else {
				start = _region.Start;
			}
			_region = new Region (start, size);
			return this;
		}

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="index">Index.</param>
		public IQuery<T> Skip (int index)
		{
			int start = index;
			int size;
			if (_region == null) {
				size = int.MaxValue;
			}
			else {
				size = _region.Size;
			}
			_region = new Region (start, size);
			return this;
		}

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public IQuery<T> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>LEnumerable.</returns>
		public IQuery<T> RangeReset ()
		{
			_region = null;
			return this;
		}

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		public IQuery<T> PageSize (int page, int size)
		{
			if (page < 1) {
				throw new ArgumentOutOfRangeException (nameof (page));
			}
			if (size < 1) {
				throw new ArgumentOutOfRangeException (nameof (size));
			}
			page--;
			int start = page * size;
			_region = new Region (start, size);
			return this;
		}

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="level">Level.</param>
		public IQuery<T> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		#region aggregate function
		/// <summary>
		/// Gets the datas count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get {
				return Convert.ToInt32 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		private object Aggregate (BasicFieldInfo field, AggregateType aggregateType, bool isDistinct)
		{
			if (!_mapping.Equals (field.TableMapping)) {
				throw new LightDataException (RE.FieldIsNotMatchDataMapping);
			}
			return _context.Aggregate (field.DataField, aggregateType, _query, isDistinct, _level);
		}

		///// <summary>
		///// Get datas count of the field.
		///// </summary>
		///// <returns>count value.</returns>
		///// <param name="field">Field.</param>
		//public int CountField (BasicFieldInfo field)
		//{
		//	return CountField (field, false);
		//}

		///// <summary>
		///// Get datas count of the field.
		///// </summary>
		///// <returns>count value.</returns>
		///// <param name="field">Field.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		//public int CountField (BasicFieldInfo field, bool isDistinct)
		//{
		//	return Convert.ToInt32 (Aggregate (field, AggregateType.COUNT, isDistinct));
		//}

		///// <summary>
		///// Get max value of the field.
		///// </summary>
		///// <returns>max value.</returns>
		///// <param name="field">Field.</param>
		//public object Max (BasicFieldInfo field)
		//{
		//	return Aggregate (field, AggregateType.MAX, false);
		//}

		///// <summary>
		///// Get min value of the field.
		///// </summary>
		///// <returns>min value.</returns>
		///// <param name="field">Field.</param>
		//public object Min (BasicFieldInfo field)
		//{
		//	return Aggregate (field, AggregateType.MIN, false);
		//}

		///// <summary>
		///// Get avg value of the field.
		///// </summary>
		///// <returns>max value.</returns>
		///// <param name="field">Field.</param>
		//public object Avg (BasicFieldInfo field)
		//{
		//	return Avg (field, false);
		//}

		///// <summary>
		///// Get avg value of the field.
		///// </summary>
		///// <returns>avg value.</returns>
		///// <param name="field">Field.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		//public object Avg (BasicFieldInfo field, bool isDistinct)
		//{
		//	return Aggregate (field, AggregateType.AVG, isDistinct);
		//}

		///// <summary>
		///// Get sum value of the field.
		///// </summary>
		///// <returns>sum value.</returns>
		///// <param name="field">Field.</param>
		//public object Sum (BasicFieldInfo field)
		//{
		//	return Sum (field, false);
		//}

		///// <summary>
		///// Get sum value of the field.
		///// </summary>
		///// <returns>sum value.</returns>
		///// <param name="field">Field.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		//public object Sum (BasicFieldInfo field, bool isDistinct)
		//{
		//	return Aggregate (field, AggregateType.SUM, isDistinct);
		//}

		#endregion

		/// <summary>
		/// Get single instance.
		/// </summary>
		/// <returns>instance.</returns>
		public T Single ()
		{
			return _context.SelectSingle<T> (_mapping, _query, _order, 0, _level);
		}

		/// <summary>
		/// Elements at index.
		/// </summary>
		/// <returns>instance.</returns>
		/// <param name="index">Index.</param>
		public T ElementAt (int index)
		{
			return _context.SelectSingle<T> (_mapping, _query, _order, index, _level);
		}

		/// <summary>
		/// Gets the data is exists with query expression.
		/// </summary>
		/// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
		public bool Exists {
			get {
				return _context.Exists (_mapping, _query, _level);
			}
		}



		///// <summary>
		///// Queries the single field.
		///// </summary>
		///// <returns>The single field enumerable.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public IEnumerable QuerySingleField<K> (DataFieldInfo fieldInfo)
		//{
		//	return QuerySingleField<K> (fieldInfo, false);
		//}

		///// <summary>
		///// Queries the single field.
		///// </summary>
		///// <returns>The single field enumerable.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public IEnumerable QuerySingleField<K> (DataFieldInfo fieldInfo, bool isDistinct)
		//{
		//	if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
		//		throw new LightDataException (RE.FieldIsNotMatchDataMapping);
		//	}
		//	return _context.QueryColumeEnumerable (fieldInfo, typeof (K), _query, _order, _region, isDistinct, _level);
		//}

		///// <summary>
		///// Queries the single field list.
		///// </summary>
		///// <returns>The single field list.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public List<K> QuerySingleFieldList<K> (DataFieldInfo fieldInfo)
		//{
		//	return QuerySingleFieldList<K> (fieldInfo, false);
		//}

		///// <summary>
		///// Queries the single field list.
		///// </summary>
		///// <returns>The single field list.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public List<K> QuerySingleFieldList<K> (DataFieldInfo fieldInfo, bool isDistinct)
		//{
		//	if (!_mapping.Equals (fieldInfo.DataField.TypeMapping)) {
		//		throw new LightDataException (RE.FieldIsNotMatchDataMapping);
		//	}
		//	return _context.QueryColumeList<K> (fieldInfo, _query, _order, _region, isDistinct, _level);
		//}

		///// <summary>
		///// Queries the single field array.
		///// </summary>
		///// <returns>The single field array.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public K [] QuerySingleFieldArray<K> (DataFieldInfo fieldInfo)
		//{
		//	return QuerySingleFieldList<K> (fieldInfo, false).ToArray ();
		//}

		///// <summary>
		///// Queries the single field array.
		///// </summary>
		///// <returns>The single field array.</returns>
		///// <param name="fieldInfo">Field info.</param>
		///// <param name="isDistinct">If set to <c>true</c> is distinct.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public K [] QuerySingleFieldArray<K> (DataFieldInfo fieldInfo, bool isDistinct)
		//{
		//	return QuerySingleFieldList<K> (fieldInfo, isDistinct).ToArray ();
		//}

		/// <summary>
		/// To the list.
		/// </summary>
		/// <returns>The list.</returns>
		public List<T> ToList ()
		{
			List<T> list = new List<T> ();
			IEnumerable ie = _context.QueryDataMappingEnumerable (typeof (T), null, _query, _order, _region, _level);
			foreach (T item in ie) {
				list.Add (item);
			}
			return list;
		}

		/// <summary>
		/// To the array.
		/// </summary>
		/// <returns>The array.</returns>
		public T [] ToArray ()
		{
			return ToList ().ToArray ();
		}

		//internal List<T> ToRelateList (object extentState)
		//{
		//	return _context.QueryDataRelateList<T> (_query, _order, _region, _level, extentState);
		//}

		#endregion

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, le, on);
		//}

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, le, on);
		//}

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> (QueryExpression query) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, le, null);
		//}

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> (LEnumerable<K> le) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, le, null);
		//}

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> (DataFieldExpression on) where K : class, new()
		//{
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, null, on);
		//}

		///// <summary>
		///// Inner join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable Join<K> () where K : class, new()
		//{
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.InnerJoin, this, null, null);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, le, on);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, le, on);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> (QueryExpression query) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, le, null);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> (LEnumerable<K> le) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, le, null);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> (DataFieldExpression on) where K : class, new()
		//{
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, null, on);
		//}

		///// <summary>
		///// Left join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable LeftJoin<K> () where K : class, new()
		//{
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.LeftJoin, this, null, null);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> (LEnumerable<K> le, DataFieldExpression on) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, le, on);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <param name="on">On.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> (QueryExpression query, DataFieldExpression on) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, le, on);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="le">Le.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> (LEnumerable<K> le) where K : class, new()
		//{
		//	if (le == null)
		//		throw new ArgumentNullException (nameof (le));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, le, null);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <param name="query">Query.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> (QueryExpression query) where K : class, new()
		//{
		//	if (query == null)
		//		throw new ArgumentNullException (nameof (query));
		//	LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, le, null);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> (DataFieldExpression on) where K : class, new()
		//{
		//	if (on == null)
		//		throw new ArgumentNullException (nameof (on));
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, null, on);
		//}

		///// <summary>
		///// Right join table.
		///// </summary>
		///// <returns>The join.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public JoinTable RightJoin<K> () where K : class, new()
		//{
		//	return JoinTable.CreateJoinTable<T, K> (this._context, JoinType.RightJoin, this, null, null);
		//}

		///// <summary>
		///// Creates the insertor.
		///// </summary>
		///// <returns>The insertor.</returns>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public SelectInsertor CreateInsertor<K> ()
		//{
		//	return new SelectInsertor (this._context, typeof (K), typeof (T), this._query, this._order);
		//}

		///// <summary>
		///// Insert the specified selectInfos.
		///// </summary>
		///// <param name="selectInfos">Select infos.</param>
		///// <typeparam name="K">The 1st type parameter.</typeparam>
		//public int Insert<K> (params SelectFieldInfo [] selectInfos)
		//{
		//	SelectInsertor insertor = new SelectInsertor (this._context, typeof (K), typeof (T), this._query, this._order);
		//	if (selectInfos != null && selectInfos.Length > 0) {
		//		insertor.SetSelectField (selectInfos);
		//	}
		//	return insertor.Execute ();
		//}

		/// <summary>
		/// Insert this instance.
		/// </summary>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public int Insert<K> ()
		{
			SelectInsertor insertor = new SelectInsertor (this._context, typeof (K), typeof (T), this._query, this._order);
			return insertor.Execute ();
		}

		/// <summary>
		/// Update the values on specified query expression..
		/// </summary>
		/// <param name="updates">Updates.</param>
		public int Update (params UpdateSetValue [] updates)
		{
			return _context.UpdateMass<T> (this._query, updates);
		}

		/// <summary>
		/// Delete datas on specified query expression.
		/// </summary>
		public int Delete ()
		{
			return _context.DeleteMass<T> (this._query);
		}

		public ISelect<TResult> Select<TResult> (Expression<Func<T, TResult>> expression) where TResult : class
		{
			ISelector selector = LambdaExpressionExtend.CreateSelector (expression);
			Delegate dele = expression.Compile ();
			LightSelect<TResult> selectable = new LightSelect<TResult> (_context, dele, selector, typeof (T), _query, _order, _region, _level);
			return selectable;
		}

		public IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, null, onExpression);
		}

		public IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, null, onExpression);
		}

		public IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, queryExpression, onExpression);
		}

		public IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class, new()
		{
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, null, onExpression);
		}

		public IAggregate<K> GroupBy<K> (Expression<Func<T, K>> expression) where K : class
		{
			return new LightAggregate<T, K> (this, expression);
		}
	}
}
