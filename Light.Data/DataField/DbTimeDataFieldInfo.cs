using System;

namespace Light.Data
{
	/// <summary>
	/// Db time data field info.
	/// </summary>
	class DbTimeDataFieldInfo:CommonDataFieldInfo
	{
		static DbTimeDataFieldInfo instance = new DbTimeDataFieldInfo ();

		internal static DbTimeDataFieldInfo Instance {
			get {
				return instance;
			}
		}

		private DbTimeDataFieldInfo () : base (typeof(DbTimeType), "Value")
		{
			
		}

		internal override string CreateDataFieldSql (CommandFactory factory, bool isFullName)
		{
			return factory.CreateDataBaseTimeSql ();
		}

		protected override bool EqualsDetail (DataFieldInfo info)
		{
			DbTimeDataFieldInfo target = info as DbTimeDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return true;
			}
			else {
				return false;
			}
		}

		[DataTable]
		class DbTimeType
		{
			[DataField]
			public string Value {
				get;
				set;
			}
		}
	}
}

