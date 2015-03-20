using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using Light.Data.Handler;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 基础数据实体
	/// </summary>
	public abstract class DataEntity
	{
		/// <summary>
		/// 数据库上下文
		/// </summary>
		DataContext _context = null;

		/// <summary>
		/// 数据库上下文
		/// </summary>
		internal DataContext Context {
			get {
				return _context;
			}
		}

		/// <summary>
		/// 设置当前数据库连接上下文到对象
		/// </summary>
		/// <param name="context">数据库连接上下文</param>
		public void SetContext (DataContext context)
		{
			if (context == null) {
				throw new ArgumentNullException ("DataContext");
			}
			_context = context;
		}

		/// <summary>
		///  线程同步锁
		/// </summary>
		object synobj1 = new object ();

		/// <summary>
		///  线程同步锁
		/// </summary>
		object synobj2 = new object ();

		/// <summary>
		/// 关联数据储存
		/// </summary>
		Dictionary<string, object> _tempRelationDatas = null;

		/// <summary>
		/// 关联数据储存
		/// </summary>
		internal Dictionary<string, object> TempRelationDatas {
			get {
				//有需要才建立字典对象
				if (_tempRelationDatas == null) {
					lock (synobj1) {
						if (_tempRelationDatas == null) {
							_tempRelationDatas = new Dictionary<string, object> ();
						}
					}
				}
				return _tempRelationDatas;
			}
		}

		/// <summary>
		/// 重置关联数据
		/// </summary>
		public void RefleshData ()
		{
			if (_tempRelationDatas != null) {
				lock (synobj1) {
					_tempRelationDatas.Clear ();
				}
			}
		}


		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <param name="cacheData">是否缓存数据</param>
		/// <returns>关联数据</returns>
		protected object LoadRelationData (string keyName, bool cacheData)
		{
			return LoadRelationData (keyName, null, null, cacheData);
		}

		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <param name="extendQuery">扩展查询</param>
		/// <param name="extendOrder">扩展排序</param>
		/// <param name="cacheData">是否缓存数据</param>
		/// <returns>关联数据</returns>
		protected object LoadRelationData (string keyName, QueryExpression extendQuery, OrderExpression extendOrder, bool cacheData)
		{
			if (Context == null) {
				return null;
			}
			if (!cacheData) {
				return LoadRelationData (keyName, extendQuery, extendOrder);
			}
			if (!TempRelationDatas.ContainsKey (keyName)) {
				lock (synobj2) {
					if (!TempRelationDatas.ContainsKey (keyName)) {
						TempRelationDatas [keyName] = LoadRelationData (keyName, extendQuery, extendOrder);
					}
				}
			}
			return TempRelationDatas [keyName];
		}

		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <returns>关联数据</returns>
		protected object LoadRelationData (string keyName)
		{
			return LoadRelationData (keyName, null, null, true);
		}

		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <param name="extendQuery">扩展查询</param>
		/// <returns>关联数据</returns>
		protected object LoadRelationData (string keyName, QueryExpression extendQuery)
		{
			return LoadRelationData (keyName, extendQuery, null, true);
		}

		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <param name="extendOrder">扩展排序</param>
		/// <returns>关联数据</returns>
		protected object LoadRelationData (string keyName, OrderExpression extendOrder)
		{
			return LoadRelationData (keyName, null, extendOrder, true);
		}

		/// <summary>
		/// 读取关联数据
		/// </summary>
		/// <param name="keyName">关联字段属性名称</param>
		/// <param name="extendQuery">扩展查询</param>
		/// <param name="extendOrder">扩展排序</param>
		/// <returns>关联数据</returns>
		private object LoadRelationData (string keyName, QueryExpression extendQuery, OrderExpression extendOrder)
		{
			DataEntityMapping selfMapping = DataMapping.GetEntityMapping (this.GetType ());
			RelationFieldMapping relationMapping = selfMapping.FindRelateionMapping (keyName);
			QueryExpression expression = null;
			foreach (RelationFieldMapping.RelationKeyValue rt in relationMapping.GetRelationKeyValues()) {
				DataFieldInfo info = new DataFieldInfo (relationMapping.RelateTableMapping.ObjectType, rt.RelateField.Name);
				object objkey = rt.MasterField.Handler.Get (this);
				if (Object.Equals (objkey, null)) {
					expression &= info.IsNull ();
				}
				else {
					expression &= info == objkey;
				}
			}
			if (extendQuery != null) {
				expression = QueryExpression.And (expression, extendQuery);
			}
			//判断与关联表的关联关系
			if (relationMapping.Kind == RelationFieldMapping.RelationKind.OneToOne) {
				//一对一关系,直接查询单个对象
				object obj = this.Context.SelectSingle (relationMapping.RelateTableMapping, expression, extendOrder, 0, SafeLevel.None);
				DataEntity de = obj as DataEntity;
				if (de == null)
					return obj;
				if (relationMapping.RelateRelationMapping != null) {//判断是否相互关联
					if (relationMapping.RelateRelationMapping.Kind == RelationFieldMapping.RelationKind.OneToOne) {
						de.TempRelationDatas.Add (relationMapping.RelateRelationMapping.RelationName, this);
					}
				}
				return de;
			}
			else {
				IList list = this.Context.QueryDataList (relationMapping.RelateTableMapping, expression, extendOrder, null, SafeLevel.Default) as IList;
				//if (list.Count == 0) return list;

				if (relationMapping.RelateRelationMapping != null && relationMapping.RelateRelationMapping.MasterTableMapping.IsDataEntity) {
					if (relationMapping.RelateRelationMapping.Kind == RelationFieldMapping.RelationKind.OneToOne) {
						foreach (object obj in list) {
							DataEntity de = obj as DataEntity;
							de.TempRelationDatas.Add (relationMapping.RelateRelationMapping.RelationName, this);
						}
					}
				}
				if (relationMapping.ResultDataKind == RelationFieldMapping.DataKind.IList) {
					return list;
				}
				else {
					Array array = Array.CreateInstance (relationMapping.RelateTableMapping.ObjectType, list.Count);
					list.CopyTo (array, 0);
					return array;
				}
			}
		}

		/// <summary>
		/// 数据读取完成
		/// </summary>
		internal virtual void LoadDataComplete ()
		{

		}
	}
}
