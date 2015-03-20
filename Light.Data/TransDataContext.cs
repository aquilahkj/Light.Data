using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 进行事务的数据上下文
	/// </summary>
	public class TransDataContext : DataContext, IDisposable
	{
		TransactionConnection _trconnection = null;

		bool _isTransaction = false;

		bool _isDisposed = false;

		internal TransDataContext (string connectionString, string configName, Database dataBase)
			: base (connectionString, configName, dataBase)
		{

		}


		/// <summary>
		/// 开始事务
		/// </summary>
		public void BeginTrans (SafeLevel level)
		{
			if (_trconnection != null) {
				if (_isTransaction) {
					_trconnection.Rollback ();
				}
				_trconnection.Dispose ();
			}
			_isTransaction = true;
			if (level == SafeLevel.None) {
				_trconnection = CreateTransactionConnection (SafeLevel.Default);
			}
			else {
				_trconnection = CreateTransactionConnection (level);
			}
			_trconnection.Open ();
		}

		/// <summary>
		/// 提交事务
		/// </summary>
		public void CommitTrans ()
		{
			if (_trconnection != null) {
				if (_isTransaction) {
					_trconnection.Commit ();
				}
				_isTransaction = false;
			}
		}

		/// <summary>
		/// 回滚事务
		/// </summary>
		public void RollbackTrans ()
		{
			if (_trconnection != null) {
				if (_isTransaction) {
					_trconnection.Rollback ();
				}
				_isTransaction = false;
			}
		}

		TransactionConnection GetTransactionConnection ()
		{
			if (_trconnection == null) {
				_trconnection = CreateTransactionConnection (SafeLevel.None);
				_trconnection.Open ();
			}
			return _trconnection;
		}

		internal override int ExecuteMultiCommands (IDbCommand[] dbcommands, SafeLevel level)
		{
			int rInt = 0;
			if (_isTransaction) {
				foreach (IDbCommand dbcommand in dbcommands) {
					_trconnection.SetupCommand (dbcommand);
					rInt += dbcommand.ExecuteNonQuery ();
				}
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				try {
					foreach (IDbCommand dbcommand in dbcommands) {
						transaction.SetupCommand (dbcommand);
						rInt += dbcommand.ExecuteNonQuery ();
					}
					transaction.Commit ();
				}
				catch (Exception ex) {
					transaction.Rollback ();
					throw ex;
				}
			}
			return rInt;
		}

		internal override object ExecuteInsertCommand (IDbCommand dbcommand, IDbCommand indentityCommand, SafeLevel level)
		{
			object result = null;
			if (_isTransaction) {
				_trconnection.SetupCommand (dbcommand);
				dbcommand.ExecuteNonQuery ();
				if (indentityCommand != null) {
					_trconnection.SetupCommand (indentityCommand);
					object obj = indentityCommand.ExecuteScalar ();
					if (obj != null) {
						result = obj;
					}
				}
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				try {
					transaction.SetupCommand (dbcommand);
					dbcommand.ExecuteNonQuery ();
					if (indentityCommand != null) {
						transaction.SetupCommand (indentityCommand);
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


		internal override int ExecuteNonQuery (IDbCommand dbcommand, SafeLevel level)
		{
			int rInt = 0;
			if (_isTransaction) {
				_trconnection.SetupCommand (dbcommand);
				rInt = dbcommand.ExecuteNonQuery ();
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				try {
					transaction.SetupCommand (dbcommand);
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



		internal override object ExecuteScalar (IDbCommand dbcommand, SafeLevel level)
		{
			object result = null;
			if (_isTransaction) {
				_trconnection.SetupCommand (dbcommand);
				result = dbcommand.ExecuteScalar ();
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				try {
					transaction.SetupCommand (dbcommand);
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

		internal override DataSet QueryDataSet (IDbCommand dbcommand, SafeLevel level)
		{
			DataSet ds = new DataSet ();
			if (_isTransaction) {
				_trconnection.SetupCommand (dbcommand);
				IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
				adapter.Fill (ds);
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				try {
					transaction.SetupCommand (dbcommand);
					IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
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

		internal override IEnumerable QueryDataReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level)
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
			if (_isTransaction) {
				_trconnection.SetupCommand (dbcommand);
				using (IDataReader reader = dbcommand.ExecuteReader ()) {
					int index = 0;
					int count = 0;
					while (reader.Read ()) {
						if (index >= start) {
							count++;
							object item = source.LoadData (this, reader);
							yield return item;
							if (count >= size) {
								dbcommand.Cancel ();
								break;
							}
						}
						index++;
					}
				}
			}
			else {
				TransactionConnection transaction = GetTransactionConnection ();
				transaction.ResetTransaction (level);
				transaction.SetupCommand (dbcommand);
				using (IDataReader reader = dbcommand.ExecuteReader ()) {
					int index = 0;
					int count = 0;
					while (reader.Read ()) {
						if (index >= start) {
							count++;
							object item = source.LoadData (this, reader);
							yield return item;
							if (count >= size) {
								dbcommand.Cancel ();
								break;
							}
						}
						index++;
					}
				}
				transaction.Commit ();
			}
		}

		/// <summary>
		/// 对象注销
		/// </summary>
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		/// <summary>
		/// protected的Dispose方法，保证不会被外部调用。
		/// </summary>
		/// <param name="disposing">传入bool值disposing以确定是否释放托管资源</param>
		protected void Dispose (bool disposing)
		{

			if (_isDisposed) {
				return;
			}

			if (disposing) {
				//在这里加入清理"托管资源"的代码
				//DisposeCache();
			}

			// 在这里加入清理"非托管资源"的代码
			if (_trconnection != null) {
				_trconnection.Dispose ();
				_trconnection = null;
			}
			_isDisposed = true;
		}

		/// <summary>
		/// 供GC调用的析构函数
		/// </summary>
		~TransDataContext ()
		{
			Dispose (false);//释放非托管资源
		}

	}
}
