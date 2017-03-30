using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class SelectFieldBase<K> : ISelectField<K>
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

		protected DataFieldInfo _fieldInfo;

		protected SelectFieldBase (DataContext context, LambdaExpression expression)
		{
			_context = context;
			_fieldInfo = LambdaExpressionExtend.ResolveSingleField (expression);
		}

		public abstract IEnumerator<K> GetEnumerator ();

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public abstract List<K> ToList ();

		public abstract K [] ToArray ();

		public abstract K First ();

		public abstract K ElementAt (int index);
	}
}
