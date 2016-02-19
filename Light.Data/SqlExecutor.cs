using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace Light.Data
{
	/// <summary>
	/// SQL语句执行器,直接执行SQL语句
	/// </summary>
	public class SqlExecutor
	{
		/// <summary>
		/// 数据库命令
		/// </summary>
		IDbCommand _command;

		/// <summary>
		/// 数据库上下文
		/// </summary>
		DataContext _context;

		/// <summary>
		/// 安全级别
		/// </summary>
		SafeLevel _level = SafeLevel.Default;

		/// <summary>
		/// 设置命令超时
		/// </summary>
		public int CommandTimeOut {
			get {
				return _command.CommandTimeout;
			}
			set {
				_command.CommandTimeout = value;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数</param>
		/// <param name="commandType">命令类型</param>
		/// <param name="level">安全级别</param>
		/// <param name="context">数据库上下文</param>
		internal SqlExecutor (string sql, DataParameter[] parameters, CommandType commandType, SafeLevel level, DataContext context)
		{
			_level = level;
			_context = context;
			_command = context.DataBase.CreateCommand (sql);
			_command.CommandType = commandType;
			if (parameters != null) {
				foreach (DataParameter param in parameters) {
					string parameterName = param.ParameterName;
					if (parameterName.StartsWith ("@") && parameterName.Length > 1) {
						parameterName = parameterName.Substring (1);
					}
					IDataParameter dataParameter = context.DataBase.CreateParameter (parameterName, param.Value, param.DbType, param.Direction);
					param.SetDataParameter (dataParameter);
					_command.Parameters.Add (dataParameter);
					if (commandType == CommandType.Text) {
						_command.CommandText = _command.CommandText.Replace (param.ParameterName, dataParameter.ParameterName);
					}
					else if (commandType == CommandType.StoredProcedure) {
						context.DataBase.FormatStoredProcedureParameter (dataParameter);
					}
				}
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="command">数据库命令</param>
		/// <param name="context">数据库上下文</param>
		internal SqlExecutor (IDbCommand command, DataContext context)
		{
			_context = context;
			_command = command;
		}

		/// <summary>
		/// 执行语句
		/// </summary>
		/// <returns>受影响的行数</returns>
		public int ExecuteNonQuery ()
		{
			return _context.ExecuteNonQuery (_command, _level);
		}

		/// <summary>
		/// 执行语句并返回结果
		/// </summary>
		/// <returns>返回结果</returns>
		public object ExecuteScalar ()
		{
			return _context.ExecuteScalar (_command, _level);
		}

		/// <summary>
		/// 查询DataSet数据集
		/// </summary>
		/// <returns>DataSet数据集</returns>
		public DataSet QueryDataSet ()
		{
			return _context.QueryDataSet (_command, _level);
		}

		/// <summary>
		/// 查询DataTable数据集
		/// </summary>
		/// <param name="region">查询范围</param>
		/// <returns>DataSet数据集</returns>
		public DataTable QueryDataTable (Region region)
		{
			return _context.QueryDataTable (_command, region, _level);
		}

		/// <summary>
		/// 查询DataTable数据集
		/// </summary>
		/// <returns>DataSet数据集</returns>
		public DataTable QueryDataTable ()
		{
			return QueryDataTable (null);
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
		/// 查询并返回所有指定类型的数据集合
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <returns>数据集合</returns>
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
