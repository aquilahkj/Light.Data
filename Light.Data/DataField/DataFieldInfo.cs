using System;

namespace Light.Data
{
	/// <summary>
	/// Data field info.
	/// </summary>
	public class DataFieldInfo<T> : DataFieldInfo where T : class, new()
	{
		/// <summary>
		/// Create the specified name.
		/// </summary>
		/// <param name="name">Name.</param>
		public static DataFieldInfo<T> Create (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException (nameof (name));
			}
			return new DataFieldInfo<T> (name);
		}

		private DataFieldInfo (string name)
			: base (typeof (T), name)
		{

		}
	}

	/// <summary>
	/// Data field info.
	/// </summary>
	public partial class DataFieldInfo : BasicFieldInfo
	{
		public virtual DataFieldInfo CreateAliasTableInfo (string aliasTableName)
		{
			DataFieldInfo info = this.MemberwiseClone () as DataFieldInfo;
			info._aliasTableName = aliasTableName;
			return info;
		}

		internal DataFieldInfo (Type type, string name)
			: this (DataEntityMapping.GetEntityMapping (type), false, name)
		{

		}

		internal DataFieldInfo (DataEntityMapping mapping, bool customName, string name)
			: base (mapping, customName, name)
		{
			//TableMapping = mapping;
			//DataField = TableMapping.FindDataEntityField (name);
			//if (DataField == null) {
			//	DataField = new CustomFieldMapping (name, mapping);
			//}
		}

		internal DataFieldInfo (DataEntityMapping mapping, bool customName, string name, string aliasTableName)
			: base (mapping, customName, name)
		{
			//TableMapping = mapping;
			//DataField = TableMapping.FindDataEntityField (name);
			//if (DataField == null) {
			//	DataField = new CustomFieldMapping (name, mapping);
			//}
			_aliasTableName = aliasTableName;
		}

		internal DataFieldInfo (DataEntityMapping mapping, DataFieldMapping fieldMapping)
			: base (mapping, fieldMapping)
		{
			//TableMapping = mapping;
			//DataField = fieldMapping;
			//_aliasTableName = aliasTableName;
		}

		internal DataFieldInfo (DataEntityMapping mapping, DataFieldMapping fieldMapping, string aliasTableName)
			: base (mapping, fieldMapping)
		{
			//TableMapping = mapping;
			//DataField = fieldMapping;
			_aliasTableName = aliasTableName;
		}

		internal DataFieldInfo (DataFieldMapping fieldMapping)
			: base (fieldMapping.EntityMapping, fieldMapping)
		{
			//TableMapping = fieldMapping.EntityMapping;
			//DataField = fieldMapping;
		}

		internal DataFieldInfo (DataFieldMapping fieldMapping, string aliasTableName)
			: base (fieldMapping.EntityMapping, fieldMapping)
		{
			//TableMapping = fieldMapping.EntityMapping;
			//DataField = fieldMapping;
			_aliasTableName = aliasTableName;
		}

		internal DataFieldInfo (DataEntityMapping mapping)
			: base (mapping)
		{
			//TableMapping = mapping;
		}

		//internal DataFieldInfo (DataEntityMapping mapping, string aliasTableName)
		//{
		//	TableMapping = mapping;
		//	_aliasTableName = aliasTableName;
		//}

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>The position.</value>
		public int Position {
			get {
				if (DataField != null) {
					return DataField.PositionOrder;
				}
				else {
					return -1;
				}
			}
		}

		/// <summary>
		/// Gets the DBtype of the field.
		/// </summary>
		/// <value>The type of the DB.</value>
		internal virtual string DBType {
			get {
				return DataField.DBType;
			}
		}

		protected string _aliasTableName;

		internal virtual string AliasTableName {
			get {
				return _aliasTableName;
			}
			//set {
			//	_aliasTableName = value;
			//}
		}

		internal virtual string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state)
		{
			if (isFullName) {
				string tableName = this._aliasTableName ?? TableMapping.TableName;
				return factory.CreateFullDataFieldSql (tableName, FieldName);
			}
			else {
				return factory.CreateDataFieldSql (FieldName);
			}
		}


		/// <summary>
		/// Tos the parameter.
		/// </summary>
		/// <returns>The parameter.</returns>
		/// <param name="value">Value.</param>
		internal virtual object ToParameter (object value)
		{
			return DataField.ToParameter (value);
		}
	}
}
