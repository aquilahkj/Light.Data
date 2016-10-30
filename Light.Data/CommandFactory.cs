﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Light.Data
{
	internal delegate string GetAliasHandler (object obj);

	abstract class CommandFactory
	{
		protected virtual string CreateCustomFieldName ()
		{
			return string.Format ("field_{0}", Guid.NewGuid ().ToString ("N").Substring (0, 8));
		}

		public virtual string CreateTempParamName ()
		{
			return string.Format ("_param_{0}_", Guid.NewGuid ().ToString ("N"));
		}

		protected string _wildcards = "%";

		protected bool _havingAlias = false;

		protected bool _orderbyAlias = false;

		protected Dictionary<QueryPredicate, string> _queryPredicateDict = new Dictionary<QueryPredicate, string> ();

		protected Dictionary<QueryCollectionPredicate, string> _queryCollectionPredicateDict = new Dictionary<QueryCollectionPredicate, string> ();

		protected Dictionary<JoinType, string> _joinCollectionPredicateDict = new Dictionary<JoinType, string> ();

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

			_joinCollectionPredicateDict [JoinType.InnerJoin] = "inner join";
			_joinCollectionPredicateDict [JoinType.LeftJoin] = "left join";
			_joinCollectionPredicateDict [JoinType.RightJoin] = "right join";
		}

		public virtual string GetJoinPredicate (JoinType joinType)
		{
			return _joinCollectionPredicateDict [joinType];
		}

		protected string GetQueryPredicate (QueryPredicate predicate)
		{
			if (_queryPredicateDict.ContainsKey (predicate)) {
				return _queryPredicateDict [predicate];
			}
			else {
				throw new LightDataException (string.Format (RE.UnSupportPredicate, predicate));
			}
		}

		protected string GetQueryCollectionPredicate (QueryCollectionPredicate predicate)
		{
			if (_queryCollectionPredicateDict.ContainsKey (predicate)) {
				return _queryCollectionPredicateDict [predicate];
			}
			else {
				throw new LightDataException (string.Format (RE.UnSupportPredicate, predicate));
			}
		}

		protected CommandFactory ()
		{
			InitialPredicate ();
		}

		internal virtual string Null {
			get {
				return "null";
			}
		}

		#region 增删改操作命令

		public virtual CommandData CreateInsertCommand (DataTableEntityMapping mapping, object entity, CreateSqlState state)
		{
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException (RE.NoFieldInsert);
			}
			string [] insertList = new string [insertLen];
			string [] valuesList = new string [insertLen];
			for (int i = 0; i < insertLen; i++) {
				DataFieldMapping field = fields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				insertList [i] = CreateDataFieldSql (field.Name);
				valuesList [i] = state.AddDataParameter (value, field.DBType, ParameterDirection.Input);
			}
			string insert = string.Join (",", insertList);
			string values = string.Join (",", valuesList);
			string sql = string.Format ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			CommandData command = new CommandData (sql);
			return command;
		}

		public virtual CommandData CreateUpdateCommand (DataTableEntityMapping mapping, object entity, CreateSqlState state)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			DataTableEntity tableEntity = entity as DataTableEntity;
			string [] updatefieldNames = null;
			if (tableEntity != null) {
				updatefieldNames = tableEntity.GetUpdateFields ();
			}
			IList<DataFieldMapping> columnFields;
			if (updatefieldNames != null && updatefieldNames.Length > 0) {
				List<DataFieldMapping> updateFields = new List<DataFieldMapping> ();
				foreach (string name in updatefieldNames) {
					DataFieldMapping fm = mapping.FindDataEntityField (name);
					if (fm == null) {
						continue;
					}
					PrimitiveFieldMapping pfm = fm as PrimitiveFieldMapping;
					if (pfm != null && pfm.IsPrimaryKey) {
						continue;
					}
					if (!updateFields.Contains (fm)) {
						updateFields.Add (fm);
					}
				}
				columnFields = updateFields;
			}
			else {
				columnFields = mapping.NoPrimaryKeyFields;
			}
			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;
			int updateLen = columnFields.Count;
			if (updateLen == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			string [] updateList = new string [updateLen];
			string [] whereList = new string [keyLen];
			for (int i = 0; i < updateLen; i++) {
				DataFieldMapping field = columnFields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				updateList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
			}
			for (int i = 0; i < keyLen; i++) {
				DataFieldMapping field = keyFields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
			}
			string update = string.Join (",", updateList);
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("update {0} set {1} where {2}", CreateDataTableSql (mapping.TableName), update, where);
			CommandData command = new CommandData (sql);
			return command;
		}

		public virtual CommandData CreateDeleteCommand (DataTableEntityMapping mapping, object entity, CreateSqlState state)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;
			string [] whereList = new string [keyLen];
			for (int i = 0; i < keyLen; i++) {
				DataFieldMapping field = keyFields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("delete from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			CommandData command = new CommandData (sql);
			return command;
		}

		public virtual CommandData CreateEntityExistsCommand (DataTableEntityMapping mapping, object entity, CreateSqlState state)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;
			string [] whereList = new string [keyLen];
			for (int i = 0; i < keyLen; i++) {
				DataFieldMapping field = keyFields [i];
				object obj = field.Handler.Get (entity);
				object value = field.ToColumn (obj);
				whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("select 1 from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			CommandData command = new CommandData (sql);
			return command;
		}

		public virtual CommandData CreateTruncateTableCommand (DataTableEntityMapping mapping)
		{
			string sql = string.Format ("truncate table {0};", CreateDataTableSql (mapping));
			CommandData command = new CommandData (sql);
			return command;
		}

		#endregion

		#region 主命令语句块

		public virtual string GetHavingString (QueryExpression query, bool isFullField, CreateSqlState state)
		{
			string queryString = null;
			if (query != null) {
				if (_havingAlias) {
					bool alias = state.UseFieldAlias;
					state.UseFieldAlias = true;
					queryString = string.Format (" having {0}", query.CreateSqlString (this, isFullField, state));
					state.UseFieldAlias = alias;
				}
				else {
					queryString = string.Format (" having {0}", query.CreateSqlString (this, isFullField, state));
				}
			}
			return queryString;
		}

		public virtual string GetHavingString (AggregateHavingExpression query, bool isFullField, CreateSqlState state)
		{
			string queryString = null;
			if (query != null) {
				if (_havingAlias) {
					bool alias = state.UseFieldAlias;
					state.UseFieldAlias = true;
					queryString = string.Format (" having {0}", query.CreateSqlString (this, isFullField, state));
					state.UseFieldAlias = alias;
				}
				else {
					queryString = string.Format (" having {0}", query.CreateSqlString (this, isFullField, state));
				}
			}
			return queryString;
		}

		public virtual string GetQueryString (QueryExpression query, bool isFullField, CreateSqlState state)
		{
			string queryString = null;
			if (query != null) {
				queryString = string.Format (" where {0}", query.CreateSqlString (this, isFullField, state));
			}
			return queryString;
		}

		public virtual string GetOrderString (OrderExpression order, bool isFullField, CreateSqlState state)
		{
			string orderString = null;
			if (order != null) {
				orderString = string.Format (" order by {0}", order.CreateSqlString (this, isFullField, state));
			}
			return orderString;
		}

		public virtual string GetAggregateOrderString (OrderExpression order, bool isFullField, CreateSqlState state)
		{
			string orderString = null;
			if (order != null) {
				bool alias = state.UseFieldAlias;
				state.UseFieldAlias = true;
				orderString = string.Format (" order by {0}", order.CreateSqlString (this, isFullField, state));
				state.UseFieldAlias = alias;
			}
			return orderString;
		}

		public virtual string GetOnString (DataFieldExpression on, CreateSqlState state)
		{
			string onString = null;
			if (on != null) {
				onString = string.Format (" on {0}", on.CreateSqlString (this, true, state));
			}
			return onString;
		}

		public virtual CommandData CreateSelectCommand (DataEntityMapping mapping, ISelector selector, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			string selectString = selector.CreateSelectString (this, false, state);
			CommandData data = this.CreateSelectBaseCommand (mapping, selectString, query, order, region, state);
			return data;
		}

		public virtual CommandData CreateSelectSingleFieldCommand (DataFieldInfo fieldinfo, QueryExpression query, OrderExpression order, bool distinct, Region region, CreateSqlState state)
		{
			string select = fieldinfo.CreateSqlString (this, false, state);
			if (distinct) {
				select = "distinct " + select;
			}
			return CreateSelectBaseCommand (fieldinfo.TableMapping, select, query, order, region, state);
		}

		public virtual CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();
			sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));
			if (query != null) {
				sql.Append (GetQueryString (query, false, state));
			}
			if (order != null) {
				sql.Append (GetOrderString (order, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateSelectJoinTableCommand (ISelector selector, IJoinModel [] modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			string selectString = selector.CreateSelectString (this, true, state);
			return CreateSelectJoinTableBaseCommand (selectString, modelList, query, order, region, state);
		}

		public virtual CommandData CreateSelectJoinTableBaseCommand (string customSelect, IJoinModel [] modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
		{
			StringBuilder tables = new StringBuilder ();
			OrderExpression totalOrder = null;
			QueryExpression totalQuery = null;
			foreach (IJoinModel model in modelList) {
				if (model.Connect != null) {
					tables.AppendFormat (" {0} ", _joinCollectionPredicateDict [model.Connect.Type]);
				}

				string modelsql = model.CreateSqlString (this, state);
				tables.Append (modelsql);
				if (model.Connect != null && model.Connect.On != null) {
					tables.Append (GetOnString (model.Connect.On, state));
				}

				if (model.Order != null) {
					totalOrder &= model.Order.CreateAliasTableNameOrder (model.AliasTableName);
				}
			}
			totalQuery &= query;
			totalOrder &= order;
			StringBuilder sql = new StringBuilder ();

			sql.AppendFormat ("select {0} from {1}", customSelect, tables);
			if (totalQuery != null) {
				sql.AppendFormat (GetQueryString (totalQuery, true, state));
			}
			if (totalOrder != null) {
				sql.AppendFormat (GetOrderString (totalOrder, true, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateAggregateTableCommand (DataEntityMapping mapping, AggregateDataFieldInfo [] fieldInfos, QueryExpression query, QueryExpression having, OrderExpression order, Region region, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();
			List<string> selectList = new List<string> ();
			List<string> groupbyList = new List<string> ();
			foreach (AggregateDataFieldInfo info in fieldInfos) {
				state.SetAliasData (info.FieldInfo, info.AggregateName);
				selectList.Add (info.CreateAliasDataFieldSql (this, false, state));
				if (!info.Aggregate) {
					groupbyList.Add (info.CreateSqlString (this, false, state));
				}
			}
			string select = string.Join (",", selectList);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));
			if (query != null) {
				sql.AppendFormat (GetQueryString (query, false, state));
			}
			if (groupbyList.Count > 0) {
				string groupby = string.Join (",", groupbyList);
				sql.AppendFormat (" group by {0}", groupby);
			}
			if (having != null) {
				sql.AppendFormat (GetHavingString (having, false, state));
			}
			if (order != null) {
				sql.AppendFormat (GetAggregateOrderString (order, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateAggregateTableCommand (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();
			string [] selectList = new string [groupbys.Count + functions.Count];
			string [] groupbyList = new string [groupbys.Count];
			int index = 0;
			foreach (AggregateDataInfo groupbyInfo in groupbys) {
				AggregateData data = groupbyInfo.Data;
				if (!mapping.Equals (data.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				string groupbyField = data.CreateSqlString (this, false, state);
				groupbyList [index] = groupbyField;
				string selectField = CreateAliasFieldSql (groupbyField, groupbyInfo.Name);
				selectList [index] = selectField;
				index++;
			}
			foreach (AggregateDataInfo functionInfo in functions) {
				AggregateData function = functionInfo.Data;
				if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				string aggField = function.CreateSqlString (this, false, state);
				string selectField = CreateAliasFieldSql (aggField, functionInfo.Name);
				selectList [index] = selectField;
				index++;
			}
			string select = string.Join (",", selectList);
			string groupby = string.Join (",", groupbyList);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));
			if (query != null) {
				sql.AppendFormat (GetQueryString (query, false, state));
			}
			sql.AppendFormat (" group by {0}", groupby);
			if (having != null) {
				sql.AppendFormat (GetHavingString (having, false, state));
			}
			if (order != null) {
				sql.AppendFormat (GetAggregateOrderString (order, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateExistsCommand (DataEntityMapping mapping, QueryExpression query, CreateSqlState state)
		{
			Region region = new Region (0, 1);
			return this.CreateSelectBaseCommand (mapping, "1", query, null, region, state);
		}

		public virtual CommandData CreateAggregateFunctionCommand (DataFieldInfo field, AggregateType aggregateType, QueryExpression query, bool distinct, CreateSqlState state)
		{
			DataEntityMapping mapping = field.TableMapping;
			if (aggregateType != AggregateType.COUNT) {
				if (field.DataField != null) {
					TypeCode code = Type.GetTypeCode (field.DataField.ObjectType);
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
			}
			string fieldSql = field.CreateSqlString (this, false, state);
			string function = null;
			switch (aggregateType) {
			case AggregateType.COUNT:
				function = CreateCountSql (fieldSql, distinct);
				break;
			case AggregateType.SUM:
				function = CreateSumSql (fieldSql, distinct);
				break;
			case AggregateType.AVG:
				function = CreateAvgSql (fieldSql, distinct);
				break;
			case AggregateType.MAX:
				function = CreateMaxSql (fieldSql);
				break;
			case AggregateType.MIN:
				function = CreateMinSql (fieldSql);
				break;
			}
			return CreateSelectBaseCommand (mapping, function, query, null, null, state);
		}

		public virtual CommandData CreateAggregateCountCommand (DataEntityMapping mapping, QueryExpression query, CreateSqlState state)
		{
			string select = CreateCountAllSql ();
			return CreateSelectBaseCommand (mapping, select, query, null, null, state);
		}

		public virtual CommandData CreateAggregateJoinCountCommand (IJoinModel [] modelList, QueryExpression query, CreateSqlState state)
		{
			string select = CreateCountAllSql ();
			return CreateSelectJoinTableBaseCommand (select, modelList, query, null, null, state);
		}

		public virtual CommandData CreateDeleteMassCommand (DataTableEntityMapping mapping, QueryExpression query, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();

			sql.AppendFormat ("delete from {0}", CreateDataTableSql (mapping.TableName));
			if (query != null) {
				sql.Append (GetQueryString (query, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			//command.TransParamName = true;
			return command;
		}

		public virtual CommandData CreateUpdateMassCommand (DataTableEntityMapping mapping, MassUpdator updator, QueryExpression query, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();
			//DataTableEntityMapping mapping = updator.Mapping;
			string setString = updator.CreateSqlString (this, false, state);
			sql.AppendFormat ("update {0} set {1}", CreateDataTableSql (mapping.TableName), setString);
			if (query != null) {
				sql.Append (GetQueryString (query, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateSelectInsertCommand (DataTableEntityMapping insertTableMapping, DataEntityMapping selectMapping, QueryExpression query, OrderExpression order, CreateSqlState state)
		{
			StringBuilder sql = new StringBuilder ();
			string insertString;
			string selectString;
			ReadOnlyCollection<DataFieldMapping> insertFields;
			ReadOnlyCollection<DataFieldMapping> selectFields;
			if (insertTableMapping.HasIdentity) {
				DataTableEntityMapping selectTableEntityMapping = selectMapping as DataTableEntityMapping;
				if (selectTableEntityMapping != null && selectTableEntityMapping.HasIdentity) {
					if (insertTableMapping.FieldCount == selectTableEntityMapping.FieldCount && insertTableMapping.IdentityField.PositionOrder == selectTableEntityMapping.IdentityField.PositionOrder) {
						insertFields = insertTableMapping.NoIdentityFields;
						selectFields = selectTableEntityMapping.NoIdentityFields;
					}
					else {
						throw new LightDataException (RE.SelectFieldsCountNotEquidInsertFieldCount);
					}
				}
				else {
					if (insertTableMapping.FieldCount == selectMapping.FieldCount + 1) {
						insertFields = insertTableMapping.NoIdentityFields;
						selectFields = selectMapping.DataEntityFields;
					}
					else {
						throw new LightDataException (RE.SelectFieldsCountNotEquidInsertFieldCount);
					}
				}
			}
			else {
				if (insertTableMapping.FieldCount == selectMapping.FieldCount) {
					insertFields = insertTableMapping.DataEntityFields;
					selectFields = selectMapping.DataEntityFields;
				}
				else {
					throw new LightDataException (RE.SelectFieldsCountNotEquidInsertFieldCount);
				}
			}

			string [] insertFieldNames = new string [insertFields.Count];
			for (int i = 0; i < insertFields.Count; i++) {
				insertFieldNames [i] = CreateDataFieldSql (insertFields [i].Name);
			}
			insertString = string.Join (",", insertFieldNames);

			string [] selectFieldNames = new string [selectFields.Count];
			for (int i = 0; i < insertFields.Count; i++) {
				selectFieldNames [i] = CreateDataFieldSql (selectFields [i].Name);
			}
			selectString = string.Join (",", selectFieldNames);
			sql.AppendFormat ("insert into {0}({1})", CreateDataTableSql (insertTableMapping.TableName), insertString);
			sql.AppendFormat ("select {0} from {1}", selectString, CreateDataTableSql (selectMapping.TableName));
			if (query != null) {
				sql.Append (GetQueryString (query, false, state));
			}
			if (order != null) {
				sql.Append (GetOrderString (order, false, state));
			}
			CommandData command = new CommandData (sql.ToString ());
			return command;
		}

		public virtual CommandData CreateSelectInsertCommand (InsertSelector selector, DataEntityMapping mapping, QueryExpression query, OrderExpression order, CreateSqlState state)
		{
			CommandData selectCommandData = CreateSelectCommand (mapping, selector, query, order, null, state);
			DataFieldInfo [] insertFields = selector.GetInsertFields ();
			string [] insertFieldNames = new string [insertFields.Length];
			for (int i = 0; i < insertFields.Length; i++) {
				insertFieldNames [i] = CreateDataFieldSql (insertFields [i].FieldName);
			}
			string insertString = string.Join (",", insertFieldNames);
			string sql = string.Format ("insert into {0}({1})", CreateDataTableSql (selector.InsertMapping.TableName), insertString);
			selectCommandData.CommandText = sql + selectCommandData.CommandText;
			return selectCommandData;
		}

		public virtual CommandData CreateSelectInsertCommand (InsertSelector selector, IJoinModel [] modelList, QueryExpression query, OrderExpression order, CreateSqlState state)
		{
			CommandData selectCommandData = CreateSelectJoinTableCommand (selector, modelList, query, order, null, state);
			DataFieldInfo [] insertFields = selector.GetInsertFields ();
			string [] insertFieldNames = new string [insertFields.Length];
			for (int i = 0; i < insertFields.Length; i++) {
				insertFieldNames [i] = CreateDataFieldSql (insertFields [i].FieldName);
			}
			string insertString = string.Join (",", insertFieldNames);
			string sql = string.Format ("insert into {0}({1})", CreateDataTableSql (selector.InsertMapping.TableName), insertString);
			selectCommandData.CommandText = sql + selectCommandData.CommandText;
			return selectCommandData;
		}

		public virtual Tuple<CommandData, CreateSqlState> [] CreateBatchInsertCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			int totalCount = entitys.Count;
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException (RE.NoFieldInsert);
			}
			string [] insertList = new string [insertLen];
			for (int i = 0; i < insertLen; i++) {
				DataFieldMapping field = fields [i];
				insertList [i] = CreateDataFieldSql (field.Name);
			}
			string insert = string.Join (",", insertList);
			string insertSql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				string [] valuesList = new string [insertLen];
				for (int i = 0; i < insertLen; i++) {
					DataFieldMapping field = fields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					valuesList [i] = state.AddDataParameter (value, field.DBType, ParameterDirection.Input);
				}
				string values = string.Join (",", valuesList);
				totalSql.AppendFormat ("{0}values({1});", insertSql, values);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					CommandData command = new CommandData (totalSql.ToString ());
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
				}
			}
			return list.ToArray ();
		}

		public virtual Tuple<CommandData, CreateSqlState> [] CreateBatchUpdateCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			if (mapping.NoPrimaryKeyFields.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;

			int totalCount = entitys.Count;
			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				IList<DataFieldMapping> columnFields;
				DataTableEntity tableEntity = entity as DataTableEntity;
				if (tableEntity != null) {
					string [] updatefieldNames = tableEntity.GetUpdateFields ();
					if (updatefieldNames != null && updatefieldNames.Length > 0) {
						List<DataFieldMapping> updateFields = new List<DataFieldMapping> ();
						foreach (string name in updatefieldNames) {
							DataFieldMapping fm = mapping.FindDataEntityField (name);
							if (fm == null) {
								continue;
							}
							PrimitiveFieldMapping pfm = fm as PrimitiveFieldMapping;
							if (pfm != null && pfm.IsPrimaryKey) {
								continue;
							}
							if (!updateFields.Contains (fm)) {
								updateFields.Add (fm);
							}
						}
						columnFields = updateFields;
					}
					else {
						columnFields = mapping.NoPrimaryKeyFields;
					}
				}
				else {
					columnFields = mapping.NoPrimaryKeyFields;
				}

				int updateLen = columnFields.Count;
				string [] updateList = new string [updateLen];
				string [] whereList = new string [keyLen];
				for (int i = 0; i < updateLen; i++) {
					DataFieldMapping field = columnFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					updateList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				for (int i = 0; i < keyLen; i++) {
					DataFieldMapping field = keyFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				string update = string.Join (",", updateList);
				string where = string.Join (" and ", whereList);
				totalSql.AppendFormat ("update {0} set {1} where {2};", CreateDataTableSql (mapping.TableName), update, where);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					CommandData command = new CommandData (totalSql.ToString ());
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
				}
			}
			return list.ToArray ();
		}

		public virtual Tuple<CommandData, CreateSqlState> [] CreateBatchDeleteCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
		{
			if (entitys == null || entitys.Count == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			if (mapping.NoPrimaryKeyFields.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			IList<DataFieldMapping> keyFields = mapping.PrimaryKeyFields;
			int keyLen = keyFields.Count;

			int totalCount = entitys.Count;
			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			CreateSqlState state = new CreateSqlState (this);
			List<Tuple<CommandData, CreateSqlState>> list = new List<Tuple<CommandData, CreateSqlState>> ();

			foreach (object entity in entitys) {
				string [] whereList = new string [keyLen];
				for (int i = 0; i < keyLen; i++) {
					DataFieldMapping field = keyFields [i];
					object obj = field.Handler.Get (entity);
					object value = field.ToColumn (obj);
					whereList [i] = string.Format ("{0}={1}", CreateDataFieldSql (field.Name), state.AddDataParameter (value, field.DBType, ParameterDirection.Input));
				}
				string where = string.Join (" and ", whereList);
				totalSql.AppendFormat ("delete from {0} where {1};", CreateDataTableSql (mapping.TableName), where);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					CommandData command = new CommandData (totalSql.ToString ());
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
				}
			}
			return list.ToArray ();
		}


		public virtual CommandData CreateIdentityCommand (DataTableEntityMapping mapping)
		{
			string sql = CreateIdentitySql (mapping);
			if (!string.IsNullOrEmpty (sql)) {
				CommandData command = new CommandData (sql);
				//command.TransParamName = true;
				return command;
			}
			else {
				return null;
			}
		}


		#endregion

		#region 基本语句块

		public virtual string CreateCatchExpressionSql (string expressionString1, string expressionString2, CatchOperatorsType operatorType)
		{
			return string.Format ("({0} {2} {1})", expressionString1, expressionString2, operatorType.ToString ().ToLower ());
		}

		public virtual string CreateCatchExpressionSql (string [] expressionStrings)
		{
			return string.Join (",", expressionStrings);
		}

		public virtual string CreateSingleParamSql (object fieldName, QueryPredicate predicate, bool isReverse, string name)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryPredicate (predicate);
			if (!isReverse) {
				sb.AppendFormat ("{0}{2}{1}", fieldName, name, op);
			}
			else {
				sb.AppendFormat ("{1}{2}{0}", fieldName, name, op);
			}
			return sb.ToString ();
		}

		public virtual string CreateRelationTableSql (object fieldName, QueryPredicate predicate, bool isReverse, string relationFieldName)
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

		public virtual string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, IEnumerable<object> list)
		{
			string op = GetQueryCollectionPredicate (predicate);

			int i = 0;
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {1} (", fieldName, op);
			foreach (object item in list) {
				if (i > 0)
					sb.Append (",");
				sb.Append (item);
				i++;
			}
			sb.Append (")");
			return sb.ToString ();
		}

		public virtual string CreateExistsQuerySql (string queryTableName, string whereString, bool isNot)
		{
			return string.Format ("{2}exists (select 1 from {0} where {1})", queryTableName, whereString, isNot ? "not " : string.Empty);
		}

		public virtual string CreateNotQuerySql (string whereString)
		{
			return string.Format ("not({0})", whereString);
		}

		public virtual string CreateSubQuerySql (object fieldName, QueryCollectionPredicate predicate, string queryfieldName, string queryTableName, string whereString)
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

		public virtual string CreateBetweenParamsQuerySql (object fieldName, bool isNot, string fromParam, string toParam)
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {3}between {1} and {2}", fieldName, fromParam, toParam, isNot ? string.Empty : "not ");
			return sb.ToString ();
		}

		public virtual string CreateSingleParamSql (object left, QueryPredicate predicate, object right)
		{
			string op = GetQueryPredicate (predicate);
			string sql = string.Format ("{0}{2}{1}", left, right, op);
			return sql;
		}

		public virtual string CreateBooleanQuerySql (object field, bool isTrue, bool isEqual, bool isReverse)
		{
			if (!isReverse) {
				return string.Format ("{0}{2}{1}", field, isTrue ? "1" : "0", isEqual ? "=" : "!=");
			}
			else {
				return string.Format ("{1}{2}{0}", field, isTrue ? "1" : "0", isEqual ? "=" : "!=");
			}
		}

		public virtual string CreateNotSql (object value)
		{
			string sql = string.Format ("not({0})", value);
			return sql;
		}

		public virtual string CreateConcatSql (params object [] values)
		{
			string value1 = string.Join ("+", values);
			string sql = string.Format ("({0})", value1);
			return sql;
		}

		public virtual string CreateLikeMatchQuerySql (object left, object right, bool starts, bool ends, bool isNot)
		{
			string value1 = CreateMatchSql (right.ToString (), starts, ends);
			string sql = string.Format ("{0} {2}like {1}", left, value1, isNot ? "not " : string.Empty);
			return sql;
		}

		public virtual string CreateCollectionMatchQuerySql (object fieldName, bool isReverse, bool starts, bool ends, bool isNot, IEnumerable<object> list)
		{
			int i = 0;
			StringBuilder sb = new StringBuilder ();

			foreach (string item in list) {
				if (i > 0) {
					if (isNot) {
						sb.Append (" and ");
					}
					else {
						sb.Append (" or ");
					}
				}
				if (!isReverse) {
					string value1 = CreateMatchSql (item, starts, ends);
					sb.AppendFormat ("{0} {2}like {1}", fieldName, value1, isNot ? "not " : string.Empty);
				}
				else {
					sb.AppendFormat ("{1} {2}like {0}", fieldName, item, isNot ? "not " : string.Empty);
				}
				i++;
			}
			if (i > 1) {
				sb.Insert (0, "(");
				sb.Append (")");
			}
			return sb.ToString ();
		}

		public virtual string CreateNullQuerySql (object fieldName, bool isNull)
		{
			return string.Format ("{0} is{1} null", fieldName, isNull ? string.Empty : " not");
		}

		public virtual string CreateBooleanQuerySql (object fieldName, bool isTrue)
		{
			return string.Format ("{0}={1}", fieldName, isTrue ? "1" : "0");
		}

		public virtual string CreateOrderBySql (object fieldName, OrderType orderType)
		{
			return string.Format ("{0} {1}", fieldName, orderType.ToString ().ToLower ());
		}

		public virtual string CreateRandomOrderBySql (DataEntityMapping mapping, string aliasName, bool fullFieldName)
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

		public virtual string CreateCountAllSql (string expressionSql)
		{
			return string.Format ("count(case when {0} then 1 else null end)", expressionSql);
		}

		public virtual string CreateConditionCountSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("count({2}case when {0} then {1} else null end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateCountSql (object fieldName, bool isDistinct)
		{
			return string.Format ("count({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateSumSql (object fieldName, bool isDistinct)
		{
			return string.Format ("sum({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateConditionSumSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("sum({2}case when {0} then {1} else null end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateAvgSql (object fieldName, bool isDistinct)
		{
			return string.Format ("avg({1}{0})", fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateConditionAvgSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("avg({2}case when {0} then {1} else null end)", expressionSql, fieldName, isDistinct ? "distinct " : "");
		}

		public virtual string CreateMaxSql (object fieldName)
		{
			return string.Format ("max({0})", fieldName);
		}

		public virtual string CreateConditionMaxSql (string expressionSql, object fieldName)
		{
			return string.Format ("max(case when {0} then {1} else null end)", expressionSql, fieldName);
		}

		public virtual string CreateMinSql (object fieldName)
		{
			return string.Format ("min({0})", fieldName);
		}

		public virtual string CreateConditionMinSql (string expressionSql, object fieldName)
		{
			return string.Format ("min(case when {0} then {1} else null end)", expressionSql, fieldName);
		}

		public virtual string CreateAliasFieldSql (string field, string alias)
		{
			return string.Format ("{0} as {1}", field, CreateDataFieldSql (alias));
		}

		public virtual string CreateAliasTableSql (string field, string alias)
		{
			return string.Format ("{0} as {1}", field, CreateDataFieldSql (alias));
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

		public virtual string CreateMatchSql (object field, bool starts, bool ends)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateDateSql (object field, string format)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateDateSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateDateTimeFormatSql (string field, string format)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateYearSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateMonthSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateDaySql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateHourSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateMinuteSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateSecondSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateWeekSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateWeekDaySql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateYearDaySql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateLengthSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateSubStringSql (object field, object start, object size)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateIndexOfSql (object field, object value, object startIndex)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateReplaceSql (object field, object oldValue, object newValue)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateToLowerSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateToUpperSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateTrimSql (object field)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateDataBaseTimeSql ()
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateNullSql ()
		{
			return "null";
		}

		public virtual string CreateNumberSql (object value)
		{
			return value.ToString ();
		}

		public virtual string CreateDualConcatSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}+{1})", field, value);
			}
			else {
				return string.Format ("({0}+{1})", value, field);
			}
		}

		public virtual string CreatePlusSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}+{1})", field, value);
			}
			else {
				return string.Format ("({0}+{1})", value, field);
			}
		}

		public virtual string CreateMinusSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}-{1})", field, value);
			}
			else {
				return string.Format ("({0}-{1})", value, field);
			}
		}

		public virtual string CreateMultiplySql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}*{1})", field, value);
			}
			else {
				return string.Format ("({0}*{1})", value, field);
			}
		}

		public virtual string CreateDividedSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}/{1})", field, value);
			}
			else {
				return string.Format ("({0}/{1})", value, field);
			}
		}

		public virtual string CreateModSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}%{1})", field, value);
			}
			else {
				return string.Format ("({0}%{1})", value, field);
			}
		}

		public virtual string CreatePowerSql (object field, object value, bool forward)
		{
			if (forward) {
				return string.Format ("({0}^{1})", field, value);
			}
			else {
				return string.Format ("({0}^{1})", value, field);
			}
		}

		public virtual string CreatePlusSql (object left, object right)
		{
			return string.Format ("({0}+{1})", left, right);
		}

		public virtual string CreateMinusSql (object left, object right)
		{
			return string.Format ("({0}-{1})", left, right);
		}

		public virtual string CreateMultiplySql (object left, object right)
		{
			return string.Format ("({0}*{1})", left, right);
		}

		public virtual string CreateDividedSql (object left, object right)
		{
			return string.Format ("({0}/{1})", left, right);
		}

		public virtual string CreateModSql (object left, object right)
		{
			return string.Format ("({0}%{1})", left, right);
		}

		public virtual string CreatePowerSql (object left, object right)
		{
			return string.Format ("({0}^{1})", left, right);
		}

		public virtual string CreateCastStringSql (object field, string format)
		{
			throw new NotSupportedException ();
		}

		public virtual string CreateAbsSql (object field)
		{
			return string.Format ("abs({0})", field);
		}

		public virtual string CreateSignSql (object field)
		{
			return string.Format ("sign({0})", field);
		}

		public virtual string CreateLogSql (object field)
		{
			return string.Format ("log({0})", field);
		}

		public virtual string CreateLogSql (object field, object value)
		{
			return string.Format ("log({0},{1})", field, value);
		}

		public virtual string CreateLog10Sql (object field)
		{
			return string.Format ("log10({0})", field);
		}

		public virtual string CreateExpSql (object field)
		{
			return string.Format ("exp({0})", field);
		}

		public virtual string CreatePowSql (object field, object value)
		{
			return string.Format ("power({0},{1})", field, value);
		}

		public virtual string CreateSinSql (object field)
		{
			return string.Format ("sin({0})", field);
		}

		public virtual string CreateCosSql (object field)
		{
			return string.Format ("cos({0})", field);
		}

		public virtual string CreateASinSql (object field)
		{
			return string.Format ("asin({0})", field);
		}

		public virtual string CreateACosSql (object field)
		{
			return string.Format ("acos({0})", field);
		}

		public virtual string CreateTanSql (object field)
		{
			return string.Format ("tan({0})", field);
		}

		public virtual string CreateAtanSql (object field)
		{
			return string.Format ("atan({0})", field);
		}

		public virtual string CreateAtan2Sql (object field, object value)
		{
			return string.Format ("atan2({0},{1})", field, value);
		}

		public virtual string CreateCeilingSql (object field)
		{
			return string.Format ("ceiling({0})", field);
		}

		public virtual string CreateFloorSql (object field)
		{
			return string.Format ("floor({0})", field);
		}

		public virtual string CreateRoundSql (object field, object value)
		{
			return string.Format ("round({0},{1})", field, value);
		}

		public virtual string CreateTruncateSql (object field)
		{
			return string.Format ("truncate({0})", field);
		}

		public virtual string CreateSqrtSql (object field)
		{
			return string.Format ("Sqrt({0})", field);
		}

		public virtual string CreateMaxSql (object left, object right)
		{
			return string.Format ("(case when {0}>{1} then {0} else {1} end)", left, right);
		}

		public virtual string CreateMinSql (object left, object right)
		{
			return string.Format ("(case when {0}<{1} then {0} else {1} end)", left, right);
		}

		public virtual string CreateConditionSql (string querySql, object ifTrue, object IfFalse)
		{
			return string.Format ("case when {0} then {1} else {2} end", querySql, ifTrue, IfFalse);
		}

		#endregion


		public virtual string CreateJoinOnMatchSql (string leftField, QueryPredicate predicate, string rightField)
		{
			StringBuilder sb = new StringBuilder ();
			string op = GetQueryPredicate (predicate);
			sb.AppendFormat ("{0}{2}{1}", leftField, rightField, op);
			return sb.ToString ();
		}

		public virtual string CreateParamName (string name)
		{
			if (!name.StartsWith ("@", StringComparison.Ordinal)) {
				return "@" + name;
			}
			else {
				return name;
			}
		}

		public virtual string CreateStringWrap (object value)
		{
			return string.Format ("'{0}'", value);
		}
	}
}