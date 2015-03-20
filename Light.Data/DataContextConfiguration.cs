using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 数据库连接配置器
	/// </summary>
	public static class DataContextConfiguration
	{
		/// <summary>
		/// 默认数据上下文
		/// </summary>
		static DataContext _defaultContext = null;

		static DataContextCollection _collection = null;

		/// <summary>
		/// 数据库连接集合
		/// </summary>
		public static DataContextCollection ContextCollection {
			get {
				return DataContextConfiguration._collection;
			}
		}

		/// <summary>
		/// 设置和获取默认数据上下文
		/// </summary>
		public static DataContext Default {
			get {
				return DataContextConfiguration._defaultContext;
			}
			set {
				DataContextConfiguration._defaultContext = value;
			}
		}

		/// <summary>
		/// 静态构造函数
		/// </summary>
		static DataContextConfiguration ()
		{
			_collection = new DataContextCollection ();
			if (_collection.Count > 0) {
				DataContextConfiguration._defaultContext = _collection.Default;
			}
		}
	}
}
