using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
	class MssqlCommandFactory_2008 : MssqlCommandFactory
	{
		public override Tuple<CommandData, CreateSqlState> [] CreateBatchInsertCommand (DataTableEntityMapping mapping, IList entitys, int batchCount)
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
	}
}
