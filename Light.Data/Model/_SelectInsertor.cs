//using System;

//namespace Light.Data
//{
//	/// <summary>
//	/// Select insertor.
//	/// </summary>
//	public class SelectInsertor
//	{
//		OrderExpression _order;

//		QueryExpression _query;

//		readonly Type _selectType;

//		readonly Type _insertType;

//		DataFieldInfo[] _insertFields = null;

//		SelectFieldInfo[] _selectFields = null;

//		DataContext _context;

//		/// <summary>
//		/// Initializes a new instance of the <see cref="Light.Data.SelectInsertor"/> class.
//		/// </summary>
//		/// <param name="context">Context.</param>
//		/// <param name="insertType">Insert type.</param>
//		/// <param name="selectType">Select type.</param>
//		/// <param name="query">Query.</param>
//		/// <param name="order">Order.</param>
//		internal SelectInsertor (DataContext context, Type insertType, Type selectType, QueryExpression query, OrderExpression order)
//		{
//			if (context == null)
//				throw new ArgumentNullException (nameof (context));
//			if (insertType == null)
//				throw new ArgumentNullException (nameof (insertType));
//			if (selectType == null)
//				throw new ArgumentNullException (nameof (selectType));
//			this._context = context;
//			this._insertType = insertType;
//			this._selectType = selectType;
//			this._query = query;
//			this._order = order;
//		}

//		/// <summary>
//		/// Reset the specified where expression with or.
//		/// </summary>
//		/// <returns>The reset.</returns>
//		public SelectInsertor WhereReset ()
//		{
//			_query = null;
//			return this;
//		}

//		/// <summary>
//		/// Set the specified where expression with or.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		/// <param name="expression">Expression.</param>
//		public SelectInsertor Where (QueryExpression expression)
//		{
//			_query = expression;
//			return this;
//		}

//		/// <summary>
//		/// Catch the specified where expression with and.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		/// <param name="expression">Expression.</param>
//		public SelectInsertor WhereWithAnd (QueryExpression expression)
//		{
//			_query = QueryExpression.And (_query, expression);
//			return this;
//		}

//		/// <summary>
//		/// Catch the specified where expression with or.
//		/// </summary>
//		/// <returns>LEnumerables.</returns>
//		/// <param name="expression">Expression.</param>
//		public SelectInsertor WhereWithOr (QueryExpression expression)
//		{
//			_query = QueryExpression.Or (_query, expression);
//			return this;
//		}

//		/// <summary>
//		/// Catch the specified order by expression.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		/// <param name="expression">Expression.</param>
//		public SelectInsertor OrderByCatch (OrderExpression expression)
//		{
//			_order = OrderExpression.Catch (_order, expression);
//			return this;
//		}

//		/// <summary>
//		/// Set the specified order by expression.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		/// <param name="expression">Expression.</param>
//		public SelectInsertor OrderBy (OrderExpression expression)
//		{
//			_order = expression;
//			return this;
//		}

//		/// <summary>
//		/// Reset the specified order by expression.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		public SelectInsertor OrderByReset ()
//		{
//			_order = null;
//			return this;
//		}

//		/// <summary>
//		/// Set order by random.
//		/// </summary>
//		/// <returns>SelectInsterExecutor.</returns>
//		public SelectInsertor OrderByRandom ()
//		{
//			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (_selectType));
//			return this;
//		}

//		/// <summary>
//		/// Set the insert fields.
//		/// </summary>
//		/// <returns>The insert field.</returns>
//		/// <param name="infos">Infos.</param>
//		public SelectInsertor SetInsertField (params DataFieldInfo[] infos)
//		{
//			this._insertFields = infos;
//			return this;
//		}

//		/// <summary>
//		/// Set the select fields.
//		/// </summary>
//		/// <returns>The select field.</returns>
//		/// <param name="infos">Infos.</param>
//		public SelectInsertor SetSelectField (params SelectFieldInfo[] infos)
//		{
//			this._selectFields = infos;
//			return this;
//		}

//		/// <summary>
//		/// Execute this instance.
//		/// </summary>
//		public int Execute ()
//		{
//			return this._context.SelectInsert (_insertType, _insertFields, _selectType, _selectFields, _query, _order);
//		}
//	}
//}

