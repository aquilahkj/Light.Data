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

		public override CommandData[] CreateBulkInsertCommand (Array entitys, int batchCount)
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

			List<DataParameter> paramList = GetDataParameters (fields, tmpEntity);
//			string[] insertList = new string[paramList.Count];
//			int index = 0;
//			foreach (DataParameter dataParameter in paramList) {
//				insertList [index] = CreateDataFieldSql (dataParameter.ParameterName);
//				index++;
//			}
			List<string> insertList = new List<string> ();
			foreach (DataParameter dataParameter in paramList) {
				insertList.Add (CreateDataFieldSql (dataParameter.ParameterName));
			}

			string insert = string.Join (",", insertList);
			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;
			StringBuilder values = new StringBuilder ();
//			IDbCommand command = _database.CreateCommand ();
			int paramIndex = 0;
			List<DataParameter> dataParams = new List<DataParameter> ();
			List<CommandData> commands = new List<CommandData> ();
//			List<IDbCommand> commands = new List<IDbCommand> ();
			foreach (object entity in entitys) {
				List<DataParameter> entityParams = GetDataParameters (fields, entity);
				string[] valueList = new string[paramList.Count];
				int index = 0;
				foreach (DataParameter dataParameter in entityParams) {
//					IDataParameter param = _database.CreateParameter ("P" + paramIndex, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
//					command.Parameters.Add (param);
//					valueList [vindex] = param.ParameterName;
//					paramIndex++;
//					vindex++;
					string paramName = "P" + index;
					valueList [index] = paramName;
					dataParameter.ParameterName = paramName;
					dataParams.Add (dataParameter);
					index++;
					paramIndex++;
				}
				string value = string.Join (",", valueList);
				values.AppendFormat ("({0})", value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
//					values.Append (";");
//					command.CommandText = string.Format ("{0}values{1}", insertsql, values);
					CommandData command = new CommandData (string.Format ("{0}values{1};", insertsql, values), dataParams);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
//					command = _database.CreateCommand ();
//					createCount = 0;
//					paramIndex = 0;
//					values = new StringBuilder ();
					dataParams = new List<DataParameter> ();
					createCount = 0;
					paramIndex = 0;
					values = new StringBuilder ();
				}
				else {
					values.Append (",");
				}
			}
			return commands.ToArray ();
		}
	}
}
