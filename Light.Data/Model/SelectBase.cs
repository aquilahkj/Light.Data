using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class SelectBase<K> : ISelect<K> where K : class
	{
		public abstract QueryExpression QueryExpression {
			get;
		}

		public abstract OrderExpression OrderExpression {
			get;
		}

		public abstract bool Distinct {
			get;
		}

		public abstract Region Region {
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

		protected readonly ISelector _selector;

		public ISelector Selector {
			get {
				return _selector;
			}
		}

		protected readonly DataEntityMapping _mapping;

		public DataEntityMapping Mapping {
			get {
				return _mapping;
			}
		}

		public SelectModel Model {
			get {
				return LambdaExpressionExtend.CreateSelectModel (_expression);
			}
		}

		LambdaExpression _expression;

		protected readonly Delegate _dele;

		protected SelectBase (DataContext context, LambdaExpression expression, Type type)
		{
			_context = context;
			_dele = expression.Compile ();
			_selector = LambdaExpressionExtend.CreateSelector (expression);
			_expression = expression;
			_mapping = DataEntityMapping.GetEntityMapping (type);
		}

		public abstract K First ();

		public abstract K ElementAt (int index);

		public abstract IEnumerator<K> GetEnumerator ();

		public abstract List<K> ToList ();

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public abstract IJoinTable<K, T1> Join<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (Expression<Func<T1, bool>> queryExpression, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (IQuery<T1> query, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (IAggregate<T1> aggregate, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> Join<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> LeftJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;

		public abstract IJoinTable<K, T1> RightJoin<T1> (ISelect<T1> select, Expression<Func<K, T1, bool>> onExpression) where T1 : class;
	}
}
