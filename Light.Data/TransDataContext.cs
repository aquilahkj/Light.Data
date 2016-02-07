using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Configuration;

namespace Light.Data
{
	/// <summary>
	/// 进行事务的数据上下文
	/// </summary>
	public class TransDataContext : DataContext, IDisposable
	{
		TransactionConnection _transaction = null;

		//		bool _isTransaction = false;

		bool _isDisposed = false;

		internal TransDataContext (string connectionString, string configName, Database dataBase)
			: base (connectionString, configName, dataBase)
		{

		}

		/// <summary>
		/// Begins the trans.
		/// </summary>
		public void BeginTrans ()
		{
			BeginTrans (SafeLevel.Default);
		}


		/// <summary>
		/// 开始事务
		/// </summary>
		public void BeginTrans (SafeLevel level)
		{
//			if (_trconnection != null) {
//				if (_isTransaction) {
//					_trconnection.Rollback ();
//				}
//				_trconnection.Dispose ();
//			}
//			_isTransaction = true;
			ChecKStatus (false);
			if (_transaction != null) {
				throw new LightDataException (RE.TransactionAlreadyStarted);
			}
			else {
				if (level == SafeLevel.None) {
					_transaction = CreateTransactionConnection (SafeLevel.Default);
				}
				else {
					_transaction = CreateTransactionConnection (level);
				}
				_transaction.Open ();
			}
		}

		/// <summary>
		/// 提交事务
		/// </summary>
		public void CommitTrans ()
		{
//			if (_trconnection != null) {
//				if (_isTransaction) {
//					_trconnection.Commit ();
//				}
//				_isTransaction = false;
//			}
			ChecKStatus (true);
			_transaction.Commit ();
			_transaction.Dispose ();
			_transaction = null;

		}

		/// <summary>
		/// 回滚事务
		/// </summary>
		public void RollbackTrans ()
		{
//			if (_trconnection != null) {
//				if (_isTransaction) {
//					_trconnection.Rollback ();
//				}
//				_isTransaction = false;
//			}
			ChecKStatus (true);
			_transaction.Rollback ();
			_transaction.Dispose ();
			_transaction = null;
		}

		//		TransactionConnection GetTransactionConnection ()
		//		{
		//			if (_trconnection == null) {
		//				_trconnection = CreateTransactionConnection (SafeLevel.None);
		//				_trconnection.Open ();
		//			}
		//			return _trconnection;
		//			if (_trconnection == null) {
		//				throw new LightDataException (RE.TransactionNotStarted);
		//				_trconnection = CreateTransactionConnection (SafeLevel.None);
		//				_trconnection.Open ();
		//			}
		//			else {
		//				return _trconnection;
		//			}
		//		}

		internal override int[] ExecuteBluckInsertCommands (IDbCommand[] insertCommands, IDbCommand indentityCommand, SafeLevel level, out object lastId)
		{
//			if (_isTransaction) {
			ChecKStatus (true);
			int[] rInts = new int[insertCommands.Length];
			int index = 0;
			foreach (IDbCommand dbcommand in insertCommands) {
				_transaction.SetupCommand (dbcommand);
				OutputCommand ("ExecuteMultiCommands[Trans]", dbcommand, _transaction.Level);
				rInts [index] = dbcommand.ExecuteNonQuery ();
				index++;
			}
			if (indentityCommand != null) {
				_transaction.SetupCommand (indentityCommand);
				OutputCommand ("ExecuteInsertCommand_Indentity[Trans]", indentityCommand, _transaction.Level);
				lastId = indentityCommand.ExecuteScalar ();
			}
			else {
				lastId = null;
			}
			return rInts;
//			}
//			else {
//				return base.ExecuteBluckInsertCommands (insertCommands, indentityCommand, level, out  lastId);
//			}
		}

		internal override int[] ExecuteMultiCommands (IDbCommand[] dbcommands, SafeLevel level)
		{
//			if (_isTransaction) {
			ChecKStatus (true);
			int[] rInts = new int[dbcommands.Length];
			int index = 0;
			foreach (IDbCommand dbcommand in dbcommands) {
				_transaction.SetupCommand (dbcommand);
				OutputCommand ("ExecuteMultiCommands[Trans]", dbcommand, _transaction.Level);
				rInts [index] = dbcommand.ExecuteNonQuery ();
				index++;
			}
			return rInts;
//			}
//			else {
//				return base.ExecuteMultiCommands (dbcommands, level);
//				TransactionConnection transaction = GetTransactionConnection ();
//				transaction.ResetTransaction (level);
//				try {
//					int index = 0;
//					foreach (IDbCommand dbcommand in dbcommands) {
//						transaction.SetupCommand (dbcommand);
//						rInts [index] = dbcommand.ExecuteNonQuery ();
//						index++;
//					}
//					transaction.Commit ();
//				} catch (Exception ex) {
//					transaction.Rollback ();
//					throw ex;
//				}
//			}
		}

		internal override object ExecuteInsertCommand (IDbCommand dbcommand, IDbCommand indentityCommand, SafeLevel level)
		{
//			if (_isTransaction) {
			ChecKStatus (true);
			object result = null;
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("ExecuteInsertCommand[Trans]", dbcommand, _transaction.Level);
			dbcommand.ExecuteNonQuery ();
			if (indentityCommand != null) {
				_transaction.SetupCommand (indentityCommand);
				OutputCommand ("ExecuteInsertCommand_Indentity[Trans]", indentityCommand, _transaction.Level);
				object obj = indentityCommand.ExecuteScalar ();
				if (obj != null) {
					result = obj;
				}
			}
			return result;
//			}
//			else {
//				return base.ExecuteInsertCommand (dbcommand, indentityCommand, level);
//				TransactionConnection transaction = GetTransactionConnection ();
//				transaction.ResetTransaction (level);
//				try {
//					transaction.SetupCommand (dbcommand);
//					dbcommand.ExecuteNonQuery ();
//					if (indentityCommand != null) {
//						transaction.SetupCommand (indentityCommand);
//						object obj = indentityCommand.ExecuteScalar ();
//						if (obj != null) {
//							result = obj;
//						}
//					}
//					transaction.Commit ();
//				} catch (Exception ex) {
//					transaction.Rollback ();
//					throw ex;
//				}
//			}
		}

