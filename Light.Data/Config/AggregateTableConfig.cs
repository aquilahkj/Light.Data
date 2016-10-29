using System;

namespace Light.Data
{
	/// <summary>
	/// Aggregate table config.
	/// </summary>
	class AggregateTableConfig : TableConfig, IAggregateTableConfig
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.AggregateTableConfig"/> class.
		/// </summary>
		/// <param name="dataType">Data type.</param>
		public AggregateTableConfig (Type dataType)
		{
			if (dataType == null) {
				throw new ArgumentNullException (nameof (dataType));
			}
			DataType = dataType;
		}

		/// <summary>
		/// Gets the type of the data.
		/// </summary>
		/// <value>The type of the data.</value>
		public Type DataType {
			get;
			private set;
		}
	}
}
