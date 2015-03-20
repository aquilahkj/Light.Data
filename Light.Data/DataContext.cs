using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;
using Light.Data.Mappings;
using Light.Data.Handler;

namespace Light.Data
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DataContext
    {
        [ThreadStatic]
        static DataContext _currentInstance = null;

        internal static Assembly CallingAssembly
        {
            get
            {
                return _assembly;
            }
        }

        /// <summary>
        /// 设置程序集配置
        /// </summary>
        public static void SetAssemblyConfig()
        {
            _assembly = Assembly.GetCallingAssembly();
        }

        [ThreadStatic]
        static Assembly _assembly = null;

        /// <summary>
        /// 当前数据连接上下文
        /// </summary>
        public static DataContext Current
        {
            get
            {
                return DataContext._currentInstance;
            }
            set
            {
                DataContext._currentInstance = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">配置文件中连接字符串的索引</param>
        public static DataContext Create(int index)
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[index];
            return Create(setting, true);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStringName">配置文件中连接字符串的名称</param>
        public static DataContext Create(string connectionStringName)
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[connectionStringName];
            return Create(setting, true);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">连接名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerName">提供类型名称</param>
        public static DataContext Create(string name, string connectionString, string providerName)
        {
            ConnectionStringSettings setting = new ConnectionStringSettings(name, connectionString, providerName);
            return Create(setting, true);
        }

        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <param name="setting">数据连接设置</param>
        /// <returns>数据上下文</returns>
        public static DataContext Create(ConnectionStringSettings setting)
        {
            return Create(setting, true);
        }


        internal static DataContext Create(ConnectionStringSettings setting, bool throwOnError)
        {
            if (setting == null)
            {
                throw new ArgumentException(RE.ConnectionSettingIsNotExists);
            }
            Type type = null;
            string connection = setting.ConnectionString;
            string args = null;
            int index = connection.IndexOf("--extendparam:");
            if (index > 1)
            {
                args = connection.Substring(index + 15);
                connection = connection.Substring(0, index).Trim();
            }
            if (!string.IsNullOrEmpty(setting.ProviderName))
            {
                type = System.Type.GetType(setting.ProviderName, throwOnError);
            }
            else
            {
                type = System.Type.GetType("Light.Data.Mssql,Light.Data", throwOnError);
            }
            if (type == null)
            {
                return null;
            }

            if (!throwOnError)
            {
                Type dataBaseType = typeof(Database);
                if (!TypeHelper.IsParentType(type, dataBaseType))
                {
                    return null;
                }
            }

            Database _dataBase = Activator.CreateInstance(type) as Database;
            if (_dataBase == null)
            {
                if (!throwOnError)
                {
                    return null;
                }
                else
                {
                    throw new LightDataException(string.Format(RE.TypeIsNotDatabase, type.FullName));
                }
            }
            _dataBase.SetExtentArguments(args);
            DataContext context = new DataContext(setting.ConnectionString, setting.Name, _dataBase);
            return context;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string _connectionString = null;

        /// <summary>
        /// 数据库对象
        /// </summary>
        internal Database _dataBase = null;

        /// <summary>
        /// 数据库配置名称
        /// </summary>
        protected string _configName = null;

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        internal Database DataBase
        {
            get
            {
                return _dataBase;
            }
        }

        /// <summary>
        /// 数据库连接配置名称
        /// </summary>
        public string ConfigName
        {
            get
            {
                return _configName;
            }
        }

        /// <summary>
        /// 设置和获取命令超时时间
        /// </summary>
        public int TimeOut
        {
            get
            {
                return _dataBase.CommandTimeOut;
            }
            set
            {
                _dataBase.CommandTimeOut = value;
            }
        }

        /// <summary>
        /// 获取是否已打开内分页功能
        /// </summary>
        public bool IsInnerPager
        {
            get
            {
                return _dataBase.InnerPager;
            }
        }

        /// <summary>
        /// 设置是否打开内分页功能
        /// </summary>
        /// <param name="enable">打开/关闭</param>
        /// <returns>设置是否成功,如数据库不支持内分页,则打开操作返回false</returns>
        public bool SetInnerPager(bool enable)
        {
            _dataBase.InnerPager = enable;
            return _dataBase.InnerPager == enable;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="configName">配置名称</param>
        /// <param name="dataBase">数据库实例</param>
        internal DataContext(string connectionString, string configName, Database dataBase)
        {
            _connectionString = connectionString;
            _configName = configName;
            _dataBase = dataBase;
        }

        /// <summary>
        /// 获取该类的对应关联表名 
        /// </summary>
        /// <param name="type">类名</param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            DataEntityMapping mapping = DataMapping.GetEntityMapping(type);
            if (mapping != null)
            {
                return mapping.TableName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取该类的对应关联表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>表名</returns>
        public string GetTableName<T>()
             where T : class, new()
        {
            return GetTableName(typeof(T));
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns>受影响行数</returns>
        public int Insert(object data)
        {
            DataTableEntityMapping mapping = DataMapping.GetTableMapping(data.GetType());
            object obj;
            int rInt = 0;

            using (IDbCommand command = _dataBase.Factory.CreateInsertCommand(data))
            {
                IDbCommand identityCommand = _dataBase.Factory.CreateIdentityCommand(mapping);
                obj = ExecuteInsertCommand(command, identityCommand, SafeLevel.Default);
                if (identityCommand != null)
                {
                    identityCommand.Dispose();
                }
            }
            rInt = 1;
            if (mapping.IdentityField != null && !Object.Equals(obj, null))
            {
                object id = Convert.ChangeType(obj, mapping.IdentityField.ObjectType);
                mapping.IdentityField.Handler.Set(data, id);
            }
            return rInt;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns>受影响行数</returns>
        public int Update(object data)
        {
            DataTableEntity entity = data as DataTableEntity;
            if (entity != null)
            {
                return Update(data, entity.GetUpdateFields());
            }
            else
            {
                return Update(data, null);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="updateFields">需更新字段</param>
        /// <returns>受影响行数</returns>
        public int Update(object data, string[] updateFields)
        {
            int rInt = 0;
            using (IDbCommand command = _dataBase.Factory.CreateUpdateCommand(data, updateFields))
            {
                rInt = ExecuteNonQuery(command, SafeLevel.Default);
            }
            return rInt;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns>受影响行数</returns>
        public int Delete(object data)
        {
            int rInt = 0;
            using (IDbCommand command = _dataBase.Factory.CreateDeleteCommand(data))
            {
                rInt = ExecuteNonQuery(command, SafeLevel.Default);
            }
            return rInt;
        }

        /// <summary>
        /// 创建新对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>数据对象</returns>
        public T CreateNew<T>()
            where T : class, new()
        {
            DataTableEntityMapping rawmapping = DataMapping.GetTableMapping(typeof(T));
            T obj = rawmapping.InitialData() as T;
            if (rawmapping.IsDataEntity)
            {
                DataEntity data = obj as DataEntity;
                data.SetContext(this);
            }
            return obj;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <typeparam name="T">删除对象类型</typeparam>
        /// <param name="expression">查询表达式</param>
        /// <returns>受影响行数</returns>
        public int DeleteMass<T>(QueryExpression expression)
            where T : class, new()
        {
            return DeleteMass(typeof(T), expression);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <typeparam name="T">删除对象类型</typeparam>
        /// <returns>受影响行数</returns>
        public int DeleteMass<T>()
            where T : class, new()
        {
            return DeleteMass<T>(null);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="type">删除对象类型</param>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        internal int DeleteMass(Type type, QueryExpression expression)
        {
            DataTableEntityMapping mapping = DataMapping.GetTableMapping(type);
            return DeleteMass(mapping, expression);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="mapping">表映射</param>
        /// <param name="expression">表达式</param>
        /// <returns>受影响行数</returns>
        internal int DeleteMass(DataTableEntityMapping mapping, QueryExpression expression)
        {
            int rInt = 0;
            using (IDbCommand command = _dataBase.Factory.CreateDeleteMassCommand(mapping, expression))
            {
                rInt = ExecuteNonQuery(command, SafeLevel.Default);
            }
            return rInt;
        }


        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <typeparam name="T">更新对象类型</typeparam>
        /// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
        /// <param name="expression">查询表达式</param>
        /// <returns>受影响行数</returns>
        public int UpdateMass<T>(UpdateSetValue[] updates, QueryExpression expression)
            where T : class, new()
        {
            return UpdateMass(typeof(T), updates, expression);
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <typeparam name="T">更新对象类型</typeparam>
        /// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
        /// <returns>受影响行数</returns>
        public int UpdateMass<T>(UpdateSetValue[] updates)
            where T : class, new()
        {
            return UpdateMass<T>(updates, null);
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="type">更新对象类型</param>
        /// <param name="updates">更新对象类型</param>
        /// <param name="expression">查询表达式</param>
        /// <returns>受影响行数</returns>
        internal int UpdateMass(Type type, UpdateSetValue[] updates, QueryExpression expression)
        {
            DataTableEntityMapping mapping = DataMapping.GetTableMapping(type);
            return UpdateMass(mapping, updates, expression);
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="mapping">表映射</param>
        /// <param name="updates">更新对象类型</param>
        /// <param name="expression">查询表达式</param>
        /// <returns>受影响行数</returns>
        internal int UpdateMass(DataTableEntityMapping mapping, UpdateSetValue[] updates, QueryExpression expression)
        {
            int rInt = 0;
            using (IDbCommand command = _dataBase.Factory.CreateUpdateMassCommand(mapping, updates, expression))
            {
                rInt = ExecuteNonQuery(command, SafeLevel.Default);
            }
            return rInt;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="datas">数据数组</param>
        /// <returns>返回处理行数</returns>
        public int BulkInsert(Array datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas");
            }
            if (datas.Length == 0)
            {
                return 0;
            }
            int batchCount = 10;
            IDbCommand[] dbcommands = _dataBase.Factory.CreateBulkInsertCommand(datas, batchCount);
            int result = ExecuteMultiCommands(dbcommands, SafeLevel.Default);
            foreach (IDbCommand command in dbcommands)
            {
                command.Dispose();
            }
			return result;
        }

        /// <summary>
        /// 从主键获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="primaryKeys">主键数组</param>
        /// <returns>数据对象</returns>
        public T SelectSingleFromKey<T>(params object[] primaryKeys)
            where T : class, new()
        {
            DataTableEntityMapping dtmapping = DataMapping.GetTableMapping(typeof(T));
            if (primaryKeys.Length != dtmapping.PrimaryKeyFields.Length)
            {
                throw new LightDataException(RE.TheNumberOfPrimaryKeysIsNotMatch);
            }
            DataFieldInfo[] primaryKeyInfos = new DataFieldInfo[primaryKeys.Length];
            QueryExpression query = null;
            for (int i = 0; i < primaryKeys.Length; i++)
            {
                primaryKeyInfos[i] = new DataFieldInfo(dtmapping.PrimaryKeyFields[i]);
                if (i == 0)
                {
                    query = primaryKeyInfos[i] == primaryKeys[i];
                }
                else
                {
                    query = query & primaryKeyInfos[i] == primaryKeys[i];
                }
            }
            return SelectSingle(dtmapping, query, null, 0, SafeLevel.None) as T;
        }

        /// <summary>
        /// 从自增ID获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">自增ID</param>
        /// <returns>数据对象</returns>
        public T SelectSingleFromId<T>(int id)
            where T : class, new()
        {
            return SelectSingleFromIdObj<T>(id);
        }
        /// <summary>
        /// 从自增ID获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">自增ID</param>
        /// <returns>数据对象</returns>
        public T SelectSingleFromId<T>(uint id)
            where T : class, new()
        {
            return SelectSingleFromIdObj<T>(id);
        }
        /// <summary>
        /// 从自增ID获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">自增ID</param>
        /// <returns>数据对象</returns>
        public T SelectSingleFromId<T>(long id)
            where T : class, new()
        {
            return SelectSingleFromIdObj<T>(id);
        }
        /// <summary>
        /// 从自增ID获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">自增ID</param>
        /// <returns>数据对象</returns>
        public T SelectSingleFromId<T>(ulong id)
            where T : class, new()
        {
            return SelectSingleFromIdObj<T>(id);
        }

        /// <summary>
        /// 从自增ID获取数据对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">自增</param>
        /// <returns>数据对象</returns>
        private T SelectSingleFromIdObj<T>(object id)
            where T : class, new()
        {
            DataTableEntityMapping dtmapping = DataMapping.GetTableMapping(typeof(T));
            if (dtmapping.IdentityField == null)
            {
                throw new LightDataException(RE.DataTableNotIdentityField);
            }
            DataFieldInfo idfield = new DataFieldInfo(dtmapping.IdentityField);
            QueryExpression query = idfield == id;
            return SelectSingle(dtmapping, query, null, 0, SafeLevel.None) as T;
        }

        /// <summary>
        /// 生成数据查询对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>数据查询对象</returns>
        public LEnumerable<T> LQuery<T>()
            where T : class, new()
        {
            return new LEnumerable<T>(this);
        }

        /// <summary>
        /// 生成数据统计对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>数据查询对象</returns>
        public AggregateTable<T> LAggregate<T>()
            where T : class, new()
        {
            return new AggregateTable<T>(this);
        }

        /// <summary>
        /// 生成数据查询枚举
        /// </summary>
        /// <param name="mapping">数据映射</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据枚举</returns>
        internal IEnumerable QueryDataEnumerable(DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
        {
            IDbCommand command = _dataBase.Factory.CreateSelectCommand(mapping, query, order, IsInnerPager ? region : null);
            return QueryDataReader(mapping, command, !IsInnerPager ? region : null, level);
        }

        /// <summary>
        /// 生成数据查询枚举
        /// </summary>
        /// <param name="mapping">数据映射</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据集合</returns>
        internal IList QueryDataList(DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateSelectCommand(mapping, query, order, IsInnerPager ? region : null))
            {
                IList items = CreateList(mapping.ObjectType);
                IEnumerable ie = QueryDataReader(mapping, command, !IsInnerPager ? region : null, level);
                foreach (object obj in ie)
                {
                    items.Add(obj);
                }
                return items;
            }
        }

        /// <summary>
        /// 查询单列数据
        /// </summary>
        /// <param name="fieldInfo">单列数据字段</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="distinct">是否排除重复</param>
        /// <param name="level">安全级别</param>
        /// <returns>单列数据枚举</returns>
        internal IEnumerable QueryColumeEnumerable(DataFieldInfo fieldInfo, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
        {
            IDbCommand command = _dataBase.Factory.CreateSelectSingleFieldCommand(fieldInfo, query, order, distinct, null);
            DataDefine define = TransferDataDefine(fieldInfo.DataField);
            return QueryDataReader(define, command, region, level);
        }

        /// <summary>
        /// 查询单列数据
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <param name="outputType">输出类型</param>
        /// <param name="isNullable">是否可空</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="distinct">是否排除重复</param>
        /// <param name="level">安全级别</param>
        /// <returns>单列数据枚举</returns>
        internal IEnumerable QueryColumeEnumerable(DataFieldInfo fieldInfo, Type outputType, bool isNullable, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
        {
            IDbCommand command = _dataBase.Factory.CreateSelectSingleFieldCommand(fieldInfo, query, order, distinct, null);
            DataDefine define = TransferDataDefine(outputType, null, isNullable);
            return QueryDataReader(define, command, region, level);
        }


        /// <summary>
        /// 查询单列数据
        /// </summary>
        /// <param name="fieldInfo">单列数据字段</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="distinct">是否排除重复</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据集合</returns>
        internal IList QueryColumeList(DataFieldInfo fieldInfo, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateSelectSingleFieldCommand(fieldInfo, query, order, distinct, null))
            {
                DataDefine define = TransferDataDefine(fieldInfo.DataField);
                IList items = CreateList(define.ObjectType);

                IEnumerable ie = QueryDataReader(define, command, region, level);
                if (define.IsNullable)
                {
                    MethodInfo addMethod = items.GetType().GetMethod("Add");
                    foreach (object obj in ie)
                    {
                        if (Object.Equals(obj, null))
                        {
                            addMethod.Invoke(items, new object[] { null });
                        }
                        else
                        {
                            items.Add(obj);
                        }
                    }
                }
                else
                {
                    foreach (object obj in ie)
                    {
                        items.Add(obj);
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 查询单列数据
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <param name="outputType">输出类型</param>
        /// <param name="isNullable">是否可空</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="region">查询范围</param>
        /// <param name="distinct">是否排除重复</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据集合</returns>
        internal IList QueryColumeList(DataFieldInfo fieldInfo, Type outputType, bool isNullable, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateSelectSingleFieldCommand(fieldInfo, query, order, distinct, null))
            {
                DataDefine define = TransferDataDefine(outputType, null, isNullable);
                IList items = CreateList(define.ObjectType);

                IEnumerable ie = QueryDataReader(define, command, region, level);
                if (define.IsNullable)
                {
                    MethodInfo addMethod = items.GetType().GetMethod("Add");
                    foreach (object obj in ie)
                    {
                        if (Object.Equals(obj, null))
                        {
                            addMethod.Invoke(items, new object[] { null });
                        }
                        else
                        {
                            items.Add(obj);
                        }
                    }
                }
                else
                {
                    foreach (object obj in ie)
                    {
                        items.Add(obj);
                    }
                }
                return items;
            }
        }


        /// <summary>
        /// 动态统计数据到数据表中
        /// </summary>
        /// <param name="mapping">数据映射</param>
        /// <param name="dataFieldInfoDictionary">统计字段信息</param>
        /// <param name="aggregateFunctionDictionary">统计方法信息</param>
        /// <param name="query">查询表达式</param>
        /// <param name="having">统计查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据表</returns>
        internal DataTable QueryDynamicAggregateTable(DataEntityMapping mapping, Dictionary<string, DataFieldInfo> dataFieldInfoDictionary, Dictionary<string, AggregateFunction> aggregateFunctionDictionary, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateDynamicAggregateCommand(mapping, dataFieldInfoDictionary, aggregateFunctionDictionary, query, having, order))
            {
                return QueryDataTable(command, null, level);
            }
        }

        /// <summary>
        /// 动态统计数据到数据集合中
        /// </summary>
        /// <param name="mapping">数据映射</param>
        /// <param name="amapping">统计结果类型</param>
        /// <param name="dataFieldInfoDictionary">统计字段信息</param>
        /// <param name="aggregateFunctionDictionary">统计方法信息</param>
        /// <param name="query">查询表达式</param>
        /// <param name="having">统计查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据集合</returns>
        internal IList QueryDynamicAggregateList(DataEntityMapping mapping, AggregateTableMapping amapping, Dictionary<string, DataFieldInfo> dataFieldInfoDictionary, Dictionary<string, AggregateFunction> aggregateFunctionDictionary, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
        {
            if (amapping.RelateType != null && amapping.RelateType != mapping.ObjectType)
            {
                throw new LightDataException(string.Format(RE.AggregateTypeIsNotSpecifyType, amapping.RelateType.FullName));
            }
            using (IDbCommand command = _dataBase.Factory.CreateDynamicAggregateCommand(mapping, dataFieldInfoDictionary, aggregateFunctionDictionary, query, having, order))
            {
                IList items = CreateList(amapping.ObjectType);
                IEnumerable ie = QueryDataReader(amapping, command, null, level);
                foreach (object obj in ie)
                {
                    items.Add(obj);
                }
                return items;
            }
        }

        /// <summary>
        /// 获取查询单个数据
        /// </summary>
        /// <param name="mapping">数据对象映射表</param>
        /// <param name="query">查询表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="index">数据索引</param>
        /// <param name="level">安全级别</param>
        /// <returns>数据对象</returns>
        internal object SelectSingle(DataEntityMapping mapping, QueryExpression query, OrderExpression order, int index, SafeLevel level)
        {
            object target = null;
            Region region = new Region(index, 1);
            using (IDbCommand command = _dataBase.Factory.CreateSelectCommand(mapping, query, order, IsInnerPager ? region : null))
            {
                //target = LExecuteReaderSingle(mapping, command, index, level);
                foreach (object obj in QueryDataReader(mapping, command, region, level))
                {
                    target = obj;
                }
            }
            return target;
        }

        /// <summary>
        /// 统计行数
        /// </summary>
        /// <param name="mapping">数据映射表</param>
        /// <param name="query">查询表达式</param>
        /// <param name="level"></param>
        /// <returns>查询行数</returns>
        internal object AggregateCount(DataEntityMapping mapping, QueryExpression query, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateAggregateCountCommand(mapping, query))
            {
                return ExecuteScalar(command, level);
            }
        }

        /// <summary>
        /// 统计字段函数
        /// </summary>
        /// <param name="fieldMapping">统计字段</param>
        /// <param name="aggregateType">统计方式</param>
        /// <param name="query">查询表达式</param>
        /// <param name="distinct">是否排除重复</param>
        /// <param name="level">安全级别</param>
        /// <returns>统计结果</returns>
        internal object Aggregate(DataFieldMapping fieldMapping, AggregateType aggregateType, QueryExpression query, bool distinct, SafeLevel level)
        {
            using (IDbCommand command = _dataBase.Factory.CreateAggregateCommand(fieldMapping, aggregateType, query, distinct))
            {
                object obj = ExecuteScalar(command, level);
                if (Object.Equals(obj, DBNull.Value))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
        }

        /// <summary>
        /// 查询数据是否存在
        /// </summary>
        /// <param name="mapping">数据映射表</param>
        /// <param name="query">查询表达式</param>
        /// <param name="level">安全级别</param>
        /// <returns>结果</returns>
        internal bool Exists(DataEntityMapping mapping, QueryExpression query, SafeLevel level)
        {
            bool exists = false;
            //Region region = new Region(0, 1);
            using (IDbCommand command = _dataBase.Factory.CreateExistsCommand(mapping, query))
            {
                PrimitiveDataDefine pm = PrimitiveDataDefine.Create(typeof(Int32), 0);
                foreach (object obj in QueryDataReader(pm, command, null, level))
                {
                    exists = true;
                }
            }
            return exists;
        }


        /// <summary>
        /// DataTable读取
        /// </summary>
        /// <param name="dbcommand">数据类型</param>
        /// <param name="region">查询范围</param>
        /// <param name="level">安全级别</param>
        /// <returns>DataTable对象</returns>
        internal DataTable QueryDataTable(IDbCommand dbcommand, Region region, SafeLevel level)
        {
            DataTable dt = QueryDataSet(dbcommand, level).Tables[0];
            if (region == null)
            {
                return dt;
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataRowCollection drs = dt.Rows;
                int start = region.Start;
                int size = region.Size;
                for (int i = start; i < size; i++)
                {
                    dt1.Rows.Add(drs[i]);
                }
                return dt1;
            }
        }


        #region 核心数据库方法

        internal virtual int ExecuteMultiCommands(IDbCommand[] dbcommands, SafeLevel level)
        {
            if (level == SafeLevel.None)
            {
                level = SafeLevel.Default;
            }
            int rInt = 0;
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                try
                {
                    foreach (IDbCommand dbcommand in dbcommands)
                    {
                        transaction.SetupCommand(dbcommand);
                        rInt += dbcommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return rInt;
        }

        internal virtual object ExecuteInsertCommand(IDbCommand dbcommand, IDbCommand indentityCommand, SafeLevel level)
        {
            object result = null;
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                try
                {
                    transaction.SetupCommand(dbcommand);
                    dbcommand.ExecuteNonQuery();
                    if (indentityCommand != null)
                    {
                        transaction.SetupCommand(indentityCommand);
                        object obj = indentityCommand.ExecuteScalar();
                        if (obj != null)
                        {
                            result = obj;
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        internal virtual int ExecuteNonQuery(IDbCommand dbcommand, SafeLevel level)
        {
            int rInt = 0;
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                try
                {
                    transaction.SetupCommand(dbcommand);
                    rInt = dbcommand.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return rInt;
        }

        internal virtual object ExecuteScalar(IDbCommand dbcommand, SafeLevel level)
        {
            object result = null;
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                try
                {
                    transaction.SetupCommand(dbcommand);
                    result = dbcommand.ExecuteScalar();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return result;
        }

        internal virtual DataSet QueryDataSet(IDbCommand dbcommand, SafeLevel level)
        {
            DataSet ds = new DataSet();
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                try
                {
                    transaction.SetupCommand(dbcommand);
                    IDbDataAdapter adapter = _dataBase.CreateDataAdapter(dbcommand);
                    adapter.Fill(ds);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return ds;
        }

        internal virtual IEnumerable QueryDataReader(IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level)
        {
            int start;
            int size;
            if (region != null)
            {
                start = region.Start;
                size = region.Size;
            }
            else
            {
                start = 0;
                size = int.MaxValue;
            }
            using (TransactionConnection transaction = CreateTransactionConnection(level))
            {
                transaction.Open();
                transaction.SetupCommand(dbcommand);
                using (IDataReader reader = dbcommand.ExecuteReader())
                {
                    int index = 0;
                    int count = 0;
                    bool over = false;
                    while (reader.Read())
                    {
                        if (over)
                        {
                            dbcommand.Cancel();
                            break;
                        }
                        if (index >= start)
                        {
                            count++;
                            object item = source.LoadData(this, reader);
                            if (count >= size)
                            {
                                over = true;
                            }
                            yield return item;
                        }
                        index++;
                    }
                }
                transaction.Commit();
            }
        }

        #endregion

        #region 基础方法

        internal TransactionConnection CreateTransactionConnection(SafeLevel level)
        {
            IDbConnection connection = _dataBase.CreateConnection(_connectionString);
            return new TransactionConnection(connection, level);
        }

        /// <summary>
        /// 创建事务型数据上下文
        /// </summary>
        /// <returns>事务型数据上下文</returns>
        public TransDataContext CreateTransDataContext()
        {
            TransDataContext context = new TransDataContext(_connectionString, _configName, _dataBase);
            return context;
        }

        #endregion

        #region SQL执行器
        /// <summary>
        /// 创建SQL字符串执行器
        /// </summary>
        /// <param name="sqlString">SQL字符串</param>
        /// <param name="param">参数数组</param>
        /// <param name="level">安全等级</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateSqlStringExecutor(string sqlString, DataParameter[] param, SafeLevel level)
        {
            SqlExecutor executor = new SqlExecutor(sqlString, param, CommandType.Text, level, this);
            return executor;
        }

        /// <summary>
        /// 创建SQL字符串执行器
        /// </summary>
        /// <param name="sqlString">SQL字符串</param>
        /// <param name="param">参数数组</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateSqlStringExecutor(string sqlString, DataParameter[] param)
        {
            return CreateSqlStringExecutor(sqlString, param, SafeLevel.Default);
        }

        /// <summary>
        /// 创建SQL字符串执行器
        /// </summary>
        /// <param name="sqlString">SQL字符串</param>
        /// <param name="level">安全等级</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateSqlStringExecutor(string sqlString, SafeLevel level)
        {
            return CreateSqlStringExecutor(sqlString, null, level);
        }

        /// <summary>
        /// 创建SQL字符串执行器
        /// </summary>
        /// <param name="sqlString">SQL字符串</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateSqlStringExecutor(string sqlString)
        {
            return CreateSqlStringExecutor(sqlString, null, SafeLevel.Default);
        }


        /// <summary>
        /// 创建SQL存储过程执行器
        /// </summary>
        /// <param name="storeProcedure">SQL存储过程</param>
        /// <param name="param">参数数组</param>
        /// <param name="level">安全等级</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateStoreProcedureExecutor(string storeProcedure, DataParameter[] param, SafeLevel level)
        {
            SqlExecutor executor = new SqlExecutor(storeProcedure, param, CommandType.StoredProcedure, level, this);
            return executor;
        }


        /// <summary>
        /// 创建存储过程执行器
        /// </summary>
        /// <param name="storeProcedure">SQL存储过程</param>
        /// <param name="param">参数数组</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateStoreProcedureExecutor(string storeProcedure, DataParameter[] param)
        {
            return CreateStoreProcedureExecutor(storeProcedure, param, SafeLevel.Default);
        }

        /// <summary>
        /// 创建存储过程执行器
        /// </summary>
        /// <param name="storeProcedure">SQL存储过程</param>
        /// <param name="level">安全等级</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateStoreProcedureExecutor(string storeProcedure, SafeLevel level)
        {
            return CreateStoreProcedureExecutor(storeProcedure, null, level);
        }

        /// <summary>
        /// 创建存储过程执行器
        /// </summary>
        /// <param name="storeProcedure">SQL存储过程</param>
        /// <returns>SQL执行器</returns>
        public SqlExecutor CreateStoreProcedureExecutor(string storeProcedure)
        {
            return CreateStoreProcedureExecutor(storeProcedure, null, SafeLevel.Default);
        }

        #endregion

        #region 静态函数

        private static DataDefine TransferDataDefine(Type dataType, string name, bool isNullable)
        {
            DataDefine define = null;
            if (dataType == typeof(string))
            {
                define = PrimitiveDataDefine.CreateString(isNullable, name);
            }
            else
            {
                if (isNullable)
                {
                    Type itemstype = System.Type.GetType("System.Nullable`1");
                    Type type = itemstype.MakeGenericType(dataType);
                    define = PrimitiveDataDefine.Create(type, name);
                }
                else
                {
                    define = PrimitiveDataDefine.Create(dataType, name);
                }
            }
            return define;
        }

        private static DataDefine TransferDataDefine(DataFieldMapping fieldMapping)
        {
            DataDefine define = null;
            if (fieldMapping is PrimitiveFieldMapping)
            {
                if (fieldMapping.ObjectType == typeof(string))
                {
                    define = PrimitiveDataDefine.CreateString(fieldMapping.IsNullable, fieldMapping.Name);
                }
                else
                {
                    if (fieldMapping.IsNullable)
                    {
                        Type itemstype = System.Type.GetType("System.Nullable`1");
                        Type type = itemstype.MakeGenericType(fieldMapping.ObjectType);
                        define = PrimitiveDataDefine.Create(type, fieldMapping.Name);
                    }
                    else
                    {
                        define = PrimitiveDataDefine.Create(fieldMapping.ObjectType, fieldMapping.Name);
                    }
                }
            }
            else
                if (fieldMapping is EnumFieldMapping)
                {
                    EnumFieldMapping em = fieldMapping as EnumFieldMapping;
                    if (fieldMapping.IsNullable)
                    {
                        Type itemstype = System.Type.GetType("System.Nullable`1");
                        Type type = itemstype.MakeGenericType(fieldMapping.ObjectType);
                        define = EnumDataDefine.Create(type, em.EnumType, fieldMapping.Name);
                    }
                    else
                    {
                        define = EnumDataDefine.Create(fieldMapping.ObjectType, em.EnumType, fieldMapping.Name);
                    }
                }
                else
                {
                    throw new LightDataException(RE.OnlyPrimitiveFieldCanSelectSingle);
                }
            return define;
        }

        private static IList CreateList(Type type)
        {
            IList items = null;
            Type itemstype = System.Type.GetType("System.Collections.Generic.List`1");
            itemstype = itemstype.MakeGenericType(type);
            items = (IList)Activator.CreateInstance(itemstype);
            return items;
        }

        #endregion
    }
}