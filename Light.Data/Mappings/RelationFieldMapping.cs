using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Light.Data.Handler;

namespace Light.Data.Mappings
{
	/// <summary>
	/// 关联映射表
	/// </summary>
	class RelationFieldMapping
	{
		/// <summary>
		/// 线程同步1
		/// </summary>
		object _synobj1 = new object ();

		/// <summary>
		/// 线程同步2
		/// </summary>
		object _synobj2 = new object ();

		/// <summary>
		/// 关联类型
		/// </summary>
		Type _relationTableType = null;

		/// <summary>
		/// 关联键值对应字典
		/// </summary>
		Dictionary<string, string> _relationKeys = new Dictionary<string, string> ();

		/// <summary>
		/// 关联键值数组
		/// </summary>
		RelationKeyValue[] _relationKeyValues = null;

		/// <summary>
		/// 关联方式
		/// </summary>
		RelationKind _relationKind;

		/// <summary>
		/// 关联数据类型
		/// </summary>
		DataKind _resultDataKind;

		/// <summary>
		/// 关联关系名称
		/// </summary>
		string _relationName = null;

		/// <summary>
		/// 主表映射
		/// </summary>
		DataEntityMapping _masterTableMapping = null;

		/// <summary>
		/// 关联表映射
		/// </summary>
		DataEntityMapping _relateTableMapping = null;

		/// <summary>
		/// 是否已经Load关联表的关联映射
		/// </summary>
		bool _hasLoadRelateRelationMapping = false;


		/// <summary>
		/// 关联表的关联映射
		/// </summary>
		RelationFieldMapping _relateRelationMapping = null;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="masterTableMapping">主关联表的映射</param>
		/// <param name="relateTableType">关联类型</param>
		/// <param name="relationName">关联名</param>
		public RelationFieldMapping (DataEntityMapping masterTableMapping, Type relateTableType, string relationName)
		{
			_masterTableMapping = masterTableMapping;
			_relationName = relationName;
			if (relateTableType.IsArray) {
				_relationTableType = relateTableType.GetElementType ();
				_relationKind = RelationKind.OneToMany;
				_resultDataKind = DataKind.Array;
			}
			else if (relateTableType.IsGenericType) {
				Type frameType = relateTableType.GetGenericTypeDefinition ();
				if (frameType.FullName != "System.Collections.Generic.IList`1") {
					throw new LightDataException (RE.TheRelationTypeNotIList);
				}
				else {
					Type[] arguments = relateTableType.GetGenericArguments ();
					_relationTableType = arguments [0];
					_relationKind = RelationKind.OneToMany;
					_resultDataKind = DataKind.IList;
				}
			}
			else {
				_relationTableType = relateTableType;
				_relationKind = RelationKind.OneToOne;
				_resultDataKind = DataKind.SingleObject;
			}
		}

		/// <summary>
		/// 添加关联键值对应关系
		/// </summary>
		/// <param name="masterKey"></param>
		/// <param name="relationKey"></param>
		public void AddRelationKeys (string masterKey, string relationKey)
		{
			if (string.IsNullOrEmpty (masterKey)) {
				throw new ArgumentNullException ("MasterKey");
			}
			if (string.IsNullOrEmpty (relationKey)) {
				throw new ArgumentNullException ("RelationKey");
			}
			lock (_synobj2) {
				if (!_relationKeys.ContainsKey (masterKey)) {
					_relationKeys.Add (masterKey, relationKey);
				}
				else {
					throw new LightDataException (RE.RelationMasterKeyIsExists);
				}
				_relationKeyValues = null;
			}
		}

