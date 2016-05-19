using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	/// <summary>
	/// SqlString executor.
	/// </summary>
	public class SqlExecutor
	{
		IDbCommand _command;

		DataContext _context;

		SafeLevel _level = SafeLevel.Default;

		/// <summary>
		/// Gets or sets the command time out.
		/// </summary>
		/// <value>The command time out.</value>
		public int CommandTimeOut {
			get {
				return _command.CommandTimeout;
			}
			set {
				_command.CommandTimeout = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.SqlExecutor"/> class.
		/// </summary>
		/// <param name="sql">Sql.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="commandType">Command type.</param>
		/// <param name="level">Level.</param>
		/// <param name="context">Context.</param>
		internal SqlExecutor (string sql, DataParameter[] parameters, CommandType commandType, SafeLevel level, DataContext context)
		{
			_level = level;
			_context = context;
			_command = context.DataBase.CreateCommand (sql);
			_command.CommandType = commandType;
			if (parameters != null) {
				foreach (DataParameter param in parameters) {
					string parameterName = param.ParameterName;

//					if (parameterName.StartsWith ("@")) {
//						parameterName = parameterName.TrimStart ('@');
//					}
//					string mName = "@" + parameterName;
					IDataParameter dataParameter = context.DataBase.CreateParameter (parameterName, param.Value, param.DbType, param.Direction);
					param.SetDataParameter (dataParameter);
					_command.Parameters.Add (dataParameter);
//					if (commandType == CommandType.Text) {
//						if (parameterName != dataParameter.ParameterName) {
//							_command.CommandText = _command.CommandText.Replace (parameterName, dataParameter.ParameterName);
//						}
//					}
//					else if (commandType == CommandType.StoredProcedure) {
//						context.DataBase.FormatStoredProcedureParameter (dataParameter);
//					}
					if (commandType == CommandType.StoredProcedure) {
						context.DataBase.FormatStoredProcedureParameter (dataParameter);
					}
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.SqlExecutor"/> class.
		/// </summary>
		/// <param name="command">Command.</param>
		/// <param name="context">Context.</param>
		internal SqlExecutor (IDbCommand command, DataContext context)
		{
			_context = context;
			_command = command;
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <returns>The non query.</returns>
		public int ExecuteNonQuery ()
		{
			return _context.ExecuteNonQuery (_command, _level);
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <returns>The scalar.</returns>
		public object ExecuteScalar ()
		{
			return _context.ExecuteScalar (_command, _level);
		}

		/// <summary>
		/// Queries the data set.
		/// </summary>
		/// <returns>The data set.</returns>
		public DataSet QueryDataSet ()
		{
			return _context.QueryDataSet (_command, _level);
		}

		/// <summary>
		/// Queries the data table.
		/// </summary>
		/// <returns>The data table.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public DataTable QueryDataTable (int from, int to)
		{
			int start = from;
			int size = to - from;
			Region region = new Region (start, size);
			return _context.QueryDataTable (_command, region, _level);
		}

		/// <summary>
		/// Queries the data table.
		/// </summary>
		/// <returns>The data table.</returns>
		public DataTable QueryDataTable ()
		{
			return _context.QueryDataTable (_command, null, _level);
		}

		/// <summary>
		/// 查询并返回指定类型的数据集合
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="region">查询范围</param>
		/// <returns>数据集合</returns>
		private List<T> QueryList<T> (Region region) where T : class, new()
		{
			List<T> list = new List<T> ();
			IEnumerable<T> ie = _context.QueryDataMappingReader<T> (DataMapping.GetMapping (typeof(T)), _command, region, _level, null);
			list.AddRange (ie);
			return list;
		}

		/// <summary>
		/// Queries the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public List<T> QueryList<T> () where T : class, new()
		{
			return QueryList<T> (null);
		}

		/// <summary>
		/// Queries the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="start">Start.</param>
		/// <param name="size">Size.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public List<T> QueryList<T> (int start, int size) where T : class, new()
		{
			if (start < 0) {
				throw new ArgumentOutOfRangeException ("size");
			}
			if (size < 1) {
				throw new ArgumentOutOfRangeException ("size");
			}
			Region region = new Region (start, size);
			return QueryList<T> (region);
		}

		/// <summary>
		/// Query the specified start and size.
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="size">Size.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public IEnumerable Query<T> (int start, int size) where T : class, new()
		{
			if (start < 0) {
				throw new ArgumentOutOfRangeException ("size");
			}
			if (size < 1) {
				throw new ArgumentOutOfRangeException ("size");
			}
			Region region = new Region (start, size);
			return Query<T> (region);
		}

		/// <summary>
		/// 查询并返回指定类型的枚举数据
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="region">查询范围</param>
		/// <returns>枚举数据</returns>
		private IEnumerable<T> Query<T> (Region region) where T : class, new()
		{
			return _context.QueryDataMappingReader<T> (DataMapping.GetMapping (typeof(T)), _command, region, _level, null);
		}

		/// <summary>
		/// 查询并返回指定类型的枚举数据
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <returns>枚举数据</returns>
		public IEnumerable<T> Query<T> () where T : class, new()
		{
			return Query<T> (null);
		}
	}
}
