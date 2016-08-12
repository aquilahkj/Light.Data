using System;
using System.Linq.Expressions;

namespace Light.Data
{
	public class LightJoinTable<T, T1, T2> : IJoinTable<T, T1, T2>
		where T : class, new()
		where T1 : class, new()
		where T2 : class, new()
	{
		public IJoinTable<T, T1, T2> OrderBy<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> OrderByCatch<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> OrderByDescending<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> OrderByDescendingCatch<TKey> (Expression<Func<T, T1, TKey>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> OrderByRandom ()
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> OrderByReset ()
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> PageSize (int page, int size)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> Range (int from, int to)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> RangeReset ()
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> SafeMode (SafeLevel level)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> Skip (int index)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> Take (int count)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> Where (Expression<Func<T, T1, T2, bool>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> WhereReset ()
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> WhereWithAnd (Expression<Func<T, T1, bool>> expression)
		{
			throw new NotImplementedException ();
		}

		public IJoinTable<T, T1, T2> WhereWithOr (Expression<Func<T, T1, bool>> expression)
		{
			throw new NotImplementedException ();
		}
	}
}

