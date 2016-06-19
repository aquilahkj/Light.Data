using System;

namespace Light.Data.OracleTest
{
	[Serializable]
	[DataTable("Te_User")]
	public partial class TeUser2
	{
		#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUser2>.Create("Id");

		static readonly DataFieldInfo accountField = DataFieldInfo<TeUser2>.Create("Account");

		static readonly DataFieldInfo passwordField = DataFieldInfo<TeUser2>.Create("Password");

		static readonly DataFieldInfo nickNameField = DataFieldInfo<TeUser2>.Create("NickName");

		static readonly DataFieldInfo genderField = DataFieldInfo<TeUser2>.Create("Gender");

		static readonly DataFieldInfo birthdayField = DataFieldInfo<TeUser2>.Create("Birthday");

		static readonly DataFieldInfo telephoneField = DataFieldInfo<TeUser2>.Create("Telephone");

		static readonly DataFieldInfo emailField = DataFieldInfo<TeUser2>.Create("Email");

		static readonly DataFieldInfo addressField = DataFieldInfo<TeUser2>.Create("Address");

		static readonly DataFieldInfo levelIdField = DataFieldInfo<TeUser2>.Create("LevelId");

		static readonly DataFieldInfo regTimeField = DataFieldInfo<TeUser2>.Create("RegTime");