		public IEnumerable<RelationKeyValue> GetRelationKeyValues ()
		{
			if (_relationKeyValues == null) {
				lock (_synobj2) {
					if (_relationKeyValues == null) {
						List<RelationKeyValue> tables = new List<RelationKeyValue> ();
						foreach (KeyValuePair<string, string> keys in _relationKeys) {
							PrimitiveFieldMapping masterField = MasterTableMapping.FindFieldMapping (keys.Key) as PrimitiveFieldMapping;
							if (masterField == null) {
								throw new LightDataException (string.Format (RE.RelationKeyIsNotPrimitiveField, keys.Key));
							}
							PrimitiveFieldMapping relateField = RelateTableMapping.FindFieldMapping (keys.Value) as PrimitiveFieldMapping;
							if (relateField == null) {
								throw new LightDataException (string.Format (RE.RelationKeyIsNotPrimitiveField, keys.Value));
							}
							tables.Add (new RelationKeyValue (masterField, relateField));
						}
						_relationKeyValues = tables.ToArray ();
					}
				}
			}

			foreach (RelationKeyValue kv in _relationKeyValues) {
				yield return kv;
			}
		}



		/// <summary>
		/// 关联方式
		/// </summary>
		public RelationKind Kind {
			get {
				return _relationKind;
			}
		}

		/// <summary>
		/// 关联数据类型
		/// </summary>
		public DataKind ResultDataKind {
			get {
				return _resultDataKind;
			}
		}

		/// <summary>
		/// 关联名称
		/// </summary>
		public string RelationName {
			get {
				return _relationName;
			}
		}

		/// <summary>
		/// 主映射表
		/// </summary>
		public DataEntityMapping MasterTableMapping {
			get {
				return _masterTableMapping;
			}
		}

		/// <summary>
		/// 关联映射表
		/// </summary>
		public DataEntityMapping RelateTableMapping {
			get {
				if (_relateTableMapping == null) {
					_relateTableMapping = DataMapping.GetTableMapping (_relationTableType);
				}
				return _relateTableMapping;
			}
		}


		string _relationProperty = null;

		internal void SetRelationProperty (string relationProperty)
		{
			if (!_relationTableType.IsSubclassOf (typeof(DataEntity))) {
				throw new LightDataException (string.Format (RE.TypeNotInheritFromSpecialType, _relationTableType.Name, "DataEntity"));
			}
			_relationProperty = relationProperty;
		}

		/// <summary>
		/// 关联表的关联映射
		/// </summary>
		public RelationFieldMapping RelateRelationMapping {
			get {
				if (_relateRelationMapping == null) {//如果关联映射表为空,则建立
					if (!_hasLoadRelateRelationMapping) {
						lock (_synobj1) {
							if (!_hasLoadRelateRelationMapping) {
								_hasLoadRelateRelationMapping = true;
								if (!string.IsNullOrEmpty (_relationProperty)) {
									_relateRelationMapping = RelateTableMapping.FindRelateionMapping (_relationProperty);
								}
							}
						}
					}
				}
				return _relateRelationMapping;
			}
		}

		/// <summary>
		/// 关联方式
		/// </summary>
		internal enum RelationKind
		{
			/// <summary>
			/// 一对一关联
			/// </summary>
			OneToOne,
			/// <summary>
			/// 一对多关联
			/// </summary>
			OneToMany
		}

		/// <summary>
		/// 关联数据类型
		/// </summary>
		internal enum DataKind
		{
			/// <summary>
			/// 单对象
			/// </summary>
			SingleObject,
			/// <summary>
			/// 数组
			/// </summary>
			Array,
			/// <summary>
			/// 集合
			/// </summary>
			IList
		}

		internal class RelationKeyValue
		{
			public RelationKeyValue (PrimitiveFieldMapping masterField, PrimitiveFieldMapping relateMapping)
			{
				this._masterField = masterField;
				this._relateField = relateMapping;
			}

			PrimitiveFieldMapping _masterField;

			public PrimitiveFieldMapping MasterField {
				get {
					return _masterField;
				}
			}

			PrimitiveFieldMapping _relateField;

			public PrimitiveFieldMapping RelateField {
				get {
					return _relateField;
				}
			}
		}
	}
}
