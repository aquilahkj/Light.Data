using System;

namespace Light.Data.MysqlTest
{
	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueMini : DataTableEntity
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueDefault : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false, DefaultValue = 2)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DefaultValue = 0.02)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DefaultValue = DefaultTime.Now)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DefaultValue = DefaultTime.Today)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DefaultValue = "test")]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DefaultValue = CheckLevelType.High)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueDefault2 : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false, DefaultValue = 2)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DefaultValue = 0.02)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DefaultValue = DefaultTime.Now)]
		public DateTime CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DefaultValue = DefaultTime.Today)]
		public DateTime CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DefaultValue = "test")]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DefaultValue = CheckLevelType.High)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueDefault3 : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false, DefaultValue = 2)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DefaultValue = 0.02)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DefaultValue = "2016-01-19 22:10:10")]
		public DateTime CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DefaultValue = "2016-01-19")]
		public DateTime CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DefaultValue = "test")]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DefaultValue = CheckLevelType.High)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}


	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueOrder1 : DataTableEntity
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true, DataOrder = 1)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false, DataOrder = 2)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DataOrder = 3)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DataOrder = 4)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DataOrder = 5)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DataOrder = 6)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DataOrder = 7)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueOrder2 : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true, DataOrder = 7)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false, DataOrder = 6)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DataOrder = 5)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DataOrder = 4)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DataOrder = 3)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DataOrder = 2)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DataOrder = 1)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueOrder3 : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DataOrder = 4)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DataOrder = 5)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false, DataOrder = 6)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false, DataOrder = 7)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueOrder4 : DataTableEntity
	{

		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField ("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false)]
		public int? CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double? checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false, DataOrder = 6)]
		public double? CheckRate {
			get { 
				return this.checkRate; 
			}
			set { 
				this.checkRate = value; 
			}
		}

		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField ("CheckTime", IsNullable = false, DataOrder = 5)]
		public DateTime? CheckTime {
			get { 
				return this.checkTime; 
			}
			set { 
				this.checkTime = value; 
			}
		}

		private DateTime? checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField ("CheckDate", IsNullable = false, DataOrder = 4)]
		public DateTime? CheckDate {
			get { 
				return this.checkDate; 
			}
			set { 
				this.checkDate = value; 
			}
		}

		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField ("CheckData", IsNullable = false)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType? checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false)]
		public CheckLevelType? CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}
}

