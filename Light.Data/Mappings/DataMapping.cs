using System;
using System.Collections.Generic;
using System.Data;

using Light.Data;

namespace Light.Data
{
	/// <summary>
	/// Data mapping.
	/// </summary>
	abstract class DataMapping : IFieldCollection
	{
		#region static

		static object _synobj = new object ();

		static Dictionary<Type, DataMapping> _defaultMapping = new Dictionary<Type, DataMapping> ();

		/// <summary>
		/// Gets the mapping.
		/// </summary>
		/// <returns>The mapping.</returns>
		/// <param name="type">Type.</param>
		public static DataMapping GetMapping (Type type)
		{
			Dictionary<Type, DataMapping> mappings = _defaultMapping;
			DataMapping mapping;
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
		/// Gets the table mapping.
		/// </summary>
		/// <returns>The table mapping.</returns>
		/// <param name="type">Type.</param>
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
		/// Gets the entity mapping.
		/// </summary>
		/// <returns>The entity mapping.</returns>
		/// <param name="type">Type.</param>
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
		/// Creates the mapping.
		/// </summary>
		/// <returns>The mapping.</returns>
		/// <param name="type">Type.</param>
		private static DataMapping CreateMapping (Type type)
		{
			string tableName;
			string extentParam;
			bool isEntityTable;
			DataMapping dataMapping;

			IDataTableConfig config = ConfigManager.LoadDataTableConfig (type);
			if (config != null) {
				tableName = config.TableName;
				extentParam = config.ExtendParams;
				isEntityTable = config.IsEntityTable;
			}
			else {
				throw new LightDataException (string.Format (RE.TheTypeOfDataEntityIsNoConfig, type.Name));
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

		protected List<FieldMapping> _fieldList = new List<FieldMapping> ();

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.DataMapping"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		protected DataMapping (Type type)
		{
			this.objectType = type;
		}

		Type objectType;

		/// <summary>
		/// Gets or sets the type of the object.
		/// </summary>
		/// <value>The type of the object.</value>
		public Type ObjectType {
			get {
				return objectType;
			}
			protected set {
				objectType = value;
			}
		}

		ExtendParamsCollection extentParams;

		/// <summary>
		/// Gets or sets the extent parameters.
		/// </summary>
		/// <value>The extent parameters.</value>
		public ExtendParamsCollection ExtentParams {
			get {
				return extentParams;
			}
			protected set {
				extentParams = value;
			}
		}

		#region IFieldCollection 成员

		public IEnumerable<FieldMapping> FieldMappings {
			get {
				foreach (FieldMapping item in this._fieldList) {
					yield return item;
				}
			}
		}

		public int FieldCount {
			get {
				return this._fieldList.Count;
			}
		}

		public virtual FieldMapping FindFieldMapping (string fieldName)
		{
			FieldMapping mapping;
			_fieldMappingDictionary.TryGetValue (fieldName, out mapping);
			return mapping;
		}

		public abstract object LoadData (DataContext context, IDataReader datareader, object state);

		public abstract object InitialData ();

		#endregion
	}
}
