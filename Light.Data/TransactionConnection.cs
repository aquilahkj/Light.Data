using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data
{
    class TransactionConnection : IDisposable
    {
        IDbTransaction _transaction = null;

        IDbConnection _connection = null;

        SafeLevel _level = SafeLevel.Default;

        bool _isDisposed = false;

        public TransactionConnection(IDbConnection connection, SafeLevel level)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _connection = connection;
            _level = level;
        }

        public void ResetTransaction(SafeLevel level)
        {
            _level = level;
            SetupTransaction();
        }

        private void SetupTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_level == SafeLevel.None)
            {
                _transaction = null;
            }
            else if (_level == SafeLevel.Default)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                IsolationLevel isoLevel;
                switch (_level)
                {
                    case SafeLevel.Low:
                        isoLevel = IsolationLevel.ReadUncommitted;
                        break;
                    case SafeLevel.High:
                        isoLevel = IsolationLevel.RepeatableRead;
                        break;
                    default:
                        isoLevel = IsolationLevel.ReadCommitted;
                        break;
                }
                _transaction = _connection.BeginTransaction(isoLevel);
            }
        }

        public void SetupCommand(IDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Connection = _connection;
        }

        public void Open()
        {
            _connection.Open();
            SetupTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                //在这里加入清理"托管资源"的代码
                //DisposeCache();
				if (_connection != null)
				{
					//_connection.Close();
					_connection.Dispose();
					_connection = null;
				}
				if (_transaction != null)
				{
					_transaction.Dispose();
					_transaction = null;
				}
            }

            // 在这里加入清理"非托管资源"的代码
           

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 供GC调用的析构函数
        /// </summary>
        ~TransactionConnection()
        {
            Dispose(false);//释放非托管资源
        }


    }
}
