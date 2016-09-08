using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace Light.Data
{
	/// <summary>
	/// DataContext.
	/// </summary>
	public class DataContext : ILogicalThreadAffinative
	{
		static Dictionary<string, DataContextSetting> Settings = new Dictionary<string, DataContextSetting> ();

		static DataContextSetting DefaultContextSetting;

		/// <summary>
		/// Create the default context.
		/// </summary>
		/// <value>The default.</value>
		public static DataContext Default {
			get {
				return CreateDefault ();
			}
		}

		const string CONTEXT_KEY = "Light.Data.DataContext";

		public static DataContext Current {
			get {
				return CallContext.GetData (CONTEXT_KEY) as DataContext;
			}
			set {
				CallContext.SetData (CONTEXT_KEY, value);
			}
		}

		static DataContext ()
		{
			if (ConfigurationManager.ConnectionStrings != null) {
				foreach (ConnectionStringSettings connectionSetting in ConfigurationManager.ConnectionStrings) {
					DataContextSetting contextSetting = DataContextSetting.CreateSetting (connectionSetting, false);
					if (contextSetting != null && DefaultContextSetting == null) {
						DefaultContextSetting = contextSetting;
					}
					Settings [connectionSetting.Name] = contextSetting;
				}
			}
		}

		/// <summary>
		/// Creates the default dataContext.
		/// </summary>
		/// <returns>The context.</returns>
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
		/// Create the dataContext.
		/// </summary>
		/// <returns>The context.</returns>
		/// <param name="configName">Config name.</param>
		public static DataContext Create (string configName)
		{
			if (configName == null)
				throw new ArgumentNullException (nameof (configName));
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
		/// Create the dataContext.
		/// </summary>
		/// <returns>The context.</returns>
		/// <param name="name">Name.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="providerName">Provider name.</param>
		public static DataContext Create (string name, string connectionString, string providerName)
		{
			ConnectionStringSettings connectionSetting = new ConnectionStringSettings (name, connectionString, providerName);
			return CreateFromSetting (connectionSetting);
		}

		/// <summary>
		/// Create the dataContext.
		/// </summary>
		/// <returns>The context.</returns>
		/// <param name="setting">Setting.</param>
		public static DataContext CreateFromSetting (ConnectionStringSettings setting)
		{
			if (setting == null)
				throw new ArgumentNullException (nameof (setting));
			DataContextSetting contextSetting = DataContextSetting.CreateSetting (setting, true);
			DataContext context = new DataContext (contextSetting.Connection, contextSetting.Name, contextSetting.DataBase);
			return context;
		}

		/// <summary>
		/// The connection string.
		/// </summary>
		protected string _connectionString;

		/// <summary>
		/// The name of the config.
		/// </summary>
		protected string _configName;

		Database _dataBase;

		internal Database DataBase {
			get {
				return _dataBase;
			}
		}

		/// <summary>
		/// The command output interface.
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
		/// Gets the name of the config.
		/// </summary>
		/// <value>The name of the config.</value>
		public string ConfigName {
			get {
				return _configName;
			}
		}

		/// <summary>
		/// Gets or sets the time out.
		/// </summary>
		/// <value>The time out.</value>
		public int TimeOut {
			get {
				return _dataBase.CommandTimeOut;
			}
			set {
				_dataBase.CommandTimeOut = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is inner pager.
		/// </summary>
		/// <value><c>true</c> if this instance is inner pager; otherwise, <c>false</c>.</value>
		//public bool IsInnerPager {
		//	get {
		//		return _dataBase.InnerPager;
		//	}
		//}

		///// <summary>
		///// Sets the inner pager enable.
		///// </summary>
		///// <returns><c>true</c>, if inner pager was set, <c>false</c> otherwise.</returns>
		///// <param name="enable">If set to <c>true</c> enable.</param>
		//public bool SetInnerPager (bool enable)
		//{
		//	_dataBase.InnerPager = enable;
		//	return _dataBase.InnerPager == enable;
		//}



		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.DataContext"/> class.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="configName">Config name.</param>
		/// <param name="dataBase">Data base.</param>
		internal DataContext (string connectionString, string configName, Database dataBase)
		{
			_connectionString = connectionString;
			_configName = configName;
			_dataBase = dataBase;
			//CallContext.SetData(_configName
		}

		/// <summary>
		/// Gets the name of the mapping table.
		/// </summary>
		/// <returns>The table name.</returns>
		/// <param name="type">Type.</param>
		public string GetTableName (Type type)
		{
			DataEntityMapping mapping = DataEntityMapping.GetEntityMapping (type);
			if (mapping != null) {
				return mapping.TableName;
			}
			else {
				return null;
			}
		}

		/// <summary>
		/// Gets the name of the mapping table.
		/// </summary>
		/// <returns>The table name.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public string GetTableName<T> ()
			 where T : class, new()
		{
			return GetTableName (typeof (T));
		}

		/// <summary>
		/// Insert or update the specified data.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="data">Data.</param>
		public int InsertOrUpdate (object data)
		{
			bool exists = false;
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (data.GetType ());
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateEntityExistsCommand (mapping, data, state);
			Region region = new Region (0, 1);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				//PrimitiveDataDefine pm = PrimitiveDataDefine.ParseDefine (typeof (Int32));
				DataDefine define = DataDefine.GetDefine (typeof (Int32));
				foreach (object obj in QueryDataDefineReader (define, command, region, SafeLevel.Default, null)) {
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
		/// Insert the specified data.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="data">Data.</param>
		public int Insert (object data)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (data.GetType ());
			return Insert (mapping, data);
		}

		private int Insert (DataTableEntityMapping mapping, object data)
		{
			object obj;
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateInsertCommand (mapping, data, state);
			CommandData commandDataIdentity = null;
			if (mapping.IdentityField != null) {
				commandDataIdentity = _dataBase.Factory.CreateIdentityCommand (mapping);
			}
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
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
		/// Update the specified data.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="data">Data.</param>
		public int Update (object data)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (data.GetType ());
			//DataTableEntity entity = data as DataTableEntity;
			//if (entity != null) {
			//	return Update (mapping, data, entity.GetUpdateFields ());
			//}
			//else {
			return Update (mapping, data);
			//}
		}

		/// <summary>
		/// Update the specified data and updateFields.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="data">Data.</param>
		/// <param name="updateFields">Update fields.</param>
		internal int Update (object data, string [] updateFields)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (data.GetType ());
			return Update (mapping, data);
		}

		//private int Update (DataTableEntityMapping mapping, object data)
		//{
		//	DataTableEntity entity = data as DataTableEntity;
		//	if (entity != null) {
		//		return Update (mapping, data, entity.GetUpdateFields ());
		//	}
		//	else {
		//		return Update (mapping, data, null);
		//	}
		//}

		private int Update (DataTableEntityMapping mapping, object data)
		{
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateUpdateCommand (mapping, data, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// Delete the specified data.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="data">Data.</param>
		public int Delete (object data)
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (data.GetType ());
			return Delete (mapping, data);
		}

		private int Delete (DataTableEntityMapping mapping, object data)
		{
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateDeleteCommand (mapping, data, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// Creates the new object.
		/// </summary>
		/// <returns>The new.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T CreateNew<T> ()
			where T : class, new()
		{
			DataTableEntityMapping rawmapping = DataEntityMapping.GetTableMapping (typeof (T));
			object obj = rawmapping.InitialData ();
			if (rawmapping.IsDataEntity) {
				DataEntity data = obj as DataEntity;
				if (data != null) {
					data.SetContext (this);
				}
			}
			return obj as T;
		}

		///// <summary>
		///// Mass delete datas which match queryexpression.
		///// </summary>
		///// <returns>result.</returns>
		///// <param name="query">Query.</param>
		///// <typeparam name="T">The 1st type parameter.</typeparam>
		//public int DeleteMass<T> (QueryExpression query)
		//	where T : class, new()
		//{
		//	return DeleteMass (typeof (T), query);
		//}

		///// <summary>
		///// Mass delete all data.
		///// </summary>
		///// <returns>result.</returns>
		///// <typeparam name="T">The 1st type parameter.</typeparam>
		//public int DeleteMass<T> ()
		//	where T : class, new()
		//{
		//	return DeleteMass<T> (null);
		//}

		//internal int DeleteMass (Type type, QueryExpression query)
		//{
		//	DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (type);
		//	return DeleteMass (mapping, query);
		//}

		/// <summary>
		/// Mass delete the datas.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="mapping">Mapping.</param>
		/// <param name="query">Query.</param>
		internal int DeleteMass (DataTableEntityMapping mapping, QueryExpression query, SafeLevel level)
		{
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateDeleteMassCommand (mapping, query, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		///// <summary>
		///// Mass updates the daats which match queryexpression..
		///// </summary>
		///// <returns>The mass.</returns>
		///// <param name="updates">Updates.</param>
		///// <param name="query">Query.</param>
		///// <typeparam name="T">The 1st type parameter.</typeparam>
		//public int UpdateMass<T> (QueryExpression query, params UpdateSetValue [] updates)
		//	where T : class, new()
		//{
		//	return UpdateMass (typeof (T), updates, query);
		//}

		///// <summary>
		///// 批量更新数据
		///// </summary>
		///// <typeparam name="T">更新对象类型</typeparam>
		///// <param name="updates">更新字段值数组,类型必须和更新对象一致</param>
		///// <returns>受影响行数</returns>
		//public int UpdateMass<T> (params UpdateSetValue [] updates)
		//	where T : class, new()
		//{
		//	return UpdateMass<T> (null, updates);
		//}

		//internal int UpdateMass (Type type, UpdateSetValue [] updates, QueryExpression query)
		//{
		//	DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (type);
		//	return UpdateMass (mapping, updates, query);
		//}

		//internal int UpdateMass (DataTableEntityMapping mapping, UpdateSetValue [] updates, QueryExpression query)
		//{
		//	int rInt;
		//	CreateSqlState state = new CreateSqlState (_dataBase.Factory);
		//	CommandData commandData = _dataBase.Factory.CreateUpdateMassCommand (mapping, updates, query, state);
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
		//		rInt = ExecuteNonQuery (command, SafeLevel.Default);
		//	}
		//	return rInt;
		//}


		internal int UpdateMass (MassUpdator updator, QueryExpression query, SafeLevel level)
		{
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateUpdateMassCommand (updator, query, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, SafeLevel.Default);
			}
			return rInt;
		}

		/// <summary>
		/// Bulk insert the datas.
		/// </summary>
		/// <returns>result.</returns>
		/// <param name="datas">Datas.</param>
		/// <param name="batchCount">Batch count.</param>
		public int BulkInsert (Array datas, int batchCount = 10)
		{
			if (datas == null) {
				throw new ArgumentNullException (nameof (datas));
			}
			if (datas.Length == 0) {
				return 0;
			}
			Type arrayType = datas.GetType ();
			Type type = arrayType.GetElementType ();
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (type);
			//CommandData [] commandDatas = _dataBase.Factory.CreateBulkInsertCommand (mapping, datas, batchCount);
			Tuple<CommandData, CreateSqlState> [] commandDatas = _dataBase.Factory.CreateBulkInsertCommand (mapping, datas, batchCount);
			IDbCommand [] dbcommands = new IDbCommand [commandDatas.Length];
			for (int i = 0; i < commandDatas.Length; i++) {
				dbcommands [i] = commandDatas [i].Item1.CreateCommand (_dataBase, commandDatas [i].Item2);
			}
			IDbCommand identityCommand = null;
			if (mapping.IdentityField != null) {
				CommandData commandDataIdentity = _dataBase.Factory.CreateIdentityCommand (mapping);
				identityCommand = commandDataIdentity.CreateCommand (_dataBase);
			}

			object obj;
			int [] results = ExecuteBulkCommands (dbcommands, identityCommand, SafeLevel.Default, out obj);
			foreach (IDbCommand command in dbcommands) {
				command.Dispose ();
			}
			if (!Object.Equals (obj, null)) {
				object id = Convert.ChangeType (obj, mapping.IdentityField.ObjectType);
				int len = datas.Length;
				object [] ids = CreateObjectList (id, len);

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
			if (result >= 0) {
				return result;
			}
			else {
				return -1;
			}
		}

		public int BulkUpdate (Array datas, int batchCount = 10)
		{
			if (datas == null) {
				throw new ArgumentNullException (nameof (datas));
			}
			if (datas.Length == 0) {
				return 0;
			}
			Type arrayType = datas.GetType ();
			Type type = arrayType.GetElementType ();
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (type);
			Tuple<CommandData, CreateSqlState> [] commandDatas = _dataBase.Factory.CreateBulkUpdateCommand (mapping, datas, batchCount);
			IDbCommand [] dbcommands = new IDbCommand [commandDatas.Length];
			for (int i = 0; i < commandDatas.Length; i++) {
				dbcommands [i] = commandDatas [i].Item1.CreateCommand (_dataBase, commandDatas [i].Item2);
			}

			int [] results = ExecuteMultiCommands (dbcommands, SafeLevel.Default);
			foreach (IDbCommand command in dbcommands) {
				command.Dispose ();
			}
			int result = 0;
			foreach (int i in results) {
				result += i;
			}
			if (result >= 0) {
				return result;
			}
			else {
				return -1;
			}
		}

		public int BulkDelete (Array datas, int batchCount = 10)
		{
			if (datas == null) {
				throw new ArgumentNullException (nameof (datas));
			}
			if (datas.Length == 0) {
				return 0;
			}
			Type arrayType = datas.GetType ();
			Type type = arrayType.GetElementType ();
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (type);
			Tuple<CommandData, CreateSqlState> [] commandDatas = _dataBase.Factory.CreateBulkDeleteCommand (mapping, datas, batchCount);
			IDbCommand [] dbcommands = new IDbCommand [commandDatas.Length];
			for (int i = 0; i < commandDatas.Length; i++) {
				dbcommands [i] = commandDatas [i].Item1.CreateCommand (_dataBase, commandDatas [i].Item2);
			}

			int [] results = ExecuteMultiCommands (dbcommands, SafeLevel.Default);
			foreach (IDbCommand command in dbcommands) {
				command.Dispose ();
			}
			int result = 0;
			foreach (int i in results) {
				result += i;
			}
			if (result >= 0) {
				return result;
			}
			else {
				return -1;
			}
		}

		static object [] CreateObjectList (object lastId, int len)
		{
			TypeCode code = Type.GetTypeCode (lastId.GetType ());
			object [] results = new object [len];
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

		//internal int SelectInsert (Type insertType, DataFieldInfo [] insertFields, Type selectType, SelectFieldInfo [] selectFields, QueryExpression query, OrderExpression order)
		//{
		//	DataTableEntityMapping insertMapping = DataEntityMapping.GetTableMapping (insertType);
		//	DataTableEntityMapping selectMapping = DataEntityMapping.GetTableMapping (selectType);
		//	int rInt;
		//	CreateSqlState state = new CreateSqlState (_dataBase.Factory);
		//	CommandData commandData = _dataBase.Factory.CreateSelectInsertCommand (insertMapping, insertFields, selectMapping, selectFields, query, order, state);
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
		//		rInt = ExecuteNonQuery (command, SafeLevel.Default);
		//	}
		//	return rInt;
		//}

		internal int SelectInsert (DataTableEntityMapping insertMapping, DataEntityMapping selectMapping, QueryExpression query, OrderExpression order, SafeLevel level)
		{
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateSelectInsertCommand (insertMapping, selectMapping, query, order, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, level);
			}
			return rInt;
		}

		internal int SelectInsert (InsertSelector selector, QueryExpression query, OrderExpression order, SafeLevel level)
		{
			DataEntityMapping mapping = selector.SelectMapping;
			RelationMap relationMap = mapping.GetRelationMap ();
			CommandData commandData;
			int rInt;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			if (mapping.HasJoinRelateModel) {
				QueryExpression subQuery = null;
				QueryExpression mainQuery = null;
				OrderExpression subOrder = null;
				OrderExpression mainOrder = null;
				if (query != null) {
					if (query.MutliQuery) {
						mainQuery = query;
					}
					else {
						subQuery = query;
					}
				}
				if (order != null) {
					if (order.MutliOrder) {
						mainOrder = order;
					}
					else {
						subOrder = order;
					}
				}
				EntityJoinModel [] models = relationMap.CreateJoinModels (subQuery, subOrder);
				commandData = _dataBase.Factory.CreateSelectInsertCommand (selector, models, mainQuery, mainOrder, state);
			}
			else {
				commandData = _dataBase.Factory.CreateSelectInsertCommand (selector, query, order, state);
			}
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				rInt = ExecuteNonQuery (command, level);
			}
			return rInt;
		}

		/// <summary>
		/// Selects the single object from key.
		/// </summary>
		/// <returns>object.</returns>
		/// <param name="primaryKeys">Primary keys.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T SelectSingleFromKey<T> (params object [] primaryKeys)
			where T : class, new()
		{
			if (primaryKeys == null || primaryKeys.Length == 0)
				throw new ArgumentNullException (nameof (primaryKeys));
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
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
			return SelectEntityDataSingle (mapping, query, null, 0, SafeLevel.None) as T;
		}

		/// <summary>
		/// Selects the single object from identifier.
		/// </summary>
		/// <returns>object.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T SelectSingleFromId<T> (int id)
			where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// Selects the single object from identifier.
		/// </summary>
		/// <returns>object.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T SelectSingleFromId<T> (uint id)
			where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// Selects the single object from identifier.
		/// </summary>
		/// <returns>object.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T SelectSingleFromId<T> (long id)
			where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		/// <summary>
		/// Selects the single object from identifier.
		/// </summary>
		/// <returns>object.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T SelectSingleFromId<T> (ulong id)
			where T : class, new()
		{
			return SelectSingleFromIdObj<T> (id);
		}

		private T SelectSingleFromIdObj<T> (object id)
			where T : class, new()
		{
			DataTableEntityMapping dtmapping = DataEntityMapping.GetTableMapping (typeof (T));
			if (dtmapping.IdentityField == null) {
				throw new LightDataException (RE.DataTableNotIdentityField);
			}
			DataFieldInfo idfield = new DataFieldInfo (dtmapping.IdentityField);
			QueryExpression query = idfield == id;
			return SelectEntityDataSingle (dtmapping, query, null, 0, SafeLevel.None) as T;
		}

		/// <summary>
		/// Create LEnumerable
		/// </summary>
		/// <returns>The LEnumerable.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public LEnumerable<T> LQuery<T> ()
			where T : class, new()
		{
			return new LEnumerable<T> (this);
		}

		/// <summary>
		/// Create Aggregate.
		/// </summary>
		/// <returns>The aggregate.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public AggregateTable<T> LAggregate<T> ()
			where T : class, new()
		{
			return new AggregateTable<T> (this);
		}

		/// <summary>
		/// LQs the ueryable.
		/// </summary>
		/// <returns>The ueryable.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public IQuery<T> Query<T> ()
			where T : class//, new()
		{
			return new LightQuery<T> (this);
		}

		/// <summary>
		/// Truncates the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public int TruncateTable<T> ()
			where T : class//, new()
		{
			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (typeof (T));
			CommandData commandData = _dataBase.Factory.CreateTruncateTableCommand (mapping);
			IDbCommand command = commandData.CreateCommand (_dataBase);
			return ExecuteNonQuery (command, SafeLevel.Default);
		}

		internal IEnumerable QueryEntityData (DataEntityMapping mapping, ISelector selector, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		{
			RelationMap relationMap = mapping.GetRelationMap ();
			if (selector == null) {
				selector = relationMap.GetDefaultSelector ();
			}
			CommandData commandData;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			if (mapping.HasJoinRelateModel) {
				QueryExpression subQuery = null;
				QueryExpression mainQuery = null;
				OrderExpression subOrder = null;
				OrderExpression mainOrder = null;
				if (query != null) {
					if (query.MutliQuery) {
						mainQuery = query;
					}
					else {
						subQuery = query;
					}
				}
				if (order != null) {
					if (order.MutliOrder) {
						mainOrder = order;
					}
					else {
						subOrder = order;
					}
				}
				EntityJoinModel [] models = relationMap.CreateJoinModels (subQuery, subOrder);
				commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, models, mainQuery, mainOrder, region, state);
			}
			else {
				commandData = _dataBase.Factory.CreateSelectCommand (mapping, selector, query, order, region, state);
			}
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			QueryState queryState = new QueryState ();
			queryState.SetRelationMap (relationMap);
			queryState.SetSelector (selector);
			return QueryDataDefineReader (mapping, command, commandData.InnerPage ? null : region, level, queryState);
		}

		//internal IEnumerable QueryDynamicJoinData (Type type, JoinSelector selector, List<JoinModel> models, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		//{
		//	Tuple<string, DataEntityMapping> [] array = new Tuple<string, DataEntityMapping> [models.Count];
		//	for (int i = 0; i < models.Count; i++) {
		//		JoinModel model = models [i];
		//		Tuple<string, DataEntityMapping> tuple = new Tuple<string, DataEntityMapping> (model.AliasTableName, model.Mapping);
		//		array [i] = tuple;
		//	}
		//	DynamicMultiDataMapping mapping = new DynamicMultiDataMapping (type, array);
		//	CreateSqlState state = new CreateSqlState (_dataBase.Factory);
		//	CommandData commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, models, query, order, region, state);
		//	IDbCommand command = commandData.CreateCommand (_dataBase, state);
		//	QueryState queryState = new QueryState ();
		//	queryState.SetSelector (selector);
		//	return QueryDataMappingReader (mapping, command, commandData.InnerPage ? null : region, level, queryState);
		//}

		//internal IEnumerable QueryDataMappingEnumerable (Type type, ISelector selector, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		//{
		//	DataEntityMapping mapping = DataEntityMapping.GetEntityMapping (type);
		//	RelationMap relationMap = mapping.GetRelationMap ();
		//	if (selector == null) {
		//		selector = relationMap.GetDefaultSelector ();
		//	}
		//	CommandData commandData;
		//	if (mapping.HasJoinRelateModel) {
		//		QueryExpression subQuery = null;
		//		QueryExpression mainQuery = null;
		//		OrderExpression subOrder = null;
		//		OrderExpression mainOrder = null;
		//		if (query != null) {
		//			if (query.MutliQuery) {
		//				mainQuery = query;
		//			}
		//			else {
		//				subQuery = query;
		//			}
		//		}
		//		if (order != null) {
		//			if (order.MutliOrder) {
		//				mainOrder = order;
		//			}
		//			else {
		//				subOrder = order;
		//			}
		//		}
		//		List<JoinModel> models = relationMap.CreateJoinModels (subQuery, subOrder);
		//		commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, models, mainQuery, mainOrder, region);
		//	}
		//	else {
		//		commandData = _dataBase.Factory.CreateSelectCommand (mapping, selector, query, order, region);
		//	}
		//	IDbCommand command = commandData.CreateCommand (_dataBase);
		//	QueryState state = new QueryState ();
		//	state.SetRelationMap (relationMap);
		//	state.SetSelector (selector);
		//	return QueryDataMappingReader (mapping, command, commandData.InnerPage ? null : region, level, state);
		//}

		internal IEnumerable QueryJoinData (DataMapping mapping, ISelector selector, IJoinModel [] models, QueryExpression query, OrderExpression order, Region region, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, models, query, order, region, state);
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			QueryState queryState = new QueryState ();
			queryState.SetSelector (selector);
			return QueryDataDefineReader (mapping, command, commandData.InnerPage ? null : region, level, queryState);
		}

		internal IEnumerable QuerySingleColume (DataFieldInfo fieldInfo, Type outputType, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateSelectSingleFieldCommand (fieldInfo, query, order, distinct, null, state);
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			DataDefine define = DataDefine.GetDefine (outputType);
			return QueryDataDefineReader (define, command, region, level, null);
		}

		//internal List<K> QueryColumeList<K> (DataFieldInfo fieldInfo, QueryExpression query, OrderExpression order, Region region, bool distinct, SafeLevel level)
		//{
		//	Type outputType = typeof (K);
		//	CommandData commandData = _dataBase.Factory.CreateSelectSingleFieldCommand (fieldInfo, query, order, distinct, null);
		//	List<K> list = new List<K> ();
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
		//		DataDefine define = TransferDataDefine (outputType, fieldInfo.DataField);
		//		IEnumerable ie = QueryDataReader (define, command, region, level, null);
		//		foreach (K obj in ie) {
		//			list.Add (obj);
		//		}
		//	}
		//	return list;
		//}

		//internal DataTable QueryDynamicAggregateTable (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
		//{
		//	CommandData commandData = _dataBase.Factory.CreateAggregateTableCommand (mapping, groupbys, functions, query, having, order);
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
		//		return QueryDataTable (command, null, level);
		//	}
		//}

		//internal List<T> QueryDynamicAggregateList<T> (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
		//	where T : class, new()
		//{
		//	AggregateTableMapping amapping = AggregateTableMapping.GetAggregateMapping (typeof (T));
		//	CommandData commandData = _dataBase.Factory.CreateDynamicAggregateCommand (mapping, groupbys, functions, query, having, order);
		//	List<T> list = new List<T> ();
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
		//		IEnumerable ie = QueryDataMappingReader (amapping, command, null, level, commandData.State);
		//		//list.AddRange (ie);
		//		foreach (T item in ie) {
		//			list.Add (item);
		//		}
		//	}
		//	return list;
		//}

		//internal List<T> QueryDynamicAggregateList<T> (DataEntityMapping mapping, AggregateMapping amapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
		//	where T : class
		//{
		//	CommandData commandData = _dataBase.Factory.CreateDynamicAggregateCommand (mapping, groupbys, functions, query, having, order);
		//	List<T> list = new List<T> ();
		//	using (IDbCommand command = commandData.CreateCommand (_dataBase)) {
		//		IEnumerable ie = QueryDataMappingReader (amapping, command, null, level, commandData.State);
		//		//list.AddRange (ie);
		//		foreach (T item in ie) {
		//			list.Add (item);
		//		}
		//	}
		//	return list;
		//}

		internal IEnumerable QueryDynamicAggregateEnumerable (DataEntityMapping mapping, DataMapping amapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateAggregateTableCommand (mapping, groupbys, functions, query, having, order, state);
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			return QueryDataDefineReader (amapping, command, null, level, commandData.State);
		}

		internal IEnumerable QueryDynamicAggregate (AggregateGroup group, QueryExpression query, QueryExpression having, OrderExpression order, Region region, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateAggregateTableCommand (group.EntityMapping, group.GetAggregateDataFieldInfos (), query, having, order, region, state);
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			return QueryDataDefineReader (group.AggregateMapping, command, commandData.InnerPage ? null : region, level, commandData.State);
		}

		internal object SelectEntityDataSingle (DataEntityMapping mapping, QueryExpression query, OrderExpression order, int index, SafeLevel level)
		{
			object target = null;
			Region region = new Region (index, 1);
			foreach (object obj in QueryEntityData (mapping, null, query, order, region, level)) {
				target = obj;
				break;
			}
			return target;
		}

		internal object SelectDynamicAggregateSingle (AggregateGroup group, QueryExpression query, QueryExpression having, OrderExpression order, int index, SafeLevel level)
		{
			object target = null;
			Region region = new Region (index, 1);
			foreach (object obj in QueryDynamicAggregate (group, query, having, order, region, level)) {
				target = obj;
				break;
			}
			return target;
		}

		internal object SelectJoinDataSingle (DataMapping mapping, ISelector selector, IJoinModel [] models, QueryExpression query, OrderExpression order, int index, SafeLevel level)
		{
			object target = null;
			Region region = new Region (index, 1);
			foreach (object obj in QueryJoinData (mapping, selector, models, query, order, region, level)) {
				target = obj;
				break;
			}
			return target;
		}

		internal object AggregateCount (DataEntityMapping mapping, QueryExpression query, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateAggregateCountCommand (mapping, query, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				return ExecuteScalar (command, level);
			}
		}

		internal object AggregateJoinTableCount (List<IJoinModel> models, QueryExpression query, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateAggregateJoinCountCommand (models.ToArray (), query, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				return ExecuteScalar (command, level);
			}
		}

		internal object Aggregate (DataFieldInfo field, AggregateType aggregateType, QueryExpression query, bool distinct, SafeLevel level)
		{
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateAggregateFunctionCommand (field, aggregateType, query, distinct, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				object obj = ExecuteScalar (command, level);
				if (Object.Equals (obj, DBNull.Value)) {
					return null;
				}
				else {
					return obj;
				}
			}
		}

		internal bool Exists (DataEntityMapping mapping, QueryExpression query, SafeLevel level)
		{
			bool exists = false;
			Region region = new Region (0, 1);
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			CommandData commandData = _dataBase.Factory.CreateExistsCommand (mapping, query, state);
			using (IDbCommand command = commandData.CreateCommand (_dataBase, state)) {
				//PrimitiveDataDefine pm = PrimitiveDataDefine.ParseDefine (typeof (Int32));
				DataDefine define = DataDefine.GetDefine (typeof (Int32));
				foreach (object obj in QueryDataDefineReader (define, command, region, level, null)) {
					exists = true;
				}
			}
			return exists;
		}

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
				DataParameter [] list = new DataParameter [count];
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
				DataParameter [] list = new DataParameter [count];
				int index = 0;
				foreach (IDataParameter value in command.Parameters) {
					list [index] = new DataParameter (value.ParameterName, value.Value, value.DbType.ToString (), value.Direction);
					index++;
				}
				action = string.Format ("{0}[{1},{2}]", action, start, size);
				this.output.Output (action, command.CommandText, list, command.CommandType, command.Transaction != null, level);
			}
		}

		internal virtual int [] ExecuteBulkCommands (IDbCommand [] bulkCommands, IDbCommand lastIdCommand, SafeLevel level, out object lastId)
		{
			if (level == SafeLevel.None) {
				level = SafeLevel.Default;
			}
			int [] rInts = new int [bulkCommands.Length];
			using (TransactionConnection transaction = CreateTransactionConnection (level)) {
				transaction.Open ();
				try {
					int index = 0;
					foreach (IDbCommand dbcommand in bulkCommands) {
						transaction.SetupCommand (dbcommand);
						OutputCommand ("ExecuteMultiCommands", dbcommand, level);
						rInts [index] = dbcommand.ExecuteNonQuery ();
						index++;
					}
					if (lastIdCommand != null) {
						transaction.SetupCommand (lastIdCommand);
						OutputCommand ("ExecuteBulkCommands_LastId", lastIdCommand, level);
						lastId = lastIdCommand.ExecuteScalar ();
					}
					else {
						lastId = null;
					}
					transaction.Commit ();

				}
				catch (Exception ex) {
					lastId = null;
					transaction.Rollback ();
					throw ex;
				}
			}
			return rInts;
		}

		internal virtual int [] ExecuteMultiCommands (IDbCommand [] dbcommands, SafeLevel level)
		{
			if (level == SafeLevel.None) {
				level = SafeLevel.Default;
			}
			int [] rInts = new int [dbcommands.Length];
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
				}
				catch (Exception ex) {
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
				}
				catch (Exception ex) {
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
				}
				catch (Exception ex) {
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
				}
				catch (Exception ex) {
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
				}
				catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return ds;
		}

		//internal virtual IEnumerable QueryDataReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level, object state)
		//{
		//	int start;
		//	int size;
		//	if (region != null) {
		//		start = region.Start;
		//		size = region.Size;
		//	}
		//	else {
		//		start = 0;
		//		size = int.MaxValue;
		//	}
		//	using (TransactionConnection transaction = CreateTransactionConnection (level)) {
		//		transaction.Open ();
		//		transaction.SetupCommand (dbcommand);
		//		OutputCommand ("QueryDataReader", dbcommand, level, start, size);
		//		using (IDataReader reader = dbcommand.ExecuteReader ()) {
		//			int index = 0;
		//			int count = 0;
		//			bool over = false;
		//			while (reader.Read ()) {
		//				if (over) {
		//					dbcommand.Cancel ();
		//					break;
		//				}
		//				if (index >= start) {
		//					count++;
		//					object item = source.LoadData (this, reader, state);
		//					if (count >= size) {
		//						over = true;
		//					}
		//					yield return item;
		//				}
		//				index++;
		//			}
		//		}
		//		transaction.Commit ();
		//	}
		//}

		internal virtual IEnumerable QueryDataDefineReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level, object state)
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
				OutputCommand ("QueryDataDefineReader", dbcommand, level, start, size);
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

		//internal virtual IEnumerable<T> QueryDataMappingReader<T> (DataMapping source, IDbCommand dbcommand, Region region, SafeLevel level, object state)
		//	where T : class, new()
		//{
		//	int start;
		//	int size;
		//	if (region != null) {
		//		start = region.Start;
		//		size = region.Size;
		//	}
		//	else {
		//		start = 0;
		//		size = int.MaxValue;
		//	}
		//	using (TransactionConnection transaction = CreateTransactionConnection (level)) {
		//		transaction.Open ();
		//		transaction.SetupCommand (dbcommand);
		//		OutputCommand ("QueryDataMappingReader", dbcommand, level, start, size);
		//		using (IDataReader reader = dbcommand.ExecuteReader ()) {
		//			int index = 0;
		//			int count = 0;
		//			bool over = false;
		//			while (reader.Read ()) {
		//				if (over) {
		//					dbcommand.Cancel ();
		//					break;
		//				}
		//				if (index >= start) {
		//					count++;
		//					object item = source.LoadData (this, reader, state);
		//					if (count >= size) {
		//						over = true;
		//					}
		//					yield return item as T;
		//				}
		//				index++;
		//			}
		//		}
		//		transaction.Commit ();
		//	}
		//}


		#endregion

		#region 基础方法

		internal TransactionConnection CreateTransactionConnection (SafeLevel level)
		{
			IDbConnection connection = _dataBase.CreateConnection (_connectionString);
			return new TransactionConnection (connection, level);
		}

		/// <summary>
		/// Creates the trans data context.
		/// </summary>
		/// <returns>The trans data context.</returns>
		public TransDataContext CreateTransDataContext ()
		{
			TransDataContext context = new TransDataContext (_connectionString, _configName, _dataBase);
			if (this.output != null) {
				context.output = this.output;
			}
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
		/// Creates the sql string executor.
		/// </summary>
		/// <returns>The sql string executor.</returns>
		/// <param name="sqlString">Sql string.</param>
		/// <param name="param">Parameter.</param>
		/// <param name="level">Level.</param>
		public SqlExecutor CreateSqlStringExecutor (string sqlString, DataParameter [] param, SafeLevel level)
		{
			SqlExecutor executor = new SqlExecutor (sqlString, param, CommandType.Text, level, this);
			return executor;
		}

		/// <summary>
		/// Creates the sql string executor.
		/// </summary>
		/// <returns>The sql string executor.</returns>
		/// <param name="sqlString">Sql string.</param>
		/// <param name="param">Parameter.</param>
		public SqlExecutor CreateSqlStringExecutor (string sqlString, DataParameter [] param)
		{
			return CreateSqlStringExecutor (sqlString, param, SafeLevel.Default);
		}

		/// <summary>
		/// Creates the sql string executor.
		/// </summary>
		/// <returns>The sql string executor.</returns>
		/// <param name="sqlString">Sql string.</param>
		/// <param name="level">Level.</param>
		public SqlExecutor CreateSqlStringExecutor (string sqlString, SafeLevel level)
		{
			return CreateSqlStringExecutor (sqlString, null, level);
		}

		/// <summary>
		/// Creates the sql string executor.
		/// </summary>
		/// <returns>The sql string executor.</returns>
		/// <param name="sqlString">Sql string.</param>
		public SqlExecutor CreateSqlStringExecutor (string sqlString)
		{
			return CreateSqlStringExecutor (sqlString, null, SafeLevel.Default);
		}

		/// <summary>
		/// Creates the store procedure executor.
		/// </summary>
		/// <returns>The store procedure executor.</returns>
		/// <param name="storeProcedure">Store procedure.</param>
		/// <param name="param">Parameter.</param>
		/// <param name="level">Level.</param>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, DataParameter [] param, SafeLevel level)
		{
			SqlExecutor executor = new SqlExecutor (storeProcedure, param, CommandType.StoredProcedure, level, this);
			return executor;
		}

		/// <summary>
		/// Creates the store procedure executor.
		/// </summary>
		/// <returns>The store procedure executor.</returns>
		/// <param name="storeProcedure">Store procedure.</param>
		/// <param name="param">Parameter.</param>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, DataParameter [] param)
		{
			return CreateStoreProcedureExecutor (storeProcedure, param, SafeLevel.Default);
		}

		/// <summary>
		/// Creates the store procedure executor.
		/// </summary>
		/// <returns>The store procedure executor.</returns>
		/// <param name="storeProcedure">Store procedure.</param>
		/// <param name="level">Level.</param>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure, SafeLevel level)
		{
			return CreateStoreProcedureExecutor (storeProcedure, null, level);
		}

		/// <summary>
		/// Creates the store procedure executor.
		/// </summary>
		/// <returns>The store procedure executor.</returns>
		/// <param name="storeProcedure">Store procedure.</param>
		public SqlExecutor CreateStoreProcedureExecutor (string storeProcedure)
		{
			return CreateStoreProcedureExecutor (storeProcedure, null, SafeLevel.Default);
		}

		#endregion

		#region 静态函数

		//private static DataDefine TransferDataDefine (Type type, DataFieldMapping fieldMapping)
		//{
		//	PrimitiveFieldMapping pm = fieldMapping as PrimitiveFieldMapping;
		//	if (pm != null) {
		//		PrimitiveDataDefine pd = PrimitiveDataDefine.ParseDefine (type, pm);
		//		return pd;
		//	}
		//	EnumFieldMapping em = fieldMapping as EnumFieldMapping;
		//	if (em != null) {
		//		EnumDataDefine ed = EnumDataDefine.ParseDefine (type, em);
		//		return ed;
		//	}
		//	throw new LightDataException (RE.UnsupportDataDefineType);
		//}

		//private static IList CreateList (Type type)
		//{
		//	IList items;
		//	Type itemstype = Type.GetType ("System.Collections.Generic.List`1");
		//	itemstype = itemstype.MakeGenericType (type);
		//	items = (IList)Activator.CreateInstance (itemstype);
		//	return items;
		//}

		#endregion



		#region single relate

		/// <summary>
		/// Queries the collection relate enumerable.
		/// </summary>
		/// <returns>The collection relate enumerable.</returns>
		/// <param name="type">Type.</param>
		/// <param name="selector">Selector.</param>
		/// <param name="query">Query.</param>
		/// <param name="owner">Owner.</param>
		/// <param name="fieldPaths">Field paths.</param>
		internal IEnumerable QueryCollectionRelateData (Type type, QueryExpression query, object owner, string [] fieldPaths)
		{
			DataEntityMapping mapping = DataEntityMapping.GetEntityMapping (type);
			RelationMap relationMap = mapping.GetRelationMap ();
			//if (selector == null) {
			//selector = relationMap.GetDefaultSelector ();
			//}
			ISelector selector = relationMap.CreateExceptSelector (fieldPaths);
			CommandData commandData;
			CreateSqlState state = new CreateSqlState (_dataBase.Factory);
			if (mapping.HasJoinRelateModel) {
				EntityJoinModel [] models = relationMap.CreateJoinModels (query, null);
				commandData = _dataBase.Factory.CreateSelectJoinTableCommand (selector, models, null, null, null, state);
			}
			else {
				commandData = _dataBase.Factory.CreateSelectCommand (mapping, selector, query, null, null, state);
			}
			IDbCommand command = commandData.CreateCommand (_dataBase, state);
			QueryState queryState = new QueryState ();
			queryState.SetRelationMap (relationMap);
			queryState.SetSelector (selector);
			if (fieldPaths != null && fieldPaths.Length > 0) {
				foreach (string fieldPath in fieldPaths) {
					queryState.SetExtendData (fieldPath, owner);
				}
			}
			return QueryDataDefineReader (mapping, command, null, SafeLevel.Default, queryState);
		}

		#endregion

	}
}