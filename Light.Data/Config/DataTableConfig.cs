using System;

namespace Light.Data
{
	/// <summary>
	/// Data table config.
	/// </summary>
	class DataTableConfig : TableConfig,IDataTableConfig
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.DataTableConfig"/> class.
		/// </summary>
		/// <param name="dataType">Data type.</param>
		public DataTableConfig (Type dataType)
		{
			if (dataType == null) {
				throw new ArgumentNullException ("dataType");
			}
			DataType = dataType;
			IsEntityTable = true;
		}

		/// <summary>
		/// Gets the type of the data.
		/// </summary>
		/// <value>The type of the data.</value>
		public Type DataType {
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the name of the table.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the extend parameters.
		/// </summary>
		/// <value>The extend parameters.</value>
		public string ExtendParams {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is entity table.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsEntityTable {
			get;
			set;
		}
	}
}
