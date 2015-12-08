using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
	internal delegate string GetAliasHandler (object obj);

	abstract class CommandFactory
	{
		protected virtual string CreateCustomFiledName ()
		{
			return string.Format ("field_{0}", Guid.NewGuid ().ToString ("N").Substring (0, 8));
		}

		public virtual string CreateTempParamName ()
		{
			return string.Format ("_param_{0}_", Guid.NewGuid ().ToString ("N"));
		}

		protected string _wildcards = "%";

		Regex _paramNameRegex = new Regex ("_param_[a-zA-Z0-9]{32}_", RegexOptions.Compiled);

		Dictionary<QueryPredicate, string> _queryPredicateDict = new Dictionary<QueryPredicate, string> ();

		Dictionary<QueryCollectionPredicate, string> _queryCollectionPredicateDict = new Dictionary<QueryCollectionPredicate, string> ();

		Dictionary<JoinType, string> _joinCollectionPredicateDict = new Dictionary<JoinType, string> ();

		protected void InitialPredicate ()
		{
			_queryPredicateDict [QueryPredicate.Eq] = "=";
			_queryPredicateDict [QueryPredicate.Gt] = ">";
			_queryPredicateDict [QueryPredicate.GtEq] = ">=";
			_queryPredicateDict [QueryPredicate.Lt] = "<";
			_queryPredicateDict [QueryPredicate.LtEq] = "<=";
			_queryPredicateDict [QueryPredicate.NotEq] = "<>";

			_queryCollectionPredicateDict [QueryCollectionPredicate.In] = "in";
			_queryCollectionPredicateDict [QueryCollectionPredicate.NotIn] = "not in";
			_queryCollectionPredicateDict [QueryCollectionPredicate.GtAll] = "> all";
			_queryCollectionPredicateDict [QueryCollectionPredicate.LtAll] = "< all";
			_queryCollectionPredicateDict [QueryCollectionPredicate.GtAny] = "> any";
			_queryCollectionPredicateDict [QueryCollectionPredicate.LtAny] = "< any";

			_joinCollectionPredicateDict [JoinType.Default] = "join";
			_joinCollectionPredicateDict [JoinType.InnerJoin] = "inner join";
			_joinCollectionPredicateDict [JoinType.LeftJoin] = "left join";
			_joinCollectionPredicateDict [JoinType.RightJoin] = "right join";
		}

		string GetQueryPredicate (QueryPredicate predicate)
		{
			if (_queryPredicateDict.ContainsKey (predicate)) {
				return _queryPredicateDict [predicate];
			}
			else {
				throw new LightDataException (string.Format (RE.UnSupportPredicate, predicate));
			}
		}

		string GetQueryCollectionPredicate (QueryCollectionPredicate predicate)
		{
			if (_queryCollectionPredicateDict.ContainsKey (predicate)) {
				return _queryCollectionPredicateDict [predicate];
			}
			else {
				throw new LightDataException (string.Format (RE.UnSupportPredicate, predicate));
			}
		}

		/// <summary>
		/// 数据库对象
		/// </summary>
		protected Database _database = null;

		/// <summary>
		/// 是否支持内分页
		/// </summary>
		protected bool _canInnerPage = false;

		/// <summary>
		/// 是否支持内分页
		/// </summary>
		public bool CanInnerPager {
			get {
				return _canInnerPage;
			}
		}

		/// <summary>
		/// 生成SQL命令
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="dataParameters">参数数组</param>
		/// <returns>SQL命令</returns>
		protected virtual IDbCommand BuildCommand (string sql, DataParameter[] dataParameters)
		{
			IDataParameter[] idataParameters = null;
			Dictionary<string, string> paramReplaceDict = null;
			if (dataParameters != null && dataParameters.Length > 0) {
				idataParameters = new IDataParameter[dataParameters.Length];
				paramReplaceDict = new Dictionary<string, string> ();
				int length = dataParameters.Length;
				for (int i = 0; i < length; i++) {
					DataParameter dp = dataParameters [i];
					IDataParameter idp = _database.CreateParameter ("P" + i, dp.Value, dp.DbType, dp.Direction);
					idataParameters [i] = idp;
					paramReplaceDict.Add (dp.ParameterName, idp.ParameterName);
				}
			}
			if (idataParameters != null) {
				sql = _paramNameRegex.Replace (sql, new MatchEvaluator (delegate(Match match) {
					string value = match.Value;
					if (paramReplaceDict.ContainsKey (value)) {
						return paramReplaceDict [value];
					}
					else {
						return value;
					}
				}));
			}
			IDbCommand command = _database.CreateCommand (sql);
			command.CommandType = CommandType.Text;
			if (idataParameters != null) {
				foreach (IDataParameter param in idataParameters) {
					command.Parameters.Add (param);
				}
			}
			return command;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="database">数据库对象</param>
		protected CommandFactory (Database database)
		{
			_database = database;
			InitialPredicate ();
		}

		#region 增删改操作命令

		/// <summary>
		/// 从字段映射中获取DataParameter集合
		/// </summary>
		/// <param name="mappings">字段映射列表</param>
		/// <param name="source">数据对象</param>
		/// <returns>DataParameter集合</returns>
		protected virtual List<DataParameter> GetDataParameters (IEnumerable<FieldMapping> mappings, object source)
		{
			List<DataParameter> paramList = new List<DataParameter> ();
			foreach (DataFieldMapping field in mappings) {
				if (field is PrimitiveFieldMapping) {
					PrimitiveFieldMapping primitiveFieldMapping = field as PrimitiveFieldMapping;

					object obj = primitiveFieldMapping.Handler.Get (source);
					if (primitiveFieldMapping.SpecifiedHandler != null) {
						bool hasdata = Convert.ToBoolean (primitiveFieldMapping.SpecifiedHandler.Get (source));
						if (!hasdata) {
							obj = null;
						}
					}
					if (obj == null && !primitiveFieldMapping.IsNullable) {
						throw new LightDataException (string.Format (RE.DataValueIsNotAllowEmply, primitiveFieldMapping.Name));
					}
					DataParameter dataParameter = new DataParameter (field.Name, field.ToColumn (obj), field.DBType, ParameterDirection.Input);
					paramList.Add (dataParameter);
				}
				else if (field is EnumFieldMapping) {
					EnumFieldMapping enumFieldMapping = field as EnumFieldMapping;
					object obj = enumFieldMapping.Handler.Get (source);
					if (enumFieldMapping.SpecifiedHandler != null) {
						bool hasdata = Convert.ToBoolean (enumFieldMapping.SpecifiedHandler.Get (source));
						if (!hasdata) {
							obj = null;
						}
					}
					if (obj == null && !enumFieldMapping.IsNullable) {
						throw new LightDataException (string.Format (RE.DataValueIsNotAllowEmply, enumFieldMapping.Name));
					}
					DataParameter dataParameter = new DataParameter (field.Name, field.ToColumn (obj), field.DBType, ParameterDirection.Input);
					paramList.Add (dataParameter);
				}
				else if (field is ComplexFieldMapping) {
					ComplexFieldMapping complexFieldMapping = field as ComplexFieldMapping;
					object obj = complexFieldMapping.Handler.Get (source);
					if (obj == null) {
						throw new LightDataException (string.Format (RE.DataValueIsNotAllowEmply, complexFieldMapping.Name));
					}
					List<DataParameter> subParamList = GetDataParameters (complexFieldMapping.GetFieldMappings (), obj);
					paramList.AddRange (subParamList);
				}
			}
			return paramList;
		}

		/// <summary>
		/// 生成数据新增命令
		/// </summary>
		/// <param name="entity">数据实体</param>
		/// <returns>新增命令对象</returns>
		public virtual IDbCommand CreateInsertCommand (object entity)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (entity.GetType ());
			List<FieldMapping> fields = new List<FieldMapping> ();
			fields.AddRange (mapping.GetFieldMappings ());
			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			List<DataParameter> paramList = GetDataParameters (fields, entity);

			IDataParameter[] dataParameters = new IDataParameter[paramList.Count];
			string[] insertList = new string[paramList.Count];
			string[] valuesList = new string[paramList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in paramList) {
				IDataParameter param = _database.CreateParameter ("P" + index, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				dataParameters [index] = param;
				insertList [index] = CreateDataFieldSql (dataParameter.ParameterName);
				valuesList [index] = param.ParameterName;
				index++;
			}
			string insert = string.Join (",", insertList);
			string values = string.Join (",", valuesList);
			string sql = string.Format ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			IDbCommand command = _database.CreateCommand (sql);
			command.CommandType = CommandType.Text;
			foreach (IDataParameter param in dataParameters) {
				command.Parameters.Add (param);
			}
			return command;
		}

		/// <summary>
		/// 生成数据更新命令
		/// </summary>
		/// <param name="entity">数据实体</param>
		/// <param name="updatefieldNames">需更新的数据字段</param>
		/// <returns>更新命令对象</returns>
		public virtual IDbCommand CreateUpdateCommand (object entity, string[] updatefieldNames)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (entity.GetType ());
			if (mapping.PrimaryKeyFields == null || mapping.PrimaryKeyFields.Length == 0) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}

			List<FieldMapping> fields = null;

			if (updatefieldNames != null && updatefieldNames.Length > 0) {
				List<FieldMapping> updateFields = new List<FieldMapping> ();
				foreach (string name in updatefieldNames) {
					FieldMapping fm = mapping.FindFieldMapping (name);

					if (fm != null && !updateFields.Contains (fm)) {
						updateFields.Add (fm);
					}
				}
				fields = updateFields;
			}

			if (fields == null) {
				fields = new List<FieldMapping> ();
				fields.AddRange (mapping.GetFieldMappings ());
			}

			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			foreach (DataFieldMapping primaryField in mapping.PrimaryKeyFields) {
				fields.Remove (primaryField);
			}

			List<DataParameter> columnList = GetDataParameters (fields, entity);
			List<DataParameter> primaryList = GetDataParameters (mapping.PrimaryKeyFields, entity);
			if (columnList.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			IDataParameter[] dataParameters = new IDataParameter[columnList.Count + primaryList.Count];
			string[] updateList = new string[columnList.Count];
			string[] whereList = new string[primaryList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in columnList) {
				IDataParameter param = _database.CreateParameter ("P" + index, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				dataParameters [index] = param;
				updateList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), param.ParameterName);
				index++;
			}
			foreach (DataParameter dataParameter in primaryList) {
				IDataParameter param = _database.CreateParameter ("P" + index, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				dataParameters [index] = param;
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), param.ParameterName);
				index++;
			}
			string update = string.Join (",", updateList);
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("update {0} set {1} where {2}", CreateDataTableSql (mapping.TableName), update, where);
			IDbCommand command = _database.CreateCommand (sql);
			command.CommandType = CommandType.Text;
			foreach (IDataParameter param in dataParameters) {
				command.Parameters.Add (param);
			}
			return command;
		}

		/// <summary>
		/// 生成数据删除命令
		/// </summary>
		/// <param name="entity">数据实体</param>
		/// <returns>删除命令对象</returns>
		public virtual IDbCommand CreateDeleteCommand (object entity)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (entity.GetType ());
			if (mapping.PrimaryKeyFields == null || mapping.PrimaryKeyFields.Length == 0) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			List<DataParameter> primaryList = GetDataParameters (mapping.PrimaryKeyFields, entity);
			IDataParameter[] dataParameters = new IDataParameter[primaryList.Count];
			string[] whereList = new string[primaryList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in primaryList) {
				IDataParameter param = _database.CreateParameter ("P" + index, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				dataParameters [index] = param;
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), param.ParameterName);
				index++;
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("delete from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			IDbCommand command = _database.CreateCommand (sql);
			command.CommandType = CommandType.Text;
			foreach (IDataParameter param in dataParameters) {
				command.Parameters.Add (param);
			}
			return command;
		}

		public virtual IDbCommand CreateEntityExistsCommand (object entity)
		{
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (entity.GetType ());
			if (mapping.PrimaryKeyFields == null || mapping.PrimaryKeyFields.Length == 0) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			List<DataParameter> primaryList = GetDataParameters (mapping.PrimaryKeyFields, entity);
			IDataParameter[] dataParameters = new IDataParameter[primaryList.Count];
			string[] whereList = new string[primaryList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in primaryList) {
				IDataParameter param = _database.CreateParameter ("P" + index, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
				dataParameters [index] = param;
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), param.ParameterName);
				index++;
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("select 1 from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			IDbCommand command = _database.CreateCommand (sql);
			command.CommandType = CommandType.Text;
			foreach (IDataParameter param in dataParameters) {
				command.Parameters.Add (param);
			}
			return command;
		}

		#endregion

		#region 主命令语句块

		public virtual string GetSelectString (DataEntityMapping mapping)
		{
			string[] names = mapping.GetFieldNames ();
			return GetSelectString (names);
		}

		public virtual string GetSelectString (string[] names)
		{
			for (int i = 0; i < names.Length; i++) {
				names [i] = CreateDataFieldSql (names [i]);
			}
			return string.Join (",", names);
		}

		public virtual string GetQueryString (DataEntityMapping mapping, QueryExpression query, out DataParameter[] parameters)
		{
			return GetQueryString (mapping, query, false, out parameters);
		}

		public virtual string GetQueryString (DataEntityMapping mapping, QueryExpression query, bool fullFieldName, out DataParameter[] parameters)
		{
			string queryString = null;
			parameters = null;
			if (query != null) {
				//				if (!query.IgnoreConsistency && !mapping.Equals (query.TableMapping)) {
				//					throw new LightDataException (RE.DataMappingIsNotMatchQueryExpression);
				//				}
				queryString = string.Format ("where {0}", query.CreateSqlString (this, fullFieldName, out parameters));
			}
			return queryString;
		}

		public virtual string GetHavingString (DataEntityMapping mapping, AggregateHavingExpression having, out DataParameter[] parameters, Dictionary<string, AggregateFunction> aggregateFunctionDictionary)
		{
			string havingString = null;
			parameters = null;
			if (having != null) {
//				if (!having.IgnoreConsistency && !mapping.Equals (having.TableMapping)) {
//					throw new LightDataException (RE.DataMappingIsNotMatchAggregationExpression);
//				}
				havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate(object obj) {
					string alias = null;
					if (obj is AggregateFunction) {
						foreach (KeyValuePair<string, AggregateFunction> kv in aggregateFunctionDictionary) {
							if (Object.ReferenceEquals (obj, kv.Value)) {
								alias = kv.Key;
								break;
							}
						}
					}
					else {
						throw new LightDataException (RE.UnknowOrderType);
					}
					return alias;
				})));
			}
			return havingString;
		}

		public virtual string GetOrderString (DataEntityMapping mapping, OrderExpression order)
		{
			return GetOrderString (mapping, order, false);
		}

		public virtual string GetOrderString (DataEntityMapping mapping, OrderExpression order, bool fullFieldName)
		{
			string orderString = null;
			DataParameter[] parameters = null;
			if (order != null) {
				//				if (order.IgnoreConsistency) {
				RandomOrderExpression random = order as RandomOrderExpression;
				if (random != null) {
					random.SetTableMapping (mapping);
				}
				//				}
				//				if (!order.IgnoreConsistency && !mapping.Equals (order.TableMapping)) {
				//					throw new LightDataException (RE.DataMappingIsNotMatchOrderExpression);
				//				}
				orderString = string.Format ("order by {0}", order.CreateSqlString (this, fullFieldName, out parameters));
			}
			return orderString;
		}

		public virtual string GetOrderString (DataEntityMapping mapping, OrderExpression order, out DataParameter[] parameters, Dictionary<string, DataFieldInfo> dataFieldInfoDictionary, Dictionary<string, AggregateFunction> aggregateFunctionDictionary)
		{
			string orderString = null;
			parameters = null;
			if (order != null) {
//				if (order.IgnoreConsistency) {
				RandomOrderExpression random = order as RandomOrderExpression;
				if (random != null) {
					random.SetTableMapping (mapping);
				}
//				}
//				if (!order.IgnoreConsistency && !mapping.Equals (order.TableMapping)) {
//					throw new LightDataException (RE.DataMappingIsNotMatchOrderExpression);
//				}
				orderString = string.Format ("order by {0}", order.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate(object obj) {
					string alias = null;
					if (obj is DataFieldInfo) {
						foreach (KeyValuePair<string, DataFieldInfo> kv in dataFieldInfoDictionary) {
							if (Object.ReferenceEquals (obj, kv.Value)) {
								alias = kv.Key;
								break;
							}
						}
					}
					else if (obj is AggregateFunction) {
						foreach (KeyValuePair<string, AggregateFunction> kv in aggregateFunctionDictionary) {
							if (Object.ReferenceEquals (obj, kv.Value)) {
								alias = kv.Key;
								break;
							}
						}
					}
					else {
						throw new LightDataException (RE.UnknowOrderType);
					}
					return alias;
				})));
			}
			return orderString;
		}

		//		public virtual string GetQueryString (DataEntityMapping mapping, QueryExpression query, out DataParameter[] parameters)
		//		{
		//			string queryString = null;
		//			parameters = null;
		//			if (query != null) {
		//				queryString = string.Format ("where {0}", query.CreateSqlString (this, out parameters));
		//			}
		//			return queryString;
		//		}

		public virtual string GetOnString (DataEntityMapping mapping, DataFieldExpression on)
		{
			return GetOnString (mapping, on);
		}

		public virtual string GetOnString (DataEntityMapping mapping, DataFieldExpression on, bool fullFieldName)
		{
			string onString = null;
			DataParameter[] parameters = null;
			if (on != null) {
				onString = string.Format ("on {0}", on.CreateSqlString (this, fullFieldName, out parameters));
			}
			return onString;
		}

		/// <summary>
		/// 创建查询命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="query">查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="region">查询范围,如非空则生成内分页语句</param>
		/// <returns>查询命令对象</returns>
		public virtual IDbCommand CreateSelectCommand (DataEntityMapping mapping, QueryExpression query, OrderExpression order, Region region)
		{
			if (region != null && !_canInnerPage) {
				throw new LightDataException (RE.DataBaseNotSupportInnerPage);
			}
			string select = GetSelectString (mapping);
			return this.CreateSelectBaseCommand (mapping, select, query, order, region);
		}

		public virtual IDbCommand CreateSelectSingleFieldCommand (DataFieldInfo fieldinfo, QueryExpression query, OrderExpression order, bool distinct, Region region)
		{
			if (region != null && !_canInnerPage) {
				throw new LightDataException (RE.DataBaseNotSupportInnerPage);
			}
			DataFieldMapping fieldMapping = fieldinfo.DataField;
			if (fieldMapping is PrimitiveFieldMapping || fieldMapping is EnumFieldMapping || fieldMapping is CustomFieldMapping) {
				DataEntityMapping mapping = fieldMapping.EntityMapping;
				string select = fieldinfo.CreateDataFieldSql (this);
				if (distinct) {
					select = "distinct " + select;
				}
				return CreateSelectBaseCommand (mapping, select, query, order, region);
			}
			else {
				throw new LightDataException (RE.OnlyPrimitiveFieldCanSelectSingle);
			}
		}

		/// <summary>
		/// 创建自定查询内容的命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="customSelect">查询输出的内容</param>
		/// <param name="query">查询表达式</param>
		/// <param name="order">排序表达式</param>
		/// <param name="region">查询范围</param>
		/// <returns></returns>
		protected virtual IDbCommand CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region)//, bool distinct)
		{
			StringBuilder sql = new StringBuilder ();
			DataParameter[] parameters;
			string queryString = GetQueryString (mapping, query, out parameters);
			string orderString = GetOrderString (mapping, order);
			sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));//, distinct ? "distinct " : string.Empty);
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			IDbCommand command = BuildCommand (sql.ToString (), parameters);
			return command;
		}

		public virtual IDbCommand CreateSelectJoinTableCommand (List<JoinModel> modelList, DataFieldExpression on, QueryExpression query, OrderExpression order)
		{
			return CreateSelectJoinTableCommand (modelList, on, query, order);
		}

		/// <summary>
		/// Creates the select join table command.
		/// </summary>
		/// <returns>The select join table command.</returns>
		/// <param name="customSelect">Custom select.</param>
		/// <param name="modelList">Model list.</param>
		/// <param name="on">On.</param>
		/// <param name="query">Query.</param>
		/// <param name="order">Order.</param>
		public virtual IDbCommand CreateSelectJoinTableCommand (string customSelect, List<JoinModel> modelList, DataFieldExpression on, QueryExpression query, OrderExpression order)
		{
			DataEntityMapping mapping = modelList [0].Mapping;
			List<DataParameter> listParameters = new List<DataParameter> ();
			List<string> selectList = new List<string> ();
			bool flag = false;
			StringBuilder tables = new StringBuilder ();
			foreach (JoinModel model in modelList) {
				if (model.SelectAllField) {
					foreach (string item in model.Mapping.GetFieldNames()) {
						string fieldName = CreateFullDataFieldSql (model.Mapping.TableName, item);
						selectList.Add (fieldName);
					}
				}
				else {
					DataFieldInfo[] infos = model.GetFields ();
					if (infos != null && infos.Length > 0) {
						foreach (DataFieldInfo info in infos) {
							string fieldName = info.CreateDataFieldSql (this, true);
							selectList.Add (fieldName);
						}
					}
				}
				if (flag) {
					tables.AppendFormat (" {0} ", _joinCollectionPredicateDict [model.Type]);
				}
				else {
					flag = true;
				}
				if (model.Query != null || model.Order != null) {
					DataParameter[] mparameters;
					string mqueryString = GetQueryString (model.Mapping, model.Query, out mparameters);
					string morderString = GetOrderString (model.Mapping, model.Order);
					tables.AppendFormat ("(select * from {0}", CreateDataTableSql (model.Mapping.TableName));
					if (!string.IsNullOrEmpty (mqueryString)) {
						tables.AppendFormat (" {0}", mqueryString);
					}
					if (!string.IsNullOrEmpty (morderString)) {
						tables.AppendFormat (" {0}", morderString);
					}
					tables.AppendFormat (") as {0}", CreateDataTableSql (model.Mapping.TableName));
					if (mparameters != null) {
						listParameters.AddRange (mparameters);
					}
				}
				else {
					tables.Append (CreateDataTableSql (model.Mapping.TableName));
				}
			}
			StringBuilder sql = new StringBuilder ();
			DataParameter[] parameters;
			string onString = GetOnString (mapping, on, true);
			string queryString = GetQueryString (mapping, query, true, out parameters);
			string orderString = GetOrderString (mapping, order, true);
			if (parameters != null) {
				listParameters.AddRange (parameters);
			}
			if (string.IsNullOrEmpty (customSelect)) {
				customSelect = string.Join (",", selectList);
			}
			sql.AppendFormat ("select {0} from {1}", customSelect, tables);
			if (!string.IsNullOrEmpty (onString)) {
				sql.AppendFormat (" {0}", onString);
			}
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			IDbCommand command = BuildCommand (sql.ToString (), listParameters.ToArray());
			return command;
		}

		/// <summary>
		/// 创建内容Exists查询命令
		/// </summary>
		/// <param name="mapping">数据表映射</param>
		/// <param name="query">查询表达式</param>
		/// <returns></returns>
		public virtual IDbCommand CreateExistsCommand (DataEntityMapping mapping, QueryExpression query)
		{
			Region region = null;
			if (_canInnerPage) {
				region = new Region (0, 1);
			}
			return this.CreateSelectBaseCommand (mapping, "1", query, null, region);
		}

		public virtual IDbCommand CreateAggregateCommand (DataFieldMapping fieldMapping, AggregateType aggregateType, QueryExpression query, bool distinct)
		{
			DataEntityMapping mapping = fieldMapping.EntityMapping;
			if (aggregateType != AggregateType.COUNT) {
				TypeCode code = Type.GetTypeCode (fieldMapping.ObjectType);
				if (aggregateType == AggregateType.MAX || aggregateType == AggregateType.MIN) {
					if (code == TypeCode.Char || code == TypeCode.DBNull || code == TypeCode.Object || code == TypeCode.String) {
						throw new LightDataException (RE.TheTypeOfAggregationFieldIsNotRight);
					}
				}
				else {
					if (code == TypeCode.Char || code == TypeCode.DBNull || code == TypeCode.Object || code == TypeCode.String || code == TypeCode.DateTime) {
						throw new LightDataException (RE.TheTypeOfAggregationFieldIsNotRight);
					}
				}
			}

			string function = null;
			switch (aggregateType) {
			case AggregateType.COUNT:
				function = CreateCountSql (CreateDataFieldSql (fieldMapping.Name), distinct);
				break;
			case AggregateType.SUM:
				function = CreateSumSql (CreateDataFieldSql (fieldMapping.Name), distinct);
				break;
			case AggregateType.AVG:
				function = CreateAvgSql (CreateDataFieldSql (fieldMapping.Name), distinct);
				break;
			case AggregateType.MAX:
				function = CreateMaxSql (CreateDataFieldSql (fieldMapping.Name));
				break;
			case AggregateType.MIN:
				function = CreateMinSql (CreateDataFieldSql (fieldMapping.Name));
				break;
			}
			return CreateSelectBaseCommand (mapping, function, query, null, null);//, false);
		}

		public virtual IDbCommand CreateAggregateCountCommand (DataEntityMapping mapping, QueryExpression query)
		{
			string select = CreateCountAllSql ();
			return CreateSelectBaseCommand (mapping, select, query, null, null);//, false);
		}

		public virtual IDbCommand CreateAggregateJoinCountCommand (List<JoinModel> modelList, DataFieldExpression on, QueryExpression query)
		{
			string select = CreateCountAllSql ();
			return CreateSelectJoinTableCommand (select, modelList, on, query, null);
		}

		public virtual IDbCommand CreateDeleteMassCommand (DataTableEntityMapping mapping, QueryExpression query)
		{
			StringBuilder sql = new StringBuilder ();
			DataParameter[] parameters;
			string queryString = GetQueryString (mapping, query, out parameters);

			sql.AppendFormat ("delete from {0}", CreateDataTableSql (mapping.TableName));
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			IDbCommand command = BuildCommand (sql.ToString (), parameters);
			return command;
		}

		public virtual IDbCommand CreateSelectIntoCommand (DataTableEntityMapping insertMapping, string[] insertFiledNames, DataTableEntityMapping selectMapping, string[] selectFiledNames, QueryExpression query, OrderExpression order)
		{
			StringBuilder sql = new StringBuilder ();
			if (insertFiledNames == null) {
				insertFiledNames = insertMapping.GetFieldNames ();
			}
			if (selectFiledNames == null) {
				selectFiledNames = selectMapping.GetFieldNames ();
			}
			if (insertFiledNames.Length != selectFiledNames.Length) {
				throw new LightDataException (RE.SelectFiledsCountNotEquidInsertFiledCount);
			}
			string insert = GetSelectString (insertFiledNames);
			string select = GetSelectString (selectFiledNames);
			DataParameter[] parameters;
			string queryString = GetQueryString (selectMapping, query, out parameters);
			string orderString = GetOrderString (selectMapping, order);
			sql.AppendFormat ("insert into {0}({1})", CreateDataTableSql (insertMapping.TableName), insert);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (selectMapping.TableName));
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			IDbCommand command = BuildCommand (sql.ToString (), parameters);
			return command;
		}

		public virtual IDbCommand CreateUpdateMassCommand (DataTableEntityMapping mapping, UpdateSetValue[] updateSetValues, QueryExpression query)
		{
			List<DataParameter> parameterlist = new List<DataParameter> ();

			StringBuilder sql = new StringBuilder ();
			DataParameter[] parameters;
			string queryString = GetQueryString (mapping, query, out parameters);

			int length = updateSetValues.Length;
			DataParameter[] setparameters = new DataParameter[length];
			string[] setList = new string[length];

			for (int i = 0; i < length; i++) {
				if (!mapping.Equals (updateSetValues [i].DataField.DataField.EntityMapping)) {
					throw new LightDataException (RE.UpdateFieldTypeIsError);
				}
				setparameters [i] = updateSetValues [i].CreateDataParameter (this);
				setList [i] = string.Format ("{0}={1}", updateSetValues [i].DataField.CreateDataFieldSql (this), setparameters [i].ParameterName);
			}
			string setString = string.Join (",", setList);
			sql.AppendFormat ("update {0} set {1}", CreateDataTableSql (mapping.TableName), setString);
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			parameterlist.AddRange (parameters);
			parameterlist.AddRange (setparameters);
			IDbCommand command = BuildCommand (sql.ToString (), parameterlist.ToArray ());
			return command;
		}

		public virtual IDbCommand CreateDynamicAggregateCommand (DataEntityMapping mapping, Dictionary<string, DataFieldInfo> dataFieldInfoDictionary, Dictionary<string, AggregateFunction> aggregateFunctionDictionary, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		{
			if (dataFieldInfoDictionary == null || dataFieldInfoDictionary.Count == 0) {
				throw new LightDataException (RE.DynamicAggregateFieldIsNotExists);
			}
			StringBuilder sql = new StringBuilder ();

			List<DataParameter> parameterlist = new List<DataParameter> ();
			string[] selectList = new string[dataFieldInfoDictionary.Count + aggregateFunctionDictionary.Count];
			string[] groupbyList = new string[dataFieldInfoDictionary.Count];
			int index = 0;

			foreach (KeyValuePair<string, DataFieldInfo> kv in dataFieldInfoDictionary) {
				if (!mapping.Equals (kv.Value.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				string groupbyField = kv.Value.CreateDataFieldSql (this);
				string aliasName = CreateDataFieldSql (kv.Key);
				string selectField = string.Format ("{0} as {1}", groupbyField, aliasName);
				selectList [index] = selectField;
				groupbyList [index] = groupbyField;
				index++;
			}

			foreach (KeyValuePair<string, AggregateFunction> kv in aggregateFunctionDictionary) {
				if (kv.Value.TableMapping != null && !mapping.Equals (kv.Value.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}

				DataParameter[] aggparameters = null;
				string aggField = kv.Value.CreateSqlString (this, false, out aggparameters);
				string aliasName = CreateDataFieldSql (kv.Key);
				string selectField = string.Format ("{0} as {1}", aggField, aliasName);
				selectList [index] = selectField;
				parameterlist.AddRange (aggparameters);
				index++;
			}
			string select = string.Join (",", selectList);
			string groupby = string.Join (",", groupbyList);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

			DataParameter[] queryparameters = null;
			string queryString = GetQueryString (mapping, query, out queryparameters);
			DataParameter[] havingparameters = null;
			string havingString = GetHavingString (mapping, having, out havingparameters, aggregateFunctionDictionary);
			DataParameter[] orderbyparameters = null;
			string orderString = GetOrderString (mapping, order, out orderbyparameters, dataFieldInfoDictionary, aggregateFunctionDictionary);

			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
				parameterlist.AddRange (queryparameters);
			}

			sql.AppendFormat (" group by {0}", groupby);

			if (!string.IsNullOrEmpty (havingString)) {
				sql.AppendFormat (" {0}", havingString);
				parameterlist.AddRange (havingparameters);
			}

			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
				parameterlist.AddRange (orderbyparameters);
			}

			IDbCommand command = BuildCommand (sql.ToString (), parameterlist.ToArray ());
			return command;
		}

		public virtual IDbCommand[] CreateBulkInsertCommand (Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException ("entitys");
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			object tmpEntity = entitys.GetValue (0);
			DataTableEntityMapping mapping = DataMapping.GetTableMapping (tmpEntity.GetType ());
			List<FieldMapping> fields = new List<FieldMapping> ();
			int totalCount = entitys.Length;
			fields.AddRange (mapping.GetFieldMappings ());
			if (mapping.IdentityField != null) {
				fields.Remove (mapping.IdentityField);
			}
			StringBuilder totalSql = new StringBuilder ();

			List<DataParameter> paramList = GetDataParameters (fields, tmpEntity);
			List<string> insertList = new List<string> ();
			foreach (DataParameter dataParameter in paramList) {
				insertList.Add (CreateDataFieldSql (dataParameter.ParameterName));
			}
			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), string.Join (",", insertList));

			int createCount = 0;
			int totalCreateCount = 0;

			IDbCommand command = _database.CreateCommand ();
			int paramCount = 0;
			List<IDbCommand> commands = new List<IDbCommand> ();
			foreach (object entity in entitys) {
				paramList = GetDataParameters (fields, entity);
				string[] valueList = new string[paramList.Count];
				int index = 0;
				foreach (DataParameter dataParameter in paramList) {
					IDataParameter param = _database.CreateParameter ("P" + paramCount, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
					command.Parameters.Add (param);
					valueList [index] = param.ParameterName;
					index++;
					paramCount++;
				}
				string value = string.Join (",", valueList);
				totalSql.AppendFormat ("{0}values({1});", insertsql, value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					command.CommandText = totalSql.ToString ();
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					command = _database.CreateCommand ();
					createCount = 0;
					paramCount = 0;
					totalSql = new StringBuilder ();
				}
			}
			return commands.ToArray ();
		}

		public virtual IDbCommand CreateIdentityCommand (DataTableEntityMapping mapping)
		{
			IDbCommand command = null;
			string sql = CreateIdentitySql (mapping);
			if (!string.IsNullOrEmpty (sql)) {
				command = BuildCommand (sql, null);
			}
			return command;
		}

		#endregion

		#region 基本语句块

		public virtual string CreateCatchExpressionSql (string expressionString1, string expressionString2, CatchOperatorsType operatorType)
		{
			return string.Format ("({0} {2} {1})", expressionString1, expressionString2, operatorType.ToString ().ToLower ());
		}

		public virtual string CreateCatchExpressionSql (string[] expressionStrings)
		{
			return string.Join (",", expressionStrings);
		}

		public virtual string CreateSingleParamSql (string fieldName, QueryPredicate predicate, bool isReverse, DataParameter dataParameter)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryPredicate (predicate);
			if (!isReverse) {
				sb.AppendFormat ("{0}{2}{1}", fieldName, dataParameter.ParameterName, op);
			}
			else {
				sb.AppendFormat ("{1}{2}{0}", fieldName, dataParameter.ParameterName, op);
			}
			return sb.ToString ();
		}

		public virtual string CreateRelationTableSql (string fieldName, QueryPredicate predicate, bool isReverse, string relationFieldName)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryPredicate (predicate);
			if (!isReverse) {
				sb.AppendFormat ("{0}{2}{1}", fieldName, relationFieldName, op);
			}
			else {
				sb.AppendFormat ("{1}{2}{0}", fieldName, relationFieldName, op);
			}
			return sb.ToString ();
		}

		public virtual string CreateCollectionParamsQuerySql (string fieldName, QueryCollectionPredicate predicate, List<DataParameter> dataParameters)
		{
			string op = GetQueryCollectionPredicate (predicate);
			if (dataParameters.Count == 0) {
				throw new LightDataException (RE.EnumerableLengthNotAllowIsZero);
			}
			int i = 0;
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {1} (", fieldName, op);
			foreach (DataParameter dataParameter in dataParameters) {
				if (i > 0)
					sb.Append (",");
				sb.Append (dataParameter.ParameterName);
				i++;
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public virtual string CreateExistsQuerySql (string queryTableName, string whereString, bool isNot)
		{
			return string.Format ("{2}exists (select 1 from {0} where {1})", queryTableName, whereString, isNot ? "not " : string.Empty);
		}

		public virtual string CreateSubQuerySql (string fieldName, QueryCollectionPredicate predicate, string queryfieldName, string queryTableName, string whereString)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryCollectionPredicate (predicate);
			sb.AppendFormat ("{0} {3} (select {1} from {2}", fieldName, queryfieldName, queryTableName, op);
			if (!string.IsNullOrEmpty (whereString)) {
				sb.AppendFormat (" where {0}", whereString);
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public virtual string CreateBetweenParamsQuerySql (string fieldName, bool isNot, DataParameter fromParam, DataParameter toParam)
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {3}between {1} and {2}", fieldName, fromParam.ParameterName, fromParam.ParameterName, isNot ? string.Empty : "not ");
			return sb.ToString ();
		}

		public virtual string CreateCollectionMatchQuerySql (string fieldName, bool isReverse, bool isMatch, bool isNot, List<DataParameter> dataParameters)
		{
			if (dataParameters.Count == 0) {
				throw new LightDataException (RE.EnumerableLengthNotAllowIsZero);
			}
			int i = 0;
			int length = dataParameters.Count;
			StringBuilder sb = new StringBuilder ();
			if (length > 1) {
				sb.Append ("(");
			}
			foreach (DataParameter item in dataParameters) {
				if (i > 0)
					sb.Append (" or ");
				if (!isReverse) {
					sb.AppendFormat ("{0} {2}like {1}", fieldName, item.ParameterName, isNot ? "not " : string.Empty);
					if (isMatch) {
						item.Value = string.Format ("{1}{0}{1}", item.Value, _wildcards);
					}
				}
				else {
					sb.AppendFormat ("{1} {2}like {0}", fieldName, item.ParameterName, isNot ? "not " : string.Empty);
				}
				i++;
			}
			if (length > 1) {
				sb.Append (")");
			}
			return sb.ToString ();
		}

		public virtual string CreateNullQuerySql (string fieldName, bool isNull)
		{
			return string.Format ("{0} is{1} null", fieldName, isNull ? string.Empty : " not");
		}

		public virtual string CreateBooleanQuerySql (string fieldName, bool isTrue)
		{
			return string.Format ("{0}={1}", fieldName, isTrue ? "1" : "0");
		}

		public virtual string CreateOrderBySql (string fieldName, OrderType orderType)
		{
			return string.Format ("{0} {1}", fieldName, orderType.ToString ().ToLower ());
		}

		public virtual string CreateRandomOrderBySql (DataEntityMapping mapping, bool fullFieldName)
		{
			return "newid()";
		}

		protected virtual string CreateIdentitySql (DataTableEntityMapping mapping)
		{
			if (mapping.IdentityField != null) {
				return "select @@Identity;";
			}
			else {
				return string.Empty;
			}
		}

		public virtual string CreateCountAllSql ()
		{
			return "count(1)";
		}

		public virtual string CreateConditionCountSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("count({2}case when {0} then {1} else null end)", expressionSql, !string.IsNullOrEmpty (fieldName) ? fieldName : "1", isDistinct ? "distinct " : "");
		}

		public virtual string CreateCountSql (string fieldName, bool isDistinct)
		{
			return string.Format ("count({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateSumSql (string fieldName, bool isDistinct)
		{
			return string.Format ("sum({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateConditionSumSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("sum({2}case when {0} then {1} else 0 end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateAvgSql (string fieldName, bool isDistinct)
		{
			return string.Format ("avg({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateConditionAvgSql (string expressionSql, string fieldName, bool isDistinct)
		{
			return string.Format ("avg({2}case when {0} then {1} else 0 end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateMaxSql (string fieldName)
		{
			return string.Format ("max({0})", fieldName);
		}

		public virtual string CreateMinSql (string fieldName)
		{
			return string.Format ("min({0})", fieldName);
		}

		public virtual string CreateDataFieldSql (string fieldName)
		{
			return fieldName;
		}

		public virtual string CreateDataTableSql (string tableName)
		{
			return tableName;
		}

		public virtual string CreateDataTableSql (DataEntityMapping table)
		{
			return CreateDataTableSql (table.TableName);
		}

		public virtual string CreateFullDataFieldSql (string tableName, string fieldName)
		{
			return string.Format ("{0}.{1}", CreateDataTableSql (tableName), CreateDataFieldSql (fieldName));
		}

		public virtual string CreateMatchSql (string field, bool left, bool right)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateDateSql (string field, string format)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateYearSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateMonthSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateDaySql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateHourSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateMinuteSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateSecondSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateWeekSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateWeekDaySql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateLengthSql (string field)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreateSubStringSql (string field, int start, int size)
		{
			throw new NotImplementedException ();
		}

		public virtual string CreatePlusSql (string field, object value)
		{
			return string.Format ("{0}+{1}", field, value);
		}

		public virtual string CreateMinusSql (string field, object value)
		{
			return string.Format ("{0}-{1}", field, value);
		}

		public virtual string CreateMultiplySql (string field, object value)
		{
			return string.Format ("{0}*{1}", field, value);
		}

		public virtual string CreateDividedSql (string field, object value)
		{
			return string.Format ("{0}/{1}", field, value);
		}

		public virtual string CreateModSql (string field, object value)
		{
			return string.Format ("{0}%{1}", field, value);
		}

		public virtual string CreatePowerSql (string field, object value)
		{
			return string.Format ("{0}^{1}", field, value);
		}

		public virtual string CreateAbsSql (string field)
		{
			return string.Format ("abs({0})", field);
		}

		public virtual string CreateLogSql (string field)
		{
			return string.Format ("log({0})", field);
		}

		public virtual string CreateExpSql (string field)
		{
			return string.Format ("exp({0})", field);
		}

		public virtual string CreateSinSql (string field)
		{
			return string.Format ("sin({0})", field);
		}

		public virtual string CreateCosSql (string field)
		{
			return string.Format ("cos({0})", field);
		}

		public virtual string CreateTanSql (string field)
		{
			return string.Format ("tan({0})", field);
		}

		public virtual string CreateAtanSql (string field)
		{
			return string.Format ("atan({0})", field);
		}

		#endregion

		public virtual string CreateJoinOnMatchSql (string leftField, QueryPredicate predicate, string rightField)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryPredicate (predicate);
			sb.AppendFormat ("{0}{2}{1}", leftField, rightField, op);
			return sb.ToString ();
		}
	}
}