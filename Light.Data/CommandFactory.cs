using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

		protected bool _canInnerPage;

		public bool CanInnerPager {
			get {
				return _canInnerPage;
			}
		}

		protected CommandFactory ()
		{
			InitialPredicate ();
		}

		#region 增删改操作命令

		protected virtual List<DataParameter> CreateColumnParameter (IEnumerable<FieldMapping> mappings, object source)
		{
			List<DataParameter> paramList = new List<DataParameter> ();
			foreach (DataFieldMapping field in mappings) {
				object obj = field.Handler.Get (source);
				DataParameter dataParameter = new DataParameter (field.Name, field.ToColumn (obj), field.DBType, ParameterDirection.Input);
				paramList.Add (dataParameter);
			}
			return paramList;
		}

		protected virtual List<DataParameter> CreateExpressionParameter (IEnumerable<FieldMapping> mappings, object source)
		{
			List<DataParameter> paramList = new List<DataParameter> ();
			foreach (DataFieldMapping field in mappings) {
				object obj = field.Handler.Get (source);
				DataParameter dataParameter = new DataParameter (field.Name, field.ToParameter (obj), field.DBType, ParameterDirection.Input);
				paramList.Add (dataParameter);
			}
			return paramList;
		}

		public virtual CommandData CreateInsertCommand (DataTableEntityMapping mapping, object entity)
		{
			List<DataParameter> paramList = CreateColumnParameter (mapping.NoIdentityFields, entity);
			string [] insertList = new string [paramList.Count];
			string [] valuesList = new string [paramList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in paramList) {
				insertList [index] = CreateDataFieldSql (dataParameter.ParameterName);
				string paramName = CreateParamName ("P" + index);
				valuesList [index] = paramName;
				dataParameter.ParameterName = paramName;
				index++;
			}
			string insert = string.Join (",", insertList);
			string values = string.Join (",", valuesList);
			string sql = string.Format ("insert into {0}({1})values({2})", CreateDataTableSql (mapping.TableName), insert, values);
			CommandData command = new CommandData (sql, paramList);
			return command;
		}

		public virtual CommandData CreateUpdateCommand (DataTableEntityMapping mapping, object entity, string [] updatefieldNames)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}

			IEnumerable<FieldMapping> columnFields;

			if (updatefieldNames != null && updatefieldNames.Length > 0) {
				List<FieldMapping> updateFields = new List<FieldMapping> ();
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

			List<DataParameter> columnList = CreateColumnParameter (columnFields, entity);
			List<DataParameter> primaryList = CreateExpressionParameter (mapping.PrimaryKeyFields, entity);
			if (columnList.Count == 0) {
				throw new LightDataException (RE.UpdateFieldIsNotExists);
			}

			string [] updateList = new string [columnList.Count];
			string [] whereList = new string [primaryList.Count];
			int paramIndex = 0;
			int index = 0;
			foreach (DataParameter dataParameter in columnList) {
				string paramName = CreateParamName ("P" + paramIndex);
				updateList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), paramName);
				dataParameter.ParameterName = paramName;
				paramIndex++;
				index++;
			}
			index = 0;
			foreach (DataParameter dataParameter in primaryList) {
				string paramName = CreateParamName ("P" + paramIndex);
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), paramName);
				dataParameter.ParameterName = paramName;
				paramIndex++;
				index++;
			}
			string update = string.Join (",", updateList);
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("update {0} set {1} where {2}", CreateDataTableSql (mapping.TableName), update, where);

			DataParameter [] parameters = DataParameter.ConcatDataParameters (columnList, primaryList);
			CommandData command = new CommandData (sql, parameters);
			return command;
		}

		public virtual CommandData CreateDeleteCommand (DataTableEntityMapping mapping, object entity)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			List<DataParameter> primaryList = CreateExpressionParameter (mapping.PrimaryKeyFields, entity);
			string [] whereList = new string [primaryList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in primaryList) {
				string paramName = CreateParamName ("P" + index);
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), paramName);
				dataParameter.ParameterName = paramName;
				index++;
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("delete from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			CommandData command = new CommandData (sql, primaryList);
			return command;
		}

		public virtual CommandData CreateEntityExistsCommand (DataTableEntityMapping mapping, object entity)
		{
			if (!mapping.HasPrimaryKey) {
				throw new LightDataException (RE.PrimaryKeyIsNotExist);
			}
			List<DataParameter> primaryList = CreateColumnParameter (mapping.PrimaryKeyFields, entity);
			string [] whereList = new string [primaryList.Count];
			int index = 0;
			foreach (DataParameter dataParameter in primaryList) {
				string paramName = CreateParamName ("P" + index);
				whereList [index] = string.Format ("{0}={1}", CreateDataFieldSql (dataParameter.ParameterName), paramName);
				dataParameter.ParameterName = paramName;
				index++;
			}
			string where = string.Join (" and ", whereList);
			string sql = string.Format ("select 1 from {0} where {1}", CreateDataTableSql (mapping.TableName), where);
			CommandData command = new CommandData (sql, primaryList);
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

		public virtual string GetQueryString (QueryExpression query, out DataParameter [] parameters, bool fullFieldName = false)
		{
			string queryString = null;
			parameters = null;
			if (query != null) {
				queryString = string.Format ("where {0}", query.CreateSqlString (this, fullFieldName, out parameters));
			}
			return queryString;
		}

		public virtual string GetHavingString (AggregateHavingExpression query, out DataParameter [] parameters)
		{
			string queryString = null;
			parameters = null;
			if (query != null) {
				queryString = string.Format ("having {0}", query.CreateSqlString (this, false, out parameters));
			}
			return queryString;
		}

		//public virtual string GetHavingString (AggregateHavingExpression having, out DataParameter [] parameters, List<AggregateDataInfo> functions)
		//{
		//	string havingString = null;
		//	parameters = null;
		//	if (having != null) {
		//		havingString = string.Format ("having {0}", having.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate (object obj) {
		//			string alias = null;
		//			AggregateData aggregateFunction = obj as AggregateData;
		//			if (!Object.Equals (aggregateFunction, null)) {
		//				foreach (AggregateDataInfo info in functions) {
		//					if (Object.ReferenceEquals (aggregateFunction, info.Data)) {
		//						alias = info.Name;
		//						break;
		//					}
		//				}
		//			}
		//			else {
		//				throw new LightDataException (RE.UnknowHavingType);
		//			}
		//			return alias;
		//		})));
		//	}
		//	return havingString;
		//}

		public virtual string GetOrderString (AggregateOrderExpression order, out DataParameter [] parameters)
		{
			string orderString = null;
			parameters = null;
			if (order != null) {
				orderString = string.Format ("order by {0}", order.CreateSqlString (this, false, out parameters));
			}
			return orderString;
		}

		public virtual string GetOrderString (OrderExpression order, out DataParameter [] parameters, bool fullFieldName = false)
		{
			string orderString = null;
			parameters = null;
			if (order != null) {
				orderString = string.Format ("order by {0}", order.CreateSqlString (this, fullFieldName, out parameters));
			}
			return orderString;
		}

		//public virtual string GetOrderString (OrderExpression order, out DataParameter [] parameters, List<AggregateDataInfo> fields, List<AggregateDataInfo> functions)
		//{
		//	string orderString = null;
		//	parameters = null;
		//	if (order != null) {
		//		orderString = string.Format ("order by {0}", order.CreateSqlString (this, false, out parameters, new GetAliasHandler (delegate (object obj) {
		//			string alias = null;
		//			//if (obj is DataFieldInfo) {
		//			//	foreach (DataFieldInfo info in fields) {
		//			//		if (Object.ReferenceEquals (obj, info)) {
		//			//			AliasDataFieldInfo aliasInfo = info as AliasDataFieldInfo;
		//			//			if (!Object.Equals (aliasInfo, null)) {
		//			//				alias = aliasInfo.Alias;
		//			//			}
		//			//			break;
		//			//		}
		//			//	}
		//			//}
		//			//else
		//			if (obj is AggregateData) {
		//				foreach (AggregateDataInfo info in fields) {
		//					if (Object.ReferenceEquals (obj, info.Data)) {
		//						//alias = info.Name;
		//						//break;
		//						return info.Name;
		//					}
		//				}
		//				foreach (AggregateDataInfo info in functions) {
		//					if (Object.ReferenceEquals (obj, info.Data)) {
		//						//alias = info.Name;
		//						//break;
		//						return info.Name;
		//					}
		//				}
		//			}
		//			//else {
		//			//	throw new LightDataException (RE.UnknowOrderType);
		//			//}
		//			return alias;
		//		})));
		//	}
		//	return orderString;
		//}

		public virtual string GetOnString (DataFieldExpression on, out DataParameter [] parameters, bool fullFieldName = true)
		{
			string onString = null;
			parameters = null;
			if (on != null) {
				onString = string.Format ("on {0}", on.CreateSqlString (this, fullFieldName, out parameters));
			}
			return onString;
		}

		public virtual CommandData CreateSelectCommand (DataEntityMapping mapping, ISelector selector, QueryExpression query, OrderExpression order, Region region)
		{
			if (region != null && !_canInnerPage) {
				throw new LightDataException (RE.DataBaseNotSupportInnerPage);
			}
			CommandData data;
			string selectString;
			DataParameter [] dataParameters = null;
			if (selector != null) {
				string [] fieldNames = new string [mapping.FieldCount];
				int i = 0;
				foreach (DataFieldMapping field in mapping.DataEntityFields) {
					fieldNames [i] = CreateDataFieldSql (field.Name);
					i++;
				}
				selectString = string.Join (",", fieldNames);
			}
			else {
				selectString = selector.CreateSelectString (this, out dataParameters);
			}
			data = this.CreateSelectBaseCommand (mapping, selectString, dataParameters, query, order, region);
			return data;
		}

		public virtual CommandData CreateSelectSingleFieldCommand (DataFieldInfo fieldinfo, QueryExpression query, OrderExpression order, bool distinct, Region region)
		{
			if (region != null && !_canInnerPage) {
				throw new LightDataException (RE.DataBaseNotSupportInnerPage);
			}
			DataFieldMapping fieldMapping = fieldinfo.DataField;
			if (fieldMapping is PrimitiveFieldMapping || fieldMapping is EnumFieldMapping || fieldMapping is CustomFieldMapping) {
				DataEntityMapping mapping = fieldMapping.EntityMapping;
				DataParameter [] dataParameters;
				string select = fieldinfo.CreateDataFieldSql (this, false, out dataParameters);
				if (distinct) {
					select = "distinct " + select;
				}
				return CreateSelectBaseCommand (mapping, select, dataParameters, query, order, region);
			}
			else {
				throw new LightDataException (RE.OnlyPrimitiveFieldCanSelectSingle);
			}
		}

		public virtual CommandData CreateSelectBaseCommand (DataEntityMapping mapping, string customSelect, DataParameter [] dataParameters, QueryExpression query, OrderExpression order, Region region)
		{
			StringBuilder sql = new StringBuilder ();
			DataParameter [] queryparameters;
			DataParameter [] orderparameters;
			string queryString = GetQueryString (query, out queryparameters);
			string orderString = GetOrderString (order, out orderparameters);
			sql.AppendFormat ("select {0} from {1}", customSelect, CreateDataTableSql (mapping.TableName));
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (dataParameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		public virtual CommandData CreateSelectJoinTableCommand (ISelector selector, List<JoinModel> modelList, QueryExpression query, OrderExpression order, Region region)
		{
			DataParameter [] dataParameters;
			string selectString = selector.CreateSelectString (this, out dataParameters);
			return CreateSelectJoinTableCommand (selectString, dataParameters, modelList, query, order, region);
		}

		public virtual CommandData CreateSelectJoinTableCommand (string customSelect, DataParameter [] dataParameters, List<JoinModel> modelList, QueryExpression query, OrderExpression order, Region region)
		{
			StringBuilder tables = new StringBuilder ();
			OrderExpression totalOrder = null;
			QueryExpression totalQuery = null;
			List<DataParameter> innerParameters = new List<DataParameter> ();
			foreach (JoinModel model in modelList) {
				if (model.Connect != null) {
					tables.AppendFormat (" {0} ", _joinCollectionPredicateDict [model.Connect.Type]);
				}

				if (model.Query != null) {
					DataParameter [] queryparameters_sub;
					string mqueryString = GetQueryString (model.Query, out queryparameters_sub);
					tables.AppendFormat ("(select * from {0}", CreateDataTableSql (model.Mapping.TableName));
					if (!string.IsNullOrEmpty (mqueryString)) {
						tables.AppendFormat (" {0}", mqueryString);
						if (queryparameters_sub != null && queryparameters_sub.Length > 0) {
							innerParameters.AddRange (queryparameters_sub);
						}
					}
					string aliseName = model.AliasTableName;
					if (aliseName != null) {
						tables.AppendFormat (") as {0}", CreateDataTableSql (aliseName));
					}
					else {
						tables.AppendFormat (") as {0}", CreateDataTableSql (model.Mapping.TableName));
					}
				}
				else {
					string aliseName = model.AliasTableName;
					if (aliseName != null) {
						tables.AppendFormat ("{0} as {1}", CreateDataTableSql (model.Mapping.TableName), CreateDataTableSql (aliseName));
					}
					else {
						tables.Append (CreateDataTableSql (model.Mapping.TableName));
					}
				}
				if (model.Order != null) {
					totalOrder &= model.Order.CreateAliasTableNameOrder (model.AliasTableName);
				}
				if (model.Connect != null && model.Connect.On != null) {
					DataParameter [] onparameters;
					string onString = GetOnString (model.Connect.On, out onparameters);
					if (!string.IsNullOrEmpty (onString)) {
						tables.AppendFormat (" {0}", onString);
						if (onparameters != null && onparameters.Length > 0) {
							innerParameters.AddRange (onparameters);
						}
					}
				}
			}
			totalQuery &= query;
			totalOrder &= order;
			StringBuilder sql = new StringBuilder ();
			DataParameter [] queryparameters;
			DataParameter [] orderparameters;
			string queryString = GetQueryString (totalQuery, out queryparameters, true);
			string orderString = GetOrderString (totalOrder, out orderparameters, true);

			sql.AppendFormat ("select {0} from {1}", customSelect, tables);
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (dataParameters, innerParameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		public virtual CommandData CreateExistsCommand (DataEntityMapping mapping, QueryExpression query)
		{
			Region region = null;
			if (_canInnerPage) {
				region = new Region (0, 1);
			}
			return this.CreateSelectBaseCommand (mapping, "1", null, query, null, region);
		}

		public virtual CommandData CreateAggregateCommand (DataFieldMapping fieldMapping, AggregateType aggregateType, QueryExpression query, bool distinct)
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
			return CreateSelectBaseCommand (mapping, function, null, query, null, null);
		}

		public virtual CommandData CreateAggregateCountCommand (DataEntityMapping mapping, QueryExpression query)
		{
			string select = CreateCountAllSql ();
			return CreateSelectBaseCommand (mapping, select, null, query, null, null);//, false);
		}

		public virtual CommandData CreateAggregateJoinCountCommand (List<JoinModel> modelList, QueryExpression query)
		{
			string select = CreateCountAllSql ();
			return CreateSelectJoinTableCommand (select, null, modelList, query, null, null);
		}

		public virtual CommandData CreateDeleteMassCommand (DataTableEntityMapping mapping, QueryExpression query)
		{
			StringBuilder sql = new StringBuilder ();
			DataParameter [] parameters;
			string queryString = GetQueryString (query, out parameters);

			sql.AppendFormat ("delete from {0}", CreateDataTableSql (mapping.TableName));
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		public virtual CommandData CreateSelectInsertCommand (DataTableEntityMapping insertMapping, DataFieldInfo [] insertFields, DataTableEntityMapping selectMapping, SelectFieldInfo [] selectFields, QueryExpression query, OrderExpression order)
		{
			StringBuilder sql = new StringBuilder ();
			string insertString;
			string selectString;
			int insertCount;
			bool noidentity = false;
			DataFieldMapping [] insertFieldMappings;
			List<DataParameter> isnertparameters = new List<DataParameter> ();
			if (insertFields != null && insertFields.Length > 0) {
				insertFieldMappings = new DataFieldMapping [insertFields.Length];
				string [] fieldNames = new string [insertFields.Length];
				for (int i = 0; i < insertFields.Length; i++) {
					DataFieldInfo info = insertFields [i];
					DataParameter [] dataParameters = null;
					if (!insertMapping.Equals (info.TableMapping)) {
						throw new LightDataException (RE.FieldIsNotMatchDataMapping);
					}
					insertFieldMappings [i] = info.DataField;
					fieldNames [i] = info.CreateDataFieldSql (this, false, out dataParameters);
					if (dataParameters != null && dataParameters.Length > 0) {
						isnertparameters.AddRange (dataParameters);
					}
				}
				insertCount = fieldNames.Length;
				insertString = string.Join (",", fieldNames);
			}
			else if (insertMapping.HasIdentity && selectMapping.HasIdentity && insertMapping.IdentityField.PositionOrder == selectMapping.IdentityField.PositionOrder) {
				insertFieldMappings = new DataFieldMapping [insertMapping.FieldCount - 1];
				string [] fieldNames = new string [insertMapping.FieldCount - 1];
				int i = 0;
				foreach (DataFieldMapping field in insertMapping.NoIdentityFields) {
					insertFieldMappings [i] = field;
					fieldNames [i] = CreateDataFieldSql (field.Name);
					i++;
				}
				insertCount = fieldNames.Length;
				insertString = string.Join (",", fieldNames);
				noidentity = true;
			}
			else {
				insertFieldMappings = new DataFieldMapping [insertMapping.FieldCount];
				string [] fieldNames = new string [insertMapping.FieldCount];
				int i = 0;
				foreach (DataFieldMapping field in insertMapping.DataEntityFields) {
					insertFieldMappings [i] = field;
					fieldNames [i] = CreateDataFieldSql (field.Name);
					i++;
				}
				insertCount = fieldNames.Length;
				insertString = string.Join (",", fieldNames);
			}

			List<DataParameter> selectparameters = new List<DataParameter> ();
			if (selectFields != null && selectFields.Length > 0) {
				if (selectFields.Length != insertCount) {
					throw new LightDataException (RE.SelectFiledsCountNotEquidInsertFiledCount);
				}
				string [] selectFieldNames = new string [selectFields.Length];

				for (int i = 0; i < selectFields.Length; i++) {
					SelectFieldInfo info = selectFields [i];
					if (info.TableMapping != null && !selectMapping.Equals (info.TableMapping)) {
						throw new LightDataException (RE.FieldIsNotMatchDataMapping);
					}
					DataParameter [] dps;
					EnumSelectFieldInfo enuminfo = info as EnumSelectFieldInfo;
					if (enuminfo != null && enuminfo.EnumType == insertFieldMappings [i].ObjectType) {
						EnumFieldMapping enumMapping = insertFieldMappings [i] as EnumFieldMapping;
						selectFieldNames [i] = enuminfo.CreateDataFieldSql (this, enumMapping.EnumType, out dps);
					}
					else {
						selectFieldNames [i] = info.CreateDataFieldSql (this, out dps);
					}

					if (dps != null && dps.Length > 0) {
						selectparameters.AddRange (dps);
					}
				}
				selectString = string.Join (",", selectFieldNames);
			}
			else if (noidentity) {
				if (selectMapping.FieldCount - 1 != insertCount) {
					throw new LightDataException (RE.SelectFiledsCountNotEquidInsertFiledCount);
				}
				string [] fieldNames = new string [selectMapping.FieldCount - 1];
				int i = 0;
				foreach (DataFieldMapping field in selectMapping.NoIdentityFields) {
					fieldNames [i] = CreateDataFieldSql (field.Name);
					i++;
				}
				selectString = string.Join (",", fieldNames);
			}
			else {
				if (selectMapping.FieldCount != insertCount) {
					throw new LightDataException (RE.SelectFiledsCountNotEquidInsertFiledCount);
				}
				string [] fieldNames = new string [insertCount];
				int i = 0;
				foreach (DataFieldMapping field in selectMapping.DataEntityFields) {
					fieldNames [i] = CreateDataFieldSql (field.Name);
					i++;
				}
				selectString = string.Join (",", fieldNames);
			}
			DataParameter [] queryparameters;
			DataParameter [] orderparameters;
			string queryString = GetQueryString (query, out queryparameters);
			string orderString = GetOrderString (order, out orderparameters);
			sql.AppendFormat ("insert into {0}({1})", CreateDataTableSql (insertMapping.TableName), insertString);
			sql.AppendFormat ("select {0} from {1}", selectString, CreateDataTableSql (selectMapping.TableName));
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (isnertparameters, selectparameters, queryparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		public virtual CommandData CreateUpdateMassCommand (DataTableEntityMapping mapping, UpdateSetValue [] updateSetValues, QueryExpression query)
		{
			StringBuilder sql = new StringBuilder ();
			DataParameter [] queryparameters;
			string queryString = GetQueryString (query, out queryparameters);

			int length = updateSetValues.Length;
			DataParameter [] setparameters = new DataParameter [length];
			List<DataParameter> fieldParameters = new List<DataParameter> ();
			string [] setList = new string [length];

			for (int i = 0; i < length; i++) {
				if (!mapping.Equals (updateSetValues [i].DataField.DataField.EntityMapping)) {
					throw new LightDataException (RE.UpdateFieldTypeIsError);
				}
				string pn = CreateTempParamName ();
				UpdateSetValue ups = updateSetValues [i];
				DataFieldInfo fieldInfo = ups.DataField;
				DataParameter dataParameter = new DataParameter (pn, fieldInfo.DataField.ToColumn (ups.Value), fieldInfo.DBType);

				setparameters [i] = dataParameter;
				DataParameter [] dataParameters = null;
				setList [i] = string.Format ("{0}={1}", updateSetValues [i].DataField.CreateDataFieldSql (this, false, out dataParameters), setparameters [i].ParameterName);
				if (dataParameters != null && dataParameters.Length > 0) {
					fieldParameters.AddRange (dataParameters);
				}
			}
			string setString = string.Join (",", setList);
			sql.AppendFormat ("update {0} set {1}", CreateDataTableSql (mapping.TableName), setString);
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (fieldParameters, setparameters, queryparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		//public virtual CommandData CreateDynamicAggregateCommand (DataEntityMapping mapping, List<DataFieldInfo> fields, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		//{
		//	if (fields == null || fields.Count == 0) {
		//		throw new LightDataException (RE.DynamicAggregateFieldIsNotExists);
		//	}
		//	StringBuilder sql = new StringBuilder ();

		//	string [] selectList = new string [fields.Count + functions.Count];
		//	string [] groupbyList = new string [fields.Count];
		//	int index = 0;
		//	List<DataParameter> innerParameters = new List<DataParameter> ();
		//	foreach (DataFieldInfo fieldInfo in fields) {
		//		if (!mapping.Equals (fieldInfo.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] dataParameters = null;
		//		string groupbyField = fieldInfo.CreateDataFieldSql (this, false, out dataParameters);
		//		groupbyList [index] = groupbyField;
		//		AliasDataFieldInfo aliasInfo = fieldInfo as AliasDataFieldInfo;
		//		if (!Object.Equals (aliasInfo, null)) {
		//			selectList [index] = aliasInfo.CreateAliasDataFieldSql (this, false, out dataParameters);
		//		}
		//		else {
		//			selectList [index] = groupbyField;
		//		}
		//		index++;
		//	}
		//	List<DataParameter> functionParameters = new List<DataParameter> ();
		//	foreach (AggregateDataInfo functionInfo in functions) {
		//		AggregateData function = functionInfo.Data;
		//		if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
		//			throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
		//		}
		//		DataParameter [] aggparameters;
		//		string aggField = function.CreateSqlString (this, false, out aggparameters);
		//		string selectField = CreateAliasSql (aggField, functionInfo.Name);
		//		selectList [index] = selectField;
		//		if (aggparameters != null && aggparameters.Length > 0) {
		//			functionParameters.AddRange (aggparameters);
		//		}
		//		index++;
		//	}
		//	string select = string.Join (",", selectList);
		//	string groupby = string.Join (",", groupbyList);
		//	sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

		//	DataParameter [] queryparameters;
		//	string queryString = GetQueryString (query, out queryparameters);
		//	DataParameter [] havingparameters;
		//	string havingString = GetHavingString (having, out havingparameters);
		//	DataParameter [] orderparameters;
		//	string orderString = GetOrderString (order, out orderparameters, fields, functions);

		//	if (!string.IsNullOrEmpty (queryString)) {
		//		sql.AppendFormat (" {0}", queryString);
		//	}

		//	sql.AppendFormat (" group by {0}", groupby);

		//	if (!string.IsNullOrEmpty (havingString)) {
		//		sql.AppendFormat (" {0}", havingString);
		//	}

		//	if (!string.IsNullOrEmpty (orderString)) {
		//		sql.AppendFormat (" {0}", orderString);
		//	}
		//	DataParameter [] parameters = DataParameter.ConcatDataParameters (innerParameters, functionParameters, queryparameters, havingparameters, orderparameters);
		//	CommandData command = new CommandData (sql.ToString (), parameters);
		//	command.TransParamName = true;
		//	return command;
		//}

		public virtual CommandData CreateDynamicAggregateCommand (DataEntityMapping mapping, List<AggregateDataInfo> groupbys, List<AggregateDataInfo> functions, QueryExpression query, AggregateHavingExpression having, OrderExpression order)
		{
			StringBuilder sql = new StringBuilder ();

			string [] selectList = new string [groupbys.Count + functions.Count];
			string [] groupbyList = new string [groupbys.Count];
			int index = 0;
			List<DataParameter> innerParameters = new List<DataParameter> ();
			foreach (AggregateDataInfo groupbyInfo in groupbys) {
				AggregateData data = groupbyInfo.Data;
				if (!mapping.Equals (data.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				DataParameter [] dataParameters = null;
				string groupbyField = data.CreateSqlString (this, false, out dataParameters);
				groupbyList [index] = groupbyField;
				string selectField = CreateAliasSql (groupbyField, groupbyInfo.Name);
				selectList [index] = selectField;
				if (dataParameters != null && dataParameters.Length > 0) {
					innerParameters.AddRange (dataParameters);
				}
				index++;
			}
			List<DataParameter> functionParameters = new List<DataParameter> ();
			foreach (AggregateDataInfo functionInfo in functions) {
				AggregateData function = functionInfo.Data;
				if (function.TableMapping != null && !mapping.Equals (function.TableMapping)) {
					throw new LightDataException (RE.DataMappingIsNotMatchAggregateField);
				}
				DataParameter [] aggparameters;
				string aggField = function.CreateSqlString (this, false, out aggparameters);
				string selectField = CreateAliasSql (aggField, functionInfo.Name);
				selectList [index] = selectField;
				if (aggparameters != null && aggparameters.Length > 0) {
					functionParameters.AddRange (aggparameters);
				}
				index++;
			}
			string select = string.Join (",", selectList);
			string groupby = string.Join (",", groupbyList);
			sql.AppendFormat ("select {0} from {1}", select, CreateDataTableSql (mapping.TableName));

			DataParameter [] queryparameters;
			string queryString = GetQueryString (query, out queryparameters);
			DataParameter [] havingparameters;
			string havingString = GetHavingString (having, out havingparameters);
			DataParameter [] orderparameters;
			//string orderString = GetOrderString (order, out orderparameters, groupbys, functions);
			string orderString = GetOrderString (order, out orderparameters);
			if (!string.IsNullOrEmpty (queryString)) {
				sql.AppendFormat (" {0}", queryString);
			}

			sql.AppendFormat (" group by {0}", groupby);

			if (!string.IsNullOrEmpty (havingString)) {
				sql.AppendFormat (" {0}", havingString);
			}

			if (!string.IsNullOrEmpty (orderString)) {
				sql.AppendFormat (" {0}", orderString);
			}
			DataParameter [] parameters = DataParameter.ConcatDataParameters (innerParameters, functionParameters, queryparameters, havingparameters, orderparameters);
			CommandData command = new CommandData (sql.ToString (), parameters);
			command.TransParamName = true;
			return command;
		}

		public virtual CommandData [] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			int totalCount = entitys.Length;
			List<string> insertList = new List<string> ();
			foreach (DataFieldMapping field in mapping.NoIdentityFields) {
				insertList.Add (CreateDataFieldSql (field.Name));
			}

			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), string.Join (",", insertList));

			int createCount = 0;
			int totalCreateCount = 0;

			StringBuilder totalSql = new StringBuilder ();
			int paramIndex = 0;
			List<DataParameter> dataParams = new List<DataParameter> ();
			List<CommandData> commands = new List<CommandData> ();
			int i = 0;
			foreach (object entity in entitys) {
				List<DataParameter> entityParams = CreateColumnParameter (mapping.NoIdentityFields, entity);
				string [] valueList = new string [entityParams.Count];
				int index = 0;
				foreach (DataParameter dataParameter in entityParams) {
					string paramName = CreateParamName ("P" + paramIndex);
					valueList [index] = paramName;
					dataParameter.ParameterName = paramName;
					dataParams.Add (dataParameter);
					index++;
					paramIndex++;
				}
				string value = string.Join (",", valueList);
				totalSql.AppendFormat ("{0}values({1});", insertsql, value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					CommandData command = new CommandData (totalSql.ToString (), dataParams);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					dataParams = new List<DataParameter> ();
					createCount = 0;
					paramIndex = 0;
					totalSql = new StringBuilder ();
				}
				i++;
			}
			return commands.ToArray ();
		}

		public virtual CommandData CreateIdentityCommand (DataTableEntityMapping mapping)
		{
			string sql = CreateIdentitySql (mapping);
			if (!string.IsNullOrEmpty (sql)) {
				CommandData command = new CommandData (sql, null);
				command.TransParamName = true;
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

		public virtual string CreateSingleParamSql (object fieldName, QueryPredicate predicate, bool isReverse, DataParameter dataParameter)
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

		public virtual string CreateCollectionParamsQuerySql (object fieldName, QueryCollectionPredicate predicate, List<DataParameter> dataParameters)
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

		public virtual string CreateBetweenParamsQuerySql (object fieldName, bool isNot, DataParameter fromParam, DataParameter toParam)
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("{0} {3}between {1} and {2}", fieldName, fromParam.ParameterName, toParam.ParameterName, isNot ? string.Empty : "not ");
			return sb.ToString ();
		}

		//public virtual string CreateLambdaMatchQuerySql (string fieldName, bool isReverse, bool starts, bool ends, bool isNot, DataParameter dataParameter)
		//{
		//	StringBuilder sb = new StringBuilder ();
		//	sb.AppendFormat ("{0} {2}like {1}", fieldName, dataParameter.ParameterName, isNot ? "not " : string.Empty);
		//	string value = dataParameter.Value.ToString ();
		//	if (starts && !value.StartsWith (_wildcards, StringComparison.Ordinal)) {
		//		value = _wildcards + value;
		//	}
		//	if (ends && !value.EndsWith (_wildcards, StringComparison.Ordinal)) {
		//		value = value + _wildcards;
		//	}
		//	dataParameter.Value = value;
		//	return sb.ToString ();
		//}

		public virtual string CreateLambdaSingleParamSql (object left, QueryPredicate predicate, object right)
		{
			string op = GetQueryPredicate (predicate);
			string sql = string.Format ("{0}{2}{1}", left, right, op);
			return sql;
		}

		public virtual string CreateLambdaBooleanQuerySql (object field, bool isTrue, bool isEqual, bool isReverse)
		{
			if (!isReverse) {
				return string.Format ("{0}{2}{1}", field, isTrue ? "1" : "0", isEqual ? "=" : "!=");
			}
			else {
				return string.Format ("{1}{2}{0}", field, isTrue ? "1" : "0", isEqual ? "=" : "!=");
			}
		}

		public virtual string CreateLambdaNotSql (object value)
		{
			string sql = string.Format ("not({0})", value);
			return sql;
		}

		public virtual string CreateLambdaConcatSql (params object [] values)
		{
			string value1 = string.Join ("+", values);
			string sql = string.Format ("({0})", value1);
			return sql;
		}

		public virtual string CreateLambdaMatchQuerySql (object fieldName, object value, bool starts, bool ends, bool isNot)
		{
			string value1 = CreateMatchSql (value.ToString (), starts, ends);
			string sql = string.Format ("{0} {2}like {1}", fieldName, value1, isNot ? "not " : string.Empty);
			return sql;
		}

		public virtual string CreateCollectionMatchQuerySql (object fieldName, bool isReverse, bool starts, bool ends, bool isNot, List<DataParameter> dataParameters)
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
				if (i > 0) {
					if (isNot) {
						sb.Append (" and ");
					}
					else {
						sb.Append (" or ");
					}
				}
				if (!isReverse) {
					sb.AppendFormat ("{0} {2}like {1}", fieldName, item.ParameterName, isNot ? "not " : string.Empty);
					string value = item.Value.ToString ();
					if (starts && !value.StartsWith (_wildcards, StringComparison.Ordinal)) {
						value = _wildcards + value;
					}
					if (ends && !value.EndsWith (_wildcards, StringComparison.Ordinal)) {
						value = value + _wildcards;
					}
					item.Value = value;
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

		public virtual string CreateConditionCountSql (string expressionSql, object fieldName, bool isDistinct)
		{
			return string.Format ("count({2}case when {0} then {1} else null end)", expressionSql, !Object.Equals (fieldName, null) ? fieldName : "1", isDistinct ? "distinct " : "");
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

		public virtual string CreateAliasSql (string field, string alias)
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
			throw new NotImplementedException ();
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

		//public virtual string CreateStringSql (string value)
		//{
		//	if (value.IndexOf ('\\') >= 0) {
		//		value = value.Replace ("\\", "\\\\");
		//	}
		//	if (value.IndexOf ('\'') >= 0) {
		//		value = value.Replace ("\'", "\\'");
		//	}
		//	return string.Format ("'{0}'", value);
		//}

		public virtual string CreateNullSql ()
		{
			return "null";
		}

		public virtual string CreateNumberSql (object value)
		{
			return value.ToString ();
		}

		//public virtual string CreateBooleanSql (bool value)
		//{
		//	return value ? "true" : "false";
		//}

		public virtual string CreateConcatSql (object field, object value, bool forward)
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


		//public virtual string CreateConcatSql (object left, object right)
		//{
		//	return string.Format ("({0}+{1})", field, value);
		//}

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