using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using Light.Data.Mappings;

namespace Light.Data
{
	internal abstract class Database
	{
		protected CommandFactory _factory = null;

		protected int _commandTimeOut = 60000;

		protected bool _isInnerPager = false;

		/// <summary>
		/// 创建数据库连接
		/// </summary>
		/// <returns>数据库连接</returns>
		public abstract IDbConnection CreateConnection ();

		/// <summary>
		/// 创建数据库连接
		/// </summary>
		/// <param name="connectionString">连接字符串</param>
		/// <returns>数据库连接</returns>
		public abstract IDbConnection CreateConnection (string connectionString);

		/// <summary>
		/// 创建数据适配器
		/// </summary>
		/// <param name="cmd">数据库命令</param>
		/// <returns>数据适配器</returns>
		public abstract IDbDataAdapter CreateDataAdapter (IDbCommand cmd);

		/// <summary>
		/// 创建数据库命令
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <returns>数据库命令</returns>
		public abstract IDbCommand CreateCommand (string sql);

		/// <summary>
		/// 创建数据库命令
		/// </summary>
		/// <returns>数据库命令</returns>
		public abstract IDbCommand CreateCommand ();

		/// <summary>
		/// 创建SQL参数
		/// </summary>
		/// <param name="name">参数名称</param>
		/// <param name="value">参数值</param>
		/// <param name="dbType">参数数据库类型</param>
		/// <param name="direction">参数方向</param>
		/// <returns>SQL参数</returns>
		public abstract IDataParameter CreateParameter (string name, object value, string dbType, ParameterDirection direction);

		/// <summary>
		/// 获取数据库命令工厂
		/// </summary>
		public CommandFactory Factory {
			get {
				return _factory;
			}
		}

		/// <summary>
		/// 设置和获取命令超时时间
		/// </summary>
		public int CommandTimeOut {
			get {
				return _commandTimeOut;
			}
			set {
				if (value > 0) {
					_commandTimeOut = value;
				}
				else {
					throw new ArgumentOutOfRangeException ("CommandTimeOut");
				}
			}
		}

		/// <summary>
		/// 设置和获取是否使用内分页
		/// </summary>
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
		/// 格式化存储过程参数
		/// </summary>
		/// <param name="dataParmeter">存储过程参数</param>
		public abstract void FormatStoredProcedureParameter (IDataParameter dataParmeter);

		/// <summary>
		/// 设置扩展参数
		/// </summary>
		/// <param name="arguments">扩展参数</param>
		public virtual void SetExtentArguments (string arguments)
		{

		}
	}

}
