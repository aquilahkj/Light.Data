using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class QueryBase<T> : IQuery<T> where T : class
	{
		public abstract QueryExpression QueryExpression {
			get;
		}

		public abstract OrderExpression OrderExpression {
			get;
		}

		public abstract Region Region {
			get;
		}

		public abstract bool Distinct {
			get;
		}

		public abstract SafeLevel Level {
			get;
		}

		protected readonly DataContext _context;

		public DataContext Context {
			get {
				return _context;
			}
		}

		protected readonly DataEntityMapping _mapping;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
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

		public abstract long LongCount {
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

		public abstract IQuery<T> OrderBy<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByCatch<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByDescending<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByDescendingCatch<TKey> (Expression<Func<T, TKey>> expression);

		public abstract IQuery<T> OrderByRandom ();

		public abstract IQuery<T> OrderByReset ();

		public abstract IQuery<T> PageSize (int page, int size);

		//public abstract IEnumerable<K> QuerySingleField<K> (Expression<Func<T, K>> expression);

		//public abstract List<K> QuerySingleFieldList<K> (Expression<Func<T, K>> expression);

		public abstract IQuery<T> Range (int from, int to);

		public abstract IQuery<T> RangeReset ();

		public abstract IQuery<T> SafeMode (SafeLevel level);

		public abstract ISelect<K> Select<K> (Expression<Func<T, K>> expression) where K : class;

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

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public abstract IJoinTable<T, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> Join<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> LeftJoin<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<T, T1> RightJoin<T1> (ISelect<T1> select, Expression<Func<T, T1, bool>> onExpression) where T1 : class;

		public abstract ISelectField<K> SelectField<K> (Expression<Func<T, K>> expression);

		/*

		public abstract IEnumerable<bool> QueryField (Expression<Func<T, bool>> expression);

		public abstract IEnumerable<bool?> QueryField (Expression<Func<T, bool?>> expression);

		public abstract IEnumerable<byte> QueryField (Expression<Func<T, byte>> expression);

		public abstract IEnumerable<byte?> QueryField (Expression<Func<T, byte?>> expression);

		public abstract IEnumerable<sbyte> QueryField (Expression<Func<T, sbyte>> expression);

		public abstract IEnumerable<sbyte?> QueryField (Expression<Func<T, sbyte?>> expression);

		public abstract IEnumerable<short> QueryField (Expression<Func<T, short>> expression);

		public abstract IEnumerable<short?> QueryField (Expression<Func<T, short?>> expression);

		public abstract IEnumerable<int> QueryField (Expression<Func<T, int>> expression);

		public abstract IEnumerable<int?> QueryField (Expression<Func<T, int?>> expression);

		public abstract IEnumerable<long> QueryField (Expression<Func<T, long>> expression);

		public abstract IEnumerable<long?> QueryField (Expression<Func<T, long?>> expression);

		public abstract IEnumerable<float> QueryField (Expression<Func<T, float>> expression);

		public abstract IEnumerable<float?> QueryField (Expression<Func<T, float?>> expression);

		public abstract IEnumerable<double> QueryField (Expression<Func<T, double>> expression);

		public abstract IEnumerable<double?> QueryField (Expression<Func<T, double?>> expression);

		public abstract IEnumerable<decimal> QueryField (Expression<Func<T, decimal>> expression);

		public abstract IEnumerable<decimal?> QueryField (Expression<Func<T, decimal?>> expression);

		public abstract IEnumerable<DateTime> QueryField (Expression<Func<T, DateTime>> expression);

		public abstract IEnumerable<DateTime?> QueryField (Expression<Func<T, DateTime?>> expression);

		public abstract IEnumerable<string> QueryField (Expression<Func<T, string>> expression);

		public abstract List<bool> QueryFieldList (Expression<Func<T, bool>> expression);

		public abstract List<bool?> QueryFieldList (Expression<Func<T, bool?>> expression);

		public abstract List<byte> QueryFieldList (Expression<Func<T, byte>> expression);

		public abstract List<byte?> QueryFieldList (Expression<Func<T, byte?>> expression);

		public abstract List<sbyte> QueryFieldList (Expression<Func<T, sbyte>> expression);

		public abstract List<sbyte?> QueryFieldList (Expression<Func<T, sbyte?>> expression);

		public abstract List<short> QueryFieldList (Expression<Func<T, short>> expression);

		public abstract List<short?> QueryFieldList (Expression<Func<T, short?>> expression);

		public abstract List<int> QueryFieldList (Expression<Func<T, int>> expression);

		public abstract List<int?> QueryFieldList (Expression<Func<T, int?>> expression);

		public abstract List<long> QueryFieldList (Expression<Func<T, long>> expression);

		public abstract List<long?> QueryFieldList (Expression<Func<T, long?>> expression);

		public abstract List<float> QueryFieldList (Expression<Func<T, float>> expression);

		public abstract List<float?> QueryFieldList (Expression<Func<T, float?>> expression);

		public abstract List<double> QueryFieldList (Expression<Func<T, double>> expression);

		public abstract List<double?> QueryFieldList (Expression<Func<T, double?>> expression);

		public abstract List<decimal> QueryFieldList (Expression<Func<T, decimal>> expression);

		public abstract List<decimal?> QueryFieldList (Expression<Func<T, decimal?>> expression);

		public abstract List<DateTime> QueryFieldList (Expression<Func<T, DateTime>> expression);

		public abstract List<DateTime?> QueryFieldList (Expression<Func<T, DateTime?>> expression);

		public abstract List<string> QueryFieldList (Expression<Func<T, string>> expression);

		*/
	}

}