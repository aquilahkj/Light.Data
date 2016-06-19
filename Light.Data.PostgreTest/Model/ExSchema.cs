using System;

namespace Light.Data.PostgreTest
{
	[Serializable]
	[DataTable ("Te_CheckValue")]
	public partial class TeCheckValueMini : DataTableEntity
	{
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueMini>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueMini>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueMini>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueMini>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueMini>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueMini>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueMini>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueDefault>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueDefault>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueDefault2>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueDefault2>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueDefault3>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueDefault3>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueOrder1>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueOrder1>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueOrder2>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueOrder2>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueOrder3>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueOrder3>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueOrder4>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueOrder4>.Create ("CheckLevel");

		#endregion

		#region "Static DataFieldInfo"

		public static DataFieldInfo IdField {
			get {
				return idField;
			}
		}

		public static DataFieldInfo CheckIdField {
			get {
				return checkIdField;
			}
		}

		public static DataFieldInfo CheckRateField {
			get {
				return checkRateField;
			}
		}

		public static DataFieldInfo CheckTimeField {
			get {
				return checkTimeField;
			}
		}

		public static DataFieldInfo CheckDateField {
			get {
				return checkDateField;
			}
		}

		public static DataFieldInfo CheckDataField {
			get {
				return checkDataField;
			}
		}

		public static DataFieldInfo CheckLevelField {
			get {
				return checkLevelField;
			}
		}

		#endregion

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

