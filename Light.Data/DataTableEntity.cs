using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data
{
	/// <summary>
	/// 表数据实体
	/// </summary>
	public class DataTableEntity : DataEntity
	{

		/// <summary>
		/// 是否已经读取数据
		/// </summary>
		bool _hasLoadData = false;

		/// <summary>
		/// 保存数据
		/// </summary>
		public void Save ()
		{
			if (Context != null) {
				if (_hasLoadData) {
					Context.Update (this, GetUpdateFields ());
					Clear ();
				}
				else {
					Context.Insert (this);
					_hasLoadData = true;
				}
			}
			else {
				throw new LightDataException (RE.DataContextIsNotExists);
			}
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		public void Erase ()
		{
			if (Context != null) {
				Context.Delete (this);
				_hasLoadData = false;
			}
			else {
				throw new LightDataException (RE.DataContextIsNotExists);
			}
		}

		/// <summary>
		/// 被更新的数据字段
		/// </summary>
		List<string> _updateFields = null;


		/// <summary>
		/// 更新字段
		/// </summary>
		/// <param name="fieldName">字段名字</param>
		protected void UpdateDataNotify (string fieldName)
		{
			if (_updateFields == null) {
				_updateFields = new List<string> ();
			}
			if (!_updateFields.Contains (fieldName)) {
				_updateFields.Add (fieldName);
			}
		}

		internal string[] GetUpdateFields ()
		{
			if (_updateFields != null) {
				return _updateFields.ToArray ();
			}
			else {
				return null;
			}
           
		}

		private void Clear ()
		{
			if (_updateFields != null) {
				_updateFields.Clear ();
				_updateFields = null;
			}
		}

		/// <summary>
		/// 读取数据完成
		/// </summary>
		internal override void LoadDataComplete ()
		{
			_hasLoadData = true;
		}
	}
}
