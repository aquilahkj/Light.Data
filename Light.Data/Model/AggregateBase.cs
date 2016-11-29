using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class AggregateBase<K> : IAggregate<K>
	  where K : class
	{
		protected readonly DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		protected readonly AggregateGroup _model;

		internal AggregateGroup Model {
			get {
				return _model;
			}
		}

		protected QueryExpression _query;

		internal QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		protected QueryExpression _having;

		internal QueryExpression HavingExpression {
			get {
				return _having;
			}
		}

		protected OrderExpression _order;

		internal OrderExpression OrderExpression {
			get {
				return _order;
			}
		}

		protected Region _region;

		internal Region Region {
			get {
				return _region;
			}
		}

		protected SafeLevel _level;

		internal SafeLevel SafeLevel {
			get {
				return _level;
			}
		}

		protected AggregateBase (DataContext context, AggregateGroup model)
		{
			_context = context;
			_model = model;
		}

		public abstract K First ();

		//public abstract AggregateGroupData GetGroupData ();

		public abstract IAggregate<K> Having (Expression<Func<K, bool>> expression);

		public abstract IAggregate<K> HavingReset ();

		public abstract IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression);

		public abstract IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression);

		public abstract IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression);

		public abstract IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression);

		public abstract IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression);

		public abstract IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression);

		public abstract IAggregate<K> OrderByRandom ();

		public abstract IAggregate<K> OrderByReset ();

		public abstract IAggregate<K> PageSize (int page, int size);

		public abstract IAggregate<K> Range (int from, int to);

		public abstract IAggregate<K> RangeReset ();

		public abstract IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IAggregate<K> SafeMode (SafeLevel level);

		public abstract IAggregate<K> Skip (int index);

		public abstract IAggregate<K> Take (int count);

		public abstract List<K> ToList ();
	}
}
