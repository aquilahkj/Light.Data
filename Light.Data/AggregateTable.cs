using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// 统计表生成器
	/// </summary>
	/// <typeparam name="T">要统计的表类型</typeparam>
	public class AggregateTable<T> where T : class, new()
	{
		DataEntityMapping _enetityMapping;

		DataContext _context;

		QueryExpression _query;

		AggregateHavingExpression _having;

		OrderExpression _order;

		SafeLevel _level = SafeLevel.Default;

		Dictionary<string, DataFieldInfo> _dataFieldInfoDictionary = new Dictionary<string, DataFieldInfo> ();

		Dictionary<string, AggregateFunctionInfo> _aggregateFunctionDictionary = new Dictionary<string, AggregateFunctionInfo> ();

		internal AggregateTable (DataContext dataContext)
		{
			_context = dataContext;
			_enetityMapping = DataMapping.GetEntityMapping (typeof(T));
		}

		//		/// <summary>
		//		/// 重置条件语句
		//		/// </summary>
		//		/// <returns>统计表生成器</returns>
		//		public AggregateTable<T> Reset ()
		//		{
		//			_query = null;
		//			_order = null;
		//			_having = null;
		//			_level = SafeLevel.Default;
		//			return this;
		//		}

		/// <summary>
		/// 生成统计表的DataTable
		/// </summary>
		/// <returns>统计表生成器</returns>
		public DataTable GetDataTable ()
		{
			List<DataFieldInfo> fields = new List<DataFieldInfo> (_dataFieldInfoDictionary.Values);
			List<AggregateFunctionInfo> functions = new List<AggregateFunctionInfo> (_aggregateFunctionDictionary.Values);
			return _context.QueryDynamicAggregateTable (_enetityMapping, fields, functions, _query, _having, _order, _level);
		}

		/// <summary>
		/// 生成统计表的类型集合
		/// </summary>
		/// <typeparam name="K">生成类型</typeparam>
		/// <returns>类型集合</returns>
		public List<K> GetObjectList<K> () where K : class, new()
		{
			List<DataFieldInfo> fields = new List<DataFieldInfo> (_dataFieldInfoDictionary.Values);
			List<AggregateFunctionInfo> functions = new List<AggregateFunctionInfo> (_aggregateFunctionDictionary.Values);
			List<K> list = _context.QueryDynamicAggregateList<K> (_enetityMapping, fields, functions, _query, _having, _order, _level);
			return list;
		}

		/// <summary>
		/// 加入分组字段
		/// </summary>
		/// <param name="fieldInfo">分组字段</param>
		/// <returns>统计表生成器</returns>
		public AggregateTable<T> GroupBy (DataFieldInfo fieldInfo)
		{
			GroupBy (fieldInfo, null);
			return this;
		}

		/// <summary>
		/// 添加分组字段
		/// </summary>
		/// <param name="fieldInfo">分组字段</param>
		/// <param name="alias">查询别名,与字段配置字段对应</param>
		/// <returns>统计表生成器</returns>
		public AggregateTable<T> GroupBy (DataFieldInfo fieldInfo, string alias)
		{
			if (Object.Equals (fieldInfo, null)) {
				throw new ArgumentNullException ("fieldInfo");
			}
			if (string.IsNullOrEmpty (alias)) {
				alias = fieldInfo.FieldName;
				if (fieldInfo is ExtendDataFieldInfo) {
					fieldInfo = new AliasDataFieldInfo (fieldInfo, alias);
				}
			}
			else {
				fieldInfo = new AliasDataFieldInfo (fieldInfo, alias);
			}
			if (_dataFieldInfoDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.GroupNameFieldIsExists, alias));
			}
			if (_aggregateFunctionDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.AggregateFunctionFieldIsExists, alias));
			}
			_dataFieldInfoDictionary.Add (alias, fieldInfo);

			return this;
		}

		/// <summary>
		/// 添加聚合方法
		/// </summary>
		/// <param name="function">聚合函数</param>
		/// <param name="alias">查询别名,与字段配置字段对应</param>
		/// <returns>统计表生成器</returns>
		public AggregateTable<T> Aggregate (AggregateFunction function, string alias)
		{
			if (Object.Equals (function, null)) {
				throw new ArgumentNullException ("function");
			}
			if (string.IsNullOrEmpty (alias)) {
				throw new ArgumentNullException ("alias");
			}
			if (_aggregateFunctionDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.AggregateFunctionFieldIsExists, alias));
			}
			if (_dataFieldInfoDictionary.ContainsKey (alias)) {
				throw new LightDataException (string.Format (RE.GroupNameFieldIsExists, alias));
			}
			AggregateFunctionInfo info = new AggregateFunctionInfo (function, alias);
			_aggregateFunctionDictionary.Add (alias, info);

			return this;
		}

		/// <summary>
		/// Having the specified expression.
		/// </summary>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> Having (AggregateHavingExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_having = expression;
			return this;
		}

		/// <summary>
		/// 添加统计够查询条件
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <returns>统计表生成器</returns>
		public AggregateTable<T> HavingWithAnd (AggregateHavingExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_having = AggregateHavingExpression.And (_having, expression);
			return this;
		}

		/// <summary>
		/// 添加统计够查询条件
		/// </summary>
		/// <param name="expression">查询表达式</param>
		/// <returns>统计表生成器</returns>
		public AggregateTable<T> HavingWithOr (AggregateHavingExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_having = AggregateHavingExpression.Or (_having, expression);
			return this;
		}

		/// <summary>
		/// Havings the reset.
		/// </summary>
		/// <returns>The reset.</returns>
		public AggregateTable<T> HavingReset ()
		{
			_having = null;
			return this;
		}

		/// <summary>
		/// catch order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">AggregateTable.</param>
		public AggregateTable<T> OrderByCatch (OrderExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_order = OrderExpression.Catch (_order, expression);
			return this;
		}

		/// <summary>
		/// replace order by expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">AggregateTable.</param>
		public AggregateTable<T> OrderBy (OrderExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_order = expression;
			return this;
		}

		/// <summary>
		/// reset order by expression
		/// </summary>
		/// <returns>AggregateTable.</returns>
		public AggregateTable<T> OrderByReset ()
		{
			_order = null;
			return this;
		}

		/// <summary>
		/// Orders the by random.
		/// </summary>
		/// <returns>The by random.</returns>
		public AggregateTable<T> OrderByRandom ()
		{
			_order = new RandomOrderExpression (DataMapping.GetEntityMapping (typeof(T)));
			return this;
		}

		/// <summary>
		/// reset where expression.
		/// </summary>
		/// <returns>The reset.</returns>
		public AggregateTable<T> WhereReset ()
		{
			_query = null;
			return this;
		}

		/// <summary>
		/// replace where expression
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> Where (QueryExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_query = expression;
			return this;
		}

		/// <summary>
		/// and catch where expression.
		/// </summary>
		/// <returns>LEnumerable.</returns>
		/// <param name="expression">Expression.</param>
		public AggregateTable<T> WhereWithAnd (QueryExpression expression)
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
		public AggregateTable<T> WhereWithOr (QueryExpression expression)
		{
//			if (expression == null)
//				throw new ArgumentNullException ("expression");
			_query = QueryExpression.Or (_query, expression);
			return this;
		}

		/// <summary>
		/// 生成命令
		/// </summary>
		/// <returns>命令接口</returns>
		public IDbCommand GetDbCommand ()
		{
			List<DataFieldInfo> fields = new List<DataFieldInfo> (_dataFieldInfoDictionary.Values);
			List<AggregateFunctionInfo> functions = new List<AggregateFunctionInfo> (_aggregateFunctionDictionary.Values);
			CommandData commandData = _context.DataBase.Factory.CreateDynamicAggregateCommand (_enetityMapping, fields, functions, _query, _having, _order);
			return commandData.CreateCommand (_context.DataBase);
		}

		/// <summary>
		/// 输出查询命令语句
		/// </summary>
		/// <returns>查询命令语句</returns>
		public override string ToString ()
		{
			return GetDbCommand ().CommandText;
		}

	}
}
