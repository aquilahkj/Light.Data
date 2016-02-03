using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Light.Data;

namespace Light.Data
{
	abstract class DataMapping : IFieldCollection
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, DataMapping> _defaultMapping = new Dictionary<Type, DataMapping> ();

		public static DataMapping GetMapping (Type type)
		{
			Dictionary<Type, DataMapping> mappings = _defaultMapping;
			DataMapping mapping = null;
			mappings.TryGetValue (type, out mapping);
			if (mapping == null) {
				lock (_synobj) {
					mappings.TryGetValue (type, out mapping);
					if (mapping == null) {
						try {
							mapping = CreateMapping (type);
						} catch (Exception ex) {
							mapping = new ErrorDataMapping (ex);
						}
						mappings [type] = mapping;
					}
				}
			}
			ErrorDataMapping errMapping = mapping as ErrorDataMapping;
			if (errMapping != null) {
				throw errMapping.InnerException;
			}
			else {
				return mapping;
			}
		}

		/// <summary>
		/// 获取数据表映射图
		/// </summary>
		/// <param name="type">映射类型</param>
		/// <returns>数据表映射图</returns>
		public static DataTableEntityMapping GetTableMapping (Type type)
		{
			DataTableEntityMapping dataMapping = GetMapping (type) as DataTableEntityMapping;
			if (dataMapping == null) {
				throw new LightDataException (RE.TheDataMappingIsNotDataTableMapping);
			}
			else {
				return dataMapping;
			}
		}

		/// <summary>
		/// 获取关系映射图
		/// </summary>
		/// <param name="type">映射类型</param>
		/// <returns>关系映射图</returns>
		public static DataEntityMapping GetEntityMapping (Type type)
		{
			DataEntityMapping dataMapping = GetMapping (type) as DataEntityMapping;
			if (dataMapping == null) {
				throw new LightDataException (RE.TheDataMappingIsNotDataEntityMapping);
			}
			else {
				return dataMapping;
			}
		}

		/// <summary>
		/// 创建关系映射图
		/// </summary>
		/// <param name="type">映射类型</param>
		/// <returns>关系映射图</returns>
		private static DataMapping CreateMapping (Type type)
		{
			string tableName = null;
			string extentParam = null;
			bool isEntityTable = true;
			DataMapping dataMapping;

			IDataTableConfig config = ConfigManager.LoadDataTableConfig (type);
			if (config != null) {
				tableName = config.TableName;
				extentParam = config.ExtendParams;
				isEntityTable = config.IsEntityTable;
			}

			if (string.IsNullOrEmpty (tableName)) {
				tableName = type.Name;
			}

			if (type.IsSubclassOf (typeof(DataTableEntity))) {
				dataMapping = new DataTableEntityMapping (type, tableName, true);
			}
			else if (type.IsSubclassOf (typeof(DataEntity))) {
				dataMapping = new DataEntityMapping (type, tableName, true);
			}
			else {
				if (!isEntityTable) {
					dataMapping = new DataEntityMapping (type, tableName, false);
				}
				else {
					dataMapping = new DataTableEntityMapping (type, tableName, false);
				}
			}
			dataMapping.ExtentParams = new ExtendParamsCollection (extentParam);
			return dataMapping;
		}

		#endregion

		protected Dictionary<string, FieldMapping> _fieldMappingDictionary = new Dictionary<string, FieldMapping> ();

		//		protected Dictionary<string, FieldMapping> _fieldMappingAlterNameDictionary = new Dictionary<string, FieldMapping> ();
		//
		//		protected Dictionary<string, FieldMapping> _fieldMappingAllNameDictionary = new Dictionary<string, FieldMapping> ();

		protected List<FieldMapping> _fieldList = new List<FieldMapping> ();

		protected List<string> _fieldNames = new List<string> ();

		protected DataMapping (Type type)
		{
			this.objectType = type;

		}

		Type objectType;

		/// <summary>
		/// 映射数据类型
		/// </summary>
		public Type ObjectType {
			get {
				return objectType;
			}
			private set {
				objectType = value;
			}
		}