		internal override int ExecuteNonQuery (IDbCommand dbcommand, SafeLevel level)
		{
//			if (_isTransaction) {
			ChecKStatus (true);
			int rInt;
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("ExecuteNonQuery[Trans]", dbcommand, _transaction.Level);
			rInt = dbcommand.ExecuteNonQuery ();
			return rInt;
//			}
//			else {
//				return base.ExecuteNonQuery (dbcommand, level);
//				TransactionConnection transaction = GetTransactionConnection ();
//				transaction.ResetTransaction (level);
//				try {
//					transaction.SetupCommand (dbcommand);
//					rInt = dbcommand.ExecuteNonQuery ();
//					transaction.Commit ();
			//				} catch (Exception ex) {
//					transaction.Rollback ();
//					throw ex;
//				}
//			}
		}

		internal override object ExecuteScalar (IDbCommand dbcommand, SafeLevel level)
		{
//			if (_isTransaction) {
			ChecKStatus (true);
			object result;
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("ExecuteScalar[Trans]", dbcommand, _transaction.Level);
			result = dbcommand.ExecuteScalar ();
			return result;
//			}
//			else {
//				return base.ExecuteScalar (dbcommand, level);
//				TransactionConnection transaction = GetTransactionConnection ();
//				transaction.ResetTransaction (level);
//				try {
//					transaction.SetupCommand (dbcommand);
//					result = dbcommand.ExecuteScalar ();
//					transaction.Commit ();
//				} catch (Exception ex) {
//					transaction.Rollback ();
//					throw ex;
//				}
//			}

		}

		internal override DataSet QueryDataSet (IDbCommand dbcommand, SafeLevel level)
		{
			ChecKStatus (true);
			DataSet ds = new DataSet ();
			_transaction.SetupCommand (dbcommand);
			IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
			OutputCommand ("QueryDataSet[Trans]", dbcommand, _transaction.Level);
			adapter.Fill (ds);
			return ds;
		}

		internal override IEnumerable QueryDataReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level)
		{
			ChecKStatus (true);
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
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("QueryDataReader[Trans]", dbcommand, _transaction.Level, start, size);
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
						object item = source.LoadData (this, reader);
						if (count >= size) {
							over = true;
						}
						yield return item;
					}
					index++;
				}
			}
		}

		//		internal override DataSet QueryDataSet (IDbCommand dbcommand, SafeLevel level)
		//		{
		//			if (_isTransaction) {
		//				DataSet ds = new DataSet ();
		//				_trconnection.SetupCommand (dbcommand);
		//				IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
		//				adapter.Fill (ds);
		//				return ds;
		//			}
		//			else {
		//				return base.QueryDataSet (dbcommand, level);
		////				TransactionConnection transaction = GetTransactionConnection ();
		////				transaction.ResetTransaction (level);
		////				try {
		////					transaction.SetupCommand (dbcommand);
		////					IDbDataAdapter adapter = _dataBase.CreateDataAdapter (dbcommand);
		////					adapter.Fill (ds);
		////					transaction.Commit ();
		////				}
		////				catch (Exception ex) {
		////					transaction.Rollback ();
		////					throw ex;
		////				}
		//			}
		//
		//		}
		//
		//		internal override IEnumerable QueryDataReader (IDataDefine source, IDbCommand dbcommand, Region region, SafeLevel level)
		//		{
		//			if (_isTransaction) {
		//				int start;
		//				int size;
		//				if (region != null) {
		//					start = region.Start;
		//					size = region.Size;
		//				}
		//				else {
		//					start = 0;
		//					size = int.MaxValue;
		//				}
		//				_trconnection.SetupCommand (dbcommand);
		//				using (IDataReader reader = dbcommand.ExecuteReader ()) {
		//					int index = 0;
		//					int count = 0;
		//					while (reader.Read ()) {
		//						if (index >= start) {
		//							count++;
		//							object item = source.LoadData (this, reader);
		//							yield return item;
		//							if (count >= size) {
		//								dbcommand.Cancel ();
		//								break;
		//							}
		//						}
		//						index++;
		//					}
		//				}
		//			}
		//			else {
		//				foreach (object item in base.QueryDataReader (source, dbcommand, region, level)) {
		//					yield return item;
		//				}
		//					
		////				TransactionConnection transaction = GetTransactionConnection ();
		////				transaction.ResetTransaction (level);
		////				transaction.SetupCommand (dbcommand);
		////				using (IDataReader reader = dbcommand.ExecuteReader ()) {
		////					int index = 0;
		////					int count = 0;
		////					while (reader.Read ()) {
		////						if (index >= start) {
		////							count++;
		////							object item = source.LoadData (this, reader);
		////							yield return item;
		////							if (count >= size) {
		////								dbcommand.Cancel ();
		////								break;
		////							}
		////						}
		////						index++;
		////					}
		////				}
		////				transaction.Commit ();
		//			}
		//		}

		void ChecKStatus (bool checkStart)
		{
			if (this._isDisposed) {
				throw new LightDataException (RE.TransactionAlreadyDisposed);
			}
			if (checkStart && _transaction == null) {
				throw new LightDataException (RE.TransactionNotStarted);
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
			if (_transaction != null) {
				_transaction.Dispose ();
				_transaction = null;
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
