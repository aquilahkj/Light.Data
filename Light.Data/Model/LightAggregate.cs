using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	public class LightAggregate<T, K> : IAggregate<K>
		where T : class, new()
		where K : class
	{
		AggregateModel _model;

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

		DataEntityMapping _mapping;

		public LightAggregate (LightQuery<T> query, Expression<Func<T, K>> expression)
		{
			_mapping = DataEntityMapping.GetEntityMapping (typeof (T));
			_context = query.Context;
			_query = query.Query;
			_level = query.Level;
			_model = LambdaExpressionExtend.CreateAggregateModel (expression);
		}

		public IAggregate<K> Having (Expression<Func<K, bool>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> HavingReset ()
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> HavingWithAnd (Expression<Func<K, bool>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> HavingWithOr (Expression<Func<K, bool>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderBy<TKey> (Expression<Func<K, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderByCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderByDescending<TKey> (Expression<Func<K, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderByDescendingCatch<TKey> (Expression<Func<K, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderByRandom ()
		{
			throw new NotImplementedException ();
		}

		public IAggregate<K> OrderByReset ()
		{
			throw new NotImplementedException ();
		}

		public List<K> ToList ()
		{
			List<K> list = _context.QueryDynamicAggregateList<K> (_mapping, _model.Mapping, _model.FieldInfoList, _model.AggregateFunctionList, _query, null, null, _level);
			return list;
		}
	}
}

