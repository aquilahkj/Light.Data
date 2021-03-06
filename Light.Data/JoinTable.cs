﻿using System;
using System.Collections.Generic;

namespace Light.Data
{
	/// <summary>
	/// Join table.
	/// </summary>
	public class JoinTable
	{
		internal static JoinTable CreateJoinTable<T,K> (DataContext dataContext, JoinType joinType, LEnumerable<T> left, LEnumerable<K> right, DataFieldExpression on)
			where T : class, new() 
			where K : class, new()
		{
			JoinTable table = new JoinTable (dataContext);
			JoinConnect connect = new JoinConnect (joinType, on);

			JoinModel model1;
			if (left != null) {
				model1 = new JoinModel (left.Mapping, null, left.Query, left.Order);
			}
			else {
				model1 = new JoinModel (DataMapping.GetEntityMapping (typeof(T)), null, null, null);
			}
			JoinModel model2;
			if (right != null) {
				model2 = new JoinModel (right.Mapping, connect, right.Query, right.Order);
			}
			else {
				model2 = new JoinModel (DataMapping.GetEntityMapping (typeof(K)), connect, null, null);
			}

			if (model1.Mapping.Equals (model2.Mapping)) {
				throw new LightDataException (RE.CanNotJoinTheSameTable);
			}

			table._modelList.Add (model1);
			table._modelList.Add (model2);
			return table;
		}

		internal JoinTable (DataContext dataContext)
		{
			_context = dataContext;
		}

		JoinSelector _selector = new JoinSelector ();

		QueryExpression _query;

		OrderExpression _order;

		Region _region;

		DataContext _context;

		SafeLevel _level = SafeLevel.None;

