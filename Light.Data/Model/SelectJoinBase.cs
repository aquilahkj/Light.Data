using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class SelectJoinBase<K> : ISelectJoin<K> where K : class
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

		protected readonly DynamicMultiDataMapping _mapping;

		public DynamicMultiDataMapping Mapping {
			get {
				return _mapping;
			}
		}

		protected IJoinModel [] _models;

		protected Delegate _dele;

		protected SelectJoinBase (DataContext context, LambdaExpression expression, List<IJoinModel> models, List<IMap> maps)
		{
			_selector = LambdaExpressionExtend.CreateMutliSelector (expression, maps);
			_dele = expression.Compile ();
			_models = models.ToArray ();
			_context = context;
			_mapping = DynamicMultiDataMapping.CreateDynamicMultiDataMapping (typeof (K), _models);
		}

		public abstract IEnumerator<K> GetEnumerator ();

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public abstract List<K> ToList ();

		public abstract K First ();

		public abstract K ElementAt (int index);
	}
}
