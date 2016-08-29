using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
	class MssqlCommandFactory_2008 : MssqlCommandFactory
	{
		public MssqlCommandFactory_2008 ()
		{

		}

		public override Tuple<CommandData, CreateSqlState> [] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		{
			if (entitys == null || entitys.Length == 0) {
				throw new ArgumentNullException (nameof (entitys));
			}
			if (batchCount <= 0) {
				batchCount = 10;
			}
			int totalCount = entitys.Length;
			IList<DataFieldMapping> fields = mapping.NoIdentityFields;
			int insertLen = fields.Count;
			if (insertLen == 0) {
				throw new LightDataException ("");
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

			totalSql.AppendFormat ("{0}values", insertSql);

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

				totalSql.AppendFormat ("({0})", values);

				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					totalSql.Append (";");
					CommandData command = new CommandData (totalSql.ToString ());
					list.Add (new Tuple<CommandData, CreateSqlState> (command, state));
					if (totalCreateCount == totalCount) {
						break;
					}
					state = new CreateSqlState (this);
					createCount = 0;
					totalSql = new StringBuilder ();
					totalSql.AppendFormat ("{0}values", insertSql);
				}
				else {
					totalSql.Append (",");
				}
			}
			return list.ToArray ();
		}

		//		public override CommandData[] CreateBulkInsertCommand (DataTableEntityMapping mapping, Array entitys, int batchCount)
		//		{
		//			if (entitys == null || entitys.Length == 0) {
		//				throw new ArgumentNullException ("entitys");
		//			}
		//			if (batchCount <= 0) {
		//				batchCount = 10;
		//			}
		////			object tmpEntity = entitys.GetValue (0);
		////			DataTableEntityMapping mapping = DataEntityMapping.GetTableMapping (tmpEntity.GetType ());
		////			List<DataParameter> paramList = CreateColumnParameter (mapping.NoIdentityFields, tmpEntity);
		////			List<string> insertList = new List<string> ();
		////			foreach (DataParameter dataParameter in paramList) {
		////				insertList.Add (CreateDataFieldSql (dataParameter.ParameterName));
		////			}
		//			int totalCount = entitys.Length;
		//			List<string> insertList = new List<string> ();
		//			foreach (DataFieldMapping field in mapping.NoIdentityFields) {
		//				insertList.Add (CreateDataFieldSql (field.Name));
		//			}

		//			string insert = string.Join (",", insertList);
		//			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

		//			int createCount = 0;
		//			int totalCreateCount = 0;
		//			StringBuilder values = new StringBuilder ();
		//			int paramIndex = 0;
		//			List<DataParameter> dataParams = new List<DataParameter> ();
		//			List<CommandData> commands = new List<CommandData> ();
		//			foreach (object entity in entitys) {
		//				List<DataParameter> entityParams = CreateColumnParameter (mapping.NoIdentityFields, entity);
		//				string[] valueList = new string[entityParams.Count];
		//				int index = 0;
		//				foreach (DataParameter dataParameter in entityParams) {
		//					string paramName = CreateParamName ("P" + paramIndex);
		//					valueList [index] = paramName;
		//					dataParameter.ParameterName = paramName;
		//					dataParams.Add (dataParameter);
		//					index++;
		//					paramIndex++;
		//				}
		//				string value = string.Join (",", valueList);
		//				values.AppendFormat ("({0})", value);
		//				createCount++;
		//				totalCreateCount++;
		//				if (createCount == batchCount || totalCreateCount == totalCount) {
		//					CommandData command = new CommandData (string.Format ("{0}values{1};", insertsql, values), dataParams);
		//					commands.Add (command);
		//					if (totalCreateCount == totalCount) {
		//						break;
		//					}
		//					dataParams = new List<DataParameter> ();
		//					createCount = 0;
		//					paramIndex = 0;
		//					values = new StringBuilder ();
		//				}
		//				else {
		//					values.Append (",");
		//				}
		//			}
		//			return commands.ToArray ();
		//		}
	}
}