		ExtendParamsCollection extentParams;

		/// <summary>
		/// 扩展参数集合
		/// </summary>
		public ExtendParamsCollection ExtentParams {
			get {
				return extentParams;
			}
			set {
				extentParams = value;
			}
		}

		/// <summary>
		/// 获取字段名数组
		/// </summary>
		/// <returns></returns>
		public string[] GetFieldNames ()
		{
//			return GetFieldNames (GetFieldMappings ());
			return this._fieldNames.ToArray();
		}

		//		private string[] GetFieldNames (IEnumerable<FieldMapping> fields)
		//		{
		//			List<string> list = new List<string> ();
		//			foreach (DataFieldMapping field in fields) {
		////				ComplexFieldMapping cmField = field as ComplexFieldMapping;
		////				if (cmField != null) {
		////					list.AddRange (GetFieldNames (cmField.GetFieldMappings ()));
		////				}
		////				else {
		////					list.Add (field.Name);
		////				}
		//				list.Add (field.Name);
		//			}
		//			return list.ToArray ();
		//		}

		#region IFieldCollection 成员

//		public virtual IEnumerable<FieldMapping> GetFieldMappings ()
//		{
//			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingDictionary) {
//				yield return kv.Value;
//			}
//		}

		public FieldMapping[] GetFieldMappings ()
		{
			return this._fieldList.ToArray ();
		}

		public int FieldCount {
			get {
				return this._fieldList.Count;
			}
		}

		public virtual FieldMapping FindFieldMapping (string fieldName)
		{
//			if (!_fieldMappingAllNameDictionary.ContainsKey (fieldName)) {
//				lock (_fieldMappingAllNameDictionary) {
//					if (!_fieldMappingAllNameDictionary.ContainsKey (fieldName)) {
//						FieldMapping mapping = SearchFieldMapping (fieldName);
//						if (mapping != null) {
//							_fieldMappingAllNameDictionary [fieldName] = mapping;
//						}
//						return mapping;
//					}
//				}
//			}
//			return _fieldMappingAllNameDictionary [fieldName];
			FieldMapping mapping;
			_fieldMappingDictionary.TryGetValue (fieldName, out mapping);
			return mapping;

		}

		//		private FieldMapping SearchFieldMapping (string fieldName)
		//		{
		//			FieldMapping mapping;
		//			if (_fieldMappingDictionary.TryGetValue (fieldName, out mapping)) {
		//				return mapping;
		//			}
		//			if (_fieldMappingAlterNameDictionary.TryGetValue (fieldName, out mapping)) {
		//				return mapping;
		//			}
		//			return null;
		////			if (_fieldMappingDictionary.ContainsKey (fieldName)) {
		////				FieldMapping m = _fieldMappingDictionary [fieldName];
		////				if (m is PrimitiveFieldMapping || m is EnumFieldMapping) {
		////					return m;
		////				}
		////			}
		////			if (_fieldMappingAlterNameDictionary.ContainsKey (fieldName)) {
		////				FieldMapping m = _fieldMappingAlterNameDictionary [fieldName];
		////				if (m is PrimitiveFieldMapping || m is EnumFieldMapping) {
		////					return m;
		////				}
		////			}
		//
		////			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingDictionary) {
		////				if (fieldName.StartsWith (kv.Key + "_") && kv.Value is ComplexFieldMapping) {
		////					return ((ComplexFieldMapping)kv.Value).FindFieldMapping (fieldName);
		////				}
		////			}
		////
		////			foreach (KeyValuePair<string, FieldMapping> kv in _fieldMappingAlterNameDictionary) {
		////				if (fieldName.StartsWith (kv.Key + "_") && kv.Value is ComplexFieldMapping) {
		////					return ((ComplexFieldMapping)kv.Value).FindFieldMapping (fieldName);
		////				}
		////			}
		////			return null;
		//		}
		//
		//		public abstract void InitialDataFieldMapping ();

		public abstract object LoadData (DataContext context, IDataReader datareader);

		public abstract object LoadData (DataContext context, DataRow datarow);

		public abstract object InitialData ();

		#endregion
	}
}