		static readonly DataFieldInfo lastLoginTimeField = DataFieldInfo<TeUser2>.Create("LastLoginTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUser2>.Create("Status");

		static readonly DataFieldInfo hotRateField = DataFieldInfo<TeUser2>.Create("HotRate");

		static readonly DataFieldInfo areaField = DataFieldInfo<TeUser2>.Create("Area");

		static readonly DataFieldInfo deleteFlagField = DataFieldInfo<TeUser2>.Create("DeleteFlag");

		static readonly DataFieldInfo refereeIdField = DataFieldInfo<TeUser2>.Create("RefereeId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeUser2>.Create("CheckPoint");

		static readonly DataFieldInfo checkStatusField = DataFieldInfo<TeUser2>.Create("CheckStatus");

		static readonly DataFieldInfo checkLevelTypeField = DataFieldInfo<TeUser2>.Create("CheckLevelType");

		static readonly DataFieldInfo loginTimesField = DataFieldInfo<TeUser2>.Create("LoginTimes");

		static readonly DataFieldInfo markField = DataFieldInfo<TeUser2>.Create("Mark");

		#endregion

		#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
		{
			get {
				return idField;
			}
		}

		public static DataFieldInfo AccountField
		{
			get {
				return accountField;
			}
		}

		public static DataFieldInfo PasswordField
		{
			get {
				return passwordField;
			}
		}

		public static DataFieldInfo NickNameField
		{
			get {
				return nickNameField;
			}
		}

		public static DataFieldInfo GenderField
		{
			get {
				return genderField;
			}
		}

		public static DataFieldInfo BirthdayField
		{
			get {
				return birthdayField;
			}
		}

		public static DataFieldInfo TelephoneField
		{
			get {
				return telephoneField;
			}
		}

		public static DataFieldInfo EmailField
		{
			get {
				return emailField;
			}
		}

		public static DataFieldInfo AddressField
		{
			get {
				return addressField;
			}
		}

		public static DataFieldInfo LevelIdField
		{
			get {
				return levelIdField;
			}
		}

		public static DataFieldInfo RegTimeField
		{
			get {
				return regTimeField;
			}
		}

		public static DataFieldInfo LastLoginTimeField
		{
			get {
				return lastLoginTimeField;
			}
		}

		public static DataFieldInfo StatusField
		{
			get {
				return statusField;
			}
		}

		public static DataFieldInfo HotRateField
		{
			get {
				return hotRateField;
			}
		}

		public static DataFieldInfo AreaField
		{
			get {
				return areaField;
			}
		}

		public static DataFieldInfo DeleteFlagField
		{
			get {
				return deleteFlagField;
			}
		}

		public static DataFieldInfo RefereeIdField
		{
			get {
				return refereeIdField;
			}
		}

		public static DataFieldInfo CheckPointField
		{
			get {
				return checkPointField;
			}
		}

		public static DataFieldInfo CheckStatusField
		{
			get {
				return checkStatusField;
			}
		}

		public static DataFieldInfo CheckLevelTypeField
		{
			get {
				return checkLevelTypeField;
			}
		}

		public static DataFieldInfo LoginTimesField
		{
			get {
				return loginTimesField;
			}
		}

		public static DataFieldInfo MarkField
		{
			get {
				return markField;
			}
		}

		#endregion

		#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
		public int Id
		{
			get { 
				return this.id; 
			}
			set { 
				this.id = value; 
			}
		}
		private string account;

		/// <summary>
		/// Account
		/// </summary>
		/// <value></value>
		[DataField("Account")]
		public string Account
		{
			get { 
				return this.account; 
			}
			set { 
				this.account = value; 
			}
		}
		private string password;

		/// <summary>
		/// Password
		/// </summary>
		/// <value></value>
		[DataField("Password")]
		public string Password
		{
			get { 
				return this.password; 
			}
			set { 
				this.password = value; 
			}
		}
		private string nickName;

		/// <summary>
		/// NickName
		/// </summary>
		/// <value></value>
		[DataField("NickName", IsNullable = true)]
		public string NickName
		{
			get { 
				return this.nickName; 
			}
			set { 
				this.nickName = value; 
			}
		}
		private GenderType gender;

		/// <summary>
		/// #EnumType:GenderType#sexy
		/// </summary>
		/// <value></value>
		[DataField("Gender")]
		public GenderType Gender
		{
			get { 
				return this.gender; 
			}
			set { 
				this.gender = value; 
			}
		}
		private DateTime birthday;

		/// <summary>
		/// Birthday
		/// </summary>
		/// <value></value>
		[DataField("Birthday")]
		public DateTime Birthday
		{
			get { 
				return this.birthday; 
			}
			set { 
				this.birthday = value; 
			}
		}
		private string telephone;

		/// <summary>
		/// Telephone
		/// </summary>
		/// <value></value>
		[DataField("Telephone", IsNullable = true)]
		public string Telephone
		{
			get { 
				return this.telephone; 
			}
			set { 
				this.telephone = value; 
			}
		}
		private string email;

		/// <summary>
		/// Email
		/// </summary>
		/// <value></value>
		[DataField("Email", IsNullable = true)]
		public string Email
		{
			get { 
				return this.email; 
			}
			set { 
				this.email = value; 
			}
		}
		private string address;

		/// <summary>
		/// Address
		/// </summary>
		/// <value></value>
		[DataField("Address", IsNullable = true)]
		public string Address
		{
			get { 
				return this.address; 
			}
			set { 
				this.address = value; 
			}
		}
		private int levelId;

		/// <summary>
		/// LevelId
		/// </summary>
		/// <value></value>
		[DataField("LevelId")]
		public int LevelId
		{
			get { 
				return this.levelId; 
			}
			set { 
				this.levelId = value; 
			}
		}
		private DateTime regTime;

		/// <summary>
		/// RegTime
		/// </summary>
		/// <value></value>
		[DataField("RegTime")]
		public DateTime RegTime
		{
			get { 
				return this.regTime; 
			}
			set { 
				this.regTime = value; 
			}
		}
		private DateTime? lastLoginTime;

		/// <summary>
		/// LastLoginTime
		/// </summary>
		/// <value></value>
		[DataField("LastLoginTime", IsNullable = true)]
		public DateTime? LastLoginTime
		{
			get { 
				return this.lastLoginTime; 
			}
			set { 
				this.lastLoginTime = value; 
			}
		}
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
		public int Status
		{
			get { 
				return this.status; 
			}
			set { 
				this.status = value; 
			}
		}
		private double hotRate;

		/// <summary>
		/// HotRate
		/// </summary>
		/// <value></value>
		[DataField("HotRate")]
		public double HotRate
		{
			get { 
				return this.hotRate; 
			}
			set { 
				this.hotRate = value; 
			}
		}
		private int? area;

		/// <summary>
		/// Area
		/// </summary>
		/// <value></value>
		[DataField("Area", IsNullable = true)]
		public int? Area
		{
			get { 
				return this.area; 
			}
			set { 
				this.area = value; 
			}
		}
		private bool deleteFlag;

		/// <summary>
		/// DeleteFlag
		/// </summary>
		/// <value></value>
		[DataField("DeleteFlag")]
		public bool DeleteFlag
		{
			get { 
				return this.deleteFlag; 
			}
			set { 
				this.deleteFlag = value; 
			}
		}
		private int? refereeId;

		/// <summary>
		/// RefereeId
		/// </summary>
		/// <value></value>
		[DataField("RefereeId", IsNullable = true)]
		public int? RefereeId
		{
			get { 
				return this.refereeId; 
			}
			set { 
				this.refereeId = value; 
			}
		}
		private double? checkPoint;

		/// <summary>
		/// Check_Point
		/// </summary>
		/// <value></value>
		[DataField("Check_Point", IsNullable = true)]
		public double? CheckPoint
		{
			get { 
				return this.checkPoint; 
			}
			set { 
				this.checkPoint = value; 
			}
		}
		private bool? checkStatus;

		/// <summary>
		/// Check_Status
		/// </summary>
		/// <value></value>
		[DataField("Check_Status", IsNullable = true)]
		public bool? CheckStatus
		{
			get { 
				return this.checkStatus; 
			}
			set { 
				this.checkStatus = value; 
			}
		}
		private CheckLevelType? checkLevelType;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelType", IsNullable = true)]
		public CheckLevelType? CheckLevelType
		{
			get { 
				return this.checkLevelType; 
			}
			set { 
				this.checkLevelType = value; 
			}
		}
		private int loginTimes;

		/// <summary>
		/// LoginTimes
		/// </summary>
		/// <value></value>
		[DataField("LoginTimes")]
		public int LoginTimes
		{
			get { 
				return this.loginTimes; 
			}
			set { 
				this.loginTimes = value; 
			}
		}
		private int mark;

		/// <summary>
		/// Mark
		/// </summary>
		/// <value></value>
		[DataField("Mark")]
		public int Mark
		{
			get { 
				return this.mark; 
			}
			set { 
				this.mark = value; 
			}
		}
		#endregion
	}
}

