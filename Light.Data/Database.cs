using System;
using System.Data;

namespace Light.Data
{
	abstract class Database
	{
		protected CommandFactory _factory;

		protected int _commandTimeOut = 60000;

		protected bool _isInnerPager;

		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>The connection.</returns>
		public abstract IDbConnection CreateConnection ();

		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>The connection.</returns>
		/// <param name="connectionString">Connection string.</param>
		public abstract IDbConnection CreateConnection (string connectionString);

		/// <summary>
		/// Creates the data adapter.
		/// </summary>
		/// <returns>The data adapter.</returns>
		/// <param name="cmd">Cmd.</param>
		public abstract IDbDataAdapter CreateDataAdapter (IDbCommand cmd);

		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="sql">Sql.</param>
		public abstract IDbCommand CreateCommand (string sql);

		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <returns>The command.</returns>
		public abstract IDbCommand CreateCommand ();

		/// <summary>
		/// Creates the parameter.
		/// </summary>
		/// <returns>The parameter.</returns>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <param name="dbType">Db type.</param>
		/// <param name="direction">Direction.</param>
		public abstract IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction);

		/// <summary>
		/// Gets the factory.
		/// </summary>
		/// <value>The factory.</value>
		public CommandFactory Factory {
			get {
				return _factory;
			}
		}

		/// <summary>
		/// Gets or sets the command time out.
		/// </summary>
		/// <value>The command time out.</value>
		public int CommandTimeOut {
			get {
				return _commandTimeOut;
			}
			set {
				if (value > 0) {
					_commandTimeOut = value;
				}
				else {
					throw new ArgumentOutOfRangeException ("value");
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Light.Data.Database"/> inner pager.
		/// </summary>
		/// <value><c>true</c> if inner pager; otherwise, <c>false</c>.</value>
		public bool InnerPager {
			get {
				return _isInnerPager;
			}
			set {
				if (value) {
					_isInnerPager = Factory.CanInnerPager;
				}
				else {
					_isInnerPager = false;
				}
			}
		}

		/// <summary>
		/// Formats the stored procedure parameter.
		/// </summary>
		/// <param name="dataParmeter">Data parmeter.</param>
		public abstract void FormatStoredProcedureParameter (IDataParameter dataParmeter);

		/// <summary>
		/// Sets the extent arguments.
		/// </summary>
		/// <param name="arguments">Arguments.</param>
		public virtual void SetExtentArguments (string arguments)
		{

		}
	}

}
