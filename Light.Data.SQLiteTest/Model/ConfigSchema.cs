using System;

namespace Light.Data.SQLiteTest
{
	[Serializable]
	[DataTable ("TeCheckValueConfig")]
	public partial class TeCheckValueConfig : DataTableEntity
	{
		#region "Static Field"

		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValueConfig>.Create ("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValueConfig>.Create ("CheckLevel");

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

		private int checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField ("CheckId", IsNullable = false)]
		public int CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField ("CheckRate", IsNullable = false)]
		public double CheckRate {
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
		[DataField ("CheckTime", IsNullable = false)]
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
		[DataField ("CheckDate", IsNullable = false)]
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
		[DataField ("CheckData", IsNullable = false)]
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField ("CheckLevel", IsNullable = false)]
		public CheckLevelType CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	public partial class TeCheckValueConfig0
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		public int CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		public double CheckRate {
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
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		public CheckLevelType CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	public partial class TeCheckValueConfig1
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		public int Id {
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}

		private int checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		public int CheckId {
			get { 
				return this.checkId; 
			}
			set { 
				this.checkId = value; 
			}
		}

		private double checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		public double CheckRate {
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
		public string CheckData {
			get { 
				return this.checkData; 
			}
			set { 
				this.checkData = value; 
			}
		}

		private CheckLevelType checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		public CheckLevelType CheckLevel {
			get { 
				return this.checkLevel; 
			}
			set { 
				this.checkLevel = value; 
			}
		}

		#endregion
	}

	public partial class TeCheckValueConfig2
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
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

	public partial class TeCheckValueConfig3
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
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

	public partial class TeCheckValueConfigS1
	{
		#region "Data Property"

		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
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

	public partial class TeCheckValueConfigC1:TeCheckValueConfig2
	{
		
	}

	public partial class TeCheckValueConfigC2:TeCheckValueConfig2
	{
		
	}

	public partial class TeCheckValueConfigC3:TeCheckValueConfig2
	{

	}

	public partial class TeCheckValueConfigC4:TeCheckValueConfig2
	{

	}

	public partial class TeCheckValueConfigA1:TeCheckValueConfig2
	{

	}

	[AggregateTable]
	public partial class LevelIdAggConfig
	{
		int levelId;

		[AggregateField ("LevelId")]
		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;

		[AggregateField ("Data1")]
		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}
		
	public partial class LevelIdAggConfig0
	{
		int levelId;

		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;

		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}

	public partial class LevelIdAggConfig1
	{
		int levelId;

		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;

		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}

	public partial class LevelIdAggConfig2
	{
		int levelId;

		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;

		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}

	public partial class LevelIdAggConfigA1
	{
		int levelId;

		public int LevelId {
			get {
				return levelId;
			}
			set {
				levelId = value;
			}
		}

		int data;

		public int Data {
			get {
				return data;
			}
			set {
				data = value;
			}
		}
	}
}

