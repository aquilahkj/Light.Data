using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class QueryBase<T> : IQuery<T> where T : class
	{
		protected readonly DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		protected readonly DataEntityMapping _mapping;

		internal DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		protected QueryExpression _query;

		internal QueryExpression QueryExpression {
			get {
				return _query;
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

		protected bool _distinct;

		internal bool Distinct {
			get {
				return _distinct;
			}
		}

		protected SafeLevel _level;

		internal SafeLevel Level {
			get {
				return _level;
			}
		}

		protected QueryBase (DataContext dataContext)
		{
			_context = dataContext;
			_mapping = DataEntityMapping.GetEntityMapping (typeof (T));
		}

		public abstract int Count {
			get;
		}

		public abstract bool Exists {
			get;
		}

		public abstract int Delete ();

		public abstract T ElementAt (int index);

		public abstract T First ();

		public abstract IEnumerator<T> GetEnumerator ();

		public abstract IAggregate<K> GroupBy<K> (Expression<Func<T, K>> expression) where K : class;

		public abstract int Insert<K> () where K : class, new();

		public abstract IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;



		public abstract IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByRandom ();

		public abstract IQuery<T> OrderByReset ();

		public abstract IQuery<T> PageSize (int page, int size);

		public abstract IEnumerable<K> QuerySingleField<K> (Expression<Func<T, K>> expression);

		public abstract List<K> QuerySingleFieldList<K> (Expression<Func<T, K>> expression);

		public abstract IQuery<T> Range (int from, int to);

		public abstract IQuery<T> RangeReset ();

		public abstract IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IQuery<T> SafeMode (SafeLevel level);

		public abstract ISelect<TResult> Select<TResult> (Expression<Func<T, TResult>> expression) where TResult : class;

		public abstract int SelectInsert<K> (Expression<Func<T, K>> expression) where K : class, new();

		public abstract IQuery<T> SetDistinct (bool distinct);

		public abstract IQuery<T> Skip (int index);

		public abstract IQuery<T> Take (int count);

		public abstract T [] ToArray ();

		public abstract List<T> ToList ();

		public abstract int Update (Expression<Func<T, T>> expression);

		public abstract IQuery<T> Where (Expression<Func<T, bool>> expression);

		public abstract IQuery<T> WhereReset ();

		public abstract IQuery<T> WhereWithAnd (Expression<Func<T, bool>> expression);

		public abstract IQuery<T> WhereWithOr (Expression<Func<T, bool>> expression);

		public abstract IJoinTable<T, T1> Join<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;


		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

	}
}