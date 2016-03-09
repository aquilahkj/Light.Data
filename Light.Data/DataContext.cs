using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// 数据库上下文
	/// </summary>
	public class DataContext
	{
		static Dictionary<string,DataContextSetting> Settings = new Dictionary<string, DataContextSetting> ();

		static DataContextSetting DefaultContextSetting;

		//		static DataContext DefaultContext;

		/// <summary>
		/// Gets the default.
		/// </summary>
		/// <value>The default.</value>
		public static DataContext Default {
			get {
//				if (DefaultContext == null) {
//					throw new LightDataException (RE.DefaultConnectionNotExists);
//				}
//				else {
//					return DefaultContext;
//				}
				return CreateDefault ();
			}
		}

		static DataContext ()
		{
			if (ConfigurationManager.ConnectionStrings != null) {
				foreach (ConnectionStringSettings connectionSetting in ConfigurationManager.ConnectionStrings) {
					DataContextSetting contextSetting = DataContextSetting.CreateSetting (connectionSetting, false);
					if (contextSetting != null && DefaultContextSetting == null) {
						DefaultContextSetting = contextSetting;
//						DefaultContext = new DataContext (contextSetting.Connection, contextSetting.Name, contextSetting.DataBase);
					}
					Settings [connectionSetting.Name] = contextSetting;
				}
			}
		}

		/// <summary>
		/// Creates the default.
		/// </summary>
		/// <returns>The default.</returns>
		public static DataContext CreateDefault ()
		{
			if (DefaultContextSetting == null) {
				throw new LightDataException (RE.DefaultConnectionNotExists);
			}
			else {
				DataContext context = new DataContext (DefaultContextSetting.Connection, DefaultContextSetting.Name, DefaultContextSetting.DataBase);
				return context;
			}
		}

		/// <summary>
		/// Create the specified configName.
		/// </summary>
		/// <param name="configName">Config name.</param>
		public static DataContext Create (string configName)
		{
			if (configName == null)
				throw new ArgumentNullException ("configName");
			DataContextSetting setting;
			if (Settings.TryGetValue (configName, out setting)) {
				if (setting != null) {
					DataContext context = new DataContext (setting.Connection, setting.Name, setting.DataBase);
					return context;
				}
				else {
					throw new LightDataException (RE.SpecifiedConfigNameConnectionStringError);
				}
			}
			else {
				throw new LightDataException (RE.SpecifiedConfigNameConnectionStringNotExists);
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">连接名称</param>
		/// <param name="connectionString">连接字符串</param>
		/// <param name="providerName">提供类型名称</param>
		public static DataContext Create (string name, string connectionString, string providerName)
		{
			ConnectionStringSettings connectionSetting = new ConnectionStringSettings (name, connectionString, providerName);
			return CreateFromSetting (connectionSetting);
		}

		/// <summary>
		/// 创建数据上下文
		/// </summary>
		/// <param name="setting">数据连接设置</param>
		/// <returns>数据上下文</returns>
		public static DataContext CreateFromSetting (ConnectionStringSettings setting)
		{
			if (setting == null)
				throw new ArgumentNullException ("setting");
			DataContextSetting contextSetting = DataContextSetting.CreateSetting (setting, true);
			DataContext context = new DataContext (contextSetting.Connection, contextSetting.Name, contextSetting.DataBase);
			return context;
		}

		/// <summary>
		/// 连接字符串
		/// </summary>
		protected string _connectionString;

		/// <summary>
		/// 数据库对象
		/// </summary>
		internal Database _dataBase;

		/// <summary>
		/// 数据库配置名称
		/// </summary>
		protected string _configName;

		/// <summary>
		/// 获取数据库对象
		/// </summary>
		internal Database DataBase {
			get {
				return _dataBase;
			}
		}

		/// <summary>
		/// 命令输出接口
		/// </summary>
		protected ICommandOutput output;

		/// <summary>
		/// Sets the commanf output.
		/// </summary>
		/// <param name="output">Output.</param>
		public void SetCommanfOutput (ICommandOutput output)
		{
			this.output = output;
		}

		/// <summary>
		/// 数据库连接配置名称
		/// </summary>
		public string ConfigName {
			get {
				return _configName;
			}
		}

		/// <summary>
		/// 设置和获取命令超时时间
		/// </summary>
		public int TimeOut {
			get {
				return _dataBase.CommandTimeOut;
			}
			set {
				_dataBase.CommandTimeOut = value;
			}
		}

		/// <summary>
		/// 获取是否已打开内分页功能
		/// </summary>
		public bool IsInnerPager {
			get {
				return _dataBase.InnerPager;
			}
		}

		/// <summary>
		/// 设置是否打开内分页功能
		/// </summary>
		/// <param name="enable">打开/关闭</param>
		/// <returns>设置是否成功,如数据库不支持内分页,则打开操作返回false</returns>
		public bool SetInnerPager (bool enable)
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
		internal DataContext (string connectionString, string configName, Database dataBase)
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
		public string GetTableName (Type type)
		{
			DataEntityMapping mapping = DataMapping.GetEntityMapping (type);
			if (mapping != null) {
				return mapping.TableName;
			}
			else {
				return null;
			}
		}

		/// <summary>
		/// 获取该类的对应关联表名
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>表名</returns>
		public string GetTableName<T> ()
             where T : class, new()
		{
			return GetTableName (typeof(T));
		}

		/// <summary>
		/// Insert or update.
		/// </summary>
		/// <returns></returns>
		/// <param name="data">Data.</param>
		public int InsertOrUpdate (object data)
		{
			bool exists = false;
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (data.GetType ());
			CommandData commandData = _dataBase.Factory.CreateEntityExistsCommand (mapping, data);
			Region region = new Region (0, 1);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				PrimitiveDataDefine pm = PrimitiveDataDefine.ParseDefine (typeof(Int32));
				foreach (object obj in QueryDataReader(pm, command, region, SafeLevel.Default,null)) {
					exists = true;
				}
			}
			int result;
			if (exists) {
				result = Update (mapping, data);
			}
			else {
				result = Insert (mapping, data);
			}
			return result;
		}

		/// <summary>
		/// 新增数据
		/// </summary>
		/// <param name="data">数据对象</param>
		/// <returns>受影响行数</returns>
		public int Insert (object data)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (data.GetType ());
			return Insert (mapping, data);
		}

		int Insert (DataTableEntityMapping mapping, object data)
		{
			object obj;
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateInsertCommand (mapping, data);
			CommandData commandDataIdentity = null;
			if (mapping.IdentityField != null) {
				commandDataIdentity = _dataBase.Factory.CreateIdentityCommand (mapping);
			}
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				IDbCommand identityCommand = null;
				if (commandDataIdentity != null) {
					identityCommand = commandDataIdentity.CreateCommand (_dataBase);
				}
				obj = ExecuteInsertCommand (command, identityCommand, SafeLevel.Default);
				if (identityCommand != null) {
					identityCommand.Dispose ();
				}
			}
			rInt = 1;
			if (!Object.Equals (obj, null)) {
				object id = Convert.ChangeType (obj, mapping.IdentityField.ObjectType);
				mapping.IdentityField.Handler.Set (data, id);
			}
			return rInt;
		}

		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="data">数据对象</param>
		/// <returns>受影响行数</returns>
		public int Update (object data)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (data.GetType ());
			DataTableEntity entity = data as DataTableEntity;
			if (entity != null) {
				return Update (mapping, data, entity.GetUpdateFields ());
			}
			else {
				return Update (mapping, data, null);
			}
		}

		internal int Update (object data, string[] updateFields)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (data.GetType ());
			return Update (mapping, data, updateFields);
		}

		int Update (DataTableEntityMapping mapping, object data)
		{
			DataTableEntity entity = data as DataTableEntity;
			if (entity != null) {
				return Update (mapping, data, entity.GetUpdateFields ());
			}
			else {
				return Update (mapping, data, null);
			}
		}

		int Update (DataTableEntityMapping mapping, object data, string[] updateFields)
		{
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateUpdateCommand (mapping, data, updateFields);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="data">数据对象</param>
		/// <returns>受影响行数</returns>
		public int Delete (object data)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (data.GetType ());
			return Delete (mapping, data);
		}

		int Delete (DataTableEntityMapping mapping, object data)
		{
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateDeleteCommand (mapping, data);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// 创建新对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <returns>数据对象</returns>
		public T CreateNew<T> ()
            where T : class, new()
		{
			DataTableEntityMapping rawmapping = DataMapping.GetTableMapping (typeof(T));
			object obj = rawmapping.InitialData ();
			if (rawmapping.IsDataEntity) {
				DataEntity data = obj as DataEntity;
				if (data != null) {
					data.SetContext (this);
				}
			}
			return obj as T;
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		/// <typeparam name="T">删除对象类型</typeparam>
		/// <param name="query">查询表达式</param>
		/// <returns>受影响行数</returns>
		public int DeleteMass<T> (QueryExpression query)
            where T : class, new()
		{
			return DeleteMass (typeof(T), query);
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		/// <typeparam name="T">删除对象类型</typeparam>
		/// <returns>受影响行数</returns>
		public int DeleteMass<T> ()
            where T : class, new()
		{
			return DeleteMass<T> (null);
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		/// <param name="type">删除对象类型</param>
		/// <param name="query">查询表达式</param>
		/// <returns></returns>
		internal int DeleteMass (Type type, QueryExpression query)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (type);
			return DeleteMass (mapping, query);
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		/// <param name="mapping">表映射</param>
		/// <param name="query">表达式</param>
		/// <returns>受影响行数</returns>
		internal int DeleteMass (DataTableEntityMapping mapping, QueryExpression query)
		{
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateDeleteMassCommand (mapping, query);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}


		/// <summary>
		/// 批量更新数据
		/// </summary>
		/// <typeparam name="T">更新对象类型</typeparam>
		/// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
		/// <param name="query">查询表达式</param>
		/// <returns>受影响行数</returns>
		public int UpdateMass<T> (UpdateSetValue[] updates, QueryExpression query)
            where T : class, new()
		{
			return UpdateMass (typeof(T), updates, query);
		}

		/// <summary>
		/// 批量更新数据
		/// </summary>
		/// <typeparam name="T">更新对象类型</typeparam>
		/// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
		/// <returns>受影响行数</returns>
		public int UpdateMass<T> (UpdateSetValue[] updates)
            where T : class, new()
		{
			return UpdateMass<T> (updates, null);
		}

		/// <summary>
		/// 批量更新数据
		/// </summary>
		/// <param name="type">更新对象类型</param>
		/// <param name="updates">更新对象类型</param>
		/// <param name="query">查询表达式</param>
		/// <returns>受影响行数</returns>
		internal int UpdateMass (Type type, UpdateSetValue[] updates, QueryExpression query)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (type);
			return UpdateMass (mapping, updates, query);
		}

		/// <summary>
		/// 批量更新数据
		/// </summary>
		/// <param name="mapping">表映射</param>
		/// <param name="updates">更新对象类型</param>
		/// <param name="query">查询表达式</param>
		/// <returns>受影响行数</returns>
		internal int UpdateMass (DataTableEntityMapping mapping, UpdateSetValue[] updates, QueryExpression query)
		{
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateUpdateMassCommand (mapping, updates, query);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// 批量插入数据
		/// </summary>
		/// <param name="datas">数据数组</param>
		/// <returns>返回处理行数</returns>
		/// <param name = "batchCount"></param>
		public int BulkInsert (Array datas, int batchCount = 10)
		{
			if (datas == null) {
				throw new ArgumentNullException ("datas");
			}
			if (datas.Length == 0) {
				return 0;
			}
			Type arrayType = datas.GetType ();
			Type type = arrayType.GetElementType ();
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (type);
			CommandData[] commandDatas = _dataBase.Factory.CreateBulkInsertCommand (mapping, datas, batchCount);
			IDbCommand[] dbcommands = new IDbCommand[commandDatas.Length];
			for (int i = 0; i < commandDatas.Length; i++) {
				dbcommands [i] = commandDatas [i].CreateCommand (_dataBase);
			}
			IDbCommand identityCommand = null;
			if (mapping.IdentityField != null) {
				CommandData commandDataIdentity = _dataBase.Factory.CreateIdentityCommand (mapping);
				identityCommand = commandDataIdentity.CreateCommand (_dataBase);
			}
				
			object obj;
			int[] results = ExecuteBluckInsertCommands (dbcommands, identityCommand, SafeLevel.Default, out obj);
			foreach (IDbCommand command in dbcommands) {
				command.Dispose ();
			}
			if (!Object.Equals (obj, null)) {
				object id = Convert.ChangeType (obj, mapping.IdentityField.ObjectType);
				int len = datas.Length;
				object[] ids = CreateObjectList (id, len);

				for (int i = 0; i < len; i++) {
					object data = datas.GetValue (i);
					object value = ids [i];
					mapping.IdentityField.Handler.Set (data, value);
				}

			}
			int result = 0;
			foreach (int i in results) {
				result += i;
			}
			return result;
		}

		static object[] CreateObjectList (object lastId, int len)
		{
			TypeCode code = Type.GetTypeCode (lastId.GetType ());
			object[] results = new object[len];
			if (code == TypeCode.Int16) {
				short id = (short)lastId;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			else if (code == TypeCode.Int32) {
				int id = (int)lastId;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			else if (code == TypeCode.Int64) {
				long id = (long)lastId;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			else if (code == TypeCode.UInt16) {
				ushort id = (ushort)lastId;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			else if (code == TypeCode.UInt32) {
				uint id = (uint)lastId;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			else if (code == TypeCode.UInt64) {
				ulong id = (ulong)lastId;
				id++;
				for (int i = len - 1; i >= 0; i--) {
					results [i] = id--;
				}
			}
			return results;
		}

		internal int SelectInsert (Type insertType, DataFieldInfo[] insertFields, Type selectType, SelectFieldInfo[] selectFields, QueryExpression query, OrderExpression order)
		{
			DataTableEntityMapping insertMapping = DataMapping.GetTableMapping (insertType);
			DataTableEntityMapping selectMapping = DataMapping.GetTableMapping (selectType);
			int rInt;
			CommandData commandData = _dataBase.Factory.CreateSelectInsertCommand (insertMapping, insertFields, selectMapping, selectFields, query, order);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}


		/// <summary>
		/// 从主键获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="primaryKeys">主键数组</param>
		/// <returns>数据对象</returns>
		public T SelectSingleFromKey<T> (params object[] primaryKeys)
            where T : class, new()
		{
			if (primaryKeys == null || primaryKeys.Length == 0)
				throw new ArgumentNullException ("primaryKeys");
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(T));
			if (primaryKeys.Length != mapping.PrimaryKeyCount) {
				throw new LightDataException (RE.TheNumberOfPrimaryKeysIsNotMatch);
			}
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			QueryExpression query = null;
			int i = 0;
			foreach (DataFieldMapping fieldMapping in mapping.PrimaryKeyFields) {
				DataFieldInfo info = new DataFieldInfo (fieldMapping);
				query = QueryExpression.And (query, info == primaryKeys [i]);
				i++;
			}
			return SelectSingle<T> (mapping, query, null, 0, SafeLevel.None);
		}

		/// <summary>
		/// 从自增ID获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="id">自增ID</param>
		/// <returns>数据对象</returns>
		public T SelectSingleFromId<T> (int id)
            where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// 从自增ID获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="id">自增ID</param>
		/// <returns>数据对象</returns>
		public T SelectSingleFromId<T> (uint id)
            where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// 从自增ID获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="id">自增ID</param>
		/// <returns>数据对象</returns>
		public T SelectSingleFromId<T> (long id)
            where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// 从自增ID获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="id">自增ID</param>
		/// <returns>数据对象</returns>
		public T SelectSingleFromId<T> (ulong id)
            where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// 从自增ID获取数据对象
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="id">自增</param>
		/// <returns>数据对象</returns>
		private T SelectSingleFromIdObj<T> (object id)
            where T : class, new()
		{
			DataTableEntityMapping dtmapping = DataMapping.GetTableMapping (typeof(T));
			if (dtmapping.IdentityField == null) {
				throw new LightDataException (RE.DataTableNotIdentityField);
			}
			DataFieldInfo idfield = new DataFieldInfo (dtmapping.IdentityField);
			QueryExpression query = idfield == id;
			return SelectSingle<T> (dtmapping, query, null, 0, SafeLevel.None);
		}

		/// <summary>
		/// 生成数据查询对象
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <returns>数据查询对象</returns>
		public LEnumerable<T> LQuery<T> ()
            where T : class, new()
		{
			return new LEnumerable<T> (this);
		}

		/// <summary>
		/// 生成数据统计对象
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <returns>数据查询对象</returns>
		public AggregateTable<T> LAggregate<T> ()
            where T : class, new()
		{
			return new AggregateTable<T> (this);
		}

		/// <summary>
		/// Truncates the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public int TruncateTable<T> () 
			where T : class, new()
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (typeof(T));
			CommandData commandData = _dataBase.Factory.CreateTruncatCommand (mapping);
			IDbCommand command = commandData.CreateCommand (_dataBase);
			return ExecuteNonQuery (command, SafeLevel.Default);
		}

		//		/// <summary>
		//		/// 生成数据查询枚举
		//		/// </summary>
		//		/// <param name="mapping">数据映射</param>
		//		/// <param name="query">查询表达式</param>
		//		/// <param name="order">排序表达式</param>
		//		/// <param name="region">查询范围</param>
		//		/// <param name="level">安全级别</param>
		//		/// <returns>数据枚举</returns>
		//		internal IEnumerable QueryDataEnumerable (DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		//		{
		//			CommandData commandData = _dataBase.Factory.CreateSelectCommand (mapping, query, order, IsInnerPager ? region : null);
		//			IDbCommand command = commandData.CreateCommand (_dataBase);
		//			return QueryDataReader (mapping, command, !IsInnerPager ? region : null, level);
		//		}

		/// <summary>
		/// 生成数据查询枚举
		/// </summary>
		/// <param name="mapping">数据映射</param>
		/// <param name="query">查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="region">查询范围</param>
		/// <param name="level">安全级别</param>
		/// <returns>数据枚举</returns>
		internal IEnumerable<T> QueryDataMappingEnumerable<T> (DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
			where T:class, new()
		{
			bool innerRegion = IsInnerPager && mapping.IsSupportInnerPage;
			CommandData commandData = _dataBase.Factory.CreateSelectCommand (mapping, query, order, innerRegion ? region : null);
			IDbCommand command = commandData.CreateCommand (_dataBase);
			return QueryDataMappingReader<T> (mapping, command, innerRegion ? null : region, level, commandData.State);
		}

		/// <summary>
		/// 生成数据查询枚举
		/// </summary>
		/// <param name="query">查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="region">查询范围</param>
		/// <param name="level">安全级别</param>
		/// <returns>数据集合</returns>
		internal List<T> QueryDataList<T> (QueryExpression query, OrderExpression order, Region region, SafeLevel level)
			where T:class, new()
		{
			DataEntityMapping mapping = DataMapping.GetEntityMapping (typeof(T));
			bool innerRegion = IsInnerPager && mapping.IsSupportInnerPage;
			CommandData commandData = _dataBase.Factory.CreateSelectCommand (mapping, query, order, innerRegion ? region : null);
			List<T> list = new List<T> ();
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				IEnumerable<T> ie = QueryDataMappingReader<T> (mapping, command, innerRegion ? null : region, level, commandData.State);
				list.AddRange (ie);
			}
			return list;
		}

		/// <summary>
		/// Queries the join data list.
		/// </summary>
		/// <returns>The join data list.</returns>
		/// <param name="mapping">Mapping.</param>
		/// <param name="selector">Selector.</param>
		/// <param name="modelList">Model list.</param>
		/// <param name="query">Query.</param>
		/// <param name="order">Order.</param>
		/// <param name="region">Region.</param>
		/// <param name="level">Level.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		internal List<T> QueryJoinDataList<T> (DataMapping mapping, JoinSelector selector, List<JoinModel> modelList, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
			where T:class, new()
		{
			CommandData commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, modelList, query, order);
			List<T> list = new List<T> ();
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				IEnumerable<T> ie = QueryDataMappingReader<T> (mapping, command, region, level, commandData.State);
				list.AddRange (ie);
			}
			return list;
		}

		/// <summary>
		/// Queries the colume enumerable.
		/// </summary>
		/// <returns>The colume enumerable.</returns>
		/// <param name="fieldInfo">Field info.</param>
		/// <param name="outputType">Output type.</param>
		/// <param name="query">Query.</param>
		/// <param name="order">Order.</param>
		/// <param name="region">Region.</param>
		/// <param name="distinct">If set to <c>true</c> distinct.</param>
		/// <param name="level">Level.</param>
		internal IEnumerable QueryColumeEnumerable (DataFieldInfo fieldInfo, Type outputType, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
		{
			CommandData commandData = _dataBase.Factory.CreateSelectSingleFieldCommand (fieldInfo, query, order, distinct, null);
			IDbCommand command = commandData.CreateCommand (_dataBase);
			DataDefine define = TransferDataDefine (outputType, fieldInfo.DataField);
			return QueryDataReader (define, command, region, level, null);
		}

		/// <summary>
		/// 查询单列数据
		/// </summary>
		/// <param name="fieldInfo">字段信息</param>
		/// <param name="query">查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="region">查询范围</param>
		/// <param name="distinct">是否排除重复</param>
		/// <param name="level">安全级别</param>
		/// <returns>数据集合</returns>
		internal List<K> QueryColumeList<K> (DataFieldInfo fieldInfo, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
		{
			Type outputType = typeof(K);
			CommandData commandData = _dataBase.Factory.CreateSelectSingleFieldCommand (fieldInfo, query, order, distinct, null);
			List<K> list = new List<K> ();
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				DataDefine define = TransferDataDefine (outputType, fieldInfo.DataField);
				IEnumerable ie = QueryDataReader (define, command, region, level, null);
				foreach (K obj in ie) {
					list.Add (obj);
				}
			}
			return list;
		}


		/// <summary>
		/// 动态统计数据到数据表中
		/// </summary>
		/// <param name="mapping">数据映射</param>
		/// <param name="fields">统计字段信息</param>
		/// <param name="functions">统计方法信息</param>
		/// <param name="query">查询表达式</param>
		/// <param name="having">统计查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="level">安全级别</param>
		/// <returns>数据表</returns>
		internal DataTable QueryDynamicAggregateTable (DataEntityMapping mapping, List<DataFieldInfo> fields, List<AggregateFunctionInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
		{
			CommandData commandData = _dataBase.Factory.CreateDynamicAggregateCommand (mapping, fields, functions, query, having, order);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				return QueryDataTable (command, null, level);
			}
		}

		/// <summary>
		/// Queries the dynamic aggregate list.
		/// </summary>
		/// <returns>The dynamic aggregate list.</returns>
		/// <param name="mapping">Mapping.</param>
		/// <param name="fields">Fields.</param>
		/// <param name="functions">Functions.</param>
		/// <param name="query">Query.</param>
		/// <param name="having">Having.</param>
		/// <param name="order">Order.</param>
		/// <param name="level">Level.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		internal List<T> QueryDynamicAggregateList<T> (DataEntityMapping mapping, List<DataFieldInfo> fields, List<AggregateFunctionInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
			where T:class, new()
		{
			AggregateTableMapping amapping = AggregateTableMapping.GetAggregateMapping (typeof(T));
			CommandData commandData = _dataBase.Factory.CreateDynamicAggregateCommand (mapping, fields, functions, query, having, order);
			List<T> list = new List<T> ();
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				IEnumerable<T> ie = QueryDataMappingReader<T> (amapping, command, null, level, commandData.State);
				list.AddRange (ie);
			}
			return list;
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
		internal T SelectSingle<T> (DataEntityMapping mapping, QueryExpression query, OrderExpression order, int index, SafeLevel level)
			where T:class, new()
		{
			T target = default(T);
			Region region = new Region (index, 1);
			bool innerRegion = IsInnerPager && mapping.IsSupportInnerPage;
			CommandData commandData = _dataBase.Factory.CreateSelectCommand (mapping, query, order, innerRegion ? region : null);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				foreach (T obj in QueryDataMappingReader<T>(mapping, command, innerRegion ? null : region, level, commandData.State)) {
					target = obj;
					break;
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
		internal object AggregateCount (DataEntityMapping mapping, QueryExpression query, SafeLevel level)
		{
			CommandData commandData = _dataBase.Factory.CreateAggregateCountCommand (mapping, query);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				return ExecuteScalar (command, level);
			}
		}

		/// <summary>
		/// Aggregates the join table count.
		/// </summary>
		/// <returns>The join table count.</returns>
		/// <param name="models">Models.</param>
		/// <param name="query">Query.</param>
		/// <param name="level">Level.</param>
		internal object AggregateJoinTableCount (List<JoinModel> models, QueryExpression query, SafeLevel level)
		{
			CommandData commandData = _dataBase.Factory.CreateAggregateJoinCountCommand (models, query);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				return ExecuteScalar (command, level);
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
		internal object Aggregate (DataFieldMapping fieldMapping, AggregateType aggregateType, QueryExpression query, bool distinct, SafeLevel level)
		{
			CommandData commandData = _dataBase.Factory.CreateAggregateCommand (fieldMapping, aggregateType, query, distinct);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				object obj = ExecuteScalar (command, level);
				if (Object.Equals (obj, DBNull.Value)) {
					return null;
				}
				else {
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
		internal bool Exists (DataEntityMapping mapping, QueryExpression query, SafeLevel level)
		{
			bool exists = false;
			Region region = new Region (0, 1);
			CommandData commandData = _dataBase.Factory.CreateExistsCommand (mapping, query);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				PrimitiveDataDefine pm = PrimitiveDataDefine.ParseDefine (typeof(Int32));
				foreach (object obj in QueryDataReader(pm, command, region, level,null)) {
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
		internal DataTable QueryDataTable (IDbCommand dbcommand, Region region, SafeLevel level)
		{
			DataTable dt = QueryDataSet (dbcommand, level).Tables [0];
			if (region == null) {
				return dt;
			}
			else {
				DataTable dt1 = new DataTable ();
				DataRowCollection drs = dt.Rows;
				int start = region.Start;
				int size = region.Size;
				for (int i = start; i < size; i++) {
					dt1.Rows.Add (drs [i]);
				}
				return dt1;
			}
		}

		#region 核心数据库方法

		/// <summary>
		/// Outputs the command.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="command">Command.</param>
		/// <param name="level">Level.</param>
		protected virtual void OutputCommand (string action, IDbCommand command, SafeLevel level)
		{
			if (this.output != null) {
				int count = command.Parameters.Count;
				DataParameter[] list = new DataParameter[count];
				int index = 0;
				foreach (IDataParameter value in command.Parameters) {
					list [index] = new DataParameter (value.ParameterName, value.Value, value.DbType.ToString (), value.Direction);
					index++;
				}
				this.output.Output (action, command.CommandText, list, command.CommandType, command.Transaction != null, level);
			}
		}

		/// <summary>
		/// Outputs the command.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="command">Command.</param>
		/// <param name="level">Level.</param>
		/// <param name="start">Start.</param>
		/// <param name="size">Size.</param>
		protected virtual void OutputCommand (string action, IDbCommand command, SafeLevel level, int start, int size)
		{
			if (this.output != null) {
				int count = command.Parameters.Count;
				DataParameter[] list = new DataParameter[count];
				int index = 0;
				foreach (IDataParameter value in command.Parameters) {
					list [index] = new DataParameter (value.ParameterName, value.Value, value.DbType.ToString (), value.Direction);
					index++;
				}
				action = string.Format ("{0}[{1},{2}]", action, start, size);
				this.output.Output (action, command.CommandText, list, command.CommandType, command.Transaction != null, level);
			}
		}

		internal virtual int[] ExecuteBluckInsertCommands (IDbCommand[] insertCommands, IDbCommand indentityCommand, SafeLevel level, out object lastId)
		{
			if (level == SafeLevel.None) {
				level = SafeLevel.Default;
			}
			int[] rInts = new int[insertCommands.Length];
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					int index = 0;
					foreach (IDbCommand dbcommand in insertCommands) {
						transaction.SetupCommand (dbcommand);
						OutputCommand ("ExecuteMultiCommands", dbcommand, level);
						rInts [index] = dbcommand.ExecuteNonQuery ();
						index++;
					}
					if (indentityCommand != null) {
						transaction.SetupCommand (indentityCommand);
						OutputCommand ("ExecuteInsertCommand_Indentity", indentityCommand, level);
						lastId = indentityCommand.ExecuteScalar ();
					}
					else {
						lastId = null;
					}
					transaction.Commit ();

				} catch (Exception ex) {
					lastId = null;
					transaction.Rollback ();
					throw ex;
				}
			}
			return rInts;
		}

		internal virtual int[] ExecuteMultiCommands (IDbCommand[] dbcommands, SafeLevel level)
		{
			if (level == SafeLevel.None) {
				level = SafeLevel.Default;
			}
			int[] rInts = new int[dbcommands.Length];
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					int index = 0;
					foreach (IDbCommand dbcommand in dbcommands) {
						transaction.SetupCommand (dbcommand);
						OutputCommand ("ExecuteMultiCommands", dbcommand, level);
						rInts [index] = dbcommand.ExecuteNonQuery ();
						index++;
					}
					transaction.Commit ();
				} catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return rInts;
		}

		internal virtual object ExecuteInsertCommand (IDbCommand dbcommand, IDbCommand indentityCommand, SafeLevel level)
		{
			object result = null;
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					transaction.SetupCommand (dbcommand);
					OutputCommand ("ExecuteInsertCommand", dbcommand, level);
					dbcommand.ExecuteNonQuery ();
					if (indentityCommand != null) {
						transaction.SetupCommand (indentityCommand);
						OutputCommand ("ExecuteInsertCommand_Indentity", indentityCommand, level);
						object obj = indentityCommand.ExecuteScalar ();
						if (obj != null) {
							result = obj;
						}
					}
					transaction.Commit ();
				} catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return result;
		}

		internal virtual int ExecuteNonQuery (IDbCommand dbcommand, SafeLevel level)
		{
			int rInt;
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					transaction.SetupCommand (dbcommand);
					OutputCommand ("ExecuteNonQuery", dbcommand, level);
					rInt = dbcommand.ExecuteNonQuery ();
					transaction.Commit ();
				} catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return rInt;
		}

		internal virtual object ExecuteScalar (IDbCommand dbcommand, SafeLevel level)
		{
			object result;
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					transaction.SetupCommand (dbcommand);
					OutputCommand ("ExecuteScalar", dbcommand, level);
					result = dbcommand.ExecuteScalar ();
					transaction.Commit ();
				} catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return result;
		}

		internal virtual DataSet QueryDataSet (IDbCommand dbcommand, SafeLevel level)
		{
			DataSet ds = new DataSet ();
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					transaction.SetupCommand (dbcommand);
					IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
					OutputCommand ("QueryDataSet", dbcommand, level);
					adapter.Fill (ds);
					transaction.Commit ();
				} catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return ds;
		}

		internal virtual IEnumerable QueryDataReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level, object state)
		{
			int start;
			int size;
			if (region != null) {
				start = region.Start;
				size = region.Size;
			}
			else {
				start = 0;
				size = int.MaxValue;
			}
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				transaction.SetupCommand (dbcommand);
				OutputCommand ("QueryDataReader", dbcommand, level, start, size);
				using (IDataReader reader = dbcommand.ExecuteReader ()) {
					int index = 0;
					int count = 0;
					bool over = false;
					while (reader.Read ()) {
						if (over) {
							dbcommand.Cancel ();
							break;
						}
						if (index >= start) {
							count++;
							object item = source.LoadData (this, reader, state);
							if (count >= size) {
								over = true;
							}
							yield return item;
						}
						index++;
					}
				}
				transaction.Commit ();
			}
		}

		internal virtual IEnumerable<T> QueryDataMappingReader<T> (DataMapping source, IDbCommand dbcommand, Region region, SafeLevel level, object state)
			where T :class, new()
		{
			int start;
			int size;
			if (region != null) {
				start = region.Start;
				size = region.Size;
			}
			else {
				start = 0;
				size = int.MaxValue;
			}
//			try {
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				transaction.SetupCommand (dbcommand);
				OutputCommand ("QueryDataMappingReader", dbcommand, level, start, size);
				using (IDataReader reader = dbcommand.ExecuteReader ()) {
					int index = 0;
					int count = 0;
					bool over = false;
					while (reader.Read ()) {
						if (over) {
							dbcommand.Cancel ();
							break;
						}
						if (index >= start) {
							count++;
							object item = source.LoadData (this, reader, state);
							if (count >= size) {
								over = true;
							}
							yield return item as T;
						}
						index++;
					}
				}
				transaction.Commit ();
			}
//			} finally {
//				ClearTempRelate ();
//			}
		}


		#endregion

		#region 基础方法

		internal TransactionConnection CreateTransactionConnection (SafeLevel level)
		{
			IDbConnection connection = _dataBase.CreateConnection (_connectionString);
			return new TransactionConnection (connection, level);
		}

		/// <summary>
		/// 创建事务型数据上下文
		/// </summary>
		/// <returns>事务型数据上下文</returns>
		public TransDataContext CreateTransDataContext ()
		{
			TransDataContext context = new TransDataContext (_connectionString, _configName, _dataBase);
			return context;
		}

		internal DataContext CloneContext ()
		{
			DataContext context = new DataContext (_connectionString, _configName, _dataBase);
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
		public SqlExecutor CreateSqlStringExecutor (string sqlString, DataParameter[] param, SafeLevel level)
		{
			SqlExecutor executor = new SqlExecutor (sqlString, param, CommandType.Text, level, this);
			return executor;
		}

		/// <summary>
		/// 创建SQL字符串执行器
		/// </summary>
		/// <param name="sqlString">SQL字符串</param>
		/// <param name="param">参数数组</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateSqlStringExecutor (string sqlString, DataParameter[] param)
		{
			return CreateSqlStringExecutor (sqlString, param, SafeLevel.Default);
		}

		/// <summary>
		/// 创建SQL字符串执行器
		/// </summary>
		/// <param name="sqlString">SQL字符串</param>
		/// <param name="level">安全等级</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateSqlStringExecutor (string sqlString, SafeLevel level)
		{
			return CreateSqlStringExecutor (sqlString, null, level);
		}

		/// <summary>
		/// 创建SQL字符串执行器
		/// </summary>
		/// <param name="sqlString">SQL字符串</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateSqlStringExecutor (string sqlString)
		{
			return CreateSqlStringExecutor (sqlString, null, SafeLevel.Default);
		}


		/// <summary>
		/// 创建SQL存储过程执行器
		/// </summary>
		/// <param name="storeProcedure">SQL存储过程</param>
		/// <param name="param">参数数组</param>
		/// <param name="level">安全等级</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, DataParameter[] param, SafeLevel level)
		{
			SqlExecutor executor = new SqlExecutor (storeProcedure, param, CommandType.StoredProcedure, level, this);
			return executor;
		}


		/// <summary>
		/// 创建存储过程执行器
		/// </summary>
		/// <param name="storeProcedure">SQL存储过程</param>
		/// <param name="param">参数数组</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, DataParameter[] param)
		{
			return CreateStoreProcedureExecutor (storeProcedure, param, SafeLevel.Default);
		}

		/// <summary>
		/// 创建存储过程执行器
		/// </summary>
		/// <param name="storeProcedure">SQL存储过程</param>
		/// <param name="level">安全等级</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, SafeLevel level)
		{
			return CreateStoreProcedureExecutor (storeProcedure, null, level);
		}

		/// <summary>
		/// 创建存储过程执行器
		/// </summary>
		/// <param name="storeProcedure">SQL存储过程</param>
		/// <returns>SQL执行器</returns>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure)
		{
			return CreateStoreProcedureExecutor (storeProcedure, null, SafeLevel.Default);
		}

		#endregion

		#region 静态函数

		private static DataDefine TransferDataDefine (Type type, DataFieldMapping fieldMapping)
		{
			PrimitiveFieldMapping pm = fieldMapping as PrimitiveFieldMapping;
			if (pm != null) {
				PrimitiveDataDefine pd = PrimitiveDataDefine.ParseDefine (type, pm);
				return pd;
			}
			EnumFieldMapping em = fieldMapping as EnumFieldMapping;
			if (em != null) {
				EnumDataDefine ed = EnumDataDefine.ParseDefine (type, em);
				return ed;
			}
			throw new LightDataException (RE.UnsupportDataDefineType);
		}

		private static IList CreateList (Type type)
		{
			IList items;
			Type itemstype = Type.GetType ("System.Collections.Generic.List`1");
			itemstype = itemstype.MakeGenericType (type);
			items = (IList)Activator.CreateInstance (itemstype);
			return items;
		}

		#endregion



		#region single relate

		//		DataContext innerContext = null;
		//
		//		internal DataContext InnerContext {
		//			get {
		//				if (innerContext == null) {
		//					innerContext = this.CloneContext ();
		//				}
		//				return innerContext;
		//			}
		//		}

		Dictionary<DataEntityMapping,Hashtable> tempRelate = new Dictionary<DataEntityMapping, Hashtable> ();

		//		internal void SetRelationData (DataEntityMapping mapping, object key, object value)
		//		{
		//			Hashtable table;
		//			if (!tempRelate.TryGetValue (mapping, out table)) {
		//				table = new Hashtable ();
		//				tempRelate.Add (mapping, table);
		//			}
		//			table.Add (key, value);
		//		}
		//
		//		internal bool GetRelationData (DataEntityMapping mapping, object key, out object value)
		//		{
		//			value = null;
		//			Hashtable table;
		//			if (!tempRelate.TryGetValue (mapping, out table)) {
		//				return false;
		//			}
		//			if (table.Contains (key)) {
		//				value = table [key];
		//				return true;
		//			}
		//			else {
		//				return false;
		//			}
		//		}
		//
		//		internal void ClearTempRelate ()
		//		{
		//			if (tempRelate.Count > 0) {
		//				tempRelate.Clear ();
		//			}
		//		}
		//
		internal object SelectFirst (DataEntityMapping mapping, QueryExpression query, object state)
		{
			object target;
			Region region = new Region (0, 1);
			bool innerRegion = IsInnerPager && mapping.IsSupportInnerPage;
			CommandData commandData = _dataBase.Factory.CreateSelectCommand (mapping, query, null, innerRegion ? region : null);
			using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
				target = QueryRelateDataMappingReader (mapping, command, state);
			}
			return target;
		}

		internal virtual object QueryRelateDataMappingReader (DataMapping source, IDbCommand dbcommand, object state)
		{
			object target = null;
			using (TransactionConnection transaction = CreateTransactionConnection (SafeLevel.None)) {
				transaction.Open ();
				transaction.SetupCommand (dbcommand);
				OutputCommand ("QueryRelateDataMappingReader", dbcommand, SafeLevel.None);
				using (IDataReader reader = dbcommand.ExecuteReader ()) {
					while (reader.Read ()) {
						object item = source.LoadData (this, reader, state);
						target = item;
						dbcommand.Cancel ();
						break;
					}
				}
				transaction.Commit ();
			}
			return target;
		}

		#endregion
	}
}