		List<JoinModel> _modelList = new List<JoinModel> ();

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.InnerJoin, le, on);
		}

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (QueryExpression query, DataFieldExpression on)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.InnerJoin, le, on);
		}

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.InnerJoin, null, on);
		}

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.InnerJoin, le, null);
		}

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> (QueryExpression query)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.InnerJoin, le, null);
		}

		/// <summary>
		/// Inner join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable Join<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.InnerJoin, null, null);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name = "on"></param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.LeftJoin, le, on);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (QueryExpression query, DataFieldExpression on)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.LeftJoin, le, on);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.LeftJoin, null, on);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.LeftJoin, le, null);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> (QueryExpression query)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.LeftJoin, le, null);
		}

		/// <summary>
		/// Left join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable LeftJoin<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.LeftJoin, null, null);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le, DataFieldExpression on)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.RightJoin, le, on);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (QueryExpression query, DataFieldExpression on)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (on == null)
				throw new ArgumentNullException ("on");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.RightJoin, le, on);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="on">On.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (DataFieldExpression on)where K : class, new()
		{
			if (on == null)
				throw new ArgumentNullException ("on");
			return InternalJoin<K> (JoinType.RightJoin, null, on);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="le">Le.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (LEnumerable<K> le)where K : class, new()
		{
			if (le == null)
				throw new ArgumentNullException ("le");
			return InternalJoin<K> (JoinType.RightJoin, le, null);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> (QueryExpression query)where K : class, new()
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			LEnumerable<K> le = this._context.LQuery<K> ().Where (query);
			return InternalJoin<K> (JoinType.RightJoin, le, null);
		}

		/// <summary>
		/// Right join table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable RightJoin<K> ()where K : class, new()
		{
			return InternalJoin<K> (JoinType.RightJoin, null, null);
		}

		private JoinTable InternalJoin<K> (JoinType joinType, LEnumerable<K> right, DataFieldExpression on)where K : class, new()
		{
			JoinConnect connect = new JoinConnect (joinType, on);
			JoinModel model2;
			if (right != null) {
				model2 = new JoinModel (right.Mapping, connect, right.Query, right.Order);
			}
			else {
				model2 = new JoinModel (DataMapping.GetEntityMapping (typeof(K)), connect, null, null);
			}
			foreach (JoinModel model in this._modelList) {
				if (model.Mapping.Equals (model2.Mapping)) {
					throw new LightDataException (RE.CanNotJoinTheSameTable);
				}
			}
			this._modelList.Add (model2);
			return this;
		}

		/// <summary>
		/// Set on expression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable On (DataFieldExpression expression)
		{
			if (expression == null)
				throw new ArgumentNullException ("expression");
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = expression;
			return this;
		}

		/// <summary>
		/// Catch on expression with and.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable OnWithAnd (DataFieldExpression expression)
		{
			if (expression == null)
				throw new ArgumentNullException ("expression");
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = DataFieldExpression.And (connect.On, expression);
			return this;
		}

		/// <summary>
		/// Catch on expression with or.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable OnWithOr (DataFieldExpression expression)
		{
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = DataFieldExpression.Or (connect.On, expression);
			return this;
		}

		/// <summary>
		/// Reset on expression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <returns>The join.</returns>
		public JoinTable OnReset ()
		{
			JoinModel model = this._modelList [this._modelList.Count - 1];
			JoinConnect connect = model.Connect;
			connect.On = null;
			return this;
		}

		/// <summary>
		/// Set the select fields.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="fields">Fields.</param>
		public JoinTable Select (params DataFieldInfo[] fields)
		{
			foreach (DataFieldInfo field in fields) {
				JoinModel m = null;
				foreach (JoinModel model in this._modelList) {
					if (model.Mapping.Equals (field.TableMapping)) {
						m = model;
						break;
					}
				}
				if (m != null) {
					_selector.SetDataField (field);
				}
				else {
					throw new LightDataException (RE.SelectFiledsNotInJoinTables);
				}
			}
			return this;
		}

		/// <summary>
		/// Set the select fields and alias.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="field">Field.</param>
		/// <param name="alias">Alias.</param>
		public JoinTable SelectAlias (DataFieldInfo field, string alias)
		{
			if (Object.Equals (field, null))
				throw new ArgumentNullException ("field");
			if (string.IsNullOrEmpty (alias))
				throw new ArgumentNullException ("alias");
			JoinModel m = null;
			foreach (JoinModel model in this._modelList) {
				if (model.Mapping.Equals (field.TableMapping)) {
					m = model;
					break;
				}
			}
			if (m != null) {
				AliasDataFieldInfo aliasField = new AliasDataFieldInfo (field, alias);
				_selector.SetAliasDataField (aliasField);
			}
			else {
				throw new LightDataException (RE.SelectFiledsNotInJoinTables);
			}

			return this;
		}

		/// <summary>
		/// Selects all fields in specified table.
		/// </summary>
		/// <returns>The join.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public JoinTable SelectAll<K> ()
		{
			DataEntityMapping mapping = DataMapping.GetEntityMapping (typeof(K));
			JoinModel m = null;
			foreach (JoinModel model in _modelList) {
				if (model.Mapping.Equals (mapping)) {
					m = model;
					break;
				}
			}
			if (m != null) {
				_selector.SetDataEntity (mapping);
			}
			else {
				throw new LightDataException (RE.SelectFiledsNotInJoinTables);
			}
			return this;
		}

		/// <summary>
		/// Reset the specified where expression
		/// </summary>
		/// <returns>The join.</returns>
		public JoinTable WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// Set the specified where expression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable Where (QueryExpression expression)
		{
			_query = expression;
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable WhereWithAnd (QueryExpression expression)
		{
			_query = QueryExpression.And (_query, expression);
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable WhereWithOr (QueryExpression expression)
		{
			_query = QueryExpression.Or (_query, expression);
			return this;
		}

		/// <summary>
		/// Set the specified order by expression.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns>The join.</returns>
		public JoinTable OrderBy (OrderExpression expression)
		{
			_order = expression;
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="expression">Expression.</param>
		public JoinTable OrderByCatch (OrderExpression expression)
		{
			_order = OrderExpression.Catch (_order, expression);
			return this;
		}

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>The join.</returns>
		public JoinTable OrderByRandom ()
		{
			_order = new RandomOrderExpression (this._modelList [0].Mapping);
			return this;
		}

		/// <summary>
		/// Reset the specified order by expression.
		/// </summary>
		/// <returns>The join.</returns>
		public JoinTable OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// Take the datas count.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="count">Count.</param>
		public JoinTable Take (int count)
		{
			if (_region == null) {
				_region = new Region (0, count);
			}
			else {
				_region.Size = count;
			}
			return this;
		}

		/// <summary>
		/// Skip the specified index.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="index">Index.</param>
		public JoinTable Skip (int index)
		{
			if (_region == null) {
				_region = new Region (index, int.MaxValue);
			}
			else {
				_region.Start = index;
			}
			return this;
		}

		/// <summary>
		/// Range the specified from and to.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public JoinTable Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			if (_region == null) {
				_region = new Region (start, size);
			}
			else {
				_region.Start = start;
				_region.Size = size;
			}
			return this;
		}

		/// <summary>
		/// reset the range
		/// </summary>
		/// <returns>The join.</returns>
		public JoinTable RangeReset ()
		{
			_region = null;
			return this;
		}

		/// <summary>
		/// Sets page size.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="page">Page.</param>
		/// <param name="size">Size.</param>
		public JoinTable PageSize (int page, int size)
		{
			int start = page * size;
			if (_region == null) {
				_region = new Region (start, size);
			}
			else {
				_region.Start = start;
				_region.Size = size;
			}
			return this;
		}

		/// <summary>
		/// Safes the mode.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="level">Level.</param>
		public JoinTable SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		/// <summary>
		/// Gets the datas count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get {
				return Convert.ToInt32 (_context.AggregateJoinTableCount (_modelList, _query, _level));
			}
		}

		/// <summary>
		/// Gets the datas count.
		/// </summary>
		/// <value>The long count.</value>
		public long LongCount {
			get {
				return Convert.ToInt64 (_context.AggregateJoinTableCount (_modelList, _query, _level));
			}
		}

		/// <summary>
		/// Tos the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public List<K> ToList<K> () where K : class, new()
		{
			DataMapping mapping = DataMapping.GetMapping (typeof(K));
			List<K> list = _context.QueryJoinDataList<K> (mapping, _selector, _modelList, _query, _order, _region, _level);
			return list;
		}

		/// <summary>
		/// Tos the array.
		/// </summary>
		/// <returns>The array.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public K[] ToArray<K> () where K : class, new()
		{
			return ToList<K> ().ToArray ();
		}
	}
}

