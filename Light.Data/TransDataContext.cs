using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// 进行事务的数据上下文
	/// </summary>
	public class TransDataContext : DataContext, IDisposable
	{
		TransactionConnection _transaction;

		bool _isDisposed;

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
			ChecKStatus (true);
			_transaction.Rollback ();
			_transaction.Dispose ();
			_transaction = null;
		}

		internal override int[] ExecuteBluckInsertCommands (IDbCommand[] insertCommands, IDbCommand indentityCommand, SafeLevel level, out object lastId)
		{
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
		}

		internal override int[] ExecuteMultiCommands (IDbCommand[] dbcommands, SafeLevel level)
		{
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
		}

		internal override object ExecuteInsertCommand (IDbCommand dbcommand, IDbCommand indentityCommand, SafeLevel level)
		{
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
		}

		internal override int ExecuteNonQuery (IDbCommand dbcommand, SafeLevel level)
		{
			ChecKStatus (true);
			int rInt;
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("ExecuteNonQuery[Trans]", dbcommand, _transaction.Level);
			rInt = dbcommand.ExecuteNonQuery ();
			return rInt;
		}

		internal override object ExecuteScalar (IDbCommand dbcommand, SafeLevel level)
		{
			ChecKStatus (true);
			object result;
			_transaction.SetupCommand (dbcommand);
			OutputCommand ("ExecuteScalar[Trans]", dbcommand, _transaction.Level);
			result = dbcommand.ExecuteScalar ();
			return result;
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

		internal override IEnumerable<T> QueryDataMappingReader<T> (DataMapping source, IDbCommand dbcommand, Region region, SafeLevel level)
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
			OutputCommand ("QueryDataMappingReader[Trans]", dbcommand, level, start, size);
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
						yield return item as T;
					}
					index++;
				}
			}
		}

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
