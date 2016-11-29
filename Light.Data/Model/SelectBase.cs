using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class SelectBase<K> : ISelect<K> where K : class
	{
		protected readonly QueryExpression _query;

		internal QueryExpression QueryExpression {
			get {
				return _query;
			}
		}

		protected readonly OrderExpression _order;

		internal OrderExpression OrderExpression {
			get {
				return _order;
			}
		}

		protected readonly bool _distinct;

		internal bool Distinct {
			get {
				return _distinct;
			}
		}

		protected readonly Region _region;

		internal Region Region {
			get {
				return _region;
			}
		}

		protected readonly DataContext _context;

		internal DataContext Context {
			get {
				return _context;
			}
		}

		protected readonly SafeLevel _level;

		internal SafeLevel Level {
			get {
				return _level;
			}
		}

		protected readonly ISelector _selector;

		internal ISelector Selector {
			get {
				return _selector;
			}
		}


		protected readonly DataEntityMapping _mapping;

		internal DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		protected readonly AggregateGroup _model;

		internal AggregateGroup Model {
			get {
				return _model;
			}
		}

		protected SelectBase (DataContext context, ISelector selector, Type type, AggregateGroup model, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
		{
			_context = context;
			_selector = selector;
			_query = query;
			_order = order;
			_distinct = distinct;
			_region = region;
			_level = level;
			_mapping = DataEntityMapping.GetEntityMapping (type);
			_model = model;
		}

		public abstract K First ();

		public abstract IEnumerator<K> GetEnumerator ();

		public abstract List<K> ToList ();

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class
		{
			LightQuery<T1> lightQuery = new LightQuery<T1> (_context);
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, lightQuery, onExpression);
		}

		public IJoinTable<K, T1> Join<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class
		{
			QueryBase<T1> queryBase = query as QueryBase<T1>;
			if (queryBase == null) {
				throw new ArgumentException (nameof (query));
			}
			return new LightJoinTable<K, T1> (this, JoinType.LeftJoin, queryBase, onExpression);
		}
	}
}
