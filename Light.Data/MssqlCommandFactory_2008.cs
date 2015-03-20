using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
	class MssqlCommandFactory_2008 : MssqlCommandFactory
	{
		public MssqlCommandFactory_2008 (Database database)
			: base (database)
		{
            
		}

		public override IDbCommand[] CreateBulkInsertCommand (Array entitys, int batchCount)
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
			StringBuilder values = new StringBuilder ();
			StringBuilder insert = new StringBuilder ();


			List<DataParameter> paramList = GetDataParameters (fields, tmpEntity);
			foreach (DataParameter dataParameter in paramList) {
				insert.AppendFormat ("{0},", CreateDataFieldSql (dataParameter.ParameterName));
			}
			insert.Remove (insert.Length - 1, 1);
			string insertsql = string.Format ("insert into {0}({1})", CreateDataTableSql (mapping.TableName), insert);

			int createCount = 0;
			int totalCreateCount = 0;

			IDbCommand command = _database.CreateCommand ();
			int paramCount = 0;
			List<IDbCommand> commands = new List<IDbCommand> ();
			foreach (object entity in entitys) {
				paramList = GetDataParameters (fields, entity);
				StringBuilder value = new StringBuilder ();
				foreach (DataParameter dataParameter in paramList) {
					IDataParameter param = _database.CreateParameter ("P" + paramCount, dataParameter.Value, dataParameter.DbType, dataParameter.Direction);
					value.AppendFormat ("{0},", param.ParameterName);
					command.Parameters.Add (param);
					paramCount++;
				}
				value.Remove (value.Length - 1, 1);
				values.AppendFormat ("({0}),", value);
				createCount++;
				totalCreateCount++;
				if (createCount == batchCount || totalCreateCount == totalCount) {
					values.Remove (values.Length - 1, 1);
					command.CommandText = string.Format ("{0}values{1}", insertsql, values);
					commands.Add (command);
					if (totalCreateCount == totalCount) {
						break;
					}
					command = _database.CreateCommand ();
					createCount = 0;
					paramCount = 0;
					values = new StringBuilder ();
				}
			}
			return commands.ToArray ();
		}
	}
}
