using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;

namespace Light.Data
{
	/// <summary>
	/// 数据库连接集合
	/// </summary>
	public class DataContextCollection : ICollection
	{
		Dictionary<string, DataContext> _contextNameDictionary = new Dictionary<string, DataContext> ();

		DataContext _default = null;

		/// <summary>
		/// 默认数据上下文
		/// </summary>
		public DataContext Default {
			get {
				return _default;
			}
		}

		object _syncRoot = null;

		/// <summary>
		/// 构造函数
		/// </summary>
		internal DataContextCollection ()
		{
			if (this._syncRoot == null) {
				Interlocked.CompareExchange (ref this._syncRoot, new object (), null);
			}
			foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings) {
				DataContext context = DataContext.Create (setting, false);
				if (context != null) {
					_contextNameDictionary.Add (setting.Name, context);
					if (_default == null) {
						_default = context;
					}
				}
			}
		}

		#region ICollection 成员

		/// <summary>
		/// 复制
		/// </summary>
		/// <param name="array">对象数组</param>
		/// <param name="index">复制索引</param>
		public void CopyTo (Array array, int index)
		{
			DataContext[] values = new DataContext[_contextNameDictionary.Count];
			int i = 0;
			foreach (DataContext context in _contextNameDictionary.Values) {
				values [i] = context;
				i++;
			}
			values.CopyTo (array, index);
		}

		/// <summary>
		/// 集合数量
		/// </summary>
		public int Count {
			get {
				return _contextNameDictionary.Count;
			}
		}

		/// <summary>
		/// 是否同步
		/// </summary>
		public bool IsSynchronized {
			get {
				return true;
			}
		}

		/// <summary>
		/// 同步对象
		/// </summary>
		public object SyncRoot {
			get {
				return this._syncRoot;
			}
		}

		#endregion

		#region IEnumerable 成员

		/// <summary>
		/// 枚举接口
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator ()
		{
			return _contextNameDictionary.Values.GetEnumerator ();
		}

		#endregion

		/// <summary>
		/// 获取该连接名称的数据库连接
		/// </summary>
		/// <param name="connectionStringName">数据库连接名称</param>
		/// <returns>数据库连接</returns>
		public DataContext this [string connectionStringName] {
			get {
				if (!_contextNameDictionary.ContainsKey (connectionStringName)) {
					ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings [connectionStringName];
					if (setting == null) {
						return null;
					}
					DataContext context = DataContext.Create (setting, true);
					_contextNameDictionary.Add (connectionStringName, context);
					return context;
				}
				else {
					return _contextNameDictionary [connectionStringName];
				}
			}
		}
	}
}
