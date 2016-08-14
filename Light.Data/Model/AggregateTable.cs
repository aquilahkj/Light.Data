using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// Aggregate table.
	/// </summary>
	public class AggregateTable<T> where T : class, new()
	{
		DataEntityMapping _enetityMapping;

		readonly DataContext _context;

		QueryExpression _query;

		AggregateHavingExpression _having;

		OrderExpression _order;

		SafeLevel _level = SafeLevel.Default;

		Dictionary<string, AggregateDataInfo> _dataFieldInfoDictionary = new Dictionary<string, AggregateDataInfo> ();

		Dictionary<string, AggregateDataInfo> _aggregateFunctionDictionary = new Dictionary<string, AggregateDataInfo> ();

		internal AggregateTable (DataContext dataContext)
		{
			_context = dataContext;
			_enetityMapping = DataEntityMapping.GetEntityMapping (typeof (T));
		}

		/// <summary>
		/// Gets the data table.
		/// </summary>
		/// <returns>The data table.</returns>
		public DataTable GetDataTable ()
		{
			List<AggregateDataInfo> groupbys = new List<AggregateDataInfo> (_dataFieldInfoDictionary.Values);
			List<AggregateDataInfo> functions = new List<AggregateDataInfo> (_aggregateFunctionDictionary.Values);
			return _context.QueryDynamicAggregateTable (_enetityMapping, groupbys, functions, _query, _having, _order, _level);
		}

		/// <summary>
		/// Gets the object list.
		/// </summary>
		/// <returns>The object list.</returns>
		/// <typeparam name="K">The 1st type parameter.</typeparam>
		public List<K> GetObjectList<K> () where K : class, new()
		{
			List<AggregateDataInfo> groupbys = new List<AggregateDataInfo> (_dataFieldInfoDictionary.Values);
			List<AggregateDataInfo> functions = new List<AggregateDataInfo> (_aggregateFunctionDictionary.Values);
			List<K> list = _context.QueryDynamicAggregateList<K> (_enetityMapping, groupbys, functions, _query, _having, _order, _level);
			return list;
		}

		/// <summary>
		/// Groups by field.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="fieldInfo">Field info.</param>
		public AggregateTable<T> GroupBy (DataFieldInfo fieldInfo)
		{
			GroupBy (fieldInfo, null);
			return this;
		}

		/// <summary>
		/// Add groups by field with alias name.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="alias">Alias.</param>
		public AggregateTable<T> GroupBy (DataFieldInfo fieldInfo, string alias)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException (nameof (fieldInfo));
			}
			//if (string.IsNullOrEmpty (alias)) {
			//	alias = fieldInfo.FieldName;
			//	if (fieldInfo is ExtendDataFieldInfo) {
			//		fieldInfo = new AliasDataFieldInfo (fieldInfo, alias);
			//	}
			//}
			//else {
			//	fieldInfo = new AliasDataFieldInfo (fieldInfo, alias);
			//}
			if (string.IsNullOrEmpty (alias)) {
				alias = fieldInfo.FieldName;
			}

			AggregateDataFieldInfo dataFieldInfo = new AggregateDataFieldInfo (fieldInfo);
			AggregateDataInfo data = new AggregateDataInfo (dataFieldInfo, alias);

			if (_dataFieldInfoDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.GroupNameFieldIsExists, alias));
			}
			if (_aggregateFunctionDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.AggregateFunctionFieldIsExists, alias));
			}
			_dataFieldInfoDictionary.Add (alias, data);

			return this;
		}

		/// <summary>
		/// Add the specified function with alias name.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="function">Function.</param>
		/// <param name="alias">Alias.</param>
		public AggregateTable<T> Aggregate (AggregateData function, string alias)
		{
			if (Object.Equals (function, null)) {
				throw new ArgumentNullException (nameof (function));
			}
			if (string.IsNullOrEmpty (alias)) {
				throw new ArgumentNullException (nameof (alias));
			}
			if (_aggregateFunctionDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.AggregateFunctionFieldIsExists, alias));
			}
			if (_dataFieldInfoDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.GroupNameFieldIsExists, alias));
			}
			AggregateDataInfo info = new AggregateDataInfo (function, alias);
			_aggregateFunctionDictionary.Add (alias, info);

			return this;
		}

		/// <summary>
		/// Set the specified having expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> Having (AggregateHavingExpression expression)
		{
			_having = expression;
			return this;
		}

		/// <summary>
		/// Catch the specified having expression with and.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> HavingWithAnd (AggregateHavingExpression expression)
		{
			_having = AggregateHavingExpression.And (_having, expression);
			return this;
		}

		/// <summary>
		/// Catch the specified having expression with or.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> HavingWithOr (AggregateHavingExpression expression)
		{
			_having = AggregateHavingExpression.Or (_having, expression);
			return this;
		}

		/// <summary>
		/// Reset the specified having expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <returns>The reset.</returns>
		public AggregateTable<T> HavingReset ()
		{
			_having = null;
			return this;
		}

		/// <summary>
		/// Catch the specified order by expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">AggregateTable.</param>
		public AggregateTable<T> OrderByCatch (OrderExpression expression)
		{
			_order = OrderExpression.Catch (_order, expression);
			return this;
		}

		/// <summary>
		/// Set the specified order by expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">AggregateTable.</param>
		public AggregateTable<T> OrderBy (OrderExpression expression)
		{
			_order = expression;
			return this;
		}

		/// <summary>
		/// Reset the specified order by expression
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <returns>AggregateTable.</returns>
		public AggregateTable<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// Set order by random.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		public AggregateTable<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataEntityMapping.GetEntityMapping (typeof (T)));
			return this;
		}

		/// <summary>
		/// Reset the specified where expression.
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		public AggregateTable<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// Set the specified where expression
		/// </summary>
		/// <returns>The aggregateTable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> Where (QueryExpression expression)
		{
			_query = expression;
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with and.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> WhereWithAnd (QueryExpression expression)
		{
			_query = QueryExpression.And (_query, expression);
			return this;
		}

		/// <summary>
		/// Catch the specified where expression with or.
		/// </summary>
		/// <returns>LEnumerables.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> WhereWithOr (QueryExpression expression)
		{
			_query = QueryExpression.Or (_query, expression);
			return this;
		}
	}
}
