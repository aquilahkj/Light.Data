using System;
using System.Collections.Generic;
using System.Text;
using Light.Data;

namespace Light.Data.MysqlTest
{
    [Serializable]
    [DataTable("Te_Article")]
    public partial class TeArticle : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeArticle>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeArticle>.Create("UserId");

		static readonly DataFieldInfo titleField = DataFieldInfo<TeArticle>.Create("Title");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeArticle>.Create("Content");

		static readonly DataFieldInfo publishTimeField = DataFieldInfo<TeArticle>.Create("PublishTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticle>.Create("Status");

		static readonly DataFieldInfo readNumField = DataFieldInfo<TeArticle>.Create("ReadNum");

		static readonly DataFieldInfo praiseNumField = DataFieldInfo<TeArticle>.Create("PraiseNum");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<TeArticle>.Create("LastModifyTime");

		static readonly DataFieldInfo lastCommentTimeField = DataFieldInfo<TeArticle>.Create("LastCommentTime");

		static readonly DataFieldInfo tagsField = DataFieldInfo<TeArticle>.Create("Tags");

		static readonly DataFieldInfo columnIdField = DataFieldInfo<TeArticle>.Create("ColumnId");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo TitleField
        {
            get {
                return titleField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo PublishTimeField
        {
            get {
                return publishTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ReadNumField
        {
            get {
                return readNumField;
            }
        }

		public static DataFieldInfo PraiseNumField
        {
            get {
                return praiseNumField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
            }
        }

		public static DataFieldInfo LastCommentTimeField
        {
            get {
                return lastCommentTimeField;
            }
        }

		public static DataFieldInfo TagsField
        {
            get {
                return tagsField;
            }
        }

		public static DataFieldInfo ColumnIdField
        {
            get {
                return columnIdField;
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
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string title;

		/// <summary>
		/// Title
		/// </summary>
		/// <value></value>
		[DataField("Title")]
        public string Title
        {
            get { 
            	return this.title; 
            }
            set { 
            	this.title = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		private DateTime publishTime;

		/// <summary>
		/// PublishTime
		/// </summary>
		/// <value></value>
		[DataField("PublishTime")]
        public DateTime PublishTime
        {
            get { 
            	return this.publishTime; 
            }
            set { 
            	this.publishTime = value; 
            }
        }
		private string status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public string Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int readNum;

		/// <summary>
		/// ReadNum
		/// </summary>
		/// <value></value>
		[DataField("ReadNum")]
        public int ReadNum
        {
            get { 
            	return this.readNum; 
            }
            set { 
            	this.readNum = value; 
            }
        }
		private int praiseNum;

		/// <summary>
		/// PraiseNum
		/// </summary>
		/// <value></value>
		[DataField("PraiseNum")]
        public int PraiseNum
        {
            get { 
            	return this.praiseNum; 
            }
            set { 
            	this.praiseNum = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// LastModifyTime
		/// </summary>
		/// <value></value>
		[DataField("LastModifyTime")]
        public DateTime LastModifyTime
        {
            get { 
            	return this.lastModifyTime; 
            }
            set { 
            	this.lastModifyTime = value; 
            }
        }
		private DateTime lastCommentTime;

		/// <summary>
		/// LastCommentTime
		/// </summary>
		/// <value></value>
		[DataField("LastCommentTime")]
        public DateTime LastCommentTime
        {
            get { 
            	return this.lastCommentTime; 
            }
            set { 
            	this.lastCommentTime = value; 
            }
        }
		private string tags;

		/// <summary>
		/// Tags
		/// </summary>
		/// <value></value>
		[DataField("Tags", IsNullable = true)]
        public string Tags
        {
            get { 
            	return this.tags; 
            }
            set { 
            	this.tags = value; 
            }
        }
		private string columnId;

		/// <summary>
		/// ColumnId
		/// </summary>
		/// <value></value>
		[DataField("ColumnId", IsNullable = true)]
        public string ColumnId
        {
            get { 
            	return this.columnId; 
            }
            set { 
            	this.columnId = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_ArticleColumn")]
    public partial class TeArticleColumn : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo columnIdField = DataFieldInfo<TeArticleColumn>.Create("ColumnId");

		static readonly DataFieldInfo columnNameField = DataFieldInfo<TeArticleColumn>.Create("ColumnName");

		static readonly DataFieldInfo parentIdField = DataFieldInfo<TeArticleColumn>.Create("ParentId");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticleColumn>.Create("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeArticleColumn>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo ColumnIdField
        {
            get {
                return columnIdField;
            }
        }

		public static DataFieldInfo ColumnNameField
        {
            get {
                return columnNameField;
            }
        }

		public static DataFieldInfo ParentIdField
        {
            get {
                return parentIdField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string columnId;

		/// <summary>
		/// ColumnId
		/// </summary>
		/// <value></value>
		[DataField("ColumnId", IsPrimaryKey = true)]
        public string ColumnId
        {
            get { 
            	return this.columnId; 
            }
            set { 
            	this.columnId = value; 
            }
        }
		private string columnName;

		/// <summary>
		/// ColumnName
		/// </summary>
		/// <value></value>
		[DataField("ColumnName")]
        public string ColumnName
        {
            get { 
            	return this.columnName; 
            }
            set { 
            	this.columnName = value; 
            }
        }
		private string parentId;

		/// <summary>
		/// ParentId
		/// </summary>
		/// <value></value>
		[DataField("ParentId")]
        public string ParentId
        {
            get { 
            	return this.parentId; 
            }
            set { 
            	this.parentId = value; 
            }
        }
		private string status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public string Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_ArticleComment")]
    public partial class TeArticleComment : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeArticleComment>.Create("Id");

		static readonly DataFieldInfo articleIdField = DataFieldInfo<TeArticleComment>.Create("ArticleId");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeArticleComment>.Create("UserId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeArticleComment>.Create("Content");

		static readonly DataFieldInfo publishTimeField = DataFieldInfo<TeArticleComment>.Create("PublishTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticleComment>.Create("Status");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo ArticleIdField
        {
            get {
                return articleIdField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo PublishTimeField
        {
            get {
                return publishTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
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
		private int articleId;

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		[DataField("ArticleId")]
        public int ArticleId
        {
            get { 
            	return this.articleId; 
            }
            set { 
            	this.articleId = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		private DateTime publishTime;

		/// <summary>
		/// PublishTime
		/// </summary>
		/// <value></value>
		[DataField("PublishTime")]
        public DateTime PublishTime
        {
            get { 
            	return this.publishTime; 
            }
            set { 
            	this.publishTime = value; 
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
		#endregion
    }

    [Serializable]
    [DataTable("Te_User")]
    public partial class TeUser : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUser>.Create("Id");

		static readonly DataFieldInfo accountField = DataFieldInfo<TeUser>.Create("Account");

		static readonly DataFieldInfo passwordField = DataFieldInfo<TeUser>.Create("Password");

		static readonly DataFieldInfo nickNameField = DataFieldInfo<TeUser>.Create("NickName");

		static readonly DataFieldInfo genderField = DataFieldInfo<TeUser>.Create("Gender");

		static readonly DataFieldInfo birthdayField = DataFieldInfo<TeUser>.Create("Birthday");

		static readonly DataFieldInfo telephoneField = DataFieldInfo<TeUser>.Create("Telephone");

		static readonly DataFieldInfo emailField = DataFieldInfo<TeUser>.Create("Email");

		static readonly DataFieldInfo addressField = DataFieldInfo<TeUser>.Create("Address");

		static readonly DataFieldInfo levelIdField = DataFieldInfo<TeUser>.Create("LevelId");

		static readonly DataFieldInfo regTimeField = DataFieldInfo<TeUser>.Create("RegTime");

		static readonly DataFieldInfo lastLoginTimeField = DataFieldInfo<TeUser>.Create("LastLoginTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUser>.Create("Status");

		static readonly DataFieldInfo hotRateField = DataFieldInfo<TeUser>.Create("HotRate");

		static readonly DataFieldInfo areaField = DataFieldInfo<TeUser>.Create("Area");

		static readonly DataFieldInfo deleteFlagField = DataFieldInfo<TeUser>.Create("DeleteFlag");

		static readonly DataFieldInfo refereeIdField = DataFieldInfo<TeUser>.Create("RefereeId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeUser>.Create("CheckPoint");

		static readonly DataFieldInfo checkStatusField = DataFieldInfo<TeUser>.Create("CheckStatus");

		static readonly DataFieldInfo checkLevelTypeField = DataFieldInfo<TeUser>.Create("CheckLevelType");

		static readonly DataFieldInfo loginTimesField = DataFieldInfo<TeUser>.Create("LoginTimes");

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
		/// CheckPoint
		/// </summary>
		/// <value></value>
		[DataField("CheckPoint", IsNullable = true)]
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
		/// CheckStatus
		/// </summary>
		/// <value></value>
		[DataField("CheckStatus", IsNullable = true)]
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
		[DataField("CheckLevelType", IsNullable = true)]
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
		#endregion
    }

    [Serializable]
    [DataTable("Te_UserLevel")]
    public partial class TeUserLevel : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUserLevel>.Create("Id");

		static readonly DataFieldInfo levelNameField = DataFieldInfo<TeUserLevel>.Create("LevelName");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUserLevel>.Create("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeUserLevel>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo LevelNameField
        {
            get {
                return levelNameField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private string levelName;

		/// <summary>
		/// LevelName
		/// </summary>
		/// <value></value>
		[DataField("LevelName")]
        public string LevelName
        {
            get { 
            	return this.levelName; 
            }
            set { 
            	this.levelName = value; 
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
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		#endregion
    }

}

