using System;

namespace Light.Data
{
	public class SelectInsterExecutor
	{
		OrderExpression _order;

		QueryExpression _query;

		readonly Type _selectType;

		readonly Type _insertType;

		DataFieldInfo[] _insertFields = null;

		SelectFieldInfo[] _selectFields = null;

		DataContext _context;

		internal SelectInsterExecutor (DataContext context, Type insertType, Type selectType, QueryExpression query, OrderExpression order)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			if (insertType == null)
				throw new ArgumentNullException ("insertType");
			if (selectType == null)
				throw new ArgumentNullException ("selectType");
			this._context = context;
			this._insertType = insertType;
			this._selectType = selectType;
			this._query = query;
			this._order = order;
		}

		/// <summary>
		/// reset where expression.
		/// </summary>
		/// <returns>The reset.</returns>
		public SelectInsterExecutor WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// replace where expression
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		/// <param name="expression">Expression.</param>
		public SelectInsterExecutor Where (QueryExpression expression)
		{
			//			if (expression == null)
			//				throw new ArgumentNullException ("expression");
			_query = expression;
			return this;
		}

		/// <summary>
		/// and catch where expression.
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		/// <param name="expression">Expression.</param>
		public SelectInsterExecutor WhereWithAnd (QueryExpression expression)
		{
			//			if (expression == null)
			//				throw new ArgumentNullException ("expression");
			_query = QueryExpression.And (_query, expression);
			return this;
		}

		/// <summary>
		/// or catch where expression.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		public SelectInsterExecutor WhereWithOr (QueryExpression expression)
		{
			//			if (expression == null)
			//				throw new ArgumentNullException ("expression");
			_query = QueryExpression.Or (_query, expression);
			return this;
		}

		/// <summary>
		/// catch order by expression.
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		/// <param name="expression">Expression.</param>
		public SelectInsterExecutor OrderByCatch (OrderExpression expression)
		{
			//			if (expression == null)
			//				throw new ArgumentNullException ("expression");
			_order = OrderExpression.Catch (_order, expression);
			return this;
		}

		/// <summary>
		/// replace order by expression.
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		/// <param name="expression">Expression.</param>
		public SelectInsterExecutor OrderBy (OrderExpression expression)
		{
			//			if (expression == null)
			//				throw new ArgumentNullException ("expression");
			_order = expression;
			return this;
		}

		/// <summary>
		/// reset order by expression
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		public SelectInsterExecutor OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// order by random.
		/// </summary>
		/// <returns>SelectInsterExecutor.</returns>
		public SelectInsterExecutor OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataMapping.GetEntityMapping (_selectType));
			return this;
		}

		public SelectInsterExecutor SetInsertField (params DataFieldInfo[] infos)
		{
			this._insertFields = infos;
			return this;
		}

		public SelectInsterExecutor SetSelectField (params SelectFieldInfo[] infos)
		{
			this._selectFields = infos;
			return this;
		}

		public int Execute ()
		{
			return this._context.SelectInto (_insertType, _insertFields, _selectType, _selectFields, _query, _order);
		}
	}
}

