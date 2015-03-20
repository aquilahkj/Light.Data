using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
    /// <summary>
    /// 统计表生成器
    /// </summary>
    /// <typeparam name="T">要统计的表类型</typeparam>
    public class AggregateTable<T> where T : class, new()
    {
        DataEntityMapping _enetityMapping = null;

        DataContext _context = null;

        QueryExpression _query = null;

        AggregateHavingExpression _having = null;

        OrderExpression _order = null;

        SafeLevel _level = SafeLevel.Default;

        Dictionary<string, DataFieldInfo> _dataFieldInfoDictionary = new Dictionary<string, DataFieldInfo>();

        Dictionary<string, AggregateFunction> _aggregateFunctionDictionary = new Dictionary<string, AggregateFunction>();

        internal AggregateTable(DataContext dataContext)
        {
            _context = dataContext;
            _enetityMapping = DataMapping.GetEntityMapping(typeof(T));
        }

        /// <summary>
        /// 重置条件语句
        /// </summary>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> Reset()
        {
            _query = null;
            _order = null;
            _having = null;
            _level = SafeLevel.Default;
            return this;
        }

        /// <summary>
        /// 生成统计表的DataTable
        /// </summary>
        /// <returns>统计表生成器</returns>
        public DataTable GetDataTable()
        {
            return _context.QueryDynamicAggregateTable(_enetityMapping, _dataFieldInfoDictionary, _aggregateFunctionDictionary, _query, _having, _order, _level);
        }

        /// <summary>
        /// 生成统计表的类型集合
        /// </summary>
        /// <typeparam name="K">生成类型</typeparam>
        /// <returns>类型集合</returns>
        public List<K> GetObjectList<K>() where K : class, new()
        {
            AggregateTableMapping aggregateMapping = AggregateTableMapping.GetAggregateMapping(typeof(K));
            List<K> list = _context.QueryDynamicAggregateList(_enetityMapping, aggregateMapping, _dataFieldInfoDictionary, _aggregateFunctionDictionary, _query, _having, _order, _level) as List<K>;
            return list;
        }

        /// <summary>
        /// 加入分组字段
        /// </summary>
        /// <param name="fieldInfo">分组字段</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> GroupBy(DataFieldInfo fieldInfo)
        {
            GroupBy(fieldInfo, null);
            return this;
        }

        /// <summary>
        /// 添加分组字段
        /// </summary>
        /// <param name="fieldInfo">分组字段</param>
        /// <param name="alias">查询别名,与字段配置字段对应</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> GroupBy(DataFieldInfo fieldInfo, string alias)
        {
            if (Object.Equals(fieldInfo, null))
            {
                throw new ArgumentNullException("DataFieldInfo");
            }
            if (string.IsNullOrEmpty(alias))
            {
                alias = fieldInfo.FieldName;
            }
            if (_dataFieldInfoDictionary.ContainsKey(alias))
            {
                throw new LightDataException(string.Format(RE.GroupNameFieldIsExists, alias));
            }
            if (_aggregateFunctionDictionary.ContainsKey(alias))
            {
                throw new LightDataException(string.Format(RE.AggregateFunctionFieldIsExists, alias));
            }
            _dataFieldInfoDictionary.Add(alias, fieldInfo);

            return this;
        }

        /// <summary>
        /// 添加聚合方法
        /// </summary>
        /// <param name="function">聚合函数</param>
        /// <param name="alias">查询别名,与字段配置字段对应</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> Aggregate(AggregateFunction function, string alias)
        {
            if (Object.Equals(function, null))
            {
                throw new ArgumentNullException("AggregateFunction");
            }
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentNullException("alias");
            }
            if (_aggregateFunctionDictionary.ContainsKey(alias))
            {
                throw new LightDataException(string.Format(RE.AggregateFunctionFieldIsExists, alias));
            }
            if (_dataFieldInfoDictionary.ContainsKey(alias))
            {
                throw new LightDataException(string.Format(RE.GroupNameFieldIsExists, alias));
            }

            //if (function.TableMapping == null)
            //{
            //    function.TableMapping = _enetityMapping;
            //}
            //function.CName = alias;
            _aggregateFunctionDictionary.Add(alias, function);

            return this;
        }

        /// <summary>
        /// 添加统计够查询条件
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> Having(AggregateHavingExpression expression)
        {
            _having &= expression;
            return this;
        }

        /// <summary>
        /// 添加排序表达式
        /// </summary>
        /// <param name="expression">排序表达式</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> OrderBy(OrderExpression expression)
        {
            _order &= expression;
            return this;
        }

        /// <summary>
        /// 添加查询表达式
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>统计表生成器</returns>
        public AggregateTable<T> Where(QueryExpression expression)
        {
            _query &= expression;
            return this;
        }

        /// <summary>
        /// 生成命令
        /// </summary>
        /// <returns>命令接口</returns>
        public IDbCommand GetDbCommand()
        {
            return _context.DataBase.Factory.CreateDynamicAggregateCommand(_enetityMapping, _dataFieldInfoDictionary, _aggregateFunctionDictionary, _query, _having, _order);
        }

        /// <summary>
        /// 输出查询命令语句
        /// </summary>
        /// <returns>查询命令语句</returns>
        public override string ToString()
        {
            return GetDbCommand().CommandText;
        }

    }
}
