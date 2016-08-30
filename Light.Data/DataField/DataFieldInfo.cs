using System;
using System.Collections.Generic;

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
	public partial class DataFieldInfo : BasicFieldInfo, ICloneable
	{
		#region ICloneable implementation
		/// <summary>
		/// Clone this instance.
		/// </summary>
		public object Clone ()
		{
			return this.MemberwiseClone ();
		}

		#endregion

		internal DataFieldInfo (Type type, string name) :
			this (DataEntityMapping.GetEntityMapping (type), name)
		{

		}

		internal DataFieldInfo (DataEntityMapping mapping, string name)
		{
			TableMapping = mapping;
			DataField = TableMapping.FindDataEntityField (name);
			if (DataField == null) {
				DataField = new CustomFieldMapping (name, mapping);
			}
		}

		internal DataFieldInfo (DataFieldMapping fieldMapping)
		{
			if (fieldMapping != null) {
				TableMapping = fieldMapping.EntityMapping;
				DataField = fieldMapping;
			}
		}

		internal DataFieldInfo (DataEntityMapping mapping)
		{
			TableMapping = mapping;
		}

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

		string _aliasTableName;

		internal virtual string AliasTableName {
			get {
				return _aliasTableName;
			}
			set {
				_aliasTableName = value;
			}
		}

		//internal virtual string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter [] dataParameters)
		//{
		//	dataParameters = null;
		//	if (isFullName) {
		//		string tableName = this._aliasTableName ?? TableMapping.TableName;
		//		return factory.CreateFullDataFieldSql (tableName, FieldName);
		//	}
		//	else {
		//		return factory.CreateDataFieldSql (FieldName);
		//	}
		//}

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
			return base.DataField.ToParameter (value);
		}
	}
}
