using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightQuery<T> : QueryBase<T> where T : class
	{
		#region IEnumerable implementation

		public override IEnumerator<T> GetEnumerator ()
		{
			foreach (T item in _context.QueryEntityData (_mapping, null, _query, _order, _distinct, _region, _level)) {
				yield return item;
			}
		}

		#endregion

		QueryExpression _query;

		public override QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		OrderExpression _order;

		public override OrderExpression OrderExpression {
			get {
				return _order;
			}
		}

		Region _region;

		public override Region Region {
			get {
				return _region;
			}
		}

		bool _distinct;

		public override bool Distinct {
			get {
				return _distinct;
			}
		}

		SafeLevel _level = SafeLevel.None;

		public override SafeLevel Level {
			get {
				return _level;
			}
		}

		internal LightQuery (DataContext dataContext)
			: base (dataContext)
		{

		}

		#region LQuery<T> 成员

		public override IQuery<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		public override IQuery<T> Where (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = queryExpression;
			return this;
		}

		public override IQuery<T> WhereWithAnd (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.And (_query, queryExpression);
			return this;
		}

		public override IQuery<T> WhereWithOr (Expression<Func<T, bool>> expression)
		{
			var queryExpression = LambdaExpressionExtend.ResolveLambdaQueryExpression (expression);
			_query = QueryExpression.Or (_query, queryExpression);
			return this;
		}

		public override IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public override IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = OrderExpression.Catch (_order, orderExpression);
			return this;
		}

		public override IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.ASC);
			_order = orderExpression;
			return this;
		}

		public override IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression)
		{
			var orderExpression = LambdaExpressionExtend.ResolveLambdaOrderByExpression (expression, OrderType.DESC);
			_order = orderExpression;
			return this;
		}

		public override IQuery<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		public override IQuery<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		public override IQuery<T> Take (int count)
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

		public override IQuery<T> Skip (int index)
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

		public override IQuery<T> Range (int from, int to)
		{
			int start = from;
			int size = to - from;
			_region = new Region (start, size);
			return this;
		}

		public override IQuery<T> RangeReset ()
		{
			_region = null;
			return this;
		}

		public override IQuery<T> PageSize (int page, int size)
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

		public override IQuery<T> SafeMode (SafeLevel level)
		{
			_level = level;
			return this;
		}

		public override IQuery<T> SetDistinct (bool distinct)
		{
			_distinct = distinct;
			return this;
		}

		#region aggregate function

		public override int Count {
			get {
				return Convert.ToInt32 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		public override long LongCount {
			get {
				return Convert.ToInt64 (_context.AggregateCount (_mapping, _query, _level));
			}
		}

		public override IAggregateFunction<T> AggregateFunction ()
		{
			LightAggregateFunction<T> aggregate = new LightAggregateFunction<T> (_context, _query, _distinct, _level);
			return aggregate;
		}

		#endregion

		public override T First ()
		{
			return ElementAt (0);
		}

		public override T ElementAt (int index)
		{
			T target = default (T);
			Region region = new Region (index, 1);
			IEnumerable ie = _context.QueryEntityData (_mapping, null, _query, _order, false, region, _level);
			if (ie != null) {
				foreach (T item in ie) {
					target = item;
					break;
				}
			}
			return target;
		}

		public override bool Exists {
			get {
				return _context.Exists (_mapping, _query, _level);
			}
		}

		public override List<T> ToList ()
		{
			List<T> list = new List<T> ();
			IEnumerable ie = _context.QueryEntityData (_mapping, null, _query, _order, _distinct, _region, _level);
			if (ie != null) {
				foreach (T item in ie) {
					list.Add (item);
				}
			}
			return list;
		}

		public override T [] ToArray ()
		{
			return ToList ().ToArray ();
		}


		#endregion

		public override int Insert<K> ()
		{
			DataTableEntityMapping insertMapping = DataEntityMapping.GetTableMapping (typeof (K));
			return this._context.SelectInsert (insertMapping, _mapping, _query, _order, _level);
		}

		public override int SelectInsert<K> (Expression<Func<T, K>> expression)
		{
			InsertSelector selector = LambdaExpressionExtend.CreateInsertSelector (expression);
			return this._context.SelectInsert (selector, _mapping, _query, _order, _distinct, _level);
		}

		public override int Update (Expression<Func<T, T>> expression)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
			MassUpdator updator = LambdaExpressionExtend.CreateMassUpdator (expression);
			return this._context.Update (mapping, updator, _query, _level);
		}

		public override int Delete ()
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
			return _context.Delete (mapping, this._query, _level);
		}

		public override ISelect<K> Select<K> (Expression<Func<T, K>> expression)
		{
			LightSelect<T, K> selectable = new LightSelect<T, K> (_context, expression, _query, _order, _distinct, _region, _level);
			return selectable;
		}

		public override IAggregate<K> GroupBy<K> (Expression<Func<T, K>> expression)
		{
			return new LightAggregate<T, K> (this, expression);
		}

		public override IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> Join<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, queryBase, onExpression);
		}

		public override IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, queryBase, onExpression);
		}

		public override IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<T, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, queryBase, onExpression);
		}

		public override IJoinTable<T, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<T, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<T, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<T, T1> Join<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression)
		{
			SelectBase<T1> selectBase = select as SelectBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<T, T1> (this, JoinType.InnerJoin, selectBase, onExpression);
		}

		public override IJoinTable<T, T1> LeftJoin<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression)
		{
			SelectBase<T1> selectBase = select as SelectBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<T, T1> (this, JoinType.LeftJoin, selectBase, onExpression);
		}

		public override IJoinTable<T, T1> RightJoin<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression)
		{
			SelectBase<T1> selectBase = select as SelectBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<T, T1> (this, JoinType.RightJoin, selectBase, onExpression);
		}

		public override ISelectField<K> SelectField<K> (Expression<Func<T, K>> expression)
		{
			LightSelectField<K> selectField = new LightSelectField<K> (this.Context, expression, _query, _order, _distinct, _region, _level);
			return selectField;
		}
		/*
		public override IEnumerable<bool> QueryField (Expression<Func<T, bool>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (bool), _query, _order, _distinct, _region, _level);
			foreach (bool item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<bool?> QueryField (Expression<Func<T, bool?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (bool?), _query, _order, _distinct, _region, _level);
			foreach (bool? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<byte> QueryField (Expression<Func<T, byte>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (byte), _query, _order, _distinct, _region, _level);
			foreach (byte item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<byte?> QueryField (Expression<Func<T, byte?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (byte?), _query, _order, _distinct, _region, _level);
			foreach (byte? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<sbyte> QueryField (Expression<Func<T, sbyte>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (sbyte), _query, _order, _distinct, _region, _level);
			foreach (sbyte item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<sbyte?> QueryField (Expression<Func<T, sbyte?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (sbyte?), _query, _order, _distinct, _region, _level);
			foreach (sbyte? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<short> QueryField (Expression<Func<T, short>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (short), _query, _order, _distinct, _region, _level);
			foreach (short item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<short?> QueryField (Expression<Func<T, short?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (short?), _query, _order, _distinct, _region, _level);
			foreach (short? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<int> QueryField (Expression<Func<T, int>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (int), _query, _order, _distinct, _region, _level);
			foreach (int item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<int?> QueryField (Expression<Func<T, int?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (int?), _query, _order, _distinct, _region, _level);
			foreach (int? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<long> QueryField (Expression<Func<T, long>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (long), _query, _order, _distinct, _region, _level);
			foreach (long item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<long?> QueryField (Expression<Func<T, long?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (long?), _query, _order, _distinct, _region, _level);
			foreach (long? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<float> QueryField (Expression<Func<T, float>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (float), _query, _order, _distinct, _region, _level);
			foreach (float item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<float?> QueryField (Expression<Func<T, float?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (float?), _query, _order, _distinct, _region, _level);
			foreach (float? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<double> QueryField (Expression<Func<T, double>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (double), _query, _order, _distinct, _region, _level);
			foreach (double item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<double?> QueryField (Expression<Func<T, double?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (double?), _query, _order, _distinct, _region, _level);
			foreach (double? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<decimal> QueryField (Expression<Func<T, decimal>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (decimal), _query, _order, _distinct, _region, _level);
			foreach (decimal item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<decimal?> QueryField (Expression<Func<T, decimal?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (decimal?), _query, _order, _distinct, _region, _level);
			foreach (decimal? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<DateTime> QueryField (Expression<Func<T, DateTime>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (DateTime), _query, _order, _distinct, _region, _level);
			foreach (DateTime item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<DateTime?> QueryField (Expression<Func<T, DateTime?>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (DateTime?), _query, _order, _distinct, _region, _level);
			foreach (DateTime? item in ie) {
				yield return item;
			}
		}

		public override IEnumerable<string> QueryField (Expression<Func<T, string>> expression)
		{
			DataFieldInfo fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
			IEnumerable ie = _context.QuerySingleField (fieldInfo, typeof (string), _query, _order, _distinct, _region, _level);
			foreach (string item in ie) {
				yield return item;
			}
		}

		public override List<bool> QueryFieldList (Expression<Func<T, bool>> expression)
		{
			IEnumerable<bool> collection = QueryField (expression);
			List<bool> list = new List<bool> (collection);
			return list;
		}

		public override List<bool?> QueryFieldList (Expression<Func<T, bool?>> expression)
		{
			IEnumerable<bool?> collection = QueryField (expression);
			List<bool?> list = new List<bool?> (collection);
			return list;
		}

		public override List<byte> QueryFieldList (Expression<Func<T, byte>> expression)
		{
			IEnumerable<byte> collection = QueryField (expression);
			List<byte> list = new List<byte> (collection);
			return list;
		}

		public override List<byte?> QueryFieldList (Expression<Func<T, byte?>> expression)
		{
			IEnumerable<byte?> collection = QueryField (expression);
			List<byte?> list = new List<byte?> (collection);
			return list;
		}

		public override List<sbyte> QueryFieldList (Expression<Func<T, sbyte>> expression)
		{
			IEnumerable<sbyte> collection = QueryField (expression);
			List<sbyte> list = new List<sbyte> (collection);
			return list;
		}

		public override List<sbyte?> QueryFieldList (Expression<Func<T, sbyte?>> expression)
		{
			IEnumerable<sbyte?> collection = QueryField (expression);
			List<sbyte?> list = new List<sbyte?> (collection);
			return list;
		}

		public override List<short> QueryFieldList (Expression<Func<T, short>> expression)
		{
			IEnumerable<short> collection = QueryField (expression);
			List<short> list = new List<short> (collection);
			return list;
		}

		public override List<short?> QueryFieldList (Expression<Func<T, short?>> expression)
		{
			IEnumerable<short?> collection = QueryField (expression);
			List<short?> list = new List<short?> (collection);
			return list;
		}

		public override List<int> QueryFieldList (Expression<Func<T, int>> expression)
		{
			IEnumerable<int> collection = QueryField (expression);
			List<int> list = new List<int> (collection);
			return list;
		}

		public override List<int?> QueryFieldList (Expression<Func<T, int?>> expression)
		{
			IEnumerable<int?> collection = QueryField (expression);
			List<int?> list = new List<int?> (collection);
			return list;
		}

		public override List<long> QueryFieldList (Expression<Func<T, long>> expression)
		{
			IEnumerable<long> collection = QueryField (expression);
			List<long> list = new List<long> (collection);
			return list;
		}

		public override List<long?> QueryFieldList (Expression<Func<T, long?>> expression)
		{
			IEnumerable<long?> collection = QueryField (expression);
			List<long?> list = new List<long?> (collection);
			return list;
		}

		public override List<float> QueryFieldList (Expression<Func<T, float>> expression)
		{
			IEnumerable<float> collection = QueryField (expression);
			List<float> list = new List<float> (collection);
			return list;
		}

		public override List<float?> QueryFieldList (Expression<Func<T, float?>> expression)
		{
			IEnumerable<float?> collection = QueryField (expression);
			List<float?> list = new List<float?> (collection);
			return list;
		}

		public override List<double> QueryFieldList (Expression<Func<T, double>> expression)
		{
			IEnumerable<double> collection = QueryField (expression);
			List<double> list = new List<double> (collection);
			return list;
		}

		public override List<double?> QueryFieldList (Expression<Func<T, double?>> expression)
		{
			IEnumerable<double?> collection = QueryField (expression);
			List<double?> list = new List<double?> (collection);
			return list;
		}

		public override List<decimal> QueryFieldList (Expression<Func<T, decimal>> expression)
		{
			IEnumerable<decimal> collection = QueryField (expression);
			List<decimal> list = new List<decimal> (collection);
			return list;
		}

		public override List<decimal?> QueryFieldList (Expression<Func<T, decimal?>> expression)
		{
			IEnumerable<decimal?> collection = QueryField (expression);
			List<decimal?> list = new List<decimal?> (collection);
			return list;
		}

		public override List<DateTime> QueryFieldList (Expression<Func<T, DateTime>> expression)
		{
			IEnumerable<DateTime> collection = QueryField (expression);
			List<DateTime> list = new List<DateTime> (collection);
			return list;
		}

		public override List<DateTime?> QueryFieldList (Expression<Func<T, DateTime?>> expression)
		{
			IEnumerable<DateTime?> collection = QueryField (expression);
			List<DateTime?> list = new List<DateTime?> (collection);
			return list;
		}

		public override List<string> QueryFieldList (Expression<Func<T, string>> expression)
		{
			IEnumerable<string> collection = QueryField (expression);
			List<string> list = new List<string> (collection);
			return list;
		}
		*/
	}
}

