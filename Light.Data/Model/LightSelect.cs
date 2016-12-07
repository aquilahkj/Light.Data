using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LightSelect<T, K> : SelectBase<K>
		where T : class
		where K : class
	{
		#region IEnumerable implementation

		public override IEnumerator<K> GetEnumerator ()
		{
			foreach (object item in _context.QueryEntityData (_mapping, _selector, _query, _order, _distinct, _region, _level)) {
				object obj = _dele.DynamicInvoke (item);
				yield return obj as K;
			}
		}

		#endregion

		protected QueryExpression _query;

		public override QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		protected OrderExpression _order;

		public override OrderExpression OrderExpression {
			get {
				return _order;
			}
		}

		protected bool _distinct;

		public override bool Distinct {
			get {
				return _distinct;
			}
		}

		protected Region _region;

		public override Region Region {
			get {
				return _region;
			}
		}

		protected SafeLevel _level;

		public override SafeLevel Level {
			get {
				return _level;
			}
		}

		public LightSelect (DataContext context, Expression<Func<T, K>> expression, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			: base (context, expression, typeof (T))
		{
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
		}



		public override List<K> ToList ()
		{
			List<K> list = new List<K> ();
			foreach (object item in _context.QueryEntityData (_mapping, _selector, _query, _order, _distinct, _region, _level)) {
				if (item != null) {
					object obj = _dele.DynamicInvoke (item);
					list.Add (obj as K);
				}
				else {
					list.Add (null);
				}

			}
			return list;
		}

		public override K First ()
		{
			object item = _context.SelectEntityDataSingle (_mapping, _query, _order, 0, _level);
			if (item != null) {
				object obj = _dele.DynamicInvoke (item);
				return obj as K;
			}
			else {
				return null;
			}
		}

		public override IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, queryBase, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, queryBase, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			if (queryExpression != null) {
				lightQuery.Where (queryExpression);
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression)
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, lightQuery, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression)
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, queryBase, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> aggregateBase = aggregate as AggregateBase<T1>;
			if (aggregateBase == null) {
				throw new ArgumentException (nameof (aggregate));
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, aggregateBase, onExpression);
		}

		public override IJoinTable<K, T1> Join<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> selectBase = select as AggregateBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<K, T1> (this, JoinType.InnerJoin, selectBase, onExpression);
		}

		public override IJoinTable<K, T1> LeftJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> selectBase = select as AggregateBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, selectBase, onExpression);
		}

		public override IJoinTable<K, T1> RightJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression)
		{
			AggregateBase<T1> selectBase = select as AggregateBase<T1>;
			if (selectBase == null) {
				throw new ArgumentException (nameof (select));
			}
			return new LightJoinTable<K, T1> (this, JoinType.RightJoin, selectBase, onExpression);
		}
	}
}

