using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 数据表别名
	/// </summary>
	public class DataTableAlias : IDisposable
	{
		Dictionary<Type, DataEntityMapping> _aliaslist = new Dictionary<Type, DataEntityMapping> ();

		/// <summary>
		/// 设置数据类型的别名
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="tableName">别名</param>
		public void Set<T> (string tableName) where T : class, new()
		{
			if (string.IsNullOrEmpty (tableName)) {
				throw new ArgumentNullException ("tableName");
			}
			Type type = typeof(T);
			DataEntityMapping mapping = null;
			if (_aliaslist.ContainsKey (type)) {
				mapping = _aliaslist [type];
			}
			else {
				mapping = DataMapping.GetEntityMapping (type);
				_aliaslist [type] = mapping;
			}
			mapping.SetAliasName (tableName);
		}

		/// <summary>
		/// 取消数据类型的别名
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void Unset<T> ()
		{
			Type type = typeof(T);
			if (_aliaslist.ContainsKey (type)) {
				DataEntityMapping mapping = _aliaslist [type];
				mapping.ClearAliasName ();
				_aliaslist.Remove (type);
			}
		}

		bool _isDisposed = false;

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

			}

			// 在这里加入清理"非托管资源"的代码
			foreach (KeyValuePair<Type, DataEntityMapping> kvs in _aliaslist) {
				kvs.Value.ClearAliasName ();
			}
			_aliaslist.Clear ();
			_isDisposed = true;
		}

		/// <summary>
		/// 供GC调用的析构函数
		/// </summary>
		~DataTableAlias ()
		{
			Dispose (false);//释放非托管资源
		}
	}
}
